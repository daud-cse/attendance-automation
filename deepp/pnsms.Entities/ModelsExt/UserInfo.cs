using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    [MetadataType(typeof(UserInfoMetadata))]
    public partial class UserInfo:Entity
    {
        public List<KeyValuePair<int, string>> GenderList { get; set; }
        public List<KeyValuePair<int, string>> NationalityList { get; set; }
        public List<KeyValuePair<int, string>> ReligionList { get; set; }
        public IEnumerable<KeyValuePair<int, string>> BloodGroupList { get; set; }
        public IEnumerable<KeyValuePair<int, string>> RoleList { get; set; }
        public List<int> UserRoles { get; set; }
         
    }
}
