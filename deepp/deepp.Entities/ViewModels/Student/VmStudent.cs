using System.Collections.Generic;
using deepp.Entities.Models;

namespace deepp.Entities.ViewModels.Student
{
    public class VmStudent
    {
        public List<KeyValuePair<int, string>> CountryList { get; set; }
        public List<DistrictOrState> DistrictOrStateList { get; set; } 
        //comments by mohebbo
        public UserInfo UserInfo { get; set; }
        public Models.Student Student { get; set; }
        public List<Guardian> Guardians { get; set; }
        public List<Address> Addresses { get; set; }
        public Address SingleAddresses { get; set; }
        public Guardian SingleGuardian { get; set; } 
        public byte[] ProfileImage { get; set; }
        public byte[] ProfileImageSmall { get; set; }

    }
}
