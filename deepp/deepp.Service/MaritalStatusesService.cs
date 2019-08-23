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


    public interface IMaritalStatusService : IService<MaritalStatus>
    {
        IEnumerable<MaritalStatus> GetMaritalStatuss(int instituteId, bool? isActive=null);
        IEnumerable<MaritalStatus> GetActiveMaritalStatus();
        MaritalStatus GetMaritalStatusById(int id);
        IEnumerable<MaritalStatus> GetMaritalStatuss(bool isActive);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, MaritalStatus maritalStatus);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, MaritalStatus maritalStatus);
    }
    public class MaritalStatusService : Service<MaritalStatus>, IMaritalStatusService
    {


        private readonly IRepositoryAsync<MaritalStatus> _redeeppitory;


        public MaritalStatusService(IRepositoryAsync<MaritalStatus> redeeppitory)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
        }


        public IEnumerable<MaritalStatus> GetMaritalStatuss(int instituteId, bool? isActive = null)
        {
            return isActive != null ? _redeeppitory.Query(m => m.InstituteId.Equals(instituteId) && m.IsActive == isActive).Select() : _redeeppitory.Query(m => m.InstituteId == instituteId).Select();
        }

        public IEnumerable<MaritalStatus> GetMaritalStatuss(bool isActive)
        {
            if (isActive)
            {
                return _redeeppitory.Query().Select().Where(d => d.IsActive.Equals(true));
            }

            return _redeeppitory.Query().Select();
        }

        public IEnumerable<MaritalStatus> GetActiveMaritalStatus()
        {
            return _redeeppitory.Query(d => d.IsActive).Select();
        }
        public MaritalStatus GetMaritalStatusById(int id)
        {
            return _redeeppitory.Query(x => x.Id == id).Select().FirstOrDefault();
        }

        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, MaritalStatus maritalStatus)
        {
            maritalStatus.LastUpdateTime = DateTime.Now;
            _redeeppitory.Insert(maritalStatus);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, MaritalStatus maritalStatus)
        {
            maritalStatus.LastUpdateTime = DateTime.Now;
            _redeeppitory.Update(maritalStatus);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
