using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class ShortMessageTemplate: Entity
    {
        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string SmsTemplate { get; set; }
        public bool IsStaticSms { get; set; }
        public bool IsActive { get; set; }
        public Nullable<bool> IsForGeneral { get; set; }
        public Nullable<bool> IsForStudent { get; set; }
        public Nullable<bool> IsForGuardian { get; set; }
        public Nullable<bool> IsForTeacher { get; set; }
        public Nullable<bool> IsForEmployee { get; set; }
        public Nullable<bool> IsForGoverningBody { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public int SmsCount { get; set; }
        public string Name { get; set; }
        public virtual Institute Institute { get; set; }
    }
}
