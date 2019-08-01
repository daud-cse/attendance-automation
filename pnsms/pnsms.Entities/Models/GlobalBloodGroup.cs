using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class GlobalBloodGroup: Entity
    {
        public GlobalBloodGroup()
        {
            this.BloodGroups = new List<BloodGroup>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<BloodGroup> BloodGroups { get; set; }
    }
}
