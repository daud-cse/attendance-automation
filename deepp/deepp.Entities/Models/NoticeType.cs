using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class NoticeType: Entity
    {
        public NoticeType()
        {
            this.Notices = new List<Notice>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Notice> Notices { get; set; }
    }
}
