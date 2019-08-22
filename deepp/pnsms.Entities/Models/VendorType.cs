using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class VendorType: Entity
    {
        public VendorType()
        {
            this.VendorInfoes = new List<VendorInfo>();
        }

        public int VendorTypeId { get; set; }
        public string VendorTypeName { get; set; }
        public int InstituteId { get; set; }
        public virtual ICollection<VendorInfo> VendorInfoes { get; set; }
    }
}
