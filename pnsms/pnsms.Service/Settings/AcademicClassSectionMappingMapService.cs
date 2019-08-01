using pnsms.Entities.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service.Settings
{
   
    public interface IAcademicClassSectionMappingService
    {
        IEnumerable<AcademicClassSectionMapping> Get(int instituteId, int academicBranchId, int? academicShiftId);
        IEnumerable<AcademicClassSectionMapping> Get(int instituteId, int academicBranchId, int? academicShiftId, int classId);

        AcademicClassSectionMapping Get(int instituteId, int academicBranchId, int? academicShiftId, int classId,
            int sectionId);

        IEnumerable<AcademicClassSectionMapping> Get(int instituteId, int teacherId);
        AcademicClassSectionMapping Get(int id);
        IEnumerable<AcademicClassSectionMapping> GetAll(int instituteId,int academicBranchId, int classId, int? shiftId);
        IEnumerable<AcademicClassSectionMapping> GetAll(int instituteId, int classId);
        IEnumerable<AcademicClassSectionMapping> GetAll(int instituteId);
        IEnumerable<AcademicClassSectionMapping> GetAllForApps(int instituteId);
        List<KeyValuePair<int, string>> Getkvp(int instituteId, int classId);
      
        void Insert(int InstituteId ,IUnitOfWorkAsync unitOfWorkAsync, List<AcademicClassSectionMapping> academicClassSectionMapping);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, AcademicClassSectionMapping RoutinePeriodByClass);

        AcademicClassSectionMapping New(int instituteId);
    }
    public class AcademicClassSectionMappingService : Service<AcademicClassSectionMapping>, IAcademicClassSectionMappingService
    {
        private readonly IRepositoryAsync<AcademicClassSectionMapping> repository;
        private readonly IAcademicBranchService _academicBranchService;
        private readonly IAcademicClassService _academicClassService;
        private readonly IAcademicShiftService _academicShiftService;
        private readonly IAcademicSectionService _academicSectionService;
        private readonly IEmployeeService _employeeService;
        private readonly IAcademicGroupService _academicGroupService;

        public AcademicClassSectionMappingService(IRepositoryAsync<AcademicClassSectionMapping> repository, IAcademicBranchService academicBranchService,
            IAcademicClassService academicClassService, IAcademicShiftService academicShiftService, IAcademicSectionService academicSectionService, IEmployeeService employeeService, IAcademicGroupService academicGroupService)
            : base(repository)
        {
            this.repository = repository;
            _academicBranchService = academicBranchService;
            _academicClassService = academicClassService;
            _academicShiftService = academicShiftService;
            _academicSectionService = academicSectionService;
            _employeeService = employeeService;
            _academicGroupService = academicGroupService;
        }

        //TODO: refactor multiple overloading funtion.
        public IEnumerable<AcademicClassSectionMapping> Get(int instituteId, int academicBranchId, int? academicShiftId)
        {
            return repository.Query(c => c.InstituteId == instituteId
                && c.AcademicBranchId == academicBranchId
                && c.AcademicShiftId == academicShiftId)
                .Select();
        }

        public IEnumerable<AcademicClassSectionMapping> Get(int instituteId, int academicBranchId, int? academicShiftId, int classId)
        {
            return repository.Query(c => c.InstituteId == instituteId
                && c.AcademicBranchId == academicBranchId
                && c.AcademicShiftId == academicShiftId
                && c.AcademicClassId == classId)
                .Select();
        }
        public AcademicClassSectionMapping Get(int instituteId, int academicBranchId, int? academicShiftId, int classId, int sectionId)
        {
            return repository.Query(c => c.InstituteId == instituteId
                && c.AcademicBranchId == academicBranchId
                && c.AcademicShiftId == academicShiftId
                && c.AcademicClassId == classId
                && c.AcademicSectionId == sectionId)
                .Select().SingleOrDefault();
        }

        public AcademicClassSectionMapping Get(int id)
        {
            return repository.Query(c => c.Id == id)
                .Select().SingleOrDefault();
        }

        /// <summary>
        /// Gets the specified institute identifier.
        /// Used in ExammarkEntryCocuricular
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="academicBranchId">The academic branch identifier.</param>
        /// <param name="academicShiftId">The academic shift identifier.</param>
        /// <param name="classId">The class identifier.</param>
        /// <param name="sectionId">The section identifier.</param>
        /// <param name="teacherId">The teacher identifier.</param>
        /// <returns></returns>
        public IEnumerable<AcademicClassSectionMapping> Get(int instituteId, int teacherId)
        {
            var result = repository.Query(c => c.InstituteId == instituteId
                && (c.ClassTeacherId == teacherId || c.AssClassTeacherId == teacherId)).Include(c => c.AcademicClass).Include(c => c.AcademicSection)
                .Select();
            foreach (var item in result)
            {
                item.SectionName = item.AcademicClass.Name + "-" + item.AcademicSection.Name;
            }
            return result;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="branchId">The branch identifier.</param>
        /// <param name="classId">The class identifier.</param>
        /// <param name="shiftId">The shift identifier.</param>
        /// <returns></returns>
        public IEnumerable<AcademicClassSectionMapping> GetAll(int instituteId,int branchId,int classId, int? shiftId)
        {
            var result = repository.Query(c => c.InstituteId == instituteId && (c.AcademicBranchId == branchId)
                && (c.AcademicClassId == classId )).Include(c => c.AcademicClass).Include(c => c.AcademicSection)
                .Select().ToList();
            var sections = _academicSectionService.GetKVP(instituteId).Select(s => new AcademicClassSectionMapping()
            {
                AcademicBranchId = branchId,
                AcademicClassId = classId,
                AcademicShiftId = shiftId,
                AcademicSectionId = s.Key,
                Name = s.Value,
                InstituteId = instituteId
               
            }).ToList();

            if (result.Any())
            {

                foreach (var r in result)
                {
                    foreach (var s in sections)
                    {
                        if (r.AcademicBranchId == s.AcademicBranchId && r.AcademicClassId == s.AcademicClassId &&
                            r.AcademicSectionId == s.AcademicSectionId)
                        {
                            s.Id = r.Id;
                            s.SectionName = r.SectionName;
                            s.AcademicSection = r.AcademicSection;
                            s.ClassTeacherId = r.ClassTeacherId;
                            s.AssClassTeacherId = r.AssClassTeacherId;
                            s.AcademicGroupId = r.AcademicGroupId;
                            s.IsActive = true;
                        }

                    }
                }



            }


            return sections;
        }
        public IEnumerable<AcademicClassSectionMapping> GetAll(int instituteId, int classId)
        {
            var result = repository.Query(c => c.InstituteId == instituteId
                && (c.AcademicClassId == classId)).Include(c => c.AcademicClass)
                .Include(c => c.AcademicSection)                
                .Select().ToList();


            return result;
        }

      
        public List<KeyValuePair<int,string>> Getkvp(int instituteId, int classId)
        {

            var kvpAcademicClassSectionMapping = new List<KeyValuePair<int, string>>();
             repository.Query(c => c.InstituteId == instituteId
                && (c.AcademicClassId == classId))
                .Include(x=>x.AcademicSection)
                .Select().ToList().ForEach(c => kvpAcademicClassSectionMapping.Add(new KeyValuePair<int,string>(c.Id,c.AcademicSection.Name)));


            return kvpAcademicClassSectionMapping;
        }
        /// <summary>
        /// Gets all.
        /// use for getting all mapping data for global data
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <returns></returns>
        public IEnumerable<AcademicClassSectionMapping> GetAll(int instituteId)
        {
            var result = repository.Query(c => c.InstituteId == instituteId)
                .Include(s => s.AcademicSection)
                .Include(s => s.AcademicShift)
                .Include(s => s.AcademicClass).Select();

            return result;
        }
        public IEnumerable<AcademicClassSectionMapping> GetAllForApps(int instituteId)
        {
            var result = repository.Query(c => c.InstituteId == instituteId) .Select();

            return result;
        }

        public void Insert(int InstituteId,IUnitOfWorkAsync unitOfWorkAsync, List<AcademicClassSectionMapping> academicClassSectionMapping)
        {
            try
            {
                unitOfWorkAsync.BeginTransaction(isolationLevel: System.Data.IsolationLevel.Unspecified);
                foreach (var objAcademicClassSectionMapping in academicClassSectionMapping)
                {
                    AcademicClassSectionMapping objAcademicClassSectionMapping1 = new AcademicClassSectionMapping();
                    objAcademicClassSectionMapping1.Id = objAcademicClassSectionMapping.Id;

                    objAcademicClassSectionMapping1.InstituteId =InstituteId;
                    objAcademicClassSectionMapping1.IsActive = objAcademicClassSectionMapping.IsActive;
                    objAcademicClassSectionMapping1.Name = objAcademicClassSectionMapping.Name;
                    objAcademicClassSectionMapping1.SectionName = objAcademicClassSectionMapping.SectionName;
                    objAcademicClassSectionMapping1.AcademicBranchId = objAcademicClassSectionMapping.AcademicBranchId;
                    objAcademicClassSectionMapping1.AcademicClassId = objAcademicClassSectionMapping.AcademicClassId;
                    objAcademicClassSectionMapping1.AcademicSectionId = objAcademicClassSectionMapping.AcademicSectionId;
                    objAcademicClassSectionMapping1.AcademicShiftId = objAcademicClassSectionMapping.AcademicShiftId;



                    if (objAcademicClassSectionMapping1.Id > 0)
                    {
                        if (objAcademicClassSectionMapping1.IsActive)
                        {
                            repository.Update(objAcademicClassSectionMapping1);

                        }
                        else
                        {
                            repository.Delete(objAcademicClassSectionMapping1);
                        }
                    }
                    else
                    {
                        if (objAcademicClassSectionMapping1.IsActive)
                        {
                            repository.Insert(objAcademicClassSectionMapping1);

                        }


                    }
                }

                unitOfWorkAsync.SaveChanges();

                unitOfWorkAsync.Commit();
            }
            catch (Exception e)
            {
                unitOfWorkAsync.Rollback();
                throw e;

            }
        }

        public void Update(IUnitOfWorkAsync unitOfWorkAsync, AcademicClassSectionMapping RoutinePeriodByClass)
        {
            try
            {
                unitOfWorkAsync.BeginTransaction(isolationLevel: System.Data.IsolationLevel.Unspecified);

                repository.Update(RoutinePeriodByClass);
                unitOfWorkAsync.SaveChanges();

                unitOfWorkAsync.Commit();
            }
            catch (Exception)
            {
                unitOfWorkAsync.Rollback();
                throw;

            }
        }

        public AcademicClassSectionMapping New(int instituteId)
        {
            var routinePeriodByClass = new AcademicClassSectionMapping();
            routinePeriodByClass.AcademicBranchList = _academicBranchService.GetKVP(instituteId);
            routinePeriodByClass.AcademicClassList = _academicClassService.GetKVP(instituteId);
          //  routinePeriodByClass.AcademicSectionList = _academicShiftService.GetKVP(instituteId);
           // routinePeriodByClass.AcademicShiftList = _academicShiftService.GetKVP(instituteId);
           // routinePeriodByClass.AcademicGroupList = _academicGroupService.GetKVP(instituteId);
            //RoutinePeriodByClass.RoutinePeriodList= _routinePeriodService. 
            //routinePeriodByClass.TeacherList = _employeeService.GetKVP(instituteId);

            return routinePeriodByClass;
        }
    }
}
