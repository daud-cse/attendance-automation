using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using pnsms.Entities.Models;
using pnsms.Service;
using pnsms.Service.Library;
using Repository.Pattern.UnitOfWork;

namespace pnsms.erp.Api
{
    public class LibraryBookController : ApiController
    {
        #region "  -  [  Constractor  ]  -  "

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly ILibraryBookService _libraryBookService;

        public LibraryBookController(IUnitOfWorkAsync unitOfWorkAsync, ILibraryBookService libraryBookService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _libraryBookService = libraryBookService;
        }

        #endregion

        #region "  -  [  Crud  ]  -  "

        // GET api/LibraryBook
        public IEnumerable<LibraryBook> Get()
        {
            return _libraryBookService.GetLibraryBooks(Sessions.InstituteId);
        }

        // GET api/LibraryBook/5
        public LibraryBook Get(int id)
        {
            return _libraryBookService.GetLibraryBookById(id);
        }

        // POST api/LibraryBook
        public HttpResponseMessage Post([FromBody] LibraryBook libraryBook)
        {

            libraryBook.InstituteId = Sessions.InstituteId;
            _libraryBookService.Insert(_unitOfWorkAsync, libraryBook);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        // PUT api/LibraryBook/5
        public void Put(int id, [FromBody] LibraryBook libraryBook)
        {
            libraryBook.InstituteId = Sessions.InstituteId;
            _libraryBookService.Update(_unitOfWorkAsync, libraryBook);
        }

        // DELETE api/LibraryBook/5
        public void Delete(int id)
        {
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWorkAsync.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

       
    }
}
