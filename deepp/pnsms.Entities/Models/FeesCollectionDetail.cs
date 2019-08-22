using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class FeesCollectionDetail: Entity
    {
        public int Id { get; set; }
        public int FeesCollectionId { get; set; }
        public int FeesGenerateId { get; set; }
        public decimal Amount { get; set; }
        public Nullable<bool> IsCompleted { get; set; }
        public virtual FeesCollection FeesCollection { get; set; }
        public virtual FeesGenerate FeesGenerate { get; set; }
    }
}
