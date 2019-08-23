using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class DistrictOrState: Entity
    {
        public DistrictOrState()
        {
            this.Addresses = new List<Address>();
            this.AdmissionFormAddresses = new List<AdmissionFormAddress>();
        }

        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<int> GlobalDistrictId { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<AdmissionFormAddress> AdmissionFormAddresses { get; set; }
        public virtual Country Country { get; set; }
        public virtual GlobalDistrict GlobalDistrict { get; set; }
    }
}
