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

    public interface IAcademicBranchService : IService<AcademicBranch>
    {
        IEnumerable<AcademicBranch> GetAcademicBranchs(int instituteId);
        IEnumerable<AcademicBranch> GetActiveAcademicBranch();
        AcademicBranch GetAcademicBranchById(int id);
        IEnumerable<AcademicBranch> GetAcademicBranchs(bool isActive);
        List<KeyValuePair<int, string>> GetKVP(int instituteId);
        IEnumerable<AcademicBranch> GetAcademicBranchsByInstituteId(int instituteId);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, AcademicBranch academicBranch);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, AcademicBranch academicBranch);
    }
    public class AcademicBranchService : Service<AcademicBranch>, IAcademicBranchService
    {


        private readonly IRepositoryAsync<AcademicBranch> _repository;


        public AcademicBranchService(IRepositoryAsync<AcademicBranch> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<AcademicBranch> GetAcademicBranchsByInstituteId(int instituteId)
        {
            return _repository.Query().Select().Where(x => x.InstituteId == instituteId && x.IsActive==true);
        }

        public IEnumerable<AcademicBranch> GetAcademicBranchs(int instituteId)
        {

            return _repository.Query(b=>b.InstituteId==instituteId).Select();
        }

        public IEnumerable<AcademicBranch> GetAcademicBranchs(bool isActive)
        {
            if (isActive)
            {
                return _repository.Query().Select().Where(d => d.IsActive.Equals(true));
            }

            return _repository.Query().Select();
        } 
       
        public IEnumerable<AcademicBranch> GetActiveAcademicBranch()
        {
            return _repository.Query().Select().Where(d => d.IsActive == true);
        }
        public AcademicBranch GetAcademicBranchById(int id)
        {
            return _repository.Query().Select().FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="academicBranch">The academic branch.</param>
        public  void Insert(IUnitOfWorkAsync unitOfWorkAsync, AcademicBranch academicBranch)
        {
            academicBranch.LastUpdateTime = DateTime.Now;
           _repository.Insert(academicBranch);
           unitOfWorkAsync.SaveChanges();

       }
        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="academicBranch">The academic branch.</param>
       public  void Update(IUnitOfWorkAsync unitOfWorkAsync, AcademicBranch academicBranch)
       {
           academicBranch.LastUpdateTime = DateTime.Now;
           _repository.Update(academicBranch);
           unitOfWorkAsync.SaveChanges();

       }
       /// <summary>
       /// anirban
       /// </summary>
       /// <returns></returns>
       public List<KeyValuePair<int, string>> GetKVP(int instituteId)
       {
           var data = _repository.Query(c => c.IsActive && c.InstituteId == instituteId).Select().ToList();
           var branchList = new List<KeyValuePair<int, string>>();
           data.ForEach(c => branchList.Add(new KeyValuePair<int, string>(c.Id, c.Name)));

           return branchList;
       }

        
    }
}
