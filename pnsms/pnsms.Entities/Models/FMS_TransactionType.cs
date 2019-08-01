using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class FMS_TransactionType: Entity
    {
        public int TransactionTypeId { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public int InstituteId { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public System.DateTime CreatedDate { get; set; }
    }
}
