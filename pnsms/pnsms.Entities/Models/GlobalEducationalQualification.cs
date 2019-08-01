using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class GlobalEducationalQualification: Entity
    {
        public GlobalEducationalQualification()
        {
            this.EducationalQualifications = new List<EducationalQualification>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<EducationalQualification> EducationalQualifications { get; set; }
    }
}
