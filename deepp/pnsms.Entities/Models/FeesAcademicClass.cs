using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class FeesAcademicClass: Entity
    {
        public int FeesAcademicClassId { get; set; }
        public int AcademicClassId { get; set; }
        public int InstituteId { get; set; }
        public int FeesHeadsId { get; set; }
        public int FeesTypeId { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public virtual AcademicClass AcademicClass { get; set; }
        public virtual FeesHead FeesHead { get; set; }
        public virtual FeesType FeesType { get; set; }
        public virtual Institute Institute { get; set; }
    }
}
