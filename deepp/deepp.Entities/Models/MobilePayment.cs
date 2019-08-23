using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class MobilePayment: Entity
    {
        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string MobileNo { get; set; }
        public Nullable<int> ReffStudentId { get; set; }
        public decimal Payment { get; set; }
        public string TransactionId { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public int PaymentTypeId { get; set; }
        public int LastActionBy { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual Student Student { get; set; }
    }
}
