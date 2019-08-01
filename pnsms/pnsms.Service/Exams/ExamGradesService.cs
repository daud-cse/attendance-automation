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
    public interface IExamGradeService
    {
        IEnumerable<ExamGrade> Get();
        ExamGrade Find(int ExamGradeID);
        IQueryable<ExamGrade> SelectQuery(string query, params object[] parameters);
        void Insert(ExamGrade entity);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, ExamGrade ExamGrade);
        void InsertRange(IEnumerable<ExamGrade> entities);
        void InsertOrUpdateGraph(ExamGrade entity);
        void InsertGraphRange(IEnumerable<ExamGrade> entities);
        void Update(ExamGrade entity);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, ExamGrade ExamGrade);
        void Delete(object id);
        void Delete(ExamGrade entity);
        IQueryFluent<ExamGrade> Query();
        IQueryFluent<ExamGrade> Query(IQueryObject<ExamGrade> queryObject);
        IQueryFluent<ExamGrade> Query(Expression<Func<ExamGrade, bool>> query);
        Task<ExamGrade> FindAsync(params object[] keyValues);
        Task<ExamGrade> FindAsync(CancellationToken cancellationToken, params object[] keyValues);
        Task<bool> DeleteAsync(params object[] keyValues);
        Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);
        IQueryable<ExamGrade> Queryable();
        List<KeyValuePair<int, string>> GetKVP(int instituteid);
        IEnumerable<ExamGrade> GetExamGradeByInstituteId(int instituteid);
    }

    public class ExamGradeService : Service<ExamGrade>, IExamGradeService
    {
        private readonly IRepositoryAsync<ExamGrade> _repository;

        public ExamGradeService(IRepositoryAsync<ExamGrade> repository)
            : base(repository)
        {
            _repository = repository;
        }
        public IEnumerable<ExamGrade> Get()
        {
            return _repository.Query().Select();
        }

        public ExamGrade Find(int ExamGradeID)
        {
            return _repository.Query(x => x.Id == ExamGradeID).Select().SingleOrDefault();
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, ExamGrade objExamGrade)
        {
            objExamGrade.LastUpdateDate = DateTime.Now;
            _repository.Insert(objExamGrade);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, ExamGrade objExamGrade)
        {
            objExamGrade.LastUpdateDate = DateTime.Now;
            _repository.Update(objExamGrade);
            unitOfWorkAsync.SaveChanges();

        }
        public List<KeyValuePair<int, string>> GetKVP(int instituteid)
        {
            var kvpExamGrade = new List<KeyValuePair<int, string>>();
            _repository.Query(x=>x.InstituteId==instituteid && x.IsActive==true).Select().ToList().ForEach(delegate(ExamGrade item)
            {
                kvpExamGrade.Add(new KeyValuePair<int, string>(item.Id, item.GradeName));
            });

            return kvpExamGrade;
        }
        public IEnumerable<ExamGrade> GetExamGradeByInstituteId(int instituteid)
        {
            return _repository.Query(x => x.InstituteId == instituteid).Select();
        }
    
    }

}
