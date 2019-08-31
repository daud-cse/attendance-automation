using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class AcademicShift: Entity
    {
        public AcademicShift()
        {
            this.AcademicClassSectionMappings = new List<AcademicClassSectionMapping>();
            this.StudentAttendances = new List<StudentAttendance>();
            this.Students = new List<Student>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string Name { get; set; }
        public Nullable<System.TimeSpan> StartAt { get; set; }
        public Nullable<System.TimeSpan> EndAt { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<int> TagId { get; set; }
        public virtual ICollection<AcademicClassSectionMapping> AcademicClassSectionMappings { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual Tag Tag { get; set; }
        public virtual ICollection<StudentAttendance> StudentAttendances { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
