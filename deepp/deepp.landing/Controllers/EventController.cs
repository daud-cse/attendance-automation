using deepp.Service;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using deepp.Entities.Models;
namespace deepp.landing.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventServic;
        private readonly IUnitOfWork _unitOfWork;
        public EventController(IUnitOfWork unitOfWork, IEventService eventeServic)
        {
            _eventServic = eventeServic;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAll()
        {
            var InstituteId = Sessions.InstituteId;
            var Event = _eventServic.AllActiveEvent(InstituteId);
            return PartialView("GetPartialEvent", Event);
        }
        public ActionResult EventPartialList(int? page, string searchItem)
        {
            var InstituteId = Sessions.InstituteId;
            ViewData["searchItem"] = searchItem;
            const int defaultPageSize = 10;
            var currentPageIndex = page.HasValue ? page.Value : 1;
            var events = (IList<Event>)_eventServic.AllActiveEvent(InstituteId).ToList();
            events = string.IsNullOrWhiteSpace(searchItem) ? events.ToPagedList(currentPageIndex, defaultPageSize) : events.Where(p => p.EventTitle.ToLower().Contains(searchItem.ToLower())).ToPagedList(currentPageIndex, defaultPageSize);
            return PartialView("_EventPartialList",(IPagedList<Event>) events);
        }
        /// <summary>
        /// GetAllWithDetails
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllWithDetails()
        {
            var InstituteId = Sessions.InstituteId;
            var Event = _eventServic.GetGlobalEventByCurrentDateWithenStartEndDate(InstituteId,true);
            return View("EventList", Event);
        }
        /// <summary>
        /// GetAllWithGrid
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllWithGrid(int? page, string searchItem)
        {
           

            var InstituteId = Sessions.InstituteId;
            ViewData["searchItem"] = searchItem;
            const int defaultPageSize = 4;
            var currentPageIndex = page.HasValue ? page.Value : 1;
            var events = (IList<Event>)_eventServic.GetAllEvent(InstituteId).ToList();
            events = string.IsNullOrWhiteSpace(searchItem) ? events.ToPagedList(currentPageIndex, defaultPageSize) : events.Where(p => p.EventTitle.ToLower().Contains(searchItem.ToLower())).ToPagedList(currentPageIndex, defaultPageSize);
            return PartialView("EventGrid", (IPagedList<Event>)events);
        }
        
        /// <summary>
        /// Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            var InstituteId = Sessions.InstituteId;
            if (id == null)
            {
                var EventMax = _eventServic.GetGlobalEventByCurrentDateWithenStartEndDate(InstituteId,true).FirstOrDefault();
                return View("SingleEvent", EventMax);
            }
            else
            {
                var Event = _eventServic.GetEventById(Convert.ToInt32(id));
                if (Event.Image != null)
                {
                    Event.Image.ImageBinaryData = null;
                }
                return View("SingleEvent", Event);
            }


        }
        //protected override void Dispose(bool disdeepping)
        //{
        //    if (disdeepping)
        //    {
        //        _unitOfWork.Dispose();
        //    }
        //    base.Dispose(disdeepping);
        //}



    }
}
