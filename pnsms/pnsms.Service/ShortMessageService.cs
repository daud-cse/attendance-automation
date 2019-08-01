using pnsms.Entities.Models;
using pnsms.Service.ShortMessages;
using pnsms.Service.ViewModels;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service
{
    public interface IShortMessageService
    {
        /// <summary>
        /// Gets the short message by institute identifier.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <returns></returns>
        IEnumerable<ShortMessage> GetShortMessageByInstituteId(int instituteId);
        /// <summary>
        /// Gets the short message by identifier.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        ShortMessage  GetShortMessageById(int instituteId,int id);

        /// <summary>
        /// Gets the new short message.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        VmShortMessage GetNewShortMessage(int instituteId, int userId);
        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="vmShortMessage">The vm short message.</param>
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, VmShortMessage vmShortMessage);

        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="shortMessage">The short message.</param>
        void Update(IUnitOfWorkAsync unitOfWorkAsync, ShortMessage shortMessage);
    }

    public class ShortMessageService : IShortMessageService
    {
        readonly IStoredProcedures sprService;
        readonly IRepositoryAsync<ShortMessage> repository;
        readonly IRepositoryAsync<ShortMessageDetail> repositoryDetail;
        private readonly IEmployeeService _employeeService;
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;
        private readonly IGuardianService _guardianService;
        private readonly IShortMessageTemplateService _shortMessageTemplateService;

        public ShortMessageService(IStoredProcedures sprService, 
            IRepositoryAsync<ShortMessage> repository,
            IRepositoryAsync<ShortMessageDetail> repositoryDetail,
            IEmployeeService employeeService,
            IStudentService  studentService,
            ITeacherService teacherService,
            IGuardianService guardianService,
            IShortMessageTemplateService shortMessageTemplateService
            )
        {
            this.sprService = sprService;
            this.repository = repository;
            this.repositoryDetail = repositoryDetail;
            _employeeService = employeeService;
            _studentService = studentService;
            _teacherService = teacherService;
            _guardianService = guardianService;
            _shortMessageTemplateService = shortMessageTemplateService;
        }

        /// <summary>
        /// Generates the SMS.
        /// </summary>
        /// <returns></returns>
        public string GenerateSms()
        {
            return sprService.SprSmsGeneration();
        }
         
        /// <summary>
        /// Gets the short message by institute identifier.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <returns></returns>
        public IEnumerable<ShortMessage> GetShortMessageByInstituteId(int instituteId)
        {
            return   repository.Query(d => d.InstituteId == instituteId).Select();
        }

        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="shortMessage">The short message.</param>
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, VmShortMessage vmShortMessage)
        {

            repository.Insert(vmShortMessage.ShortMessages);
            unitOfWorkAsync.SaveChanges();

            foreach (var shortmessageDetail in vmShortMessage.ShortMessageDetails)
            {
                shortmessageDetail.ShortMessageId = vmShortMessage.ShortMessages.Id;
                shortmessageDetail.MobileNumber = shortmessageDetail.MobileNumber ?? "";
                repositoryDetail.Insert(shortmessageDetail);
            }
            unitOfWorkAsync.SaveChanges();
        }

        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="shortMessage">The short message.</param>
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, ShortMessage shortMessage)
        {

            repository.Update(shortMessage);
            unitOfWorkAsync.SaveChanges();
        }


        /// <summary>
        /// Gets the short message by identifier.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ShortMessage GetShortMessageById(int instituteId, int id)
        {
            return repository.Query(d => d.Id == id).Include(d=>d.ShortMessageDetails).Select().SingleOrDefault();
        }


        /// <summary>
        /// Gets the new short message.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public VmShortMessage GetNewShortMessage(int instituteId, int userId)
        {
            var vmShortMessage = new VmShortMessage();

            vmShortMessage.ShortMessages= new ShortMessage();
            vmShortMessage.Employee = _employeeService.NewEmployee(instituteId, userId);
            vmShortMessage.Student = _studentService.NewStudent(instituteId, userId,0);
            vmShortMessage.Teacher = _teacherService.NewTeacher(instituteId, userId);
            vmShortMessage.Guardian = _guardianService.NewGuardian(instituteId);
            vmShortMessage.ShortMessageTemplate = _shortMessageTemplateService.NewShortMessageTemplate(instituteId);
            vmShortMessage.ShortMessageTemplates =
                _shortMessageTemplateService.GetShortMessageTemplatesByInstituteId(instituteId).ToList();
            return vmShortMessage;
        }
    }

    public interface IShortMessageOuterService
    {
        /// <summary>
        /// Generates the SMS.
        /// </summary>
        /// <returns></returns>
        string GenerateSms();
    }

    public class ShortMessageOuterService : IShortMessageOuterService
    {
        readonly IStoredProcedures sprService;

        public ShortMessageOuterService(IStoredProcedures sprService
            )
        {
            this.sprService = sprService;
        }

        /// <summary>
        /// Generates the SMS.
        /// </summary>
        /// <returns></returns>
        public string GenerateSms()
        {
            return sprService.SprSmsGeneration();
        }
    }
}
