using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class GlobalUserType: Entity
    {
        public GlobalUserType()
        {
            this.GlobalUsers = new List<GlobalUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<GlobalUser> GlobalUsers { get; set; }
    }
}
