using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class AcademicSection: Entity
    {
        public AcademicSection()
        {
            this.AcademicClassSectionMappings = new List<AcademicClassSectionMapping>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public virtual ICollection<AcademicClassSectionMapping> AcademicClassSectionMappings { get; set; }
        public virtual Institute Institute { get; set; }
    }
}
