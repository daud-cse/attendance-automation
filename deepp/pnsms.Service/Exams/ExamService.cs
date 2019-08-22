using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pnsms.Entities.Models;
using System.Threading;
using System.Linq.Expressions;
using Repository.Pattern.UnitOfWork;
using pnsms.Entities.ViewModels;
using pnsms.Service.Settings;
namespace pnsms.Service.Exams
{


    public interface IExamService
    {
        IEnumerable<Exam> Get();
        Exam Find(int ExamID);
        IQueryable<Exam> SelectQuery(string query, params object[] parameters);
        void Insert(Exam entity);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, Exam exam);
        void InsertRange(IEnumerable<Exam> entities);
        void InsertOrUpdateGraph(Exam entity);
        void InsertGraphRange(IEnumerable<Exam> entities);
        void Update(Exam entity);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, Exam exam);
        void Delete(object id);
        void Delete(Exam entity);
        IQueryFluent<Exam> Query();
        IQueryFluent<Exam> Query(IQueryObject<Exam> queryObject);
        IQueryFluent<Exam> Query(Expression<Func<Exam, bool>> query);
        Task<Exam> FindAsync(params object[] keyValues);
        Task<Exam> FindAsync(CancellationToken cancellationToken, params object[] keyValues);
        Task<bool> DeleteAsync(params object[] keyValues);
        Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);
        IQueryable<Exam> Queryable();
        Exam newExam(int instituteid, int currentsessionid, int classid);
        List<KeyValuePair<int, string>> GetKVP(int instituteid);

        List<KeyValuePair<int, string>> GetKVP(VmCommonSearch objVmCommonSearch);
        
        IEnumerable<Exam> GetExamByInstituteId(int instituteid);
    }

    public class ExamService : Service<Exam>, IExamService
    {
       private readonly IRepositoryAsync<Exam> _repository;
        private readonly IAcademicBranchService _academicBranchService;
        private readonly IAcademicClassService _academicClassService;        
        private readonly IAcademicSectionService _academicSectionService;
        private readonly IAcademicClassSectionMappingService _academicClassSectionMappingService;
        private readonly IAcademicSessionService _academicSessionService;
        private readonly IExamTypeService _examTypeService;
        

        public ExamService(IRepositoryAsync<Exam> repository,
            IAcademicBranchService academicBranchService,
            IAcademicClassService academicClassService,            
            IAcademicSectionService academicSectionService,
            IAcademicClassSectionMappingService academicClassSectionMappingService,
            IAcademicSessionService academicSessionService,
            IExamTypeService examTypeService
            
            )
            : base(repository)
        {
            _repository = repository;
            _academicBranchService = academicBranchService;
            _academicClassService = academicClassService;            
            _academicSectionService = academicSectionService;
            _academicClassSectionMappingService = academicClassSectionMappingService;
            _academicSessionService = academicSessionService;
            _examTypeService = examTypeService;
            
        }
        public IEnumerable<Exam> Get()
        {
            return _repository.Query().Select();
        }

        public Exam Find(int ExamID)
        {
            return _repository.Query(x => x.Id == ExamID)
                .Include(x=>x.ExamType)
                .Select().SingleOrDefault();
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, Exam exam)
        {

            exam.LastUpdateTime = DateTime.Now;
            _repository.Insert(exam);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, Exam exam)
        {
          
            Exam objExam = new Exam();
            objExam.Id = exam.Id; ;
            objExam.InstituteId = exam.InstituteId;
            objExam.LastUpdateTime = DateTime.Now;
            objExam.ExamTypeId = exam.ExamTypeId;
            objExam.AcademicBranchId = exam.AcademicBranchId;
            objExam.AcademicClassesSectionMapId = exam.AcademicClassesSectionMapId;
            objExam.AcademicClassesId = exam.AcademicClassesId;
            objExam.AcademicSessionId = exam.AcademicSessionId;
            objExam.TotalMarks = exam.TotalMarks;
            objExam.PassMarks = exam.PassMarks;
            objExam.AcceptMarks = exam.AcceptMarks;
            objExam.Name = exam.Name;
            objExam.IsActive = exam.IsActive;
            objExam.ExamDateFrom = exam.ExamDateFrom;
            objExam.ExamDateTo = exam.ExamDateTo;
            objExam.IsGroupExam = exam.IsGroupExam;
            objExam.ExamTime = exam.ExamTime;
            _repository.Update(objExam);
            unitOfWorkAsync.SaveChanges();

        }
        public Exam newExam(int instituteid, int currentsessionid, int classid)
        {
            Exam objExam = new Exam();
          //  objExam.objExam = new Exam();
            objExam.AcademicBranchList = _academicBranchService.GetKVP(instituteid);
            objExam.AcademicClassList = _academicClassService.GetKVP(instituteid);
            objExam.AcademicSectionList = new List<KeyValuePair<int, string>>();//_academicSectionService.GetKVP(instituteid);

            if (classid>0)
            {
               objExam.AcademicSectionList= _academicClassSectionMappingService.Getkvp(instituteid, classid);
            }
            objExam.AcademicSessionList = _academicSessionService.GetKVP(instituteid, currentsessionid, true);
            objExam.ExamTypeList = _examTypeService.GetKVP(instituteid);
            objExam.LastUpdateTime = DateTime.Now;
            //objExam.Institute = new Institute();
            objExam.ExamDateFrom = DateTime.Now;
            objExam.ExamDateTo = DateTime.Now;
            objExam.AcceptMarks = objExam.AcceptMarks ?? 0;
            objExam.AcceptMarks = objExam.PassMarks ?? 0;
            objExam.ExamTime = "";
            objExam.ExamTypeId = 0;
            objExam.AcademicBranchId = 0;
            objExam.AcademicClassesId = 0;
            objExam.AcademicClassesSectionMapId = 0;
            objExam.Id = 0;
            objExam.HighestMarks = objExam.HighestMarks ?? 0;
            objExam.Name = "";
            return objExam;
            


        }
        public List<KeyValuePair<int, string>> GetKVP(int instituteid)
        {
            var kvpExam = new List<KeyValuePair<int, string>>();
            _repository.Query(x => x.InstituteId == instituteid).Select().ToList().ForEach(delegate(Exam item)
            {
                kvpExam.Add(new KeyValuePair<int, string>(item.Id, item.Name));
            });

            return kvpExam;
        }
        public List<KeyValuePair<int, string>> GetKVP(VmCommonSearch objVmCommonSearch)
        {
              var kvpExam = new List<KeyValuePair<int, string>>();
              _repository.Query(x => x.InstituteId == objVmCommonSearch.InstituteId && x.IsActive==true
                  && x.AcademicClassesId == objVmCommonSearch.AcademicClassesId
                  && x.AcademicClassesSectionMapId == objVmCommonSearch.AcademicClassesSectionMapId
                  && x.ExamTypeId == objVmCommonSearch.ExamTypeId
                  ).Select().ToList().ForEach(delegate(Exam item)
            {
                kvpExam.Add(new KeyValuePair<int, string>(item.Id, item.Name));
            });

            return kvpExam;
        }
        public IEnumerable<Exam> GetExamByInstituteId(int instituteid)
        {
            return _repository.Query(x => x.InstituteId == instituteid)
                .Include(x=>x.ExamType)
                .Include(x=>x.AcademicBranch)
                .Include(x => x.AcademicClass)
                .Include(x => x.AcademicClassSectionMapping)                
                
                .Select();
        }
    }


}
