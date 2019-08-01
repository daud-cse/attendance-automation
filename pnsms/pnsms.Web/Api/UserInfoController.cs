using pnsms.Entities.StoredProcedures.Models;
using pnsms.Service;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace pnsms.erp.Api
{
    public class UserInfoController : ApiController
    {

        
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly int userId = Sessions.UserId;
        private readonly int instituteId = Sessions.InstituteId;
        private readonly IStoredProcedureService _storedProcedureService;
        public UserInfoController(
              IStoredProcedureService storedProcedureService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _storedProcedureService = storedProcedureService;
            _unitOfWorkAsync = unitOfWorkAsync;

        }
        [HttpGet]
        [Route("api/UserInfoSearch")]
        public IEnumerable<VmUserInfo> SearchUserInfo(string searchItem,int userTypeId)
        {
            var userinfoList= _storedProcedureService.GetUserInfo(Sessions.InstituteId, Sessions.CurrentSessionId, userTypeId, searchItem);
            return userinfoList;
        }

    }
}
