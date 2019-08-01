using pnsms.Entities.Models;
using pnsms.Entities.ViewModels;
using pnsms.Entities.ViewModels.Fees;
using pnsms.Service;
using pnsms.Service.ViewModels;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace pnsms.erp.Api
{
    public class FeesCollectionController : ApiController
    {
        private readonly IFeesCollectionService _feesCollectionService;
        private readonly IAcademicClassService _classService;
        private readonly IAcademicGroupService _groupService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        int institutionId = Sessions.InstituteId;

        public FeesCollectionController(
              IFeesCollectionService feesCollectionService
            , IAcademicClassService classService
            , IAcademicGroupService groupService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _feesCollectionService = feesCollectionService;
            _classService = classService;
            _groupService = groupService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("api/feescollection/new")]
        [HttpPost]
        public VmFeesGenerate Create()
        {
            //var feesCollectionCreate = _feesCollectionService.CreateNew(institutionId);
            //return feesCollectionCreate;
            return null;

        }

        [Route("api/feescollection/getstudents")]
        [HttpPost]
        public VmFeesGenerate GetStudentList([FromBody]FeesGenerateAcademic feesGenerateAcademicModel)
        {
            //VmFeesGenerate feesCollection = new VmFeesGenerate();
            //feesGenerateAcademicModel.InstituteId = institutionId;
            //var studentsList = _feesCollectionService.GetAllStudents(feesGenerateAcademicModel);
            //feesCollection.SearchStudents = studentsList.Select(c => new VmAutoComplete { Id = c.StudentId, Text = c.UserInfo.Name + " [" + c.UserInfo.PIN + "]" });
            return null;
        }
        [Route("api/feescollection/monthlyFees")]
        [HttpGet]
        public VmFeesCollection MonthlyFees(int p_studentId,int p_month)
        {
            var instituteId = Sessions.InstituteId;
           return  _feesCollectionService.MonthlyFees(instituteId,p_studentId, p_month);
        }

        [HttpGet]
        [Route("api/feescollection/getReport")]
        public List<VMFeesClassMonthlyReport> getReport(int p_selectedOption, int p_feesAcademicClassId)
        {
            var feesReport = _feesCollectionService.GetReport(p_selectedOption, p_feesAcademicClassId);
            return feesReport;

        }

        [Route("api/feescollection/save")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]VmFeesCollection p_vmFeesCollection)
        {
            HttpResponseMessage output = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
            if (ModelState.IsValid)
            {
               if(_feesCollectionService.Saves(_unitOfWorkAsync, p_vmFeesCollection) == true)
                {
                    output = new HttpResponseMessage(HttpStatusCode.Created);
                }
            }
            return output;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWorkAsync.Dispose();
            }
            base.Dispose(disposing);
        }

    }   
}
