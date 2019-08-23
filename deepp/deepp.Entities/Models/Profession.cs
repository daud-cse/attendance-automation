using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class Profession: Entity
    {
        public Profession()
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
        public Nullable<int> GlobalProfessionId { get; set; }
        public virtual ICollection<AdmissionFormGuardian> AdmissionFormGuardians { get; set; }
        public virtual GlobalProfession GlobalProfession { get; set; }
        public virtual ICollection<Guardian> Guardians { get; set; }
        public virtual Institute Institute { get; set; }
    }
}
