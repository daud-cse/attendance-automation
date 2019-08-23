using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using deepp.Entities.Models;
using deepp.utility;
using Repository.Pattern.Repositories;
using Service.Pattern;
using deepp.Entities.ViewModels;

namespace deepp.Service
{

    public interface IEmployeeService : IService<Employee>
    {
        /// <summary>
        /// Gets all employee.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="searchText">The search text.</param>
        /// <param name="branchId">The branch identifier.</param>
        /// <param name="isActive">The is active.</param>
        /// <returns></returns>
        IEnumerable<Employee> GetAllEmployee(int instituteId, string searchText = "", int? branchId = null, bool? isActive = null);
        /// <summary>
        /// Gets the employee by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Employee GetEmployeeById(int id);
        /// <summary>
        /// News the employee.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Employee NewEmployee(int instituteId, int userId);
        /// <summary>
        /// Gets all short message detail.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="employee">The employee.</param>
        /// <param name="branchId">The branch identifier.</param>
        /// <returns></returns>
        IEnumerable<ShortMessageDetail> GetAllShortMessageDetail(int instituteId, int userId, Employee employee = null,int? branchId = null);

    }
    public class EmployeeService : Service<Employee>, IEmployeeService
    {
        private readonly IRepositoryAsync<Employee> _redeeppitory;

        private readonly IMaritalStatusService _maritalStatusService;
        private readonly IDesignationService _designationService;
        private readonly IDepartmentService _departmentService;
        private readonly IAcademicBranchService _academicBranchService;
        private readonly IAcademicBranchesOfUserInfoService _academicBranchesOfUserInfoService;

        public EmployeeService(IRepositoryAsync<Employee> redeeppitory,

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
        /// News the employee.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public Employee NewEmployee(int instituteId,int userId)
        {
            var lstdepartmentKv = new List<KeyValuePair<int, string>>();
            _departmentService.GetDepartments(instituteId,true).ToList().ForEach(item => lstdepartmentKv.Add(new KeyValuePair<int, string>(item.Id, item.Name)));

            var lstacademicBranchKv = new List<KeyValuePair<int, string>>();
            //_academicBranchService.GetAcademicBranchsByInstituteId(instituteId).ToList().ForEach(item => lstacademicBranchKv.Add(new KeyValuePair<int, string>(item.Id, item.Name)));
            _academicBranchesOfUserInfoService.GetAcademicBranchesByUserId(userId).ToList().ForEach(item => lstacademicBranchKv.Add(new KeyValuePair<int, string>(item.Id, item.Name)));

            var lstMaritalStatusList = new List<KeyValuePair<int, string>>();
            _maritalStatusService.GetMaritalStatuss(instituteId,true).ToList().ForEach(item => lstMaritalStatusList.Add(new KeyValuePair<int, string>(item.Id, item.Name)));

            var lstDesignation = _designationService.GetDesignations(instituteId,true).Select(d => new KeyValuePair<int, string>(d.Id, d.Name)).ToList();
            var objemployee = new Employee
            {
                MaritalStatusList = lstMaritalStatusList,
                DesignationList = lstDesignation,
                DepartmentList = lstdepartmentKv,
                AcademicBranchList = lstacademicBranchKv
            };
            return objemployee;
        }
        /// <summary>
        /// Gets all employee.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="searchText">The search text.</param>
        /// <param name="branchId">The branch identifier.</param>
        /// <param name="isActive">The is active.</param>
        /// <returns></returns>
        public IEnumerable<Employee> GetAllEmployee(int instituteId, string searchText = "", int? branchId = null, bool? isActive = null)
        {
            var predicate = PredicateBuilder.True<Employee>();
            predicate = predicate.And(p => p.UserInfo.InstituteId==instituteId);

            if (!string.IsNullOrEmpty(searchText))
                predicate = predicate.And(p => p.UserInfo.Name.Contains(searchText) || p.EmployeeId.ToString().Contains(searchText));

            if (branchId != null)
                predicate = predicate.And(p => p.UserInfo.AcademicBranchesOfUserInfoes.Select(s => s.AcademicBranchId).Contains((int)branchId));
           
            if (isActive != null)
                predicate = predicate.And(p => p.UserInfo.IsActive == isActive);

            return _redeeppitory.Query(predicate).Include(u => u.UserInfo)
                .Include(u => u.UserInfo.AcademicBranchesOfUserInfoes)
                .Include(u => u.UserInfo.AcademicBranchesOfUserInfoes.Select(k => k.AcademicBranch))
                .Include(u => u.Designation).Include(s => s.Department).Select();
        }
        /// <summary>
        /// Gets the employee by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Employee GetEmployeeById(int id)
        {
            var employee = _redeeppitory.Query(x => x.EmployeeId == id)
                .Include(x=>x.Department)
                .Include(x => x.Designation)
                .Include(x=>x.UserInfo)
                .Select().FirstOrDefault();
            return employee;

        }
        /// <summary>
        /// Gets all short message detail.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="employee">The employee.</param>
        /// <param name="branchId">The branch identifier.</param>
        /// <returns></returns>
        public IEnumerable<ShortMessageDetail> GetAllShortMessageDetail(int instituteId, int userId, Employee employee = null, int? branchId = null)
        {

            var predicate = PredicateBuilder.True<Employee>();
            predicate = predicate.And(p => p.UserInfo.InstituteId == instituteId && p.UserInfo.IsActive== true );
            if (employee != null)
            {
                if (employee.UserInfo != null)
                {
                    string searchText = String.IsNullOrEmpty(employee.UserInfo.Name) ? "" : employee.UserInfo.Name;
                    predicate =
                        predicate.And(
                            p =>
                                p.UserInfo.Name.Contains(searchText));
                }
                if (branchId != null && branchId>0)
                {
                    predicate = predicate.And(p => p.UserInfo.AcademicBranchesOfUserInfoes.Select(s => s.AcademicBranchId).Contains((int)branchId));

                }
                if (employee.DesignationId > 0)
                    predicate = predicate.And(p => p.DesignationId == employee.DesignationId);
                if (employee.DepartmentId > 0)
                    predicate = predicate.And(p => p.DepartmentId == employee.DepartmentId);


            }

            var result = _redeeppitory.Query(predicate)
                .Include(s => s.UserInfo)
                .Select();
            return result
                .Select(s => new ShortMessageDetail() { UserInfoId = s.UserInfo.Id, MobileNumber = s.UserInfo.ContactNumber1, UserInfoName = s.UserInfo.Name });
        }
    }

}
