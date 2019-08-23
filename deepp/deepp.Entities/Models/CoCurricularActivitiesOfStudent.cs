using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class CoCurricularActivitiesOfStudent: Entity
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CoCurricularActivityId { get; set; }
        public virtual CoCurricularActivity CoCurricularActivity { get; set; }
        public virtual Student Student { get; set; }
    }
}
