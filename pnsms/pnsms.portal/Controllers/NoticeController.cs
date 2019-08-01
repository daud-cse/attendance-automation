using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using pnsms.Service;

namespace pnsms.portal.Controllers
{
    public class NoticeController : Controller
    {
        private readonly INoticeService _noticeService;

        public NoticeController(INoticeService noticeService)
        {
            _noticeService = noticeService;
        }

        // GET: Notice
        public ActionResult Index()
        {
            var instituteId = Sessions.InstituteId;
            var notice = _noticeService.GetGlobalNoticeByCurrentDateWithenStartEndDate(instituteId,true);
            return View(notice);
        }
        public ActionResult Details(int? id)
        {
            // Sessions.
            var instituteId = Sessions.InstituteId;
            if (id == null)
            {
                var noticeMax = _noticeService.GetGlobalNoticeByCurrentDateWithenStartEndDate(instituteId,true).FirstOrDefault();
                return View("SingleNotice", noticeMax);
            }
            else
            {
                var notice = _noticeService.GetNoticeById(Convert.ToInt32(id));
                return View("SingleNotice", notice);
            }
        }
    }
}