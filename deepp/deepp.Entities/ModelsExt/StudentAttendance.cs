using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    [MetadataType(typeof(StudentAttendanceMetadata))]
    partial class StudentAttendance : Entity
    {
        public List<KeyValuePair<int, string>> AcademicBranchList { get; set; }
        public List<KeyValuePair<int, string>> AcademicClassList { get; set; }
        public List<KeyValuePair<int, string>> AcademicSectionList { get; set; }
        public List<KeyValuePair<int, string>> AcademicPeriodList { get; set; }
        public List<KeyValuePair<int, string>> TeacherList { get; set; }
        public string BranchName;
        public string ClassName;
        public string PeriodName;
        public string SectionName;
        public string SessionName;
        public string TeacherName;
        public string SubjectName;
       
    }

    public class StudentAttendanceListModel : Entity
    {
        public int Id { get; set; }

        public string PeriodName { get; set; }
        public string SubjectName { get; set; }
        public string TeacherName { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int? TotalStudent { get; set; }
        public int? PresentCount { get; set; }
        public int? AbsentCount { get; set; }
        public int? AbscondingCount { get; set; }
        public decimal? PresentPercent { get; set; }
        public decimal? AbsentPercent { get; set; }
        public decimal? AbscondingPercent { get; set; }
        public DateTime Date { get; set; }
    }
}
