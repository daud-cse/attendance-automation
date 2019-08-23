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
  
    public interface IBloodGroupService : IService<BloodGroup>
    {
        IEnumerable<BloodGroup> GetBloodGroups();
        IEnumerable<BloodGroup> GetBloodGroups(int instituteId, bool isActive=false);
        IEnumerable<BloodGroup> GetActiveBloodGroup();
        IEnumerable<BloodGroup> GetBloodGroupsByInstituteId(int instituteId);
        BloodGroup GetBloodGroupById(int id);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, BloodGroup bloodGroup);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, BloodGroup bloodGroup);

    }
    public class BloodGroupService : Service<BloodGroup>, IBloodGroupService
    {


        private readonly IRepositoryAsync<BloodGroup> _redeeppitory;


        public BloodGroupService(IRepositoryAsync<BloodGroup> redeeppitory)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
        }


        public IEnumerable<BloodGroup> GetBloodGroups()
        {

            return _redeeppitory.Query().Select();
        }

        public IEnumerable<BloodGroup> GetBloodGroups(int instituteId,bool isActive=false)
        {
            if (isActive)
            {
                return _redeeppitory.Query(d => d.IsActive.Equals(true) && d.InstituteId == instituteId).Select();
            }

            return _redeeppitory.Query(d =>d.InstituteId == instituteId).Select();
        }

        public IEnumerable<BloodGroup> GetActiveBloodGroup()
        {
            return _redeeppitory.Query().Select().Where(d => d.IsActive == true);
        }
        public BloodGroup GetBloodGroupById(int id)
        {
            return _redeeppitory.Query().Select().FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<BloodGroup> GetBloodGroupsByInstituteId(int instituteId)
        {
            return _redeeppitory.Query(x=>x.InstituteId==instituteId).Select().Where(x => x.InstituteId == instituteId && x.IsActive == true);
        }

        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="bloodGroup">The blood group.</param>
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, BloodGroup bloodGroup)
        {
            bloodGroup.LastUpdateTime = DateTime.Now;
            _redeeppitory.Insert(bloodGroup);
            unitOfWorkAsync.SaveChanges();

        }
        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="bloodGroup">The blood group.</param>
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, BloodGroup bloodGroup)
        {
            bloodGroup.LastUpdateTime = DateTime.Now;
            _redeeppitory.Update(bloodGroup);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
