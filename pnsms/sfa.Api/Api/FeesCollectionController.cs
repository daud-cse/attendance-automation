using pnsms.Entities.Models;
using pnsms.Entities.ViewModels;
using pnsms.Service;
using pnsms.Service.ViewModels;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace sfa.Api.Api
{
    public class FeesCollectionController : ApiController
    {
        private readonly IVmFeesCollectionService _feesCollectionService;
        private readonly IAcademicClassService _classService;
        private readonly IAcademicGroupService _groupService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        int institutionId = Sessions.InstituteId;

        public FeesCollectionController(
              IVmFeesCollectionService feesCollectionService
            , IAcademicClassService classService
            , IAcademicGroupService groupService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _feesCollectionService = feesCollectionService;
            _classService = classService;
            _groupService = groupService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("api/feescollection/new1")]
        [HttpPost]
        public VmFeesGenerate Create()
        {
            var feesCollectionCreate = _feesCollectionService.CreateNew(institutionId);
            return feesCollectionCreate;

        }

        [Route("api/feescollection/getstudents1")]
        [HttpPost]
        public VmFeesGenerate GetStudentList([FromBody]FeesGenerateAcademic feesGenerateAcademicModel)
        {
            VmFeesGenerate feesCollection = new VmFeesGenerate();
            feesGenerateAcademicModel.InstituteId = institutionId;
            var studentsList = _feesCollectionService.GetAllStudents(feesGenerateAcademicModel);
            feesCollection.SearchStudents = studentsList.Select(c => new VmAutoComplete { Id = c.StudentId, Text = c.UserInfo.Name + " [" + c.UserInfo.PIN + "]" });
            return feesCollection;
        }


        [Route("api/feescollection/save1")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]VmFeesGenerate VmFeesModel)
        {
            if (VmFeesModel.FeesGenerate.Id == 0)
            {
                _feesCollectionService.Save(_unitOfWorkAsync, VmFeesModel);
            }
            //else
            //{
            //    //_feesCollectionService.Update(VmFeesModel);
            //}


            return new HttpResponseMessage(HttpStatusCode.Created);
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
