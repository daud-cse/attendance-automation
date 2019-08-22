using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.Models
{
     [MetadataType(typeof(AdmissionFormAddressMetadata))]
    public partial class AdmissionFormAddress
    {
         public List<KeyValuePair<int, string>> DistrictOrStateList { get; set; }
    }
}
