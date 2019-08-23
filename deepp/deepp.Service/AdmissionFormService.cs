using deepp.Entities.Models;
using deepp.Entities.ViewModels;
using deepp.utility;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Service
{
    public interface IAdmissionFormService : IService<AdmissionForm>
    {
        AdmissionForm newadmissionForm(int instituteId);
        AdmissionForm GetDetailsByApplicantFormId(int applicantFormId);
        IEnumerable<AdmissionForm> GetAllBySearch(VmSearch<AdmissionForm> searchModel);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, AdmissionForm admissionForm);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, AdmissionForm admissionForm);
        void Insert(AdmissionForm admissionForm);
    }
    public class AdmissionFormService : Service<AdmissionForm>, IAdmissionFormService
    {
        private readonly IRepositoryAsync<AdmissionForm> _redeeppitory;
        private readonly IGenderService _genderService;
        private readonly INationalityService _nationalityService;
        private readonly IReligionService _religionService;
        private readonly IBloodGroupService _bloodGroupService;
        private readonly IAcademicSessionService _academicSessionService;
        private readonly IAcademicClassService _academicClassService;
        private readonly IAcademicBranchService _academicBranchService;
        private readonly IAddressTypeService _addressTypeService;
        public AdmissionFormService(IRepositoryAsync<AdmissionForm> redeeppitory
            ,IGenderService genderService
            ,INationalityService nationalityService
            ,IReligionService religionService
            ,IBloodGroupService bloodGroupService
            ,IAcademicSessionService academicSessionService
            ,IAcademicClassService academicClassService
            ,IAcademicBranchService academicBranchService
            , IAddressTypeService addressTypeService)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
            _genderService = genderService;
            _nationalityService = nationalityService;
            _religionService = religionService;
            _bloodGroupService =bloodGroupService;
            _academicSessionService = academicSessionService;
            _academicClassService = academicClassService;
            _academicBranchService = academicBranchService;
            _addressTypeService = addressTypeService;

        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, AdmissionForm admissionForm)
        {
            admissionForm.IsSelected = false;
            _redeeppitory.Insert(admissionForm);
        }
        public void Insert(AdmissionForm admissionForm)
        {
            admissionForm.IsSelected = false;
            _redeeppitory.Insert(admissionForm);
        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, AdmissionForm admissionForm)
        {
            _redeeppitory.Update(admissionForm);
            unitOfWorkAsync.SaveChanges();
        }
        public AdmissionForm newadmissionForm(int instituteId)
        {
            var admissionForm=new AdmissionForm();
            admissionForm.GenderList = new List<KeyValuePair<int, string>>();
            _genderService.GetGenderByInstituteId(instituteId).ToList().ForEach(item => admissionForm.GenderList.Add(new KeyValuePair<int, string>(item.Id, item.Name)));
            admissionForm.NationalityList = new List<KeyValuePair<int, string>>();
            _nationalityService.GetNationalityByInstituteId(instituteId).ToList().ForEach(item => admissionForm.NationalityList.Add(new KeyValuePair<int, string>(item.Id, item.Name)));
            admissionForm.ReligionList = new List<KeyValuePair<int, string>>();
            _religionService.GetReligionByInstituteId(instituteId).ToList().ForEach(item => admissionForm.ReligionList.Add(new KeyValuePair<int, string>(item.Id, item.Name)));
            admissionForm.BloodGroupList = new List<KeyValuePair<int, string>>();
            _bloodGroupService.GetBloodGroupsByInstituteId(instituteId).ToList().ForEach(item => admissionForm.BloodGroupList.Add(new KeyValuePair<int, string>(item.Id, item.Name)));
            admissionForm.AcademicSessionList = new List<KeyValuePair<int, string>>();
            _academicSessionService.GetAcademicSessionByInstituteId(instituteId).ToList().ForEach(item => admissionForm.AcademicSessionList.Add(new KeyValuePair<int, string>(item.Id, item.Name)));
            admissionForm.AcademicClassList = new List<KeyValuePair<int, string>>();
            _academicClassService.GetAcademicClassesByInstituteId(instituteId).ToList().ForEach(item => admissionForm.AcademicClassList.Add(new KeyValuePair<int, string>(item.Id, item.Name)));
            admissionForm.AcademicBranchList = new List<KeyValuePair<int, string>>();
            _academicBranchService.GetAcademicBranchsByInstituteId(instituteId).ToList().ForEach(item => admissionForm.AcademicBranchList.Add(new KeyValuePair<int, string>(item.Id, item.Name)));
            admissionForm.AddressTypeList = new List<KeyValuePair<int, string>>();
            _addressTypeService.GetAddressTypeByInstituteId(instituteId).ToList().ForEach(item => admissionForm.AddressTypeList.Add(new KeyValuePair<int, string>(item.Id, item.Name)));            

            
            return admissionForm;
        }

        public IEnumerable<AdmissionForm> GetAllBySearch(VmSearch<AdmissionForm> searchModel)
        {

            var predicate = PredicateBuilder.True<AdmissionForm>();
            predicate = predicate.And(p => p.InstituteId == searchModel.InstituteId);
            if (searchModel != null)
            {

                if (searchModel.DropDownId1 > 0)
                    predicate = predicate.And(p => p.AcademicBranchId == searchModel.DropDownId1);

                if (searchModel.DropDownId2 > 0)
                    predicate = predicate.And(p => p.AcademicSessionId == searchModel.DropDownId2);

                if (searchModel.DropDownId3 > 0)
                    predicate = predicate.And(p => p.AcademicClassId == searchModel.DropDownId3);

                if (searchModel.selectedStatus)
                { predicate = predicate.And(p => p.IsActive && p.IsSelected); }
                else
                { predicate = predicate.And(p => p.IsActive && !p.IsSelected); }

            }
            return _redeeppitory.Query(predicate).Select();

        }

        public AdmissionForm GetDetailsByApplicantFormId(int applicantFormId)
        {
            return _redeeppitory.Query(x => x.Id == applicantFormId)
                .Include(x => x.Gender)
                .Include(x => x.Religion)
                .Include(x => x.BloodGroup)
                .Include(x => x.AcademicBranch)
                .Include(x => x.AcademicClass)
                .Include(x => x.AcademicSession)
                .Include(x => x.Nationality).Select().FirstOrDefault();

        }


    }
}
