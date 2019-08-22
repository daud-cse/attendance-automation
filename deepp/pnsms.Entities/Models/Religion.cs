using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class Religion: Entity
    {
        public Religion()
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
        public virtual ICollection<AdmissionForm> AdmissionForms { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<UserInfo> UserInfoes { get; set; }
    }
}
