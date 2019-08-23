using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class AcademicClassSectionMapping: Entity
    {
        public AcademicClassSectionMapping()
        {
            this.Exams = new List<Exam>();
            this.StudentAttendances = new List<StudentAttendance>();
            this.Students = new List<Student>();
            this.SubjectAcademicClassMappings = new List<SubjectAcademicClassMapping>();
            this.Teachers = new List<Teacher>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public int AcademicBranchId { get; set; }
        public Nullable<int> AcademicShiftId { get; set; }
        public int AcademicClassId { get; set; }
        public int AcademicSectionId { get; set; }
        public string SectionName { get; set; }
        public Nullable<int> ClassTeacherId { get; set; }
        public Nullable<int> AssClassTeacherId { get; set; }
        public Nullable<int> AcademicGroupId { get; set; }
        public bool IsActive { get; set; }
        public virtual AcademicBranch AcademicBranch { get; set; }
        public virtual AcademicClass AcademicClass { get; set; }
        public virtual AcademicGroup AcademicGroup { get; set; }
        public virtual AcademicSection AcademicSection { get; set; }
        public virtual AcademicShift AcademicShift { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Employee Employee1 { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
        public virtual ICollection<StudentAttendance> StudentAttendances { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<SubjectAcademicClassMapping> SubjectAcademicClassMappings { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
