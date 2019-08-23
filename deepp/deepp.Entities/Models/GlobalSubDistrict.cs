using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class GlobalSubDistrict: Entity
    {
        public GlobalSubDistrict()
        {
            this.GlobalUsers = new List<GlobalUser>();
            this.Institutes = new List<Institute>();
        }

        public int Id { get; set; }
        public int GlobalDistrictId { get; set; }
        public int GlobalSubDistrictTypeId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public virtual GlobalDistrict GlobalDistrict { get; set; }
        public virtual GlobalSubDistrictType GlobalSubDistrictType { get; set; }
        public virtual ICollection<GlobalUser> GlobalUsers { get; set; }
        public virtual ICollection<Institute> Institutes { get; set; }
    }
}
