using deepp.Entities.ModelsExt;
using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Entities.Models
{
    [MetadataType(typeof(MobilePaymentMetadata))]

    public partial class MobilePayment : Entity
    {
        public List<KeyValuePair<int, string>> PaymentTypeList { get; set; }
        public string studentPin;
        public string studentName;
    }
}
