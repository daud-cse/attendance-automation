using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.Models
{
    public class MobilePaymentMetadata
    {
        [Display(Name = "Mobile No")]
        public string MobileNo { get; set; }
        [Display(Name = "Transaction Id")]
        public string TransactionId { get; set; }
        [Display(Name = "Transaction Date")]
        public System.DateTime TransactionDate { get; set; }
    }
}
