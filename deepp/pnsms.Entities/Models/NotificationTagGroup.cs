using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class NotificationTagGroup: Entity
    {
        public NotificationTagGroup()
        {
            this.NotificationTags = new List<NotificationTag>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<NotificationTag> NotificationTags { get; set; }
    }
}
