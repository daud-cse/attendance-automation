using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class FMS_MonthlyLedgerSummary: Entity
    {
        public int GLAccountId { get; set; }
        public int SummaryYM { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<decimal> DrAmount { get; set; }
        public Nullable<decimal> CrAmount { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public int InstituteId { get; set; }
    }
}
