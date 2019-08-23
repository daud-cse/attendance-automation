using deepp.Entities.Models;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace deepp.landing.Controllers
{
    public class AcademicCalenderController : Controller
    {
        //[Dependency]
        private readonly IUnitOfWork _unitOfWork;


       
        public AcademicCalenderController(
             IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

           
        }
        // GET: AcademicCalender
        public ActionResult AcademicCalender()
        {
            return View();
        }
        public ActionResult GetEvents(double start, double end)
        {
            var fromDate = ConvertFromUnixTimestamp(start);
            var toDate = ConvertFromUnixTimestamp(end);

            //Get the events
            //You may get from the redeeppitory also
            var eventList = GetEvents();

            var rows = eventList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        private List<Models.Events> GetEvents()
        {
            List<Models.Events> eventList = new List<Models.Events>();

            Models.Events newEvent = new Models.Events
            {
                id = "1",
                title = "Event 1",
                start = DateTime.Now.AddDays(4).ToString("s"),
                end = DateTime.Now.AddDays(3).ToString("s"),
                allDay = false
            };


            eventList.Add(newEvent);

            newEvent = new Models.Events
            {
                id = "1",
                title = "Event 3",
                start = DateTime.Now.AddDays(2).ToString("s"),
                end = DateTime.Now.AddDays(3).ToString("s"),
                allDay = false
            };

            eventList.Add(newEvent);

            return eventList;
        }

        private static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }
    }
}