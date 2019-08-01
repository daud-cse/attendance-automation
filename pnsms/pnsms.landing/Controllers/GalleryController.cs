using pnsms.Service;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pnsms.landing.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IGalleryService _galleryServic;
        private readonly IUnitOfWork _unitOfWork;
        public GalleryController(IUnitOfWork unitOfWork, IGalleryService galleryServic)
        {
            _galleryServic = galleryServic;
            _unitOfWork = unitOfWork;
        }


        public ActionResult GetPartialGallery()
        {

            var InstituteId = Sessions.InstituteId;
          //  var gallery = _galleryServic.GetGlobalGalleryByCurrentDateWithenStartEndDateTop10(InstituteId,true);
            var gallery = _galleryServic.GetGalleryDefault(InstituteId, true);
            
           // var gallery = _galleryServic.Ge(InstituteId,true);
            return PartialView("GalleryList", gallery);
        }

        public ActionResult GetAllGallery()
        {

            var InstituteId = Sessions.InstituteId;
            var gallery = _galleryServic.GetActiveGallery(InstituteId);
            return View("GalleryGrid", gallery);
        }

        public ActionResult GetGalleryById(int Id)
        {

            var InstituteId = Sessions.InstituteId;
            var gallery = _galleryServic.GetGalleryById(InstituteId, Id);
            return View("GalleryListById", gallery);
        }
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        _unitOfWork.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}


    }
}
