using Microsoft.Practices.Unity;
using pnsms.Entities.Models;
using pnsms.Service.Settings;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace pnsms.erp.Api.Setting
{
    public class AcademicClassSectionMappingController : ApiController
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
        public IAcademicClassSectionMappingService AcademicClassSectionMappingService { get; set; }
        /// <summary>
        /// Gets the specified is active.
        /// </summary>
        /// <param name="academicBranchId">The academic branch identifier.</param>
        /// <param name="academicClassId">The academic class identifier.</param>
        /// <param name="academicShiftId">The academic shift identifier.</param>
        /// <returns></returns>
       
        public IEnumerable<AcademicClassSectionMapping> Get(int academicBranchId, int academicClassId, int? academicShiftId = null)
        {
            var result = AcademicClassSectionMappingService.GetAll(Sessions.InstituteId,academicBranchId, academicClassId, academicShiftId);
            return result;
        }

        [Route("api/AcademicClassSectionMapping/getsectionbyacademicclass")]
        [HttpGet]
        public List<KeyValuePair<int, string>> GetAcademicClassSectionMappingByClassId(int id)
        {
            var academicclassid = id;
            var result = AcademicClassSectionMappingService.Getkvp(Sessions.InstituteId,academicclassid);
            return result;
        }
        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
       
        public AcademicClassSectionMapping Get(int id)
        {
            return AcademicClassSectionMappingService.Get(id);
        }


        /// <summary>
        /// Posts the specified teacher department.
        /// </summary>
        /// <param name="AcademicClassSectionMapping">The teacher department.</param>
        /// <returns></returns>
        [Validate]
      
        public HttpResponseMessage Post([FromBody]List<AcademicClassSectionMapping> academicClassSectionMapping)
        {

            //foreach (var b in academicClassSectionMapping)
            //{
            //    b.InstituteId = Sessions.InstituteId;
            //}
            AcademicClassSectionMappingService.Insert(Sessions.InstituteId, UnitOfWorkAsync, academicClassSectionMapping);
            return new HttpResponseMessage(HttpStatusCode.Created);

        }


        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="AcademicClassSectionMapping">The teacher department.</param>
        [Validate]
       
        public void Put(int id, [FromBody]AcademicClassSectionMapping academicClassSectionMapping)
        {
            academicClassSectionMapping.InstituteId = Sessions.InstituteId;
            AcademicClassSectionMappingService.Update(UnitOfWorkAsync, academicClassSectionMapping);
        }


        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
       
        public void Delete(int id)
        {
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

        // VmTeacher 
        [Route("api/AcademicClassSectionMapping/new")]
       
        public AcademicClassSectionMapping GetNew()
        {

            var teacher = AcademicClassSectionMappingService.New(Sessions.InstituteId);
            return teacher;
        }
    }
}
