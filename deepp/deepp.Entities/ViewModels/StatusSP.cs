using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Entities.ViewModels
{
    public class StatusSP
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }      
        public IEnumerable<dynamic> Output { get; set; }
        public bool IsAllPromotedTaskDone { get; set; }
    }
}
