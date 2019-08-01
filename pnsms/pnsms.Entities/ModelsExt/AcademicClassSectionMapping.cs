using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.Models
{
    public partial class AcademicClassSectionMapping : Entity
    {

        public string Name = "";
       // public bool IsActive;
        public List<KeyValuePair<int, string>> AcademicBranchList { get; set; }
        public List<KeyValuePair<int, string>> AcademicClassList { get; set; }
        public List<KeyValuePair<int, string>> AcademicShiftList { get; set; }
        public List<KeyValuePair<int, string>> AcademicSectionList { get; set; }
        public List<KeyValuePair<int, string>> TeacherList { get; set; }
        public List<KeyValuePair<int, string>> AcademicGroupList { get; set; }

    }
}
