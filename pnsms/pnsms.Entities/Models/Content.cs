using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class Content: Entity
    {
        public Content()
        {
            this.ContentDetails = new List<ContentDetail>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public int UserInfoId { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ContentDetail> ContentDetails { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}
