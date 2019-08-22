using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.Models
{
   public partial class ExamSubjectMark:Entity
    {
        public List<KeyValuePair<int, string>> AcademicBranchList { get; set; }
        public List<KeyValuePair<int, string>> AcademicClassList { get; set; }

        public List<KeyValuePair<int, string>> AcademicSectionList { get; set; }
        public List<KeyValuePair<int, string>> AcademicSessionList { get; set; }
        public List<KeyValuePair<int, string>> ExamTypeList { get; set; }
        public List<KeyValuePair<int, string>> ExamList { get; set; }
        public string StudentName;
        public string StudentRoleNo;
        public string SubjectName;
        public int SessionId;
        public int ExamTypeId;
        public decimal SubjectMarks;


    }
}
