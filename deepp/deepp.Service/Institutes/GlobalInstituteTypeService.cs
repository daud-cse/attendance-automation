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
    public interface IGlobalInstituteTypeService : IService<GlobalInstituteType>
    {

        /// <summary>
        /// Gets the global institute type by institute identifier.
        /// </summary>
        /// <param name="isActive">The is active.</param>
        /// <returns></returns>
        IEnumerable<GlobalInstituteType> GetGlobalInstituteTypes(bool? isActive = null);
        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="GlobalInstituteType">The global institute type.</param>
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, GlobalInstituteType globalInstituteType);
        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="GlobalInstituteType">The global institute type.</param>
        void Update(IUnitOfWorkAsync unitOfWorkAsync, GlobalInstituteType globalInstituteType);

    }
    public class GlobalInstituteTypeService : Service<GlobalInstituteType>, IGlobalInstituteTypeService
    {


        private readonly IRepositoryAsync<GlobalInstituteType> _redeeppitory;


        public GlobalInstituteTypeService(IRepositoryAsync<GlobalInstituteType> redeeppitory)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
        }

 

        public IEnumerable<GlobalInstituteType> GetGlobalInstituteTypes(bool? isActive=null)
        {
            return isActive != null ? _redeeppitory.Query(d => d.IsActive == isActive).Select() : _redeeppitory.Query().Select();
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, GlobalInstituteType globalInstituteType)
        {

            _redeeppitory.Insert(globalInstituteType);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, GlobalInstituteType globalInstituteType)
        {

            _redeeppitory.Update(globalInstituteType);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
