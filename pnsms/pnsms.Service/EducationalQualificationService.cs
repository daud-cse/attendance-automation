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


        private readonly IRepositoryAsync<EducationalQualification> _repository;


        public EducationalQualificationService(IRepositoryAsync<EducationalQualification> repository)
            : base(repository)
        {
            _repository = repository;
        }


        public IEnumerable<EducationalQualification> GetEducationalQualifications(int instituteId)
        {

            return _repository.Query(e=>e.InstituteId==instituteId).Select();
        }

        public IEnumerable<EducationalQualification> GetEducationalQualifications(bool isActive)
        {
            if (isActive)
            {
                return _repository.Query().Select().Where(d => d.IsActive.Equals(true));
            }

            return _repository.Query().Select();
        }

        public IEnumerable<EducationalQualification> GetActiveEducationalQualification()
        {
            return _repository.Query().Select().Where(d => d.IsActive == true);
        }
        public EducationalQualification GetEducationalQualificationById(int id)
        {
            return _repository.Query().Select().FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<EducationalQualification> GetEducationalQualificationByInstituteId(int instituteId)
        {
            return _repository.Query(x => x.InstituteId == instituteId && x.IsActive == true).Select();
        }
        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="educationalQualification">The educational qualification.</param>
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, EducationalQualification educationalQualification)
        {
            educationalQualification.LastUpdateTime = DateTime.Now;
            _repository.Insert(educationalQualification);
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
            _repository.Update(educationalQualification);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
