using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.ViewModels.Subjects
{
    public class VmSubject
    {
        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string Name { get; set; }
        public Nullable<int> ParentSubjectId { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> TagId { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public string Code { get; set; }
        public int TeacherId { get; set; }
        public List<int> BuildingRoomIds { get; set; }


        public bool? IsSubjectGroupWise { get; set; }

        public bool? IsEnableTeacherOverlapAtRoutine { get; set; }
        public DateTime? ExamDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public string ShortName { get; set; }
        public int ExamSubjectId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsAssesment { get; set; }

    }
}
