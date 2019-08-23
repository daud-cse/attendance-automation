using deepp.Entities.Models;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Service
{
    public interface IUserAttendanceDetailService : IService<UserAttendanceDetail>
    {
        IEnumerable<UserAttendanceDetail> GetListwithTeacherAttendanceTypeById(int attendanceId);
        IEnumerable<UserAttendanceDetail> GetListwithEmployeeAttendanceTypeById(int attendanceId);
    }
    public class UserAttendanceDetailService : Service<UserAttendanceDetail>, IUserAttendanceDetailService
    {
        private readonly IRepositoryAsync<UserAttendanceDetail> _redeeppitory;
        public UserAttendanceDetailService(IRepositoryAsync<UserAttendanceDetail> redeeppitory)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
        }

        public IEnumerable<UserAttendanceDetail> GetListwithTeacherAttendanceTypeById(int attendanceId)
        {
            return _redeeppitory.Query(r => r.UserAttendanceId == attendanceId).Include(g => g.UserInfo)
                .Include(g => g.UserInfo.Teacher).Include(g => g.UserInfo.Teacher.Designation)
                .Include(g => g.AttendanceType).Include(g => g.AttendanceType.Colour).Select();
        }

        public IEnumerable<UserAttendanceDetail> GetListwithEmployeeAttendanceTypeById(int attendanceId)
        {
            return _redeeppitory.Query(r => r.UserAttendanceId == attendanceId).Include(g => g.UserInfo).Include(g => g.UserInfo.Employee).Include(g => g.UserInfo.Employee.Designation).Include(g => g.AttendanceType).Include(g => g.AttendanceType.Colour).Select();
        }
    }




}