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
    public interface IGlobalSubDistrictTypeService : IService<GlobalSubDistrictType>
    {


        /// <summary>
        /// Gets the global sub district types.
        /// </summary>
        /// <param name="isActive">The is active.</param>
        /// <returns></returns>
        IEnumerable<GlobalSubDistrictType> GetGlobalSubDistrictTypes(bool? isActive = null);

        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="GlobalSubDistrictType">Type of the global sub district.</param>
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, GlobalSubDistrictType globalSubDistrictType);

        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="GlobalSubDistrictType">Type of the global sub district.</param>
        void Update(IUnitOfWorkAsync unitOfWorkAsync, GlobalSubDistrictType globalSubDistrictType);

    }
    public class GlobalSubDistrictTypeService : Service<GlobalSubDistrictType>, IGlobalSubDistrictTypeService
    {


        private readonly IRepositoryAsync<GlobalSubDistrictType> _redeeppitory;


        public GlobalSubDistrictTypeService(IRepositoryAsync<GlobalSubDistrictType> redeeppitory)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
        }

 

        public IEnumerable<GlobalSubDistrictType> GetGlobalSubDistrictTypes(bool? isActive=null)
        {
            return   _redeeppitory.Query().Select();
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, GlobalSubDistrictType globalSubDistrictType)
        {

            _redeeppitory.Insert(globalSubDistrictType);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, GlobalSubDistrictType globalSubDistrictType)
        {

            _redeeppitory.Update(globalSubDistrictType);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
