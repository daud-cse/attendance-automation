using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    [MetadataType(typeof(CertificatePrintMetadata))]
    public partial class CertificatePrint : Entity
    {
        public List<KeyValuePair<int, string>> CertificatePrintTypeList { get; set; }
    }
}
