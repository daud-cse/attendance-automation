using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class GlobalProfession: Entity
    {
        public GlobalProfession()
        {
            this.Professions = new List<Profession>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Profession> Professions { get; set; }
    }
}
