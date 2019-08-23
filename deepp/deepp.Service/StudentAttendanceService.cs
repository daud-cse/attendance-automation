using deepp.Entities.Models;
using deepp.Entities.ViewModels;
using deepp.utility;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Service
{

    public interface IStudentAttendanceService : IService<StudentAttendance>
    {
        IEnumerable<StudentAttendance> GetAllStudentAttendance(VmSearchAttendance vmSearchAttendance);
        StudentAttendance GetStudentAttendance(StudentAttendance objStudentAttendance);
        StudentAttendance GetStudentAttendanceForApps(StudentAttendance objStudentAttendance);
        StudentAttendance GetStudentAttendanceById(int id, int instituteId);
        StudentAttendance GetStudentAttendanceForDetailsById(int attendanceId, int instituteId);
        StudentAttendance GetStudentAttendanceForPortal(int InstituteId, int Year, int Month, int Day);
        List<StudentAttendance> GeStudentAttendanceList(int instituteId);
    }
    public class StudentAttendanceService : Service<StudentAttendance>, IStudentAttendanceService
    {
        private readonly IRepositoryAsync<StudentAttendance> _redeeppitory;
        public StudentAttendanceService(IRepositoryAsync<StudentAttendance> redeeppitory)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
        }

        public IEnumerable<StudentAttendance> GetAllStudentAttendance(VmSearchAttendance vmSearchAttendance)
        {
            DateTime start = vmSearchAttendance.startDate;
            DateTime end = vmSearchAttendance.endDate.AddDays(1);

            var predicate = PredicateBuilder.True<StudentAttendance>();
            predicate = predicate.And(p => p.AttendanceDate >= start.Date && p.AttendanceDate < end);
            if (vmSearchAttendance != null)
            {

                if (vmSearchAttendance.BranchId > 0)
                    predicate = predicate.And(p => p.AcademicBranchId == vmSearchAttendance.BranchId);

                if (vmSearchAttendance.ClassId > 0)
                    predicate = predicate.And(p => p.AcademicClassId == vmSearchAttendance.ClassId);

                if (vmSearchAttendance.SectionId > 0)
                    predicate = predicate.And(p => p.AcademicSectionId == vmSearchAttendance.SectionId);

                if (vmSearchAttendance.TeacherId > 0)
                    predicate = predicate.And(p => p.TeacherId == vmSearchAttendance.TeacherId);

                if (vmSearchAttendance.UserId > 0)
                    predicate = predicate.And(p => p.TeacherId == vmSearchAttendance.UserId);

            }
            return _redeeppitory.Query(predicate)
                .Include(s => s.Teacher)
                .Include(s => s.Teacher.UserInfo)
                .Include(s => s.AcademicClass)
                .Include(s => s.AcademicPeriod)
                .Include(s => s.SubjectAcademicClassMapping.InstituteSubject.Subject)
                .Include(s => s.AcademicClassSectionMapping.AcademicSection).Select();

        }
        public StudentAttendance GetStudentAttendance(StudentAttendance objStudentAttendance)
        {
            // DateTime start = objStudentAttendance.startDate;
            // DateTime end = objStudentAttendance.endDate.AddDays(1);

            var predicate = PredicateBuilder.True<StudentAttendance>();
            // predicate = predicate.And(p => p.AttendanceDate >= start.Date && p.AttendanceDate < end);
            if (objStudentAttendance != null)
            {

                if (objStudentAttendance.AttendanceDate != null)
                    predicate = predicate.And(p => EntityFunctions.TruncateTime(p.AttendanceDate) == objStudentAttendance.AttendanceDate.Date);

                if (objStudentAttendance.AcademicBranchId > 0)
                    predicate = predicate.And(p => p.AcademicBranchId == objStudentAttendance.AcademicBranchId);

                if (objStudentAttendance.AcademicClassId > 0)
                    predicate = predicate.And(p => p.AcademicClassId == objStudentAttendance.AcademicClassId);

                if (objStudentAttendance.AcademicSectionId > 0)
                    predicate = predicate.And(p => p.AcademicSectionId == objStudentAttendance.AcademicSectionId);

                if (objStudentAttendance.AcademicPeriodId > 0)
                    predicate = predicate.And(p => p.AcademicPeriodId == objStudentAttendance.AcademicPeriodId);

                if (objStudentAttendance.TeacherId > 0)
                    predicate = predicate.And(p => p.TeacherId == objStudentAttendance.TeacherId);

                if (objStudentAttendance.SubjectAcademicClassMappingsId > 0)
                    predicate = predicate.And(p => p.SubjectAcademicClassMappingsId == objStudentAttendance.SubjectAcademicClassMappingsId);

                //if (objStudentAttendance.UserId > 0)
                //    predicate = predicate.And(p => p.TeacherId == objStudentAttendance.UserId);

            }
            return _redeeppitory.Query(predicate)
                   .Include(s => s.Teacher)
                    .Include(s => s.AcademicBranch)
                   .Include(s => s.Teacher.UserInfo)
                   .Include(s => s.AcademicClass)
                   .Include(s => s.AcademicClassSectionMapping.AcademicSection)
                   .Include(s => s.SubjectAcademicClassMapping.InstituteSubject.Subject)
                   .Include(s => s.AcademicPeriod).Select().FirstOrDefault();


        }
        public StudentAttendance GetStudentAttendanceForPortal(int InstituteId, int Year, int Month, int Day)
        {


            var predicate = PredicateBuilder.True<StudentAttendance>();



            predicate = predicate.And(p => p.InstituteId == InstituteId);


            if (Year != 0)
                predicate = predicate.And(p => p.AttendanceDate.Year == Year);

            if (Month != 0)
                predicate = predicate.And(p => p.AttendanceDate.Month == Month);


            if (Day != 0)
                predicate = predicate.And(p => p.AttendanceDate.Day == Day);


            return _redeeppitory.Query(predicate)
                    .Include(x => x.StudentAttendanceDetails)
                   .Include(s => s.AcademicPeriod).Select().FirstOrDefault();


        }
        public StudentAttendance GetStudentAttendanceForApps(StudentAttendance objStudentAttendance)
        {


            var predicate = PredicateBuilder.True<StudentAttendance>();
            if (objStudentAttendance != null)
            {


                if (objStudentAttendance.AttendanceDate != null)
                    predicate = predicate.And(p => EntityFunctions.TruncateTime(p.AttendanceDate) == objStudentAttendance.AttendanceDate.Date);

                if (objStudentAttendance.Id > 0)
                    predicate = predicate.And(p => p.Id == objStudentAttendance.Id);

                if (objStudentAttendance.AcademicBranchId > 0)
                    predicate = predicate.And(p => p.AcademicBranchId == objStudentAttendance.AcademicBranchId);

                if (objStudentAttendance.AcademicClassId > 0)
                    predicate = predicate.And(p => p.AcademicClassId == objStudentAttendance.AcademicClassId);

                if (objStudentAttendance.AcademicSectionId > 0)
                    predicate = predicate.And(p => p.AcademicSectionId == objStudentAttendance.AcademicSectionId);

                if (objStudentAttendance.AcademicPeriodId > 0)
                    predicate = predicate.And(p => p.AcademicPeriodId == objStudentAttendance.AcademicPeriodId);

                if (objStudentAttendance.TeacherId > 0)
                    predicate = predicate.And(p => p.TeacherId == objStudentAttendance.TeacherId);

                if (objStudentAttendance.SubjectAcademicClassMappingsId > 0)
                    predicate = predicate.And(p => p.SubjectAcademicClassMappingsId == objStudentAttendance.SubjectAcademicClassMappingsId);

                //if (objStudentAttendance.UserId > 0)
                //    predicate = predicate.And(p => p.TeacherId == objStudentAttendance.UserId);

            }
            return _redeeppitory.Query(predicate)
                   .Include(s => s.AcademicPeriod).Select().FirstOrDefault();
        }

        public StudentAttendance GetStudentAttendanceById(int attendanceId, int instituteId)
        {
            var student = _redeeppitory.Query()
                .Include(g => g.Teacher)
                .Include(g => g.Teacher.UserInfo)
                .Include(g => g.AcademicBranch)
                .Include(g => g.AcademicClass)
                .Include(g => g.AcademicClassSectionMapping.AcademicSection)
                .Include(g => g.AcademicSession)
                .Include(g => g.AcademicPeriod)
                .Select().Single(x => x.Id == attendanceId);

            return student;

        }

        public StudentAttendance GetStudentAttendanceForDetailsById(int attendanceId, int instituteId)
        {
            var student = _redeeppitory.Query()
                .Include(g => g.Teacher)
                .Include(g => g.Teacher.UserInfo)
                .Include(g => g.AcademicBranch)
                .Include(g => g.AcademicClass)
                .Include(g => g.AcademicClassSectionMapping.AcademicSection)
                .Include(g => g.AcademicSession)
                .Include(g => g.StudentAttendanceDetails)
                .Select().Single(x => x.Id == attendanceId);

            return student;

        }
        public List<StudentAttendance> GeStudentAttendanceList(int instituteId)
        {
            return _redeeppitory.Query(x => x.InstituteId == instituteId).Select().ToList();
        }


    }



}
