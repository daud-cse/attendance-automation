using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class GlobalCoCurricularActivity: Entity
    {
        public GlobalCoCurricularActivity()
        {
            this.CoCurricularActivities = new List<CoCurricularActivity>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<CoCurricularActivity> CoCurricularActivities { get; set; }
    }
}
