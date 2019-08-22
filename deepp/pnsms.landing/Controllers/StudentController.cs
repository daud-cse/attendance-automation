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
    public class StudentController : Controller
    {
        #region "  -  [  Constractor  ]  -  "
        private readonly IStudentService studentService;
        private readonly IAcademicClassService academicClassService;
        private readonly IUnitOfWork unitOfWork;
        // private readonly IVmStudentService _vmStudentService;

        public StudentController(IStudentService studentService
            , IAcademicClassService academicClassService
            , IUnitOfWork unitOfWork
            )
        {
            this.studentService = studentService;
            this.academicClassService = academicClassService;
            this.unitOfWork = unitOfWork;

        }
        #endregion

        public ActionResult List()
        {
            var classes = academicClassService.GetAcademicClassesByInstituteId(Sessions.InstituteId);
            ViewBag.classId = classes.ToSelectList(null, "Id", "Name");

            var students = studentService.GetAllStudent(classes.FirstOrDefault().Id);

            ViewBag.Count = students.Count();

            return View(students);
        }

        [HttpPost]
        public ActionResult List(int classId)
        {
            var classes = academicClassService.GetAcademicClassesByInstituteId(Sessions.InstituteId);
            ViewBag.classId = classes.ToSelectList(classId, "Id", "Name");

            var students = studentService.GetAllStudent(classId);

            ViewBag.Count = students.Count();

            return View(students);
        }
    }
}