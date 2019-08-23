using System;
using Microsoft.Practices.Unity;
using deepp.Service.Institutes;
using Repository.Pattern.DataContext;
using deepp.Entities.Models;
using Repository.Pattern.UnitOfWork;
using Repository.Pattern.Repositories;
using Repository.Pattern.Ef6;
using deepp.Service;
using deepp.Service.ViewModels;
using deepp.Service.ShortMessages;
using deepp.Service.SSOLogin;
using deepp.Service.Settings;
using deepp.Service.DashBoard;
using deepp.Service.GlobalUsers;


namespace deepp.landing
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
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();
                 container.RegisterType<IDataContextAsync, PNSMSContext>(new PerRequestLifetimeManager())
                .RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager())
                .RegisterType<IUnitOfWorkAsync, UnitOfWork>(new PerRequestLifetimeManager())
                .RegisterType<IRepositoryAsync<Notice>, Repository<Notice>>()
                .RegisterType<INoticeService, NoticeService>()
                .RegisterType<IRepositoryAsync<NoticeType>, Repository<NoticeType>>()
                .RegisterType<INoticeTypeService, NoticeTypeService>()
                .RegisterType<IRepositoryAsync<Event>, Repository<Event>>()
                .RegisterType<IEventService, EventService>()
                .RegisterType<IRepositoryAsync<Testimonial>, Repository<Testimonial>>()
                .RegisterType<ITestimonialService, TestimonialService>()
                .RegisterType<IVmLandingService, VmLandingService>()
                .RegisterType<IRepositoryAsync<ContactU>, Repository<ContactU>>()
                .RegisterType<IContactUService, ContactUService>()
                .RegisterType<IRepositoryAsync<Image>, Repository<Image>>()
                .RegisterType<IImageService, ImageService>()
                .RegisterType<IRepositoryAsync<Gallery>, Repository<Gallery>>()
                .RegisterType<IGalleryService, GalleryService>()

                .RegisterType<IRepositoryAsync<UserInfo>, Repository<UserInfo>>()
                .RegisterType<IUserInfoService, UserInfoService>()

                .RegisterType<IRepositoryAsync<Package>, Repository<Package>>()
                .RegisterType<IPackageService, PackageService>()
                .RegisterType<IRepositoryAsync<UserInfoSecurity>, Repository<UserInfoSecurity>>()
                .RegisterType<IUserInfoSecurityService, UserInfoSecurityService>()

                .RegisterType<IRepositoryAsync<UserInfoSecurity>, Repository<UserInfoSecurity>>()
                .RegisterType<IUserInfoSecurityService, UserInfoSecurityService>()

                //.RegisterType<IRepositoryAsync<Content>, Repository<Content>>()
                //.RegisterType<IContentService, ContentService>()

                // .RegisterType<IRepositoryAsync<ContentDetail>, Repository<ContentDetail>>()
                //.RegisterType<IContentDetailService, ContentDetailService>()

                .RegisterType<IRepositoryAsync<GuardiansOfStudent>, Repository<GuardiansOfStudent>>()
                .RegisterType<IGuardiansOfStudentService, GuardiansOfStudentService>()

                .RegisterType<IRepositoryAsync<BloodGroup>, Repository<BloodGroup>>()
                .RegisterType<IBloodGroupService, BloodGroupService>()

                .RegisterType<IRepositoryAsync<Gender>, Repository<Gender>>()
                .RegisterType<IGenderService, GenderService>()

                .RegisterType<IRepositoryAsync<Nationality>, Repository<Nationality>>()
                .RegisterType<INationalityService, NationalityService>()

                .RegisterType<IRepositoryAsync<Religion>, Repository<Religion>>()
                .RegisterType<IReligionService, ReligionService>()

                .RegisterType<IRepositoryAsync<NoticeType>, Repository<NoticeType>>()
                .RegisterType<INoticeTypeService, NoticeTypeService>()
                .RegisterType<IRepositoryAsync<Teacher>, Repository<Teacher>>()
                .RegisterType<ITeacherService, TeacherService>()
                .RegisterType<IVmTeacherService, VmTeacherService>()
                .RegisterType<IRepositoryAsync<Employee>, Repository<Employee>>()
                .RegisterType<IEmployeeService, EmployeeService>()

                .RegisterType<IScholarshipService, ScholarshipService>()
                .RegisterType<IRepositoryAsync<Scholarship>, Repository<Scholarship>>()

                .RegisterType<IGuardianService, GuardianService>()
                .RegisterType<IRepositoryAsync<Guardian>, Repository<Guardian>>()


                .RegisterType<IScholarshipOfStudentService, ScholarshipOfStudentService>()
                .RegisterType<IRepositoryAsync<ScholarshipOfStudent>, Repository<ScholarshipOfStudent>>()
                
                .RegisterType<IAdmissionFormService, AdmissionFormService>()
                .RegisterType<IRepositoryAsync<AdmissionForm>, Repository<AdmissionForm>>()
                .RegisterType<IAdmissionFormAddressService, AdmissionFormAddressService>()
                .RegisterType<IRepositoryAsync<AdmissionFormAddress>, Repository<AdmissionFormAddress>>()
                .RegisterType<IAdmissionFormGuardianService, AdmissionFormGuardianService>()
                .RegisterType<IRepositoryAsync<AdmissionFormGuardian>, Repository<AdmissionFormGuardian>>()

                .RegisterType<IGoverningbodyService, GoverningbodyService>()
                .RegisterType<IRepositoryAsync<Governingbody>, Repository<Governingbody>>()
                 .RegisterType<IStoredProcedures, PNSMSContext>(new PerRequestLifetimeManager())
                .RegisterType<IStoredProcedureService, StoredProcedureService>()

                .RegisterType<IShortMessageTemplateService, ShortMessageTemplateService>()
                .RegisterType<IRepositoryAsync<ShortMessageTemplate>, Repository<ShortMessageTemplate>>()
                
                .RegisterType<IShortMessageService, ShortMessageService>()
                .RegisterType<IRepositoryAsync<ShortMessage>, Repository<ShortMessage>>()
                .RegisterType<IRepositoryAsync<ShortMessageDetail>, Repository<ShortMessageDetail>>()

                .RegisterType<INotificationTagService, NotificationTagService>()
                .RegisterType<IRepositoryAsync<NotificationTag>, Repository<NotificationTag>>()

                .RegisterType<INotificationTagGroupService, NotificationTagGroupService>()
                .RegisterType<IRepositoryAsync<NotificationTagGroup>, Repository<NotificationTagGroup>>()
                .RegisterType<IVmOnlineAdmissionService, VmOnlineAdmissionService>()

              
                .RegisterType<IVoucherDetailService, VoucherDetailService>()
                .RegisterType<IRepositoryAsync<Voucher>, Repository<Voucher>>()
                .RegisterType<IRepositoryAsync<VoucherDetail>, Repository<VoucherDetail>>()
                .RegisterType<IRepositoryAsync<ChartOfAccount>, Repository<ChartOfAccount>>()



            #region INSTITUE
