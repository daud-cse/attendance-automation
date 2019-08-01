using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class EducationalQualification: Entity
    {
        public EducationalQualification()
        {
            this.AdmissionFormGuardians = new List<AdmissionFormGuardian>();
            this.Guardians = new List<Guardian>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<int> GlobalEducationalQualificationId { get; set; }
        public virtual ICollection<AdmissionFormGuardian> AdmissionFormGuardians { get; set; }
        public virtual GlobalEducationalQualification GlobalEducationalQualification { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<Guardian> Guardians { get; set; }
    }
}
