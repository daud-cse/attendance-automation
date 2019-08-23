using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class BankAccountType: Entity
    {
        public BankAccountType()
        {
            this.BankAccountInfoes = new List<BankAccountInfo>();
        }

        public int BankAccountTypeId { get; set; }
        public string BankAccountTypeName { get; set; }
        public int InstituteId { get; set; }
        public virtual ICollection<BankAccountInfo> BankAccountInfoes { get; set; }
    }
}
