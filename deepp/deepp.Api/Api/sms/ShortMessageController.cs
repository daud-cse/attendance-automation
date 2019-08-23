using deepp.Entities.Models;
using deepp.Service;
using deepp.Service.ShortMessages;
using deepp.Service.ViewModels;
using Repository.Pattern.UnitOfWork;
using deepp.Api.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace deepp.Api.Api
{
    public class ShortMessageController : ApiController
    {
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IShortMessageService _shortMessageService;
        
        int institutionId = Sessions.InstituteId;
 
        
 

        
        #region "  -  [  Constractor  ]  -  "


       

        public ShortMessageController(
             IUnitOfWorkAsync unitOfWorkAsync, IShortMessageService shortMessageService
            )
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _shortMessageService = shortMessageService;
        }

        #endregion

        #region "  -  [  CRUD  ]  -  "

        

        // GET api/ShortMessage

        public IEnumerable<ShortMessage> Get(bool isActive = false)
        {

            return _shortMessageService.GetShortMessageByInstituteId(Sessions.InstituteId);
        }

        public ShortMessage Get(int id)
        {
            var shortMessage = _shortMessageService.GetShortMessageById(Sessions.InstituteId, id);
            return shortMessage;
        }

        public HttpResponseMessage Post([FromBody]VmShortMessage vmShortMessage)
        {
            //vmShortMessage.ShortMessages.DateFrom = DateTime.Now;
            //vmShortMessage.ShortMessages.DateTo = DateTime.Now; 
            vmShortMessage.ShortMessages.InstituteId = Sessions.InstituteId;
            _shortMessageService.Insert(_unitOfWorkAsync, vmShortMessage);
            return new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = new JsonContent(new
                {
                    Id = vmShortMessage.ShortMessages.Id,
                    Message = "Success"
                })
            };
        }

        // PUT api/ShortMessage/5

        public void Put(int id, [FromBody]ShortMessage shortMessage)
        {
            shortMessage.InstituteId = Sessions.InstituteId;
            _shortMessageService.Update(_unitOfWorkAsync, shortMessage);

        }

        [Route("api/ShortMessage/new")]
        [HttpGet]
        public VmShortMessage GetNewShortMessageTemplate()
        {
            var shortMessage = _shortMessageService.GetNewShortMessage(institutionId,Sessions.UserId);
            return shortMessage;
        }
        // DELETE api/ShortMessage/5

        public void Delete(int id)
        {
        }

         
        #endregion

        #region "  -  [  Others  ]  -  "
        
       

        protected override void Dispose(bool disdeepping)
        {
            if (disdeepping)
            {
                _unitOfWorkAsync.Dispose();
            }
            base.Dispose(disdeepping);
        }

        #endregion


    }
}