﻿using deepp.Entities.Models;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Service
{
    public interface IStudentAttendanceDetailService : IService<StudentAttendanceDetail>
    {
        IEnumerable<StudentAttendanceDetail> GetListByAttendaceId(int attendanceId);
        IEnumerable<StudentAttendanceDetail> GetListwithAttendanceTypeByAttendaceId(int attendanceId);

        IEnumerable<StudentAttendanceDetail> AttendancesByStudentId(int studentId);
        IEnumerable<StudentAttendanceDetail> AttendancesByStudentId(int studentId, DateTime fromDate, DateTime toDate);

        List<StudentAttendanceDetail> GeStudentAttendanceDetailsList(int instituteId);
        void  Update(StudentAttendanceDetail objStudentAttendanceDetail);
        
    }
    public class StudentAttendanceDetailService : Service<StudentAttendanceDetail>, IStudentAttendanceDetailService
    {
        private readonly IRepositoryAsync<StudentAttendanceDetail> _redeeppitory;
        private readonly IRepositoryAsync<StudentAttendance> _redeeppitory1;
        public StudentAttendanceDetailService(IRepositoryAsync<StudentAttendanceDetail> redeeppitory,
            IRepositoryAsync<StudentAttendance> redeeppitory1)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
            _redeeppitory1 = redeeppitory1;
        }

        public IEnumerable<StudentAttendanceDetail> GetListByAttendaceId(int attendanceId)
        {
            return _redeeppitory.Query(r => r.StudentAttendanceId == attendanceId).Include(g => g.Student).Include(g => g.Student.UserInfo).Select();
        }

        public IEnumerable<StudentAttendanceDetail> GetListwithAttendanceTypeByAttendaceId(int attendanceId)
        {
            return _redeeppitory.Query(r => r.StudentAttendanceId == attendanceId).Include(g => g.Student).Include(g => g.Student.UserInfo).Include(g => g.AttendanceType).Include(g => g.AttendanceType.Colour).Select();
        }

        /// <summary>
        /// return prticular student attendence all record
        /// Author : Mohebbo
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns>IEnumerable<StudentAttendanceDetail></returns>
        public IEnumerable<StudentAttendanceDetail> AttendancesByStudentId(int studentId)
        {
            return _redeeppitory.Query()
                .Include(x => x.Student)
                .Include(x => x.StudentAttendance)
                .Select().Where(x => x.StudentId == studentId);
        }
        /// <summary>
        /// return prticular student attendence all record with in a date range
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns>IEnumerable<StudentAttendanceDetail></returns>
        public IEnumerable<StudentAttendanceDetail> AttendancesByStudentId(int studentId, DateTime fromDate, DateTime toDate)
        {
            return _redeeppitory.Query()
                .Include(x => x.Student)
                .Include(x => x.StudentAttendance)
                .Select().Where(x => x.StudentId == studentId && x.StudentAttendance.AttendanceDate > fromDate && x.StudentAttendance.AttendanceDate < toDate);
        }
        public List<StudentAttendanceDetail> GeStudentAttendanceDetailsList(int instituteId)
        {
            return _redeeppitory.Query(x=>x.InstituteId==instituteId).Select().ToList();
        }

        public void Update(StudentAttendanceDetail objStudentAttendanceDetail)
        {

           // StudentAttendance objStudentAttendance = new StudentAttendance();
          //  _redeeppitory1.UpdateRemoveSameKey(objStudentAttendance);

            _redeeppitory.Update(objStudentAttendanceDetail);
        }
    }
}