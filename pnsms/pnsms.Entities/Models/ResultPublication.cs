using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class ResultPublication: Entity
    {
        public int Id { get; set; }
        public int InstituteId { get; set; }
        public int AcademicSessionId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public virtual AcademicSession AcademicSession { get; set; }
        public virtual Institute Institute { get; set; }
    }
}
