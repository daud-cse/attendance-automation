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
    public class ProfessionController : ApiController
    { 
        private readonly IProfessionService _professionService;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;



        public ProfessionController(IProfessionService professionService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _professionService = professionService;
            _unitOfWorkAsync = unitOfWorkAsync;
          
            
        }
        // GET api/profession
        public IEnumerable<Profession> Get()
        {
            return _professionService.GetProfessions(Sessions.InstituteId);
        }


       
        // GET api/profession/5
        public Profession Get(int id)
        {
            return _professionService.GetProfessionById(id);
        }

        // POST api/profession
        [Validate]
        public HttpResponseMessage Post([FromBody]Profession profession)
        {
            profession.InstituteId = Sessions.InstituteId;
            _professionService.Insert(_unitOfWorkAsync, profession);
 
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/profession/5
        [Validate]
        public void Put(int id, [FromBody]Profession profession)
        {
            profession.InstituteId = Sessions.InstituteId;
            _professionService.Update(_unitOfWorkAsync , profession);
 
        }

        // DELETE api/profession/5
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
