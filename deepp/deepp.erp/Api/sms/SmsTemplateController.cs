using deepp.erp;
using deepp.Entities.Models;
using deepp.erp.Attributes;
using deepp.Service.ShortMessages;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace deepp.erp.Api
{
    public class SmsTemplateController : ApiController
    {
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IShortMessageTemplateService _shortMessageTemplateService;
        int institutionId = Sessions.InstituteId;
 
        
 

        
        #region "  -  [  Constractor  ]  -  "


       

        public SmsTemplateController(
             IUnitOfWorkAsync unitOfWorkAsync, IShortMessageTemplateService shortMessageTemplateService
            )
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _shortMessageTemplateService = shortMessageTemplateService;
        }

        #endregion

        #region "  -  [  CRUD  ]  -  "

        

        // GET api/smstemplate

        public IEnumerable<ShortMessageTemplate> Get(bool isActive = false)
        {

            return _shortMessageTemplateService.GetShortMessageTemplatesByInstituteId(Sessions.InstituteId);
        }

        public ShortMessageTemplate Get(int id)
        {
            var smstemplate = _shortMessageTemplateService.GetShortMessageTemplateById(Sessions.InstituteId, id);
            return smstemplate;
        }

        public HttpResponseMessage Post([FromBody]ShortMessageTemplate shortMessageTemplate)
        {
            shortMessageTemplate.InstituteId = Sessions.InstituteId;
            _shortMessageTemplateService.Insert(_unitOfWorkAsync, shortMessageTemplate);
            return new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = new JsonContent(new
                {
                    Id = shortMessageTemplate.Id,
                    Message = "Success"
                })
            };
        }

        // PUT api/smstemplate/5

        public void Put(int id, [FromBody]ShortMessageTemplate shortMessageTemplate)
        {
            shortMessageTemplate.InstituteId = Sessions.InstituteId;
            _shortMessageTemplateService.Update(_unitOfWorkAsync, shortMessageTemplate);

        }
        [Route("api/smstemplate/new")]
        [HttpGet]
        public ShortMessageTemplate GetNewShortMessageTemplate()
        {
            var mPaymentCreate = _shortMessageTemplateService.NewShortMessageTemplate(institutionId);
            return mPaymentCreate;
        }
        // DELETE api/smstemplate/5

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