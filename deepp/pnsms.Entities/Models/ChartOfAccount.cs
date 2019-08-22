using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class ChartOfAccount: Entity
    {
        public ChartOfAccount()
        {
            this.ChartOfAccounts1 = new List<ChartOfAccount>();
            this.VoucherDetails = new List<VoucherDetail>();
        }

        public int Id { get; set; }
        public Nullable<int> ParentId { get; set; }
        public int InstituteId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Nullable<bool> IsAsset { get; set; }
        public Nullable<bool> IsLiabilities { get; set; }
        public Nullable<bool> IsIncome { get; set; }
        public Nullable<bool> IsExpense { get; set; }
        public Nullable<bool> IsCapital { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public virtual ICollection<ChartOfAccount> ChartOfAccounts1 { get; set; }
        public virtual ChartOfAccount ChartOfAccount1 { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<VoucherDetail> VoucherDetails { get; set; }
    }
}
