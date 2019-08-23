using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    [MetadataType(typeof(VoucherDetailMetadata))]
    public partial class VoucherDetail : Entity
    {
        public string HeadName; 
    }
}
