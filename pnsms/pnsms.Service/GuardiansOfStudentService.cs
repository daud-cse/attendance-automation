using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using pnsms.Entities.Models;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace pnsms.Service
{
    public interface IGuardiansOfStudentService : IService<GuardiansOfStudent>
    {
        /// <summary>
        /// Gets the guardians of student.
        /// </summary>
        /// <param name="studentId">The student identifier.</param>
        /// <returns></returns>
        IEnumerable<GuardiansOfStudent> GetGuardiansOfStudent(int studentId);
        /// <summary>
        /// Gets the students by guardian.
        /// </summary>
        /// <param name="guardianId">The guardian identifier.</param>
        /// <returns></returns>
        IEnumerable<GuardiansOfStudent> GetStudentsByGuardian(int guardianId);

        /// <summary>
        /// Gets the guardians by student ids.
        /// </summary>
        /// <param name="studentIds">The student ids.</param>
        /// <returns></returns>
        IEnumerable<GuardiansOfStudent> GetGuardiansByStudentIds(int[] studentIds);
    }

    public class GuardiansOfStudentService : Service<GuardiansOfStudent>, IGuardiansOfStudentService
    {
        private readonly IRepositoryAsync<GuardiansOfStudent> _repository;


        public GuardiansOfStudentService(IRepositoryAsync<GuardiansOfStudent> repository)
            : base(repository)
        {
            _repository = repository;

        }

        /// <summary>
        /// Gets the guardians of student.
        /// </summary>
        /// <param name="studentId">The student identifier.</param>
        /// <returns></returns>
        public IEnumerable<GuardiansOfStudent> GetGuardiansOfStudent(int studentId)
        {
            return _repository.Query(x => x.StudentId == studentId)
                .Include(x => x.Student.UserInfo)
                .Include(x => x.Guardian.UserInfo)
                .Select();
        }
        /// <summary>
        /// Gets the students by guardian.
        /// </summary>
        /// <param name="guardianId">The guardian identifier.</param>
        /// <returns></returns>
        public IEnumerable<GuardiansOfStudent> GetStudentsByGuardian(int guardianId)
        {
            return _repository.Query(x => x.GuardianId == guardianId)
                .Include(x => x.Guardian.UserInfo)
                .Include(x => x.Student.UserInfo)
                .Select();
        }

        /// <summary>
        /// Gets the guardians by student ids.
        /// </summary>
        /// <param name="studentIds">The student ids.</param>
        /// <returns></returns>
        public IEnumerable<GuardiansOfStudent> GetGuardiansByStudentIds(int[] studentIds)
        {
            return _repository.Query(x =>studentIds.Contains(x.StudentId))
               .Include(x => x.Guardian).Include(x => x.Guardian.UserInfo).Include(x => x.Student.UserInfo)
                .Select();
        }
    }
}
