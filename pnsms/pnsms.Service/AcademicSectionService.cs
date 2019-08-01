using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pnsms.Entities.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;

namespace pnsms.Service
{
    public interface IAcademicSectionService : IService<AcademicSection>
    {

        IEnumerable<AcademicSection> GetAcademicSections(int institueId);

        AcademicSection GetAcademicSectionById(int id);
        IEnumerable<AcademicSection> GetAcademicSectionByInstituteId(int instituteId);
        List<KeyValuePair<int, string>> GetKVP(int instituteId);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, AcademicSection academicSection);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, AcademicSection academicSection);
    }

    public class AcademicSectionService : Service<AcademicSection>, IAcademicSectionService
    {
        private readonly IRepositoryAsync<AcademicSection> _repository;

        public AcademicSectionService(IRepositoryAsync<AcademicSection> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<AcademicSection> GetAcademicSections(int institueId)
        {

            return _repository.Query(a => a.InstituteId == institueId).Select();

        }

        public AcademicSection GetAcademicSectionById(int id)
        {
            return _repository.Query().Select().FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<AcademicSection> GetAcademicSectionByInstituteId(int instituteId)
        {
            return _repository.Query().Select().Where(x => x.IsActive == true && x.InstituteId == instituteId);
        }
        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="academicSection">The academic section.</param>
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, AcademicSection academicSection)
        {
            academicSection.LastUpdateTime = DateTime.Now;
            _repository.Insert(academicSection);
            unitOfWorkAsync.SaveChanges();

        }

        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="academicSection">The academic section.</param>
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, AcademicSection academicSection)
        {
            academicSection.LastUpdateTime = DateTime.Now;
            _repository.Update(academicSection);
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
            var sectionList = new List<KeyValuePair<int, string>>();
            data.ForEach(c => sectionList.Add(new KeyValuePair<int, string>(c.Id, c.Name)));

            return sectionList;
        }
    }


}
