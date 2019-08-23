using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class FMS_GLAccountMasterType: Entity
    {
        public int MasterTypeID { get; set; }
        public string MasterTypeName { get; set; }
        public string Description { get; set; }
        public string DrCr { get; set; }
    }
}
