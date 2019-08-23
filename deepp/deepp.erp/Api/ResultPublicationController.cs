using deepp.erp;
using deepp.Entities.Models;
using deepp.Entities.ViewModels;
using deepp.Service;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace deepp.erp.Api
{
    public class ResultPublicationController : ApiController
    {
        private readonly IResultPublicationService _resultService;
        private readonly IAcademicSessionService _sessionService;
        private readonly IImageService _imageService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        int institutionId = Sessions.InstituteId;

        public ResultPublicationController(
              IResultPublicationService resultService
            , IImageService imageService
            , IAcademicSessionService sessionService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _resultService = resultService;
            _imageService = imageService;
            _sessionService = sessionService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("api/resultpublish/new")]
        [HttpPost]
        public ResultPublication AddNotice()
        {
            var resultCreate = _resultService.AddNewResultPublication(institutionId);
            return resultCreate;
        }


        [Route("api/resultpublish/list")]
        [HttpPost]
        public VmSearch<ResultPublication> GetAllNotices([FromBody]VmSearch<ResultPublication> resultPublishWithListModel)
        {

            resultPublishWithListModel = resultPublishWithListModel ?? new VmSearch<ResultPublication>();
            resultPublishWithListModel.InstituteId = institutionId;
            var sessionList = _sessionService.GetKVP(institutionId);
            resultPublishWithListModel.DropDownList1 = resultPublishWithListModel.DropDownList1!=null ? resultPublishWithListModel.DropDownList1 : _sessionService.GetKVP(institutionId);
            resultPublishWithListModel.DropDownId1 = resultPublishWithListModel.DropDownId1 > 0 ? resultPublishWithListModel.DropDownId1 : sessionList.FirstOrDefault().Key;
            IEnumerable<ResultPublication> noticelist = _resultService.GetAllResultPublicationListBySearch(resultPublishWithListModel);
            resultPublishWithListModel.SearchData = noticelist;
            return resultPublishWithListModel;

        }


        [Route("api/resultpublish/save")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]ResultPublication resultPublishWithListModel)
        {
            List<byte[]> imageList = new List<byte[]>();
           
            List<byte[]> objlt = (List<byte[]>)Sessions.Temp;

            if (objlt != null)
            {
                for (int i = 0; i < objlt.Count; i++)
                {
                    imageList.Add(objlt[i]);
                }
            }

            Sessions.Temp = null;

            if (resultPublishWithListModel.Id == 0)
            {

                _resultService.SaveResultPublication(_unitOfWorkAsync, imageList, resultPublishWithListModel);
            }
            else 
            {

                _resultService.UpdateResultPublication(_unitOfWorkAsync, imageList, resultPublishWithListModel);
            
            }
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [Route("api/resultpublish/getsingle")]
        public ResultPublication GetSingleNoticeById(int id)
        {
            ResultPublication resultPublishModel = _resultService.GetresultPublishById(id);
            resultPublishModel.AcademicSessionList = _sessionService.GetKVP(institutionId);
            int RefTypeId = (int)utility.RefCode.Result_Publish;
            IEnumerable<Image> imgList = _imageService.GetImageByRefTypeIdAndRefPrimaryKey(RefTypeId,id);
            if(imgList.Count()>0)
            {
                resultPublishModel.ImageList = imgList;
            }
            return resultPublishModel;
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
