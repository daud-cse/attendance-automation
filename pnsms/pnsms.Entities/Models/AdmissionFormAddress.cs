using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class AdmissionFormAddress: Entity
    {
        public int Id { get; set; }
        public int AdmissionFormId { get; set; }
        public Nullable<int> DistrictOrStateId { get; set; }
        public string ZipCode { get; set; }
        public string AddressBody { get; set; }
        public virtual AdmissionForm AdmissionForm { get; set; }
        public virtual DistrictOrState DistrictOrState { get; set; }
    }
}
