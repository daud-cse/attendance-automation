using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class FMS_SubsidyAccount: Entity
    {
        public int SubsidyAccountId { get; set; }
        public Nullable<int> SubsidyTypeID { get; set; }
        public int SubsidyId { get; set; }
        public string SubsidyName { get; set; }
        public string BankName { get; set; }
        public string BankAccountNo { get; set; }
        public string BranchName { get; set; }
        public bool IsActive { get; set; }
        public string SetBy { get; set; }
        public string ActionType { get; set; }
        public System.DateTime SetDate { get; set; }
        public int InstituteId { get; set; }
    }
}