.RegisterType<IRepositoryAsync<Institute>, Repository<Institute>>()
                .RegisterType<IInstituteService, InstituteService>()
                .RegisterType<IGlobalCountryService, GlobalCountryService>()
                .RegisterType<IRepositoryAsync<GlobalCountry>, Repository<GlobalCountry>>()
                .RegisterType<IGlobalDistrictService, GlobalDistrictService>()
                .RegisterType<IRepositoryAsync<GlobalDistrict>, Repository<GlobalDistrict>>()
                .RegisterType<IGlobalSubDistrictService, GlobalSubDistrictService>()
                .RegisterType<IRepositoryAsync<GlobalSubDistrict>, Repository<GlobalSubDistrict>>()
                .RegisterType<IGlobalDivisionService, GlobalDivisionService>()
                .RegisterType<IRepositoryAsync<GlobalDivision>, Repository<GlobalDivision>>()
                .RegisterType<IGlobalInstituteTypeService, GlobalInstituteTypeService>()
                .RegisterType<IRepositoryAsync<GlobalInstituteType>, Repository<GlobalInstituteType>>()


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
            #endregion

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


                .RegisterType<IRepositoryAsync<AcademicClassSectionMapping>, Repository<AcademicClassSectionMapping>>()
                .RegisterType<IAcademicClassSectionMappingService, AcademicClassSectionMappingService>()

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
                 .RegisterType<IRepositoryAsync<AcademicBranchesOfUserInfo>, Repository<AcademicBranchesOfUserInfo>>()
                .RegisterType<IAcademicBranchesOfUserInfoService, AcademicBranchesOfUserInfoService>()
                 .RegisterType<IRepositoryAsync<Address>, Repository<Address>>()
                 .RegisterType<IAddressService, AddressService>()
                  .RegisterType<IRepositoryAsync<Profession>, Repository<Profession>>()
                 .RegisterType<IProfessionService, ProfessionService>()

                 
            #endregion

            #region Role
.RegisterType<IRepositoryAsync<Role>, Repository<Role>>()
                   .RegisterType<IRoleService, RoleService>()
                   .RegisterType<IRepositoryAsync<RolesOfUserInfo>, Repository<RolesOfUserInfo>>()
                   .RegisterType<IRolesOfUserInfoService, RolesOfUserInfoService>()
            #endregion
            #region Right
.RegisterType<IRepositoryAsync<Right>, Repository<Right>>()
                   .RegisterType<IRightsService, RightsService>()
                   .RegisterType<IRepositoryAsync<RightsOfRole>, Repository<RightsOfRole>>()
                   .RegisterType<IRightsOfRoleService, RightsOfRoleService>()
            #endregion
                   .RegisterType<IDashboardService, DashboardService>()
            #region Global user
                .RegisterType<IRepositoryAsync<GlobalUser>, Repository<GlobalUser>>()
               .RegisterType<IGlobalUserService, GlobalUserService>()
               .RegisterType<IVmGlobalUsersService, VmGlobalUsersService>()

            #endregion


           
            #region SSO
               .RegisterType<IRepositoryAsync<SSO>, Repository<SSO>>()
               .RegisterType<ISSOService, SSOService>();

            #endregion
               

        }
    }
}
