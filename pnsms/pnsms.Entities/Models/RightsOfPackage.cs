using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class RightsOfPackage: Entity
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        public int RightId { get; set; }
        public virtual Package Package { get; set; }
        public virtual Right Right { get; set; }
    }
}
