using deepp.portal;
using deepp.Service;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace deepp.Protal.Controllers
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
        public ActionResult Index()
        {
            var InstituteId = Sessions.InstituteId;
            var Event = _eventServic.GetGlobalEventByCurrentDateWithenStartEndDate(InstituteId, true);
            return View(Event);
        }

        public ActionResult GetAllWithDetails()
        {
            var InstituteId = Sessions.InstituteId;
            var Event = _eventServic.GetGlobalEventByCurrentDateWithenStartEndDate(InstituteId, true);
            return View("EventList", Event);
        }
        public ActionResult GetAllWithGrid()
        {
            var InstituteId = Sessions.InstituteId;
            var Event = _eventServic.GetGlobalEventByCurrentDateWithenStartEndDate(InstituteId, true);
            return View("EventGrid", Event);
        }
        //
        // GET: /Notice/Details/5
        public ActionResult Details(int? id)
        {
            var InstituteId = Sessions.InstituteId;
            if (id == null)
            {
                var EventMax = _eventServic.GetGlobalEventByCurrentDateWithenStartEndDate(InstituteId, true).FirstOrDefault();
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

    }
}
