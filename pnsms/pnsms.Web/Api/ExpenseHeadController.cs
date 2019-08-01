using pnsms.Entities.Models;
using pnsms.Service;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace pnsms.erp.Api
{
    public class ExpenseHeadController : ApiController
    {
        private readonly IChartOfAccountService _chartOfAService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly int instituteId = Sessions.InstituteId;

        public ExpenseHeadController(IChartOfAccountService chartOfAService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _chartOfAService = chartOfAService;
            _unitOfWorkAsync = unitOfWorkAsync;


        }
        // GET api/ChartOfAccount
        public IEnumerable<ChartOfAccount> Get()
        {
            return _chartOfAService.GetAllExpenseHeads(instituteId);
        }


        public ChartOfAccount Get(int id)
        {
            return _chartOfAService.GetById(id);
        }

        // POST api/ChartOfAccount
        [Validate]
        public HttpResponseMessage Post([FromBody]ChartOfAccount expenseHead)
        {
            expenseHead.InstituteId = instituteId;
            _chartOfAService.SaveExpenseHead(_unitOfWorkAsync, expenseHead);
      
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/ChartOfAccount/5
        [Validate]
        public void Put(int id, [FromBody]ChartOfAccount expenseHead)
        {

            expenseHead.InstituteId = instituteId;
            _chartOfAService.Update(_unitOfWorkAsync, expenseHead);
        

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
