using pnsms.Entities.ViewModels.Exams;
using pnsms.Service.Exams;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace sfa.Api.Api.Exams
{
    public class ExamTabulationSheetController : ApiController
    {
        private readonly IUnitOfWorkAsync _UnitOfWorkAsync;
        private readonly IVmExamTabulationSheetService _vmExamTabulationSheetService;


        public ExamTabulationSheetController(IUnitOfWorkAsync UnitOfWorkAsync
          , IVmExamTabulationSheetService vmExamTabulationSheetService
         )
        {
            _UnitOfWorkAsync = UnitOfWorkAsync;
            _vmExamTabulationSheetService = vmExamTabulationSheetService;

        }
        // GET api/ExamGrade
        [Route("api/ExamTabulationSheet/")]
        [HttpGet]
        public VmExamTabulationSheet Get(int instituteId, int examtypeid, int studentid, int sessionid)
        {
            return _vmExamTabulationSheetService.GetVmExamTabulationSheet(instituteId, examtypeid, studentid, sessionid);
        }

    }
}
