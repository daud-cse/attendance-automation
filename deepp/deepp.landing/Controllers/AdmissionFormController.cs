using deepp.Entities.Models;
using deepp.Entities.ViewModels;
using deepp.Service;
using deepp.Service.ViewModels;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace deepp.landing.Controllers
{
    public class AdmissionFormController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IVmOnlineAdmissionService _vmOnlineAdmissionService;


        public AdmissionFormController(IUnitOfWork unitOfWork
             ,IVmOnlineAdmissionService vmOnlineAdmissionService           
            )
        {
            _vmOnlineAdmissionService = vmOnlineAdmissionService;
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(bool? IsSuccess)
        {
            var vmOnlineAdmission = _vmOnlineAdmissionService.newVmOnlineAdmission(Sessions.InstituteId);
            if (IsSuccess == true)
            {
                ViewBag.result = "Data Save Successfully.";
            }
            else
            {
                ViewBag.result = "";
            }
            return View("CreateOnlineAdmission", vmOnlineAdmission);
        }
        public ActionResult GetAdmissionInfo()
        {
            Institute objInstitute = new Institute();
            return View("AdmissionInfo", objInstitute);
        }

        public ActionResult SaveAdmissionForm(VmOnlineAdmission vmOnlineAdmission)
        {
           
            try
            {
                bool IsSuccess = false;
                var InstituteId = Sessions.InstituteId;
                if (ModelState.IsValid)
                {
                   vmOnlineAdmission.AdmissionForm.InstituteId = InstituteId;
                    IsSuccess = _vmOnlineAdmissionService.SaveOnlineAdmission(_unitOfWork, vmOnlineAdmission);




                    return RedirectToAction("Index", new { IsSuccess = IsSuccess });
                }
                else
                {
                    vmOnlineAdmission = _vmOnlineAdmissionService.newVmOnlineAdmission(Sessions.InstituteId);

                    return View("Index", vmOnlineAdmission);
                }

            }
            catch
            {
                 vmOnlineAdmission = _vmOnlineAdmissionService.newVmOnlineAdmission(Sessions.InstituteId);
          
                ViewBag.result = "Exception Occured!.";
                return View("Index", vmOnlineAdmission);
            }

        }
    }
}