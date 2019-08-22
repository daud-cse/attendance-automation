using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class RoutineDetail: Entity
    {
        public int Id { get; set; }
        public int RoutineId { get; set; }
        public int WeekDayId { get; set; }
        public int RoutinePeriodId { get; set; }
        public Nullable<int> SubjectId { get; set; }
        public Nullable<int> TeacherId { get; set; }
        public Nullable<int> BuildingRoomId { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<int> SubjectSplitId { get; set; }
        public Nullable<int> SubstituteTeacherId { get; set; }
        public Nullable<int> RoutineSubjectGroupId { get; set; }
        public Nullable<int> SubjectGroupId { get; set; }
        public Nullable<int> TeacherDepartmentId { get; set; }
        public virtual BuildingRoom BuildingRoom { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Employee Employee1 { get; set; }
        public virtual RoutinePeriod RoutinePeriod { get; set; }
        public virtual Routine Routine { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual WeekDay WeekDay { get; set; }
    }
}
