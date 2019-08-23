using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Entities.ViewModels
{
    public class VmCommonSearch
    {
        public int InstituteId { get; set; }
        public int CurrentSessionId { get; set; }
        public int ExamTypeId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public int AcademicBranchId { get; set; }
        public int AcademicClassesId { get; set; }
        public int AcademicGroupId { get; set; }
        public int AcademicClassesSectionMapId { get; set; }
        public int  SubjectAcademicClassMappingsMapId{get;set;}
        public int AcademicSectionId { get; set; }

        public int AcademicPeriodId { get; set; }

        public int StudentId { get; set; }

        public int UserInfoId { get; set; }
        public int ExamId { get; set; }
        public int InstituteSubjectClassId { get; set; }
        public int AcademicDepartmentId{ get; set; }

        public int SubjectByAcademicClassSectionId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }

        public List<KeyValuePair<int, string>> SubjectList {get;set;}
        public List<KeyValuePair<int, string>> AcademicBranchList { get; set; }
        public List<KeyValuePair<int, string>> AcademicClassList { get; set; }
        public List<KeyValuePair<int, string>> AcademicSectionList { get; set; }
        public List<KeyValuePair<int, string>> AcademicPeriodList { get; set; }
        public List<KeyValuePair<int, string>> TeacherList { get; set; }
        public List<KeyValuePair<int, string>> AcademicDepartmentList { get; set; }
    }
}
