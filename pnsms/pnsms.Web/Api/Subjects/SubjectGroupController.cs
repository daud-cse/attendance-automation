using System;

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
    public class SubjectGroupController : ApiController
    {
        [Dependency]
        public ISubjectGroupService _SubjectGroupService { set; get; }

        [Dependency]
        public IUnitOfWorkAsync unitOfWorkAsync { get; set; }

        /// <summary>
        /// Gets All Subject Type
        /// </summary>

        [Route("api/SubjectGroup/getAll")]
        [HttpGet]
        public IEnumerable<SubjectGroup> Get()
        {
            int instituteId = Sessions.InstituteId;
            return _SubjectGroupService.Get(instituteId, null);
        }

        /// <summary>
        /// Get Subject Type Detail
        /// </summary>

        [Route("api/SubjectGroup/getDetailsById")]
        [HttpGet]
        public SubjectGroup GetDetails(int id)
        {
            return _SubjectGroupService.Get(id);
        }

        /// <summary>
        /// Save a Subject Type
        /// </summary>

        [Route("api/SubjectGroup/save")]
        [HttpPost]
        public void Post([FromBody]SubjectGroup SubjectGroup)
        {
            SubjectGroup.InstituteId = Sessions.InstituteId;
            _SubjectGroupService.Insert(unitOfWorkAsync, SubjectGroup);
        }

        /// <summary>
        /// Update a Subject Type
        /// </summary>

        [Route("api/SubjectGroup/update")]
        [HttpPut]
        public void Put([FromBody]SubjectGroup SubjectGroup)
        {
            _SubjectGroupService.Update(unitOfWorkAsync, SubjectGroup);
        }

        
    }
}

