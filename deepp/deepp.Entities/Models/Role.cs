using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class Role: Entity
    {
        public Role()
        {
            this.RightsOfRoles = new List<RightsOfRole>();
            this.RolesOfUserInfoes = new List<RolesOfUserInfo>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsForEmployee { get; set; }
        public Nullable<bool> IsForTeacher { get; set; }
        public Nullable<bool> IsForStudent { get; set; }
        public Nullable<bool> IsForGuardian { get; set; }
        public Nullable<int> GuardianTypeId { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<bool> EnableGlobalUser { get; set; }
        public Nullable<int> GlobalUserTypeId { get; set; }
        public Nullable<bool> EnableGoverningbody { get; set; }
        public virtual GuardianType GuardianType { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<RightsOfRole> RightsOfRoles { get; set; }
        public virtual ICollection<RolesOfUserInfo> RolesOfUserInfoes { get; set; }
    }
}
