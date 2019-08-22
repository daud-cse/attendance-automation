using pnsms.Entities.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service.Institutes
{


    /// <summary>
    /// 
    /// </summary>
    public interface IGlobalCountryService : IService<GlobalCountry>
    {

        /// <summary>
        /// Gets the global country by institute identifier.
        /// </summary>
        /// <param name="isActive">The is active.</param>
        /// <returns></returns>
        IEnumerable<GlobalCountry> GetGlobalCountries(bool? isActive = null);
        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="globalCountry">The global country.</param>
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, GlobalCountry globalCountry);
        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="globalCountry">The global country.</param>
        void Update(IUnitOfWorkAsync unitOfWorkAsync, GlobalCountry globalCountry);

    }
    public class GlobalCountryService : Service<GlobalCountry>, IGlobalCountryService
    {


        private readonly IRepositoryAsync<GlobalCountry> _repository;

        public GlobalCountryService(IRepositoryAsync<GlobalCountry> repository)
            : base(repository)
        {
            _repository = repository;
        }
        
        public IEnumerable<GlobalCountry> GetGlobalCountries(bool? isActive = null)
        {
            return isActive != null ? _repository.Query(d => d.IsActive == isActive).Select() : _repository.Query().Select();
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, GlobalCountry globalCountry)
        {

            _repository.Insert(globalCountry);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, GlobalCountry globalCountry)
        {

            _repository.Update(globalCountry);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
