using pnsms.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

namespace pnsms.Service.Library
{
    public interface ILibraryBookAuthoreService
    {
        IEnumerable<LibraryBookAuthore> GetLibraryBookAuthore(int instituteId, bool? isActive = null);
        LibraryBookAuthore GetLibraryBookAuthoreById(int id);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, LibraryBookAuthore libraryBookAuthore);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, LibraryBookAuthore libraryBookAuthore);
    }
    public class LibraryBookAuthoreService : ILibraryBookAuthoreService
    {
        #region    "  -  [  Constractor  ]  -  "

        private readonly IRepositoryAsync<LibraryBookAuthore> _repository;

        public LibraryBookAuthoreService(IRepositoryAsync<LibraryBookAuthore> repository)
        {

            _repository = repository;
        }


        #endregion

        #region "  -  [  Crud  ]  -  "

        /// <summary>
        /// Gets the LibraryBookAuthore.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public IEnumerable<LibraryBookAuthore> GetLibraryBookAuthore(int instituteId, bool? isActive = null)
        {
            return isActive != null
                ? _repository.Query(d => d.InstituteId.Equals(instituteId) && d.IsActive == isActive).Select()
                : _repository.Query(d => d.InstituteId == instituteId).Select();
        }

        /// <summary>
        /// Gets the LibraryBookAuthore by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public LibraryBookAuthore GetLibraryBookAuthoreById(int id)
        {
            return _repository.Query(r => r.Id.Equals(id)).Select().SingleOrDefault();
        }

        /// <summary>
        /// Inserts the specified unit of work asynchronous for LibraryBookAuthore.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="libraryBookAuthore">The LibraryBookAuthore.</param>
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, LibraryBookAuthore libraryBookAuthore)
        {

            _repository.Insert(libraryBookAuthore);
            unitOfWorkAsync.SaveChanges();

        }

        /// <summary>
        /// Updates the specified unit of work asynchronous for LibraryBookAuthore.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="libraryBookAuthore">The LibraryBookAuthore.</param>
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, LibraryBookAuthore libraryBookAuthore)
        {
            _repository.Update(libraryBookAuthore);
            unitOfWorkAsync.SaveChanges();

        }

        #endregion
    }
}
