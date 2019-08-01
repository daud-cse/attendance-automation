using pnsms.Entities.Models;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service
{
    public interface IUserAttendanceDetailService : IService<UserAttendanceDetail>
    {
        IEnumerable<UserAttendanceDetail> GetListwithTeacherAttendanceTypeById(int attendanceId);
        IEnumerable<UserAttendanceDetail> GetListwithEmployeeAttendanceTypeById(int attendanceId);
    }
    public class UserAttendanceDetailService : Service<UserAttendanceDetail>, IUserAttendanceDetailService
    {
        private readonly IRepositoryAsync<UserAttendanceDetail> _repository;
        public UserAttendanceDetailService(IRepositoryAsync<UserAttendanceDetail> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<UserAttendanceDetail> GetListwithTeacherAttendanceTypeById(int attendanceId)
        {
            return _repository.Query(r => r.UserAttendanceId == attendanceId).Include(g => g.UserInfo)
                .Include(g => g.UserInfo.Teacher).Include(g => g.UserInfo.Teacher.Designation)
                .Include(g => g.AttendanceType).Include(g => g.AttendanceType.Colour).Select();
        }

        public IEnumerable<UserAttendanceDetail> GetListwithEmployeeAttendanceTypeById(int attendanceId)
        {
            return _repository.Query(r => r.UserAttendanceId == attendanceId).Include(g => g.UserInfo).Include(g => g.UserInfo.Employee).Include(g => g.UserInfo.Employee.Designation).Include(g => g.AttendanceType).Include(g => g.AttendanceType.Colour).Select();
        }
    }




}