using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class FeesHead: Entity
    {
        public FeesHead()
        {
            this.FeesAcademicClasses = new List<FeesAcademicClass>();
            this.FeesAutoGenerateConfigDetails = new List<FeesAutoGenerateConfigDetail>();
            this.FeesCollections = new List<FeesCollection>();
            this.FeesGenerateHeads = new List<FeesGenerateHead>();
            this.FeesGenerateStudentDetails = new List<FeesGenerateStudentDetail>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<int> ChartOfAccountId { get; set; }
        public virtual ICollection<FeesAcademicClass> FeesAcademicClasses { get; set; }
        public virtual ICollection<FeesAutoGenerateConfigDetail> FeesAutoGenerateConfigDetails { get; set; }
        public virtual ICollection<FeesCollection> FeesCollections { get; set; }
        public virtual ICollection<FeesGenerateHead> FeesGenerateHeads { get; set; }
        public virtual ICollection<FeesGenerateStudentDetail> FeesGenerateStudentDetails { get; set; }
        public virtual Institute Institute { get; set; }
    }
}
