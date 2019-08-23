using deepp.erp;
using Microsoft.Practices.Unity;
using deepp.Entities.Models;

using deepp.Service.Teachers;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace deepp.erp.Api.Teachers
{
    /// <summary>
    /// Teacher Department 
    /// </summary>
    /// <seealso cref="deepp.api.Api.BaseApiController" />
    //  [Module("TEC")]
    public class TeacherSubjectAcademicMappingController : ApiController
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
        public ITeacherSubjectAcademicMappingService TeacherSubjectAcademicMappingService { get; set; }

       // [Dependency]
      //  public ITeacherSubjectAcademicMappingBL teacherSubjectAcademicMappingService { get; set; }

        /// <summary>
        /// Gets the specified is active.
        /// </summary>
        /// <param name="academicBranchId">The academic branch identifier.</param>
        /// <param name="academicShiftId">The academic shift identifier.</param>
        /// <param name="teacherId">The teacher identifier.</param>
        /// <returns></returns>
        // [SubModuleAttribute("TEC_REF")]
        public IEnumerable<TeacherSubjectAcademicMapping> Get(int academicBranchId, int teacherId, int? academicShiftId = null)
        {
            var result = TeacherSubjectAcademicMappingService.Get(Sessions.InstituteId, academicBranchId, academicShiftId, teacherId);
            return result;
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        //   [SubModuleAttribute("TEC_REF")]
        public TeacherSubjectAcademicMapping Get(int id)
        {
            return TeacherSubjectAcademicMappingService.GetById(id);
        }


        /// <summary>
        /// Posts the specified teacher department.
        /// </summary>
        /// <param name="teacherSubjectAcademicMapping">The teacher department.</param>
        /// <returns></returns>
        [Validate]
        // [SubModuleAttribute("TEC_REF")]
        public HttpResponseMessage Post([FromBody]TeacherSubjectAcademicMapping teacherSubjectAcademicMapping)
        {
            teacherSubjectAcademicMapping.InstituteId = Sessions.InstituteId;
            TeacherSubjectAcademicMappingService.Insert(UnitOfWorkAsync, teacherSubjectAcademicMapping);
            return new HttpResponseMessage(HttpStatusCode.Created);

        }


        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="teacherSubjectAcademicMapping">The teacher department.</param>
        [Validate]
       // [SubModuleAttribute("TEC_REF")]
        public void Put(int id, [FromBody]TeacherSubjectAcademicMapping teacherSubjectAcademicMapping)
        {
            teacherSubjectAcademicMapping.InstituteId = Sessions.InstituteId; //authInfo.InstituteId;
            TeacherSubjectAcademicMappingService.Update(UnitOfWorkAsync, teacherSubjectAcademicMapping);
        }


        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
       // [SubModuleAttribute("TEC_REF")]
        //public void Delete(int id)
        //{
        //    TeacherSubjectAcademicMappingService.Delete(id, UnitOfWorkAsync);
        //}


        /// <summary>
        /// Releases the unmanaged resources that are used by the object and, optionally, releases the managed resources.
        /// </summary>
        /// <param name="disdeepping">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disdeepping)
        {
            if (disdeepping)
            {
                UnitOfWorkAsync.Dispose();
            }
            base.Dispose(disdeepping);
        }


        ///// <summary>
        ///// Gets the new.
        ///// </summary>
        ///// <returns></returns>
        //[Route("api/TeacherSubjectAcademicMapping/new")]
        ////  [SubModuleAttribute("TEC_REF")]
        //public TeacherSubjectAcademicMapping GetNew()
        //{

        //    var teacher = TeacherSubjectAcademicMappingService.
        //    return teacher;
        //}

        //[Route("api/TeacherSubjectAcademicMapping/newJunior")]
        //// [SubModuleAttribute("TEC_REF")]
        //public VmTeacherSubjectAcademicMapping GetNewJunior()
        //{

        //    var teacher = teacherSubjectAcademicMappingService.NewMappingJunior(authInfo.InstituteId);
        //    return teacher;
        //}


        //[Route("api/TeacherSubjectAcademicMapping/GetMarksEntryStatus")]
        //[HttpPost]
        //public VmEntryStatus GetMarksEntryStatus([FromBody]int id)
        //{
        //    var examMark = teacherSubjectAcademicMappingService.GetMarksEntryStatus(id);
        //    return examMark;
        //}

        //[Route("api/TeacherSubjectAcademicMapping/getresult")]
        //[HttpPost]
        //public VmTeacherSubjectAcademicMapping SearchMappingJunior([FromBody]VmTeacherSubjectAcademicMapping entryModel)
        //{
        //    var examMark = teacherSubjectAcademicMappingService.SearchMappingJunior(entryModel, authInfo.InstituteId);
        //    return examMark;
        //}

        //[Route("api/TeacherSubjectAcademicMapping/saveJunior")]
        //[HttpPost]
        //public HttpResponseMessage SaveJunior([FromBody]VmTeacherSubjectAcademicMapping entryModel)
        //{
        //    teacherSubjectAcademicMappingService.InsertJunior(UnitOfWorkAsync, entryModel, authInfo.InstituteId);
        //    return new HttpResponseMessage(HttpStatusCode.Created);
        //}
    }
}
