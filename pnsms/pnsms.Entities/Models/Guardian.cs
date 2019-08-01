using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class Guardian: Entity
    {
        public Guardian()
        {
            this.GuardiansOfStudents = new List<GuardiansOfStudent>();
        }

        public int GuardianId { get; set; }
        public int GuardianTypeId { get; set; }
        public Nullable<int> EducationalQualificationId { get; set; }
        public Nullable<int> ProfessionId { get; set; }
        public Nullable<int> MonthlyIncome { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public virtual EducationalQualification EducationalQualification { get; set; }
        public virtual GuardianType GuardianType { get; set; }
        public virtual Profession Profession { get; set; }
        public virtual UserInfo UserInfo { get; set; }
        public virtual ICollection<GuardiansOfStudent> GuardiansOfStudents { get; set; }
    }
}
