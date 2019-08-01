using pnsms.Entities.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service
{


    /// <summary>
    /// ScholarshipOfStudent
    /// </summary>
    public interface IScholarshipOfStudentService : IService<ScholarshipOfStudent>
    {

        /// <summary>
        /// Gets the scholarship of student by student identifier.
        /// </summary>
        /// <param name="studentId">The student identifier.</param>
        /// <returns></returns>
        IEnumerable<ScholarshipOfStudent> GetScholarshipOfStudentByStudentId(int studentId);
        /// <summary>
        /// Deletes the scholarship of student.
        /// </summary>
        /// <param name="studentId">The student identifier.</param>
        /// <returns></returns>
        bool DeleteScholarshipOfStudent(int studentId);

        /// <summary>
        /// Saves the scholarship of student.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="studentId">The student identifier.</param>
        /// <param name="academicSessionId">The academic session identifier.</param>
        /// <param name="coCurricularActivities">The co curricular activities.</param>
        void SaveScholarshipOfStudent(IUnitOfWorkAsync unitOfWorkAsync, int studentId,int academicSessionId,
            List<int> coCurricularActivities);

        IEnumerable<ScholarshipOfStudent> GetScholarshipOfStudentByInstituteId(int instituteId);
    }
    /// <summary>
    /// ScholarshipOfStudent
    /// </summary>
    public class ScholarshipOfStudentService : Service<ScholarshipOfStudent>, IScholarshipOfStudentService
    {

        /// <summary>
        /// The _repository
        /// </summary>
        private readonly IRepositoryAsync<ScholarshipOfStudent> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScholarshipOfStudentService" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ScholarshipOfStudentService(IRepositoryAsync<ScholarshipOfStudent> repository)
            : base(repository)
        {
            _repository = repository;
        }


        /// <summary>
        /// Gets the scholarship of student by student identifier.
        /// </summary>
        /// <param name="studentId">The student identifier.</param>
        /// <returns></returns>
        public IEnumerable<ScholarshipOfStudent> GetScholarshipOfStudentByStudentId(int studentId)
        {
            return   _repository.Query(d => d.StudentId.Equals(studentId)).Select();
        }

        /// <summary>
        /// Deletes the scholarship of student.
        /// </summary>
        /// <param name="studentId">The student identifier.</param>
        /// <returns></returns>
        public bool DeleteScholarshipOfStudent(int studentId)
        {

            var deleteItems = GetScholarshipOfStudentByStudentId(studentId);
            if (deleteItems != null)
            {
                foreach (var item in deleteItems)
                {
                    _repository.Delete(item);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Saves the scholarship of student.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="studentId">The student identifier.</param>
        /// <param name="academicSessionId">The academic session identifier.</param>
        /// <param name="scholarships">The scholarships.</param>
        public void SaveScholarshipOfStudent(IUnitOfWorkAsync unitOfWorkAsync, int studentId,int academicSessionId, List<int> scholarships)
        {
            foreach (int sid in scholarships)
            {
                _repository.Insert(new ScholarshipOfStudent
                {
                    StudentId = studentId,
                    ScholarshipId = sid,
                    AcademicSessionId = academicSessionId
                });
            }
            unitOfWorkAsync.SaveChanges();
             
        }

        /// <summary>
        /// Gets the scholarship of student by institute identifier.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <returns></returns>
        public IEnumerable<ScholarshipOfStudent> GetScholarshipOfStudentByInstituteId(int instituteId)
        {

            return _repository
                .Query(c => c.Student.UserInfo.InstituteId == instituteId)
                .Include(x => x.Student.UserInfo)
                .Include(x => x.Student.AcademicClass)
                .Include(x => x.Student.AcademicSession)
                .Include(x => x.Student.AcademicClassSectionMapping.AcademicSection)
                .Include(x => x.Scholarship).Include(x => x.AcademicSession).Select();
        }
    }
}
