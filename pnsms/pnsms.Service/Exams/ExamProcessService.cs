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
   
    public interface IExamProcessService
    {
        IEnumerable<ExamProcess> Get();
        ExamProcess Find(int ExamProcessID);
        IQueryable<ExamProcess> SelectQuery(string query, params object[] parameters);
        void Insert(ExamProcess entity);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, ExamProcess ExamProcess);
        void InsertRange(IEnumerable<ExamProcess> entities);
        void InsertOrUpdateGraph(ExamProcess entity);
        void InsertGraphRange(IEnumerable<ExamProcess> entities);
        void Update(ExamProcess entity);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, ExamProcess ExamProcess);
        void Delete(object id);
        void Delete(ExamProcess entity);
        IQueryFluent<ExamProcess> Query();
        IQueryFluent<ExamProcess> Query(IQueryObject<ExamProcess> queryObject);
        IQueryFluent<ExamProcess> Query(Expression<Func<ExamProcess, bool>> query);
        Task<ExamProcess> FindAsync(params object[] keyValues);
        Task<ExamProcess> FindAsync(CancellationToken cancellationToken, params object[] keyValues);
        Task<bool> DeleteAsync(params object[] keyValues);
        Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);
        IQueryable<ExamProcess> Queryable();
        ExamProcess GetExamProcessById(int id);
        IEnumerable<ExamProcess> GetExamProcess(int instituteId);
       
        ExamProcess newExamProcess(int instituteid, int currentsessionid);
    }

    public class ExamProcessService : Service<ExamProcess>, IExamProcessService
    {
        private readonly IRepositoryAsync<ExamProcess> _repository;
        private readonly IAcademicBranchService _academicBranchService;
        private readonly IAcademicClassService _academicClassService;
        private readonly IAcademicSectionService _academicSectionService;
        private readonly IAcademicSessionService _academicSessionService;
        private readonly IExamTypeService _examTypeService;
        private readonly IStudentService _studentService;


        public ExamProcessService(IRepositoryAsync<ExamProcess> repository,
            IAcademicBranchService academicBranchService,
            IAcademicClassService academicClassService,
            IAcademicSectionService academicSectionService,
            IAcademicSessionService academicSessionService,
            IExamTypeService examTypeService
            , IStudentService studentService

            )
            : base(repository)
        {
            _repository = repository;
            _academicBranchService = academicBranchService;
            _academicClassService = academicClassService;
            _academicSectionService = academicSectionService;
            _academicSessionService = academicSessionService;
            _examTypeService = examTypeService;
            _studentService = studentService;

        }
        public ExamProcess newExamProcess(int instituteid, int currentsessionid)
        {
            ExamProcess objExamProcess = new ExamProcess();
            objExamProcess.AcademicBranchList = _academicBranchService.GetKVP(instituteid);
            objExamProcess.AcademicClassList = _academicClassService.GetKVP(instituteid);
            objExamProcess.AcademicSectionList = _academicSectionService.GetKVP(instituteid);
            objExamProcess.AcademicSessionList = _academicSessionService.GetKVP(instituteid, currentsessionid, true);
            objExamProcess.ExamTypeList = _examTypeService.GetKVP(instituteid);

            return objExamProcess;



        }
      
        public IEnumerable<ExamProcess> Get()
        {
            return _repository.Query().Select();
        }

        public ExamProcess Find(int ExamProcessID)
        {
            return _repository.Query(x => x.Id == ExamProcessID).Select().SingleOrDefault();
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, ExamProcess ExamProcess)
        {


            _repository.Insert(ExamProcess);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, ExamProcess ExamProcess)
        {

            _repository.Update(ExamProcess);
            unitOfWorkAsync.SaveChanges();

        }
        public ExamProcess GetExamProcessById(int id)
        {
            return _repository.Query().Select().FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<ExamProcess> GetExamProcess(int instituteId)
        {

            return _repository.Query(x => x.InstituteId == instituteId).Select();
        }
    }
  

}
