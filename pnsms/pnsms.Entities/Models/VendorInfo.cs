using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class VendorInfo: Entity
    {
        public int ID { get; set; }
        public string VendorAutoId { get; set; }
        public string Name { get; set; }
        public string VendorAddress { get; set; }
        public int VendorTypeId { get; set; }
        public bool IsActive { get; set; }
        public Nullable<decimal> OpeningBalance { get; set; }
        public Nullable<decimal> DrAmount { get; set; }
        public Nullable<decimal> CrAmount { get; set; }
        public Nullable<decimal> CurrentBalance { get; set; }
        public System.DateTime SetDate { get; set; }
        public string SetBy { get; set; }
        public int InstituteId { get; set; }
        public virtual VendorType VendorType { get; set; }
    }
}
