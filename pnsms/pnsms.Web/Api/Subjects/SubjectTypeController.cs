using Microsoft.Practices.Unity;
using pnsms.Entities.Models;
using pnsms.Entities.ViewModels.Subjects;
using pnsms.Service.Subjects;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace pnsms.erp.Api.Subjects
{
    public class SubjectTypeController : ApiController
    {
        [Dependency]
        public ISubjectTypeService _subjectTypeService { set; get; }

        [Dependency]
        public IUnitOfWorkAsync unitOfWorkAsync { get; set; }

        /// <summary>
        /// Gets All Subject Type
        /// </summary>
       
        [Route("api/SubjectType/getAll")]
        [HttpGet]
        public IEnumerable<SubjectType> Get()
        {
            int instituteId = Sessions.InstituteId;
            return _subjectTypeService.Get(instituteId, null);
        }

        /// <summary>
        /// Get Subject Type Detail
        /// </summary>
        
        [Route("api/SubjectType/getDetailsById")]
        [HttpGet]
        public SubjectType GetDetails(int id)
        {
            return _subjectTypeService.Get(id);
        }

        /// <summary>
        /// Save a Subject Type
        /// </summary>
       
        [Route("api/SubjectType/save")]
        [HttpPost]
        public void Post([FromBody]SubjectType SubjectType)
        {
            SubjectType.InstituteId = Sessions.InstituteId;
            _subjectTypeService.Insert(unitOfWorkAsync, SubjectType);
        }

        /// <summary>
        /// Update a Subject Type
        /// </summary>
       
        [Route("api/SubjectType/update")]
        [HttpPut]
        public void Put([FromBody]SubjectType SubjectType)
        {
            _subjectTypeService.Update(unitOfWorkAsync, SubjectType);
        }

        /// <summary>
        /// New call for ExamSub
        /// </summary>
        //[SubModule("EXM_STP")]
        //[Route("api/SubjectType/new")]
        //[HttpPost]
        //public VmExamSetup New()
        //{
        //    int instituteId = authInfo.InstituteId;
        //    int userid = authInfo.UserId;
        //    return ExamSubBusinessLayer.New(instituteId, userid);
        //}
    }
}
