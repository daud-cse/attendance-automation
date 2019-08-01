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
    public class AdmissionFormController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAdmissionFormService _admissionFormService;

        public AdmissionFormController(IUnitOfWork unitOfWork, IAdmissionFormService admissionFormService)
        {
            _admissionFormService = admissionFormService;
            _unitOfWork = unitOfWork;
        }

        public ActionResult CreateOnlineAdmission(bool? IsSuccess)
        {
            var admissionForm = _admissionFormService.newadmissionForm(Sessions.InstituteId);
            if (IsSuccess==true )
            {
                ViewBag.result = "Data Save Successfully.";
            }

            return View("CreateOnlineAdmission",admissionForm);
        }
        public ActionResult GetAdmissionInfo()
        {
            return View("AdmissionInfo");
        }

        public ActionResult SaveAdmissionForm(FormCollection collection)
        {
            var admissionForm = new AdmissionForm();
            try
            {
                bool IsSuccess = false;
                var InstituteId = Sessions.InstituteId;
                if (TryUpdateModel(admissionForm, collection))
                {
                  
                    admissionForm.InstituteId = InstituteId;
                    admissionForm.IsActive = true;
                    admissionForm.IsSelected = true;
                    admissionForm.LastUpdateTime = DateTime.Now;
                    admissionForm.Name = admissionForm.FirstName+" " + admissionForm.MiddleName +" "+ admissionForm.LastName;
                    _admissionFormService.Insert(admissionForm);
                    _unitOfWork.SaveChanges();
                    IsSuccess = true;
                    return RedirectToAction("CreateOnlineAdmission", new { IsSuccess = IsSuccess });
                }
                else
                {
                    var admissionFormNew = _admissionFormService.newadmissionForm(Sessions.InstituteId);
                    admissionForm.GenderList = admissionFormNew.GenderList;
                    admissionForm.BloodGroupList = admissionFormNew.BloodGroupList;
                    admissionForm.NationalityList = admissionFormNew.NationalityList;
                    admissionForm.ReligionList = admissionFormNew.ReligionList;
                    admissionForm.AcademicSessionList = admissionFormNew.AcademicSessionList;
                    admissionForm.AcademicClassList = admissionFormNew.AcademicClassList;
                    return View("CreateOnlineAdmission", admissionForm);
                }
                
            }
            catch
            {
                var admissionFormNew = _admissionFormService.newadmissionForm(Sessions.InstituteId);
                admissionForm.GenderList=admissionFormNew.GenderList;
                admissionForm.BloodGroupList = admissionFormNew.BloodGroupList;
                admissionForm.NationalityList = admissionFormNew.NationalityList;
                admissionForm.ReligionList = admissionFormNew.ReligionList;
                admissionForm.AcademicSessionList = admissionFormNew.AcademicSessionList;
                admissionForm.AcademicClassList = admissionFormNew.AcademicClassList;
                ViewBag.result = "Exception Occured!.";                
                return View("CreateOnlineAdmission", admissionForm);
            }

        }
	}
}