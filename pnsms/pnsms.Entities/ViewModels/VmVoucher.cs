using pnsms.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.ViewModels
{
    public class VmVoucher
    {
        public Voucher Voucher { get; set; }
        public IEnumerable<VoucherDetail> IncomeList { get; set; }
        public IEnumerable<VoucherDetail> ExpenseList { get; set; }

    }
}
