using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;
using deepp.Entities.Models;
using deepp.utility;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace deepp.Service
{
    public interface IGuardianService
    {
        Guardian Find(params object[] keyValues);
        IQueryable<Guardian> SelectQuery(string query, params object[] parameters);
        void Insert(Guardian entity);
        void InsertRange(IEnumerable<Guardian> entities);
        void InsertOrUpdateGraph(Guardian entity);
        void InsertGraphRange(IEnumerable<Guardian> entities);
        void Update(Guardian entity);
        void Delete(object id);
        void Delete(Guardian entity);
        IQueryFluent<Guardian> Query();
        IQueryFluent<Guardian> Query(IQueryObject<Guardian> queryObject);
        IQueryFluent<Guardian> Query(Expression<Func<Guardian, bool>> query);
        Task<Guardian> FindAsync(params object[] keyValues);
        Task<Guardian> FindAsync(CancellationToken cancellationToken, params object[] keyValues);
        Task<bool> DeleteAsync(params object[] keyValues);
        Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);
        IQueryable<Guardian> Queryable();
        Guardian NewGuardian(int instituteId);
        /// <summary>
        /// return no of gurdians of a student
        /// Author: Mohebbo
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        IEnumerable<Guardian> GetGuardiansByStudentId(int studentId);
        /// <summary>
        /// Return GetGuardianDetails
        /// Date:30/04/2015
        /// Author: Mohebbo
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Guardian</returns>
        Guardian GetGuardianDetails(int id);

        /// <summary>
        /// Gets all short message detail.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="student">The student.</param>
        /// <returns></returns>
        IEnumerable<ShortMessageDetail> GetAllShortMessageDetail(int instituteId, int userId, Student student = null);
    }

    public class GuardianService : Service<Guardian>, IGuardianService
    {
        private readonly IRepositoryAsync<Guardian> _redeeppitory;
        private readonly IGuardianTypeService _guardianTypeService;
        private readonly IProfessionService _professionService;
        private readonly IEducationalQualificationService _educationalQualificationService;
        private readonly IGuardiansOfStudentService _guardiansOfStudentService;
        private readonly IStudentService _studentService;

        public GuardianService(IRepositoryAsync<Guardian> redeeppitory,
            IGuardianTypeService guardianTypeService,
            IProfessionService professionService, 
            IEducationalQualificationService educationalQualificationService,
            IGuardiansOfStudentService guardiansOfStudentService,
            IStudentService studentService)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
            _guardianTypeService = guardianTypeService;
            _professionService = professionService;
            _educationalQualificationService = educationalQualificationService;
            _guardiansOfStudentService = guardiansOfStudentService;
            _studentService = studentService;
        }

        public Guardian NewGuardian(int instituteId)
        {
            var objGuardian = new Guardian();
            objGuardian.UserInfo = new UserInfo();
            objGuardian.UserInfo.IsActive = true;
            var lstguardianTypeKv = new List<KeyValuePair<int, string>>();
            _guardianTypeService.GetGuardianTypeByInstituteId(instituteId).ToList().ForEach(item => lstguardianTypeKv.Add(new KeyValuePair<int, string>(item.Id, item.Name)));

            objGuardian.GuardianTypesList = lstguardianTypeKv;
            objGuardian.EducationalQualificationList = _educationalQualificationService.GetEducationalQualificationByInstituteId(instituteId).Select(e => new KeyValuePair<int, string>(e.Id, e.Name)).ToList();
            objGuardian.ProfessionList = _professionService.GetProfessionByInstituteId(instituteId).Select(e => new KeyValuePair<int, string>(e.Id, e.Name)).ToList();
            return objGuardian;
        }
        /// <summary>
        /// return no of gurdians of a student
        /// Author: Mohebbo
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public IEnumerable<Guardian> GetGuardiansByStudentId(int studentId)
        {
            var guardians = _guardiansOfStudentService.GetGuardiansOfStudent(studentId).Where(g => g.Guardian.UserInfo.IsActive == true).Select(ss => ss.Guardian);
            return guardians;
        }
        /// <summary>
        /// Return GetGuardianDetails
        /// Date:30/04/2015
        /// Author: Mohebbo
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Guardian</returns>
        public Guardian GetGuardianDetails(int id)
        {
            return _redeeppitory.Query(x => x.GuardianId == id)
                .Include(x => x.UserInfo).Include(x => x.Profession).Include(x => x.UserInfo.Nationality)
                .Select().SingleOrDefault();
        }

        /// <summary>
        /// Gets all user information.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="userId"></param>
        /// <param name="student">The student.</param>
        /// <returns></returns>
        public IEnumerable<ShortMessageDetail> GetAllShortMessageDetail(int instituteId, int userId, Student student = null)
        {
            //TODO: refactor GetAllStudent(...) of student service to get only ids.
            // try make a single query
            int[] studentIds = _studentService.GetAllShortMessageDetail(instituteId,userId, student).Select(s=>s.UserInfoId != null ? s.UserInfoId.Value : 0).ToArray();
            var guardians=_guardiansOfStudentService.GetGuardiansByStudentIds(studentIds);

            return guardians
               .Select(s => new ShortMessageDetail() { UserInfoId = s.Guardian.UserInfo.Id, MobileNumber = s.Guardian.UserInfo.ContactNumber1, UserInfoName = s.Guardian.UserInfo.Name, StudentId = s.StudentId});
        }

    }
}
