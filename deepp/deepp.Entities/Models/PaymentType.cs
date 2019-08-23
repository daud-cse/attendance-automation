using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
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
