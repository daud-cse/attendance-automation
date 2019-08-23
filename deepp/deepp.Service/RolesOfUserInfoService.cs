using deepp.Entities.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Service
{


    public interface IRolesOfUserInfoService : IService<RolesOfUserInfo>
    {
        IEnumerable<RolesOfUserInfo> GetRolesOfUserInfoByUserId(int userId);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, RolesOfUserInfo rolesOfUserInfo);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, RolesOfUserInfo rolesOfUserInfo);
        void DeleteByUserId(IUnitOfWorkAsync unitOfWorkAsync, int userId);
    }
    public class RolesOfUserInfoService : Service<RolesOfUserInfo>, IRolesOfUserInfoService
    {


        private readonly IRepositoryAsync<RolesOfUserInfo> _redeeppitory;


        public RolesOfUserInfoService(IRepositoryAsync<RolesOfUserInfo> redeeppitory)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
        }

        public IEnumerable<RolesOfUserInfo> GetRolesOfUserInfoByUserId(int userId)
        {
            return _redeeppitory.Query(r => r.UserInfoId == userId).Include(r => r.Role).Select();
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, RolesOfUserInfo rolesOfUserInfo)
        {
            _redeeppitory.Insert(rolesOfUserInfo);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, RolesOfUserInfo rolesOfUserInfo)
        {

            _redeeppitory.Update(rolesOfUserInfo);
            unitOfWorkAsync.SaveChanges();

        }
        public void DeleteByUserId(IUnitOfWorkAsync unitOfWorkAsync, int userId)
        {
            var roles = _redeeppitory.Query(r => r.UserInfoId == userId).Select();
            if (roles != null)
                foreach (var role in roles)
                    _redeeppitory.Delete(role);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
