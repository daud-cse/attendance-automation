using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pnsms.Entities.Models;

namespace pnsms.Entities.ViewModels.Teacher
{
    public class VmTeacher
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
