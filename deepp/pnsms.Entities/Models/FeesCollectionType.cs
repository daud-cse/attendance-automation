using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class FeesCollectionType: Entity
    {
        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<bool> IsShowRefNo { get; set; }
        public string RefNoTitle { get; set; }
        public Nullable<bool> IsShowMobileNo { get; set; }
        public string MobileNoTitle { get; set; }
    }
}
