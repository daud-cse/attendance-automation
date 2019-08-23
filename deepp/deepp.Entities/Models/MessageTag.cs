using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class MessageTag: Entity
    {
        public int Id { get; set; }
        public int MessageTagGroupId { get; set; }
        public string TagName { get; set; }
        public string TagDescription { get; set; }
        public int CharCount { get; set; }
        public virtual MessageTagGroup MessageTagGroup { get; set; }
    }
}
