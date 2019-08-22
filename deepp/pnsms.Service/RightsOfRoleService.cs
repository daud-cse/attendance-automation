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


    public interface IRightsOfRoleService : IService<RightsOfRole>
    {
        IEnumerable<Right> GetRightsOfRolesByRoleId(int roleId);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, RightsOfRole rightsOfRole);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, RightsOfRole rightsOfRole);
        void DeleteByRoleId(IUnitOfWorkAsync unitOfWorkAsync, int roleId);
    }
    public class RightsOfRoleService : Service<RightsOfRole>, IRightsOfRoleService
    {


        private readonly IRepositoryAsync<RightsOfRole> _repository;


        public RightsOfRoleService(IRepositoryAsync<RightsOfRole> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<Right> GetRightsOfRolesByRoleId(int roleId)
        {
           return _repository.Query(r => r.RoleId == roleId).Include(r=>r.Right).Select(r=>r.Right);
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, RightsOfRole rightsOfRole)
        {
            _repository.Insert(rightsOfRole);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, RightsOfRole rightsOfRole)
        {

            _repository.Update(rightsOfRole);
            unitOfWorkAsync.SaveChanges();

        }
        public void DeleteByRoleId(IUnitOfWorkAsync unitOfWorkAsync, int roleId)
        {
            var rights = _repository.Query(r => r.RoleId == roleId).Select();
            if (rights != null)
                foreach (var rightsOfRole in rights)
                    _repository.Delete(rightsOfRole);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
