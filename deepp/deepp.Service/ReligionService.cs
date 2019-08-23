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


    public interface IReligionService : IService<Religion>
    {
        IEnumerable<Religion> GetReligions(int instituteId);
        IEnumerable<Religion> GetReligions(bool isActive);
        IEnumerable<Religion> GetActiveReligion();
        Religion GetReligionById(int id);
        IEnumerable<Religion> GetReligionByInstituteId(int instituteId);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, Religion religion);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, Religion religion);
    }
    public class ReligionService : Service<Religion>, IReligionService
    {


        private readonly IRepositoryAsync<Religion> _redeeppitory;


        public ReligionService(IRepositoryAsync<Religion> redeeppitory)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
        }


        public IEnumerable<Religion> GetReligions(int instituteId)
        {

            return _redeeppitory.Query(r=>r.InstituteId==instituteId).Select();
        }

        public IEnumerable<Religion> GetReligions(bool isActive)
        {
            if (isActive)
            {
                return _redeeppitory.Query().Select().Where(d => d.IsActive.Equals(true));
            }

            return _redeeppitory.Query().Select();
        }

        public IEnumerable<Religion> GetActiveReligion()
        {
            return _redeeppitory.Query().Select().Where(d => d.IsActive == true);
        }
        public Religion GetReligionById(int id)
        {
            return _redeeppitory.Query().Select().FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<Religion> GetReligionByInstituteId(int instituteId)
        {
            return _redeeppitory.Query(x=>x.InstituteId==instituteId).Select().Where(x => x.InstituteId == instituteId && x.IsActive == true);
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, Religion religion)
        {
            religion.LastUpdateTime = DateTime.Now;
            _redeeppitory.Insert(religion);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, Religion religion)
        {
            religion.LastUpdateTime = DateTime.Now;
            _redeeppitory.Update(religion);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
