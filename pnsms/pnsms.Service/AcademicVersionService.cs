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
  

    public interface IAcademicVersionService : IService<AcademicVersion>
    {
        IEnumerable<AcademicVersion> GetAcademicVersions(int instituteId);
        IEnumerable<AcademicVersion> GetAcademicVersions(bool isActive);
        IEnumerable<AcademicVersion> GetActiveAcademicVersion();
        AcademicVersion GetAcademicVersionById(int id);
        List<KeyValuePair<int, string>> GetKVP(int instituteId);
        IEnumerable<AcademicVersion> GetActiveAcademicVersionByInstituteId(int instituteId);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, AcademicVersion academicVersion);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, AcademicVersion academicVersion);
    }
    public class AcademicVersionService : Service<AcademicVersion>, IAcademicVersionService
    {


        private readonly IRepositoryAsync<AcademicVersion> _repository;


        public AcademicVersionService(IRepositoryAsync<AcademicVersion> repository)
            : base(repository)
        {
            _repository = repository;
        }


        public IEnumerable<AcademicVersion> GetAcademicVersions(int instituteId)
        {

            return _repository.Query(v => v.InstituteId == instituteId).Select();
        }

        public IEnumerable<AcademicVersion> GetAcademicVersions(bool isActive)
        {
            if (isActive)
            {
                return _repository.Query().Select().Where(d => d.IsActive.Equals(true));
            }

            return _repository.Query().Select();
        }

        public IEnumerable<AcademicVersion> GetActiveAcademicVersion()
        {
            return _repository.Query().Select().Where(d => d.IsActive == true);
        }

        public IEnumerable<AcademicVersion> GetActiveAcademicVersionByInstituteId(int instituteId)
        {
            return _repository.Query().Select().Where(d => d.IsActive == true && d.InstituteId == instituteId);
        }

        public AcademicVersion GetAcademicVersionById(int id)
        {
            return _repository.Query().Select().FirstOrDefault(x => x.Id == id);
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, AcademicVersion academicVersion)
        {
            academicVersion.LastUpdateTime = DateTime.Now;
            _repository.Insert(academicVersion);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, AcademicVersion academicVersion)
        {
            academicVersion.LastUpdateTime = DateTime.Now;
            _repository.Update(academicVersion);
            unitOfWorkAsync.SaveChanges();

        }
        public List<KeyValuePair<int, string>> GetKVP(int instituteId)
        {
            var data = _repository.Query(c => c.IsActive && c.InstituteId == instituteId).Select().ToList();
            var versionList = new List<KeyValuePair<int, string>>();
            data.ForEach(c => versionList.Add(new KeyValuePair<int, string>(c.Id, c.Name)));

            return versionList;
        }
    }
}
