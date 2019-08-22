using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class GlobalScholarship: Entity
    {
        public GlobalScholarship()
        {
            this.Scholarships = new List<Scholarship>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Scholarship> Scholarships { get; set; }
    }
}
