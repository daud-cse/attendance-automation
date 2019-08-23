using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class Colour: Entity
    {
        public Colour()
        {
            this.AttendanceTypes = new List<AttendanceType>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ColorCode { get; set; }
        public virtual ICollection<AttendanceType> AttendanceTypes { get; set; }
    }
}
