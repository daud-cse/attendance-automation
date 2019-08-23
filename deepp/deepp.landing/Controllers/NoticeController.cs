using deepp.Entities.Models;
using deepp.Service;
using deepp.Service.ViewModels;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
namespace deepp.landing.Controllers
{
    public class NoticeController : Controller
    {

        private readonly INoticeService _noticeServic;
        private readonly ITestimonialService _testimonialService;
        private readonly IVmLandingService _vmLandingService;
        private readonly IUnitOfWork _unitOfWork;
        public NoticeController(IUnitOfWork unitOfWork, INoticeService noticeServic, IVmLandingService vmLandingService, ITestimonialService testimonialService)
        {
            _noticeServic = noticeServic;
            _testimonialService = testimonialService;
            _vmLandingService = vmLandingService;
            _unitOfWork = unitOfWork;
        }
        public ActionResult GetAll()
        {
            var InstituteId = Sessions.InstituteId;
            var notice = _noticeServic.GetActiveNotice(InstituteId);
            return PartialView("GetPartialNotice", notice);
        }
        /// <summary>
        /// Get all Notice List
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllWithDetails()
        {
            var InstituteId = Sessions.InstituteId;
            var notice = _noticeServic.GetGlobalNoticeByCurrentDateWithenStartEndDate(InstituteId, true);
            return View("NoticeList", notice);
        }
        public ActionResult GetAllWithGrid(int? page, string searchItem)
        {
            var InstituteId = Sessions.InstituteId;
            ViewData["searchItem"] = searchItem;
            const int defaultPageSize = 4;
            var currentPageIndex = page.HasValue ? page.Value : 1;
            //var notice = _noticeServic.GetActiveNotice(InstituteId);

            var notices = (IList<Notice>)_noticeServic.GetActiveNotice(InstituteId).ToList();
            notices = string.IsNullOrWhiteSpace(searchItem) ? notices.ToPagedList(currentPageIndex, defaultPageSize) : notices.Where(p => p.NoticeTitle.ToLower().Contains(searchItem.ToLower())).ToPagedList(currentPageIndex, defaultPageSize);

            return View("NoticeGrid", (IPagedList<Notice>)notices);
        }
        public ActionResult GetUrgentNotice()
        {
            var InstituteId = Sessions.InstituteId;
            var notice = _noticeServic.GetActiveNotice(InstituteId);
            return PartialView("NoticeUrgent", notice);
        }
        // GET: /Notice/Details/5
        public ActionResult Details(int? id)
        {
            var InstituteId = Sessions.InstituteId;
            if (id == null)
            {
                var noticeMax = _noticeServic.GetGlobalNoticeByCurrentDateWithenStartEndDate(InstituteId, true).FirstOrDefault();
                return View("SingleNotice", noticeMax);
            }
            else
            {
                var notice = _noticeServic.GetNoticeById(Convert.ToInt32(id));
                return View("SingleNotice", notice);
            }
        }

        public ActionResult ActiveTeacherNotice(int? page, string searchItem)
        {
          
            var InstituteId = Sessions.InstituteId;
            ViewData["searchItem"] = searchItem;
            const int defaultPageSize = 4;
            var currentPageIndex = page.HasValue ? page.Value : 1;        
            var notices = (IList<Notice>)_noticeServic.GetActiveNoticeByTypeId(InstituteId, (int)deepp.utility.NoticeType.Teacher).ToList();
            notices = string.IsNullOrWhiteSpace(searchItem) ? notices.ToPagedList(currentPageIndex, defaultPageSize) : notices.Where(p => p.NoticeTitle.ToLower().Contains(searchItem.ToLower())).ToPagedList(currentPageIndex, defaultPageSize);

            return View("ActiveTeacherNotice", (IPagedList<Notice>)notices);
        }
        public ActionResult ActiveStudentNotice(int? page, string searchItem)
        {
          
            var InstituteId = Sessions.InstituteId;
            ViewData["searchItem"] = searchItem;
            const int defaultPageSize = 4;
            var currentPageIndex = page.HasValue ? page.Value : 1;
            var notices = (IList<Notice>)_noticeServic.GetActiveNoticeByTypeId(InstituteId, (int)deepp.utility.NoticeType.Student).ToList();
            notices = string.IsNullOrWhiteSpace(searchItem) ? notices.ToPagedList(currentPageIndex, defaultPageSize) : notices.Where(p => p.NoticeTitle.ToLower().Contains(searchItem.ToLower())).ToPagedList(currentPageIndex, defaultPageSize);

            return View("ActiveStudentNotice", (IPagedList<Notice>)notices);
        }
        public ActionResult ActiveEmployeeNotice(int? page, string searchItem)
        {
           
            var InstituteId = Sessions.InstituteId;
            ViewData["searchItem"] = searchItem;
            const int defaultPageSize = 4;
            var currentPageIndex = page.HasValue ? page.Value : 1;
            var notices = (IList<Notice>)_noticeServic.GetActiveNoticeByTypeId(InstituteId, (int)deepp.utility.NoticeType.Employee).ToList();
            notices = string.IsNullOrWhiteSpace(searchItem) ? notices.ToPagedList(currentPageIndex, defaultPageSize) : notices.Where(p => p.NoticeTitle.ToLower().Contains(searchItem.ToLower())).ToPagedList(currentPageIndex, defaultPageSize);

            return View("ActiveEmployeeNotice", (IPagedList<Notice>)notices);
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
