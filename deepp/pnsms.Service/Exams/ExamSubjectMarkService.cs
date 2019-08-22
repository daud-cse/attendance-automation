using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pnsms.Entities.Models;
using Service.Pattern;
using System.Linq.Expressions;
using Repository.Pattern.Repositories;
using System.Threading;
using Repository.Pattern.UnitOfWork;
using pnsms.Entities.ViewModels;

using pnsms.Service.Subjects;
namespace pnsms.Service.Exams
{
    public interface IExamSubjectMarkService
    {
        IEnumerable<ExamSubjectMark> Get();
        ExamSubjectMark Find(int ExamSubjectMarkID);
        IQueryable<ExamSubjectMark> SelectQuery(string query, params object[] parameters);
        void Insert(ExamSubjectMark entity);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, ExamSubjectMark ExamSubjectMark);
        void InsertRange(IEnumerable<ExamSubjectMark> entities);
        void InsertOrUpdateGraph(ExamSubjectMark entity);
        void InsertGraphRange(IEnumerable<ExamSubjectMark> entities);
        void Update(ExamSubjectMark entity);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, ExamSubjectMark ExamSubjectMark);
        void Delete(object id);
        void Delete(ExamSubjectMark entity);
        IQueryFluent<ExamSubjectMark> Query();
        IQueryFluent<ExamSubjectMark> Query(IQueryObject<ExamSubjectMark> queryObject);
        IQueryFluent<ExamSubjectMark> Query(Expression<Func<ExamSubjectMark, bool>> query);
        Task<ExamSubjectMark> FindAsync(params object[] keyValues);
        Task<ExamSubjectMark> FindAsync(CancellationToken cancellationToken, params object[] keyValues);
        Task<bool> DeleteAsync(params object[] keyValues);
        Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);
        IQueryable<ExamSubjectMark> Queryable();
        void SaveExamSubjectMarksList(int InstituteId, IUnitOfWorkAsync unitOfWorkAsync, List<ExamSubjectMark> lstExamSubjectMark);
        ExamSubjectMark GetExamSubjectMarkById(int id);
        IEnumerable<ExamSubjectMark> GetExamSubjectMark(int instituteId);
        List<ExamSubjectMark> GetExamSubjectMarksByCriteria(VmCommonSearch objVmCommonSearch);
        ExamSubjectMark newExamSubjectMark(int instituteid, int currentsessionid);
        ExamSubjectMark newExamResultShow(int instituteid);
        object GetExamSubjectMarksByCriteria(VmCommonSearch vmCommonSearch, object objVmCommonSearch);
    }

    public class ExamSubjectMarkService : Service<ExamSubjectMark>, IExamSubjectMarkService
    {
        private readonly IRepositoryAsync<ExamSubjectMark> _repository;
        private readonly IAcademicBranchService _academicBranchService;
        private readonly IAcademicClassService _academicClassService;
        private readonly IAcademicSectionService _academicSectionService;
        private readonly IAcademicSessionService _academicSessionService;
        private readonly IExamTypeService _examTypeService;
        private readonly IStudentService _studentService;
        private readonly IInstituteSubjectClassService _instituteSubjectClassService;
        
            private readonly ISubjectAcademicClassMappingService _iSubjectAcademicClassMappingService;
        public ExamSubjectMarkService(IRepositoryAsync<ExamSubjectMark> repository,
            IAcademicBranchService academicBranchService,
            IAcademicClassService academicClassService,
            IAcademicSectionService academicSectionService,
            IAcademicSessionService academicSessionService,
            IExamTypeService examTypeService
            , IStudentService studentService
            , IInstituteSubjectClassService instituteSubjectClassService
            , ISubjectAcademicClassMappingService  iSubjectAcademicClassMappingService

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
            _instituteSubjectClassService = instituteSubjectClassService;
            _iSubjectAcademicClassMappingService = iSubjectAcademicClassMappingService;

        }
        public ExamSubjectMark newExamSubjectMark(int instituteid, int currentsessionid)
        {
            ExamSubjectMark objExamSubjectMark = new ExamSubjectMark();
            objExamSubjectMark.AcademicBranchList = _academicBranchService.GetKVP(instituteid);
            objExamSubjectMark.AcademicClassList = _academicClassService.GetKVP(instituteid);
           // objExamSubjectMark.AcademicSectionList = _academicSectionService.GetKVP(instituteid);
            objExamSubjectMark.AcademicSessionList = _academicSessionService.GetKVP(instituteid, currentsessionid, true);
            objExamSubjectMark.ExamTypeList = _examTypeService.GetKVP(instituteid);

            return objExamSubjectMark;



        }


        public ExamSubjectMark newExamResultShow(int instituteid)
        {
            ExamSubjectMark newExamResultShow = new ExamSubjectMark();
            //objExamSubjectMark.AcademicBranchList = _academicBranchService.GetKVP(instituteid);
            //objExamSubjectMark.AcademicClassList = _academicClassService.GetKVP(instituteid);           
            newExamResultShow.AcademicSessionList = _academicSessionService.GetKVP(instituteid);
            newExamResultShow.ExamTypeList = _examTypeService.GetKVP(instituteid);

            return newExamResultShow;



        }
        public List<ExamSubjectMark> GetExamSubjectMarksByCriteria(VmCommonSearch objVmCommonSearch)
        {
            Student objStudent = new Student();
            objStudent.CurrentAcademicClassId = objVmCommonSearch.AcademicClassesId;
            objStudent.CurrentAcademicSectionId = objVmCommonSearch.AcademicClassesSectionMapId;
            objStudent.CurrentAcademicGroupId = objVmCommonSearch.AcademicGroupId;
            objStudent.CurrentAcademicSessionId = objVmCommonSearch.CurrentSessionId;
            var objSubjectAcademicClassMapping = _iSubjectAcademicClassMappingService.Get(objVmCommonSearch.SubjectAcademicClassMappingsMapId);
            List<ExamSubjectMark> objExamSubjectMarkList = new List<ExamSubjectMark>();


            var  lstSearchExamSubjectMarkList = _repository.Query(x => x.InstituteId == objVmCommonSearch.InstituteId && x.SubjectAcademicClassMappingsMapId == objVmCommonSearch.SubjectAcademicClassMappingsMapId && x.ExamId == objVmCommonSearch.ExamId)
                                                 .Include(x=>x.SubjectAcademicClassMapping)
                                                 .Include(x => x.SubjectAcademicClassMapping.InstituteSubject)
                                                 .Include(x=>x.Student.UserInfo).Select().ToList();
            if (lstSearchExamSubjectMarkList.Count == 0)
            {

               
               KeyValuePair<int ,string> subject=new KeyValuePair<int, string>();
                //if (objVmCommonSearch.SubjectList.Count>= 0)
                //{
                //    subject= objVmCommonSearch.SubjectList.Find(x => x.Key == objVmCommonSearch.SubjectAcademicClassMappingsMapId);
                //}
                var objstudentList = _studentService.GetAllStudent(objVmCommonSearch.InstituteId, objStudent);
                objstudentList.ToList().ForEach(delegate(Student item)
                {
                    ExamSubjectMark objExamSubjectMark = new ExamSubjectMark();
                    objExamSubjectMark.SubjectAcademicClassMapping = new SubjectAcademicClassMapping();
                    objExamSubjectMark.SubjectAcademicClassMapping.InstituteSubject = new InstituteSubject();
                    objExamSubjectMark.Student = new Student();
                    objExamSubjectMark.Student.UserInfo = new UserInfo();
                    
                    objExamSubjectMark.ExamId = objVmCommonSearch.ExamId;
                    objExamSubjectMark.StudentId = item.StudentId;
                    objExamSubjectMark.Student.UserInfo.Name = item.UserInfo.Name;
                    objExamSubjectMark.StudentRoleNo = item.CurrentRollNo;
                    objExamSubjectMark.SubjectAcademicClassMappingsMapId = objSubjectAcademicClassMapping.Id;
                    objExamSubjectMark.SubjectMarks = objSubjectAcademicClassMapping.SubjectMarks;
                    objExamSubjectMark.SubjectAcademicClassMapping.InstituteSubject.Name = objSubjectAcademicClassMapping.InstituteSubject.Name;
                  

                    objExamSubjectMarkList.Add(objExamSubjectMark);
                });
            }
            else
            {
                KeyValuePair<int, string> subject = new KeyValuePair<int, string>();
                if (objVmCommonSearch.SubjectList.Count >= 0)
                {
                    subject = objVmCommonSearch.SubjectList.Find(x => x.Key == objVmCommonSearch.SubjectAcademicClassMappingsMapId);
                }
                var objstudentList = _studentService.GetAllStudent(objVmCommonSearch.InstituteId, objStudent);
                lstSearchExamSubjectMarkList.ToList().ForEach(delegate (ExamSubjectMark item)
                {
                    ExamSubjectMark objExamSubjectMark = new ExamSubjectMark();
                    objExamSubjectMark.SubjectAcademicClassMapping = new SubjectAcademicClassMapping();
                    objExamSubjectMark.SubjectAcademicClassMapping.InstituteSubject = new InstituteSubject();
                    objExamSubjectMark.Student = new Student();
                    objExamSubjectMark.Student.UserInfo = new UserInfo();
                    objExamSubjectMark.Id = item.Id;
                    objExamSubjectMark.SubjectAcademicClassMappingsMapId= item.SubjectAcademicClassMappingsMapId;
                    objExamSubjectMark.ExamId = objVmCommonSearch.ExamId;
                    objExamSubjectMark.StudentId = item.StudentId;
                    objExamSubjectMark.MarksObtained = item.MarksObtained;
                    objExamSubjectMark.AcceptMarksTotal = item.AcceptMarksTotal;
                    objExamSubjectMark.Comment = item.Comment;
                    objExamSubjectMark.Student.UserInfo.Name = item.Student.UserInfo.Name;
                    objExamSubjectMark.StudentRoleNo = item.Student.CurrentRollNo;
                    objExamSubjectMark.SubjectAcademicClassMappingsMapId = objSubjectAcademicClassMapping.Id;
                    objExamSubjectMark.SubjectMarks = objSubjectAcademicClassMapping.SubjectMarks;
                    objExamSubjectMark.SubjectAcademicClassMapping.InstituteSubject.Name = objSubjectAcademicClassMapping.InstituteSubject.Name;


                    objExamSubjectMarkList.Add(objExamSubjectMark);
                });
            }
            return objExamSubjectMarkList;
        }
        public IEnumerable<ExamSubjectMark> Get()
        {
            return _repository.Query().Select();
        }

        public ExamSubjectMark Find(int ExamSubjectMarkID)
        {
            return _repository.Query(x => x.Id == ExamSubjectMarkID).Select().SingleOrDefault();
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, ExamSubjectMark ExamSubjectMark)
        {


            _repository.Insert(ExamSubjectMark);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, ExamSubjectMark ExamSubjectMark)
        {

            _repository.Update(ExamSubjectMark);
            unitOfWorkAsync.SaveChanges();

        }

        public void SaveExamSubjectMarksList(int InstituteId, IUnitOfWorkAsync unitOfWorkAsync, List<ExamSubjectMark> lstExamSubjectMark)
        {
            lstExamSubjectMark.ForEach(delegate(ExamSubjectMark item)
            {
                ExamSubjectMark objExamSubjectMark=new ExamSubjectMark();

                objExamSubjectMark.Id = item.Id;
                objExamSubjectMark.InstituteId = InstituteId;
                objExamSubjectMark.ExamId = item.ExamId;
                objExamSubjectMark.LastUpdateTime = DateTime.Now;
                objExamSubjectMark.StudentId=item.StudentId;
                objExamSubjectMark.SubjectAcademicClassMappingsMapId = item.SubjectAcademicClassMappingsMapId;
                objExamSubjectMark.MarksObtained=item.MarksObtained;
                objExamSubjectMark.AcceptMarksTotal=item.AcceptMarksTotal;
                objExamSubjectMark.Comment = item.Comment;
                if(item.Id>0){
                    _repository.Update(objExamSubjectMark);
                    unitOfWorkAsync.SaveChanges();
                }
                else
                {
                    _repository.Insert(objExamSubjectMark);
                    unitOfWorkAsync.SaveChanges();
                }                
            });
           

        }
        public ExamSubjectMark GetExamSubjectMarkById(int id)
        {
            return _repository.Query().Select().FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<ExamSubjectMark> GetExamSubjectMark(int instituteId)
        {

            return _repository.Query(x => x.InstituteId == instituteId).Select();
        }

        public object GetExamSubjectMarksByCriteria(VmCommonSearch vmCommonSearch, object objVmCommonSearch)
        {
            throw new NotImplementedException();
        }
    }



}
