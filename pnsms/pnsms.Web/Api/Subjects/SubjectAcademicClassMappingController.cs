using Microsoft.Practices.Unity;
using pnsms.Entities.Models;
using pnsms.Entities.ViewModels;
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
    public class SubjectAcademicClassMappingController : ApiController
    {

        /// <summary>
        /// Gets or sets the unit of work asynchronous.
        /// </summary>
        /// <value>
        /// The unit of work asynchronous.
        /// </value>
        [Dependency]
        public IUnitOfWorkAsync UnitOfWorkAsync { get; set; }
        /// <summary>
        /// Gets or sets the teacher department service.
        /// </summary>
        /// <value>
        /// The teacher department service.
        /// </value>
        [Dependency]
        public ISubjectAcademicClassMappingService _subjectAcademicClassMappingService { get; set; }
        /// <summary>
        /// Gets the specified is active.
        /// </summary>
        /// <param name="academicSessionId">The academic session identifier.</param>
        /// <param name="academicClassId">The academic class identifier.</param>
        /// <param name="academicGroupId">The academic group identifier.</param>
        /// <returns></returns>

        public IEnumerable<VmSubjectAcademicClassMapping> Get(int academicClassId, int? academicGroupId = null, int AcademicBranchId=0)
        {
            int academicSessionId = Sessions.CurrentSessionId;
            var result = _subjectAcademicClassMappingService.GetVMSubjectAcademicClass(Sessions.InstituteId,AcademicBranchId, academicSessionId, academicClassId, 0).OrderBy(x => x.Id).OrderBy(x => x.AcademicClassSectionMapId);
            return result;
        }

        [Route("api/SubjectAcademicClassMapping/getsectionbyacademicclass")]
        [HttpGet]
        public VmStudentAttendance GetSectionByAcademicClass(int id)
        {
            int academicSessionId = Sessions.CurrentSessionId;
            var academicClassId = id;
            var result = _subjectAcademicClassMappingService.GetSectionByAcademicClass(Sessions.InstituteId, academicSessionId, academicClassId, 0);

            VmStudentAttendance objVmStudentAttendance = new VmStudentAttendance();

            
            objVmStudentAttendance.StudentAttendance = new StudentAttendance();
            objVmStudentAttendance.StudentAttendance.AcademicSectionList = result;
            return objVmStudentAttendance;
        }
        /// <summary>
        /// section Wise Subject
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [Route("api/SubjectAcademicClassMapping/GetSubjectAcademicClassMapping")]
        [HttpGet]
        public VmSubjectAcademicClassMapping GetSubjectAcademicClassMapping(int academicClassSectionMapId)
        {
            int academicSessionId = Sessions.CurrentSessionId;

           // var academicClassId = id;
            var objVmSubjectAcademicClassMapping = _subjectAcademicClassMappingService.GetSubjectTeacherBySectionWise(Sessions.InstituteId, academicClassSectionMapId);
          
            return objVmSubjectAcademicClassMapping;
        }
        /// <summary>
        /// Copy Mapping
        /// </summary>
        /// <param name="academicSessionId"></param>
        /// <param name="academicClassId"></param>
        /// <param name="academicGroupId"></param>
        /// <param name="toAcademicSessionId"></param>
        /// <param name="toAcademicClassId"></param>
        [Route("api/SubjectAcademicClassMapping/copyMapping")]
        [HttpGet]
       
        public void CopyMapping(int? academicSessionId, int? academicClassId, int? academicGroupId, int? toAcademicSessionId, int? toAcademicClassId)
        {
           // _subjectAcademicClassMappingService.CopyMapping(UnitOfWorkAsync, Sessions.InstituteId, academicSessionId, academicClassId, academicGroupId, toAcademicSessionId, toAcademicClassId);
        }
        /// <summary> 
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="subjectAcademicClassMappings">The subject academic class mappings.</param>
        /// <returns></returns>
        [Validate]
       
        public HttpResponseMessage Post([FromBody]List<VmSubjectAcademicClassMapping> subjectAcademicClassMappings)
        {
            //foreach (var b in subjectAcademicClassMappings)
            //{
            //    b.InstituteId = Sessions.InstituteId;
            //    // b.Id = 0;
            //}

            _subjectAcademicClassMappingService.Insert(Sessions.InstituteId, Sessions.CurrentSessionId,UnitOfWorkAsync, subjectAcademicClassMappings);
            return new HttpResponseMessage(HttpStatusCode.Created);

        }


        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="SubjectAcademicClassMapping">The teacher department.</param>
        [Validate]

        public void Put(int id, [FromBody]SubjectAcademicClassMapping SubjectAcademicClassMapping)
        {
            SubjectAcademicClassMapping.InstituteId = Sessions.InstituteId;
          //  _subjectAcademicClassMappingService.Update(UnitOfWorkAsync, SubjectAcademicClassMapping);
        }


        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>

        public void Delete(int id)
        {
        }
        // VmTeacher 
        //[Route("api/SubjectAcademicClassMapping/new")]

        public SubjectAcademicClassMapping GetNew()
        {

            var subjectAcademicClassMapping = _subjectAcademicClassMappingService.New(Sessions.InstituteId);
            return subjectAcademicClassMapping;
        }
        /// <summary>
        /// Releases the unmanaged resources that are used by the object and, optionally, releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnitOfWorkAsync.Dispose();
            }
            base.Dispose(disposing);
        }

       
    }
}
