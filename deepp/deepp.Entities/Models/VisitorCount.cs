using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class VisitorCount: Entity
    {
        public int Id { get; set; }
        public int InstituteId { get; set; }
        public System.DateTime Date { get; set; }
        public int VisitorCount1 { get; set; }
        public virtual Institute Institute { get; set; }
    }
}
