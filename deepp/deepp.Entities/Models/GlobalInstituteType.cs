using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class GlobalInstituteType: Entity
    {
        public GlobalInstituteType()
        {
            this.Institutes = new List<Institute>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Institute> Institutes { get; set; }
    }
}
