using pnsms.Entities.Models;
using pnsms.Entities.ViewModels.Institutes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.ViewModels.Subjects
{
    public class VmSubjectAcademicClassMapping
    {

        public int Id { get; set; }

        public bool IsActive { get; set; }
        public int InstituteId { get; set; }

        public int AcademicBranchId { get; set; }
        public int AcademicClassId { get; set; }
        public int SubjectId { get; set; }
        public int SubjectMarks { get; set; }
        public Nullable<int> ParentSubjectId { get; set; }
        public Nullable<int> OrderBy { get; set; }
        public Nullable<int> MarksEntryTypeKey { get; set; }
        public Nullable<bool> IsSubjectGroupWise { get; set; }
        public int AcademicSessionId { get; set; }
        public Nullable<int> AcademicGroupId { get; set; }

        public string SubjectName = "";
        
        public int AcademicClassSectionMapId { get; set; }
        public string SectionName = "";
        public int TeacherId { get; set; }
        public string TeacherName = "";
        public int SubjectTypeId { get; set; }

        public int? SubjectGroupId { get; set; }

        public string SubjectGroupNameList { get; set; }
        public IEnumerable<VmAcademicClass> AcademicClassList { get; set; }
        public List<KeyValuePair<int, string>> AcademicGroupList { get; set; }

        public List<KeyValuePair<int, string>> AcademicSectionList { get; set; }
        public List<KeyValuePair<int, string>> TeacherList { get; set; }
        public List<KeyValuePair<int, string>> AcademicSessionList { get; set; }
        public List<KeyValuePair<int, string>> MarkEntryTypeList { get; set; }
        public List<KeyValuePair<int, string>> SubjectTypeList { get; set; }
        public List<KeyValuePair<int, string>> kvpSubjectList { get; set; }

        public List<KeyValuePair<int, string>> SubjectTypes { get; set; }
        public List<KeyValuePair<int, string>> SubjectGroups { get; set; }
        public List<VmSubject> SubjectList { get; set; }
        public List<SubjectType> subjectTypeDetailsList { get; set; }

      public  List<SubjectAcademicClassMapping> lstSubjectAcademicClassMapping { get; set; }
    }


}
