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

    public interface IAcademicShiftService : IService<AcademicShift>
    {
        IEnumerable<AcademicShift> GetAcademicShifts(int instituteId);
        IEnumerable<AcademicShift> GetAcademicShifts(bool isActive);
        IEnumerable<AcademicShift> GetActiveAcademicShift();
        AcademicShift GetAcademicShiftById(int id);
        IEnumerable<AcademicShift> GetActiveAcademicShiftByInstituteId(int instituteId);
        List<KeyValuePair<int, string>> GetKVP(int institutionId);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, AcademicShift academicShift);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, AcademicShift academicShift);

    }
    public class AcademicShiftService : Service<AcademicShift>, IAcademicShiftService
    {


        private readonly IRepositoryAsync<AcademicShift> _repository;


        public AcademicShiftService(IRepositoryAsync<AcademicShift> repository)
            : base(repository)
        {
            _repository = repository;
        }


        public IEnumerable<AcademicShift> GetAcademicShifts(int instituteId)
        {

            return _repository.Query(s=>s.InstituteId==instituteId).Select();
        }

        public IEnumerable<AcademicShift> GetAcademicShifts(bool isActive)
        {
            if (isActive)
            {
                return _repository.Query().Select().Where(d => d.IsActive.Equals(true));
            }

            return _repository.Query().Select();
        }

        public IEnumerable<AcademicShift> GetActiveAcademicShift()
        {
            return _repository.Query().Select().Where(d => d.IsActive == true);
        }
        public AcademicShift GetAcademicShiftById(int id)
        {
            return _repository.Query().Select().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<AcademicShift> GetActiveAcademicShiftByInstituteId(int instituteId)
        {
            return _repository.Query().Select().Where(d => d.IsActive == true && d.InstituteId == instituteId);
        }

        public List<KeyValuePair<int, string>> GetKVP(int institutionId)
        {
            var data = _repository.Query(c => c.IsActive && c.InstituteId == institutionId).Select().ToList();
            var shiftList = new List<KeyValuePair<int, string>>();
            data.ForEach(c => shiftList.Add(new KeyValuePair<int, string>(c.Id, c.Name)));

            return shiftList;
        }

        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, AcademicShift academicShift)
        {
            academicShift.LastUpdateTime = DateTime.Now;
            _repository.Insert(academicShift);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, AcademicShift academicShift)
        {
            academicShift.LastUpdateTime = DateTime.Now;
            _repository.Update(academicShift);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
