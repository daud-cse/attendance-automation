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


        private readonly IRepositoryAsync<GlobalSubDistrict> _redeeppitory;


        public GlobalSubDistrictService(IRepositoryAsync<GlobalSubDistrict> redeeppitory)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
        }

 

        public IEnumerable<GlobalSubDistrict> GetGlobalSubDistricts(bool? isActive=null)
        {
            return isActive != null ? _redeeppitory.Query(d => d.IsActive == isActive).Select() : _redeeppitory.Query().Select();
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, GlobalSubDistrict globalSubDistrict)
        {

            _redeeppitory.Insert(globalSubDistrict);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, GlobalSubDistrict globalSubDistrict)
        {

            _redeeppitory.Update(globalSubDistrict);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
