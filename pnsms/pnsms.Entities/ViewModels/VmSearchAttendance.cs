using pnsms.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.ViewModels
{
    public class VmSearchAttendance
    {
        public List<KeyValuePair<int, string>> BranchList { get; set; }
        public int BranchId { get; set; }
        
        public List<KeyValuePair<int, string>> ClassList { get; set; }
        public int ClassId { get; set; }

        public List<KeyValuePair<int, string>> SectionList { get; set; }
        public int SectionId { get; set; }
        public List<KeyValuePair<int, string>> PeriodList { get; set; }
        public int PeriodId { get; set; }
        public List<KeyValuePair<int, string>> TeacherList { get; set; }

        public int InstituteId { get; set;}
        public List<KeyValuePair<int, string>> InstituteList { get; set; }
        public int TeacherId { get; set; }

        public int SubjectId { get; set; }
        public System.DateTime startDate { get; set; }
        public System.DateTime endDate { get; set; }

        public int UserInfoTypeId { get; set; }
        public int UserId { get; set; }

        public string instituteName { get; set; }
        public Image instituteLogo { get; set; }

        public IEnumerable<StudentAttendanceListModel> SearchData { get; set; }
        public IEnumerable<UserAttendance> SearchTeacherAttendanceData { get; set; }

        public UserAttendance objUserAttendance { get; set; }
        public IEnumerable<UserAttendanceDetail> lstserAttendanceDetail { get; set; }
        public IEnumerable<StudentAttendanceDetail> studentList { get; set; }

    }

}
