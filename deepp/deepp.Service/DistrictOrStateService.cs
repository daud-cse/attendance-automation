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


        private readonly IRepositoryAsync<DistrictOrState> _redeeppitory;


        public DistrictOrStateService(IRepositoryAsync<DistrictOrState> redeeppitory)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
        }


        public IEnumerable<DistrictOrState> GetDistrictOrStates()
        {

            return _redeeppitory.Query().Select();
        }

      

        public IEnumerable<DistrictOrState> GetDistrictOrStates(bool isActive=false)
        {
            if (isActive)
            {
                return _redeeppitory.Query().Select().Where(d => d.IsActive.Equals(true));
            }

            return _redeeppitory.Query().Select();
        }

        public IEnumerable<DistrictOrState> GetActiveDistrictOrState()
        {
            return _redeeppitory.Query().Select().Where(d => d.IsActive == true);
        }
        public DistrictOrState GetDistrictOrStateById(int id)
        {
            return _redeeppitory.Query().Select().FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<DistrictOrState> GetActiveDistrictOrStateByinstituteId(int instituteId)
        {
            return _redeeppitory.Query(d => d.IsActive == true && d.Country.InstituteId == instituteId).Include(x=>x.Country).Select();
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, DistrictOrState districtOrState)
        {
            districtOrState.LastUpdateTime = DateTime.Now;
            _redeeppitory.Insert(districtOrState);
            unitOfWorkAsync.SaveChanges();

        }

        public void Update(IUnitOfWorkAsync unitOfWorkAsync, DistrictOrState districtOrState)
        {
            districtOrState.LastUpdateTime = DateTime.Now;
            _redeeppitory.Update(districtOrState);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
