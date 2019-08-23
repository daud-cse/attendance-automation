using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class ScholarshipOfStudent: Entity
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ScholarshipId { get; set; }
        public int AcademicSessionId { get; set; }
        public virtual AcademicSession AcademicSession { get; set; }
        public virtual Scholarship Scholarship { get; set; }
        public virtual Student Student { get; set; }
    }
}
