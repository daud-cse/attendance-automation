using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class Tag: Entity
    {
        public Tag()
        {
            this.AcademicBranches = new List<AcademicBranch>();
            this.AcademicClasses = new List<AcademicClass>();
            this.AcademicGroups = new List<AcademicGroup>();
            this.AcademicShifts = new List<AcademicShift>();
            this.AcademicVersions = new List<AcademicVersion>();
            this.ContentDetails = new List<ContentDetail>();
            this.Subjects = new List<Subject>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string TagText { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public virtual ICollection<AcademicBranch> AcademicBranches { get; set; }
        public virtual ICollection<AcademicClass> AcademicClasses { get; set; }
        public virtual ICollection<AcademicGroup> AcademicGroups { get; set; }
        public virtual ICollection<AcademicShift> AcademicShifts { get; set; }
        public virtual ICollection<AcademicVersion> AcademicVersions { get; set; }
        public virtual ICollection<ContentDetail> ContentDetails { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
