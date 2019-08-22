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


        private readonly IRepositoryAsync<BloodGroup> _repository;


        public BloodGroupService(IRepositoryAsync<BloodGroup> repository)
            : base(repository)
        {
            _repository = repository;
        }


        public IEnumerable<BloodGroup> GetBloodGroups()
        {

            return _repository.Query().Select();
        }

        public IEnumerable<BloodGroup> GetBloodGroups(int instituteId,bool isActive=false)
        {
            if (isActive)
            {
                return _repository.Query(d => d.IsActive.Equals(true) && d.InstituteId == instituteId).Select();
            }

            return _repository.Query(d =>d.InstituteId == instituteId).Select();
        }

        public IEnumerable<BloodGroup> GetActiveBloodGroup()
        {
            return _repository.Query().Select().Where(d => d.IsActive == true);
        }
        public BloodGroup GetBloodGroupById(int id)
        {
            return _repository.Query().Select().FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<BloodGroup> GetBloodGroupsByInstituteId(int instituteId)
        {
            return _repository.Query(x=>x.InstituteId==instituteId).Select().Where(x => x.InstituteId == instituteId && x.IsActive == true);
        }

        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="bloodGroup">The blood group.</param>
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, BloodGroup bloodGroup)
        {
            bloodGroup.LastUpdateTime = DateTime.Now;
            _repository.Insert(bloodGroup);
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
            _repository.Update(bloodGroup);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
