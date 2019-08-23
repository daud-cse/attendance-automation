using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Repository.Pattern.DataContext;
using Repository.Pattern.Repositories;
using deepp.Service;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;
using deepp.Service.Institutes;
using deepp.Service.ViewModels;
using deepp.Service.ShortMessages;
using deepp.Service.Library;
using deepp.Service.Subjects;
using deepp.Service.Exams;
using deepp.Service.GlobalUsers;
using deepp.Service.InstituteSubjects;
using Repository.Pattern.UnitOfWork;
using deepp.Service.Settings;
using deepp.Service.DashBoard;

namespace deepp.portal.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            //container.LoadConfiguration();

            container
                .RegisterType<IDataContextAsync, PNSMSContext>(new PerRequestLifetimeManager())
                .RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager())
                .RegisterType<IUnitOfWorkAsync, UnitOfWork>(new PerRequestLifetimeManager())
                .RegisterType<IStoredProcedures, PNSMSContext>(new PerRequestLifetimeManager())
                .RegisterType<IStoredProcedureService, StoredProcedureService>()

            #region settings 

                .RegisterType<IRepositoryAsync<AcademicBranch>, Repository<AcademicBranch>>()
                .RegisterType<IAcademicBranchService, AcademicBranchService>()

                .RegisterType<IRepositoryAsync<AcademicClass>, Repository<AcademicClass>>()
                .RegisterType<IAcademicClassService, AcademicClassService>()
                .RegisterType<IRepositoryAsync<AcademicGroup>, Repository<AcademicGroup>>()
                .RegisterType<IAcademicGroupService, AcademicGroupService>()
                .RegisterType<IRepositoryAsync<AcademicSection>, Repository<AcademicSection>>()
                .RegisterType<IAcademicSectionService, AcademicSectionService>()

                .RegisterType<IRepositoryAsync<AcademicSession>, Repository<AcademicSession>>()
                .RegisterType<IAcademicSessionService, AcademicSessionService>()

                .RegisterType<IRepositoryAsync<AcademicShift>, Repository<AcademicShift>>()
                .RegisterType<IAcademicShiftService, AcademicShiftService>()

                .RegisterType<IRepositoryAsync<AcademicVersion>, Repository<AcademicVersion>>()
                .RegisterType<IAcademicVersionService, AcademicVersionService>()

                .RegisterType<IRepositoryAsync<AddressType>, Repository<AddressType>>()
                .RegisterType<IAddressTypeService, AddressTypeService>()

                .RegisterType<IRepositoryAsync<BloodGroup>, Repository<BloodGroup>>()
                .RegisterType<IBloodGroupService, BloodGroupService>()

                .RegisterType<IRepositoryAsync<Country>, Repository<Country>>()
                .RegisterType<ICountryService, CountryService>()
                 .RegisterType<IRepositoryAsync<Department>, Repository<Department>>()
                .RegisterType<IDepartmentService, DepartmentService>()

                .RegisterType<IRepositoryAsync<Designation>, Repository<Designation>>()
                .RegisterType<IDesignationService, DesignationService>()

                .RegisterType<IRepositoryAsync<DistrictOrState>, Repository<DistrictOrState>>()
                .RegisterType<IDistrictOrStateService, DistrictOrStateService>()

                .RegisterType<IRepositoryAsync<EducationalQualification>, Repository<EducationalQualification>>()
                .RegisterType<IEducationalQualificationService, EducationalQualificationService>()

                .RegisterType<IRepositoryAsync<StudentAttendance>, Repository<StudentAttendance>>()
                .RegisterType<IRepositoryAsync<StudentAttendanceDetail>, Repository<StudentAttendanceDetail>>()

                .RegisterType<IRepositoryAsync<Gender>, Repository<Gender>>()
                .RegisterType<IGenderService, GenderService>()

                .RegisterType<IRepositoryAsync<GuardianType>, Repository<GuardianType>>()
                .RegisterType<IGuardianTypeService, GuardianTypeService>()

                .RegisterType<IRepositoryAsync<Institute>, Repository<Institute>>()
                .RegisterType<IInstituteService, InstituteService>()

                .RegisterType<IRepositoryAsync<MaritalStatus>, Repository<MaritalStatus>>()
                .RegisterType<IMaritalStatusService, MaritalStatusService>()

                .RegisterType<IRepositoryAsync<Nationality>, Repository<Nationality>>()
                .RegisterType<INationalityService, NationalityService>()

                .RegisterType<IRepositoryAsync<Package>, Repository<Package>>()
                .RegisterType<IPackageService, PackageService>()

                .RegisterType<IRepositoryAsync<Profession>, Repository<Profession>>()
                .RegisterType<IProfessionService, ProfessionService>()

                .RegisterType<IRepositoryAsync<Religion>, Repository<Religion>>()
                .RegisterType<IReligionService, ReligionService>()

                .RegisterType<IRepositoryAsync<Colour>, Repository<Colour>>()
                .RegisterType<IColourService, ColourService>()
                .RegisterType<IRepositoryAsync<AttendanceType>, Repository<AttendanceType>>()
                .RegisterType<IAttendanceTypeService, AttendanceTypeService>()

                   .RegisterType<IRepositoryAsync<InstituteSubject>, Repository<InstituteSubject>>()
                .RegisterType<IInstituteSubjectService, InstituteSubjectService>()



                .RegisterType<IRepositoryAsync<UserInfo>, Repository<UserInfo>>()
                .RegisterType<IUserInfoService, UserInfoService>()

                .RegisterType<IRepositoryAsync<Guardian>, Repository<Guardian>>()
                .RegisterType<IGuardianService, GuardianService>()

                .RegisterType<IRepositoryAsync<Address>, Repository<Address>>()
                .RegisterType<IAddressService, AddressService>()

                .RegisterType<IRepositoryAsync<AcademicPeriod>, Repository<AcademicPeriod>>()
                .RegisterType<IAcademicPeriodService, AcademicPeriodService>()

                .RegisterType<IRepositoryAsync<GuardiansOfStudent>, Repository<GuardiansOfStudent>>()
                .RegisterType<IGuardiansOfStudentService, GuardiansOfStudentService>()

                .RegisterType<IStudentAttendanceService, StudentAttendanceService>()
                .RegisterType<IStudentAttendanceDetailService, StudentAttendanceDetailService>()

                .RegisterType<IRepositoryAsync<AcademicClassSectionMapping>, Repository<AcademicClassSectionMapping>>()
                .RegisterType<IAcademicClassSectionMappingService, AcademicClassSectionMappingService>()



            #endregion


            #region Notice

                .RegisterType<IRepositoryAsync<Notice>, Repository<Notice>>()
                .RegisterType<INoticeService, NoticeService>()
                .RegisterType<IRepositoryAsync<NoticeType>, Repository<NoticeType>>()
                .RegisterType<INoticeTypeService, NoticeTypeService>()

            #endregion

            #region institute

                .RegisterType<IRepositoryAsync<GlobalCountry>, Repository<GlobalCountry>>()
                .RegisterType<IGlobalCountryService, GlobalCountryService>()
                .RegisterType<IRepositoryAsync<GlobalDistrict>, Repository<GlobalDistrict>>()
                .RegisterType<IGlobalDistrictService, GlobalDistrictService>()
                .RegisterType<IRepositoryAsync<GlobalSubDistrict>, Repository<GlobalSubDistrict>>()
                .RegisterType<IGlobalSubDistrictService, GlobalSubDistrictService>()
                .RegisterType<IRepositoryAsync<GlobalSubDistrictType>, Repository<GlobalSubDistrictType>>()
                .RegisterType<IGlobalSubDistrictTypeService, GlobalSubDistrictTypeService>()
                .RegisterType<IRepositoryAsync<GlobalDivision>, Repository<GlobalDivision>>()
                .RegisterType<IGlobalDivisionService, GlobalDivisionService>()
                .RegisterType<IRepositoryAsync<GlobalInstituteType>, Repository<GlobalInstituteType>>()
                .RegisterType<IGlobalInstituteTypeService, GlobalInstituteTypeService>()
            #endregion

            #region Right
                   .RegisterType<IRepositoryAsync<Right>, Repository<Right>>()
                   .RegisterType<IRightsService, RightsService>()
                   .RegisterType<IRepositoryAsync<RightsOfRole>, Repository<RightsOfRole>>()
                   .RegisterType<IRightsOfRoleService, RightsOfRoleService>()

            #endregion

            #region Role
                   .RegisterType<IRepositoryAsync<Role>, Repository<Role>>()
                   .RegisterType<IRoleService, RoleService>()
                   .RegisterType<IRepositoryAsync<RolesOfUserInfo>, Repository<RolesOfUserInfo>>()
                   .RegisterType<IRolesOfUserInfoService, RolesOfUserInfoService>()

            #endregion
            #region Testimonial
                .RegisterType<IRepositoryAsync<Testimonial>, Repository<Testimonial>>()
                .RegisterType<ITestimonialService, TestimonialService>()
            #endregion

            #region STUDENT
                .RegisterType<IRepositoryAsync<Student>, Repository<Student>>()
                .RegisterType<IStudentService, StudentService>()
                .RegisterType<IVmStudentService, VmStudentService>()
                .RegisterType<IVmStudentAttendanceService, VmStudentAttendanceService>()
                .RegisterType<IRepositoryAsync<CoCurricularActivity>, Repository<CoCurricularActivity>>()
                .RegisterType<ICoCurricularActivityService, CoCurricularActivityService>()
                .RegisterType<IRepositoryAsync<CoCurricularActivitiesOfStudent>, Repository<CoCurricularActivitiesOfStudent>>()
                .RegisterType<ICoCurricularActivitiesOfStudentService, CoCurricularActivitiesOfStudentService>()
                .RegisterType<IRepositoryAsync<Scholarship>, Repository<Scholarship>>()
                .RegisterType<IScholarshipService, ScholarshipService>()
                .RegisterType<IRepositoryAsync<ScholarshipOfStudent>, Repository<ScholarshipOfStudent>>()
                .RegisterType<IScholarshipOfStudentService, ScholarshipOfStudentService>()
            #endregion

            #region Teacher
                .RegisterType<IRepositoryAsync<Teacher>, Repository<Teacher>>()
                .RegisterType<ITeacherService, TeacherService>()
                .RegisterType<IVmTeacherService, VmTeacherService>()
            #endregion

            #region Employee
                .RegisterType<IRepositoryAsync<Employee>, Repository<Employee>>()
                .RegisterType<IEmployeeService, EmployeeService>()
                .RegisterType<IVmEmployeeService, VmEmployeeService>()
                .RegisterType<IRepositoryAsync<AcademicBranchesOfUserInfo>, Repository<AcademicBranchesOfUserInfo>>()
                .RegisterType<IAcademicBranchesOfUserInfoService, AcademicBranchesOfUserInfoService>()
            #endregion

            #region Global user
                .RegisterType<IRepositoryAsync<GlobalUser>, Repository<GlobalUser>>()
               .RegisterType<IGlobalUserService, GlobalUserService>()
               .RegisterType<IVmGlobalUsersService, VmGlobalUsersService>()

            #endregion


            #region Image

                .RegisterType<IRepositoryAsync<Image>, Repository<Image>>()
                .RegisterType<IImageService, ImageService>()

            #endregion

            #region Gallery

                .RegisterType<IRepositoryAsync<Gallery>, Repository<Gallery>>()
                .RegisterType<IGalleryService, GalleryService>()

            #endregion

            #region Event

                .RegisterType<IRepositoryAsync<Event>, Repository<Event>>()
                .RegisterType<IEventService, EventService>()

            #endregion

            #region Mobile Payment

                .RegisterType<IRepositoryAsync<MobilePayment>, Repository<MobilePayment>>()
                .RegisterType<IMobilePaymentService, MobilePaymentService>()
                .RegisterType<IRepositoryAsync<PaymentType>, Repository<PaymentType>>()
                .RegisterType<IPaymentTypeService, PaymentTypeService>()

            #endregion

            #region User Attendance

                .RegisterType<IRepositoryAsync<UserAttendance>, Repository<UserAttendance>>()
                .RegisterType<IUserAttendanceService, UserAttendanceService>()
                .RegisterType<IRepositoryAsync<UserAttendanceDetail>, Repository<UserAttendanceDetail>>()
                .RegisterType<IUserAttendanceDetailService, UserAttendanceDetailService>()
                .RegisterType<IVmUserAttendanceService, VmUserAttendanceService>()

            #endregion

            #region ShortMessageTemplate. NotificationTag
                .RegisterType<IRepositoryAsync<ShortMessage>, Repository<ShortMessage>>()
                .RegisterType<IShortMessageService, ShortMessageService>()
                .RegisterType<IRepositoryAsync<ShortMessageDetail>, Repository<ShortMessageDetail>>()
                .RegisterType<IShortMessageDetailService, ShortMessageDetailService>()
                .RegisterType<IRepositoryAsync<ShortMessageTemplate>, Repository<ShortMessageTemplate>>()
                .RegisterType<IShortMessageTemplateService, ShortMessageTemplateService>()
                .RegisterType<IRepositoryAsync<NotificationTag>, Repository<NotificationTag>>()
                .RegisterType<INotificationTagService, NotificationTagService>()
                .RegisterType<IRepositoryAsync<NotificationTagGroup>, Repository<NotificationTagGroup>>()
                .RegisterType<INotificationTagGroupService, NotificationTagGroupService>()
            #endregion

            #region Certificate Print
                .RegisterType<IRepositoryAsync<CertificatePrint>, Repository<CertificatePrint>>()
                .RegisterType<ICertificatePrintService, CertificatePrintService>()
                .RegisterType<IRepositoryAsync<CertificatePrintType>, Repository<CertificatePrintType>>()
                .RegisterType<ICertificatePrintTypeService, CertificatePrintTypeService>()
            #endregion

            #region Online Admission
                .RegisterType<IRepositoryAsync<AdmissionForm>, Repository<AdmissionForm>>()
                .RegisterType<IAdmissionFormService, AdmissionFormService>()
                .RegisterType<IRepositoryAsync<AdmissionFormAddress>, Repository<AdmissionFormAddress>>()
                .RegisterType<IAdmissionFormAddressService, AdmissionFormAddressService>()
                .RegisterType<IRepositoryAsync<AdmissionFormGuardian>, Repository<AdmissionFormGuardian>>()
                .RegisterType<IAdmissionFormGuardianService, AdmissionFormGuardianService>()
                .RegisterType<IVmOnlineAdmissionService, VmOnlineAdmissionService>()
            #endregion

            #region Governingbody 

                .RegisterType<IRepositoryAsync<Governingbody>, Repository<Governingbody>>()
                .RegisterType<IGoverningbodyService, GoverningbodyService>()

            #endregion

            #region Contact & FeedBack

                .RegisterType<IRepositoryAsync<ContactU>, Repository<ContactU>>()
                .RegisterType<IContactUService, ContactUService>()

            #endregion

            #region UserSecurities

                .RegisterType<IRepositoryAsync<UserInfoSecurity>, Repository<UserInfoSecurity>>()
                .RegisterType<IUserInfoSecurityService, UserInfoSecurityService>()

            #endregion

         

            #region Result Publish

                .RegisterType<IRepositoryAsync<ResultPublication>, Repository<ResultPublication>>()
                .RegisterType<IResultPublicationService, ResultPublicationService>()

            #endregion

            #region Voucher Entry

                .RegisterType<IVmVoucherService, VmVoucherService>()
                .RegisterType<IRepositoryAsync<Voucher>, Repository<Voucher>>()
                .RegisterType<IVoucherService, VoucherService>()
                .RegisterType<IRepositoryAsync<VoucherDetail>, Repository<VoucherDetail>>()
                .RegisterType<IVoucherDetailService, VoucherDetailService>()
                .RegisterType<IRepositoryAsync<ChartOfAccount>, Repository<ChartOfAccount>>()
                .RegisterType<IChartOfAccountService, ChartOfAccountService>()

            #endregion

          
         
         
            
                .RegisterType<IDashboardService, DashboardService>()
                .RegisterType<IVMportalService, VMportalService>()
                
;
        }
    }
}
