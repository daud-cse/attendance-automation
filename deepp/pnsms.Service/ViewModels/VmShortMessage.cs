using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pnsms.Entities.Models;

namespace pnsms.Service.ViewModels
{
    public class VmShortMessage
    {
        public ShortMessage ShortMessages { get; set; }
        public List<ShortMessageTemplate> ShortMessageTemplates { get; set; }

        public List<ShortMessageDetail> ShortMessageDetails { get; set; }
        public ShortMessageTemplate ShortMessageTemplate { get; set; }
        public Guardian Guardian { get; set; }
        public Student Student { get; set; }
        public Employee Employee { get; set; }
        public Teacher Teacher { get; set; }
    }
}
