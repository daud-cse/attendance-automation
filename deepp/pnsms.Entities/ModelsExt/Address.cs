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
    [MetadataType(typeof(AddressMetadata))]
    public partial class Address:Entity
    {
        public List<KeyValuePair<int, string>> AddressTypeList { get; set; }
        public List<KeyValuePair<int, string>> DistrictList { get; set; }
    }
}
