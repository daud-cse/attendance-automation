using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class RoutineNote: Entity
    {
        public RoutineNote()
        {
            this.Routines = new List<Routine>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public string Name { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<Routine> Routines { get; set; }
    }
}
