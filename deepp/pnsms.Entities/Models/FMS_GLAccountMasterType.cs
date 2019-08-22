using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class FMS_GLAccountMasterType: Entity
    {
        public int MasterTypeID { get; set; }
        public string MasterTypeName { get; set; }
        public string Description { get; set; }
        public string DrCr { get; set; }
    }
}
