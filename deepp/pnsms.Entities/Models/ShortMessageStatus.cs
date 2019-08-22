using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
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
