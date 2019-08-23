using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using deepp.Entities.Models;
using deepp.utility;
using Repository.Pattern.Repositories;
using Service.Pattern;
using deepp.Entities.ViewModels;
using deepp.Service.Settings;

namespace deepp.Service
{

    /// <summary>
    ///  Student Service
    /// </summary>
    public interface IStudentService : IService<Student>
    {
        /// ---   [ Get All Student ]   ---
        /// <summary>
        ///     Gets all student.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="student">The student.</param>
        /// <returns></returns>
        IEnumerable<Student> GetAllStudent(int instituteId, Student student = null);

        IEnumerable<Student> GetAllStudent(int classId);

        /// ---   [ Gets the student by identifier ]   ---
        /// <summary>
        ///     Gets the student by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Student GetStudentById(int id);

        /// ---   [ New the student ]   ---
        /// <summary>
        ///     New the student.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Student</returns>
        Student NewStudent(int instituteId, int userId, int classid );

        /// ---   [ Gets all student branch class section wise ]   ---
        /// <summary>
        ///     Gets all student branch class section wise.
        /// </summary>
        /// <param name="vmSearchStudentAttendance">The vm search student attendance.</param>
        /// <param name="instituteId">The institute identifier.</param>
        /// <returns></returns>
        IEnumerable<Student> GetAllStudentBranchClassSectionWise(VmStudentAttendance vmStudentAttendance,
            int instituteId);

        /// ---   [ Gets all student branch class section wise for print ]   ---
        /// <summary>
        ///     Gets all student branch class section wise for print.
        /// </summary>
        /// <param name="vmStudentAttendance">The vm student attendance.</param>
        /// <param name="instituteId">The institute identifier.</param>
        /// <returns></returns>
        IEnumerable<Student> GetAllStudentBranchClassSectionWiseForPrint(VmSearchAttendance vmStudentAttendance,
            int instituteId);

        /// <summary>
        /// Gets all short message detail.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="student">The student.</param>
        /// <returns></returns>
        IEnumerable<ShortMessageDetail> GetAllShortMessageDetail(int instituteId,int userId, Student student = null);
    }
    /// <summary>
    ///  Student Service
    /// </summary>
    public class StudentService : Service<Student>, IStudentService
    {
        #region  "  -  [  Constractor  ]  -  "

        private readonly IRepositoryAsync<Student> _redeeppitory;
        private readonly IAcademicBranchService _academicBranchService;
        private readonly IAcademicClassService _academicClassService;
        private readonly IAcademicGroupService _academicGroupService;
        private readonly IAcademicSectionService _academicSectionService;
        private readonly IAcademicClassSectionMappingService _academicClassSectionMappingService;
        private readonly IAcademicSessionService _academicSessionService;
        private readonly IAcademicVersionService _academicVersionService;
        private readonly IAcademicShiftService _academicShiftService;
        private readonly IAcademicBranchesOfUserInfoService _academicBranchesOfUserInfoService;
        private readonly ICoCurricularActivityService _coCurricularActivityService;
        private readonly IScholarshipService _scholarshipService;

        public StudentService(IRepositoryAsync<Student> redeeppitory,
            IAcademicBranchService academicBranchService,
            IAcademicClassService academicClassService,
            IAcademicGroupService academicGroupService,
            IAcademicSectionService academicSectionService,
            IAcademicClassSectionMappingService academicClassSectionMappingService,
            IAcademicSessionService academicSessionService,
            IAcademicVersionService academicVersionService,
            IAcademicShiftService academicShiftService,
            IAcademicBranchesOfUserInfoService academicBranchesOfUserInfoService,
            ICoCurricularActivityService coCurricularActivityService,
            IScholarshipService scholarshipService
            )
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
            _academicBranchService = academicBranchService;
            _academicClassService = academicClassService;
            _academicGroupService = academicGroupService;
            _academicSectionService = academicSectionService;
            _academicClassSectionMappingService = academicClassSectionMappingService;
            _academicSessionService = academicSessionService;
            _academicVersionService = academicVersionService;
            _academicShiftService = academicShiftService;
            _academicBranchesOfUserInfoService = academicBranchesOfUserInfoService;
            _coCurricularActivityService = coCurricularActivityService;
            _scholarshipService = scholarshipService;
        }

