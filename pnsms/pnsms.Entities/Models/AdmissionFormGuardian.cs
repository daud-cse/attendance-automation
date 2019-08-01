using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class AdmissionFormGuardian: Entity
    {
        public int Id { get; set; }
        public int AdmissionFormId { get; set; }
        public int GuardianTypeId { get; set; }
        public Nullable<int> EducationalQualificationId { get; set; }
        public Nullable<int> ProfessionId { get; set; }
        public Nullable<int> MonthlyIncome { get; set; }
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
        public virtual AdmissionForm AdmissionForm { get; set; }
        public virtual EducationalQualification EducationalQualification { get; set; }
        public virtual GuardianType GuardianType { get; set; }
        public virtual Profession Profession { get; set; }
    }
}
