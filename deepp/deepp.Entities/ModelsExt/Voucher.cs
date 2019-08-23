using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    [MetadataType(typeof(VoucherMetadata))]
    public partial class Voucher : Entity
    {
        public List<KeyValuePair<int, string>> AcademicBranchList { get; set; }
        public String BranchName;
    }
}