        #endregion

        #region "  -  [  Crud  ]  -  "


        /// <summary>
        ///     New the student.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Student</returns>
        public Student NewStudent(int instituteId, int userId,int classid=0)
        {
            // --- Branch List ---
            var lstacademicBranchKv = new List<KeyValuePair<int, string>>();
            _academicBranchesOfUserInfoService.GetAcademicBranchesByUserId(userId)
                .ToList()
                .ForEach(item => lstacademicBranchKv.Add(new KeyValuePair<int, string>(item.Id, item.Name)));
            // --- Class List ---
            var lstAcademicClassList = new List<KeyValuePair<int, string>>();
            _academicClassService.GetAcademicClassesByInstituteId(instituteId)
                .ToList()
                .ForEach(item => lstAcademicClassList.Add(new KeyValuePair<int, string>(item.Id, item.Name)));
            // --- Group List ---
            var lstAcademicGroupList = new List<KeyValuePair<int, string>>();
            _academicGroupService.GetActiveGetAcademicGroupsByInstituteId(instituteId)
                .ToList()
                .ForEach(item => lstAcademicGroupList.Add(new KeyValuePair<int, string>(item.Id, item.Name)));
            // --- Section List ---
            var lstAcademicSectionList = new List<KeyValuePair<int, string>>();

            if(classid>0){
              lstAcademicSectionList = _academicClassSectionMappingService.Getkvp(instituteId,classid);
               
            }
            
            // --- Session List ---
            var lstacademicSessionList = new List<KeyValuePair<int, string>>();
            _academicSessionService.GetAcademicSessionByInstituteId(instituteId)
                .ToList()
                .ForEach(item => lstacademicSessionList.Add(new KeyValuePair<int, string>(item.Id, item.Name)));
            // --- Version List ---
            var lstacademicVersionList = new List<KeyValuePair<int, string>>();
            _academicVersionService.GetActiveAcademicVersionByInstituteId(instituteId)
                .ToList()
                .ForEach(item => lstacademicVersionList.Add(new KeyValuePair<int, string>(item.Id, item.Name)));
            // --- Shift List ---
            var lstAcademicShiftList = new List<KeyValuePair<int, string>>();
            _academicShiftService.GetActiveAcademicShiftByInstituteId(instituteId)
                .ToList()
                .ForEach(item => lstAcademicShiftList.Add(new KeyValuePair<int, string>(item.Id, item.Name)));
            // --- coCurricularActivity List ---
            var coCurricularActivityList = new List<KeyValuePair<int, string>>();
            _coCurricularActivityService.GetCoCurricularActivityByInstituteId(instituteId, true)
                .ToList()
                .ForEach(item => coCurricularActivityList.Add(new KeyValuePair<int, string>(item.Id, item.Name)));
            // --- coCurricularActivity List ---
            var scholarshipsList = _scholarshipService.GetScholarshipByInstituteId(instituteId, true)
                .Select(item => new KeyValuePair<int, string>(item.Id, item.Name)).ToList();


            var objStudent = new Student
            {
                AcademicBranchList = lstacademicBranchKv,
                AcademicClassList = lstAcademicClassList,
                AcademicGroupList = lstAcademicGroupList,
                AcademicSectionList = lstAcademicSectionList,
                AcademicSessionList = lstacademicSessionList,
                AcademicVerssionList = lstacademicVersionList,
                AcademicShiftList = lstAcademicShiftList,
                CoCurricularActivityList = coCurricularActivityList,
                ScholarshipList = scholarshipsList
            };
            return objStudent;
        }

