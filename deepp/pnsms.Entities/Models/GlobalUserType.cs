using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
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
