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
  
    public interface IEducationalQualificationService : IService<EducationalQualification>
    {
        IEnumerable<EducationalQualification> GetEducationalQualifications(int instituteId);
        IEnumerable<EducationalQualification> GetEducationalQualifications(bool isActive);
        IEnumerable<EducationalQualification> GetActiveEducationalQualification();
        EducationalQualification GetEducationalQualificationById(int id);
        IEnumerable<EducationalQualification> GetEducationalQualificationByInstituteId(int instituteId);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, EducationalQualification educationalQualification);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, EducationalQualification educationalQualification);
    }
    public class EducationalQualificationService : Service<EducationalQualification>, IEducationalQualificationService
    {


        private readonly IRepositoryAsync<EducationalQualification> _redeeppitory;


        public EducationalQualificationService(IRepositoryAsync<EducationalQualification> redeeppitory)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
        }


        public IEnumerable<EducationalQualification> GetEducationalQualifications(int instituteId)
        {

            return _redeeppitory.Query(e=>e.InstituteId==instituteId).Select();
        }

        public IEnumerable<EducationalQualification> GetEducationalQualifications(bool isActive)
        {
            if (isActive)
            {
                return _redeeppitory.Query().Select().Where(d => d.IsActive.Equals(true));
            }

            return _redeeppitory.Query().Select();
        }

        public IEnumerable<EducationalQualification> GetActiveEducationalQualification()
        {
            return _redeeppitory.Query().Select().Where(d => d.IsActive == true);
        }
        public EducationalQualification GetEducationalQualificationById(int id)
        {
            return _redeeppitory.Query().Select().FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<EducationalQualification> GetEducationalQualificationByInstituteId(int instituteId)
        {
            return _redeeppitory.Query(x => x.InstituteId == instituteId && x.IsActive == true).Select();
        }
        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="educationalQualification">The educational qualification.</param>
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, EducationalQualification educationalQualification)
        {
            educationalQualification.LastUpdateTime = DateTime.Now;
            _redeeppitory.Insert(educationalQualification);
            unitOfWorkAsync.SaveChanges();

        }
        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="educationalQualification">The educational qualification.</param>
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, EducationalQualification educationalQualification)
        {
            educationalQualification.LastUpdateTime = DateTime.Now;
            _redeeppitory.Update(educationalQualification);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
