using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class FMS_SubsidyType: Entity
    {
        public FMS_SubsidyType()
        {
            this.FMS_GLSubsidyTypeMap = new List<FMS_GLSubsidyTypeMap>();
        }

        public int SubsidyTypeId { get; set; }
        public string SubsidyTypeName { get; set; }
        public string TableName { get; set; }
        public string DRCR { get; set; }
        public bool IsActive { get; set; }
        public string SetBy { get; set; }
        public System.DateTime SetDate { get; set; }
        public int InstituteId { get; set; }
        public virtual ICollection<FMS_GLSubsidyTypeMap> FMS_GLSubsidyTypeMap { get; set; }
    }
}
