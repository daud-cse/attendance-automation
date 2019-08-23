using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class Governingbody: Entity
    {
        public int GoverningbodyId { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}
