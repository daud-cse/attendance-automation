using pnsms.Entities.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service
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


        private readonly IRepositoryAsync<RolesOfUserInfo> _repository;


        public RolesOfUserInfoService(IRepositoryAsync<RolesOfUserInfo> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<RolesOfUserInfo> GetRolesOfUserInfoByUserId(int userId)
        {
            return _repository.Query(r => r.UserInfoId == userId).Include(r => r.Role).Select();
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, RolesOfUserInfo rolesOfUserInfo)
        {
            _repository.Insert(rolesOfUserInfo);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, RolesOfUserInfo rolesOfUserInfo)
        {

            _repository.Update(rolesOfUserInfo);
            unitOfWorkAsync.SaveChanges();

        }
        public void DeleteByUserId(IUnitOfWorkAsync unitOfWorkAsync, int userId)
        {
            var roles = _repository.Query(r => r.UserInfoId == userId).Select();
            if (roles != null)
                foreach (var role in roles)
                    _repository.Delete(role);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
