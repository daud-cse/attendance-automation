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


    public interface IDistrictOrStateService : IService<DistrictOrState>
    {
        IEnumerable<DistrictOrState> GetDistrictOrStates();
        IEnumerable<DistrictOrState> GetDistrictOrStates(bool isActive=false);
        IEnumerable<DistrictOrState> GetActiveDistrictOrState();
        DistrictOrState GetDistrictOrStateById(int id);
        IEnumerable<DistrictOrState> GetActiveDistrictOrStateByinstituteId(int instituteId);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, DistrictOrState districtOrState);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, DistrictOrState districtOrState);
    }
    public class DistrictOrStateService : Service<DistrictOrState>, IDistrictOrStateService
    {


        private readonly IRepositoryAsync<DistrictOrState> _repository;


        public DistrictOrStateService(IRepositoryAsync<DistrictOrState> repository)
            : base(repository)
        {
            _repository = repository;
        }


        public IEnumerable<DistrictOrState> GetDistrictOrStates()
        {

            return _repository.Query().Select();
        }

      

        public IEnumerable<DistrictOrState> GetDistrictOrStates(bool isActive=false)
        {
            if (isActive)
            {
                return _repository.Query().Select().Where(d => d.IsActive.Equals(true));
            }

            return _repository.Query().Select();
        }

        public IEnumerable<DistrictOrState> GetActiveDistrictOrState()
        {
            return _repository.Query().Select().Where(d => d.IsActive == true);
        }
        public DistrictOrState GetDistrictOrStateById(int id)
        {
            return _repository.Query().Select().FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<DistrictOrState> GetActiveDistrictOrStateByinstituteId(int instituteId)
        {
            return _repository.Query(d => d.IsActive == true && d.Country.InstituteId == instituteId).Include(x=>x.Country).Select();
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, DistrictOrState districtOrState)
        {
            districtOrState.LastUpdateTime = DateTime.Now;
            _repository.Insert(districtOrState);
            unitOfWorkAsync.SaveChanges();

        }

        public void Update(IUnitOfWorkAsync unitOfWorkAsync, DistrictOrState districtOrState)
        {
            districtOrState.LastUpdateTime = DateTime.Now;
            _repository.Update(districtOrState);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
