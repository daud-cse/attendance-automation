using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class FeesAutoGenerateConfig: Entity
    {
        public FeesAutoGenerateConfig()
        {
            this.FeesAutoGenerateConfigDetails = new List<FeesAutoGenerateConfigDetail>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAllAcademicBranch { get; set; }
        public bool IsAllAcademicVerssion { get; set; }
        public bool IsAllAcademicClass { get; set; }
        public bool IsAllAcademicShift { get; set; }
        public bool IsAllAcademicGroup { get; set; }
        public bool IsAllFacility { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<int> FeesAutoGenerateConfigTypeId { get; set; }
        public Nullable<int> FacilityId { get; set; }
        public virtual FeesAutoGenerateConfigType FeesAutoGenerateConfigType { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<FeesAutoGenerateConfigDetail> FeesAutoGenerateConfigDetails { get; set; }
    }
}
