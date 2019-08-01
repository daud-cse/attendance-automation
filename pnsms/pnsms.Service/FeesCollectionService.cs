using pnsms.Entities.Models;
using pnsms.Entities.ViewModels.Fees;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service
{
    public interface IFeesCollectionService : IService<FeesCollection>
    {
        VmFeesCollection MonthlyFees(int instituteId, int studentId, int month);
        bool Saves(IUnitOfWork _unitOfWorkAsync, VmFeesCollection p_vmFeesCollection);
        List<VMFeesClassMonthlyReport> GetReport(int searchOption, int p_feesAcdemicClassId);
    }
    public class FeesCollectionService : Service<FeesCollection>, IFeesCollectionService
    {
        private readonly IRepositoryAsync<FeesCollection> _repository;
        private readonly IStudentService _studentService;
        private readonly IFeesAcademicClassService _feesAcademicClassService;

        public FeesCollectionService(IRepositoryAsync<FeesCollection> repository, IStudentService studentService, IFeesAcademicClassService feesAcademicClassService)
            : base(repository)
        {
            _repository = repository;
            this._studentService = studentService;
            this._feesAcademicClassService = feesAcademicClassService;
        }

        public VmFeesCollection MonthlyFees(int instituteId,int studentId, int month)
        {
            VmFeesCollection studentFees = new VmFeesCollection();
            try
            {
                var student = _studentService.Query(x => x.StudentId == studentId && x.UserInfo.InstituteId==instituteId)
                .Include(x => x.UserInfo)
                .Include(x => x.AcademicSession)
                
                .Include(x => x.AcademicClassSectionMapping)
                .Include(x => x.AcademicClassSectionMapping.AcademicSection)
                .Include(x => x.AcademicClassSectionMapping.AcademicClass)
                .Include(x => x.AcademicSession)
                .Include(x => x.AcademicBranch)
                //.Include(x => x.FeesCollections.Where(y => y.CollectionDate.Month == month))
                .Select()
                .SingleOrDefault();
                if (student==null)
                {
                    return studentFees;
                }

                var facs = _feesAcademicClassService.Query(x => x.AcademicClassId == student.CurrentAcademicClassId).Include(x => x.FeesHead).Select().ToList();
                var studentPaidAmount = _repository.Query(x => x.StudentId == studentId && x.Month == month).Select().ToList();

                studentFees.StudentName = student.UserInfo.Name;
                studentFees.StudentId = student.StudentId;
                studentFees.InstituteId = instituteId;
                studentFees.Roll = student.CurrentRollNo;
                studentFees.Session = student.AcademicSession.Name;
                studentFees.Section = (int)student.CurrentAcademicSectionId;
                studentFees.SessionId = (int)student.CurrentAcademicSessionId;
                studentFees.Branch = student.AcademicBranch.Name;
                studentFees.Class = (int)student.CurrentAcademicClassId;
                studentFees.SectionName = student.AcademicClassSectionMapping.AcademicSection.Name;
                studentFees.ClassName = student.AcademicClassSectionMapping.AcademicClass.Name;
                studentFees.CollectionDate = DateTime.Now;
                List<VmStudentFees> sfs = new List<VmStudentFees>();
                foreach (var fac in facs)
                {
                    VmStudentFees sf = new VmStudentFees();
                    sf.PaidAmount = studentPaidAmount.Where(x => x.FeesHeadsId == fac.FeesHeadsId).ToList().Sum(x => x.TotalReceiveAmount);
                    sf.RemainingAmount = (fac.Amount - sf.PaidAmount);
                    sf.MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                    sf.FeesHeadName = fac.FeesHead.Name;
                    sf.FeesHeadId = fac.FeesHeadsId;
                    sf.FeesAmount = fac.Amount;
                    sfs.Add(sf);
                }
                studentFees.StudentFeesList = sfs;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }




            return studentFees;
        }

        public bool Saves(IUnitOfWork _unitOfWorkAsync, VmFeesCollection p_vmFeesCollection)
        {
            var output = false;
            foreach (var item in p_vmFeesCollection.StudentFeesList)
            {
                if (item.RemainingAmount > 0)
                {
                    FeesCollection sfc = new FeesCollection();
                    sfc.StudentId = p_vmFeesCollection.StudentId;
                    sfc.InstituteId = p_vmFeesCollection.InstituteId;
                    sfc.IsActive = true;
                    sfc.AcademicClassId = p_vmFeesCollection.Class;
                    sfc.LastUpdateTime = DateTime.Now;
                    sfc.Month = DateTimeFormatInfo.CurrentInfo.MonthNames.ToList().IndexOf(item.MonthName) + 1;
                    sfc.TotalReceiveAmount = item.RemainingAmount;
                    sfc.AcademicSessionId = p_vmFeesCollection.SessionId;
                    sfc.CollectionDate = p_vmFeesCollection.CollectionDate;
                    sfc.FeesHeadsId = item.FeesHeadId;
                    _repository.Insert(sfc);

                }
            }
            if (_unitOfWorkAsync.SaveChanges() > 0)
            {
                output = true;
            }
            return output;
        }



        public List<VMFeesClassMonthlyReport> GetReport(int searchOption, int p_feesAcdemicClassId)
        {
            var studentsInfo = _studentService.Query(x => x.CurrentAcademicClassId == p_feesAcdemicClassId)
                .Include(x => x.UserInfo)
                .Include(x => x.AcademicBranch)
                .Select();
            var feesCollection = _repository.Query(x => x.AcademicClassId == p_feesAcdemicClassId && x.Month == searchOption).Select();
            var monthlyAcademicFessHeads = _feesAcademicClassService.Query(x => x.AcademicClassId == p_feesAcdemicClassId).Include(x => x.FeesHead).Select();
            var monthlyAcademicFess = monthlyAcademicFessHeads.Sum(x => x.Amount);
            List<VMFeesClassMonthlyReport> output = new List<VMFeesClassMonthlyReport>();
            VMFeesClassMonthlyReport vmFeesClassMonthlyReport;
            var MonthlyAnount = monthlyAcademicFess;
            var Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(searchOption);
            var Institute = studentsInfo.FirstOrDefault().AcademicBranch.Name;
            foreach (var studentinfo in studentsInfo)
            {
                vmFeesClassMonthlyReport = new VMFeesClassMonthlyReport();
                vmFeesClassMonthlyReport.MonthlyAnount = MonthlyAnount;
                vmFeesClassMonthlyReport.Month = Month;
                vmFeesClassMonthlyReport.Institute = Institute;
                vmFeesClassMonthlyReport.StudentName = studentinfo.UserInfo.Name;
                vmFeesClassMonthlyReport.AmountPaid = feesCollection.Where(x => x.StudentId == studentinfo.StudentId).Sum(y => y.TotalReceiveAmount);
                vmFeesClassMonthlyReport.RemainingAmount = vmFeesClassMonthlyReport.MonthlyAnount-vmFeesClassMonthlyReport.AmountPaid;
                List <VMFeesCollection> vmCollections = new List<VMFeesCollection>();
                foreach (var head in monthlyAcademicFessHeads.ToList())
                {
                    VMFeesCollection vmfeescollection = new VMFeesCollection();
                    vmfeescollection.FeesHeadId = head.FeesHeadsId;
                    vmfeescollection.FeesHeadName = head.FeesHead.Name;
                    vmfeescollection.Amount = feesCollection.Where(x => x.StudentId == studentinfo.StudentId && x.FeesHeadsId == head.FeesHeadsId).GroupBy(x => x.FeesHeadsId).Select(x => x.Sum(g => g.TotalReceiveAmount)).FirstOrDefault();
                    vmCollections.Add(vmfeescollection);
                }
                vmFeesClassMonthlyReport.ReceiveAmounts = vmCollections;
                output.Add(vmFeesClassMonthlyReport);

            }


            return output;
        }

    }
}
