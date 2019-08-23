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


        private readonly IRepositoryAsync<Profession> _redeeppitory;


        public ProfessionService(IRepositoryAsync<Profession> redeeppitory)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
        }


        public IEnumerable<Profession> GetProfessions(int instituteId)
        {

            return _redeeppitory.Query(p=>p.InstituteId== instituteId).Select();
        }

        public IEnumerable<Profession> GetProfessions(bool isActive)
        {
            if (isActive)
            {
                return _redeeppitory.Query().Select().Where(d => d.IsActive.Equals(true));
            }

            return _redeeppitory.Query().Select();
        }

        public IEnumerable<Profession> GetActiveProfession()
        {
            return _redeeppitory.Query().Select().Where(d => d.IsActive == true);
        }
        public Profession GetProfessionById(int id)
        {
            return _redeeppitory.Query().Select().FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<Profession> GetProfessionByInstituteId(int instituteId)
        {
            return _redeeppitory.Query(x => x.InstituteId == instituteId && x.IsActive == true).Select();
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, Profession profession)
        {
            profession.LastUpdateTime = DateTime.Now;
            _redeeppitory.Insert(profession);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, Profession profession)
        {
            profession.LastUpdateTime = DateTime.Now;
            _redeeppitory.Update(profession);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
