using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using pnsms.Service;

namespace pnsms.portal.Controllers
{
    public class GuardianController : Controller
    {
        private readonly IGuardianService _guardianService;

        public GuardianController(IGuardianService guardianService)
        {
            _guardianService = guardianService;
        }

        // GET: Guardian
        public ActionResult Index(int guardianId)
        {
            var guardian = _guardianService.GetGuardianDetails(guardianId);
            return View(guardian);
        }
    }
}