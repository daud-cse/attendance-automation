using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class CertificatePrint: Entity
    {
        public int Id { get; set; }
        public int CertificatePrintTypeId { get; set; }
        public int InstituteId { get; set; }
        public string Body { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public virtual CertificatePrintType CertificatePrintType { get; set; }
        public virtual Institute Institute { get; set; }
    }
}