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
   
    public interface ICountryService : IService<Country>
    {
        IEnumerable<Country> GetCountrys(int instituteId);
        IEnumerable<Country> GetCountrys(bool isActive);
        IEnumerable<Country> GetActiveCountry();
        Country GetCountryById(int id);
        IEnumerable<Country> GetCountryByInstituteId(int instituteId);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, Country country);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, Country country);
    }
    public class CountryService : Service<Country>, ICountryService
    {


        private readonly IRepositoryAsync<Country> _repository;


        public CountryService(IRepositoryAsync<Country> repository)
            : base(repository)
        {
            _repository = repository;
        }


        public IEnumerable<Country> GetCountrys(int instituteId)
        {

            return _repository.Query(c => c.InstituteId == instituteId).Select();
        }

        public IEnumerable<Country> GetCountrys(bool isActive)
        {
            if (isActive)
            {
                return _repository.Query().Select().Where(d => d.IsActive.Equals(true));
            }

            return _repository.Query().Select();
        }

        public IEnumerable<Country> GetActiveCountry()
        {
            return _repository.Query().Select().Where(d => d.IsActive == true);
        }
        public Country GetCountryById(int id)
        {
            return _repository.Query().Select().FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<Country> GetCountryByInstituteId(int instituteId)
        {
            return _repository.Query(d => d.IsActive == true && d.InstituteId == instituteId).Select();
        }

        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="country">The country.</param>
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, Country country)
        {
            country.LastUpdateTime = DateTime.Now;
            _repository.Insert(country);
            unitOfWorkAsync.SaveChanges();

        }
        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="country">The country.</param>
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, Country country)
        {
            country.LastUpdateTime = DateTime.Now;
            _repository.Update(country);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
