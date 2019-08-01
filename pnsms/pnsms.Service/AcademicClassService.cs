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


    public interface IAcademicClassService : IService<AcademicClass>
    {
        IEnumerable<AcademicClass> GetAcademicClass(int instituteId);
        IEnumerable<AcademicClass> GetActiveAcademicClass();
        IEnumerable<AcademicClass> GetAcademicClass(bool IsActive);
        AcademicClass GetAcademicClassById(int id);
        IEnumerable<AcademicClass> GetAcademicClassesByInstituteId(int instituteId);
        List<KeyValuePair<int, string>> GetKVP(int instituteId);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, AcademicClass academicClass);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, AcademicClass academicClass);
    }
    public class AcademicClassService : Service<AcademicClass>, IAcademicClassService
    {
        private readonly IRepositoryAsync<AcademicClass> _repository;

        public AcademicClassService(IRepositoryAsync<AcademicClass> repository)
            : base(repository)
        {
            _repository = repository;
        }


        public IEnumerable<AcademicClass> GetAcademicClass(int instituteId)
        {

            return _repository.Query(x => x.InstituteId == instituteId).Select();
        }
        public IEnumerable<AcademicClass> GetAcademicClass(bool isActive = false)
        {
            if (isActive)
            {
                return _repository.Query().Select().Where(d => d.IsActive.Equals(true));
            }

            return _repository.Query().Select();
        }
        public IEnumerable<AcademicClass> GetActiveAcademicClass()
        {
            return _repository.Query().Select().Where(d => d.IsActive == true);
        }
        public AcademicClass GetAcademicClassById(int id)
        {
            return _repository.Query().Select().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<AcademicClass> GetAcademicClassesByInstituteId(int instituteId)
        {
            return _repository.Query().Select().Where(x => x.InstituteId == instituteId && x.IsActive == true);
        }

        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, AcademicClass academicClass)
        {
            academicClass.LastUpdateTime = DateTime.Now;
            _repository.Insert(academicClass);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, AcademicClass academicClass)
        {
            academicClass.LastUpdateTime = DateTime.Now;
            _repository.Update(academicClass);
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
