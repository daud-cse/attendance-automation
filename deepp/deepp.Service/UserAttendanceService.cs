using deepp.Entities.Models;
using deepp.Entities.ViewModels;
using deepp.utility;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Service
{
    public interface IUserAttendanceService : IService<UserAttendance>
    {
        IEnumerable<UserAttendance> GetAllTeacherAttendance(VmSearchAttendance vmSearchAttendance);
        UserAttendance GetUserAttendanceForDetailsById(int attendanceId, int instituteId);
        IEnumerable<UserAttendance> GetAllEmployeeAttendance(VmSearchAttendance vmSearchAttendance);
        UserAttendance GetTeacherAttendance(VmUserAttendance vmUserAttendance);
        IEnumerable<UserAttendance> GetAllTeacherAttendanceForGlobalUser(VmSearchAttendance vmSearchAttendance);
        IEnumerable<UserAttendance> GetAllTeacherAttendanceForGlobal(VmSearchAttendance vmSearchAttendance);
    }
    public class UserAttendanceService : Service<UserAttendance>, IUserAttendanceService
    {
        private readonly IRepositoryAsync<UserAttendance> _redeeppitory;
        private readonly IUserAttendanceDetailService _IUserAttendanceDetailService;
        public UserAttendanceService(IRepositoryAsync<UserAttendance> redeeppitory
            , IUserAttendanceDetailService IUserAttendanceDetailService)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
            _IUserAttendanceDetailService = IUserAttendanceDetailService;
        }

        public IEnumerable<UserAttendance> GetAllTeacherAttendance(VmSearchAttendance vmSearchAttendance)
        {
            DateTime start = vmSearchAttendance.startDate;
            DateTime end = vmSearchAttendance.endDate.AddDays(1);
            int RefTypeId = (int)utility.UserInfoType.Teacher;

            return _redeeppitory.Query(p => p.AttendanceDate >= start.Date
                && p.AttendanceDate < end
                && p.UserInfoTypeId == RefTypeId
                && p.AcademicBranchId == vmSearchAttendance.BranchId).Select();

        }
        public IEnumerable<UserAttendance> GetAllTeacherAttendanceForGlobal(VmSearchAttendance vmSearchAttendance)
        {
            DateTime? start = vmSearchAttendance.startDate;
            DateTime? end = vmSearchAttendance.endDate;
            int RefTypeId = (int)utility.UserInfoType.Teacher;


            Expression<Func<UserAttendance, bool>> predicate = PredicateBuilder.True<UserAttendance>();

            if (RefTypeId > 0)
            {
                predicate = predicate.And(p => p.UserInfoTypeId == RefTypeId);
            }
            if (vmSearchAttendance.InstituteId > 0)
            {
                predicate = predicate.And(p => p.InstituteId == vmSearchAttendance.InstituteId);
            }
            //if (start!=null && end!=null)
            //{
            //    predicate = predicate.And(p => p.AttendanceDate >= start
            //    && p.AttendanceDate < end);
            //}

            return _redeeppitory
                .Query(predicate)
                .Select();
        }
        public IEnumerable<UserAttendance> GetAllTeacherAttendanceForGlobalUser(VmSearchAttendance vmSearchAttendance)
        {
            DateTime start = vmSearchAttendance.startDate;
            DateTime end = vmSearchAttendance.endDate.AddDays(1);
            int RefTypeId = (int)utility.UserInfoType.Teacher;

            return _redeeppitory.Query(p => p.AttendanceDate >= start.Date
                && p.AttendanceDate < end
                && p.UserInfoTypeId == RefTypeId
                && p.InstituteId == vmSearchAttendance.InstituteId).Select();

        }
        public UserAttendance GetTeacherAttendance(VmUserAttendance vmUserAttendance)
        {

            int RefTypeId = (int)utility.UserInfoType.Teacher;

            return _redeeppitory.Query(p => EntityFunctions.TruncateTime(p.AttendanceDate) == EntityFunctions.TruncateTime(vmUserAttendance.UserAttendance.AttendanceDate.Date)
                                        && p.UserInfoTypeId == RefTypeId
                                        && p.InstituteId == vmUserAttendance.UserAttendance.InstituteId
                                        && p.AcademicBranchId == vmUserAttendance.UserAttendance.AcademicBranchId)
                                     .Include(x => x.UserAttendanceDetails)
                                     .Select().FirstOrDefault();

        }
        public IEnumerable<UserAttendance> GetAllEmployeeAttendance(VmSearchAttendance vmSearchAttendance)
        {
            DateTime start = vmSearchAttendance.startDate;
            DateTime end = vmSearchAttendance.endDate.AddDays(1);
            int RefTypeId = (int)utility.UserInfoType.Employee;

            return _redeeppitory.Query(p => p.AttendanceDate >= start.Date
                && p.AttendanceDate < end
                && p.UserInfoTypeId == RefTypeId
                && p.AcademicBranchId == vmSearchAttendance.BranchId).Select();

        }

        public UserAttendance GetUserAttendanceForDetailsById(int attendanceId, int instituteId)
        {
            var objUserAttendance = _redeeppitory.Query(x => x.Id == attendanceId)
                .Include(g => g.AcademicBranch)
                .Include(g => g.UserInfoType)                                
                .Select().FirstOrDefault();
            var UserAttendanceDetail = _IUserAttendanceDetailService.GetListwithTeacherAttendanceTypeById(attendanceId);
            objUserAttendance.UserAttendanceDetails = new List<UserAttendanceDetail>();
            objUserAttendance.UserAttendanceDetails = UserAttendanceDetail.ToList();
            return objUserAttendance;

        }
        //public DateTime GetLastTakeAttendacneDate(int instituteId, int userTypeId)
        //{
        //    _redeeppitory.Query(x=>x.AttendanceDate==).Select().Max(x=>x.AttendanceDate)
        //}
    }


}
