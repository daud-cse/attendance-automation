using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class ShortMessageStatus: Entity
    {
        public ShortMessageStatus()
        {
            this.ShortMessageDetails = new List<ShortMessageDetail>();
        }

        public int Id { get; set; }
        public string Status { get; set; }
        public virtual ICollection<ShortMessageDetail> ShortMessageDetails { get; set; }
    }
}
