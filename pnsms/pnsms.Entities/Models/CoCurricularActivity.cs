using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class CoCurricularActivity: Entity
    {
        public CoCurricularActivity()
        {
            this.CoCurricularActivitiesOfStudents = new List<CoCurricularActivitiesOfStudent>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<int> GlobalCoCurricularActivitiyId { get; set; }
        public virtual GlobalCoCurricularActivity GlobalCoCurricularActivity { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<CoCurricularActivitiesOfStudent> CoCurricularActivitiesOfStudents { get; set; }
    }
}
