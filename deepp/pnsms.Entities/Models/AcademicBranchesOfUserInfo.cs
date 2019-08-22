using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class AcademicBranchesOfUserInfo: Entity
    {
        public int Id { get; set; }
        public int UserInfoId { get; set; }
        public int AcademicBranchId { get; set; }
        public virtual AcademicBranch AcademicBranch { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}
