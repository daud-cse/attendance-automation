using deepp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Entities.ViewModels
{
    public class VmFeesGenerate
    {
        public FeesGenerate FeesGenerate { get; set; }
        public FeesGenerateAcademic FeesGenerateAcademic { get; set; }
        public IEnumerable<FeesGenerateHead> FeesGenerateHeadList { get; set; }
        public IEnumerable<VmAutoComplete> SearchStudents { get; set; }
        public IEnumerable<AdjStudentList> AdjStudentList { get; set; }
        public List<KeyValuePair<int, string>> FeesHeadList { get; set; }
    }

    public class AdjStudentList {
        public int FeesGenerateStudentId { get; set; }
        public int FeesHeadId { get; set; }
        public decimal Amount { get; set; }    
    }

}
