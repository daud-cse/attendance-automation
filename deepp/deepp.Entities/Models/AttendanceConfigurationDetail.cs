using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class AttendanceConfigurationDetail: Entity
    {
        public int Id { get; set; }
        public int AttendanceConfigurationId { get; set; }
        public string MachineNo { get; set; }
        public string MachineSerialNo { get; set; }
        public int MachineUseTypeId { get; set; }
        public Nullable<int> InstituteId { get; set; }
        public Nullable<System.DateTime> AttendanceStartSynDate { get; set; }
        public Nullable<System.DateTime> AttendanceLastSynDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public System.DateTime SetDate { get; set; }
        public string SetBy { get; set; }
        public virtual AttendanceConfiguration AttendanceConfiguration { get; set; }
    }
}
