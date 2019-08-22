using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class RightsOfRole: Entity
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int RightId { get; set; }
        public virtual Right Right { get; set; }
        public virtual Role Role { get; set; }
    }
}
