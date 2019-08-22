using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
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
