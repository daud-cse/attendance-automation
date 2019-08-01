using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class Right: Entity
    {
        public Right()
        {
            this.RightsOfPackages = new List<RightsOfPackage>();
            this.RightsOfRoles = new List<RightsOfRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Nullable<bool> EnableEmployee { get; set; }
        public Nullable<bool> EnableTeacher { get; set; }
        public Nullable<bool> EnableStudent { get; set; }
        public Nullable<bool> EnableGuardian { get; set; }
        public Nullable<bool> EnableGlobalUser { get; set; }
        public Nullable<bool> EnableGoverningbody { get; set; }
        public virtual ICollection<RightsOfPackage> RightsOfPackages { get; set; }
        public virtual ICollection<RightsOfRole> RightsOfRoles { get; set; }
    }
}
