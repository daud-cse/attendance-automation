using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class GlobalSubDistrictType: Entity
    {
        public GlobalSubDistrictType()
        {
            this.GlobalSubDistricts = new List<GlobalSubDistrict>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<GlobalSubDistrict> GlobalSubDistricts { get; set; }
    }
}
