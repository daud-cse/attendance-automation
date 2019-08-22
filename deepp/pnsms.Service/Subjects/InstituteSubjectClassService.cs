using pnsms.Entities.Models;
using pnsms.Entities.ViewModels;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service.Subjects
{
   
    public interface IInstituteSubjectClassService : IService<InstituteSubjectClass>
    {
        IEnumerable<InstituteSubjectClass> GetInstituteSubjectClasss(int instituteId);
        IEnumerable<InstituteSubjectClass> GetActiveInstituteSubjectClass();
        InstituteSubjectClass GetInstituteSubjectClassById(int id);
        IEnumerable<InstituteSubjectClass> GetInstituteSubjectClasss(bool isActive);
        List<KeyValuePair<int, string>> GetKVP(int instituteId);
        List<KeyValuePair<int, string>> GetKVP(VmCommonSearch objVmCommonSearch);
        IEnumerable<InstituteSubjectClass> GetInstituteSubjectClasssByInstituteId(int instituteId);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, InstituteSubjectClass InstituteSubjectClass);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, InstituteSubjectClass InstituteSubjectClass);
    }
    public class InstituteSubjectClassService : Service<InstituteSubjectClass>, IInstituteSubjectClassService
    {


        private readonly IRepositoryAsync<InstituteSubjectClass> _repository;


        public InstituteSubjectClassService(IRepositoryAsync<InstituteSubjectClass> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<InstituteSubjectClass> GetInstituteSubjectClasssByInstituteId(int instituteId)
        {
            return _repository.Query(x => x.InstituteSubject.InstituteId == instituteId && x.IsActive == true).Select();
        }

        public IEnumerable<InstituteSubjectClass> GetInstituteSubjectClasss(int instituteId)
        {

            return _repository.Query(x => x.InstituteSubject.InstituteId == instituteId && x.IsActive == true).Select();
        }

        public IEnumerable<InstituteSubjectClass> GetInstituteSubjectClasss(bool isActive)
        {
            if (isActive)
            {
                return _repository.Query().Select().Where(d => d.IsActive.Equals(true));
            }

            return _repository.Query().Select();
        }

        public IEnumerable<InstituteSubjectClass> GetActiveInstituteSubjectClass()
        {
            return _repository.Query().Select().Where(d => d.IsActive == true);
        }
        public InstituteSubjectClass GetInstituteSubjectClassById(int id)
        {
            return _repository.Query().Include(x=>x.InstituteSubject).Select().FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="InstituteSubjectClass">The academic branch.</param>
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, InstituteSubjectClass InstituteSubjectClass)
        {
            //InstituteSubjectClass.CreateDate = DateTime.Now;
            _repository.Insert(InstituteSubjectClass);
            unitOfWorkAsync.SaveChanges();

        }
        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="InstituteSubjectClass">The academic branch.</param>
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, InstituteSubjectClass InstituteSubjectClass)
        {
           // InstituteSubjectClass.CreateDate = DateTime.Now;
            _repository.Update(InstituteSubjectClass);
            unitOfWorkAsync.SaveChanges();

        }
        /// <summary>
        /// anirban
        /// </summary>
        /// <returns></returns>
        public List<KeyValuePair<int, string>> GetKVP(int instituteId)
        {
            var data = _repository.Query(c => c.IsActive && c.InstituteSubject.InstituteId == instituteId).Select().ToList();
            var kvpSubjectList = new List<KeyValuePair<int, string>>();
            data.ForEach(c => kvpSubjectList.Add(new KeyValuePair<int, string>(c.Id, c.InstituteSubject.Name)));

            return kvpSubjectList;
        }
        public List<KeyValuePair<int, string>> GetKVP(VmCommonSearch objVmCommonSearch)
        {
            var data = _repository.Query(c => c.IsActive && c.InstituteSubject.InstituteId == objVmCommonSearch.InstituteId && c.ClassId==objVmCommonSearch.AcademicClassesId).Include(x=>x.InstituteSubject)
                .Select().ToList();
            var kvpSubjectList = new List<KeyValuePair<int, string>>();
            data.ForEach(c => kvpSubjectList.Add(new KeyValuePair<int, string>(c.Id, c.InstituteSubject.Name)));

            return kvpSubjectList;
        }
        

    }
}
