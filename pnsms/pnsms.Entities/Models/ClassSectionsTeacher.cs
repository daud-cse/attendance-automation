using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class ClassSectionsTeacher: Entity
    {
        public int Id { get; set; }
        public Nullable<int> AcademicSessionId { get; set; }
        public Nullable<int> AcademicClassesId { get; set; }
        public Nullable<int> AcademicGroupId { get; set; }
        public Nullable<int> TeacherId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
    }
}
