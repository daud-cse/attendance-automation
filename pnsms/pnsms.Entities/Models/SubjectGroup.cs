using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class SubjectGroup: Entity
    {
        public SubjectGroup()
        {
            this.SubjectAcademicClassMappings = new List<SubjectAcademicClassMapping>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string Name { get; set; }
        public string SubjectName { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<SubjectAcademicClassMapping> SubjectAcademicClassMappings { get; set; }
    }
}
