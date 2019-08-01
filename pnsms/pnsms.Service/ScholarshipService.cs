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
    /// Scholarship Service
    /// </summary>
    public interface IScholarshipService : IService<Scholarship>
    {

        /// <summary>
        /// Gets the scholarship by institute identifier.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="isActive">The is active.</param>
        /// <returns></returns>
        IEnumerable<Scholarship> GetScholarshipByInstituteId(int instituteId, bool? isActive = null);
        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="scholarship">The scholarship.</param>
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, Scholarship scholarship);
        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="scholarship">The scholarship.</param>
        void Update(IUnitOfWorkAsync unitOfWorkAsync, Scholarship scholarship);

    }
    /// <summary>
    /// Scholarship Service
    /// </summary>
    public class ScholarshipService : Service<Scholarship>, IScholarshipService
    {

        /// <summary>
        /// The _repository
        /// </summary>
        private readonly IRepositoryAsync<Scholarship> _repository;
        /// <summary>
        /// Initializes a new instance of the <see cref="ScholarshipService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ScholarshipService(IRepositoryAsync<Scholarship> repository)
            : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets the scholarship by institute identifier.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="isActive">The is active.</param>
        /// <returns></returns>
        public IEnumerable<Scholarship> GetScholarshipByInstituteId(int instituteId, bool? isActive = null)
        {
            return isActive != null ? _repository.Query(d => d.InstituteId.Equals(instituteId) && d.IsActive == isActive).Select() : _repository.Query(d => d.InstituteId == instituteId).Select();
        }
        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="scholarship">The scholarship.</param>
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, Scholarship scholarship)
        {
            scholarship.LastUpdateTime = DateTime.Now;
            _repository.Insert(scholarship);
            unitOfWorkAsync.SaveChanges();

        }
        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="scholarship">The scholarship.</param>
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, Scholarship scholarship)
        {
            scholarship.LastUpdateTime = DateTime.Now;
            _repository.Update(scholarship);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
