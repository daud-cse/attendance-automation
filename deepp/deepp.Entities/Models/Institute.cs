using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class Institute: Entity
    {
        public Institute()
        {
            this.AcademicBranches = new List<AcademicBranch>();
            this.AcademicCalendars = new List<AcademicCalendar>();
            this.AcademicClasses = new List<AcademicClass>();
            this.AcademicClassSectionMappings = new List<AcademicClassSectionMapping>();
            this.AcademicGroups = new List<AcademicGroup>();
            this.AcademicPeriods = new List<AcademicPeriod>();
            this.AcademicSections = new List<AcademicSection>();
            this.AcademicSessions = new List<AcademicSession>();
            this.AcademicShifts = new List<AcademicShift>();
            this.AcademicVersions = new List<AcademicVersion>();
            this.AddressTypes = new List<AddressType>();
            this.AdmissionForms = new List<AdmissionForm>();
            this.AttendanceTypes = new List<AttendanceType>();
            this.BloodGroups = new List<BloodGroup>();
            this.Buildings = new List<Building>();
            this.CertificatePrints = new List<CertificatePrint>();
            this.CertificatePrintTypes = new List<CertificatePrintType>();
            this.ChartOfAccounts = new List<ChartOfAccount>();
            this.CoCurricularActivities = new List<CoCurricularActivity>();
            this.ContactUs = new List<ContactU>();
            this.Contents = new List<Content>();
            this.Countries = new List<Country>();
            this.Departments = new List<Department>();
            this.Designations = new List<Designation>();
            this.DynamicPages = new List<DynamicPage>();
            this.EducationalQualifications = new List<EducationalQualification>();
            this.Events = new List<Event>();
            this.Exams = new List<Exam>();
            this.ExamProcesses = new List<ExamProcess>();
            this.ExamSubjectMarks = new List<ExamSubjectMark>();
            this.ExamTypes = new List<ExamType>();
            this.FeesAcademicClasses = new List<FeesAcademicClass>();
            this.FeesAutoGenerateConfigs = new List<FeesAutoGenerateConfig>();
            this.FeesAutoGenerateConfigTypes = new List<FeesAutoGenerateConfigType>();
            this.FeesGenerates = new List<FeesGenerate>();
            this.FeesHeads = new List<FeesHead>();
            this.FeesTypes = new List<FeesType>();
            this.Galleries = new List<Gallery>();
            this.Genders = new List<Gender>();
            this.GuardianTypes = new List<GuardianType>();
            this.InstituteSubjects = new List<InstituteSubject>();
            this.LeaveApplications = new List<LeaveApplication>();
            this.LibraryBookAuthores = new List<LibraryBookAuthore>();
            this.LibraryBooks = new List<LibraryBook>();
            this.MaritalStatuses = new List<MaritalStatus>();
            this.MobilePayments = new List<MobilePayment>();
            this.Nationalities = new List<Nationality>();
            this.Notices = new List<Notice>();
            this.Professions = new List<Profession>();
            this.Religions = new List<Religion>();
            this.ResultPublications = new List<ResultPublication>();
            this.Roles = new List<Role>();
            this.RoutineNotes = new List<RoutineNote>();
            this.RoutinePeriods = new List<RoutinePeriod>();
            this.RoutinePeriodTypes = new List<RoutinePeriodType>();
            this.Routines = new List<Routine>();
            this.Scholarships = new List<Scholarship>();
            this.ShortMessages = new List<ShortMessage>();
            this.ShortMessageTemplates = new List<ShortMessageTemplate>();
            this.SubjectAcademicClassMappings = new List<SubjectAcademicClassMapping>();
            this.SubjectAcademicClassMappingSubjectTypes = new List<SubjectAcademicClassMappingSubjectType>();
            this.SubjectGroups = new List<SubjectGroup>();
            this.SubjectStudentMappings = new List<SubjectStudentMapping>();
            this.SubjectTypes = new List<SubjectType>();
            this.Tags = new List<Tag>();
            this.TeacherSubjectAcademicMappings = new List<TeacherSubjectAcademicMapping>();
            this.Testimonials = new List<Testimonial>();
            this.UserInfoes = new List<UserInfo>();
            this.UserInfoSecurities = new List<UserInfoSecurity>();
            this.VisitorCounts = new List<VisitorCount>();
            this.Vouchers = new List<Voucher>();
            this.WeekDays = new List<WeekDay>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int PackageId { get; set; }
        public string Code { get; set; }
        public string Url { get; set; }
        public Nullable<decimal> latitude { get; set; }
        public Nullable<decimal> longitude { get; set; }
        public string SeoText { get; set; }
        public string WelComeText { get; set; }
        public string ContactText { get; set; }
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string GoogleUrl { get; set; }
        public string LinkedinUrl { get; set; }
        public string BehanceUrl { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public string HistoryText { get; set; }
        public string InfractructureText { get; set; }
        public string MasterPlanText { get; set; }
        public string UsefulLinkText { get; set; }
        public Nullable<int> GlobalCountryId { get; set; }
        public Nullable<int> GlobalDivisionId { get; set; }
        public Nullable<int> GlobalDistrictId { get; set; }
        public Nullable<int> GlobalSubDistrictId { get; set; }
        public Nullable<int> VisitorToday { get; set; }
        public Nullable<int> VisitorThisMonth { get; set; }
        public Nullable<int> VisitorTotal { get; set; }
        public Nullable<int> GlobalInstituteTypeId { get; set; }
        public string Asset { get; set; }
        public string IncExp { get; set; }
        public string Sanitation { get; set; }
        public string Multimedia { get; set; }
        public string LibraryInfo { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public Nullable<bool> IsSMSCostPayByReceipient { get; set; }
        public Nullable<bool> IsAuthenticationSMSCostPayByReceipient { get; set; }
        public Nullable<int> RestSMSCount { get; set; }
        public string MenuHtml { get; set; }
        public string CssOverwrite { get; set; }
        public string GoogleMapAddress { get; set; }
        public virtual ICollection<AcademicBranch> AcademicBranches { get; set; }
        public virtual ICollection<AcademicCalendar> AcademicCalendars { get; set; }
        public virtual ICollection<AcademicClass> AcademicClasses { get; set; }
        public virtual ICollection<AcademicClassSectionMapping> AcademicClassSectionMappings { get; set; }
        public virtual ICollection<AcademicGroup> AcademicGroups { get; set; }
        public virtual ICollection<AcademicPeriod> AcademicPeriods { get; set; }
        public virtual ICollection<AcademicSection> AcademicSections { get; set; }
        public virtual ICollection<AcademicSession> AcademicSessions { get; set; }
        public virtual ICollection<AcademicShift> AcademicShifts { get; set; }
        public virtual ICollection<AcademicVersion> AcademicVersions { get; set; }
        public virtual ICollection<AddressType> AddressTypes { get; set; }
        public virtual ICollection<AdmissionForm> AdmissionForms { get; set; }
        public virtual ICollection<AttendanceType> AttendanceTypes { get; set; }
        public virtual ICollection<BloodGroup> BloodGroups { get; set; }
        public virtual ICollection<Building> Buildings { get; set; }
        public virtual ICollection<CertificatePrint> CertificatePrints { get; set; }
        public virtual ICollection<CertificatePrintType> CertificatePrintTypes { get; set; }
        public virtual ICollection<ChartOfAccount> ChartOfAccounts { get; set; }
        public virtual ICollection<CoCurricularActivity> CoCurricularActivities { get; set; }
        public virtual ICollection<ContactU> ContactUs { get; set; }
        public virtual ICollection<Content> Contents { get; set; }
        public virtual ICollection<Country> Countries { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Designation> Designations { get; set; }
        public virtual ICollection<DynamicPage> DynamicPages { get; set; }
        public virtual ICollection<EducationalQualification> EducationalQualifications { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
        public virtual ICollection<ExamProcess> ExamProcesses { get; set; }
        public virtual ICollection<ExamSubjectMark> ExamSubjectMarks { get; set; }
        public virtual ICollection<ExamType> ExamTypes { get; set; }
        public virtual ICollection<FeesAcademicClass> FeesAcademicClasses { get; set; }
        public virtual ICollection<FeesAutoGenerateConfig> FeesAutoGenerateConfigs { get; set; }
        public virtual ICollection<FeesAutoGenerateConfigType> FeesAutoGenerateConfigTypes { get; set; }
        public virtual ICollection<FeesGenerate> FeesGenerates { get; set; }
        public virtual ICollection<FeesHead> FeesHeads { get; set; }
        public virtual ICollection<FeesType> FeesTypes { get; set; }
        public virtual ICollection<Gallery> Galleries { get; set; }
        public virtual ICollection<Gender> Genders { get; set; }
        public virtual GlobalCountry GlobalCountry { get; set; }
        public virtual GlobalDistrict GlobalDistrict { get; set; }
        public virtual GlobalDivision GlobalDivision { get; set; }
        public virtual GlobalInstituteType GlobalInstituteType { get; set; }
        public virtual GlobalSubDistrict GlobalSubDistrict { get; set; }
        public virtual ICollection<GuardianType> GuardianTypes { get; set; }
        public virtual Package Package { get; set; }
        public virtual ICollection<InstituteSubject> InstituteSubjects { get; set; }
        public virtual ICollection<LeaveApplication> LeaveApplications { get; set; }
        public virtual ICollection<LibraryBookAuthore> LibraryBookAuthores { get; set; }
        public virtual ICollection<LibraryBook> LibraryBooks { get; set; }
        public virtual ICollection<MaritalStatus> MaritalStatuses { get; set; }
        public virtual ICollection<MobilePayment> MobilePayments { get; set; }
        public virtual ICollection<Nationality> Nationalities { get; set; }
        public virtual ICollection<Notice> Notices { get; set; }
        public virtual ICollection<Profession> Professions { get; set; }
        public virtual ICollection<Religion> Religions { get; set; }
        public virtual ICollection<ResultPublication> ResultPublications { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<RoutineNote> RoutineNotes { get; set; }
        public virtual ICollection<RoutinePeriod> RoutinePeriods { get; set; }
        public virtual ICollection<RoutinePeriodType> RoutinePeriodTypes { get; set; }
        public virtual ICollection<Routine> Routines { get; set; }
        public virtual ICollection<Scholarship> Scholarships { get; set; }
        public virtual ICollection<ShortMessage> ShortMessages { get; set; }
        public virtual ICollection<ShortMessageTemplate> ShortMessageTemplates { get; set; }
        public virtual ICollection<SubjectAcademicClassMapping> SubjectAcademicClassMappings { get; set; }
        public virtual ICollection<SubjectAcademicClassMappingSubjectType> SubjectAcademicClassMappingSubjectTypes { get; set; }
        public virtual ICollection<SubjectGroup> SubjectGroups { get; set; }
        public virtual ICollection<SubjectStudentMapping> SubjectStudentMappings { get; set; }
        public virtual ICollection<SubjectType> SubjectTypes { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<TeacherSubjectAcademicMapping> TeacherSubjectAcademicMappings { get; set; }
        public virtual ICollection<Testimonial> Testimonials { get; set; }
        public virtual ICollection<UserInfo> UserInfoes { get; set; }
        public virtual ICollection<UserInfoSecurity> UserInfoSecurities { get; set; }
        public virtual ICollection<VisitorCount> VisitorCounts { get; set; }
        public virtual ICollection<Voucher> Vouchers { get; set; }
        public virtual ICollection<WeekDay> WeekDays { get; set; }
    }
}
