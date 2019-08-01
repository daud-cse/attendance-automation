using System;
using System.Collections.Generic;
using System.Linq;
using pnsms.Entities.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

namespace pnsms.Service.Library
{
    /// <summary>
    /// LibraryBook interface Service
    /// </summary>
    public interface ILibraryBookService
    {
        /// <summary>
        /// Gets the LibraryBooks.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="isActive">The is active.</param>
        /// <returns></returns>
        IEnumerable<LibraryBook> GetLibraryBooks(int instituteId, bool? isActive = null);
        /// <summary>
        /// Gets the LibraryBook by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        LibraryBook GetLibraryBookById(int id);
        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="libraryBook">The LibraryBook.</param>
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, LibraryBook libraryBook);
        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="libraryBook">The LibraryBook.</param>
        void Update(IUnitOfWorkAsync unitOfWorkAsync, LibraryBook libraryBook);
   
    }
    /// <summary>
    /// LibraryBook Service
    /// </summary>
    public class LibraryBookService : ILibraryBookService
    {

        #region "  -  [  Constractor  ]  -  "

        private readonly IRepositoryAsync<LibraryBook> _repository;
        private readonly ILibraryBookAuthorOfBookService _libraryBookAuthorOfBookService;
      

        public LibraryBookService(IRepositoryAsync<LibraryBook> repository, ILibraryBookAuthorOfBookService libraryBookAuthorOfBookService)
        {
            _repository = repository;
            _libraryBookAuthorOfBookService = libraryBookAuthorOfBookService;
           
        }

        #endregion

        #region "  -  [  Crud  ]  -  "

        /// <summary>
        /// Gets the LibraryBook.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="isActive">The is active.</param>
        /// <returns></returns>
        public IEnumerable<LibraryBook> GetLibraryBooks(int instituteId, bool? isActive = null)
        {
            return isActive != null
                ? _repository.Query(d => d.InstituteId.Equals(instituteId) && d.IsActive == isActive).Select()
                : _repository.Query(d => d.InstituteId == instituteId).Select();
        }
 
        /// <summary>
        /// Gets the LibraryBook by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public LibraryBook GetLibraryBookById(int id)
        {
            var libraryBook = _repository.Query(r => r.Id.Equals(id)).Select().SingleOrDefault();
            if (libraryBook != null)
            {
                libraryBook.LibraryBookAuthoreList = _libraryBookAuthorOfBookService.GetLibraryBookAuthoresByBookId(id).ToList();

            }
            return libraryBook;
        }

        /// <summary>
        /// Inserts the specified unit of work asynchronous for LibraryBook.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="libraryBook">The LibraryBook.</param>
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, LibraryBook libraryBook)
        {
            try
            {
                unitOfWorkAsync.BeginTransaction(isolationLevel: System.Data.IsolationLevel.Unspecified);

                _repository.Insert(libraryBook);
                unitOfWorkAsync.SaveChanges();

                foreach (var r in libraryBook.LibraryBookAuthoreList)
                {

                    var authorOfLibraryBook = new LibraryBookAuthorOfBook() { LibraryBookAuthorId = r.Id, LibraryBookId = libraryBook.Id };
                    _libraryBookAuthorOfBookService.Insert(unitOfWorkAsync, authorOfLibraryBook);
                }
                unitOfWorkAsync.Commit();
            }
            catch (Exception)
            {
                unitOfWorkAsync.Rollback();
                throw;

            }

        }

        /// <summary>
        /// Updates the specified unit of work asynchronous for LibraryBook.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="libraryBook">The LibraryBook.</param>
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, LibraryBook libraryBook)
        {
            try
            {
                unitOfWorkAsync.BeginTransaction(isolationLevel: System.Data.IsolationLevel.Unspecified);

                _repository.Update(libraryBook);
                unitOfWorkAsync.SaveChanges();
                if (libraryBook.LibraryBookAuthoreList != null)
                {
                    _libraryBookAuthorOfBookService.DeleteByBookId(unitOfWorkAsync, libraryBook.Id);
                    foreach (var r in libraryBook.LibraryBookAuthoreList)
                    {
                        var rightsOfLibraryBook = new LibraryBookAuthorOfBook() { LibraryBookAuthorId = r.Id, LibraryBookId = libraryBook.Id };
                        _libraryBookAuthorOfBookService.Insert(unitOfWorkAsync, rightsOfLibraryBook);
                    }
                }

                unitOfWorkAsync.Commit();
            }
            catch (Exception)
            {
                unitOfWorkAsync.Rollback();
                throw;

            }
        }
        #endregion
         
    }
}
