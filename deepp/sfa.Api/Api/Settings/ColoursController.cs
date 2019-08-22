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

namespace sfa.Api.Api.Settings
{
    public class ColoursController : ApiController
    {
        private readonly IColourService _ColoursService;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;



        public ColoursController(IColourService ColoursService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _ColoursService = ColoursService;
            _unitOfWorkAsync = unitOfWorkAsync;
          
            
        }
        // GET api/Colours
        //public IEnumerable<Colour> Get(bool IsActive=false)
        //{
        //    return _ColoursService.GetColourss(IsActive);
        //}


        public IEnumerable<Colour> Get()
        {
            return _ColoursService.GetColours();
        }
        // GET api/Colours/5
        public Colour Get(int id)
        {
            return _ColoursService.GetColourById(id);
        }

        // POST api/Colours
        [Validate]
        public HttpResponseMessage Post([FromBody]Colour Colours)
        {
            //Colour colours=new Colour();
             var colours = _ColoursService.GetColourById(Colours.Id);
             if (colours==null)
            {
                  _ColoursService.Insert(Colours);
                _unitOfWorkAsync.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.Created);
            }

            if (colours.Id != Colours.Id)
            {
                _ColoursService.Insert(Colours);
                _unitOfWorkAsync.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            else
            {
                throw new ValidationException("The Name already exist.");
            }
        }

        // PUT api/department/5
        [Validate]
        public void Put(int id, [FromBody]Colour Colours)
        {

            //if (DepartmentService.Queryable()
            //    .Any(d => d.Name.Equals(department.Name) && !d.Id.Equals(department.Id)))
            //var getcolours=_ColoursService.GetColourById(Colours.Id);

            //if (getcolours.Id == Colours.Id)
            //{
                    
                _ColoursService.Update(Colours);
                _unitOfWorkAsync.SaveChanges();
            //}

            //else {
            //    throw new ValidationException("The Name already exist.");
            //}
            

        }

        // DELETE api/Colours/5
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
