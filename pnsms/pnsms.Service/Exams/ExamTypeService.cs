using pnsms.Entities.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace pnsms.Service.Exams
{
   
    public interface IExamTypeService
    {
        IEnumerable<ExamType> Get();
        ExamType Find(int ExamTypeID);
        IQueryable<ExamType> SelectQuery(string query, params object[] parameters);
        void Insert(ExamType entity);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, ExamType examType);
        void InsertRange(IEnumerable<ExamType> entities);
        void InsertOrUpdateGraph(ExamType entity);
        void InsertGraphRange(IEnumerable<ExamType> entities);
        void Update(ExamType entity);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, ExamType examType);
        void Delete(object id);
        void Delete(ExamType entity);
        IQueryFluent<ExamType> Query();
        IQueryFluent<ExamType> Query(IQueryObject<ExamType> queryObject);
        IQueryFluent<ExamType> Query(Expression<Func<ExamType, bool>> query);
        Task<ExamType> FindAsync(params object[] keyValues);
        Task<ExamType> FindAsync(CancellationToken cancellationToken, params object[] keyValues);
        Task<bool> DeleteAsync(params object[] keyValues);
        Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);
        IQueryable<ExamType> Queryable();
        ExamType GetExamTypeById(int id);
        IEnumerable<ExamType> GetExamType(int instituteId);
        List<KeyValuePair<int, string>> GetKVP(int instituteId);
    }

    public class ExamTypeService : Service<ExamType>, IExamTypeService
    {
        private readonly IRepositoryAsync<ExamType> _repository;

        public ExamTypeService(IRepositoryAsync<ExamType> repository)
            : base(repository)
        {
            _repository = repository;
        }
        public IEnumerable<ExamType> Get()
        {
            return _repository.Query().Select();
        }

        public ExamType Find(int ExamTypeID)
        {
            return _repository.Query(x => x.Id == ExamTypeID).Select().SingleOrDefault();
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, ExamType examType)
        {


            _repository.Insert(examType);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, ExamType examType)
        {
           
            _repository.Update(examType);
            unitOfWorkAsync.SaveChanges();

        }
        public ExamType GetExamTypeById(int id)
        {
            return _repository.Query().Select().FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<ExamType> GetExamType(int instituteId)
        {

            return _repository.Query(x => x.InstituteId == instituteId).Select();
        }
        public List<KeyValuePair<int, string>> GetKVP(int instituteId)
        {
            var data = _repository.Query(c => c.InstituteId == instituteId && c.IsActive==true).Select().ToList();
            var examTypeList = new List<KeyValuePair<int, string>>();
            data.ForEach(c => examTypeList.Add(new KeyValuePair<int, string>(c.Id, c.Name)));

            return examTypeList;
        }
    }
}
