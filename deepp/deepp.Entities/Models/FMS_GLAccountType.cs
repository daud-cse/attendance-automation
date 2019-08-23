using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class FMS_GLAccountType: Entity
    {
        public int TypeID { get; set; }
        public Nullable<int> MasterTypeID { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
        public Nullable<int> AppearInReportTypeID { get; set; }
        public string DrCr { get; set; }
    }
}
