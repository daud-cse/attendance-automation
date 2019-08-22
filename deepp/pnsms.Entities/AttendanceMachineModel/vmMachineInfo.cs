using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.AttendanceMachineModel
{
    public class vmMachineInfo
    {
        public MachineInfo objMachineInfo { get; set; }
        public List<MachineInfo> lstMachineInfo { get; set; }

        public string message { get; set; }
    }
}
