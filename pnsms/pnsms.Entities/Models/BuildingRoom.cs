using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class BuildingRoom: Entity
    {
        public BuildingRoom()
        {
            this.Diaries = new List<Diary>();
            this.RoutineDetails = new List<RoutineDetail>();
            this.Routines = new List<Routine>();
        }

        public int Id { get; set; }
        public int BuildingId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public virtual Building Building { get; set; }
        public virtual ICollection<Diary> Diaries { get; set; }
        public virtual ICollection<RoutineDetail> RoutineDetails { get; set; }
        public virtual ICollection<Routine> Routines { get; set; }
    }
}
