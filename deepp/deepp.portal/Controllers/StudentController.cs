using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using deepp.Service;
using Repository.Pattern.UnitOfWork;

namespace deepp.portal.Controllers
{
    public class StudentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStudentService _studentService;

        public StudentController(IUnitOfWork unitOfWork,IStudentService studentService)
        {
            _unitOfWork = unitOfWork;
            _studentService = studentService;
        }

        // GET: student
        public ActionResult Index(int studentId)
        {
            var student = _studentService.GetStudentById(studentId);
            return View(student);
        }
    }
}