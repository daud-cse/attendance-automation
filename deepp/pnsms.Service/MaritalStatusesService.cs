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


        private readonly IRepositoryAsync<MaritalStatus> _repository;


        public MaritalStatusService(IRepositoryAsync<MaritalStatus> repository)
            : base(repository)
        {
            _repository = repository;
        }


        public IEnumerable<MaritalStatus> GetMaritalStatuss(int instituteId, bool? isActive = null)
        {
            return isActive != null ? _repository.Query(m => m.InstituteId.Equals(instituteId) && m.IsActive == isActive).Select() : _repository.Query(m => m.InstituteId == instituteId).Select();
        }

        public IEnumerable<MaritalStatus> GetMaritalStatuss(bool isActive)
        {
            if (isActive)
            {
                return _repository.Query().Select().Where(d => d.IsActive.Equals(true));
            }

            return _repository.Query().Select();
        }

        public IEnumerable<MaritalStatus> GetActiveMaritalStatus()
        {
            return _repository.Query(d => d.IsActive).Select();
        }
        public MaritalStatus GetMaritalStatusById(int id)
        {
            return _repository.Query(x => x.Id == id).Select().FirstOrDefault();
        }

        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, MaritalStatus maritalStatus)
        {
            maritalStatus.LastUpdateTime = DateTime.Now;
            _repository.Insert(maritalStatus);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, MaritalStatus maritalStatus)
        {
            maritalStatus.LastUpdateTime = DateTime.Now;
            _repository.Update(maritalStatus);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
