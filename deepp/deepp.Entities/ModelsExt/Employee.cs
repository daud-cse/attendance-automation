using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;


namespace deepp.Entities.Models
{
    [MetadataType(typeof(EmployeeMetadata))]
    public partial class Employee : Entity
    {
        public List<KeyValuePair<int, string>> MaritalStatusList { get; set; }
        public List<KeyValuePair<int, string>> DesignationList { get; set; }
        public List<KeyValuePair<int, string>> DepartmentList { get; set; }
        public List<KeyValuePair<int, string>> AcademicBranchList { get; set; }
        public List<int> AcademicBranches { get; set; }
    }
}
