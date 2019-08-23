using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class DocumentType: Entity
    {
        public DocumentType()
        {
            this.Documents = new List<Document>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}