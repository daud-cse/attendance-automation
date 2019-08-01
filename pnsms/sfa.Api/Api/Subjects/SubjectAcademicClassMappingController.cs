using pnsms.Entities.Models;
using pnsms.Entities.ViewModels;
using pnsms.Entities.ViewModels.Subjects;
using pnsms.Service.SSOLogin;
using pnsms.Service.Subjects;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using Unity.Attributes;

namespace sfa.Api.Api.Subjects
{
    public class SubjectAcademicClassMappingController : ApiController
    {
      
        private readonly ISubjectAcademicClassMappingService _subjectAcademicClassMappingService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
       
        private readonly ISSOService _SSOService;
      
        public SubjectAcademicClassMappingController(ISubjectAcademicClassMappingService subjectAcademicClassMappingService
            , IUnitOfWorkAsync unitOfWorkAsync
           
              ,ISSOService SSOService
          
            )
        {
            _subjectAcademicClassMappingService = subjectAcademicClassMappingService;
            _SSOService = SSOService;
            _unitOfWorkAsync = unitOfWorkAsync;
          
        }


        [Route("api/SubjectAcademicClassMapping/getsectionbyacademicclass")]
        [HttpGet]
        public VmStudentAttendance GetSectionByAcademicClass(int id)
        {
            int academicSessionId = 2; //Sessions.CurrentSessionId;
            var academicClassId = id;
            var result = _subjectAcademicClassMappingService.GetSectionByAcademicClass(Sessions.InstituteId, academicSessionId, academicClassId, 0);

            VmStudentAttendance objVmStudentAttendance = new VmStudentAttendance();


            objVmStudentAttendance.StudentAttendance = new StudentAttendance();
            objVmStudentAttendance.StudentAttendance.AcademicSectionList = result;
            return objVmStudentAttendance;
        }


        [HttpGet]
        [Route("api/SubjectClassSectionTeacherMapping/GetAll")]
        public List<SubjectAcademicClassMapping> GetAll()
        {



            var objSSO = _SSOService.IsTokenValid(this.Request.Headers);
            if (objSSO == null)
            {
                List<SubjectAcademicClassMapping> SubjectAcademicClassMappinglst = new List<SubjectAcademicClassMapping> { new SubjectAcademicClassMapping {  SubjectName = "Token Is not found" } };
                return SubjectAcademicClassMappinglst;
            }
            else
            {
                // var CurrentSessionId = 2;
                return _subjectAcademicClassMappingService.GetAll(objSSO.InstituteId,objSSO.AcademicSessionId);
            }


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

            _subjectAcademicClassMappingService.Insert(Sessions.InstituteId, 3, _unitOfWorkAsync, subjectAcademicClassMappings);
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
                _unitOfWorkAsync.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
