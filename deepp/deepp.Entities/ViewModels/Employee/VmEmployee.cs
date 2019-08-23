using System.Collections.Generic;
using deepp.Entities.Models;

namespace deepp.Entities.ViewModels.Employee
{
    public class VmEmployee
    {
        public List<KeyValuePair<int, string>> CountryList { get; set; }
        public List<DistrictOrState> DistrictOrStateList { get; set; }
        public UserInfo UserInfo { get; set; } 
        public List<Address> Addresses { get; set; }
        public Address SingleAddresses { get; set; }
        public byte[] ProfileImage { get; set; }
        public byte[] ProfileImageSmall { get; set; }
    }
}
