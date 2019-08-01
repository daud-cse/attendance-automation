using pnsms.Entities.Models;
using pnsms.Service;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace pnsms.erp.Api
{
    public class DesignationController : ApiController
    {
         private readonly IDesignationService _designationService;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;



        public DesignationController(IDesignationService designationService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _designationService = designationService;
            _unitOfWorkAsync = unitOfWorkAsync;
          
            
        }
        // GET api/designation
        public IEnumerable<Designation> Get()
        {
            return _designationService.GetDesignations(Sessions.InstituteId);
        }


       
        // GET api/designation/5
        public Designation Get(int id)
        {
            return _designationService.GetDesignationById(id);
        }

        // POST api/designation
        [Validate]
        public HttpResponseMessage Post([FromBody]Designation designation)
        {
            designation.InstituteId = Sessions.InstituteId;
            _designationService.Insert(_unitOfWorkAsync, designation);
            
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/designation/5
        [Validate]
        public void Put(int id, [FromBody]Designation designation)
        {
            designation.InstituteId = Sessions.InstituteId;
 
            _designationService.Update(_unitOfWorkAsync, designation);

        }

        // DELETE api/designation/5
        public void Delete(int id)
        {
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
