using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class UserInfoType: Entity
    {
        public UserInfoType()
        {
            this.UserAttendances = new List<UserAttendance>();
            this.UserInfoes = new List<UserInfo>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<UserAttendance> UserAttendances { get; set; }
        public virtual ICollection<UserInfo> UserInfoes { get; set; }
    }
}