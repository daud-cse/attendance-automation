using pnsms.Entities.Models;
using pnsms.Entities.ViewModels;
using pnsms.utility;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service.ViewModels
{
    public interface IVmFeesCollectionService
    {
        IEnumerable<Student> GetAllStudents(FeesGenerateAcademic feesColeectionAcademicModel);
        VmFeesGenerate CreateNew(int institutionId);
        VmFeesGenerate Save(IUnitOfWork _unitOfWork, VmFeesGenerate VmFeesModel);
    }

    public class VmFeesCollectionService : IVmFeesCollectionService
    {
        private readonly IStudentService _studentService;
        private readonly IFeesHeadService _feesHeedService;
        private readonly IFeesGenerateService _feesGenerateService;
        private readonly IFeesGenerateAcademicService _feesGenerateAcademicService;
        private readonly IAcademicBranchService _branchService;
        private readonly IAcademicClassService _classService;
        private readonly IAcademicGroupService _groupService;
        private readonly IAcademicVersionService _versionService;
        public VmFeesCollectionService(
              IStudentService studentService
            , IFeesHeadService feesHeedService
            , IFeesGenerateService feesGenerateService
            , IFeesGenerateAcademicService feesGenerateAcademicService
            , IAcademicBranchService branchService
            , IAcademicClassService classService
            , IAcademicGroupService groupService
            , IAcademicVersionService versionService
            )
        {

            _studentService = studentService;
            _feesHeedService = feesHeedService;
            _feesGenerateService = feesGenerateService;
            _feesGenerateAcademicService = feesGenerateAcademicService;
            _branchService = branchService;
            _classService = classService;
            _groupService = groupService;
            _versionService = versionService;
        }

        public IEnumerable<Student> GetAllStudents(FeesGenerateAcademic feesColeectionAcademicModel)
        {
            var predicate = PredicateBuilder.True<Student>();
            predicate = predicate.And(p => p.UserInfo.InstituteId >= feesColeectionAcademicModel.InstituteId && p.UserInfo.IsActive);
            if (feesColeectionAcademicModel != null)
            {

                if (feesColeectionAcademicModel.AcademicBranchId > 0)
                    predicate = predicate.And(p => p.CurrentAcademicBranchId == feesColeectionAcademicModel.AcademicBranchId);

                if (feesColeectionAcademicModel.AcademicClassId > 0)
                    predicate = predicate.And(p => p.CurrentAcademicClassId == feesColeectionAcademicModel.AcademicClassId);

                if (feesColeectionAcademicModel.AcademicGroupId > 0)
                    predicate = predicate.And(p => p.CurrentAcademicGroupId == feesColeectionAcademicModel.AcademicGroupId);

                if (feesColeectionAcademicModel.AcademicVerssionId > 0)
                    predicate = predicate.And(p => p.CurrentAcademicVerssionId == feesColeectionAcademicModel.AcademicVerssionId);

            }
            return _studentService.Query(predicate)
                .Include(s => s.UserInfo).Select();
        }

        public VmFeesGenerate CreateNew(int institutionId)
        {

            VmFeesGenerate VmfeesCollection = new VmFeesGenerate();
            var feedList = _feesHeedService.GetFeesHeads(institutionId, true);

            var feesGenerateHeadList = new List<FeesGenerateHead>();

            if (feedList.Count() > 0)
            {
                foreach (FeesHead item in feedList)
                {
                    FeesGenerateHead entity = new FeesGenerateHead();
                    entity.HeadName = item.Name;
                    entity.FeesHeadId = item.Id;
                    entity.Amount = 0;
                    feesGenerateHeadList.Add(entity);

                }

                VmfeesCollection.FeesGenerateHeadList = feesGenerateHeadList;
            }
            VmfeesCollection.FeesGenerateAcademic = new FeesGenerateAcademic();

            var branchList = _branchService.GetKVP(institutionId);
            VmfeesCollection.FeesGenerateAcademic.AcademicBranchList = branchList;

            VmfeesCollection.FeesGenerateAcademic.AcademicBranchId = branchList.FirstOrDefault().Key;
            VmfeesCollection.FeesGenerateAcademic.AcademicClassList = _classService.GetKVP(institutionId);
            VmfeesCollection.FeesGenerateAcademic.AcademicGroupList = _groupService.GetKVP(institutionId);
            VmfeesCollection.FeesGenerateAcademic.AcademicVerssionList = _versionService.GetKVP(institutionId);

            var feesHeads = new List<KeyValuePair<int, string>>();
            feedList.ToList().ForEach(c => feesHeads.Add(new KeyValuePair<int, string>(c.Id, c.Name)));
            VmfeesCollection.FeesHeadList = feesHeads;

            VmfeesCollection.FeesGenerate = new FeesGenerate();

            VmfeesCollection.FeesGenerate.GenerationDate = DateTime.Now.Date;
            VmfeesCollection.FeesGenerate.DueDate = DateTime.Now.Date;

            return VmfeesCollection;
        }

        public VmFeesGenerate Save(IUnitOfWork _unitOfWork, VmFeesGenerate VmFeesModel)
        {

            int day = 1;
            int month = 0;
            int year = 0;

            month = Convert.ToInt32(VmFeesModel.FeesGenerate.ForTheMonth.ToString());
            year = Convert.ToInt32(VmFeesModel.FeesGenerate.ForTheYear.ToString());

            System.DateTime newdate = new DateTime(year, month, day);

            VmFeesModel.FeesGenerate.ForTheDate = newdate;

            //insert data to fees Genrate Table
            _feesGenerateService.Insert(VmFeesModel.FeesGenerate);
            _unitOfWork.SaveChanges();

            int feesGenerateId = VmFeesModel.FeesGenerate.Id;

            if (feesGenerateId != 0)
            {
                //insert data to fees Genrate Academic Table
                VmFeesModel.FeesGenerateAcademic.FeesGenerateId = feesGenerateId;
                _feesGenerateAcademicService.Insert(VmFeesModel.FeesGenerateAcademic);

                if (VmFeesModel.FeesGenerateHeadList != null)
                    foreach (var details in VmFeesModel.FeesGenerateHeadList)
                    {
                        
                    }
            
            }

            return VmFeesModel;

        }
    
    
    }
}
