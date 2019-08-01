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

namespace sfa.Api.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class LibraryBookAuthoreController : ApiController
    {
        #region "  -  [  Constractor  ]  -  "

        private readonly ILibraryBookAuthoreService _ibraryBookAuthoreService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public LibraryBookAuthoreController(ILibraryBookAuthoreService libraryBookAuthoreService,
            IUnitOfWorkAsync unitOfWorkAsync)
        {
            _ibraryBookAuthoreService = libraryBookAuthoreService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        #endregion

        #region "  -  [  Crud  ]  -  "

        // GET api/LibraryBookAuthore
        /// <summary>
        /// Gets the specified is active.
        /// </summary>
        /// <param name="isActive">The is active.</param>
        /// <returns></returns>
        public IEnumerable<LibraryBookAuthore> Get(bool? isActive = null)
        {
            return _ibraryBookAuthoreService.GetLibraryBookAuthore(Sessions.InstituteId, isActive);
        }

        // GET api/LibraryBookAuthore/5
        public LibraryBookAuthore Get(int id)
        {
            return _ibraryBookAuthoreService.GetLibraryBookAuthoreById(id);
        }

        // POST api/LibraryBookAuthore
        public HttpResponseMessage Post([FromBody] LibraryBookAuthore libraryBookAuthore)
        {
            libraryBookAuthore.InstituteId = Sessions.InstituteId;
            libraryBookAuthore.IsActive =true;
            libraryBookAuthore.LastUpdateTime = DateTime.Now;
            _ibraryBookAuthoreService.Insert(_unitOfWorkAsync, libraryBookAuthore);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        // PUT api/LibraryBookAuthore/5
        public void Put(int id, [FromBody] LibraryBookAuthore libraryBookAuthore)
        {
            libraryBookAuthore.InstituteId = Sessions.InstituteId;
            
            libraryBookAuthore.LastUpdateTime = DateTime.Now;
            _ibraryBookAuthoreService.Update(_unitOfWorkAsync, libraryBookAuthore);
        }

        // DELETE api/LibraryBookAuthore/5
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
