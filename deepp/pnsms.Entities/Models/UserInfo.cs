using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class UserInfo: Entity
    {
        public UserInfo()
        {
            this.AcademicBranchesOfUserInfoes = new List<AcademicBranchesOfUserInfo>();
            this.Contents = new List<Content>();
            this.LeaveApplications = new List<LeaveApplication>();
            this.RolesOfUserInfoes = new List<RolesOfUserInfo>();
            this.ShortMessageDetails = new List<ShortMessageDetail>();
            this.UserAttendanceDetails = new List<UserAttendanceDetail>();
        }

        public int Id { get; set; }
        public int UserInfoTypeId { get; set; }
        public Nullable<int> InstituteId { get; set; }
        public string PIN { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string ContactNumber1 { get; set; }
        public string ContactNumber2 { get; set; }
        public string EmailAddress { get; set; }
        public string SSN { get; set; }
        public string PassportNo { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public Nullable<int> GenderId { get; set; }
        public Nullable<int> NationalityId { get; set; }
        public Nullable<int> ReligionId { get; set; }
        public Nullable<int> BloodGroupId { get; set; }
        public string GoogleId { get; set; }
        public string FacebookId { get; set; }
        public string MicrosoftId { get; set; }
        public string TwitterId { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public string AboutUser { get; set; }
        public virtual ICollection<AcademicBranchesOfUserInfo> AcademicBranchesOfUserInfoes { get; set; }
        public virtual BloodGroup BloodGroup { get; set; }
        public virtual ICollection<Content> Contents { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual GlobalUser GlobalUser { get; set; }
        public virtual Governingbody Governingbody { get; set; }
        public virtual Guardian Guardian { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<LeaveApplication> LeaveApplications { get; set; }
        public virtual Nationality Nationality { get; set; }
        public virtual Religion Religion { get; set; }
        public virtual ICollection<RolesOfUserInfo> RolesOfUserInfoes { get; set; }
        public virtual ICollection<ShortMessageDetail> ShortMessageDetails { get; set; }
        public virtual Student Student { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<UserAttendanceDetail> UserAttendanceDetails { get; set; }
        public virtual UserInfoType UserInfoType { get; set; }
        public virtual UserInfoSecurity UserInfoSecurity { get; set; }
    }
}
