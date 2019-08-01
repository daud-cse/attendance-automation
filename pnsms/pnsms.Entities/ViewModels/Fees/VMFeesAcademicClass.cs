using pnsms.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.ViewModels.Fees
{
    public class VMFeesAcademicClass
    {
        public int FeesAcademicClassId { get; set; }
        public int AcademicClassId { get; set; }
        public string AcademicClassName { get; set; }
        public bool IsAlreadyAdded { get; set; }
        public bool IsDirty { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int FeesHeadsId { get; set; }
        public string FeesHeadName { get; set; }
        public int FeesTypeId { get; set; }
        public string FeesTypeName { get; set; }
        public int InstituteId { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public List<KeyValuePair<int,string>> kvpFeesTypes { get; set; }

    }
}
