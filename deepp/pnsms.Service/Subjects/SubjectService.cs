using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pnsms.Entities.Models;
using Repository.Pattern.Repositories;
using System.ComponentModel.DataAnnotations;

namespace pnsms.Service.Subjects
{
    /// <summary>
    /// Subjects interface Service
    /// </summary>
    public interface ISubjectService
    {


        /// <summary>
        /// Gets the Subjects.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <returns></returns>
        IEnumerable<Subject> Get( bool? isActive = null);
        IEnumerable<Subject> GetAll(bool IsActive);
        /// <summary>
        /// Gets the Subject by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Subject Get(int id);

        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="subject">The Subject.</param>
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, Subject subject);
        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="subject">The Subject.</param>
        void Update(IUnitOfWorkAsync unitOfWorkAsync, Subject subject);
        List<KeyValuePair<int, string>> GetKVP();
    }
    /// <summary>
    /// Subjects Service
    /// </summary>
    public class SubjectService : ISubjectService
    {

        #region "  -  [  Constractor  ]  -  "

        private readonly IRepositoryAsync<Subject> _repository;




        public SubjectService(IRepositoryAsync<Subject> repository)
        {
            _repository = repository;

        }

        #endregion

        #region "  -  [  Crud  ]  -  "



        public IEnumerable<Subject> Get( bool? isActive = null)
        {
            if (isActive != null)
            {

                var result = _repository.Query(c => c.IsActive == isActive)
                   .Select();
                return result;
            }
            else
            {

                var result = _repository.Query().Select();
                return result;
            }
        }

        public IEnumerable<Subject> GetAll(bool IsActive)
        {
            var result = _repository.Query(x=>x.IsActive==IsActive).Select();
            return result;
        }

        public Subject Get(int id)
        {
            var Subjects = _repository.Query(r => r.Id.Equals(id)).Select().SingleOrDefault();
            if (Subjects == null)
                throw new ValidationException("Invalid Subjects");
            return Subjects;
        }



        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, Subject subject)
        {
            try
            {
                unitOfWorkAsync.BeginTransaction(isolationLevel: System.Data.IsolationLevel.Unspecified);
                ValidateSubject(subject);

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

        private void ValidateSubject(Subject subject)
        {
            var shortNameExist = _repository.Query(s => s.ShortName == subject.ShortName && s.Id != subject.Id).Select().Any();
            if (shortNameExist)
                throw new ValidationException("Duplicate Short Name");
            var codeExist = _repository.Query(s => s.Code == subject.Code && s.Id != subject.Id).Select().Any();
            if (codeExist)
                throw new ValidationException("Duplicate Code");
        }


        public void Update(IUnitOfWorkAsync unitOfWorkAsync, Subject subject)
        {
            try
            {
                unitOfWorkAsync.BeginTransaction(isolationLevel: System.Data.IsolationLevel.Unspecified);
                ValidateSubject(subject);
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

        public List<KeyValuePair<int, string>> GetKVP()
        {
            var data = _repository.Query(r =>r.IsActive).Select().OrderBy(s => s.OrderBy).ToList();
            var list = new List<KeyValuePair<int, string>>();
            data.ForEach(c => list.Add(new KeyValuePair<int, string>(c.Id, c.Name)));

            return list;
        }

        #endregion

    }
}
