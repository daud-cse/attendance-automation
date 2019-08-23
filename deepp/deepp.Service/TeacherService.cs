using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using deepp.Entities.Models;
using deepp.Service.ViewModels;
using Repository.Pattern.Repositories;
using Service.Pattern;
using deepp.Entities.ViewModels;
using deepp.utility;

namespace deepp.Service
{
    public interface ITeacherService : IService<Teacher>
    {
        /// <summary>
        /// Gets all teacher.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="searchText">The search text.</param>
        /// <param name="branchId">The branch identifier.</param>
        /// <param name="isActive">The is active.</param>
        /// <returns></returns>
        IEnumerable<Teacher> GetAllTeacher(int instituteId, string searchText = "", int? branchId = null, bool? isActive=null);
        /// <summary>
        /// Gets the teacher by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Teacher GetTeacherById(int id);
        /// <summary>
        /// News the teacher.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Teacher NewTeacher(int instituteId,int userId);

        /// <summary>
        /// Gets all short message detail.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="teacher">The teacher.</param>
        /// <returns></returns>
        IEnumerable<ShortMessageDetail> GetAllShortMessageDetail(int instituteId, int userId, Teacher teacher = null);
        List<KeyValuePair<int, string>> GetKVP(int instituteId);

    }
    public class TeacherService : Service<Teacher>, ITeacherService
    {
        private readonly IRepositoryAsync<Teacher> _redeeppitory;
        private readonly IAcademicBranchService _academicBranchService;
        private readonly IAcademicClassService _academicClassService;
        private readonly IAcademicSectionService _academicSectionService;
        private readonly IMaritalStatusService _maritalStatusService;
        private readonly IDesignationService _designationService;
        private readonly IDepartmentService _departmentService;
        private readonly IAcademicBranchesOfUserInfoService _academicBranchesOfUserInfoService;

        public TeacherService(IRepositoryAsync<Teacher> redeeppitory,
            IAcademicBranchService academicBranchService,
            IAcademicClassService academicClassService,
            IAcademicSectionService academicSectionService,
            IMaritalStatusService maritalStatusService,
            IDesignationService designationService,
            IAcademicBranchesOfUserInfoService academicBranchesOfUserInfoService,
            IDepartmentService departmentService
            )
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
            _academicBranchService = academicBranchService;
            _academicClassService = academicClassService;
            _academicSectionService = academicSectionService;
            _maritalStatusService = maritalStatusService;
            _designationService = designationService;
            _academicBranchesOfUserInfoService = academicBranchesOfUserInfoService;
            _departmentService=departmentService;
        }

