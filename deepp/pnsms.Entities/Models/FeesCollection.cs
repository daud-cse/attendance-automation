using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class FeesCollection: Entity
    {
        public FeesCollection()
        {
            this.FeesCollectionDetails = new List<FeesCollectionDetail>();
        }

        public int Id { get; set; }
        public int StudentId { get; set; }
        public System.DateTime CollectionDate { get; set; }
        public int FeesHeadsId { get; set; }
        public int AcademicClassId { get; set; }
        public int AcademicSessionId { get; set; }
        public decimal TotalReceiveAmount { get; set; }
        public int Month { get; set; }
        public int InstituteId { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public virtual ICollection<FeesCollectionDetail> FeesCollectionDetails { get; set; }
        public virtual FeesHead FeesHead { get; set; }
        public virtual Student Student { get; set; }
    }
}
