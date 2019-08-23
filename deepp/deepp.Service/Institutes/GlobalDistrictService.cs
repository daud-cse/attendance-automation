using deepp.Entities.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Service.Institutes
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


        private readonly IRepositoryAsync<GlobalDistrict> _redeeppitory;


        public GlobalDistrictService(IRepositoryAsync<GlobalDistrict> redeeppitory)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
        }

 

        public IEnumerable<GlobalDistrict> GetGlobalDistricts(bool? isActive=null)
        {
            return isActive != null ? _redeeppitory.Query(d => d.IsActive == isActive).Select() : _redeeppitory.Query().Select();
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, GlobalDistrict globalDistrict)
        {

            _redeeppitory.Insert(globalDistrict);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, GlobalDistrict globalDistrict)
        {

            _redeeppitory.Update(globalDistrict);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
