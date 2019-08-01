using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class FMS_AccountingReportType: Entity
    {
        public FMS_AccountingReportType()
        {
            this.FMS_GLAccount = new List<FMS_GLAccount>();
        }

        public int ReportTypeID { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<FMS_GLAccount> FMS_GLAccount { get; set; }
    }
}
