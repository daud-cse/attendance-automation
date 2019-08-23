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
        private readonly IRepositoryAsync<Gender> _redeeppitory;

        public GenderService(IRepositoryAsync<Gender> redeeppitory)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
            
        }


        public IEnumerable<Gender> GetGenders(int instituteId)
        {

            return _redeeppitory.Query(g => g.InstituteId == instituteId).Select();
        }

        public IEnumerable<Gender> GetGenders(bool isActive)
        {
            if (isActive)
            {
                return _redeeppitory.Query().Select().Where(d => d.IsActive.Equals(true));
            }

            return _redeeppitory.Query().Select();
        }

        public IEnumerable<Gender> GetActiveGender()
        {
            return _redeeppitory.Query().Select().Where(d => d.IsActive == true);
        }
        public Gender GetGenderById(int id)
        {
            return _redeeppitory.Query().Select().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Gender> GetGenderByInstituteId(int instituteId)
        {
            return _redeeppitory.Query(x=>x.InstituteId== instituteId).Select().Where(x => x.InstituteId == instituteId && x.IsActive == true);
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, Gender gender)
        {
            gender.LastUpdateTime = DateTime.Now;
            _redeeppitory.Insert(gender);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, Gender gender)
        {
            gender.LastUpdateTime = DateTime.Now;
            _redeeppitory.Update(gender);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
