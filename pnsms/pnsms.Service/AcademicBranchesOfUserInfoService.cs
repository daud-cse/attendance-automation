using pnsms.Entities.Models;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service
{

    /// <summary>
    /// Academic Branches Of UserInfo Service Interface
    /// </summary>
    public interface IAcademicBranchesOfUserInfoService : IService<AcademicBranchesOfUserInfo>
    {
        IEnumerable<AcademicBranchesOfUserInfo> GetAcademicBranchesOfUserInfo(int userId);
        bool DeleteAcademicBranchesOfUserInfo(int userId);
        IEnumerable<AcademicBranchesOfUserInfo> GetTeacherByBranchId(int instituteId, int branchId);
        IEnumerable<AcademicBranch> GetAcademicBranchesByUserId(int userId);

        List<KeyValuePair<int, string>> GetKVP(int userId);
        List<KeyValuePair<int, string>> GetKVPUserWise(int userId);

    }
    /// <summary>
    /// Academic Branches Of UserInfo Service
    /// </summary>
    public class AcademicBranchesOfUserInfoService : Service<AcademicBranchesOfUserInfo>, IAcademicBranchesOfUserInfoService
    {


        /// <summary>
        /// The _repository
        /// </summary>
        private readonly IRepositoryAsync<AcademicBranchesOfUserInfo> _repository;


        /// <summary>
        /// Initializes a new instance of the <see cref="AcademicBranchesOfUserInfoService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public AcademicBranchesOfUserInfoService(IRepositoryAsync<AcademicBranchesOfUserInfo> repository)
            : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets the teacher by branch identifier.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="branchId">The branch identifier.</param>
        /// <returns></returns>
        public IEnumerable<AcademicBranchesOfUserInfo> GetTeacherByBranchId(int instituteId,int branchId)
        {

           return _repository.Query()
                .Include(a => a.UserInfo).Include(a => a.UserInfo.Teacher)
                .Select().Where(b => b.UserInfo.InstituteId == instituteId && b.AcademicBranchId == branchId);
        }

        /// <summary>
        /// Gets the academic branches of user information.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IEnumerable<AcademicBranchesOfUserInfo> GetAcademicBranchesOfUserInfo(int userId)
        {

            return _repository.Query().Select().Where(a => a.UserInfoId == userId);
        }
        /// <summary>
        /// Gets the academic branches by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IEnumerable<AcademicBranch> GetAcademicBranchesByUserId(int userId)
        {
            return _repository.Query(a => a.UserInfoId == userId && a.AcademicBranch.IsActive).Include(a=>a.AcademicBranch).Select(b=>b.AcademicBranch) ;
        }

        public bool DeleteAcademicBranchesOfUserInfo(int userId)
        {

            var deleteItems = GetAcademicBranchesOfUserInfo(userId);
            if (deleteItems != null)
            {
                foreach (var item in deleteItems)
                {
                    _repository.Delete(item);
                }
                return true;
            }
            return false;
        }

        public List<KeyValuePair<int, string>> GetKVP(int userId)
        {
            var data = _repository.Query(a => a.UserInfoId == userId && a.AcademicBranch.IsActive).Include(a=>a.AcademicBranch).Select(b=>b.AcademicBranch) ;
            var barnchList = new List<KeyValuePair<int, string>>();
          // data.ForEach(c => barnchList.Add(new KeyValuePair<int, string>(c.Id, c.)));

            return barnchList;
        }

        public List<KeyValuePair<int, string>> GetKVPUserWise(int userId)
        {
            var data = _repository.Query(a => a.UserInfoId == userId && a.AcademicBranch.IsActive).Include(a => a.AcademicBranch).Select().ToList();
            var branchList = new List<KeyValuePair<int, string>>();
            data.ForEach(c => branchList.Add(new KeyValuePair<int, string>(c.AcademicBranchId, c.AcademicBranch.Name)));

            return branchList;
        }
    }
}
