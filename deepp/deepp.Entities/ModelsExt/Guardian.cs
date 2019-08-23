using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using deepp.Entities.ModelsExt;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    [MetadataType(typeof(GuardianMetadata))]
    public partial class Guardian:Entity
    {
        public List<KeyValuePair<int, string>> GuardianTypesList { get; set; }
        public List<KeyValuePair<int, string>> EducationalQualificationList { get; set; }
        public List<KeyValuePair<int, string>> ProfessionList { get; set; }
        
    }
}
