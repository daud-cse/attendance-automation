using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class FMS_PaymentReceivedType: Entity
    {
        public int PRTypeId { get; set; }
        public string PRTypeName { get; set; }
    }
}
