using pnsms.Entities.Models;
using pnsms.Entities.ViewModels.Attendance;
using pnsms.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pnsms.portal.Controllers
{
    public class StudentAttendenceController : Controller
    {

        private readonly IStudentService _studentService;
        private readonly IStudentAttendanceDetailService _studentAttendanceDetailService;
        private readonly IStudentAttendanceService _studentAttendanceService;
        private readonly IStoredProcedures _storedProcedures;
        public StudentAttendenceController(IStudentService studentService,
            IStudentAttendanceDetailService studentAttendanceDetailService,
            IStudentAttendanceService studentAttendanceService
            , IStoredProcedures storedProcedures)
        {
            _studentService = studentService;
            _studentAttendanceDetailService = studentAttendanceDetailService;
            _studentAttendanceService = studentAttendanceService;
            _storedProcedures = storedProcedures;
        }
        // GET: StudentAttendence
        public ActionResult StudentAttendance()
        {

            return View();
        }

        public JsonResult GetStudentattendance(int month, int day, int year,int AcademicSessionId)
        {
            var instituteId = Sessions.InstituteId;
            var userId = Sessions.UserId;
            
            var lstStudentAttendance = _storedProcedures.spStudentAttendance(instituteId, userId, AcademicSessionId,year,month,day);
            



            return Json(lstStudentAttendance, JsonRequestBehavior.AllowGet);
        }

    }
}