using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Entities.ViewModels.Institutes
{
    public class VmAcademicClass
    {
        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<int> TagId { get; set; }
        public Nullable<int> Ordering { get; set; }
        public string Code { get; set; }
        public Nullable<decimal> MinAgeLimit { get; set; }
        public Nullable<decimal> MaxAgeLimit { get; set; }
        public Nullable<bool> IsSubjectGroupWiseRoutine { get; set; }
        public Nullable<int> RoutinePeriodTypeId { get; set; }
        public Nullable<int> ExamGradeTypeId { get; set; }
        public string FullName { get; set; }
        public Nullable<int> ExamActivityGradeTypeId { get; set; }
        public string ShortName { get; set; }
        public Nullable<bool> IsEnableAcademicGroup { get; set; }
        public Nullable<bool> IsEnableSubjectGroup { get; set; }
    }
}
