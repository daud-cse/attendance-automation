using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.Models
{
    public partial class ExamTypeWiseTabulationSheetMaster
    {

        public List<KeyValuePair<int, string>> AcademicBranchList { get; set; }
        public List<KeyValuePair<int, string>> AcademicClassList { get; set; }

        public List<KeyValuePair<int, string>> AcademicSectionList { get; set; }
        public List<KeyValuePair<int, string>> AcademicSessionList { get; set; }
        public List<KeyValuePair<int, string>> ExamTypeList { get; set; }

        public List<KeyValuePair<int, string>> SubjectList { get; set; }
    }
}
