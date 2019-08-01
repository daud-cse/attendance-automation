using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class FeesGenerate: Entity
    {
        public FeesGenerate()
        {
            this.FeesCollectionDetails = new List<FeesCollectionDetail>();
            this.FeesGenerateAcademics = new List<FeesGenerateAcademic>();
            this.FeesGenerateHeads = new List<FeesGenerateHead>();
            this.FeesGenerateStudentDetails = new List<FeesGenerateStudentDetail>();
            this.FeesGenerateStudents = new List<FeesGenerateStudent>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public int ForTheMonth { get; set; }
        public int ForTheYear { get; set; }
        public System.DateTime ForTheDate { get; set; }
        public System.DateTime GenerationDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public bool IsPublished { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual ICollection<FeesCollectionDetail> FeesCollectionDetails { get; set; }
        public virtual ICollection<FeesGenerateAcademic> FeesGenerateAcademics { get; set; }
        public virtual ICollection<FeesGenerateHead> FeesGenerateHeads { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<FeesGenerateStudentDetail> FeesGenerateStudentDetails { get; set; }
        public virtual ICollection<FeesGenerateStudent> FeesGenerateStudents { get; set; }
    }
}
