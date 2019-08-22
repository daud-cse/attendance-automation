using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class Governingbody: Entity
    {
        public int GoverningbodyId { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}
