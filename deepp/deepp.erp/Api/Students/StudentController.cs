using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using deepp.Entities.Models;
using deepp.Entities.ViewModels.Student;
using deepp.Service;
using deepp.Service.ViewModels;
using deepp.erp.Attributes;
using deepp.utility.Resource;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using deepp.erp;

namespace deepp.erp.Api.Students
{
    /// <summary>
    /// Student Controller
    /// </summary>
    public class StudentController : ApiController
    {
        #region "  -  [  Constractor  ]  -  "

        private readonly IStudentService _studentService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IVmStudentService _vmStudentService;
        private readonly IGuardianService _guardianService;
        private readonly IGoverningbodyService _governingbodyService;


        public StudentController(IStudentService studentService
            , IUnitOfWorkAsync unitOfWorkAsync,
            IVmStudentService vmStudentService,IGuardianService guardianService, IGoverningbodyService governingbodyService)
        {
            _studentService = studentService;
            _unitOfWorkAsync = unitOfWorkAsync;
            _vmStudentService = vmStudentService;
            _guardianService = guardianService;
            _governingbodyService = governingbodyService;
        }
        #endregion

        #region "  -  [  CRUD  ]  -  "

        // GET api/Student
        /// <summary>
        /// Gets the specified is active.
        /// </summary>
        /// <param name="IsActive">if set to <c>true</c> [is active].</param>
        /// <returns></returns>
        public IEnumerable<VmStudentDetails> Get(bool isActive = false)
        {

            return _vmStudentService.GetAllStudentDetails(Sessions.InstituteId);
        }
        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public VmStudent Get(int id)
        {

            var student = _vmStudentService.GetVmStudentById(Sessions.InstituteId,Sessions.UserId, id);
            return student;
        }
        /// <summary>
        /// Posts the specified vm student.
        /// </summary>
        /// <param name="vmStudent">The vm student.</param>
        /// <returns></returns>
        public HttpResponseMessage Post([FromBody]VmStudent vmStudent)
        {

            if (Sessions.Temp != null)
            {
                var images = (List<byte[]>)Sessions.Temp;
                vmStudent.ProfileImage = images[0];
                vmStudent.ProfileImageSmall = images[1];
                Sessions.Temp = null;
            }
            _vmStudentService.CreateStudent(_unitOfWorkAsync, Sessions.InstituteId, vmStudent);

            return new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = new JsonContent(new
                {
                    Id = vmStudent.UserInfo.Id,
                    Type="Success",
                    Message = String.Format("{0} created", vmStudent.UserInfo.Name)
                })
            };
        }

        // PUT api/Student/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="vmStudent">The vm student.</param>
        public void Put(int id, [FromBody]VmStudent vmStudent)
        {
            if (Sessions.Temp != null)
            {
                var images = (List<byte[]>)Sessions.Temp;
                vmStudent.ProfileImage = images[0];
                vmStudent.ProfileImageSmall = images[1];
                Sessions.Temp = null;
            }
            _vmStudentService.UpdateStudent(_unitOfWorkAsync, vmStudent);

        }

        // DELETE api/Student/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
        }


        #endregion

        #region "  -  [  Others  ]  -  "

        /// <summary>
        /// Searches the student.
        /// </summary>
        /// <param name="student">The student.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/student/search")]
        public IEnumerable<VmStudentDetails> SearchStudent(Student student)
        {
            return _vmStudentService.GetAllStudentDetails(Sessions.InstituteId, student);

        }

        [HttpPost]
        [Route("api/student/getAllSmsDetails")]
        public IEnumerable<ShortMessageDetail> GetAllSmsDetails(Student student)
        {
            return _studentService.GetAllShortMessageDetail(Sessions.InstituteId, Sessions.UserId, student);

        }
        // TODO: remove it to guardian controller
        /// <summary>
        /// Gets all guardian SMS details.
        /// </summary>
        /// <param name="student">The student.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/guardian/getAllSmsDetails")]
        public IEnumerable<ShortMessageDetail> GetAllGuardianSmsDetails(Student student)
        {
            return _guardianService.GetAllShortMessageDetail(Sessions.InstituteId, Sessions.UserId, student);

        }
        // TODO: remove it to governingbody controller
        /// <summary>
        /// Gets all governingbodies SMS details.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/governingbody/getAllSmsDetails")]
        public IEnumerable<ShortMessageDetail> GetAllGoverningbodiesSmsDetails()
        {
            return _governingbodyService.GetGoverningbodyByIinstituteId(Sessions.InstituteId).Select(s => new ShortMessageDetail() { UserInfoId = s.UserInfo.Id, MobileNumber = s.UserInfo.ContactNumber1, UserInfoName = s.UserInfo.Name });

        }
        // VmStudent 
        [Route("api/student/new")]
        public VmStudent GetNewStudent()
        { 
            var student = _vmStudentService.GetNewVmStudent(Sessions.InstituteId,Sessions.UserId);
            return student;
        }

        // new student only
        [Route("api/student/newStudent")]
        public Student GetNewStudentModel()
        {

            var student = _studentService.NewStudent(Sessions.InstituteId, Sessions.UserId,0);
            return student;
        }
        protected override void Dispose(bool disdeepping)
        {
            if (disdeepping)
            {
                _unitOfWorkAsync.Dispose();
            }
            base.Dispose(disdeepping);
        }
     
        #endregion
 

    }


}