        /// ---   [ Get All Student ]   ---
        /// <summary>
        ///     Gets all student.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="student">The student.</param>
        /// <returns></returns>
        public IEnumerable<Student> GetAllStudent(int instituteId, Student student = null)
        {
            Expression<Func<Student, bool>> predicate = PredicateBuilder.True<Student>();
            predicate = predicate.And(p => p.UserInfo.InstituteId == instituteId);
            if (student != null)
            {
                if (student.UserInfo != null)
                {
                    string searchText = String.IsNullOrEmpty(student.UserInfo.Name) ? "" : student.UserInfo.Name;
                    predicate =
                        predicate.And(
                            p =>
                                p.UserInfo.Name.Contains(searchText) || p.CurrentRollNo.Contains(searchText) ||
                                p.StudentId.ToString().Contains(searchText));
                }

                if (student.CurrentAcademicBranchId > 0)
                    predicate = predicate.And(p => p.CurrentAcademicBranchId == student.CurrentAcademicBranchId);

                if (student.CurrentAcademicClassId > 0)
                    predicate = predicate.And(p => p.CurrentAcademicClassId == student.CurrentAcademicClassId);

                if (student.CurrentAcademicSectionId > 0)
                    predicate = predicate.And(p => p.CurrentAcademicSectionId == student.CurrentAcademicSectionId);

                if (student.CurrentAcademicSessionId > 0)
                    predicate = predicate.And(p => p.CurrentAcademicSessionId == student.CurrentAcademicSessionId);
            }

            var students =_redeeppitory.Query(predicate)
                .Include(s => s.UserInfo)             
                .Include(s => s.AcademicBranch)
                .Include(s => s.AcademicClass)
                 .Include(s => s.AcademicClassSectionMapping)
                .Include(s => s.AcademicClassSectionMapping.AcademicSection)
                .Include(s => s.AcademicSession).Select();
            return students;
        }

        /// <summary>
        /// Gets all user information.
        /// </summary>
        /// <param name="instituteId">The institute identifier.</param>
        /// <param name="userId"></param>
        /// <param name="student">The student.</param>
        /// <returns></returns>
        public IEnumerable<ShortMessageDetail> GetAllShortMessageDetail(int instituteId,int userId, Student student = null)
        {
            
            Expression<Func<Student, bool>> predicate = PredicateBuilder.True<Student>();
            predicate = predicate.And(p => p.UserInfo.InstituteId == instituteId);
            if (student != null)
            {
                if (student.UserInfo != null)
                {
                    string searchText = String.IsNullOrEmpty(student.UserInfo.Name) ? "" : student.UserInfo.Name;
                    predicate =
                        predicate.And(
                            p =>
                                p.UserInfo.Name.Contains(searchText) || p.CurrentRollNo.Contains(searchText) ||
                                p.StudentId.ToString().Contains(searchText));
                }

                if (student.CurrentAcademicBranchId > 0)
                {
                    predicate = predicate.And(p => p.CurrentAcademicBranchId == student.CurrentAcademicBranchId);
                    
                }
                else
                {
                   var branchids = _academicBranchesOfUserInfoService.GetAcademicBranchesByUserId(userId).Select(s=>s.Id).ToArray();
                   predicate = predicate.And(p => p.CurrentAcademicBranchId.HasValue && branchids.Contains(p.CurrentAcademicBranchId.Value));
                }
                if (student.CurrentAcademicClassId > 0)
                    predicate = predicate.And(p => p.CurrentAcademicClassId == student.CurrentAcademicClassId);

                if (student.CurrentAcademicSectionId > 0)
                    predicate = predicate.And(p => p.CurrentAcademicSectionId == student.CurrentAcademicSectionId);

                if (student.CurrentAcademicSessionId > 0)
                    predicate = predicate.And(p => p.CurrentAcademicSessionId == student.CurrentAcademicSessionId);
            }

            var result = _redeeppitory.Query(predicate)
                .Include(s => s.UserInfo)
                .Select();
            return result
                .Select(s=>new ShortMessageDetail(){ UserInfoId = s.UserInfo.Id, MobileNumber = s.UserInfo.ContactNumber1, UserInfoName = s.UserInfo.Name});
        }

