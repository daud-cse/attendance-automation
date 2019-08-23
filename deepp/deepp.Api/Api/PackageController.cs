using deepp.Entities.Models;
using deepp.Service;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace deepp.Api.Api
{
   // [RoutePrefix("api/package")]
    public class PackageController : ApiController
    {
       private readonly IPackageService _PackageService;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;



        public PackageController(IPackageService PackageService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _PackageService = PackageService;
            _unitOfWorkAsync = unitOfWorkAsync;
          
            
        }
        // GET api/academicbranch
        public IEnumerable<Package> Get(bool IsActive = false)
        {
            return _PackageService.GetPackages(IsActive);
        }


        //public IEnumerable<AcademicBranch> Get()
        //{
        //    return _academicBranchService.GetActiveAcademicBranch();
        //}
        // GET api/academicbranch/5
        public Package Get(int id)
        {
            return _PackageService.GetPackageById(id);
        }

        // POST api/academicbranch
        [Validate]
        public HttpResponseMessage Post([FromBody]Package package)
        {
            //if (_PackageService.GetPackages().Any(d => d.Name.Equals(package.Name)))
            //    throw new ValidationException("The Name already exits.");
            _PackageService.Insert(package);
            _unitOfWorkAsync.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/department/5
        [Validate]
        public void Put(int id, [FromBody]Package package)
        {

            //if (_PackageService.GetPackages()
            //   .Any(d => d.Name.Equals(package.Name) && !d.Id.Equals(package.Id)))
            //    throw new ValidationException("The Name already exits.");
            
            
            _PackageService.Update(package);
            _unitOfWorkAsync.SaveChanges();

        }

        // DELETE api/academicbranch/5
        public void Delete(int id)
        {
        }

        protected override void Dispose(bool disdeepping)
        {
            if (disdeepping)
            {
                _unitOfWorkAsync.Dispose();
            }
            base.Dispose(disdeepping);
        }



    }
}
