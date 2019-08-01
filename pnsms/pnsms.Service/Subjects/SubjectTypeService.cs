using pnsms.Entities.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service.Subjects
{
    public interface ISubjectTypeService
    {


        /// <summary>
        /// Gets the Subjects.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <returns></returns>
        IEnumerable<SubjectType> Get(int instituteId, bool? isActive = null);

        /// <summary>
        /// Gets the Subject by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        SubjectType Get(int id);

        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="subject">The Subject.</param>
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, SubjectType subjectType);
        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="subject">The Subject.</param>
        void Update(IUnitOfWorkAsync unitOfWorkAsync, SubjectType subjectType);
        List<KeyValuePair<int, string>> GetKVP(int instituteId);
    }
    public class SubjectTypeService : ISubjectTypeService
    {

        #region "  -  [  Constractor  ]  -  "

        private readonly IRepositoryAsync<SubjectType> _repository;




        public SubjectTypeService(IRepositoryAsync<SubjectType> repository)
        {
            _repository = repository;

        }

        #endregion

        #region "  -  [  Crud  ]  -  "



        /// <summary>
        /// Gets the Subjects.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public IEnumerable<SubjectType> Get(int instituteId, bool? isActive = null)
        {
            if (isActive != null)
            {

                var result = _repository.Query(c => c.IsActive == isActive && c.InstituteId == instituteId)
                   .Select();
                return result;
            }
            else
            {

                var result = _repository.Query(c => c.InstituteId == instituteId).Select();
                return result;
            }
        }



        public SubjectType Get(int id)
        {
            var Subjects = _repository.Query(r => r.Id.Equals(id)).Select().SingleOrDefault();
            if (Subjects == null)
                throw new ValidationException("Invalid Subjects");
            return Subjects;
        }



        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, SubjectType subject)
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



        public void Update(IUnitOfWorkAsync unitOfWorkAsync, SubjectType subject)
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

        public List<KeyValuePair<int, string>> GetKVP(int instituteId)
        {
            var data = _repository.Query(r => r.InstituteId == instituteId && r.IsActive).Select().ToList();
            var list = new List<KeyValuePair<int, string>>();
            data.ForEach(c => list.Add(new KeyValuePair<int, string>(c.Id, c.Name)));

            return list;
        }

        #endregion

    }
}
