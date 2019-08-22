using pnsms.Service;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pnsms.landing.Controllers
{
    public class GoverningbodyController : Controller
    {
        #region "  -  [  Constractor  ]  -  "
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGoverningbodyService _governingbodyService;
        public GoverningbodyController(
             IUnitOfWork unitOfWork, IGoverningbodyService governingbodyService)
        {
            _unitOfWork = unitOfWork;
            _governingbodyService = governingbodyService;
        }
        #endregion
        public ActionResult GetGoverningbody()
        {

            var governingbody = _governingbodyService.GetGoverningbodyByIinstituteId(Sessions.InstituteId);
            return View("GoverningbodyList", governingbody);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}