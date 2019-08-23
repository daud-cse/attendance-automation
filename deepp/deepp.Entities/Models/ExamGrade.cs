using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class ExamGrade: Entity
    {
        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string GradeName { get; set; }
        public decimal GradePoint { get; set; }
        public decimal MarksFrom { get; set; }
        public decimal MarksUpto { get; set; }
        public string Comment { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime LastUpdateDate { get; set; }
    }
}
