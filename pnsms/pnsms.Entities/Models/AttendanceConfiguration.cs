using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class AttendanceConfiguration: Entity
    {
        public int Id { get; set; }
        public string MachineNo { get; set; }
        public string MachineSerialNo { get; set; }
        public Nullable<int> InstituteId { get; set; }
        public Nullable<System.DateTime> AttendanceStartSynDate { get; set; }
        public Nullable<System.DateTime> AttendanceLastSynDate { get; set; }
        public string MachineNo1 { get; set; }
        public string MachineSerialNo1 { get; set; }
        public string MachineNo2 { get; set; }
        public string MachineSerialNo2 { get; set; }
        public string MachineNo3 { get; set; }
        public string MachineSerialNo3 { get; set; }
        public string MachineNo4 { get; set; }
        public string MachineSerialNo4 { get; set; }
    }
}
