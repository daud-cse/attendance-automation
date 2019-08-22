using pnsms.Entities.Models;
using pnsms.Entities.ViewModels.Fees;
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
    public interface IFeesAcademicClassService : IService<FeesAcademicClass>
    {
        void saves(List<VMFeesAcademicClass> p_feesAcademicClass, IUnitOfWorkAsync unitOfWork);
        List<VMFeesAcademicClass> Get(int instituteId);
        //List<VMFeesClassMonthlyReport> GetReport(int searchOption, int p_feesAcdemicClassId);
    }

    public class FeesAcademicClassService : Service<FeesAcademicClass>, IFeesAcademicClassService
    {
        private readonly IAcademicClassService _academicClassService;
        //private readonly IFeesCollectionService _feesCollectionService;
        //private readonly IStudentService _studentService;

        public IRepositoryAsync<FeesAcademicClass> _repository;
        public IFeesHeadService _feesHeadService ;
        public IFeesTypeService _feesTypeService;

        public FeesAcademicClassService(IRepositoryAsync<FeesAcademicClass> repository
            , IFeesHeadService feesHeadService
            , IAcademicClassService academicClassService
            , IFeesTypeService feesTypeService )
            : base(repository)
        {
            _repository = repository;
            _feesHeadService = feesHeadService;
            this._academicClassService = academicClassService;
            this._feesTypeService = feesTypeService;
            //this._feesCollectionService = feesCollectionService;
            //this._studentService = studentService;
        }
        public void saves(List<VMFeesAcademicClass> p_feesAcademicClass, IUnitOfWorkAsync unitOfWork)
        {
            foreach (VMFeesAcademicClass vfac in p_feesAcademicClass)
            {
                FeesAcademicClass fac = new FeesAcademicClass();
                fac.AcademicClassId = vfac.AcademicClassId;
                fac.Amount = vfac.Amount;
                fac.Description = vfac.Description;
                fac.FeesAcademicClassId = vfac.FeesAcademicClassId;
                fac.FeesHeadsId = vfac.FeesHeadsId;
                fac.FeesTypeId = vfac.FeesTypeId;
                fac.InstituteId = vfac.InstituteId;
                fac.IsActive = vfac.IsActive;
                fac.LastUpdateTime = DateTime.Now;
                if (fac.FeesAcademicClassId == 0)
                {
                    _repository.Insert(fac);
                }
                else
                {

                    _repository.Update(fac);
                }

            }
            unitOfWork.SaveChanges();
        }
        public List<VMFeesAcademicClass> Get(int instituteId)
        {
            var feesHeads = _feesHeadService.GetFeesHeads(instituteId, true).ToList();
            var feesAcademicClassList = _repository.Query(x => x.InstituteId == instituteId).Include(x => x.FeesHead).Include(x => x.AcademicClass).Include(x => x.FeesType).Select().ToList();
            var academicClassList = _academicClassService.Query(x => x.InstituteId == instituteId).Select().ToList();
            var kvpFeesType = _feesTypeService.GetKVP(instituteId);
            List<VMFeesAcademicClass> allfac = new List<VMFeesAcademicClass>();

            foreach (var ac in academicClassList)
            {
                foreach (var fh in feesHeads)
                {
                    var existingfac = feesAcademicClassList.FirstOrDefault(x => x.AcademicClassId == ac.Id && x.FeesHeadsId == fh.Id);
                    VMFeesAcademicClass fac = new VMFeesAcademicClass();
                    fac.FeesAcademicClassId = existingfac == null ? 0 : existingfac.FeesAcademicClassId;
                    fac.AcademicClassId = ac.Id;
                    fac.AcademicClassName = ac.Name;
                    fac.FeesHeadName = fh.Name;
                    fac.IsAlreadyAdded = existingfac == null ? false : true;
                    fac.Amount = existingfac == null ? 0 : existingfac.Amount;
                    fac.Description = fh.Description;
                    fac.FeesHeadsId = fh.Id;
                    fac.FeesTypeId = existingfac == null ? 0 : existingfac.FeesTypeId;
                    fac.FeesTypeName = "Monthly";
                    fac.kvpFeesTypes = kvpFeesType;
                    fac.InstituteId = ac.InstituteId;
                    fac.IsActive = true;
                    fac.LastUpdateTime = DateTime.Now;
                    allfac.Add(fac);
                }

            }



            //int current = 0;//feesAcademicClassList.OrderBy(x => x.AcademicClassId).FirstOrDefault().AcademicClassId;
            //var lastItem = feesAcademicClassList.OrderBy(x => x.AcademicClassId).LastOrDefault();
            //var currentHeads = new List<KeyValuePair<int,FeesAcademicClass>>();
            //var allFeesHeadIds = feesHeads.Select(x => x.Id);
            //List<FeesAcademicClass> missingheads = new List<FeesAcademicClass>();
            //FeesAcademicClass previous = new FeesAcademicClass();
            //foreach (FeesAcademicClass fac in feesAcademicClassList.OrderBy(x => x.AcademicClassId))
            //{
            //    if (current == fac.AcademicClassId)
            //    {
            //        currentHeads.Add(new KeyValuePair<int, FeesAcademicClass>(fac.FeesHeadsId, fac));
            //    }
            //    else
            //    {
            //        currentHeads.Add(new KeyValuePair<int, FeesAcademicClass>(fac.FeesHeadsId, fac));
            //        var missingItems = allFeesHeadIds.Except(currentHeads.Select(x=>x.Key).ToList());
            //        if (missingItems != null && missingItems.Count() > 0)
            //        {
            //            foreach (int id in missingItems)
            //            {
            //                var missinghead = new FeesAcademicClass();
            //                var academicClass = new AcademicClass();
            //                var head = new FeesHead();
            //                var type = new FeesType();
            //                type.FeesTypeName = "monthly"; 
            //                academicClass = currentHeads.FirstOrDefault().Value.AcademicClass;
            //                var feesHead = feesHeads.FirstOrDefault(x => x.Id == id);
            //                head = feesHead;
            //                missinghead.FeesHead = head;
            //                missinghead.FeesType = type;
            //                missinghead.AcademicClass = academicClass;
            //                missinghead.FeesAcademicClassId = 0;
            //                missingheads.Add(missinghead);
            //            }
            //        }
            //        currentHeads = new List<KeyValuePair<int, FeesAcademicClass>>();
            //    }
            //    current = fac.AcademicClassId;
            //}
            //// If Last items match then some item should not be populate;
            //if (currentHeads != null && currentHeads.Count > 0)
            //{
            //    var missingItems = allFeesHeadIds.Except(currentHeads.Select(x => x.Key).ToList());
            //    if (missingItems != null && missingItems.Count() > 0)
            //    {
            //        foreach (int id in missingItems)
            //        {
            //            var missinghead = new FeesAcademicClass();

            //            var feesHead = feesHeads.FirstOrDefault(x => x.Id == id);
            //            //missinghead = currentHeads.FirstOrDefault().Value;
            //            var academicClass = new AcademicClass();
            //            academicClass = currentHeads.FirstOrDefault().Value.AcademicClass;
            //            missinghead.FeesHead = feesHead;
            //            missinghead.AcademicClass = academicClass;
            //            missinghead.FeesAcademicClassId = 0;
            //            missingheads.Add(missinghead);
            //        }
            //    }
            //}

            //feesAcademicClassList.AddRange(missingheads);
            //return feesAcademicClassList.OrderBy(x => x.AcademicClass.Name).ToList();
            return allfac.OrderBy(x => x.AcademicClassId).ToList();
        }

        //public List<VMFeesClassMonthlyReport> GetReport(int searchOption, int p_feesAcdemicClassId)
        //{
        //    var studentsInfo = _studentService.Query(x => x.CurrentAcademicClassId == p_feesAcdemicClassId)
        //        .Include(x => x.UserInfo)
        //        .Include(x => x.AcademicBranch)
        //        .Select();
        //    var feesCollection = _feesCollectionService.Query(x => x.AcademicClassId == p_feesAcdemicClassId && x.Month == searchOption).Select();
        //    var monthlyAcademicFessHeads = _repository.Query(x => x.AcademicClassId == p_feesAcdemicClassId).Include(x=>x.FeesHead).Select();
        //    var monthlyAcademicFess  = monthlyAcademicFessHeads.Sum(x => x.Amount);
        //    List<VMFeesClassMonthlyReport> output = new List<VMFeesClassMonthlyReport>();
        //    VMFeesClassMonthlyReport vmFeesClassMonthlyReport;
        //    var MonthlyAnount = monthlyAcademicFess;
        //    var Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(searchOption);
        //    var Institute = studentsInfo.FirstOrDefault().AcademicBranch.Name;
        //    foreach(var studentinfo in studentsInfo)
        //    {
        //        vmFeesClassMonthlyReport = new VMFeesClassMonthlyReport();
        //        vmFeesClassMonthlyReport.MonthlyAnount = MonthlyAnount;
        //        vmFeesClassMonthlyReport.Month = Month;
        //        vmFeesClassMonthlyReport.Institute = Institute;
        //        vmFeesClassMonthlyReport.StudentName = studentinfo.UserInfo.Name;
        //        List<VMFeesCollection> vmCollections = new List<VMFeesCollection>();
        //        foreach(var head in monthlyAcademicFessHeads)
        //        {
        //            VMFeesCollection vmfeescollection = new VMFeesCollection();
        //            vmfeescollection.FeesHeadId = head.FeesHeadsId;
        //            vmfeescollection.FeesHeadName = head.FeesHead.Name;
        //            vmfeescollection.Amount = feesCollection.Where(x => x.StudentId == studentinfo.StudentId).GroupBy(x => x.FeesHeadsId).Select(x => x.Sum(g => g.TotalReceiveAmount)).FirstOrDefault();
        //            vmCollections.Add(vmfeescollection);
        //        }
        //        vmFeesClassMonthlyReport.ReceiveAmounts = vmCollections;
        //        output.Add(vmFeesClassMonthlyReport);

        //    }


        //    return output;
        //}
    }
}
