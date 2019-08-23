using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class UserInfoSecurity: Entity
    {
        public int UserInfoId { get; set; }
        public Nullable<int> InstituteId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public bool IsActive { get; set; }
        public bool IsLockout { get; set; }
        public Nullable<System.DateTime> LastLoggedinAt { get; set; }
        public Nullable<System.DateTime> LastPasswordChangedAt { get; set; }
        public Nullable<System.DateTime> LastLockoutAt { get; set; }
        public Nullable<int> FailedPasswordAttemptCount { get; set; }
        public Nullable<System.DateTime> FailedPasswordAttemptWindowStart { get; set; }
        public string Comment { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}
