using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
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
