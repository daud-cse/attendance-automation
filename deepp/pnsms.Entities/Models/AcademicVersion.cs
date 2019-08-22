using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class AcademicVersion: Entity
    {
        public AcademicVersion()
        {
            this.FeesGenerateAcademics = new List<FeesGenerateAcademic>();
            this.Students = new List<Student>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<int> TagId { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual Tag Tag { get; set; }
        public virtual ICollection<FeesGenerateAcademic> FeesGenerateAcademics { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
