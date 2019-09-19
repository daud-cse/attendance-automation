using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class MachineInfo: Entity
    {
        public int MachineInfoId { get; set; }
        public Nullable<int> InstituteId { get; set; }
        public Nullable<int> AcademicSessionId { get; set; }
        public Nullable<int> MachineNumber { get; set; }
        public string deviceinfo { get; set; }
        public int IndRegID { get; set; }
        public string Name { get; set; }
        public string DateTimeRecord { get; set; }
        public Nullable<System.DateTime> InTime { get; set; }
        public Nullable<System.DateTime> OutTime { get; set; }
        public string StudentOrTeacherId { get; set; }
        public System.DateTime DateOnlyRecord { get; set; }
        public Nullable<System.DateTime> TimeOnlyRecord { get; set; }
        public bool IsActive { get; set; }
        public Nullable<bool> IsProcess { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> UserId { get; set; }
    }
}
