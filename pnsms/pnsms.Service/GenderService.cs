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
  

    public interface IGenderService : IService<Gender>
    {
        IEnumerable<Gender> GetGenders(int instituteId);
        IEnumerable<Gender> GetGenders(bool isActive);
        IEnumerable<Gender> GetActiveGender();
        Gender GetGenderById(int id);
        IEnumerable<Gender> GetGenderByInstituteId(int instituteId);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, Gender gender);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, Gender gender);
    }
    public class GenderService : Service<Gender>, IGenderService
    {
        private readonly IRepositoryAsync<Gender> _repository;

        public GenderService(IRepositoryAsync<Gender> repository)
            : base(repository)
        {
            _repository = repository;
            
        }


        public IEnumerable<Gender> GetGenders(int instituteId)
        {

            return _repository.Query(g => g.InstituteId == instituteId).Select();
        }

        public IEnumerable<Gender> GetGenders(bool isActive)
        {
            if (isActive)
            {
                return _repository.Query().Select().Where(d => d.IsActive.Equals(true));
            }

            return _repository.Query().Select();
        }

        public IEnumerable<Gender> GetActiveGender()
        {
            return _repository.Query().Select().Where(d => d.IsActive == true);
        }
        public Gender GetGenderById(int id)
        {
            return _repository.Query().Select().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Gender> GetGenderByInstituteId(int instituteId)
        {
            return _repository.Query(x=>x.InstituteId== instituteId).Select().Where(x => x.InstituteId == instituteId && x.IsActive == true);
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, Gender gender)
        {
            gender.LastUpdateTime = DateTime.Now;
            _repository.Insert(gender);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, Gender gender)
        {
            gender.LastUpdateTime = DateTime.Now;
            _repository.Update(gender);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
