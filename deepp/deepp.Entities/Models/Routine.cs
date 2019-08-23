using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class Routine: Entity
    {
        public Routine()
        {
            this.RoutineDetails = new List<RoutineDetail>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public int AcademicBranchId { get; set; }
        public Nullable<int> AcademicClassId { get; set; }
        public Nullable<int> AcademicShiftId { get; set; }
        public Nullable<int> AcademicSectionId { get; set; }
        public Nullable<int> AcamedicGroupId { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<int> RoutineNoteId { get; set; }
        public Nullable<int> AcademicSessionId { get; set; }
        public Nullable<int> RoutineTypeId { get; set; }
        public Nullable<int> BuildingRoomId { get; set; }
        public Nullable<int> RoutineOtherDutyTypeId { get; set; }
        public virtual AcademicBranch AcademicBranch { get; set; }
        public virtual AcademicClass AcademicClass { get; set; }
        public virtual AcademicGroup AcademicGroup { get; set; }
        public virtual AcademicSection AcademicSection { get; set; }
        public virtual AcademicSession AcademicSession { get; set; }
        public virtual AcademicShift AcademicShift { get; set; }
        public virtual BuildingRoom BuildingRoom { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<RoutineDetail> RoutineDetails { get; set; }
        public virtual RoutineNote RoutineNote { get; set; }
    }
}
