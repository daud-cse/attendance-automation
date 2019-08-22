using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class AdmissionForm: Entity
    {
        public AdmissionForm()
        {
            this.AdmissionFormAddresses = new List<AdmissionFormAddress>();
            this.AdmissionFormGuardians = new List<AdmissionFormGuardian>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public byte[] ImageBinaryData { get; set; }
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public string SSN { get; set; }
        public string PassportNo { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public Nullable<int> GenderId { get; set; }
        public Nullable<int> NationalityId { get; set; }
        public Nullable<int> ReligionId { get; set; }
        public Nullable<int> BloodGroupId { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<int> AcademicSessionId { get; set; }
        public Nullable<int> AcademicClassId { get; set; }
        public Nullable<int> AcademicBranchId { get; set; }
        public bool IsActive { get; set; }
        public bool IsSelected { get; set; }
        public virtual AcademicBranch AcademicBranch { get; set; }
        public virtual AcademicClass AcademicClass { get; set; }
        public virtual AcademicSession AcademicSession { get; set; }
        public virtual ICollection<AdmissionFormAddress> AdmissionFormAddresses { get; set; }
        public virtual ICollection<AdmissionFormGuardian> AdmissionFormGuardians { get; set; }
        public virtual BloodGroup BloodGroup { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual Nationality Nationality { get; set; }
        public virtual Religion Religion { get; set; }
    }
}
