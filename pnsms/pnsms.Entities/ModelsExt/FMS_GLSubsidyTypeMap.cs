using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.Models
{
    public partial class FMS_GLSubsidyTypeMap : Entity
    {
        public List<KeyValuePair<int, string>> kvpPostedGLAccount { get; set; }
        public List<KeyValuePair<int, string>> kvpSubsidyType { get; set; }

    }
}
