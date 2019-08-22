using pnsms.Entities.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service.Settings
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
        private readonly IRepositoryAsync<AcademicPeriod> _repository;

        public AcademicPeriodService(IRepositoryAsync<AcademicPeriod> repository)
            : base(repository)
        {
            _repository = repository;
        }


        public IEnumerable<AcademicPeriod> GetAcademicPeriod(int instituteId)
        {

            return _repository.Query(x => x.InstituteId == instituteId).Select();
        }
        public IEnumerable<AcademicPeriod> GetAcademicPeriod(bool isActive = false)
        {
            if (isActive)
            {
                return _repository.Query().Select().Where(d => d.IsActive.Equals(true));
            }

            return _repository.Query().Select();
        }
        public IEnumerable<AcademicPeriod> GetActiveAcademicPeriod()
        {
            return _repository.Query().Select().Where(d => d.IsActive == true);
        }
        public AcademicPeriod GetAcademicPeriodById(int id)
        {
            return _repository.Query().Select().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<AcademicPeriod> GetAcademicPeriodesByInstituteId(int instituteId)
        {
            return _repository.Query().Select().Where(x => x.InstituteId == instituteId && x.IsActive == true);
        }

        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, AcademicPeriod AcademicPeriod)
        {
            AcademicPeriod.LastUpdateTime = DateTime.Now;
            _repository.Insert(AcademicPeriod);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, AcademicPeriod AcademicPeriod)
        {
            AcademicPeriod.LastUpdateTime = DateTime.Now;
            _repository.Update(AcademicPeriod);
            unitOfWorkAsync.SaveChanges();

        }
        /// <summary>
        /// anirban
        /// </summary>
        /// <param name="instituteId"></param>
        /// <returns></returns>
        public List<KeyValuePair<int, string>> GetKVP(int instituteId)
        {
            var data = _repository.Query(c => c.IsActive && c.InstituteId == instituteId).Select().ToList();
            var classList = new List<KeyValuePair<int, string>>();
            data.ForEach(c => classList.Add(new KeyValuePair<int, string>(c.Id, c.Name)));

            return classList;
        }

    }
}
