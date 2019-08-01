using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class Diary: Entity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public System.DateTime DairyDate { get; set; }
        public int RoutinePeriodId { get; set; }
        public int SubjectId { get; set; }
        public Nullable<int> TeacherId { get; set; }
        public Nullable<int> BuildingRoomId { get; set; }
        public string Subject { get; set; }
        public string Details { get; set; }
        public string Homework { get; set; }
        public virtual BuildingRoom BuildingRoom { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual RoutinePeriod RoutinePeriod { get; set; }
        public virtual Subject Subject1 { get; set; }
    }
}
