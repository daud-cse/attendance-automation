using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class GuardiansOfStudent: Entity
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int GuardianId { get; set; }
        public bool IsLocalGuardian { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public virtual Guardian Guardian { get; set; }
        public virtual Student Student { get; set; }
    }
}
