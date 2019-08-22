using pnsms.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pnsms.landing.Controllers
{
    public class PersonnelController : Controller
    {
        readonly IDesignationService service;

        public PersonnelController(IDesignationService service)
        {
            this.service = service;
        }

        public ActionResult Vacancy()
        {
            var model = service.VacancyAnalysis(Sessions.InstituteId);
            return View(model);
        }
    }
}