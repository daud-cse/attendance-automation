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

namespace deepp.Api.Api
{
    public class NoticeController : ApiController
    {
        private readonly INoticeService _noticeService;
        private readonly INoticeTypeService _noticeTypeService;
        private readonly IImageService _imageService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        int institutionId = Sessions.InstituteId;

        public NoticeController(
              INoticeService noticeService
            , IImageService imageService
            , INoticeTypeService noticeTypeService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _noticeService = noticeService;
            _imageService = imageService;
            _noticeTypeService = noticeTypeService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("api/notice/new")]
        [HttpPost]
        public Notice AddNotice()
        {
            var noticeCreate = _noticeService.AddNewNotice(institutionId);
            return noticeCreate;
        }


        [Route("api/notice/list")]
        [HttpPost]
        public VmSearch<Notice> GetAllNotices([FromBody]VmSearch<Notice> noticeWithListModel)
        {

            noticeWithListModel = noticeWithListModel ?? new VmSearch<Notice>();
            noticeWithListModel.InstituteId = institutionId;
            IEnumerable<Notice> noticelist = _noticeService.GetAllNoticeListBySearch(noticeWithListModel);
            noticeWithListModel.SearchData = noticelist;
            return noticeWithListModel;

        }


        [Route("api/notice/save")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Notice noticeModel)
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

            if (noticeModel.Id == 0)
            {

                _noticeService.SaveNotice(_unitOfWorkAsync, imageList, noticeModel);
            }
            else 
            {

                _noticeService.UpdateNotice(_unitOfWorkAsync, imageList, noticeModel);
            
            }
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [Route("api/notice/getsingle")]
        public Notice GetSingleNoticeById(int id)
        {
            Notice noticeModel = _noticeService.GetNoticeById(id);
            noticeModel.NoticeTypeList = _noticeTypeService.GetKVP();
            int RefTypeId = (int)deepp.utility.RefCode.Notice;
            IEnumerable<Image> imgList = _imageService.GetImageByRefTypeIdAndRefPrimaryKey(RefTypeId,id);
            if(imgList.Count()>0)
            {
                noticeModel.ImageList = imgList;
            }  
            return noticeModel;
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
