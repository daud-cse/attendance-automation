using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.ViewModels.Fees
{
    public class VMFeesClassMonthlyReport
    {
        public string StudentName { get; set; }
        public string Month { get; set; }
        public string Class { get; set; }
        public string Institute { get; set; }
        public decimal MonthlyAnount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal RemainingAmount { get; set; }
        public List<VMFeesCollection> ReceiveAmounts { get; set; }
    }
    public class VMFeesCollection
    {
        public int FeesHeadId { get; set; }
        public string FeesHeadName { get; set; }
        public decimal Amount { get; set; }
    }
}