using deepp.Entities.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Service.Settings
{
  
    public interface IAcademicPeriodService : IService<AcademicPeriod>
    {
        IEnumerable<AcademicPeriod> GetAcademicPeriod(int instituteId);
        IEnumerable<AcademicPeriod> GetActiveAcademicPeriod();
        IEnumerable<AcademicPeriod> GetAcademicPeriod(bool IsActive);
        AcademicPeriod GetAcademicPeriodById(int id);
        IEnumerable<AcademicPeriod> GetAcademicPeriodesByInstituteId(int instituteId);
        List<KeyValuePair<int, string>> GetKVP(int instituteId);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, AcademicPeriod AcademicPeriod);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, AcademicPeriod AcademicPeriod);
    }
    public class AcademicPeriodService : Service<AcademicPeriod>, IAcademicPeriodService
    {
        private readonly IRepositoryAsync<AcademicPeriod> _redeeppitory;

        public AcademicPeriodService(IRepositoryAsync<AcademicPeriod> redeeppitory)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
        }


        public IEnumerable<AcademicPeriod> GetAcademicPeriod(int instituteId)
        {

            return _redeeppitory.Query(x => x.InstituteId == instituteId).Select();
        }
        public IEnumerable<AcademicPeriod> GetAcademicPeriod(bool isActive = false)
        {
            if (isActive)
            {
                return _redeeppitory.Query().Select().Where(d => d.IsActive.Equals(true));
            }

            return _redeeppitory.Query().Select();
        }
        public IEnumerable<AcademicPeriod> GetActiveAcademicPeriod()
        {
            return _redeeppitory.Query().Select().Where(d => d.IsActive == true);
        }
        public AcademicPeriod GetAcademicPeriodById(int id)
        {
            return _redeeppitory.Query().Select().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<AcademicPeriod> GetAcademicPeriodesByInstituteId(int instituteId)
        {
            return _redeeppitory.Query().Select().Where(x => x.InstituteId == instituteId && x.IsActive == true);
        }

        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, AcademicPeriod AcademicPeriod)
        {
            AcademicPeriod.LastUpdateTime = DateTime.Now;
            _redeeppitory.Insert(AcademicPeriod);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, AcademicPeriod AcademicPeriod)
        {
            AcademicPeriod.LastUpdateTime = DateTime.Now;
            _redeeppitory.Update(AcademicPeriod);
            unitOfWorkAsync.SaveChanges();

        }
        /// <summary>
        /// anirban
        /// </summary>
        /// <param name="instituteId"></param>
        /// <returns></returns>
        public List<KeyValuePair<int, string>> GetKVP(int instituteId)
        {
            var data = _redeeppitory.Query(c => c.IsActive && c.InstituteId == instituteId).Select().ToList();
            var classList = new List<KeyValuePair<int, string>>();
            data.ForEach(c => classList.Add(new KeyValuePair<int, string>(c.Id, c.Name)));

            return classList;
        }

    }
}
