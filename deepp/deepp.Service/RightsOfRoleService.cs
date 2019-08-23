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


    public interface IRightsOfRoleService : IService<RightsOfRole>
    {
        IEnumerable<Right> GetRightsOfRolesByRoleId(int roleId);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, RightsOfRole rightsOfRole);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, RightsOfRole rightsOfRole);
        void DeleteByRoleId(IUnitOfWorkAsync unitOfWorkAsync, int roleId);
    }
    public class RightsOfRoleService : Service<RightsOfRole>, IRightsOfRoleService
    {


        private readonly IRepositoryAsync<RightsOfRole> _redeeppitory;


        public RightsOfRoleService(IRepositoryAsync<RightsOfRole> redeeppitory)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
        }

        public IEnumerable<Right> GetRightsOfRolesByRoleId(int roleId)
        {
           return _redeeppitory.Query(r => r.RoleId == roleId).Include(r=>r.Right).Select(r=>r.Right);
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, RightsOfRole rightsOfRole)
        {
            _redeeppitory.Insert(rightsOfRole);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, RightsOfRole rightsOfRole)
        {

            _redeeppitory.Update(rightsOfRole);
            unitOfWorkAsync.SaveChanges();

        }
        public void DeleteByRoleId(IUnitOfWorkAsync unitOfWorkAsync, int roleId)
        {
            var rights = _redeeppitory.Query(r => r.RoleId == roleId).Select();
            if (rights != null)
                foreach (var rightsOfRole in rights)
                    _redeeppitory.Delete(rightsOfRole);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
