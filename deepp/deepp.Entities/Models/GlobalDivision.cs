using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class GlobalDivision: Entity
    {
        public GlobalDivision()
        {
            this.GlobalDistricts = new List<GlobalDistrict>();
            this.GlobalUsers = new List<GlobalUser>();
            this.Institutes = new List<Institute>();
        }

        public int Id { get; set; }
        public int GlobalCountryId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public virtual GlobalCountry GlobalCountry { get; set; }
        public virtual ICollection<GlobalDistrict> GlobalDistricts { get; set; }
        public virtual ICollection<GlobalUser> GlobalUsers { get; set; }
        public virtual ICollection<Institute> Institutes { get; set; }
    }
}
