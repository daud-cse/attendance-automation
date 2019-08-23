using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class GlobalUser: Entity
    {
        public int GlobalUserId { get; set; }
        public Nullable<int> UserInfoTypeId { get; set; }
        public int GlobalUserTypeId { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public Nullable<int> MaritalStatusId { get; set; }
        public Nullable<int> DesignationId { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<int> GlobalCountryId { get; set; }
        public Nullable<int> GlobalDivisionId { get; set; }
        public Nullable<int> GlobalDistrictId { get; set; }
        public Nullable<int> GlobalSubDistrictId { get; set; }
        public bool IsActive { get; set; }
        public virtual Department Department { get; set; }
        public virtual Designation Designation { get; set; }
        public virtual GlobalCountry GlobalCountry { get; set; }
        public virtual GlobalDistrict GlobalDistrict { get; set; }
        public virtual GlobalDivision GlobalDivision { get; set; }
        public virtual GlobalSubDistrict GlobalSubDistrict { get; set; }
        public virtual GlobalUserType GlobalUserType { get; set; }
        public virtual MaritalStatus MaritalStatus { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}