        /// <summary>
        /// News the teacher.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public Teacher NewTeacher(int instituteId, int userId)
        {
            var lstacademicBranchKv = new List<KeyValuePair<int, string>>();
            //_academicBranchService.GetAcademicBranchsByInstituteId(instituteId).ToList().ForEach(item => lstacademicBranchKv.Add(new KeyValuePair<int, string>(item.Id, item.Name)));
            _academicBranchesOfUserInfoService.GetAcademicBranchesByUserId(userId).ToList().ForEach(item => lstacademicBranchKv.Add(new KeyValuePair<int, string>(item.Id, item.Name)));
            var lstAcademicClassList = new List<KeyValuePair<int, string>>();
            _academicClassService.GetAcademicClassesByInstituteId(instituteId).ToList().ForEach(item => lstAcademicClassList.Add(new KeyValuePair<int, string>(item.Id, item.Name)));

            var lstAcademicSectionList = new List<KeyValuePair<int, string>>();
            _academicSectionService.GetAcademicSectionByInstituteId(instituteId).ToList().ForEach(item => lstAcademicSectionList.Add(new KeyValuePair<int, string>(item.Id, item.Name)));

            var lstMaritalStatusList = new List<KeyValuePair<int, string>>();
            _maritalStatusService.GetMaritalStatuss(instituteId,true).ToList().ForEach(item => lstMaritalStatusList.Add(new KeyValuePair<int, string>(item.Id, item.Name)));

            var lstDesignation = _designationService.GetDesignations(instituteId,true).Select(d => new KeyValuePair<int, string>(d.Id, d.Name)).ToList();

         
            var objTeacher = new Teacher
            {
                AcademicBranchList = lstacademicBranchKv,
                AcademicClassList = lstAcademicClassList,
                AcademicSectionList = lstAcademicSectionList,
                MaritalStatusList = lstMaritalStatusList,
                DesignationList = lstDesignation,
                Departmentist=   _departmentService.GetKVP(instituteId),
            };
            return objTeacher;
        }
        public IEnumerable<Teacher> GetAllTeacher(int instituteId, string searchText = "", int? branchId = null, bool? isActive = null)
        {

            var predicate = PredicateBuilder.True<Teacher>();
            predicate = predicate.And(p => p.UserInfo.InstituteId==instituteId);

            if (!string.IsNullOrEmpty(searchText))
                predicate = predicate.And(p => p.UserInfo.Name.Contains(searchText) || p.TeacherId.ToString().Contains(searchText));

            if (branchId != null)
                predicate = predicate.And(p => p.UserInfo.AcademicBranchesOfUserInfoes.Select(s => s.AcademicBranchId).Contains((int)branchId));
            if (isActive != null)
                predicate = predicate.And(p => p.UserInfo.IsActive==isActive);

            return _redeeppitory.Query(predicate).Include(u => u.UserInfo)
                .Include(u => u.UserInfo.AcademicBranchesOfUserInfoes)
                .Include(u => u.UserInfo.AcademicBranchesOfUserInfoes.Select(k => k.AcademicBranch)).Include(u=>u.Designation).Select().OrderBy(u=>u.Designation.Ordering);

        }
        /// <summary>
        /// Gets all short message detail.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="teacher">The teacher.</param>
        /// <returns></returns>
        public IEnumerable<ShortMessageDetail> GetAllShortMessageDetail(int instituteId, int userId, Teacher teacher = null)
        {

            var predicate = PredicateBuilder.True<Teacher>();
            predicate = predicate.And(p => p.UserInfo.InstituteId == instituteId);
            if (teacher != null)
            {
                if (teacher.UserInfo != null)
                {
                    string searchText = String.IsNullOrEmpty(teacher.UserInfo.Name) ? "" : teacher.UserInfo.Name;
                    predicate =
                        predicate.And(
                            p =>
                                p.UserInfo.Name.Contains(searchText));
                }

                if (teacher.CurrentAcademicBranchId > 0)
                {
                    predicate = predicate.And(p => p.UserInfo.AcademicBranchesOfUserInfoes.Select(s => s.AcademicBranchId).Contains((int)teacher.CurrentAcademicBranchId));

                }
                

                if (teacher.DesignationId > 0)
                    predicate = predicate.And(p => p.DesignationId == teacher.DesignationId);

              
            }

            var result = _redeeppitory.Query(predicate)
                .Include(s => s.UserInfo)
                .Select();
            return result
                .Select(s => new ShortMessageDetail() { UserInfoId = s.UserInfo.Id, MobileNumber = s.UserInfo.ContactNumber1, UserInfoName = s.UserInfo.Name });
        }
        /// <summary>
        /// Gets the teacher by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Teacher GetTeacherById(int id)
        {
            var teacher = _redeeppitory.Query(x => x.TeacherId == id)
                .Include(x=>x.Designation)
                .Include(x => x.Department)
                .Include(x=>x.UserInfo).Select().FirstOrDefault();
            return teacher;

        }
        /// <summary>
        /// Gets the KVP.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <returns></returns>
        public List<KeyValuePair<int, string>> GetKVP(int instituteId)
        {
            var data = _redeeppitory.Query(c => c.UserInfo.IsActive && c.UserInfo.InstituteId==instituteId).Include(s => s.UserInfo).Select().ToList();

            var classList = new List<KeyValuePair<int, string>>();
            data.ForEach(c => classList.Add(new KeyValuePair<int, string>(c.TeacherId, c.UserInfo.Name)));

            return classList;
        }

    }

}

