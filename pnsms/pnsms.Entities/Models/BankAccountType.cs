using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
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