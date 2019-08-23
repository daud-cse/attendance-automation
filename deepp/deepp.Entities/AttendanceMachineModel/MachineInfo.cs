using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Entities.AttendanceMachineModel
{
    public class MachineInfo
    {
        public int MachineNumber { get; set; }
        public int IndRegID { get; set; }
        public string Name { get; set; }
        public string DateTimeRecord { get; set; }

        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }

        public DateTime DateOnlyRecord
        {
            get;set;
          //  get { return DateTime.Parse(DateTime.Parse(DateTimeRecord).ToString("yyyy-MM-dd")); }
        }
        public DateTime TimeOnlyRecord
        {
            get;set;
            //get { return DateTime.Parse(DateTime.Parse(DateTimeRecord).ToString("hh:mm:ss tt")); }
        }
        public string deviceinfo { get; set; }


    }
}
