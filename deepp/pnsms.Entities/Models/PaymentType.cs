using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class PaymentType: Entity
    {
        public PaymentType()
        {
            this.MobilePayments = new List<MobilePayment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<MobilePayment> MobilePayments { get; set; }
    }
}
