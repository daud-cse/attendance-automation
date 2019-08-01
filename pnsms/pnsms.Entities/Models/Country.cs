using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class Country: Entity
    {
        public Country()
        {
            this.DistrictOrStates = new List<DistrictOrState>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<int> GlobalCountryId { get; set; }
        public virtual GlobalCountry GlobalCountry { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<DistrictOrState> DistrictOrStates { get; set; }
    }
}
