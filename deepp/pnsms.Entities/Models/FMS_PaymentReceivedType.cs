using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class FMS_PaymentReceivedType: Entity
    {
        public int PRTypeId { get; set; }
        public string PRTypeName { get; set; }
    }
}
