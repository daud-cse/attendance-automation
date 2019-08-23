using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class FMS_RelatedAccount: Entity
    {
        public int RelatedAccountId { get; set; }
        public int InstituteId { get; set; }
        public Nullable<int> TransactionTypeId { get; set; }
        public Nullable<int> DebitAccountRestrictionTypeId { get; set; }
        public Nullable<int> CreditAccountRestrictionTypeId { get; set; }
        public Nullable<int> SourceAccountId { get; set; }
        public Nullable<int> CompromisedAccountId { get; set; }
        public Nullable<int> LedgerTypeId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
