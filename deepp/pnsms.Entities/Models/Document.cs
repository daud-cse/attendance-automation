using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class Document: Entity
    {
        public int Id { get; set; }
        public int DocumentTypeId { get; set; }
        public int RefPrimaryKey { get; set; }
        public string Caption { get; set; }
        public string Url { get; set; }
        public string FileExt { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public string DocumentType { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdatedTime { get; set; }
        public virtual DocumentType DocumentType1 { get; set; }
    }
}
