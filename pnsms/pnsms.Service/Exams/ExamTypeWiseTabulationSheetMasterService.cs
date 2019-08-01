using pnsms.Entities.Models;
using pnsms.Entities.ViewModels;
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
  
    public interface IExamTypeWiseTabulationSheetMasterService
    {
        IEnumerable<ExamTypeWiseTabulationSheetMaster> Get();
        ExamTypeWiseTabulationSheetMaster Find(int ExamTypeWiseTabulationSheetMasterID);
        IQueryable<ExamTypeWiseTabulationSheetMaster> SelectQuery(string query, params object[] parameters);
        void Insert(ExamTypeWiseTabulationSheetMaster entity);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, ExamTypeWiseTabulationSheetMaster ExamTypeWiseTabulationSheetMaster);
        void InsertRange(IEnumerable<ExamTypeWiseTabulationSheetMaster> entities);
        void InsertOrUpdateGraph(ExamTypeWiseTabulationSheetMaster entity);
        void InsertGraphRange(IEnumerable<ExamTypeWiseTabulationSheetMaster> entities);
        void Update(ExamTypeWiseTabulationSheetMaster entity);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, ExamTypeWiseTabulationSheetMaster ExamTypeWiseTabulationSheetMaster);
        void Delete(object id);
        void Delete(ExamTypeWiseTabulationSheetMaster entity);
        IQueryFluent<ExamTypeWiseTabulationSheetMaster> Query();
        IQueryFluent<ExamTypeWiseTabulationSheetMaster> Query(IQueryObject<ExamTypeWiseTabulationSheetMaster> queryObject);
        IQueryFluent<ExamTypeWiseTabulationSheetMaster> Query(Expression<Func<ExamTypeWiseTabulationSheetMaster, bool>> query);
        Task<ExamTypeWiseTabulationSheetMaster> FindAsync(params object[] keyValues);
        Task<ExamTypeWiseTabulationSheetMaster> FindAsync(CancellationToken cancellationToken, params object[] keyValues);
        Task<bool> DeleteAsync(params object[] keyValues);
        Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);
        IQueryable<ExamTypeWiseTabulationSheetMaster> Queryable();
        ExamTypeWiseTabulationSheetMaster GetExamTypeWiseTabulationSheetMasterById(int id);
        ExamTypeWiseTabulationSheetMaster GetExamTabulationSheetMaster(int instituteId,int examtypeid, int studentid, int sessionid);
        ExamTypeWiseTabulationSheetMaster newExamTabulationSheetMaster(int instituteid, int currentsessionid);

        List<ExamTypeWiseTabulationSheetMaster> GetExamTypeWiseTabulationSheetMasterCriteria(int instituteid, int CurrentSessionId, VmCommonSearch objVmCommonSearch);
        
    }

    public class ExamTypeWiseTabulationSheetMasterService : Service<ExamTypeWiseTabulationSheetMaster>, IExamTypeWiseTabulationSheetMasterService
    {
        private readonly IRepositoryAsync<ExamTypeWiseTabulationSheetMaster> _repository;
        private readonly IAcademicBranchService _academicBranchService;
        private readonly IAcademicClassService _academicClassService;
        private readonly IAcademicSectionService _academicSectionService;
        private readonly IAcademicSessionService _academicSessionService;
        private readonly IExamTypeService _examTypeService;
        public ExamTypeWiseTabulationSheetMasterService(IRepositoryAsync<ExamTypeWiseTabulationSheetMaster> repository
            , IAcademicBranchService academicBranchService,
            IAcademicClassService academicClassService,
            IAcademicSectionService academicSectionService,
            IAcademicSessionService academicSessionService,
            IExamTypeService examTypeService)
            : base(repository)
        {
            _academicBranchService = academicBranchService;
            _academicClassService = academicClassService;
            _academicSectionService = academicSectionService;
            _academicSessionService = academicSessionService;
            _examTypeService = examTypeService;
            _repository = repository;
        }
        public IEnumerable<ExamTypeWiseTabulationSheetMaster> Get()
        {
            return _repository.Query().Select();
        }

        public ExamTypeWiseTabulationSheetMaster Find(int ExamTypeWiseTabulationSheetMasterID)
        {
            return _repository.Query(x => x.Id == ExamTypeWiseTabulationSheetMasterID).Select().SingleOrDefault();
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, ExamTypeWiseTabulationSheetMaster ExamTypeWiseTabulationSheetMaster)
        {


            _repository.Insert(ExamTypeWiseTabulationSheetMaster);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, ExamTypeWiseTabulationSheetMaster ExamTypeWiseTabulationSheetMaster)
        {

            _repository.Update(ExamTypeWiseTabulationSheetMaster);
            unitOfWorkAsync.SaveChanges();

        }
        public ExamTypeWiseTabulationSheetMaster GetExamTypeWiseTabulationSheetMasterById(int id)
        {
            return _repository.Query().Select().FirstOrDefault(x => x.Id == id);
        }
        public ExamTypeWiseTabulationSheetMaster GetExamTabulationSheetMaster(int instituteId,int examtypeid, int studentid, int sessionid)
        {

            return _repository.Query(x => x.InstituteId == instituteId && x.ExamTypeId == examtypeid && x.StudentId == studentid && x.AcademicSessionId == sessionid).Select().FirstOrDefault();
        }
        public ExamTypeWiseTabulationSheetMaster newExamTabulationSheetMaster(int instituteid, int currentsessionid)
        {
            ExamTypeWiseTabulationSheetMaster objExamTypeWiseTabulationSheetMaster = new ExamTypeWiseTabulationSheetMaster();
            objExamTypeWiseTabulationSheetMaster.AcademicBranchList = _academicBranchService.GetKVP(instituteid);
            objExamTypeWiseTabulationSheetMaster.AcademicClassList = _academicClassService.GetKVP(instituteid);
           // objExamTypeWiseTabulationSheetMaster.AcademicSectionList = _academicSectionService.GetKVP(instituteid);
            objExamTypeWiseTabulationSheetMaster.AcademicSessionList = _academicSessionService.GetKVP(instituteid, currentsessionid, true);
            objExamTypeWiseTabulationSheetMaster.ExamTypeList = _examTypeService.GetKVP(instituteid);

            return objExamTypeWiseTabulationSheetMaster;
        }

        public List<ExamTypeWiseTabulationSheetMaster> GetExamTypeWiseTabulationSheetMasterCriteria(int instituteid,int CurrentSessionId ,VmCommonSearch objVmCommonSearch)
        {
           return  _repository.Query(x => x.InstituteId == instituteid
               && x.AcademicSessionId == CurrentSessionId
               && x.AcademicClassesId == objVmCommonSearch.AcademicClassesId 
               && x.AcademicClassesSectionMapId == objVmCommonSearch.AcademicClassesSectionMapId)
               .Include(x=>x.ExamType).Select().ToList();
        }

    }
}
