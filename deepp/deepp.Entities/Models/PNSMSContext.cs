using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using deepp.Entities.Models.Mapping;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class PNSMSContext : DataContext
    {
        static PNSMSContext()
        {
            Database.SetInitializer<PNSMSContext>(null);
        }

        public PNSMSContext()
            : base("Name=PNSMSContext")
        {
        }

        public DbSet<C_Migration> C_Migration { get; set; }
        public DbSet<AcademicBranch> AcademicBranches { get; set; }
        public DbSet<AcademicBranchesOfUserInfo> AcademicBranchesOfUserInfoes { get; set; }
        public DbSet<AcademicCalendar> AcademicCalendars { get; set; }
        public DbSet<AcademicClass> AcademicClasses { get; set; }
        public DbSet<AcademicClassSectionMapping> AcademicClassSectionMappings { get; set; }
        public DbSet<AcademicGroup> AcademicGroups { get; set; }
        public DbSet<AcademicPeriod> AcademicPeriods { get; set; }
        public DbSet<AcademicSection> AcademicSections { get; set; }
        public DbSet<AcademicSession> AcademicSessions { get; set; }
        public DbSet<AcademicShift> AcademicShifts { get; set; }
        public DbSet<AcademicVersion> AcademicVersions { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AddressType> AddressTypes { get; set; }
        public DbSet<AdmissionFormAddress> AdmissionFormAddresses { get; set; }
        public DbSet<AdmissionFormGuardian> AdmissionFormGuardians { get; set; }
        public DbSet<AdmissionForm> AdmissionForms { get; set; }
        public DbSet<AttendanceConfiguration> AttendanceConfigurations { get; set; }
        public DbSet<AttendanceType> AttendanceTypes { get; set; }
        public DbSet<BankAccountInfo> BankAccountInfoes { get; set; }
        public DbSet<BankAccountType> BankAccountTypes { get; set; }
        public DbSet<BloodGroup> BloodGroups { get; set; }
        public DbSet<BuildingRoom> BuildingRooms { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<CertificatePrint> CertificatePrints { get; set; }
        public DbSet<CertificatePrintType> CertificatePrintTypes { get; set; }
        public DbSet<ChartOfAccount> ChartOfAccounts { get; set; }
        public DbSet<CoCurricularActivity> CoCurricularActivities { get; set; }
        public DbSet<CoCurricularActivitiesOfStudent> CoCurricularActivitiesOfStudents { get; set; }
        public DbSet<Colour> Colours { get; set; }
        public DbSet<ContactU> ContactUs { get; set; }
        public DbSet<ContentDetail> ContentDetails { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Diary> Diaries { get; set; }
        public DbSet<DistrictOrState> DistrictOrStates { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<DynamicPageBody> DynamicPageBodies { get; set; }
        public DbSet<DynamicPage> DynamicPages { get; set; }
        public DbSet<EducationalQualification> EducationalQualifications { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamGrade> ExamGrades { get; set; }
        public DbSet<ExamProcess> ExamProcesses { get; set; }
        public DbSet<ExamSubject> ExamSubjects { get; set; }
        public DbSet<ExamSubjectMark> ExamSubjectMarks { get; set; }
        public DbSet<ExamType> ExamTypes { get; set; }
        public DbSet<ExamTypeWiseTabulationSheetDetail> ExamTypeWiseTabulationSheetDetails { get; set; }
        public DbSet<ExamTypeWiseTabulationSheetMaster> ExamTypeWiseTabulationSheetMasters { get; set; }
        public DbSet<FeesAcademicClass> FeesAcademicClasses { get; set; }
        public DbSet<FeesAutoGenerateConfig> FeesAutoGenerateConfigs { get; set; }
        public DbSet<FeesAutoGenerateConfigDetail> FeesAutoGenerateConfigDetails { get; set; }
        public DbSet<FeesAutoGenerateConfigType> FeesAutoGenerateConfigTypes { get; set; }
        public DbSet<FeesBooth> FeesBooths { get; set; }
        public DbSet<FeesCollectionAdvance> FeesCollectionAdvances { get; set; }
        public DbSet<FeesCollectionDetail> FeesCollectionDetails { get; set; }
        public DbSet<FeesCollection> FeesCollections { get; set; }
        public DbSet<FeesCollectionType> FeesCollectionTypes { get; set; }
        public DbSet<FeesGenerateAcademic> FeesGenerateAcademics { get; set; }
        public DbSet<FeesGenerateHead> FeesGenerateHeads { get; set; }
        public DbSet<FeesGenerate> FeesGenerates { get; set; }
        public DbSet<FeesGenerateStudentDetail> FeesGenerateStudentDetails { get; set; }
        public DbSet<FeesGenerateStudent> FeesGenerateStudents { get; set; }
        public DbSet<FeesHead> FeesHeads { get; set; }
        public DbSet<FeesType> FeesTypes { get; set; }
        public DbSet<FMS_AccountingReportType> FMS_AccountingReportType { get; set; }
        public DbSet<FMS_GLAccount> FMS_GLAccount { get; set; }
        public DbSet<FMS_GLAccountMasterType> FMS_GLAccountMasterType { get; set; }
        public DbSet<FMS_GLAccountTemplate> FMS_GLAccountTemplate { get; set; }
        public DbSet<FMS_GLAccountType> FMS_GLAccountType { get; set; }
        public DbSet<FMS_GLSubsidyTypeMap> FMS_GLSubsidyTypeMap { get; set; }
        public DbSet<FMS_GLWiseOpponentsGL> FMS_GLWiseOpponentsGL { get; set; }
        public DbSet<FMS_MonthlyLedgerSummary> FMS_MonthlyLedgerSummary { get; set; }
        public DbSet<FMS_PaymentReceivedType> FMS_PaymentReceivedType { get; set; }
        public DbSet<FMS_RelatedAccount> FMS_RelatedAccount { get; set; }
        public DbSet<FMS_SubsidyAccount> FMS_SubsidyAccount { get; set; }
        public DbSet<FMS_SubsidyType> FMS_SubsidyType { get; set; }
        public DbSet<FMS_TransactionType> FMS_TransactionType { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<GlobalBloodGroup> GlobalBloodGroups { get; set; }
        public DbSet<GlobalCoCurricularActivity> GlobalCoCurricularActivities { get; set; }
        public DbSet<GlobalCountry> GlobalCountries { get; set; }
        public DbSet<GlobalDistrict> GlobalDistricts { get; set; }
        public DbSet<GlobalDivision> GlobalDivisions { get; set; }
        public DbSet<GlobalEducationalQualification> GlobalEducationalQualifications { get; set; }
        public DbSet<GlobalGender> GlobalGenders { get; set; }
        public DbSet<GlobalInstituteType> GlobalInstituteTypes { get; set; }
        public DbSet<GlobalProfession> GlobalProfessions { get; set; }
        public DbSet<GlobalScholarship> GlobalScholarships { get; set; }
        public DbSet<GlobalSubDistrict> GlobalSubDistricts { get; set; }
        public DbSet<GlobalSubDistrictType> GlobalSubDistrictTypes { get; set; }
        public DbSet<GlobalUser> GlobalUsers { get; set; }
        public DbSet<GlobalUserType> GlobalUserTypes { get; set; }
        public DbSet<Governingbody> Governingbodies { get; set; }
        public DbSet<Guardian> Guardians { get; set; }
        public DbSet<GuardiansOfStudent> GuardiansOfStudents { get; set; }
        public DbSet<GuardianType> GuardianTypes { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ImageType> ImageTypes { get; set; }
        public DbSet<Institute> Institutes { get; set; }
        public DbSet<InstituteSubject> InstituteSubjects { get; set; }
        public DbSet<InstituteSubjectClass> InstituteSubjectClasses { get; set; }
        public DbSet<LeaveApplication> LeaveApplications { get; set; }
        public DbSet<LibraryBookAuthore> LibraryBookAuthores { get; set; }
        public DbSet<LibraryBookAuthorOfBook> LibraryBookAuthorOfBooks { get; set; }
        public DbSet<LibraryBook> LibraryBooks { get; set; }
        public DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public DbSet<MobilePayment> MobilePayments { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<Notice> Notices { get; set; }
        public DbSet<NoticeType> NoticeTypes { get; set; }
        public DbSet<NotificationTagGroup> NotificationTagGroups { get; set; }
        public DbSet<NotificationTag> NotificationTags { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<Religion> Religions { get; set; }
        public DbSet<ResultPublication> ResultPublications { get; set; }
        public DbSet<Right> Rights { get; set; }
        public DbSet<RightsOfPackage> RightsOfPackages { get; set; }
        public DbSet<RightsOfRole> RightsOfRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolesOfUserInfo> RolesOfUserInfoes { get; set; }
        public DbSet<RoutineDetail> RoutineDetails { get; set; }
        public DbSet<RoutineNote> RoutineNotes { get; set; }
        public DbSet<RoutinePeriod> RoutinePeriods { get; set; }
        public DbSet<RoutinePeriodType> RoutinePeriodTypes { get; set; }
        public DbSet<Routine> Routines { get; set; }
        public DbSet<ScholarshipOfStudent> ScholarshipOfStudents { get; set; }
        public DbSet<Scholarship> Scholarships { get; set; }
        public DbSet<ShortMessageDetail> ShortMessageDetails { get; set; }
        public DbSet<ShortMessage> ShortMessages { get; set; }
        public DbSet<ShortMessageStatus> ShortMessageStatuses { get; set; }
        public DbSet<ShortMessageTemplate> ShortMessageTemplates { get; set; }
        public DbSet<Sibling> Siblings { get; set; }
        public DbSet<SSO> SSOes { get; set; }
        public DbSet<StudentAttendanceDetail> StudentAttendanceDetails { get; set; }
        public DbSet<StudentAttendance> StudentAttendances { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectAcademicClassMapping> SubjectAcademicClassMappings { get; set; }
        public DbSet<SubjectAcademicClassMappingSubjectType> SubjectAcademicClassMappingSubjectTypes { get; set; }
        public DbSet<SubjectGroup> SubjectGroups { get; set; }
        public DbSet<SubjectStudentMapping> SubjectStudentMappings { get; set; }
        public DbSet<SubjectType> SubjectTypes { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherSubjectAcademicMapping> TeacherSubjectAcademicMappings { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<UserAttendanceDetail> UserAttendanceDetails { get; set; }
        public DbSet<UserAttendance> UserAttendances { get; set; }
        public DbSet<UserInfo> UserInfoes { get; set; }
        public DbSet<UserInfoSecurity> UserInfoSecurities { get; set; }
        public DbSet<UserInfoType> UserInfoTypes { get; set; }
        public DbSet<VendorInfo> VendorInfoes { get; set; }
        public DbSet<VendorType> VendorTypes { get; set; }
        public DbSet<VisitorCount> VisitorCounts { get; set; }
        public DbSet<VoucherDetail> VoucherDetails { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<WeekDay> WeekDays { get; set; }
        public DbSet<QryRightsOfUserInfo> QryRightsOfUserInfoes { get; set; }
        public DbSet<QryStudentAttendanceDetail> QryStudentAttendanceDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new C_MigrationMap());
            modelBuilder.Configurations.Add(new AcademicBranchMap());
            modelBuilder.Configurations.Add(new AcademicBranchesOfUserInfoMap());
            modelBuilder.Configurations.Add(new AcademicCalendarMap());
            modelBuilder.Configurations.Add(new AcademicClassMap());
            modelBuilder.Configurations.Add(new AcademicClassSectionMappingMap());
            modelBuilder.Configurations.Add(new AcademicGroupMap());
            modelBuilder.Configurations.Add(new AcademicPeriodMap());
            modelBuilder.Configurations.Add(new AcademicSectionMap());
            modelBuilder.Configurations.Add(new AcademicSessionMap());
            modelBuilder.Configurations.Add(new AcademicShiftMap());
            modelBuilder.Configurations.Add(new AcademicVersionMap());
            modelBuilder.Configurations.Add(new AddressMap());
            modelBuilder.Configurations.Add(new AddressTypeMap());
            modelBuilder.Configurations.Add(new AdmissionFormAddressMap());
            modelBuilder.Configurations.Add(new AdmissionFormGuardianMap());
            modelBuilder.Configurations.Add(new AdmissionFormMap());
            modelBuilder.Configurations.Add(new AttendanceConfigurationMap());
            modelBuilder.Configurations.Add(new AttendanceTypeMap());
            modelBuilder.Configurations.Add(new BankAccountInfoMap());
            modelBuilder.Configurations.Add(new BankAccountTypeMap());
            modelBuilder.Configurations.Add(new BloodGroupMap());
            modelBuilder.Configurations.Add(new BuildingRoomMap());
            modelBuilder.Configurations.Add(new BuildingMap());
            modelBuilder.Configurations.Add(new CertificatePrintMap());
            modelBuilder.Configurations.Add(new CertificatePrintTypeMap());
            modelBuilder.Configurations.Add(new ChartOfAccountMap());
            modelBuilder.Configurations.Add(new CoCurricularActivityMap());
            modelBuilder.Configurations.Add(new CoCurricularActivitiesOfStudentMap());
            modelBuilder.Configurations.Add(new ColourMap());
            modelBuilder.Configurations.Add(new ContactUMap());
            modelBuilder.Configurations.Add(new ContentDetailMap());
            modelBuilder.Configurations.Add(new ContentMap());
            modelBuilder.Configurations.Add(new CountryMap());
            modelBuilder.Configurations.Add(new DepartmentMap());
            modelBuilder.Configurations.Add(new DesignationMap());
            modelBuilder.Configurations.Add(new DiaryMap());
            modelBuilder.Configurations.Add(new DistrictOrStateMap());
            modelBuilder.Configurations.Add(new DocumentMap());
            modelBuilder.Configurations.Add(new DocumentTypeMap());
            modelBuilder.Configurations.Add(new DynamicPageBodyMap());
            modelBuilder.Configurations.Add(new DynamicPageMap());
            modelBuilder.Configurations.Add(new EducationalQualificationMap());
            modelBuilder.Configurations.Add(new EmployeeMap());
            modelBuilder.Configurations.Add(new EventMap());
            modelBuilder.Configurations.Add(new ExamMap());
            modelBuilder.Configurations.Add(new ExamGradeMap());
            modelBuilder.Configurations.Add(new ExamProcessMap());
            modelBuilder.Configurations.Add(new ExamSubjectMap());
            modelBuilder.Configurations.Add(new ExamSubjectMarkMap());
            modelBuilder.Configurations.Add(new ExamTypeMap());
            modelBuilder.Configurations.Add(new ExamTypeWiseTabulationSheetDetailMap());
            modelBuilder.Configurations.Add(new ExamTypeWiseTabulationSheetMasterMap());
            modelBuilder.Configurations.Add(new FeesAcademicClassMap());
            modelBuilder.Configurations.Add(new FeesAutoGenerateConfigMap());
            modelBuilder.Configurations.Add(new FeesAutoGenerateConfigDetailMap());
            modelBuilder.Configurations.Add(new FeesAutoGenerateConfigTypeMap());
            modelBuilder.Configurations.Add(new FeesBoothMap());
            modelBuilder.Configurations.Add(new FeesCollectionAdvanceMap());
            modelBuilder.Configurations.Add(new FeesCollectionDetailMap());
            modelBuilder.Configurations.Add(new FeesCollectionMap());
            modelBuilder.Configurations.Add(new FeesCollectionTypeMap());
            modelBuilder.Configurations.Add(new FeesGenerateAcademicMap());
            modelBuilder.Configurations.Add(new FeesGenerateHeadMap());
            modelBuilder.Configurations.Add(new FeesGenerateMap());
            modelBuilder.Configurations.Add(new FeesGenerateStudentDetailMap());
            modelBuilder.Configurations.Add(new FeesGenerateStudentMap());
            modelBuilder.Configurations.Add(new FeesHeadMap());
            modelBuilder.Configurations.Add(new FeesTypeMap());
            modelBuilder.Configurations.Add(new FMS_AccountingReportTypeMap());
            modelBuilder.Configurations.Add(new FMS_GLAccountMap());
            modelBuilder.Configurations.Add(new FMS_GLAccountMasterTypeMap());
            modelBuilder.Configurations.Add(new FMS_GLAccountTemplateMap());
            modelBuilder.Configurations.Add(new FMS_GLAccountTypeMap());
            modelBuilder.Configurations.Add(new FMS_GLSubsidyTypeMapMap());
            modelBuilder.Configurations.Add(new FMS_GLWiseOpponentsGLMap());
            modelBuilder.Configurations.Add(new FMS_MonthlyLedgerSummaryMap());
            modelBuilder.Configurations.Add(new FMS_PaymentReceivedTypeMap());
            modelBuilder.Configurations.Add(new FMS_RelatedAccountMap());
            modelBuilder.Configurations.Add(new FMS_SubsidyAccountMap());
            modelBuilder.Configurations.Add(new FMS_SubsidyTypeMap());
            modelBuilder.Configurations.Add(new FMS_TransactionTypeMap());
            modelBuilder.Configurations.Add(new GalleryMap());
            modelBuilder.Configurations.Add(new GenderMap());
            modelBuilder.Configurations.Add(new GlobalBloodGroupMap());
            modelBuilder.Configurations.Add(new GlobalCoCurricularActivityMap());
            modelBuilder.Configurations.Add(new GlobalCountryMap());
            modelBuilder.Configurations.Add(new GlobalDistrictMap());
            modelBuilder.Configurations.Add(new GlobalDivisionMap());
            modelBuilder.Configurations.Add(new GlobalEducationalQualificationMap());
            modelBuilder.Configurations.Add(new GlobalGenderMap());
            modelBuilder.Configurations.Add(new GlobalInstituteTypeMap());
            modelBuilder.Configurations.Add(new GlobalProfessionMap());
            modelBuilder.Configurations.Add(new GlobalScholarshipMap());
            modelBuilder.Configurations.Add(new GlobalSubDistrictMap());
            modelBuilder.Configurations.Add(new GlobalSubDistrictTypeMap());
            modelBuilder.Configurations.Add(new GlobalUserMap());
            modelBuilder.Configurations.Add(new GlobalUserTypeMap());
            modelBuilder.Configurations.Add(new GoverningbodyMap());
            modelBuilder.Configurations.Add(new GuardianMap());
            modelBuilder.Configurations.Add(new GuardiansOfStudentMap());
            modelBuilder.Configurations.Add(new GuardianTypeMap());
            modelBuilder.Configurations.Add(new ImageMap());
            modelBuilder.Configurations.Add(new ImageTypeMap());
            modelBuilder.Configurations.Add(new InstituteMap());
            modelBuilder.Configurations.Add(new InstituteSubjectMap());
            modelBuilder.Configurations.Add(new InstituteSubjectClassMap());
            modelBuilder.Configurations.Add(new LeaveApplicationMap());
            modelBuilder.Configurations.Add(new LibraryBookAuthoreMap());
            modelBuilder.Configurations.Add(new LibraryBookAuthorOfBookMap());
            modelBuilder.Configurations.Add(new LibraryBookMap());
            modelBuilder.Configurations.Add(new MaritalStatusMap());
            modelBuilder.Configurations.Add(new MobilePaymentMap());
            modelBuilder.Configurations.Add(new NationalityMap());
            modelBuilder.Configurations.Add(new NoticeMap());
            modelBuilder.Configurations.Add(new NoticeTypeMap());
            modelBuilder.Configurations.Add(new NotificationTagGroupMap());
            modelBuilder.Configurations.Add(new NotificationTagMap());
            modelBuilder.Configurations.Add(new PackageMap());
            modelBuilder.Configurations.Add(new PaymentTypeMap());
            modelBuilder.Configurations.Add(new ProfessionMap());
            modelBuilder.Configurations.Add(new ReligionMap());
            modelBuilder.Configurations.Add(new ResultPublicationMap());
            modelBuilder.Configurations.Add(new RightMap());
            modelBuilder.Configurations.Add(new RightsOfPackageMap());
            modelBuilder.Configurations.Add(new RightsOfRoleMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new RolesOfUserInfoMap());
            modelBuilder.Configurations.Add(new RoutineDetailMap());
            modelBuilder.Configurations.Add(new RoutineNoteMap());
            modelBuilder.Configurations.Add(new RoutinePeriodMap());
            modelBuilder.Configurations.Add(new RoutinePeriodTypeMap());
            modelBuilder.Configurations.Add(new RoutineMap());
            modelBuilder.Configurations.Add(new ScholarshipOfStudentMap());
            modelBuilder.Configurations.Add(new ScholarshipMap());
            modelBuilder.Configurations.Add(new ShortMessageDetailMap());
            modelBuilder.Configurations.Add(new ShortMessageMap());
            modelBuilder.Configurations.Add(new ShortMessageStatusMap());
            modelBuilder.Configurations.Add(new ShortMessageTemplateMap());
            modelBuilder.Configurations.Add(new SiblingMap());
            modelBuilder.Configurations.Add(new SSOMap());
            modelBuilder.Configurations.Add(new StudentAttendanceDetailMap());
            modelBuilder.Configurations.Add(new StudentAttendanceMap());
            modelBuilder.Configurations.Add(new StudentMap());
            modelBuilder.Configurations.Add(new SubjectMap());
            modelBuilder.Configurations.Add(new SubjectAcademicClassMappingMap());
            modelBuilder.Configurations.Add(new SubjectAcademicClassMappingSubjectTypeMap());
            modelBuilder.Configurations.Add(new SubjectGroupMap());
            modelBuilder.Configurations.Add(new SubjectStudentMappingMap());
            modelBuilder.Configurations.Add(new SubjectTypeMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new TagMap());
            modelBuilder.Configurations.Add(new TeacherMap());
            modelBuilder.Configurations.Add(new TeacherSubjectAcademicMappingMap());
            modelBuilder.Configurations.Add(new TestimonialMap());
            modelBuilder.Configurations.Add(new UserAttendanceDetailMap());
            modelBuilder.Configurations.Add(new UserAttendanceMap());
            modelBuilder.Configurations.Add(new UserInfoMap());
            modelBuilder.Configurations.Add(new UserInfoSecurityMap());
            modelBuilder.Configurations.Add(new UserInfoTypeMap());
            modelBuilder.Configurations.Add(new VendorInfoMap());
            modelBuilder.Configurations.Add(new VendorTypeMap());
            modelBuilder.Configurations.Add(new VisitorCountMap());
            modelBuilder.Configurations.Add(new VoucherDetailMap());
            modelBuilder.Configurations.Add(new VoucherMap());
            modelBuilder.Configurations.Add(new WeekDayMap());
            modelBuilder.Configurations.Add(new QryRightsOfUserInfoMap());
            modelBuilder.Configurations.Add(new QryStudentAttendanceDetailMap());
        }
    }
}
