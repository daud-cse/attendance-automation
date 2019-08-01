using pnsms.Entities.Models;
using pnsms.Entities.ViewModels;
using pnsms.Entities.ViewModels.Exams;
using pnsms.erp;
using pnsms.Service.Exams;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace pnsms.Api.Api.Exams
{
    public class ExamTabulationSheetController : ApiController
    {
        private readonly IUnitOfWorkAsync _UnitOfWorkAsync;
        private readonly IVmExamTabulationSheetService _vmExamTabulationSheetService;

        private readonly IExamTypeWiseTabulationSheetMasterService _examTypeWiseTabulationSheetMasterService;


        public ExamTabulationSheetController(IUnitOfWorkAsync UnitOfWorkAsync
          , IVmExamTabulationSheetService vmExamTabulationSheetService
            , IExamTypeWiseTabulationSheetMasterService examTypeWiseTabulationSheetMasterService
         )
        {
            _UnitOfWorkAsync = UnitOfWorkAsync;
            _vmExamTabulationSheetService = vmExamTabulationSheetService;
            _examTypeWiseTabulationSheetMasterService = examTypeWiseTabulationSheetMasterService;

        }
        [Route("api/newExamTabulationSheet/")]
        [HttpGet]
        public ExamTypeWiseTabulationSheetMaster newExamTabulationSheet()
        {
            int instituteId = Sessions.InstituteId;
            var currentsessionid=Sessions.CurrentSessionId;
            return _examTypeWiseTabulationSheetMasterService.newExamTabulationSheetMaster(instituteId, currentsessionid);
        }
        // GET api/ExamGrade
        [Route("api/GetExamTabulationSheetCriteria/")]
        [HttpPost]
        public VmExamTabulationSheet Get(VmCommonSearch objVmCommonSearch)
        {
            int instituteId = Sessions.InstituteId;
            int CurrentSessionId = Sessions.CurrentSessionId;

            return _vmExamTabulationSheetService.GetVmExamTabulationSheet(instituteId, objVmCommonSearch.ExamTypeId, objVmCommonSearch.StudentId, CurrentSessionId);
        }
        // GET api/ExamGrade
        [Route("api/GetTabulationSheetMasterCriteria/")]
        [HttpPost]
        public List<ExamTypeWiseTabulationSheetMaster> GetTabulationSheetMasterCriteria(VmCommonSearch objVmCommonSearch)
        {
            int instituteId = Sessions.InstituteId;
            int CurrentSessionId = Sessions.CurrentSessionId;

            return _examTypeWiseTabulationSheetMasterService.GetExamTypeWiseTabulationSheetMasterCriteria(instituteId, CurrentSessionId, objVmCommonSearch);
        }
    }
}
