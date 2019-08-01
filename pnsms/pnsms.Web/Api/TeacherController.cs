using System.IO;
using System.Linq;
using System.Web;
using pnsms.Entities.Models;
using pnsms.Entities.ViewModels.Teacher;
using pnsms.Service;
using pnsms.Service.ViewModels;
using pnsms.erp.Attributes;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
 

namespace pnsms.erp.Api
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

        public TeacherController(
             IUnitOfWorkAsync unitOfWorkAsync, IVmTeacherService vmTeacherService,ITeacherService teacherService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _vmTeacherService = vmTeacherService;
            _teacherService = teacherService;
        }
        #endregion

        #region "  -  [  CRUD  ]  -  "

        // GET api/teacher

        public IEnumerable<VmTeacherDetails> Get(bool isActive = false)
        {

            return  _vmTeacherService.GetAllTeacherDetails(Sessions.InstituteId);
        }

        public VmTeacher Get(int id)
        {  
            var teacher = _vmTeacherService.GetVmTeacherById(Sessions.InstituteId,Sessions.UserId, id);
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
                Content = new JsonContent(new
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
            _vmTeacherService.UpdateTeacher(_unitOfWorkAsync,vmTeacher);

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
            return _vmTeacherService.GetAllTeacherDetails(Sessions.InstituteId, teacher);
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
