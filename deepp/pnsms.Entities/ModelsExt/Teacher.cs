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
    [MetadataType(typeof(TeacherMetadata))]
    public partial class Teacher : Entity
    {
        public List<KeyValuePair<int, string>> MaritalStatusList { get; set; }
        public List<KeyValuePair<int, string>> DesignationList { get; set; }
        public List<KeyValuePair<int, string>> Departmentist { get; set; }
        public List<KeyValuePair<int, string>> AcademicBranchList { get; set; }
        public List<KeyValuePair<int, string>> AcademicClassList { get; set; }
        public List<KeyValuePair<int, string>> AcademicSectionList { get; set; }

       public List<int> AcademicBranches { get; set; }
      
    }
}
