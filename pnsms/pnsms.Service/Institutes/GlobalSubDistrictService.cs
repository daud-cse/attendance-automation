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
    public interface IGlobalSubDistrictService : IService<GlobalSubDistrict>
    {

        /// <summary>
        /// Gets the global country by institute identifier.
        /// </summary>
        /// <param name="isActive">The is active.</param>
        /// <returns></returns>
        IEnumerable<GlobalSubDistrict> GetGlobalSubDistricts(bool? isActive = null);
        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="globalSubDistrict">The global sub district.</param>
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, GlobalSubDistrict globalSubDistrict);
        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="globalSubDistrict">The global sub district.</param>
        void Update(IUnitOfWorkAsync unitOfWorkAsync, GlobalSubDistrict globalSubDistrict);

    }
    public class GlobalSubDistrictService : Service<GlobalSubDistrict>, IGlobalSubDistrictService
    {


        private readonly IRepositoryAsync<GlobalSubDistrict> _repository;


        public GlobalSubDistrictService(IRepositoryAsync<GlobalSubDistrict> repository)
            : base(repository)
        {
            _repository = repository;
        }

 

        public IEnumerable<GlobalSubDistrict> GetGlobalSubDistricts(bool? isActive=null)
        {
            return isActive != null ? _repository.Query(d => d.IsActive == isActive).Select() : _repository.Query().Select();
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, GlobalSubDistrict globalSubDistrict)
        {

            _repository.Insert(globalSubDistrict);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, GlobalSubDistrict globalSubDistrict)
        {

            _repository.Update(globalSubDistrict);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
