using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class Notice: Entity
    {
        public int Id { get; set; }
        public int InstituteId { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string NoticeTitle { get; set; }
        public string NoticeBody { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<int> NoticeTypeId { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual NoticeType NoticeType { get; set; }
    }
}
