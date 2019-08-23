using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class ExamType: Entity
    {
        public ExamType()
        {
            this.Exams = new List<Exam>();
            this.ExamProcesses = new List<ExamProcess>();
            this.ExamTypeWiseTabulationSheetDetails = new List<ExamTypeWiseTabulationSheetDetail>();
            this.ExamTypeWiseTabulationSheetMasters = new List<ExamTypeWiseTabulationSheetMaster>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
        public virtual ICollection<ExamProcess> ExamProcesses { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<ExamTypeWiseTabulationSheetDetail> ExamTypeWiseTabulationSheetDetails { get; set; }
        public virtual ICollection<ExamTypeWiseTabulationSheetMaster> ExamTypeWiseTabulationSheetMasters { get; set; }
    }
}
