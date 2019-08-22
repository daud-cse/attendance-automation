using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class FeesGenerateHead: Entity
    {
        public int Id { get; set; }
        public int FeesGenerateId { get; set; }
        public int FeesHeadId { get; set; }
        public decimal Amount { get; set; }
        public virtual FeesGenerate FeesGenerate { get; set; }
        public virtual FeesHead FeesHead { get; set; }
    }
}
