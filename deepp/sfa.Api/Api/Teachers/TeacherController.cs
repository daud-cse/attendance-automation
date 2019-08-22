using System.IO;
using System.Linq;
using System.Web;
using pnsms.Entities.Models;
using pnsms.Entities.ViewModels.Teacher;
using pnsms.Service;
using pnsms.Service.ViewModels;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using pnsms.Service.SSOLogin;


namespace sfa.Api.Api.Teachers
{
    /// <summary>
    /// 
    /// </summary>
    public class TeacherController : ApiController
    {
        #region "  -  [  Constractor  ]  -  "


        //[Dependency]
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IVmTeacherService _vmTeacherService;
        private readonly ITeacherService _teacherService;
        private readonly ISSOService _SSOService;
        public TeacherController(
             IUnitOfWorkAsync unitOfWorkAsync, IVmTeacherService vmTeacherService
            , ITeacherService teacherService
            , ISSOService SSOService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _vmTeacherService = vmTeacherService;
            _teacherService = teacherService;
            _SSOService = SSOService;
        }
        #endregion

        #region "  -  [  CRUD  ]  -  "

        // GET api/teacher
        [Route("api/getallteacher/")]
        public IEnumerable<VmTeacherDetails> Get(bool isActive = false)
        {

            var objSSO = _SSOService.IsTokenValid(this.Request.Headers);
            if (objSSO == null)
            {
                IEnumerable<VmTeacherDetails> VmTeacherDetailsList = new VmTeacherDetails[] { new VmTeacherDetails { FirstName = "Token Is not found" } };
                return VmTeacherDetailsList;
            }
            else
            {
                return _vmTeacherService.GetAllTeacherDetails(objSSO.InstituteId);
            }
        }

        public VmTeacher Get(int id)
        {
            var teacher = _vmTeacherService.GetVmTeacherById(Sessions.InstituteId, Sessions.UserId, id);
            return teacher;
        }

        public HttpResponseMessage Post([FromBody]VmTeacher vmTeacher)
        {
            if (Sessions.Temp != null)
            {
                var images = (List<byte[]>)Sessions.Temp;
                vmTeacher.ProfileImage = images[0];
                vmTeacher.ProfileImageSmall = images[1];
                Sessions.Temp = null;
            }
            _vmTeacherService.CreateTeacher(_unitOfWorkAsync, Sessions.InstituteId, vmTeacher);
            return new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = new sfa.Api.Attributes.JsonContent(new
                {
                    Id = vmTeacher.UserInfo.Teacher.TeacherId,
                    Message = "Success"
                })
            };
        }

        // PUT api/Teacher/5

        public void Put(int id, [FromBody]VmTeacher vmTeacher)
        {
            if (Sessions.Temp != null)
            {
                var images = (List<byte[]>)Sessions.Temp;
                vmTeacher.ProfileImage = images[0];
                vmTeacher.ProfileImageSmall = images[1];
                Sessions.Temp = null;
            }
            _vmTeacherService.UpdateTeacher(_unitOfWorkAsync, vmTeacher);

        }

        // DELETE api/Teacher/5

        public void Delete(int id)
        {
        }


        #endregion

        #region "  -  [  Others  ]  -  "

        [HttpPost]
        [Route("api/teacher/search")]
        public IEnumerable<VmTeacherDetails> SearchTeacher(Teacher teacher)
        {
            var InstituteId = 5;
            return _vmTeacherService.GetAllTeacherDetails(InstituteId, teacher);
        }

        [HttpPost]
        [Route("api/teacher/getAllSmsDetails")]
        public IEnumerable<ShortMessageDetail> GetAllSmsDetails(Teacher teacher)
        {
            return _teacherService.GetAllShortMessageDetail(Sessions.InstituteId, Sessions.UserId, teacher);

        }

        // VmTeacher 
        [Route("api/teacher/new")]
        public VmTeacher GetNewTeacher()
        {

            var teacher = _vmTeacherService.GetNewVmTeacher(Sessions.InstituteId, Sessions.UserId);
            return teacher;
        }

        // new teacher only
        [Route("api/teacher/newTeacher")]
        public Teacher GetNewTeacherModel()
        {

            var teacher = _teacherService.NewTeacher(Sessions.InstituteId, Sessions.UserId);
            return teacher;
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWorkAsync.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion



    }


}
