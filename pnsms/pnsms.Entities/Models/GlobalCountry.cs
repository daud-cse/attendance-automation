using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class GlobalCountry: Entity
    {
        public GlobalCountry()
        {
            this.Countries = new List<Country>();
            this.GlobalDivisions = new List<GlobalDivision>();
            this.GlobalUsers = new List<GlobalUser>();
            this.Institutes = new List<Institute>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Country> Countries { get; set; }
        public virtual ICollection<GlobalDivision> GlobalDivisions { get; set; }
        public virtual ICollection<GlobalUser> GlobalUsers { get; set; }
        public virtual ICollection<Institute> Institutes { get; set; }
    }
}
