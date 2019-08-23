using deepp.Entities.Models;
using deepp.utility;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Service.GlobalUsers
{
  
    public interface IGlobalUserService : IService<GlobalUser>
    {
        /// <summary>
        /// Gets all GlobalUser.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="searchText">The search text.</param>
        /// <param name="branchId">The branch identifier.</param>
        /// <param name="isActive">The is active.</param>
        /// <returns></returns>
        IEnumerable<GlobalUser> GetAllGlobalUser(int instituteId, string searchText = "", int? branchId = null, bool? isActive = null);
        /// <summary>
        /// Gets the GlobalUser by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        GlobalUser GetGlobalUserById(int id);
        /// <summary>
        /// News the GlobalUser.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        GlobalUser NewGlobalUser(int instituteId, int userId);
        /// <summary>
        /// Gets all short message detail.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="GlobalUser">The GlobalUser.</param>
        /// <param name="branchId">The branch identifier.</param>
        /// <returns></returns>
       // IEnumerable<ShortMessageDetail> GetAllShortMessageDetail(int instituteId, int userId, GlobalUser GlobalUser = null, int? branchId = null);

    }
    public class GlobalUserService : Service<GlobalUser>, IGlobalUserService
    {
        private readonly IRepositoryAsync<GlobalUser> _redeeppitory;

        private readonly IMaritalStatusService _maritalStatusService;
        private readonly IDesignationService _designationService;
        private readonly IDepartmentService _departmentService;
        private readonly IAcademicBranchService _academicBranchService;
        private readonly IAcademicBranchesOfUserInfoService _academicBranchesOfUserInfoService;

        public GlobalUserService(IRepositoryAsync<GlobalUser> redeeppitory,

            IMaritalStatusService maritalStatusService,
            IDesignationService designationService,
            IDepartmentService departmentService, IAcademicBranchService academicBranchService,
            IAcademicBranchesOfUserInfoService academicBranchesOfUserInfoService
            )
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;

            _maritalStatusService = maritalStatusService;
            _designationService = designationService;
            _departmentService = departmentService;
            _academicBranchService = academicBranchService;
            _academicBranchesOfUserInfoService = academicBranchesOfUserInfoService;
        }

        /// <summary>
        /// News the GlobalUser.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public GlobalUser NewGlobalUser(int instituteId, int userId)
        {
            var lstdepartmentKv = new List<KeyValuePair<int, string>>();
            _departmentService.GetDepartments(instituteId, true).ToList().ForEach(item => lstdepartmentKv.Add(new KeyValuePair<int, string>(item.Id, item.Name)));

            var lstacademicBranchKv = new List<KeyValuePair<int, string>>();
            //_academicBranchService.GetAcademicBranchsByInstituteId(instituteId).ToList().ForEach(item => lstacademicBranchKv.Add(new KeyValuePair<int, string>(item.Id, item.Name)));
            _academicBranchesOfUserInfoService.GetAcademicBranchesByUserId(userId).ToList().ForEach(item => lstacademicBranchKv.Add(new KeyValuePair<int, string>(item.Id, item.Name)));

            var lstMaritalStatusList = new List<KeyValuePair<int, string>>();
            _maritalStatusService.GetMaritalStatuss(instituteId, true).ToList().ForEach(item => lstMaritalStatusList.Add(new KeyValuePair<int, string>(item.Id, item.Name)));

            var lstDesignation = _designationService.GetDesignations(instituteId, true).Select(d => new KeyValuePair<int, string>(d.Id, d.Name)).ToList();
           // var lstCountries = _designationService.GetDesignations(instituteId, true).Select(d => new KeyValuePair<int, string>(d.Id, d.Name)).ToList();
            var objGlobalUser = new GlobalUser
            {
                MaritalStatusList = lstMaritalStatusList,
                 DesignationList = lstDesignation,
                DepartmentList = lstdepartmentKv,
                AcademicBranchList = lstacademicBranchKv,
               // CountryList= lstCountries,
            };
            return objGlobalUser;
        }
        /// <summary>
        /// Gets all GlobalUser.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="searchText">The search text.</param>
        /// <param name="branchId">The branch identifier.</param>
        /// <param name="isActive">The is active.</param>
        /// <returns></returns>
        public IEnumerable<GlobalUser> GetAllGlobalUser(int instituteId, string searchText = "", int? branchId = null, bool? isActive = null)
        {
            var predicate = PredicateBuilder.True<GlobalUser>();
            predicate = predicate.And(p => p.UserInfo.InstituteId == instituteId);

            if (!string.IsNullOrEmpty(searchText))
                predicate = predicate.And(p => p.UserInfo.Name.Contains(searchText) || p.GlobalUserId.ToString().Contains(searchText));

            if (branchId != null)
                predicate = predicate.And(p => p.UserInfo.AcademicBranchesOfUserInfoes.Select(s => s.AcademicBranchId).Contains((int)branchId));

            if (isActive != null)
                predicate = predicate.And(p => p.UserInfo.IsActive == isActive);

            return _redeeppitory.Query(predicate)
                .Include(u => u.UserInfo)
                .Include(u => u.UserInfo.AcademicBranchesOfUserInfoes)
                .Include(u => u.UserInfo.AcademicBranchesOfUserInfoes.Select(k => k.AcademicBranch))
                .Include(u => u.Designation).Include(s => s.Department)
                .Select();
        }
        /// <summary>
        /// Gets the GlobalUser by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public GlobalUser GetGlobalUserById(int id)
        {
            var GlobalUser = _redeeppitory.Query(x => x.GlobalUserId == id)
               .Include(x => x.Department)
               .Include(x => x.Designation)
               .Include(x => x.GlobalCountry)
               .Include(x => x.GlobalDivision)
               .Include(x => x.GlobalDistrict)
               .Include(x => x.GlobalSubDistrict)
                .Select().FirstOrDefault();
            return GlobalUser;

        }
        /// <summary>
        /// Gets all short message detail.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="GlobalUser">The GlobalUser.</param>
        /// <param name="branchId">The branch identifier.</param>
        /// <returns></returns>
        public IEnumerable<ShortMessageDetail> GetAllShortMessageDetail(int instituteId, int userId, GlobalUser GlobalUser = null, int? branchId = null)
        {

            var predicate = PredicateBuilder.True<GlobalUser>();
            predicate = predicate.And(p => p.UserInfo.InstituteId == instituteId && p.UserInfo.IsActive == true);
            if (GlobalUser != null)
            {
                if (GlobalUser.UserInfo != null)
                {
                    string searchText = String.IsNullOrEmpty(GlobalUser.UserInfo.Name) ? "" : GlobalUser.UserInfo.Name;
                    predicate =
                        predicate.And(
                            p =>
                                p.UserInfo.Name.Contains(searchText));
                }
                if (branchId != null && branchId > 0)
                {
                    predicate = predicate.And(p => p.UserInfo.AcademicBranchesOfUserInfoes.Select(s => s.AcademicBranchId).Contains((int)branchId));

                }
                if (GlobalUser.DesignationId > 0)
                    predicate = predicate.And(p => p.DesignationId == GlobalUser.DesignationId);
                if (GlobalUser.DepartmentId > 0)
                    predicate = predicate.And(p => p.DepartmentId == GlobalUser.DepartmentId);


            }

            var result = _redeeppitory.Query(predicate)
                //   .Include(s => s.UserInfo)
                .Select();
            return result
                .Select(s => new ShortMessageDetail() { UserInfoId = s.UserInfo.Id, MobileNumber = s.UserInfo.ContactNumber1, UserInfoName = s.UserInfo.Name });
        }
    }
}
