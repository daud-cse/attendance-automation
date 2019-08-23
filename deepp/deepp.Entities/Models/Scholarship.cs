using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class Scholarship: Entity
    {
        public Scholarship()
        {
            this.ScholarshipOfStudents = new List<ScholarshipOfStudent>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<int> GlobalScholarshipId { get; set; }
        public virtual GlobalScholarship GlobalScholarship { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<ScholarshipOfStudent> ScholarshipOfStudents { get; set; }
    }
}
