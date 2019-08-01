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


        private readonly IRepositoryAsync<GlobalInstituteType> _repository;


        public GlobalInstituteTypeService(IRepositoryAsync<GlobalInstituteType> repository)
            : base(repository)
        {
            _repository = repository;
        }

 

        public IEnumerable<GlobalInstituteType> GetGlobalInstituteTypes(bool? isActive=null)
        {
            return isActive != null ? _repository.Query(d => d.IsActive == isActive).Select() : _repository.Query().Select();
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, GlobalInstituteType globalInstituteType)
        {

            _repository.Insert(globalInstituteType);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, GlobalInstituteType globalInstituteType)
        {

            _repository.Update(globalInstituteType);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
