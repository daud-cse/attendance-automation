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
    public class EventController : ApiController
    {
        private readonly IEventService _eventService;
        private readonly IImageService _imageService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        int institutionId = Sessions.InstituteId;

        public EventController(
              IEventService eventService
            , IImageService imageService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _eventService = eventService;
            _imageService = imageService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("api/event/new")]
        [HttpPost]
        public Event AddEvent()
        {
            var noticeCreate = _eventService.AddNewEvent(institutionId);
            return noticeCreate;
        }


        [Route("api/event/list")]
        [HttpPost]
        public VmSearch<Event> GetAllEvents([FromBody]VmSearch<Event> eventWithListModel)
        {

            eventWithListModel = eventWithListModel ?? new VmSearch<Event>();
            eventWithListModel.InstituteId = institutionId;
            IEnumerable<Event> eventlist = _eventService.GetAllEventListBySearch(eventWithListModel);
            eventWithListModel.SearchData = eventlist;
            return eventWithListModel;

        }


        [Route("api/event/save")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Event eventModel)
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

            if (eventModel.Id == 0)
            {

                _eventService.SaveEvent(_unitOfWorkAsync, imageList, eventModel);
            }
            else
            {

                _eventService.UpdateEvent(_unitOfWorkAsync, imageList, eventModel);

            }
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [Route("api/event/getsingle")]
        public Event GetSingleEventById(int id)
        {
            Event eventModel = _eventService.GetEventById(id);
            int RefTypeId = (int)utility.RefCode.Events;
            IEnumerable<Image> imgList = _imageService.GetImageByRefTypeIdAndRefPrimaryKey(RefTypeId, id);
            if (imgList.Count() > 0)
            {
                eventModel.ImageList = imgList;
            }
            return eventModel;
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
