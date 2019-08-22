using pnsms.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.ViewModels
{
    public class VmUserAttendance
    {
        public UserAttendance UserAttendance { get; set; }
        public IEnumerable<UserAttendanceDetail> AttendanceDetails { get; set; }
        public List<KeyValuePair<int, string>> lstNotEntryUser { get; set; }
        public List<KeyValuePair<int, string>> lsInvalidUserEntry { get; set; }
        public string Message { get; set; }
    }

}
