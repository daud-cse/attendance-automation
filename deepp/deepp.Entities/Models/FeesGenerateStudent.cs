using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class FeesGenerateStudent: Entity
    {
        public FeesGenerateStudent()
        {
            this.FeesGenerateStudentDetails = new List<FeesGenerateStudentDetail>();
        }

        public int Id { get; set; }
        public int FeesGenerateId { get; set; }
        public int StudentId { get; set; }
        public decimal TotalAmountDue { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal AmountDue { get; set; }
        public Nullable<bool> IsCompleted { get; set; }
        public Nullable<bool> IsPublished { get; set; }
        public Nullable<bool> HasAnyAdvance { get; set; }
        public virtual FeesGenerate FeesGenerate { get; set; }
        public virtual ICollection<FeesGenerateStudentDetail> FeesGenerateStudentDetails { get; set; }
        public virtual Student Student { get; set; }
    }
}
