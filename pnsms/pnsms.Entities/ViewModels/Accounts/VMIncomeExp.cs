using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.ViewModels.Accounts
{
    public class VMIncomeExp
    {
        public string HeadName { get; set; }
        public decimal Amount { get; set; }
        public bool? IsIncome { get; set; }
        public bool? IsExpense { get; set; }
    }
}
