using deepp.erp;
using deepp.Entities.Models;
using deepp.Service;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace deepp.erp.Api
{
    public class IncomeHeadController : ApiController
    {
        private readonly IChartOfAccountService _chartOfAService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly int instituteId = Sessions.InstituteId;

        public IncomeHeadController(IChartOfAccountService chartOfAService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _chartOfAService = chartOfAService;
            _unitOfWorkAsync = unitOfWorkAsync;


        }
        // GET api/ChartOfAccount
        public IEnumerable<ChartOfAccount> Get()
        {
            return _chartOfAService.GetAllIncomeHeads(instituteId);
        }


        public ChartOfAccount Get(int id)
        {
            return _chartOfAService.GetById(id);
        }

        // POST api/ChartOfAccount
        [Validate]
        public HttpResponseMessage Post([FromBody]ChartOfAccount incomeHead)
        {
            incomeHead.InstituteId = instituteId;
            _chartOfAService.SaveIncomeHead(_unitOfWorkAsync, incomeHead);
      
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/ChartOfAccount/5
        [Validate]
        public void Put(int id, [FromBody]ChartOfAccount incomeHead)
        {

            incomeHead.InstituteId = instituteId;
            _chartOfAService.Update(_unitOfWorkAsync, incomeHead);
        

        }

        protected override void Dispose(bool disdeepping)
        {
            if (disdeepping)
            {
                _unitOfWorkAsync.Dispose();
            }
            base.Dispose(disdeepping);
        }

    }
}
