using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.Models
{
   
    [MetadataType(typeof(GlobalUserMetadata))]
    public partial class GlobalUser : Entity
    {
        public List<KeyValuePair<int, string>> MaritalStatusList { get; set; }
        public List<KeyValuePair<int, string>> DesignationList { get; set; }
        public List<KeyValuePair<int, string>> DepartmentList { get; set; }
        public List<KeyValuePair<int, string>> AcademicBranchList { get; set; }
        public List<KeyValuePair<int, string>> CountryList { get; set; }
        public List<KeyValuePair<int, string>>DistrictOrStateList { get; set; }
        public List<int> AcademicBranches { get; set; }
    }
}

