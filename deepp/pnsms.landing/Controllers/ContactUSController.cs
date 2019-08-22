using pnsms.Entities.Models;
using pnsms.Service;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pnsms.landing.Controllers
{
    public class ContactUSController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IContactUService _IContactUService;
        private readonly IInstituteService _instituteService;
        public ContactUSController(IUnitOfWork unitOfWork, IContactUService IContactUService, IInstituteService instituteService)
        {
            _IContactUService = IContactUService;
            _instituteService = instituteService;
            _unitOfWork = unitOfWork;
        }

       /// <summary>
        /// Create
       /// </summary>
       /// <param name="IsSuccess"></param>
       /// <returns></returns>
        public ActionResult Create(bool IsSuccess)
        {

            var InstituteId = Sessions.InstituteId;
            var contactUs = new ContactU();

            contactUs.Institute = _instituteService.GetActiveInstituteById(InstituteId);
            if (IsSuccess) {
                ViewBag.result = "Data Save Successfully.";
            }
           
            return View(contactUs);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IsSuccess"></param>
        /// <returns></returns>
        public ActionResult CreateFeedback(bool IsSuccess)
        {

            var InstituteId = Sessions.InstituteId;
            var contactUs = new ContactU();

            contactUs.Institute = _instituteService.GetActiveInstituteById(InstituteId);
            if (IsSuccess)
            {
                ViewBag.result = "Data Save Successfully.";
            }

            return View(contactUs);
        }
      /// <summary>
      /// Save Contact
      /// </summary>
      /// <param name="collection"></param>
      /// <returns></returns>
        [HttpPost]
        public ActionResult Save(FormCollection collection)
        {
            try
            {
                var InstituteId = Sessions.InstituteId;
                var contactUs = new ContactU();
                var contactUsNew = new ContactU();
                bool IsSuccess=false;
                if (TryUpdateModel(contactUs, collection))
                {
                    contactUs.InstituteId = InstituteId;
                   
                    contactUs.CreateDate = DateTime.Now;
                    _IContactUService.Insert(contactUs);
                    _unitOfWork.SaveChanges();
                    IsSuccess = true;
                    return RedirectToAction("Create",new {IsSuccess=IsSuccess});
                    
                }
                contactUs.Institute = contactUsNew.Institute = _instituteService.GetActiveInstituteById(InstituteId);
                return View(contactUs);
            }
            catch
            {
                return View();
            }
        }

    }
}
