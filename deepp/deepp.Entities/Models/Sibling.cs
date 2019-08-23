using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class Sibling: Entity
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SiblingId { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public virtual Student Student { get; set; }
        public virtual Student Student1 { get; set; }
    }
}
