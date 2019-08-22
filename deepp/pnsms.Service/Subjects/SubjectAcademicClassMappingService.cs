using pnsms.Entities.Models;
using pnsms.Entities.ViewModels.Institutes;
using pnsms.Entities.ViewModels.Subjects;
using pnsms.Service.InstituteSubjects;
using pnsms.Service.Settings;
using pnsms.utility;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service.Subjects
{
    public interface ISubjectAcademicClassMappingService : IService<SubjectAcademicClassMapping>
    {


        /// <summary>
        /// Gets the Subjects.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="academicSessionId">The academic session identifier.</param>
        /// <param name="academicClassId">The academic class identifier.</param>
        /// <param name="academicGroupId">The academic group identifier.</param>
        /// <returns></returns>
        IEnumerable<SubjectAcademicClassMapping> Get(int instituteId, int academicSessionId, int academicClassId, int? academicGroupId);
        /// <summary>
        /// Gets the specified institute identifier.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="academicSessionId">The academic session identifier.</param>
        /// <param name="academicClassId">The academic class identifier.</param>
        /// <returns></returns>
        IEnumerable<SubjectAcademicClassMapping> Get(int instituteId, int academicSessionId, int academicClassId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instituteId"></param>
        /// <param name="academicSessionId"></param>
        /// <param name="academicClassId"></param>
        /// <returns></returns>
        IEnumerable<VmSubjectAcademicClassMapping> GetVMSubjectAcademicClass(int instituteId, int AcademicBranchId, int academicSessionId, int academicClassId, int? shiftId);


      List < KeyValuePair<int,string>> GetSectionByAcademicClass(int instituteId,int academicSessionId, int academicClassId, int? shiftId);
        /// <summary>
        /// Gets the Subject by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        SubjectAcademicClassMapping Get(int id);
        /// <summary>
        /// Gets the KVP class wise.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="classId">The class identifier.</param>
        /// <returns></returns>
        List<KeyValuePair<int, string>> GetKVPClassWise(int instituteId, int classId);
        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="subjectAcademicClassMapping">The subject academic class mapping.</param>
        void Insert(int InstituteId, int CurrentSessionId, IUnitOfWorkAsync unitOfWorkAsync, List<VmSubjectAcademicClassMapping> subjectAcademicClassMapping);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, List<SubjectAcademicClassMapping> subjectAcademicClassMapping);
        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="subject">The Subject.</param>
        void Update(IUnitOfWorkAsync unitOfWorkAsync, SubjectAcademicClassMapping subjectAcademicClassMapping);

        SubjectAcademicClassMapping New(int instituteId);
        List<SubjectAcademicClassMapping> GetAllClassWiseMapping(int instituteId, int? academicClassid = null);
        List<SubjectAcademicClassMapping> GetAll(int instituteId, int SessionId);
        List<SubjectAcademicClassMapping> GetAllMappingByCriteria(int instituteId, int CurrentSessionId, int classId, int ClasssectionMapid, int TeacherId);

        //List<KeyValuePair<int, string>> GetKVP(int instituteId, int classId, int sessionId);
        // List<KeyValuePair<int, string>> GetKVPSubjectTypeWise(int instituteId, int classId);
        List<KeyValuePair<int, string>> GetKVPSubjectTypeWise(int instituteId, int classId, int? groupId = null);
        List<KeyValuePair<int, string>> GetSubjectKVPByTypeId(int instituteId, int classId, int subjectTypeId, int? groupId = null);
        VmSubjectAcademicClassMapping GetSubjectTeacherBySectionWise(int instituteId, int AcademicClassSectionMapId);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="pnsms.Contracts.ExamResult.Subject.Service._ISubjectAcademicClassMappingService" />
    /// <seealso cref="Service.Pattern.Service{pnsms.Entities.Models.SubjectAcademicClassMapping}" />
    /// <seealso cref="Contracts.ExamResult.Subject.Service._ISubjectAcademicClassMappingService" />
    public class SubjectAcademicClassMappingService : Service<SubjectAcademicClassMapping>, ISubjectAcademicClassMappingService
    {
        #region "  -  [  Constractor  ]  -  "


        private readonly IRepositoryAsync<SubjectAcademicClassMapping> _repository;
        private readonly IAcademicClassService _academicClassService;
        private readonly IAcademicBranchService _academicBranchService;
        private readonly IInstituteSubjectService _instituteSubjectService;
        private readonly ISubjectService _subjectService;
        private readonly ISubjectGroupService _subjectGroupService;
        private readonly ITeacherService _teacherService;
        private readonly ISubjectTypeService _subjectTypeService;
        private readonly ISubjectAcademicClassMappingSubjectTypeService _subjectAcademicClassMappingSubjectTypeService;
        private readonly IAcademicClassSectionMappingService _academicClassSectionMappingService;

        // private readonly ISubjectAcademicClassMappingAcademicGroupService _subjectAcademicClassMappingAcademicGroupService;


        public SubjectAcademicClassMappingService(IRepositoryAsync<SubjectAcademicClassMapping> repository
            , IAcademicClassService academicClassService
            , ISubjectService subjectService
            , ISubjectTypeService subjectTypeService
            , IInstituteSubjectService instituteSubjectService
            , IAcademicClassSectionMappingService academicClassSectionMappingService
            , ITeacherService teacherService
            ,IAcademicBranchService academicBranchService
            , ISubjectGroupService subjectGroupService
            //,ISubjectAcademicClassMappingSubjectTypeService subjectAcademicClassMappingSubjectTypeService, 
            //ISubjectAcademicClassMappingAcademicGroupService subjectAcademicClassMappingAcademicGroupService
            )
            : base(repository)
        {
            _repository = repository;
            _academicClassService = academicClassService;
            _instituteSubjectService = instituteSubjectService;
            _subjectService = subjectService;
            _subjectTypeService = subjectTypeService;
            _academicClassSectionMappingService = academicClassSectionMappingService;
            _teacherService = teacherService;
            _academicBranchService = academicBranchService;
            _subjectGroupService = subjectGroupService;
            //_subjectAcademicClassMappingSubjectTypeService = subjectAcademicClassMappingSubjectTypeService;
            //_subjectAcademicClassMappingAcademicGroupService = subjectAcademicClassMappingAcademicGroupService;
        }

        #endregion

        #region "  -  [  Crud  ]  -  "
        /// <summary>
        /// Gets the Subjects.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="academicSessionId">The academic session identifier.</param>
        /// <param name="academicClassId">The academic class identifier.</param>
        /// <param name="academicGroupId">The academic group identifier.</param>
        /// <returns></returns>
        public IEnumerable<SubjectAcademicClassMapping> Get(int instituteId, int academicSessionId, int academicClassId, int? academicGroupId)
        {

            var results = _repository.Query(c => c.AcademicClassId == academicClassId
                && c.AcademicSessionId == academicSessionId
                && c.AcademicClassId == academicClassId
                //  && c.AcademicGroupId == academicGroupId 
                && c.InstituteId == instituteId)
                .Include(s => s.SubjectAcademicClassMappingSubjectTypes)
                //.Include(s => s.SubjectAcademicClassMappingAcademicGroups)
               .Select();
            return results;


        }

        public IEnumerable<VmSubjectAcademicClassMapping> GetVMSubjectAcademicClass(int instituteId,int AcademicBranchId, int academicSessionId, int academicClassId, int? shiftId)
        {
            // return SubjectAcademicClassMapping.Get(instituteId, academicClassId);
            var results = Get(instituteId, academicSessionId, academicClassId);
            var subjectTypeList = _subjectTypeService.GetKVP(instituteId);
            var subjectGroups = _subjectGroupService.GetKVP(instituteId);
            var academicClassSectionMappinglst = _academicClassSectionMappingService.GetAll(instituteId, academicClassId);
            var instituteSubjects = _instituteSubjectService.GetKVP(instituteId);

            var teacherlst = _teacherService.GetKVP(instituteId);

            //academicClassSectionMapping=  academicClassSectionMapping.ToList().Foreach(item );
            //var  academicClassSectionMappinglst= academicClassSectionMapping.Select(new AcademicClassSectionMapping() {})
            var subjectAcademicClassMappingList = new List<VmSubjectAcademicClassMapping>();
            academicClassSectionMappinglst.ToList().ForEach(delegate(AcademicClassSectionMapping item)
            {
                instituteSubjects.ForEach(delegate(KeyValuePair<int, string> sub)
                {
                    VmSubjectAcademicClassMapping objVmSubjectAcademicClassMapping = new VmSubjectAcademicClassMapping();
                    objVmSubjectAcademicClassMapping.InstituteId = instituteId;
                    objVmSubjectAcademicClassMapping.SubjectId = sub.Key;
                    objVmSubjectAcademicClassMapping.AcademicBranchId=AcademicBranchId;
                    objVmSubjectAcademicClassMapping.AcademicClassId = academicClassId;
                    objVmSubjectAcademicClassMapping.AcademicSessionId = academicSessionId;
                    objVmSubjectAcademicClassMapping.SectionName = item.AcademicSection.Name;

                    objVmSubjectAcademicClassMapping.IsActive = false;
                    //AcademicGroupId = academicGroupId,
                    objVmSubjectAcademicClassMapping.AcademicClassSectionMapId = item.Id;
                    objVmSubjectAcademicClassMapping.SubjectName = sub.Value;
                    objVmSubjectAcademicClassMapping.TeacherList = teacherlst;

                    objVmSubjectAcademicClassMapping.SubjectTypes = subjectTypeList;
                    objVmSubjectAcademicClassMapping.SubjectGroups = subjectGroups;
                    subjectAcademicClassMappingList.Add(objVmSubjectAcademicClassMapping);
                });
                
            });

            if (results == null)
            {
                return subjectAcademicClassMappingList;

            }
            else
            {
                foreach (var dbsubmap in results)
                    foreach (var submap in subjectAcademicClassMappingList.Where(submap => submap.SubjectId == dbsubmap.SubjectId && submap.AcademicClassSectionMapId == dbsubmap.AcademicClassSectionMapId))
                    {
                        submap.Id = dbsubmap.Id;
                        submap.AcademicClassId = dbsubmap.AcademicClassId;
                        submap.AcademicSessionId = dbsubmap.AcademicSessionId;
                        submap.AcademicClassSectionMapId = dbsubmap.AcademicClassSectionMapId;
                        submap.IsSubjectGroupWise = dbsubmap.IsSubjectGroupWise;
                        submap.AcademicBranchId=dbsubmap.AcademicBranchId;
                        submap.SubjectId = dbsubmap.SubjectId;
                        submap.SubjectTypeId = dbsubmap.SubjectTypeId;
                        submap.SubjectGroupId = dbsubmap.SubjectGroupId;
                        submap.SubjectGroupNameList = dbsubmap.SubjectGroupNameList;
                        submap.ParentSubjectId = dbsubmap.ParentSubjectId;
                        submap.SubjectMarks = dbsubmap.SubjectMarks;
                        submap.OrderBy = dbsubmap.OrderBy;
                        submap.MarksEntryTypeKey = dbsubmap.MarksEntryTypeKey;
                        submap.IsSubjectGroupWise = dbsubmap.IsSubjectGroupWise;
                        submap.TeacherList = teacherlst;
                        submap.TeacherId = dbsubmap.TeacherId;
                        submap.IsActive = true;
                        //var typeIds = dbsubmap.SubjectAcademicClassMappingSubjectTypes.Select(s => s.SubjectTypeId).ToArray();
                           submap.SubjectTypes = subjectTypeList;//subjectTypeList.Where(s => typeIds.Contains(s.Key)).ToList();
                        submap.SubjectGroups = subjectGroups;
                        //var groupIds = dbsubmap.SubjectAcademicClassMappingAcademicGroups.Select(s => s.AcademicGroupId).ToArray();
                        //submap.SubjectGroups = subjectGroups.Where(s => groupIds.Contains(s.Key)).ToList();
                    }
            }



            return subjectAcademicClassMappingList;

        }
        public List<SubjectAcademicClassMapping> GetAll(int instituteId, int SessionId)
        {
            try
            {



                return _repository.Query(x => x.InstituteId == instituteId && x.AcademicSessionId == SessionId).Select().ToList();
            }
            catch(Exception ex){
              return new List<SubjectAcademicClassMapping>();
            }
        }
        public List<KeyValuePair<int, string>> GetSectionByAcademicClass(int instituteId,int academicSessionId, int academicClassId, int? shiftId)
        {
            var SectionByAcademicClass=new List<KeyValuePair<int,string>>();


            var lst = _repository.Query(x => x.AcademicClassId == academicClassId && x.AcademicSessionId == academicSessionId && x.InstituteId == instituteId)
               .Include(x => x.AcademicClassSectionMapping)
               .Include(x => x.AcademicClassSectionMapping.AcademicSection).Select();
                lst.ToList().ForEach(y => SectionByAcademicClass.Add(new KeyValuePair<int ,string>(y.AcademicClassSectionMapping.Id,y.AcademicClassSectionMapping.AcademicSection.Name)));
                SectionByAcademicClass = SectionByAcademicClass.Distinct().ToList();
                return SectionByAcademicClass;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instituteId"></param>
        /// <param name="academicSessionId"></param>
        /// <param name="academicClassId"></param>
        /// <param name="shiftId"></param>
        /// <returns></returns>
        public VmSubjectAcademicClassMapping GetSubjectTeacherBySectionWise(int instituteId, int AcademicClassSectionMapId)
        {
            VmSubjectAcademicClassMapping objVmSubjectAcademicClassMapping = new VmSubjectAcademicClassMapping();
            var SubjectByAcademicClassSection = new List<KeyValuePair<int, string>>();
            var teacherByAcademicClassSection = new List<KeyValuePair<int, string>>();


            var lst = _repository.Query(x => x.AcademicClassSectionMapId == AcademicClassSectionMapId && x.InstituteId == instituteId)
               .Include(x => x.AcademicClassSectionMapping)
               .Include(x => x.InstituteSubject)
               .Include(x => x.SubjectGroup)
               .Include(x => x.AcademicClassSectionMapping.AcademicSection)
               .Include(x=>x.Teacher.UserInfo).Select();

            lst.ToList().ForEach(y => SubjectByAcademicClassSection.Add(new KeyValuePair<int, string>(y.Id ,y.InstituteSubject.Name)));
            //  SubjectByAcademicClassSection = SubjectByAcademicClassSection.Distinct().ToList();

            objVmSubjectAcademicClassMapping.kvpSubjectList = SubjectByAcademicClassSection;
            objVmSubjectAcademicClassMapping.lstSubjectAcademicClassMapping = lst.ToList();
            return objVmSubjectAcademicClassMapping;
        }
        /// <summary>
        /// Gets the Subject by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="ValidationException">Invalid Subjects</exception>
        public SubjectAcademicClassMapping Get(int id)
        {
            var subjectAcademicClassMapping = _repository.Query(r => r.Id.Equals(id))
                
                .Include(x => x.InstituteSubject)
                .Select().SingleOrDefault();
            if (subjectAcademicClassMapping == null)
                throw new ValidationException("Invalid SubjectAcademicClassMapping");
            return subjectAcademicClassMapping;
        }


        /// <summary>
        /// Inserts the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="subjectAcademicClassMappings">The subject academic class mappings.</param>
        public void Insert(int InstituteId, int CurrentSessionId, IUnitOfWorkAsync unitOfWorkAsync, List<VmSubjectAcademicClassMapping> subjectAcademicClassMappings)
        {
            try
            {
                unitOfWorkAsync.BeginTransaction(isolationLevel: System.Data.IsolationLevel.Unspecified);



                //if (subjectAcademicClassMappings.Any())
                //{

                //    var defaultitem = subjectAcademicClassMappings.FirstOrDefault();

                //    DeleteAll(defaultitem.InstituteId, defaultitem.AcademicSessionId, defaultitem.AcademicClassId, defaultitem.AcademicGroupId);
                //    unitOfWorkAsync.SaveChanges();
                //}
                if (subjectAcademicClassMappings != null)
                    foreach (var subjectAcademicClassMapping in subjectAcademicClassMappings)
                    {
                        var subjectClassMapping = new SubjectAcademicClassMapping();
                        subjectClassMapping.Id = subjectAcademicClassMapping.Id;
                        subjectClassMapping.InstituteId = InstituteId;
                        subjectClassMapping.AcademicSessionId = CurrentSessionId;
                        subjectClassMapping.AcademicClassSectionMapId = subjectAcademicClassMapping.AcademicClassSectionMapId;
                        subjectClassMapping.TeacherId = subjectAcademicClassMapping.TeacherId;

                        subjectClassMapping.AcademicBranchId = subjectAcademicClassMapping.AcademicBranchId;
                        subjectClassMapping.InstituteId = subjectAcademicClassMapping.InstituteId;
                        subjectClassMapping.AcademicClassId = subjectAcademicClassMapping.AcademicClassId;
                        subjectClassMapping.SubjectId = subjectAcademicClassMapping.SubjectId;
                        subjectClassMapping.ParentSubjectId = subjectAcademicClassMapping.ParentSubjectId;
                        subjectClassMapping.SubjectMarks = subjectAcademicClassMapping.SubjectMarks;
                        subjectClassMapping.OrderBy = subjectAcademicClassMapping.OrderBy;
                        subjectClassMapping.MarksEntryTypeKey = subjectAcademicClassMapping.MarksEntryTypeKey;
                        subjectClassMapping.IsSubjectGroupWise = subjectAcademicClassMapping.IsSubjectGroupWise;

                        subjectClassMapping.SubjectGroupId = subjectAcademicClassMapping.SubjectGroupId;
                        subjectClassMapping.SubjectGroupNameList = subjectAcademicClassMapping.SubjectGroupNameList;

                        subjectClassMapping.SubjectTypeId = subjectAcademicClassMapping.SubjectTypeId;
                        subjectClassMapping.AcademicSessionId = subjectAcademicClassMapping.AcademicSessionId;
                        subjectClassMapping.AcademicGroupId = subjectAcademicClassMapping.AcademicGroupId;
                        if (subjectClassMapping.Id>0)
                        {
                            _repository.Update(subjectClassMapping);
                        }
                        else
                        {
                            _repository.Insert(subjectClassMapping);
                        }
                        
                        unitOfWorkAsync.SaveChanges();
                       
                        if (subjectAcademicClassMapping.SubjectGroups != null)
                        {
                            foreach (var subType in subjectAcademicClassMapping.SubjectGroups)
                            {
                                //var subjectAcademicClassMappingSubjectType = new SubjectAcademicClassMappingAcademicGroup();
                                //subjectAcademicClassMappingSubjectType.Id = subjectAcademicClassMappingSubjectType.Id;
                                //subjectAcademicClassMappingSubjectType.SubjectAcademicClassMappingId = subjectClassMapping.Id;
                                //subjectAcademicClassMappingSubjectType.InstituteId = subjectClassMapping.InstituteId;
                                //subjectAcademicClassMappingSubjectType.AcademicClassId = subjectClassMapping.AcademicClassId;
                                //subjectAcademicClassMappingSubjectType.SubjectId = subjectClassMapping.SubjectId;
                                //subjectAcademicClassMappingSubjectType.AcademicGroupId = subType.Key;
                                // _subjectAcademicClassMappingAcademicGroupService.Insert(subjectAcademicClassMappingSubjectType);
                            }
                            //  unitOfWorkAsync.SaveChanges();
                        }


                    }


                unitOfWorkAsync.Commit();
            }
            catch (Exception)
            {
                unitOfWorkAsync.Rollback();
                throw;

            }

        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, List<SubjectAcademicClassMapping> subjectAcademicClassMappings)
        {
            try
            {
                unitOfWorkAsync.BeginTransaction(isolationLevel: System.Data.IsolationLevel.Unspecified);



                if (subjectAcademicClassMappings.Any())
                {

                    var defaultitem = subjectAcademicClassMappings.FirstOrDefault();

                    DeleteAll(defaultitem.InstituteId, defaultitem.AcademicSessionId, defaultitem.AcademicClassId, defaultitem.AcademicGroupId);
                    unitOfWorkAsync.SaveChanges();
                }
                if (subjectAcademicClassMappings != null)
                    foreach (var subjectAcademicClassMapping in subjectAcademicClassMappings)
                    {
                        var subjectClassMapping = new SubjectAcademicClassMapping();
                        subjectClassMapping.Id = subjectAcademicClassMapping.Id;
                        subjectClassMapping.InstituteId = subjectAcademicClassMapping.InstituteId;
                        subjectClassMapping.AcademicClassId = subjectAcademicClassMapping.AcademicClassId;
                        subjectClassMapping.SubjectId = subjectAcademicClassMapping.SubjectId;
                        subjectClassMapping.ParentSubjectId = subjectAcademicClassMapping.ParentSubjectId;
                        subjectClassMapping.OrderBy = subjectAcademicClassMapping.OrderBy;
                        subjectClassMapping.MarksEntryTypeKey = subjectAcademicClassMapping.MarksEntryTypeKey;
                        subjectClassMapping.IsSubjectGroupWise = subjectAcademicClassMapping.IsSubjectGroupWise;
                        subjectClassMapping.AcademicSessionId = subjectAcademicClassMapping.AcademicSessionId;
                        subjectClassMapping.AcademicGroupId = subjectAcademicClassMapping.AcademicGroupId;
                        _repository.Insert(subjectClassMapping);
                        unitOfWorkAsync.SaveChanges();
                        if (subjectAcademicClassMapping.SubjectAcademicClassMappingSubjectTypes != null)
                        {
                            foreach (var subType in subjectAcademicClassMapping.SubjectAcademicClassMappingSubjectTypes)
                            {
                                var subjectAcademicClassMappingSubjectType = new SubjectAcademicClassMappingSubjectType();
                                subjectAcademicClassMappingSubjectType.Id = subjectAcademicClassMappingSubjectType.Id;
                                subjectAcademicClassMappingSubjectType.SubjectAcademicClassMappingId = subjectClassMapping.Id;
                                subjectAcademicClassMappingSubjectType.InstituteId = subjectClassMapping.InstituteId;
                                subjectAcademicClassMappingSubjectType.AcademicClassId = subjectClassMapping.AcademicClassId;
                                subjectAcademicClassMappingSubjectType.SubjectId = subjectClassMapping.SubjectId;
                                subjectAcademicClassMappingSubjectType.SubjectTypeId = subType.SubjectTypeId;
                                _subjectAcademicClassMappingSubjectTypeService.Insert(subjectAcademicClassMappingSubjectType);
                            }
                            unitOfWorkAsync.SaveChanges();
                        }


                    }


                unitOfWorkAsync.Commit();
            }
            catch (Exception)
            {
                unitOfWorkAsync.Rollback();
                throw;

            }

        }




        /// <summary>
        /// Updates the specified unit of work asynchronous.
        /// </summary>
        /// <param name="unitOfWorkAsync">The unit of work asynchronous.</param>
        /// <param name="subjectAcademicClassMapping"></param>
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, SubjectAcademicClassMapping subjectAcademicClassMapping)
        {
            try
            {
                unitOfWorkAsync.BeginTransaction(isolationLevel: System.Data.IsolationLevel.Unspecified);

                _repository.Update(subjectAcademicClassMapping);
                unitOfWorkAsync.SaveChanges();

                unitOfWorkAsync.Commit();
            }
            catch (Exception)
            {
                unitOfWorkAsync.Rollback();
                throw;

            }
        }
        /// <summary>
        /// Gets the KVP class wise.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="classId">The class identifier.</param>
        /// <returns></returns>
        public List<KeyValuePair<int, string>> GetKVPClassWise(int instituteId, int classId)
        {
            var data = _repository.Query(c => c.InstituteId == instituteId && c.AcademicClassId == classId).Include(c => c.InstituteSubject)
                .Select().ToList();
            var classList = new List<KeyValuePair<int, string>>();
            data.ForEach(c => classList.Add(new KeyValuePair<int, string>(c.SubjectId, c.InstituteSubject.Name)));

            return classList;
        }
       public  List<SubjectAcademicClassMapping> GetAllMappingByCriteria(int instituteId, int CurrentSessionId,int classId,int ClasssectionMapid, int TeacherId)
        {



            Expression<Func<SubjectAcademicClassMapping, bool>> predicate = PredicateBuilder.True<SubjectAcademicClassMapping>();
            predicate = predicate.And(p => p.InstituteId == instituteId);
            if (CurrentSessionId > 0)
                predicate = predicate.And(p => p.AcademicSessionId == CurrentSessionId);

            if (classId > 0)
                predicate = predicate.And(p => p.AcademicClassId == classId);

            if (ClasssectionMapid > 0)
                predicate = predicate.And(p => p.AcademicClassSectionMapId == ClasssectionMapid);
            if (TeacherId > 0)
                predicate = predicate.And(p => p.TeacherId == TeacherId);


            return _repository.Query(predicate)
                .Include(x=>x.Teacher.UserInfo)
                //.Include(x => x.Ac)
                  .Include(x => x.AcademicBranch)
                .Include(x=>x.AcademicSession)
                .Include(x => x.AcademicClassSectionMapping.AcademicSection)
                .Include(x => x.AcademicClass)
                .Include(x => x.InstituteSubject)
                .Select().ToList();
        }
        public List<KeyValuePair<int, string>> GetKVPSubjectTypeWise(int instituteId, int classId, int? groupId = null)
        {
            var subList = new List<KeyValuePair<int, string>>();
            if (groupId != null)
            {
                var data = _repository.Query(c => c.InstituteId == instituteId
                    && c.AcademicClassId == classId
                    && c.AcademicGroupId == groupId
                    && c.SubjectAcademicClassMappingSubjectTypes.Any(s => !s.SubjectType.IsDefault)).Include(c => c.InstituteSubject)
                    .Select().ToList();

                data.ForEach(c => subList.Add(new KeyValuePair<int, string>(c.SubjectId, c.InstituteSubject.Name)));
            }
            else
            {
                var data = _repository.Query(c => c.InstituteId == instituteId && c.AcademicClassId == classId && c.SubjectAcademicClassMappingSubjectTypes.Any(s => !s.SubjectType.IsDefault)).Include(c => c.InstituteSubject)
                   .Select().ToList();

                data.ForEach(c => subList.Add(new KeyValuePair<int, string>(c.SubjectId, c.InstituteSubject.Name)));
            }
            return subList;
        }
        public List<KeyValuePair<int, string>> GetSubjectKVPByTypeId(int instituteId, int classId, int subjectTypeId, int? groupId = null)
        {
            var subList = new List<KeyValuePair<int, string>>();
            if (groupId != null)
            {
                var data = _repository.Query(c => c.InstituteId == instituteId
                    && c.AcademicClassId == classId
                    && c.AcademicGroupId == groupId
                    && c.SubjectAcademicClassMappingSubjectTypes.Any(s => !s.SubjectType.IsDefault && s.SubjectTypeId == subjectTypeId))
                    .Include(c => c.InstituteSubject).Include(c => c.SubjectAcademicClassMappingSubjectTypes)
                    .Select().ToList();
                data.ForEach(c => subList.Add(new KeyValuePair<int, string>(c.SubjectId, c.InstituteSubject.Name)));

            }
            else
            {
                var data = _repository.Query(c => c.InstituteId == instituteId
                    && c.AcademicClassId == classId
                    && c.SubjectAcademicClassMappingSubjectTypes.Any(s => !s.SubjectType.IsDefault && s.SubjectTypeId == subjectTypeId))
                    .Include(c => c.InstituteSubject)
                   .Select().ToList();
                data.ForEach(c => subList.Add(new KeyValuePair<int, string>(c.SubjectId, c.InstituteSubject.Name)));

            }
            return subList;
        }
        public List<KeyValuePair<int, string>> GetKVP(int instituteId, int classId, int sessionId)
        {
            var data = _repository.Query(c => c.InstituteId == instituteId && c.AcademicClassId == classId && c.AcademicSessionId == sessionId).Include(c => c.InstituteSubject)
                .Select().ToList();
            var classList = new List<KeyValuePair<int, string>>();
            data.ForEach(c => classList.Add(new KeyValuePair<int, string>(c.SubjectId, c.InstituteSubject.Name)));

            return classList;
        }
        /// <summary>
        /// News the specified institute identifier.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public SubjectAcademicClassMapping New(int instituteId)
        {

            var subjectAcademicClassMapping = new SubjectAcademicClassMapping();
            subjectAcademicClassMapping.AcademicBranchList = _academicBranchService.GetKVP(instituteId);
            subjectAcademicClassMapping.AcademicClassList = _academicClassService.GetKVP(instituteId);
            subjectAcademicClassMapping.SubjectList = _instituteSubjectService.GetKVP(instituteId);
            //subjectAcademicClassMapping.SubjectClassList = GetAllClassWiseMapping(instituteId);

            return subjectAcademicClassMapping;
        }

        private void DeleteAll(int instituteId, int? sessionId, int classId, int? groupId)
        {
            var result = _repository.Query(s => s.InstituteId == instituteId && s.AcademicSessionId == sessionId && s.AcademicGroupId == groupId && s.AcademicClassId == classId).Select();
            foreach (var item in result.ToList())
            {
              //  DeleteAllSubjectTypes(item.Id);
               // DeleteAllSubjectGroups(item.Id);
                _repository.Delete(item);
            }

        }
        private void DeleteAllSubjectGroups(int subjectAcademicClassMappingId)
        {
            //var result = _subjectAcademicClassMappingAcademicGroupService.Query(s => s.SubjectAcademicClassMappingId == subjectAcademicClassMappingId).Select();
            //foreach (var item in result)
            //{
            //    _subjectAcademicClassMappingAcademicGroupService.Delete(item);
            //}

        }
        private void DeleteAllSubjectTypes(int subjectAcademicClassMappingId)
        {
            var result = _subjectAcademicClassMappingSubjectTypeService.Query(s => s.SubjectAcademicClassMappingId == subjectAcademicClassMappingId).Select();
            foreach (var item in result)
            {
                _subjectAcademicClassMappingSubjectTypeService.Delete(item);
            }

        }

        public List<SubjectAcademicClassMapping> GetAllClassWiseMapping(int instituteId, int? academicClassid = null)
        {
            if (academicClassid != null)
            {
                var data = _repository.Query(c => c.InstituteId == instituteId && c.AcademicClassId == academicClassid).Include(c => c.InstituteSubject)
                    .Select().ToList();
                data.ForEach(c => c.SubjectName = c.InstituteSubject.Name);

                return data;
            }
            else
            {
                var data = _repository.Query(c => c.InstituteId == instituteId).Include(c => c.InstituteSubject)
                .Select().ToList();
                data.ForEach(c => c.SubjectName = c.InstituteSubject.Name);

                return data;
            }



        }
        #endregion


        public IEnumerable<SubjectAcademicClassMapping> Get(int instituteId, int academicSessionId, int academicClassId)
        {
            //TODO:Hasib Avoid Includes 
            var results = _repository.Query(c => c.AcademicClassId == academicClassId
                && c.AcademicSessionId == academicSessionId
                && c.AcademicClassId == academicClassId
                && c.InstituteId == instituteId)
                .Include(s => s.SubjectAcademicClassMappingSubjectTypes)
                //.Include(s => s.SubjectAcademicClassMappingSubjectTypes.Select(sd=>sd.SubjectType))
                //  .Include(s => s.SubjectAcademicClassMappingAcademicGroups)
               .Select();
            return results;
        }
    }

}
