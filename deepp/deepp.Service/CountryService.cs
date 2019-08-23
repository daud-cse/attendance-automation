using deepp.Entities.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Service
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


        private readonly IRepositoryAsync<Country> _redeeppitory;


        public CountryService(IRepositoryAsync<Country> redeeppitory)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
        }


        public IEnumerable<Country> GetCountrys(int instituteId)
        {

            return _redeeppitory.Query(c => c.InstituteId == instituteId).Select();
        }

        public IEnumerable<Country> GetCountrys(bool isActive)
        {
            if (isActive)
            {
                return _redeeppitory.Query().Select().Where(d => d.IsActive.Equals(true));
            }

            return _redeeppitory.Query().Select();
        }

        public IEnumerable<Country> GetActiveCountry()
        {
            return _redeeppitory.Query().Select().Where(d => d.IsActive == true);
        }
        public Country GetCountryById(int id)
        {
            return _redeeppitory.Query().Select().FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<Country> GetCountryByInstituteId(int instituteId)
        {
            return _redeeppitory.Query(d => d.IsActive == true && d.InstituteId == instituteId).Select();
        }

        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="country">The country.</param>
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, Country country)
        {
            country.LastUpdateTime = DateTime.Now;
            _redeeppitory.Insert(country);
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
            _redeeppitory.Update(country);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
