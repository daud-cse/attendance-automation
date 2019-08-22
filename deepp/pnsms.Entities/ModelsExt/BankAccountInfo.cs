using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.Models
{
  //  [MetadataType(typeof(BankAccountInfoMetaData))]
    public partial class BankAccountInfo : Entity
    {
        public List<KeyValuePair<int, string>> kvpBankAccountType { get; set; }
    }
}
