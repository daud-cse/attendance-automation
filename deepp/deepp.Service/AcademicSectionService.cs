using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using deepp.Entities.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;

namespace deepp.Service
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
        private readonly IRepositoryAsync<AcademicSection> _redeeppitory;

        public AcademicSectionService(IRepositoryAsync<AcademicSection> redeeppitory)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
        }

        public IEnumerable<AcademicSection> GetAcademicSections(int institueId)
        {

            return _redeeppitory.Query(a => a.InstituteId == institueId).Select();

        }

        public AcademicSection GetAcademicSectionById(int id)
        {
            return _redeeppitory.Query().Select().FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<AcademicSection> GetAcademicSectionByInstituteId(int instituteId)
        {
            return _redeeppitory.Query().Select().Where(x => x.IsActive == true && x.InstituteId == instituteId);
        }
        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="academicSection">The academic section.</param>
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, AcademicSection academicSection)
        {
            academicSection.LastUpdateTime = DateTime.Now;
            _redeeppitory.Insert(academicSection);
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
            _redeeppitory.Update(academicSection);
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
            var sectionList = new List<KeyValuePair<int, string>>();
            data.ForEach(c => sectionList.Add(new KeyValuePair<int, string>(c.Id, c.Name)));

            return sectionList;
        }
    }


}
