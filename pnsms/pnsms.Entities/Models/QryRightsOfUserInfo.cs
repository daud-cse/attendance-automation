using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class QryRightsOfUserInfo: Entity
    {
        public Nullable<int> UserInfoId { get; set; }
        public Nullable<int> RoleId { get; set; }
        public int RightId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}