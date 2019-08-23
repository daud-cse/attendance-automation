using deepp.Service;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace deepp.landing.Controllers
{
    public class InstituteController : Controller
    {
        private readonly IInstituteService _instituteService;

        private readonly IUnitOfWork _unitOfWork;
        public InstituteController(IInstituteService instituteService
           , IUnitOfWork unitOfWork)
        {
            _instituteService = instituteService;
            _unitOfWork = unitOfWork;
        }
        public ActionResult GetInstituteHistory()
        {

            var InstituteId = Sessions.InstituteId;
            var institute = _instituteService.GetActiveInstituteById(InstituteId);
            return View("InstituteHistory", institute);
        }
        public ActionResult GetInstituteMasterPlan()
        {

            var InstituteId = Sessions.InstituteId;
            var institute = _instituteService.GetActiveInstituteById(InstituteId);
            return View("InstituteMasterPlan", institute);
        }
        public ActionResult GetInstituteInfrastructure()
        {

            var InstituteId = Sessions.InstituteId;
            var institute = _instituteService.GetActiveInstituteById(InstituteId);
            return View("InstituteInfrastructure", institute);
        }

        public ActionResult Assets()
        {
            var InstituteId = Sessions.InstituteId;
            var institute = _instituteService.GetActiveInstituteById(InstituteId);
            return View(institute);
        }

        public ActionResult Expences()
        {
            var InstituteId = Sessions.InstituteId;
            var institute = _instituteService.GetActiveInstituteById(InstituteId);
            return View(institute);
        }

        public ActionResult Sanitation()
        {
            var InstituteId = Sessions.InstituteId;
            var institute = _instituteService.GetActiveInstituteById(InstituteId);
            return View(institute);
        }

        public ActionResult Multimedia()
        {
            var InstituteId = Sessions.InstituteId;
            var institute = _instituteService.GetActiveInstituteById(InstituteId);
            return View(institute);
        }

        public ActionResult Library()
        {
            var InstituteId = Sessions.InstituteId;
            var institute = _instituteService.GetActiveInstituteById(InstituteId);
            return View(institute);
        }

        protected override void Dispose(bool disdeepping)
        {
            if (disdeepping)
            {
                _unitOfWork.Dispose();
            }
            base.Dispose(disdeepping);
        }
       
    }
}
