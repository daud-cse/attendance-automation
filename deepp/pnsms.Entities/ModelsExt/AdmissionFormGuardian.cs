using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.Models
{
    [MetadataType(typeof(AdmissionFormGuardianMeatadata))]
    public partial class AdmissionFormGuardian
    {
        public List<KeyValuePair<int, string>> GuardianTypesList { get; set; }
        public List<KeyValuePair<int, string>> EducationalQualificationList { get; set; }
        public List<KeyValuePair<int, string>> ProfessionList { get; set; }
    }
}
