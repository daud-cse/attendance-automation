using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class FeesCollectionAdvance: Entity
    {
        public int StudentId { get; set; }
        public decimal Amount { get; set; }
        public virtual Student Student { get; set; }
    }
}
