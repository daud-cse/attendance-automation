using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class Voucher: Entity
    {
        public Voucher()
        {
            this.VoucherDetails = new List<VoucherDetail>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public int AcademicBranchId { get; set; }
        public System.DateTime VoucherDate { get; set; }
        public string Narration { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<bool> IsIncome { get; set; }
        public Nullable<bool> IsExpense { get; set; }
        public virtual AcademicBranch AcademicBranch { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<VoucherDetail> VoucherDetails { get; set; }
    }
}
