using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class FeesType: Entity
    {
        public FeesType()
        {
            this.FeesAcademicClasses = new List<FeesAcademicClass>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int InstituteId { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> OrderBy { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public virtual ICollection<FeesAcademicClass> FeesAcademicClasses { get; set; }
        public virtual Institute Institute { get; set; }
    }
}
