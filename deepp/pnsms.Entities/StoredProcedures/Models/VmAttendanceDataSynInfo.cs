using pnsms.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.StoredProcedures.Models
{
    public class VmAttendanceDataSynInfo
    {
        //public List<AttendanceDataSynInfo> lstAttendanceDataSynInfo { get; set; }
        //public List<AttendanceType> lstAttendanceType { get;set;}
        public string Message{ get; set; }
        public string MachineSerialNo { get;set;}
        public string MachineSerialNo1 { get; set; }
        public string MachineSerialNo2 { get; set; }
        public string MachineSerialNo3 { get; set; }
        public string MachineSerialNo4 { get; set; }

        public List<VmAttendanceDataSynInfo> lstLastAttendanceSynDate { get; set; }
        public DateTime LastAttendanceSynDate { get; set; }
    }
}
