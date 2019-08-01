using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class GlobalDistrict: Entity
    {
        public GlobalDistrict()
        {
            this.DistrictOrStates = new List<DistrictOrState>();
            this.GlobalSubDistricts = new List<GlobalSubDistrict>();
            this.GlobalUsers = new List<GlobalUser>();
            this.Institutes = new List<Institute>();
        }

        public int Id { get; set; }
        public int GlobalDivisionId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<DistrictOrState> DistrictOrStates { get; set; }
        public virtual GlobalDivision GlobalDivision { get; set; }
        public virtual ICollection<GlobalSubDistrict> GlobalSubDistricts { get; set; }
        public virtual ICollection<GlobalUser> GlobalUsers { get; set; }
        public virtual ICollection<Institute> Institutes { get; set; }
    }
}
