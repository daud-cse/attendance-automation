using pnsms.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pnsms.landing.Controllers
{
    
    public class CoCurricularActController : Controller
    {
        readonly ICoCurricularActivityService service;

        public CoCurricularActController(ICoCurricularActivityService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            var model = service.GetCoCurricularActivityByInstituteId(Sessions.InstituteId, true);
            return View(model);
        }
    }
}