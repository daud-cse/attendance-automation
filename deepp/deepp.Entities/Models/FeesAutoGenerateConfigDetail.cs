using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class FeesAutoGenerateConfigDetail: Entity
    {
        public int Id { get; set; }
        public int FeesAutoGenerateConfigId { get; set; }
        public int FeesHeadId { get; set; }
        public decimal Amount { get; set; }
        public virtual FeesAutoGenerateConfig FeesAutoGenerateConfig { get; set; }
        public virtual FeesHead FeesHead { get; set; }
    }
}
