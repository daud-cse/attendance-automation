using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class WeekDay: Entity
    {
        public WeekDay()
        {
            this.RoutineDetails = new List<RoutineDetail>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public int DayOfWeek { get; set; }
        public string Name { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<RoutineDetail> RoutineDetails { get; set; }
    }
}
