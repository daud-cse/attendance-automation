using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class MaritalStatus: Entity
    {
        public MaritalStatus()
        {
            this.Employees = new List<Employee>();
            this.GlobalUsers = new List<GlobalUser>();
            this.Teachers = new List<Teacher>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<GlobalUser> GlobalUsers { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
