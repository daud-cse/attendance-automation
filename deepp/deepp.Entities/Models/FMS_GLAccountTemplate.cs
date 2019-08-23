using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class FMS_GLAccountTemplate: Entity
    {
        public int GLAccountId { get; set; }
        public string GLAccountCode { get; set; }
        public string GLAccountName { get; set; }
        public string GLAccountTreeName { get; set; }
        public Nullable<int> ParentAccountId { get; set; }
        public string SubCtrlPrefix { get; set; }
        public int AccountTypeID { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> OpeningDate { get; set; }
        public Nullable<decimal> LevelCode { get; set; }
        public Nullable<decimal> SortOrder { get; set; }
        public string SetBy { get; set; }
        public System.DateTime SetDate { get; set; }
        public Nullable<decimal> CurrentBalance { get; set; }
        public Nullable<decimal> CurrentYearOpeningBalance { get; set; }
        public bool IsSubsidyExist { get; set; }
        public bool HasChild { get; set; }
        public Nullable<int> ReportTypeID { get; set; }
        public string DrCr { get; set; }
    }
}