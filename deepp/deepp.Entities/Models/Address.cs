using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class Address: Entity
    {
        public int Id { get; set; }
        public int AddressTypeId { get; set; }
        public int RefPrimaryKey { get; set; }
        public Nullable<int> DistrictOrStateId { get; set; }
        public string ZipCode { get; set; }
        public string AddressBody { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public virtual AddressType AddressType { get; set; }
        public virtual DistrictOrState DistrictOrState { get; set; }
    }
}
