using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.Models
{
    public partial class VendorInfo : Entity
    {
        public List<KeyValuePair<int, string>> kvpVendorType { get; set; }
    }
}