        /// ---   [ Gets all student branch class section wise ]   ---
        /// <summary>
        ///     Gets all student branch class section wise.
        /// </summary>
        /// <param name="vmSearchStudentAttendance">The vm search student attendance.</param>
        /// <param name="instituteId">The institute identifier.</param>
        /// <returns></returns>
        public IEnumerable<Student> GetAllStudentBranchClassSectionWise(VmStudentAttendance vmSearchStudentAttendance,
            int instituteId)
        {
            int selectedBrachId = vmSearchStudentAttendance.StudentAttendance.AcademicBranchId == null ? 0 : Convert.ToInt16(vmSearchStudentAttendance.StudentAttendance.AcademicBranchId);
            int selectedClassId = vmSearchStudentAttendance.StudentAttendance.AcademicClassId;
            int? selectedSectionId = vmSearchStudentAttendance.StudentAttendance.AcademicSectionId;

            Expression<Func<Student, bool>> predicate = PredicateBuilder.True<Student>();
            predicate = predicate.And(p => p.UserInfo.InstituteId == instituteId && p.UserInfo.IsActive);

            if (vmSearchStudentAttendance != null)
            {
                if (selectedBrachId > 0)
                    predicate = predicate.And(p => p.CurrentAcademicBranchId == selectedBrachId);

                if (selectedClassId > 0)
                    predicate = predicate.And(p => p.CurrentAcademicClassId == selectedClassId);

                if (selectedSectionId != 0 && selectedSectionId != null)
                    predicate = predicate.And(p => p.CurrentAcademicSectionId == selectedSectionId);
            }
            return _redeeppitory.Query(predicate)
                .Include(s => s.UserInfo).Select();
        }

        /// ---   [ Gets all student branch class section wise for print ]   ---
        /// <summary>
        ///     Gets all student branch class section wise for print.
        /// </summary>
        /// <param name="vmStudentAttendance">The vm student attendance.</param>
        /// <param name="instituteId">The institute identifier.</param>
        /// <returns></returns>
        public IEnumerable<Student> GetAllStudentBranchClassSectionWiseForPrint(VmSearchAttendance vmStudentAttendance,
            int instituteId)
        {
            int selectedBrachId = vmStudentAttendance.BranchId;
            int selectedClassId = vmStudentAttendance.ClassId;
            int? selectedSectionId = vmStudentAttendance.SectionId;

            Expression<Func<Student, bool>> predicate = PredicateBuilder.True<Student>();
            predicate = predicate.And(p => p.UserInfo.InstituteId == instituteId && p.UserInfo.IsActive);

            if (vmStudentAttendance != null)
            {
                if (selectedBrachId > 0)
                    predicate = predicate.And(p => p.CurrentAcademicBranchId == selectedBrachId);

                if (selectedClassId > 0)
                    predicate = predicate.And(p => p.CurrentAcademicClassId == selectedClassId);

                if (selectedSectionId != 0 && selectedSectionId != null)
                    predicate = predicate.And(p => p.CurrentAcademicSectionId == selectedSectionId);
            }
            return _redeeppitory.Query(predicate)
                .Include(s => s.UserInfo).Select();
        }

        /// ---   [ Gets the student by identifier ]   ---
        /// <summary>
        ///     Gets the student by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Student GetStudentById(int id)
        {
            Student student = _redeeppitory.Query(x => x.StudentId == id).Include(x=>x.UserInfo)
                .Include(x=>x.AcademicClass)
                .Include(x => x.AcademicClassSectionMapping)
                .Include(x => x.AcademicClassSectionMapping.AcademicSection)
                .Select().SingleOrDefault();

            return student;
        }

        public IEnumerable<Student> GetAllStudent(int classId)
        {

            return _redeeppitory
                .Query(c => c.CurrentAcademicClassId == classId && c.IsCurrent != true)
                .Include(c=> c.UserInfo)
                .Select();
        }
        #endregion
    }

}
