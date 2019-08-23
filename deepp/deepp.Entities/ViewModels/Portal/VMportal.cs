using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using deepp.Entities.Models;

namespace deepp.Entities.ViewModels.Portal
{
    public class VMportal
    {
        /// <summary>
        /// Number of Student in a gurdians
        /// </summary>
        public IEnumerable<GuardiansOfStudent> GuardiansOfStudent { get; set; }
        public IEnumerable<StudentAttendance> StudentAttendances { get; set; }
        public Guardian Guardian { get; set; }
        public MvcHtmlString MenuRights { get; set; }

        public IEnumerable<Notice> Notices { get; set; }

        public IEnumerable<Event> Events { get; set; }
        public int Inbox { get; set; }
        public int Outnox { get; set; }
    }
}
