using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
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
