using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class VoucherDetail: Entity
    {
        public int Id { get; set; }
        public int VoucherId { get; set; }
        public int ChartOfAccountId { get; set; }
        public decimal Amount { get; set; }
        public string Narration { get; set; }
        public virtual ChartOfAccount ChartOfAccount { get; set; }
        public virtual Voucher Voucher { get; set; }
    }
}
