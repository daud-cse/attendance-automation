using pnsms.Entities.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service.Subjects
{
    public interface ISubjectAcademicClassMappingSubjectTypeService : IService<SubjectAcademicClassMappingSubjectType>
    {



        /// <summary>
        /// Gets the specified institute identifier.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="subjectClassMappingId">The subject class mapping identifier.</param>
        /// <returns></returns>
        IEnumerable<SubjectAcademicClassMappingSubjectType> Get(int instituteId, int subjectClassMappingId);


        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        SubjectAcademicClassMappingSubjectType Get(int id);


        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="SubjectAcademicClassMappingSubjectType">Type of the subject academic class mapping subject.</param>
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, SubjectAcademicClassMappingSubjectType SubjectAcademicClassMappingSubjectType);

        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="SubjectAcademicClassMappingSubjectType">Type of the subject academic class mapping subject.</param>
        void Update(IUnitOfWorkAsync unitOfWorkAsync, SubjectAcademicClassMappingSubjectType SubjectAcademicClassMappingSubjectType);


    }
    public class SubjectAcademicClassMappingSubjectTypeService : Service<SubjectAcademicClassMappingSubjectType>, ISubjectAcademicClassMappingSubjectTypeService
    {

        #region "  -  [  Constractor  ]  -  "

        private readonly IRepositoryAsync<SubjectAcademicClassMappingSubjectType> _repository;




        public SubjectAcademicClassMappingSubjectTypeService(IRepositoryAsync<SubjectAcademicClassMappingSubjectType> repository)
            : base(repository)
        {
            _repository = repository;

        }

        #endregion

        #region "  -  [  Crud  ]  -  "




        public IEnumerable<SubjectAcademicClassMappingSubjectType> Get(int instituteId, int subjectClassMappingId)
        {

            var result = _repository.Query(c => c.SubjectAcademicClassMappingId == subjectClassMappingId && c.InstituteId == instituteId)
                   .Select();
            return result;

        }



        public SubjectAcademicClassMappingSubjectType Get(int id)
        {
            var subjectMappings = _repository.Query(r => r.Id.Equals(id)).Select().SingleOrDefault();
            if (subjectMappings == null)
                throw new ValidationException("Invalid Subjects");
            return subjectMappings;
        }



        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, SubjectAcademicClassMappingSubjectType subject)
        {
            try
            {
                unitOfWorkAsync.BeginTransaction(isolationLevel: System.Data.IsolationLevel.Unspecified);

                _repository.Insert(subject);
                unitOfWorkAsync.SaveChanges();



                unitOfWorkAsync.Commit();
            }
            catch (Exception)
            {
                unitOfWorkAsync.Rollback();
                throw;

            }

        }



        public void Update(IUnitOfWorkAsync unitOfWorkAsync, SubjectAcademicClassMappingSubjectType subject)
        {
            try
            {
                unitOfWorkAsync.BeginTransaction(isolationLevel: System.Data.IsolationLevel.Unspecified);

                _repository.Update(subject);
                unitOfWorkAsync.SaveChanges();

                unitOfWorkAsync.Commit();
            }
            catch (Exception)
            {
                unitOfWorkAsync.Rollback();
                throw;

            }
        }



        #endregion

    }
}
