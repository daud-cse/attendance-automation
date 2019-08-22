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
    public interface IGlobalDistrictService : IService<GlobalDistrict>
    {

        /// <summary>
        /// Gets the global country by institute identifier.
        /// </summary>
        /// <param name="isActive">The is active.</param>
        /// <returns></returns>
        IEnumerable<GlobalDistrict> GetGlobalDistricts(bool? isActive = null);
        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="GlobalDistrict">The global country.</param>
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, GlobalDistrict globalDistrict);
        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="GlobalDistrict">The global country.</param>
        void Update(IUnitOfWorkAsync unitOfWorkAsync, GlobalDistrict globalDistrict);

    }
    public class GlobalDistrictService : Service<GlobalDistrict>, IGlobalDistrictService
    {


        private readonly IRepositoryAsync<GlobalDistrict> _repository;


        public GlobalDistrictService(IRepositoryAsync<GlobalDistrict> repository)
            : base(repository)
        {
            _repository = repository;
        }

 

        public IEnumerable<GlobalDistrict> GetGlobalDistricts(bool? isActive=null)
        {
            return isActive != null ? _repository.Query(d => d.IsActive == isActive).Select() : _repository.Query().Select();
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, GlobalDistrict globalDistrict)
        {

            _repository.Insert(globalDistrict);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, GlobalDistrict globalDistrict)
        {

            _repository.Update(globalDistrict);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
