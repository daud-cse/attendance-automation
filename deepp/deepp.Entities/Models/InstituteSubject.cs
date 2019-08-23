using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class InstituteSubject: Entity
    {
        public InstituteSubject()
        {
            this.InstituteSubjectClasses = new List<InstituteSubjectClass>();
            this.SubjectAcademicClassMappings = new List<SubjectAcademicClassMapping>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public int SubjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual ICollection<InstituteSubjectClass> InstituteSubjectClasses { get; set; }
        public virtual ICollection<SubjectAcademicClassMapping> SubjectAcademicClassMappings { get; set; }
    }
}
