using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class MessageTagGroup: Entity
    {
        public MessageTagGroup()
        {
            this.MessageTags = new List<MessageTag>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsForStudent { get; set; }
        public bool IsForTeacher { get; set; }
        public bool IsForEmployee { get; set; }
        public virtual ICollection<MessageTag> MessageTags { get; set; }
    }
}
