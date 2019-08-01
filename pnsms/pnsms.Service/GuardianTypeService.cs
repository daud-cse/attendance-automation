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
 

    public interface IGuardianTypeService : IService<GuardianType>
    {
        IEnumerable<GuardianType> GetGuardianTypes(int instituteId);
        IEnumerable<GuardianType> GetGuardianTypes(bool isActive);
        IEnumerable<GuardianType> GetActiveGuardianType();
        GuardianType GetGuardianTypeById(int id);
        IEnumerable<GuardianType> GetGuardianTypeByInstituteId(int instituteId);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, GuardianType guardianType);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, GuardianType academicClass);
    }
    public class GuardianTypeService : Service<GuardianType>, IGuardianTypeService
    {


        private readonly IRepositoryAsync<GuardianType> _repository;


        public GuardianTypeService(IRepositoryAsync<GuardianType> repository)
            : base(repository)
        {
            _repository = repository;
        }


        public IEnumerable<GuardianType> GetGuardianTypes(int instituteId)
        {

            return _repository.Query(g=>g.InstituteId== instituteId).Select();
        }

        public IEnumerable<GuardianType> GetGuardianTypes(bool isActive)
        {
            if (isActive)
            {
                return _repository.Query().Select().Where(d => d.IsActive.Equals(true));
            }

            return _repository.Query().Select();
        }

        public IEnumerable<GuardianType> GetActiveGuardianType()
        {
            return _repository.Query().Select().Where(d => d.IsActive == true);
        }
        public GuardianType GetGuardianTypeById(int id)
        {
            return _repository.Query().Select().FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<GuardianType> GetGuardianTypeByInstituteId(int instituteId)
        {
            return _repository.Query(x => x.InstituteId == instituteId && x.IsActive==true).Select();
        }

        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="guardianType">Type of the guardian.</param>
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, GuardianType guardianType)
        {
            guardianType.LastUpdateTime = DateTime.Now;
            _repository.Insert(guardianType);
            unitOfWorkAsync.SaveChanges();

        }
        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="guardianType">Type of the guardian.</param>
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, GuardianType guardianType)
        {
            guardianType.LastUpdateTime = DateTime.Now;
            _repository.Update(guardianType);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
