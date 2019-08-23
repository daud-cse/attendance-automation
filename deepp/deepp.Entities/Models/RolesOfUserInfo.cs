using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class RolesOfUserInfo: Entity
    {
        public int Id { get; set; }
        public int UserInfoId { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}
