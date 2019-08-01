using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pnsms.Entities.Models;
using pnsms.Entities.ViewModels.Portal;

namespace pnsms.Service.ViewModels
{
    public interface IVMportalService
    {
        VMportal PortalInfo(int id, int instituteId);
        string GeStudentsbyGuirdians(int guirdianId);
        string GeStudentsbyId(int studentId);
    }

    public class VMportalService : IVMportalService
    {
        private readonly IGuardiansOfStudentService _guardiansOfStudentService;
        private readonly IGuardianService _guardianService;
        private readonly IUserInfoService _userInfoService;
        private readonly INoticeService _noticeService;
        private readonly IEventService _eventService;


        public VMportalService(IGuardiansOfStudentService guardiansOfStudentService, IGuardianService guardianService, IUserInfoService userInfoService, INoticeService noticeService, IEventService eventService)
        {
            _guardiansOfStudentService = guardiansOfStudentService;
            _guardianService = guardianService;
            _userInfoService = userInfoService;
            _noticeService = noticeService;
            _eventService = eventService;
        }

        public VMportal PortalInfo(int id, int instituteId)
        {
            var objVMportal = new VMportal
              {
                  //GuardiansOfStudent = "",
                  //Notices = _noticeService.GetActiveNotice(instituteId),
                  //Events = _eventService.AllActiveEvent(instituteId),
                  //Guardian = _guardianService.GetGuardianDetails(id)

              };
            return objVMportal;
        }
        public string GeStudentsbyId(int studentId)
        {
            var students = _userInfoService.GetUserAndInstituteInfoByUserId(studentId);
            var stds = "";
            
                stds += utility.PortalUtility.StudentTemplate
                    .Replace("[studentid]", students.Id.ToString())
                    .Replace("[studentname]", students.Name.ToString());
            return stds;
        }

        public string GeStudentsbyGuirdians(int guirdianId)
        {
            var students = _guardiansOfStudentService.GetStudentsByGuardian(guirdianId);
            var stds = "";
            students.ToList().ForEach(delegate(GuardiansOfStudent item)
            {
                stds += utility.PortalUtility.StudentTemplate
                    .Replace("[studentid]", item.StudentId.ToString())
                    .Replace("[studentname]", item.Student.UserInfo.Name.ToString());

            });                         
            return stds;
        }
    }
}
