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
    
    public interface IExamTypeWiseTabulationSheetDetailService
    {
        IEnumerable<ExamTypeWiseTabulationSheetDetail> Get();
        ExamTypeWiseTabulationSheetDetail Find(int ExamTypeWiseTabulationSheetDetailWiseTabulationSheetDetailID);
        IQueryable<ExamTypeWiseTabulationSheetDetail> SelectQuery(string query, params object[] parameters);
        void Insert(ExamTypeWiseTabulationSheetDetail entity);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, ExamTypeWiseTabulationSheetDetail ExamTypeWiseTabulationSheetDetail);
        void InsertRange(IEnumerable<ExamTypeWiseTabulationSheetDetail> entities);
        void InsertOrUpdateGraph(ExamTypeWiseTabulationSheetDetail entity);
        void InsertGraphRange(IEnumerable<ExamTypeWiseTabulationSheetDetail> entities);
        void Update(ExamTypeWiseTabulationSheetDetail entity);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, ExamTypeWiseTabulationSheetDetail ExamTypeWiseTabulationSheetDetail);
        void Delete(object id);
        void Delete(ExamTypeWiseTabulationSheetDetail entity);
        IQueryFluent<ExamTypeWiseTabulationSheetDetail> Query();
        IQueryFluent<ExamTypeWiseTabulationSheetDetail> Query(IQueryObject<ExamTypeWiseTabulationSheetDetail> queryObject);
        IQueryFluent<ExamTypeWiseTabulationSheetDetail> Query(Expression<Func<ExamTypeWiseTabulationSheetDetail, bool>> query);
        Task<ExamTypeWiseTabulationSheetDetail> FindAsync(params object[] keyValues);
        Task<ExamTypeWiseTabulationSheetDetail> FindAsync(CancellationToken cancellationToken, params object[] keyValues);
        Task<bool> DeleteAsync(params object[] keyValues);
        Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);
        IQueryable<ExamTypeWiseTabulationSheetDetail> Queryable();
        ExamTypeWiseTabulationSheetDetail GetExamTypeWiseTabulationSheetDetailById(int id);
        IEnumerable<ExamTypeWiseTabulationSheetDetail> GetExamTabulationSheetDetails(int instituteId,int examtypeid, int studentid, int sessionid);
    }

    public class ExamTypeWiseTabulationSheetDetailService : Service<ExamTypeWiseTabulationSheetDetail>, IExamTypeWiseTabulationSheetDetailService
    {
        private readonly IRepositoryAsync<ExamTypeWiseTabulationSheetDetail> _repository;

        public ExamTypeWiseTabulationSheetDetailService(IRepositoryAsync<ExamTypeWiseTabulationSheetDetail> repository)
            : base(repository)
        {
            _repository = repository;
        }
        public IEnumerable<ExamTypeWiseTabulationSheetDetail> Get()
        {
            return _repository.Query().Select();
        }

        public ExamTypeWiseTabulationSheetDetail Find(int ExamTypeWiseTabulationSheetDetailID)
        {
            return _repository.Query(x => x.Id == ExamTypeWiseTabulationSheetDetailID).Select().SingleOrDefault();
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, ExamTypeWiseTabulationSheetDetail ExamTypeWiseTabulationSheetDetail)
        {


            _repository.Insert(ExamTypeWiseTabulationSheetDetail);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, ExamTypeWiseTabulationSheetDetail ExamTypeWiseTabulationSheetDetail)
        {

            _repository.Update(ExamTypeWiseTabulationSheetDetail);
            unitOfWorkAsync.SaveChanges();

        }
        public ExamTypeWiseTabulationSheetDetail GetExamTypeWiseTabulationSheetDetailById(int id)
        {
            return _repository.Query().Select().FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<ExamTypeWiseTabulationSheetDetail> GetExamTabulationSheetDetails(int instituteId,int examtypeid, int studentid, int sessionid)
        {

            return _repository.Query(x => x.InstituteId == instituteId 
                && x.ExamTypeId == examtypeid
                && x.StudentId == studentid && x.AcademicSessionId == sessionid)
                .Include(x => x.ExamType).Select().ToList();
        }
    }
}
