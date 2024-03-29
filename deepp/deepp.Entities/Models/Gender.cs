using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class Gender: Entity
    {
        public Gender()
        {
            this.AdmissionForms = new List<AdmissionForm>();
            this.UserInfoes = new List<UserInfo>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<int> GlobalGenderId { get; set; }
        public virtual ICollection<AdmissionForm> AdmissionForms { get; set; }
        public virtual GlobalGender GlobalGender { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<UserInfo> UserInfoes { get; set; }
    }
}
