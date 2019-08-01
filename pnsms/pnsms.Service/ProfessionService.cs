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
 

    public interface IProfessionService : IService<Profession>
    {
        IEnumerable<Profession> GetProfessions(int instituteId);
        IEnumerable<Profession> GetProfessions(bool isActive);
        IEnumerable<Profession> GetActiveProfession();
        Profession GetProfessionById(int id);
        IEnumerable<Profession> GetProfessionByInstituteId(int instituteId);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, Profession profession);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, Profession profession);
    }
    public class ProfessionService : Service<Profession>, IProfessionService
    {


        private readonly IRepositoryAsync<Profession> _repository;


        public ProfessionService(IRepositoryAsync<Profession> repository)
            : base(repository)
        {
            _repository = repository;
        }


        public IEnumerable<Profession> GetProfessions(int instituteId)
        {

            return _repository.Query(p=>p.InstituteId== instituteId).Select();
        }

        public IEnumerable<Profession> GetProfessions(bool isActive)
        {
            if (isActive)
            {
                return _repository.Query().Select().Where(d => d.IsActive.Equals(true));
            }

            return _repository.Query().Select();
        }

        public IEnumerable<Profession> GetActiveProfession()
        {
            return _repository.Query().Select().Where(d => d.IsActive == true);
        }
        public Profession GetProfessionById(int id)
        {
            return _repository.Query().Select().FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<Profession> GetProfessionByInstituteId(int instituteId)
        {
            return _repository.Query(x => x.InstituteId == instituteId && x.IsActive == true).Select();
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, Profession profession)
        {
            profession.LastUpdateTime = DateTime.Now;
            _repository.Insert(profession);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, Profession profession)
        {
            profession.LastUpdateTime = DateTime.Now;
            _repository.Update(profession);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
