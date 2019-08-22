using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.Models
{
    public partial class FeesAutoGenerateConfigDetail : Entity
    {
        public string HeadName;
        public decimal Total;
        public decimal VAT;
    }
}
