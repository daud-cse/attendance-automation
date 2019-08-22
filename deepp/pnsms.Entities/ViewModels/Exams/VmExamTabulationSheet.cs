using pnsms.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.ViewModels.Exams
{
   public class VmExamTabulationSheet
    {
       public ExamTypeWiseTabulationSheetMaster objExamTypeWiseTabulationSheetMaster { get; set; }
        public List<ExamTypeWiseTabulationSheetMaster> objlstExamTypeWiseTabulationSheetMaster { get; set; }

        public List<ExamTypeWiseTabulationSheetDetail> lstExamTypeWiseTabulationSheetDetails { get; set; }
    }
}
