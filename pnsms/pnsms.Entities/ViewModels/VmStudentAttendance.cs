using pnsms.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.ViewModels
{
    public class VmStudentAttendance
    {
        public StudentAttendance StudentAttendance { get; set; }

        public List<StudentAttendance> lstStudentAttendance { get; set; }


        public List<SubjectAcademicClassMapping> lstSubjectAcademicClassMapping { get; set; }
        public IEnumerable<StudentAttendanceDetail> AttendanceDetails { get; set; }

        public string msg { get; set; }
    }
}
