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


    public interface INationalityService : IService<Nationality>
    {
        IEnumerable<Nationality> GetNationalitys(int instituteId);
        IEnumerable<Nationality> GetNationalitys(bool isActive);
        IEnumerable<Nationality> GetActiveNationality();
        Nationality GetNationalityById(int id);
        IEnumerable<Nationality> GetNationalityByInstituteId(int instituteId);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, Nationality nationality);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, Nationality nationality);
    }
    public class NationalityService : Service<Nationality>, INationalityService
    {


        private readonly IRepositoryAsync<Nationality> _redeeppitory;


        public NationalityService(IRepositoryAsync<Nationality> redeeppitory)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
        }


        public IEnumerable<Nationality> GetNationalitys(int instituteId)
        {

            return _redeeppitory.Query(n => n.InstituteId == instituteId).Select();
        }

        public IEnumerable<Nationality> GetNationalitys(bool isActive)
        {
            if (isActive)
            {
                return _redeeppitory.Query().Select().Where(d => d.IsActive.Equals(true));
            }

            return _redeeppitory.Query().Select();
        }

        public IEnumerable<Nationality> GetActiveNationality()
        {
            return _redeeppitory.Query().Select().Where(d => d.IsActive == true);
        }
        public Nationality GetNationalityById(int id)
        {
            return _redeeppitory.Query().Select().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Nationality> GetNationalityByInstituteId(int instituteId)
        {
            return _redeeppitory.Query(x=>x.InstituteId== instituteId).Select().Where(x => x.InstituteId == instituteId && x.IsActive == true);
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, Nationality nationality)
        {
            nationality.LastUpdateTime = DateTime.Now;
            _redeeppitory.Insert(nationality);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, Nationality nationality)
        {
            nationality.LastUpdateTime = DateTime.Now;
            _redeeppitory.Update(nationality);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
