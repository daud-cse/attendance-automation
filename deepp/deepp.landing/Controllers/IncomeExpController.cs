using deepp.Entities.ViewModels.Accounts;
using deepp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace deepp.landing.Controllers
{
    public class IncomeExpController : Controller
    {
        readonly IVoucherDetailService service;
        public IncomeExpController(IVoucherDetailService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            int year = DateTime.Now.Year;
            var years = new List<SelectListItem>();
            years.Add(new SelectListItem { Value = year.ToString(), Text = year.ToString(), Selected = true });
            years.Add(new SelectListItem { Value = (year - 1).ToString(), Text = (year - 1).ToString(), Selected = true });

            ViewBag.YearsId = years.ToSelectList(null, "Value", "Text");

            var data = service.GetAll(Sessions.InstituteId, year);
            
            var ret = data.GroupBy(c => c.ChartOfAccountId)
                .Select(c => new VMIncomeExp
                {
                    HeadName = c.First().ChartOfAccount.Name
                     ,
                    IsExpense = c.First().ChartOfAccount.IsExpense
                     ,
                    IsIncome = c.First().ChartOfAccount.IsIncome
                     ,
                    Amount = c.Sum(c2 => c2.Amount)
                }).OrderBy(c => c.IsIncome).OrderBy(c => c.IsExpense)
                ;


            return View(ret);
        }

        [HttpPost]
        public ActionResult Index(int YearsId)
        {
            int year = DateTime.Now.Year;
            var years = new List<SelectListItem>();
            years.Add(new SelectListItem { Value = year.ToString(), Text = year.ToString(), Selected = true });
            years.Add(new SelectListItem { Value = (year - 1).ToString(), Text = (year - 1).ToString(), Selected = true });

            ViewBag.YearsId = years.ToSelectList(YearsId, "Value", "Text");

            var data = service.GetAll(Sessions.InstituteId, YearsId);

            var ret = data.GroupBy(c => c.ChartOfAccountId)
                .Select(c => new VMIncomeExp
                {
                    HeadName = c.First().ChartOfAccount.Name
                     ,
                    IsExpense = c.First().ChartOfAccount.IsExpense
                     ,
                    IsIncome = c.First().ChartOfAccount.IsIncome
                     ,
                    Amount = c.Sum(c2 => c2.Amount)
                }).OrderBy(c => c.IsIncome).OrderBy(c => c.IsExpense)
                ;


            return View(ret);
        }
    }

}