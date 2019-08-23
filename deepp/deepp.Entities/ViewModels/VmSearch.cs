using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Entities.ViewModels
{
    public class VmSearch<T> where T:class
    {
        public System.DateTime startDateModel { get; set; }
        public System.DateTime endDateModel { get; set; }
        public int InstituteId { get; set; }
        public string StudentPin { get; set; }

        public string TextFieldId1 { get; set; }
        public string TextFieldId2 { get; set; }

        public List<KeyValuePair<int, string>> DropDownList1 { get; set; }
        public int DropDownId1 { get; set; }
        public List<KeyValuePair<int, string>> DropDownList2 { get; set; }
        public int DropDownId2 { get; set; }
        public List<KeyValuePair<int, string>> DropDownList3 { get; set; }
        public int DropDownId3 { get; set; }

        public bool selectedStatus { get; set; }

        public IEnumerable<T> SearchData { get; set; }
        
    }
}
