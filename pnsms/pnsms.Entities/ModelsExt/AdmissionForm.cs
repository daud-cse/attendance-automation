using pnsms.Entities.ModelsExt;
using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.Models
{
    [MetadataType(typeof(AdmissionFormMetadata))]
    public partial class AdmissionForm 
    {
        public List<KeyValuePair<int, string>> GenderList { get; set; }
        public List<KeyValuePair<int, string>> NationalityList { get; set; }
        public List<KeyValuePair<int, string>> ReligionList { get; set; }
        public List<KeyValuePair<int, string>> BloodGroupList { get; set; }
        public List<KeyValuePair<int, string>> AcademicSessionList { get; set; }
        public List<KeyValuePair<int, string>> AcademicClassList { get; set; }
        public List<KeyValuePair<int, string>> AcademicBranchList { get; set; }
        public List<KeyValuePair<int, string>> AddressTypeList { get; set; }
        public bool StatusDeatails;
        
    }
}
