using pnsms.Service.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pnsms.landing.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ILibraryBookService service;
        public LibraryController(ILibraryBookService service)
        {
            this.service = service;
        }

        // GET: Library
        public ActionResult Index()
        {
            var books = service.GetLibraryBooks(Sessions.InstituteId, true);
            ViewBag.Count = books.Count();
            ViewBag.AllCount = books.Sum(c => c.Quantity);

            return View(books);
        }
    }
}