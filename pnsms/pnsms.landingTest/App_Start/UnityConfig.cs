using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using pnsms.Service.ViewModels;
using Repository.Pattern.DataContext;
using pnsms.Entities.Models;
using Repository.Pattern.UnitOfWork;
using Repository.Pattern.Repositories;
using Repository.Pattern.Ef6;
using pnsms.Service;

namespace pnsms.landingTest
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

            //container.RegisterType<IDataContextAsync, PNSMSContext>(new PerRequestLifetimeManager())
            //    .RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager())
            //    .RegisterType<IUnitOfWorkAsync, UnitOfWork>(new PerRequestLifetimeManager());
            container.RegisterType<IDataContextAsync, PNSMSContext>(new PerRequestLifetimeManager())
                .RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager())
                .RegisterType<IUnitOfWorkAsync, UnitOfWork>(new PerRequestLifetimeManager())
                .RegisterType<IRepositoryAsync<Notice>, Repository<Notice>>()
                .RegisterType<INoticeService, NoticeService>()
                .RegisterType<IRepositoryAsync<Event>, Repository<Event>>()
                .RegisterType<IEventService, EventService>()
                .RegisterType<IRepositoryAsync<Testimonial>, Repository<Testimonial>>()
                .RegisterType<ITestimonialService, TestimonialService>()
                .RegisterType<IRepositoryAsync<ContactU>, Repository<ContactU>>()
                .RegisterType<IContactUService, ContactUService>()
                .RegisterType<IRepositoryAsync<Institute>, Repository<Institute>>()
                .RegisterType<IInstituteService, InstituteService>()
                .RegisterType<IRepositoryAsync<Image>, Repository<Image>>()
                .RegisterType<IImageService, ImageService>()
                .RegisterType<IRepositoryAsync<Gallery>, Repository<Gallery>>()
                .RegisterType<IGalleryService, GalleryService>()

                .RegisterType<IRepositoryAsync<UserInfo>, Repository<UserInfo>>()
                .RegisterType<IUserInfoService, UserInfoService>()

                .RegisterType<IRepositoryAsync<Guardian>, Repository<Guardian>>()
                .RegisterType<IGuardianService, GuardianService>()

                .RegisterType<IVMportalService, VMportalService>()
                .RegisterType<IRepositoryAsync<Student>, Repository<Student>>()
                .RegisterType<IStudentService, StudentService>()

                .RegisterType<IRepositoryAsync<UserInfo>, Repository<UserInfo>>()
                .RegisterType<IUserInfoService, UserInfoService>()

                .RegisterType<IRepositoryAsync<AcademicBranch>, Repository<AcademicBranch>>()
                .RegisterType<IAcademicBranchService, AcademicBranchService>()

                .RegisterType<IRepositoryAsync<AcademicSection>, Repository<AcademicSection>>()
                .RegisterType<IAcademicSectionService, AcademicSectionService>()

                .RegisterType<IRepositoryAsync<AcademicSession>, Repository<AcademicSession>>()
                .RegisterType<IAcademicSessionService, AcademicSessionService>()

                .RegisterType<IRepositoryAsync<AcademicVersion>, Repository<AcademicVersion>>()
                .RegisterType<IAcademicVersionService, AcademicVersionService>()

                .RegisterType<IRepositoryAsync<AcademicShift>, Repository<AcademicShift>>()
                .RegisterType<IAcademicShiftService, AcademicShiftService>()

                .RegisterType<IRepositoryAsync<AcademicClass>, Repository<AcademicClass>>()
                .RegisterType<IAcademicClassService, AcademicClassService>()

                .RegisterType<IRepositoryAsync<AcademicSection>, Repository<AcademicSection>>()
                .RegisterType<IAcademicSectionService, AcademicSectionService>()

                .RegisterType<IRepositoryAsync<AcademicGroup>, Repository<AcademicGroup>>()
                .RegisterType<IAcademicGroupService, AcademicGroupService>()

                .RegisterType<IRepositoryAsync<BloodGroup>, Repository<BloodGroup>>()
                .RegisterType<IBloodGroupService, BloodGroupService>()
                .RegisterType<IRepositoryAsync<Gender>, Repository<Gender>>()
                .RegisterType<IGenderService, GenderService>()

                .RegisterType<IRepositoryAsync<Nationality>, Repository<Nationality>>()
                .RegisterType<INationalityService, NationalityService>()
                .RegisterType<IRepositoryAsync<Religion>, Repository<Religion>>()
                .RegisterType<IReligionService, ReligionService>()
                .RegisterType<IRepositoryAsync<Right>, Repository<Right>>()
                .RegisterType<IRightsService, RightsService>()
                .RegisterType<IStoredProcedures, PNSMSContext>()
                .RegisterType<IStoredProcedureService, StoredProcedureService>()

                .RegisterType<IRepositoryAsync<Guardian>, Repository<Guardian>>()
                .RegisterType<IGuardianService, GuardianService>()


                .RegisterType<IRepositoryAsync<GuardianType>, Repository<GuardianType>>()
                .RegisterType<IGuardianTypeService, GuardianTypeService>()


                .RegisterType<IRepositoryAsync<Profession>, Repository<Profession>>()
                .RegisterType<IProfessionService, ProfessionService>()


                .RegisterType<IRepositoryAsync<EducationalQualification>, Repository<EducationalQualification>>()
                .RegisterType<IEducationalQualificationService, EducationalQualificationService>()


                .RegisterType<IRepositoryAsync<GuardiansOfStudent>, Repository<GuardiansOfStudent>>()
                .RegisterType<IGuardiansOfStudentService, GuardiansOfStudentService>()


                .RegisterType<IRepositoryAsync<Role>, Repository<Role>>()
                .RegisterType<IRoleService, RoleService>()

                .RegisterType<IRepositoryAsync<RightsOfRole>, Repository<RightsOfRole>>()
                .RegisterType<IRightsOfRoleService, RightsOfRoleService>()

                .RegisterType<IRepositoryAsync<NoticeType>, Repository<NoticeType>>()
                .RegisterType<INoticeTypeService, NoticeTypeService>()

                .RegisterType<IRepositoryAsync<AcademicBranchesOfUserInfo>, Repository<AcademicBranchesOfUserInfo>>()
                .RegisterType<IAcademicBranchesOfUserInfoService, AcademicBranchesOfUserInfoService>()


                .RegisterType<IRepositoryAsync<CoCurricularActivity>, Repository<CoCurricularActivity>>()
                .RegisterType<ICoCurricularActivityService, CoCurricularActivityService>()

                

                ;

        }
    }
}
