using deepp.Entities.Models;
using deepp.Service;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace deepp.portal.Controllers
{
    public class PaymentController : Controller
    {
       // private readonly IUnitOfWork _unitOfWork;
        private readonly IMobilePaymentService _mobilePaymentService;
        private readonly IStoredProcedures _storedProcedures;

        public PaymentController(IUnitOfWork unitOfWork, IMobilePaymentService mobilePaymentService, IStoredProcedures storedProcedures)
        {
           // _unitOfWork = unitOfWork;
            _mobilePaymentService = mobilePaymentService;
            _storedProcedures = storedProcedures;
        }

        // GET: student
        public ActionResult Index(int studentId)
        {
            var payment = _mobilePaymentService.GetPaymentsByStudentId(studentId);
            return View(payment);
        }

        //public JsonResult GetStudentPayment(int month, int day, int year,int AcademicSessionId)
        //{
        //    var instituteId = Sessions.InstituteId;
        //    var userId = Sessions.UserId;


        //   var studentfees= _storedProcedures.spStudentFeesCollection(instituteId, userId, AcademicSessionId, year, month, day);

           

        //    return Json(studentfees, JsonRequestBehavior.AllowGet);
        //}
    }
}