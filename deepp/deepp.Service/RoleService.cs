using deepp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

namespace deepp.Service
{
    /// <summary>
    /// Role interface Service
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="isActive">The is active.</param>
        /// <returns></returns>
        IEnumerable<Role> GetRoles(int instituteId, bool? isActive = null);
        /// <summary>
        /// Gets the role by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Role GetRoleById(int id);
        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="role">The role.</param>
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, Role role);
        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="role">The role.</param>
        void Update(IUnitOfWorkAsync unitOfWorkAsync, Role role);
        /// <summary>
        /// Gets the roles for teacher.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <returns></returns>
        IEnumerable<Role> GetRolesForTeacher(int instituteId);
        /// <summary>
        /// Gets the roles for employee.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <returns></returns>
        IEnumerable<Role> GetRolesForEmployee(int instituteId);
        /// <summary>
        /// Gets the roles for guardian.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <returns></returns>
        IEnumerable<Role> GetRolesForGuardian(int instituteId);
    }
    /// <summary>
    /// Role Service
    /// </summary>
    public class RoleService : IRoleService
    {

        #region "  -  [  Constractor  ]  -  "

        private readonly IRepositoryAsync<Role> _redeeppitory;
        private readonly IRightsOfRoleService _rightsOfRoleService;

        public RoleService(IRepositoryAsync<Role> redeeppitory, IRightsOfRoleService rightsOfRoleService)
        {
            _redeeppitory = redeeppitory;
            _rightsOfRoleService = rightsOfRoleService;
        }

        #endregion

        #region "  -  [  Crud  ]  -  "

        /// <summary>
        /// Gets the Role.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="isActive">The is active.</param>
        /// <returns></returns>
        public IEnumerable<Role> GetRoles(int instituteId, bool? isActive = null)
        {
            return isActive != null
                ? _redeeppitory.Query(d => d.InstituteId.Equals(instituteId) && d.IsActive == isActive).Select()
                : _redeeppitory.Query(d => d.InstituteId == instituteId).Select();
        }
        /// <summary>
        /// Gets the roles for teacher.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <returns></returns>
        public IEnumerable<Role> GetRolesForTeacher(int instituteId)
        {
            return
                _redeeppitory.Query(d => d.InstituteId.Equals(instituteId) && d.IsActive == true && d.IsForTeacher== true)
                    .Select();

        }
        /// <summary>
        /// Gets the roles for employee.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <returns></returns>
        public IEnumerable<Role> GetRolesForEmployee(int instituteId)
        {
            return
                _redeeppitory.Query(d => d.InstituteId.Equals(instituteId) && d.IsActive == true && d.IsForEmployee == true)
                    .Select();

        }
        /// <summary>
        /// Gets the roles for guardian.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <returns></returns>
        public IEnumerable<Role> GetRolesForGuardian(int instituteId)
        {
            return
                _redeeppitory.Query(d => d.InstituteId.Equals(instituteId) && d.IsActive == true && d.IsForGuardian == true)
                    .Select();

        }
        /// <summary>
        /// Gets the Role by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Role GetRoleById(int id)
        {
            var role = _redeeppitory.Query(r => r.Id.Equals(id)).Select().SingleOrDefault();
            if (role != null)
            {
                role.RightsList = _rightsOfRoleService.GetRightsOfRolesByRoleId(id).ToList();

            }
            return role;
        }

        /// <summary>
        /// Inserts the specified unit of work asynchronous for role.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="role">The role.</param>
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, Role role)
        {
            try
            {
                unitOfWorkAsync.BeginTransaction(isolationLevel: System.Data.IsolationLevel.Unspecified);

                _redeeppitory.Insert(role);
                unitOfWorkAsync.SaveChanges();

                foreach (var r in role.RightsList)
                {

                    var rightsOfRole = new RightsOfRole() { RightId = r.Id, RoleId = role.Id };
                    _rightsOfRoleService.Insert(unitOfWorkAsync, rightsOfRole);
                }
                unitOfWorkAsync.Commit();
            }
            catch (Exception)
            {
                unitOfWorkAsync.Rollback();
                throw;

            }

        }

        /// <summary>
        /// Updates the specified unit of work asynchronous for role.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="role">The role.</param>
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, Role role)
        {
            try
            {
                unitOfWorkAsync.BeginTransaction(isolationLevel: System.Data.IsolationLevel.Unspecified);

                _redeeppitory.Update(role);
                unitOfWorkAsync.SaveChanges();
                if (role.RightsList != null)
                {
                    _rightsOfRoleService.DeleteByRoleId(unitOfWorkAsync, role.Id);
                    foreach (var r in role.RightsList)
                    {
                        var rightsOfRole = new RightsOfRole() { RightId = r.Id, RoleId = role.Id };
                        _rightsOfRoleService.Insert(unitOfWorkAsync, rightsOfRole);
                    }
                }

                unitOfWorkAsync.Commit();
            }
            catch (Exception)
            {
                unitOfWorkAsync.Rollback();
                throw;

            }
        }
        #endregion
         
    }
}
