using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class Package: Entity
    {
        public Package()
        {
            this.Institutes = new List<Institute>();
            this.RightsOfPackages = new List<RightsOfPackage>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Institute> Institutes { get; set; }
        public virtual ICollection<RightsOfPackage> RightsOfPackages { get; set; }
    }
}
