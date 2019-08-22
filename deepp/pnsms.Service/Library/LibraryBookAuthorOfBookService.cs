using pnsms.Entities.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service.Library
{


    public interface ILibraryBookAuthorOfBookService : IService<LibraryBookAuthorOfBook>
    {
        /// <summary>
        /// Gets the library book authores by book identifier.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns></returns>
        IEnumerable<LibraryBookAuthore> GetLibraryBookAuthoresByBookId(int bookId);
        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="libraryBookAuthorOfBook">The library book author of book.</param>
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, LibraryBookAuthorOfBook libraryBookAuthorOfBook);
        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="libraryBookAuthorOfBook">The library book author of book.</param>
        void Update(IUnitOfWorkAsync unitOfWorkAsync, LibraryBookAuthorOfBook libraryBookAuthorOfBook);
        /// <summary>
        /// Deletes the by role identifier.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="roleId">The role identifier.</param>
        void DeleteByBookId(IUnitOfWorkAsync unitOfWorkAsync, int roleId);
    }
    public class LibraryBookAuthorOfBookService : Service<LibraryBookAuthorOfBook>, ILibraryBookAuthorOfBookService
    {


        #region  "  -  [  Constractor  ]  -  "

        private readonly IRepositoryAsync<LibraryBookAuthorOfBook> _repository;


        public LibraryBookAuthorOfBookService(IRepositoryAsync<LibraryBookAuthorOfBook> repository)
            : base(repository)
        {
            _repository = repository;
        }

        #endregion

        #region "  -  [  Crud  ]  -  "

        /// <summary>
        /// Gets the library book authores by book identifier.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns></returns>
        public IEnumerable<LibraryBookAuthore> GetLibraryBookAuthoresByBookId(int bookId)
        {
            return
                _repository.Query(r => r.LibraryBookId == bookId)
                    .Include(r => r.LibraryBookAuthore)
                    .Select(r => r.LibraryBookAuthore);
        }

        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="libraryBookAuthorOfBook">The library book author of book.</param>
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, LibraryBookAuthorOfBook libraryBookAuthorOfBook)
        {
            _repository.Insert(libraryBookAuthorOfBook);
            unitOfWorkAsync.SaveChanges();

        }

        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="libraryBookAuthorOfBook">The library book author of book.</param>
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, LibraryBookAuthorOfBook libraryBookAuthorOfBook)
        {

            _repository.Update(libraryBookAuthorOfBook);
            unitOfWorkAsync.SaveChanges();

        }
        /// <summary>
        /// Deletes the by book identifier.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="bookId">The book identifier.</param>
        public void DeleteByBookId(IUnitOfWorkAsync unitOfWorkAsync, int bookId)
        {
            var rights = _repository.Query(r => r.LibraryBookId == bookId).Select();
            if (rights != null)
                foreach (var libraryBookAuthorOfBook in rights)
                    _repository.Delete(libraryBookAuthorOfBook);
            unitOfWorkAsync.SaveChanges();

        }
        #endregion

       
    }
}
