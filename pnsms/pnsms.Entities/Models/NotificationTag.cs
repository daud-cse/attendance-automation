using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class NotificationTag: Entity
    {
        public int Id { get; set; }
        public int NotificationGroupId { get; set; }
        public string Tag { get; set; }
        public int MaxCharLength { get; set; }
        public Nullable<bool> IsForStudent { get; set; }
        public Nullable<bool> IsForGuardian { get; set; }
        public Nullable<bool> IsForTeacher { get; set; }
        public Nullable<bool> IsForEmployee { get; set; }
        public Nullable<bool> IsForGoverningBody { get; set; }
        public string PreviewText { get; set; }
        public string TextToCalculate { get; set; }
        public Nullable<bool> IsShowFromDate { get; set; }
        public Nullable<bool> IsShowToDate { get; set; }
        public virtual NotificationTagGroup NotificationTagGroup { get; set; }
    }
}
