﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using pnsms.Entities.Models;
using pnsms.Service;
using Repository.Pattern.UnitOfWork;

namespace sfa.Api.Api
{
    public class RightsController : ApiController
    {
        private readonly IRightsService _iRightsService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public RightsController(IRightsService iRightsService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _iRightsService = iRightsService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        // GET api/rights
        public IEnumerable<Right> Get()
        {
            return _iRightsService.GetRights(Sessions.InstituteId);
        }

        // GET api/rights/5
        public Right Get(int id)
        {
            return _iRightsService.GetRightsById(id);
        }

        // POST api/rights
        public HttpResponseMessage Post([FromBody]Right right)
        {
             _iRightsService.Insert(_unitOfWorkAsync, right);
             return new HttpResponseMessage(HttpStatusCode.Created);
        }

        // PUT api/rights/5
        public void Put(int id, [FromBody]Right right)
        {
            _iRightsService.Update(_unitOfWorkAsync, right);
        }

        // DELETE api/rights/5
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
    }
}