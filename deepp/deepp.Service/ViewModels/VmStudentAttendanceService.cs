using deepp.Entities.Models;
using deepp.Entities.ResponseModel;
using deepp.Entities.ViewModels;
using deepp.Entities.ViewModels.Attendance;
using deepp.Service.Settings;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace deepp.Service.ViewModels
{
    public interface IVmStudentAttendanceService
    {
        List<TakeAttendance> GetVmStudentAttendanceByTeacher(int instituteId, int sessionsId, int userId);
        VmStudentAttendance GetVmStudentAttendanceByManagement(int instituteId, int sessionsId, int userId);

        /// <summary>
        /// Apps data  Sync Method
        /// </summary>
        /// <param name="vmStudentAttendance"></param>
        /// <param name="unitOfWorkAsync"></param>
        /// <param name="InstituteId"></param>
        /// <param name="AcademicSessionId"></param>
        /// <returns></returns>
        SynStudentAttendanceReponseModel CreateUpdateStudentAttendance(List<StudentAttendance> lstStudentAttendance, IUnitOfWorkAsync unitOfWorkAsync, int InstituteId, int AcademicSessionId, int userId);
        VmStudentAttendance CreateStudentAttendance(VmStudentAttendance vmStudentAttendance, IUnitOfWorkAsync unitOfWorkAsync, int InstituteId);
        VmStudentAttendance GetVmStudentList(VmStudentAttendance vmStudentAttendance, int instituteId);
        VmSearchAttendance GeAttendanceList(VmSearchAttendance vmSearchAttendance, int instituteId);
        VmStudentAttendance GetVmStudentAttendanceEdit(int attendanceId, int instituteId);
        VmStudentAttendance UpdateStudentAttendance(VmStudentAttendance vmStudentAttendance, IUnitOfWorkAsync unitOfWorkAsync, int InstituteId);
        VmStudentAttendance UpdateAsscondingStudentAttendance(VmStudentAttendance vmStudentAttendance, int instituteId, IUnitOfWorkAsync unitOfWorkAsync);
        VmStudentAttendance GetVmStudentAttendanceDetails(int attendanceId, int instituteId);
        VmSearchAttendance GeAttendanceSheetPrint(VmSearchAttendance vmSearchAttendance, int instituteId);

    }

    public class VmStudentAttendanceService : IVmStudentAttendanceService
    {

        public static SynStudentAttendanceReponseModel objSynStudentAttendanceReponseModel = new SynStudentAttendanceReponseModel();
        private readonly IAcademicBranchService _branchService;
        private readonly IAcademicClassService _classService;
        private readonly IAcademicGroupService _groupService;
        private readonly IAcademicSectionService _sectionService;
        private readonly IAcademicShiftService _shiftService;
        private readonly IAcademicPeriodService _academicPeriodService;
        private readonly IStudentService _academicStudentService;
        private readonly IAttendanceTypeService _attendanceTypeService;
        private readonly IStudentAttendanceService _studentAttendanceService;
        private readonly IStudentAttendanceDetailService _studentAttendanceDetailService;
        private readonly ITeacherService _teacherService;
        private readonly IInstituteService _instituteService;
        private readonly IImageService _imageService;
        private readonly IAcademicClassSectionMappingService _academicClassSectionMappingService;
     
        public VmStudentAttendanceService(
              IAcademicBranchService branchService
            , IAcademicClassService classService
            , IAcademicGroupService groupService
            , IAcademicSectionService sectionService
            , IAcademicClassSectionMappingService academicClassSectionMappingService
            , IAcademicShiftService shiftService
            , IAcademicPeriodService academicPeriodService
            , IStudentService academicStudentService
            , IAttendanceTypeService attendanceTypeService
            , IStudentAttendanceService studentAttendanceService
            , IStudentAttendanceDetailService studentAttendanceDetailService
            , ITeacherService teacherService
            , IInstituteService instituteService
            , IImageService imageService            
            )
        {
            _branchService = branchService;
            _classService = classService;
            _sectionService = sectionService;

            _groupService = groupService;
            _shiftService = shiftService;
            _academicPeriodService = academicPeriodService;
            _academicStudentService = academicStudentService;
            _attendanceTypeService = attendanceTypeService;
            _studentAttendanceService = studentAttendanceService;
            _studentAttendanceDetailService = studentAttendanceDetailService;
            _teacherService = teacherService;
            _instituteService = instituteService;
            _imageService = imageService;
            _academicClassSectionMappingService = academicClassSectionMappingService;
            
        }

        public bool IsTakenAttendance(StudentAttendance item)
        {


            var objStudentAttendance = _studentAttendanceService.GetStudentAttendance(item);
            if (objStudentAttendance == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        public List<TakeAttendance> GetVmStudentAttendanceByTeacher(int instituteId, int sessionsId, int userId)
        {
            // TakeAttendance vmStudentAttendance = new VmStudentAttendance();

            //  vmStudentAttendance.StudentAttendance = new StudentAttendance();
            List<TakeAttendance> lstTakeAttendance = new List<TakeAttendance>();
            
            //var branchList = _branchService.GetKVP(instituteId);
            //subjectAcademicClassMappinglst.ForEach(delegate(SubjectAcademicClassMapping item)
            //{
            //    TakeAttendance objTakeAttendance = new TakeAttendance();
            //    objTakeAttendance.AcademicClassId = item.AcademicClassId;
            //    objTakeAttendance.AcademicClassName = item.AcademicClass.Name;
            //    objTakeAttendance.Attendancedate = DateTime.Now;
            //    objTakeAttendance.AcademicBranchId = item.AcademicBranchId;
            //    objTakeAttendance.AcademicBranchName = item.AcademicBranch.Name;
            //    objTakeAttendance.AcademicSectionName = item.AcademicClassSectionMapping.AcademicSection.Name;
            //    objTakeAttendance.AcademicClassSectionMapId = item.AcademicClassSectionMapId;
            //    objTakeAttendance.SubjectAcademicClassMappingsId = item.Id;

            //    objTakeAttendance.SubjectId = item.SubjectId;
            //    objTakeAttendance.SubjectName = item.InstituteSubject.Name;
            //    objTakeAttendance.TeacherId = item.TeacherId;
            //    objTakeAttendance.TeacherName = item.Teacher.UserInfo.Name;

            //    StudentAttendance objStudentAttendance = new StudentAttendance();
            //    objStudentAttendance.AttendanceDate = objTakeAttendance.Attendancedate;
            //    objStudentAttendance.InstituteId = instituteId;
            //    objStudentAttendance.TeacherId = userId;
            //    objStudentAttendance.AcademicSessionId = sessionsId;
            //    objStudentAttendance.AcademicClassId = item.AcademicClassId;
            //    objStudentAttendance.AcademicSessionId = item.AcademicSessionId;
            //    objStudentAttendance.AcademicSectionId = item.AcademicClassSectionMapId;
            //    objStudentAttendance.AcamedicGroupId = item.AcademicGroupId;
            //    objStudentAttendance.SubjectAcademicClassMappingsId = item.Id;
            //    objStudentAttendance.AcademicClassId = item.AcademicClassId;

            //    objTakeAttendance.IsTakenAttendance = IsTakenAttendance(objStudentAttendance);
            //    lstTakeAttendance.Add(objTakeAttendance);

            //});

            //vmStudentAttendance.StudentAttendance.AcademicPeriodList = _academicPeriodService.GetKVP(instituteId);

            //var classlist = subjectAcademicClassMappinglst.Select(x => x.AcademicClass).Distinct();
            //var classlistkvp = new List<KeyValuePair<int, string>>();
            //classlist.ToList().ForEach(x => classlistkvp.Add(new KeyValuePair<int, string>(x.Id, x.Name)));
            //vmStudentAttendance.StudentAttendance.AcademicClassList = classlistkvp;
            //vmStudentAttendance.StudentAttendance.AcademicBranchList = branchList;
            //vmStudentAttendance.StudentAttendance.TeacherId = subjectAcademicClassMappinglst.FirstOrDefault().TeacherId;
            //vmStudentAttendance.StudentAttendance.TeacherName = subjectAcademicClassMappinglst.FirstOrDefault().Teacher.UserInfo.Name;
            //vmStudentAttendance.StudentAttendance.AttendanceDate = DateTime.Now;
            //  vmStudentAttendance.lstSubjectAcademicClassMapping = subjectAcademicClassMappinglst;
            return lstTakeAttendance;
        }
        public VmStudentAttendance GetVmStudentAttendanceByManagement(int instituteId, int sessionsId, int userId)
        {
            VmStudentAttendance vmStudentAttendance = new VmStudentAttendance();

            vmStudentAttendance.StudentAttendance = new StudentAttendance();
            var branchList = _branchService.GetKVP(instituteId);
            vmStudentAttendance.StudentAttendance.AcademicBranchList = branchList;
            vmStudentAttendance.StudentAttendance.AcademicBranchId = branchList.FirstOrDefault().Key;
            vmStudentAttendance.StudentAttendance.AcademicClassList = _classService.GetKVP(instituteId);
            vmStudentAttendance.StudentAttendance.AcademicSectionList = new List<KeyValuePair<int, string>>();//_sectionService.GetKVP(instituteId);
            vmStudentAttendance.StudentAttendance.AcademicPeriodList = _academicPeriodService.GetKVP(instituteId);
            var data = _teacherService.GetAllTeacher(instituteId, "", vmStudentAttendance.StudentAttendance.AcademicBranchId).ToList();
            var teacherList = new List<KeyValuePair<int, string>>();
            data.ForEach(c => teacherList.Add(new KeyValuePair<int, string>(c.TeacherId, c.UserInfo.Name)));
            vmStudentAttendance.StudentAttendance.TeacherList = teacherList;
            vmStudentAttendance.StudentAttendance.AttendanceDate = DateTime.Now;

            return vmStudentAttendance;
        }
        public VmStudentAttendance GetVmStudentList(VmStudentAttendance vmStudentAttendance, int instituteId)
        {
            try
            {



                var attendanceTypeList = _attendanceTypeService.GetAttendanceTypesForStudent(instituteId).Select(c => new AttendanceTypeModel { AttTypeId = c.Id, IsDefaultAsSelected = c.IsDefault == true ? true : false, Flag = c.Flag, colorCode = c.Colour.ColorCode });
                int selectedAttendanceId = 0;

                foreach (var item1 in attendanceTypeList)
                {
                    if (item1.IsDefaultAsSelected == true) { selectedAttendanceId = item1.AttTypeId; }
                }

                var studentAttendanceDetail = new List<StudentAttendanceDetail>();
                var tempAttendace = _studentAttendanceService.GetStudentAttendance(vmStudentAttendance.StudentAttendance);

                if (tempAttendace != null && tempAttendace.Id != 0)
                {
                    //tempAttendace.Teacher = new Teacher();
                    vmStudentAttendance.StudentAttendance = new StudentAttendance();
                    vmStudentAttendance.StudentAttendance.Id = tempAttendace.Id;
                    vmStudentAttendance.StudentAttendance.InstituteId = instituteId;
                    vmStudentAttendance.StudentAttendance.AbscondingCount = tempAttendace.AbscondingCount;
                    vmStudentAttendance.StudentAttendance.AbscondingPercentage = tempAttendace.AbscondingPercentage;
                    vmStudentAttendance.StudentAttendance.AbsentCount = tempAttendace.AbsentCount;
                    vmStudentAttendance.StudentAttendance.AbsentPercentage = tempAttendace.AbsentPercentage;
                    vmStudentAttendance.StudentAttendance.AcademicBranchId = tempAttendace.AcademicBranchId;
                    vmStudentAttendance.StudentAttendance.AcademicClassId = tempAttendace.AcademicClassId;


                    if (tempAttendace.AcademicPeriod == null)
                    {
                        tempAttendace.AcademicPeriod = new AcademicPeriod();
                    }

                    vmStudentAttendance.StudentAttendance.AcademicPeriodId = tempAttendace.AcademicPeriodId;

                    vmStudentAttendance.StudentAttendance.PeriodName = tempAttendace.AcademicPeriod.Name;

                  
                    vmStudentAttendance.StudentAttendance.SubjectAcademicClassMappingsId = tempAttendace.SubjectAcademicClassMappingsId;


                    vmStudentAttendance.StudentAttendance.AcademicSectionId = tempAttendace.AcademicSectionId;
                    vmStudentAttendance.StudentAttendance.AcademicSessionId = tempAttendace.AcademicSessionId;
                    vmStudentAttendance.StudentAttendance.AcademicShiftId = tempAttendace.AcademicShiftId;
                    vmStudentAttendance.StudentAttendance.AcamedicGroupId = tempAttendace.AcamedicGroupId;
                    vmStudentAttendance.StudentAttendance.AttendanceDate = tempAttendace.AttendanceDate;
                    vmStudentAttendance.StudentAttendance.BranchName = tempAttendace.AcademicBranch.Name;
                    vmStudentAttendance.StudentAttendance.ClassName = tempAttendace.AcademicClass.Name;
                    vmStudentAttendance.StudentAttendance.PresentCount = tempAttendace.PresentCount;
                    vmStudentAttendance.StudentAttendance.PresentPercentage = tempAttendace.PresentPercentage;
                    vmStudentAttendance.StudentAttendance.SectionName = tempAttendace.AcademicClassSectionMapping.AcademicSection.Name;
                    vmStudentAttendance.StudentAttendance.TeacherId = tempAttendace.TeacherId;

                    if (tempAttendace.Teacher == null)
                    {
                        tempAttendace.Teacher = new Teacher();
                        tempAttendace.Teacher.UserInfo = new UserInfo();
                    }
                    vmStudentAttendance.StudentAttendance.TeacherName = tempAttendace.Teacher.UserInfo.Name;
                    vmStudentAttendance.StudentAttendance.TotalCount = tempAttendace.TotalCount;
                    var branchList = _branchService.GetKVP(instituteId);
                    vmStudentAttendance.StudentAttendance.AcademicBranchList = branchList;
                    vmStudentAttendance.StudentAttendance.AcademicBranchId = branchList.FirstOrDefault().Key;
                    vmStudentAttendance.StudentAttendance.AcademicClassList = _classService.GetKVP(instituteId);
                    vmStudentAttendance.StudentAttendance.AcademicSectionList = _sectionService.GetKVP(instituteId);
                    vmStudentAttendance.StudentAttendance.AcademicPeriodList = _academicPeriodService.GetKVP(instituteId);
                    var data = _teacherService.GetAllTeacher(instituteId, "", vmStudentAttendance.StudentAttendance.AcademicBranchId).ToList();
                    var teacherList = new List<KeyValuePair<int, string>>();
                    data.ForEach(c => teacherList.Add(new KeyValuePair<int, string>(c.TeacherId, c.UserInfo.Name)));
                    vmStudentAttendance.StudentAttendance.TeacherList = teacherList;

                    // vmStudentAttendance.StudentAttendance = tempAttendace;
                    var lstAttendanceDetails = _studentAttendanceDetailService.GetListwithAttendanceTypeByAttendaceId(tempAttendace.Id);

                    foreach (StudentAttendanceDetail item in lstAttendanceDetails)
                    {
                        StudentAttendanceDetail entityStudent = new StudentAttendanceDetail();
                        entityStudent.Id = item.Id;
                        entityStudent.InstituteId = instituteId;
                        entityStudent.StudentAttendanceId = item.StudentAttendanceId;
                        entityStudent.StudentName = item.Student.UserInfo.Name;
                        entityStudent.StudentRoll = item.Student.CurrentRollNo;
                        entityStudent.AttendanceTypeId = item.AttendanceTypeId;
                        entityStudent.StudentId = item.Student.StudentId;
                        entityStudent.AttendanceTypes = attendanceTypeList;
                        entityStudent.Comments = entityStudent.Comments;
                        studentAttendanceDetail.Add(entityStudent);
                    }
                    vmStudentAttendance.AttendanceDetails = studentAttendanceDetail;
                }
                else
                {
                    if (vmStudentAttendance.StudentAttendance.AcademicBranchList != null)
                    {
                        vmStudentAttendance.StudentAttendance.BranchName = vmStudentAttendance.StudentAttendance.AcademicBranchList.Find(x => x.Key == vmStudentAttendance.StudentAttendance.AcademicBranchId).Value.ToString();
                    }
                    if (vmStudentAttendance.StudentAttendance.AcademicClassList != null)
                    {
                        vmStudentAttendance.StudentAttendance.ClassName = vmStudentAttendance.StudentAttendance.AcademicClassList.Find(x => x.Key == vmStudentAttendance.StudentAttendance.AcademicClassId).Value.ToString();
                    }
                    if (vmStudentAttendance.StudentAttendance.AcademicSectionList != null)
                    {
                        vmStudentAttendance.StudentAttendance.SectionName = vmStudentAttendance.StudentAttendance.AcademicSectionList.Find(x => x.Key == vmStudentAttendance.StudentAttendance.AcademicSectionId).Value.ToString();
                    }


                    if (vmStudentAttendance.StudentAttendance.TeacherList != null)
                    {
                        vmStudentAttendance.StudentAttendance.TeacherName = vmStudentAttendance.StudentAttendance.TeacherList.Find(x => x.Key == vmStudentAttendance.StudentAttendance.TeacherId).Value.ToString();
                    }
                    if (vmStudentAttendance.StudentAttendance.AcademicPeriodList != null)
                    {
                        vmStudentAttendance.StudentAttendance.PeriodName = vmStudentAttendance.StudentAttendance.AcademicPeriodList.Find(x => x.Key == vmStudentAttendance.StudentAttendance.AcademicPeriodId).Value.ToString();
                    }
                    vmStudentAttendance.StudentAttendance.InstituteId = instituteId;


                    IEnumerable<Student> studentList = _academicStudentService.GetAllStudentBranchClassSectionWise(vmStudentAttendance, instituteId);
                    vmStudentAttendance.StudentAttendance.Id = 0;
                    if (studentList.Count() > 0)
                    {
                        foreach (Student item in studentList)
                        {
                            StudentAttendanceDetail entityStudent = new StudentAttendanceDetail();
                            entityStudent.InstituteId = instituteId;
                            entityStudent.StudentName = item.UserInfo.Name;
                            entityStudent.StudentRoll = item.CurrentRollNo;
                            entityStudent.AttendanceTypeId = selectedAttendanceId;
                            entityStudent.StudentId = item.StudentId;
                            entityStudent.StudentAttendanceId = 0;

                            entityStudent.AttendanceTypes = attendanceTypeList;
                            studentAttendanceDetail.Add(entityStudent);

                        }
                        vmStudentAttendance.AttendanceDetails = studentAttendanceDetail;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return vmStudentAttendance;
        }
        public SynStudentAttendanceReponseModel CreateUpdateStudentAttendanceCommon(StudentAttendance item,List<StudentAttendanceDetail> lstStudentAttendanceDetail, IUnitOfWorkAsync unitOfWorkAsync, int InstituteId, int AcademicSessionId, int userId)
        {
            StudentAttendance objStudentAttendance = new StudentAttendance();
            StudentAttendance objStudentAttendanceResponse = new StudentAttendance();
           // SynStudentAttendanceReponseModel objSynStudentAttendanceReponseModel = new SynStudentAttendanceReponseModel();
            try
            {
                item.StudentAttendanceDetails = null;
                item.InstituteId = InstituteId;
                item.AcademicSessionId = AcademicSessionId;
               // objStudentAttendance.Id = item.Id;
             //   objStudentAttendance.AcademicBranchId = item.AcademicBranchId;
               // objStudentAttendance.InstituteId = InstituteId;
               // objStudentAttendance.AcademicSessionId = AcademicSessionId;
              //  objStudentAttendance.AcademicPeriodId = item.AcademicPeriodId;
              //  objStudentAttendance.AcademicClassId = item.AcademicClassId;
              //  objStudentAttendance.AcademicSectionId = item.AcademicSectionId;
               // objStudentAttendance.LocalId = item.LocalId;
               item.AttendanceDate = Convert.ToDateTime(item.AttendanceDate);
               // objStudentAttendance.AttendanceDate = item.AttendanceDate;

                item.TeacherId = (item.TeacherId == 0) ? userId : item.TeacherId;
              //  objStudentAttendance.TeacherId = item.TeacherId;
                if (item.AcamedicGroupId == 0)
                {
                    item.AcamedicGroupId = null;
                }
               // objStudentAttendance.AcamedicGroupId = item.AcamedicGroupId;
                if (item.AcademicShiftId == 0)
                {
                    item.AcademicShiftId = null;
                }
                //objStudentAttendance.AcademicShiftId = item.AcademicShiftId;

                if (item.SubjectAcademicClassMappingsId == 0)
                {
                    item.SubjectAcademicClassMappingsId = null;
                }
              //  objStudentAttendance.SubjectAcademicClassMappingsId = item.SubjectAcademicClassMappingsId;

                if (item.AcamedicGroupId == 0)
                {
                    item.AcamedicGroupId = null;
                }
             //   objStudentAttendance.AcamedicGroupId = item.AcamedicGroupId;

                item.LastUpdateTime = Convert.ToDateTime(item.LastUpdateTime);
               // objStudentAttendance.LastUpdateTime = item.LastUpdateTime;
              //  objStudentAttendance.SyncDate = DateTime.Now;
                item.SyncDate = DateTime.Now;


                //if (item.Id == 0)
                //{
                //    CreateStudentAttendance(objVmStudentAttendance, unitOfWorkAsync, InstituteId);
                //}
                //else
                //{
                //    UpdateStudentAttendance(objVmStudentAttendance, unitOfWorkAsync, InstituteId);
                //}

                if (item.Id == 0)
                {
                    _studentAttendanceService.Insert(item);
                    // unitOfWorkAsync.SaveChanges();
                }
                else
                {
                   // objStudentAttendance.ObjectState = modifi
                    _studentAttendanceService.Update(item);
                    // unitOfWorkAsync.SaveChanges();
                }


                lstStudentAttendanceDetail.ToList().ForEach(delegate(StudentAttendanceDetail details)
                {



                    StudentAttendanceDetail entityStudent = new StudentAttendanceDetail();

                    if (item.Id == 0)
                    {
                        entityStudent.Id = 0;
                        entityStudent.StudentAttendanceId = 0;
                    }
                    else
                    {
                        entityStudent.Id = details.Id;
                        entityStudent.StudentAttendanceId = item.Id;
                    }
                   
                    entityStudent.InstituteId = InstituteId;
                    entityStudent.StudentId = details.StudentId;
                    entityStudent.Comments = details.Comments;
                    entityStudent.AttendanceTypeId = details.AttendanceTypeId;
                    entityStudent.IsAbsconding = false;
                    entityStudent.LastUpdateTime = DateTime.Now;
                    entityStudent.LocalId = details.LocalId;

                    if (entityStudent.Id == 0)
                    {
                        entityStudent.StudentAttendanceId = item.Id;
                        _studentAttendanceDetailService.Insert(entityStudent);
                        // unitOfWorkAsync.SaveChanges();
                    }
                    else
                    {
                        _studentAttendanceDetailService.Update(entityStudent);
                        // unitOfWorkAsync.SaveChanges();
                    }

                    //  objStudentAttendanceResponse.StudentAttendanceDetails.Add(entityStudent);

                });

                unitOfWorkAsync.SaveChanges();


                IEnumerable<StudentAttendanceDetail> stAttDetailList = _studentAttendanceDetailService.GetListwithAttendanceTypeByAttendaceId(item.Id);
                int presentCount = stAttDetailList.Where(d => d.AttendanceType.IsPresent == true).Count();
                int absentCount = stAttDetailList.Where(d => d.AttendanceType.IsPresent == false).Count();
                int abscondingCount = stAttDetailList.Where(d => d.IsAbsconding == true).Count();
                int totalCount = (item.StudentAttendanceDetails != null) ? item.StudentAttendanceDetails.ToList().Count() : 0;
                decimal presentPercent = (totalCount != 0) ? (presentCount * 100) / totalCount : 0;
                decimal absentPercent = (totalCount != 0) ? (absentCount * 100) / totalCount : 0;
                decimal abscondingPercent = (totalCount != 0) ? (abscondingCount * 100) / totalCount : 0;

                //objStudentAttendance.Id = item.Id;
                item.InstituteId = InstituteId;
                item.TotalCount = totalCount;
                item.PresentCount = presentCount;
                item.AbsentCount = absentCount;
                item.AbscondingCount = abscondingCount;
                item.PresentPercentage = presentPercent;
                item.AbsentPercentage = absentPercent;
                item.AbscondingPercentage = abscondingPercent;
                _studentAttendanceService.Update(item);
                unitOfWorkAsync.SaveChanges();


                //Response model

                objStudentAttendanceResponse.Id = item.Id;
                objStudentAttendanceResponse.LocalId = item.LocalId;
                objStudentAttendanceResponse.AcademicBranchId = item.AcademicBranchId;
                objStudentAttendanceResponse.InstituteId = InstituteId;
                objStudentAttendanceResponse.AcademicSessionId = AcademicSessionId;
                objStudentAttendanceResponse.AcademicPeriodId = item.AcademicPeriodId;
                objStudentAttendanceResponse.AttendanceDate = item.AttendanceDate;
                objStudentAttendanceResponse.LastUpdateTime = item.LastUpdateTime;
                objStudentAttendanceResponse.PresentCount = item.PresentCount;
                objStudentAttendanceResponse.AbsentCount = item.AbsentCount;
                objStudentAttendanceResponse.AbsentPercentage = item.AbsentPercentage;
                objStudentAttendanceResponse.AcademicClassId = item.AcademicClassId;
                objStudentAttendanceResponse.AcademicSectionId = item.AcademicSectionId;

                objStudentAttendanceResponse.StudentAttendanceDetails = new List<StudentAttendanceDetail>();


                stAttDetailList.ToList().ForEach(delegate(StudentAttendanceDetail item1)
                {
                    StudentAttendanceDetail objStudentAttendanceDetail = new StudentAttendanceDetail();
                    objStudentAttendanceDetail.LocalId = item1.LocalId;
                    objStudentAttendanceDetail.StudentAttendanceId = item1.StudentAttendanceId;
                    objStudentAttendanceDetail.Id = item1.Id;
                    objStudentAttendanceDetail.StudentId = item1.StudentId;
                    objStudentAttendanceDetail.AttendanceTypeId = item1.AttendanceTypeId;

                    objStudentAttendanceResponse.StudentAttendanceDetails.Add(objStudentAttendanceDetail);
                });


                objSynStudentAttendanceReponseModel.Success = "true";
                objSynStudentAttendanceReponseModel.Message = "Successfully Save and Update";
                objSynStudentAttendanceReponseModel.Obj = objStudentAttendanceResponse;//.StudentAttendanceDetails.Select(x=> new { x.StudentAttendanceId,x.Id,x.StudentId,x.AttendanceTypeId });
                return objSynStudentAttendanceReponseModel;
            }
            catch (Exception ex)
            {

                objSynStudentAttendanceReponseModel.Success = "false";
                objSynStudentAttendanceReponseModel.Message = ex.ToString();
                objSynStudentAttendanceReponseModel.Obj = objStudentAttendanceResponse;//.StudentAttendanceDetails.Select(x => new { x.StudentAttendanceId, x.Id, x.StudentId, x.AttendanceTypeId });
                return objSynStudentAttendanceReponseModel;
            }
        }


        /// <summary>
        /// Apps Attendance data Sync Method
        /// </summary>
        /// <param name="vmStudentAttendance"></param>
        /// <param name="unitOfWorkAsync"></param>
        /// <param name="InstituteId"></param>
        /// <param name="AcademicSessionId"></param>
        /// <returns></returns>
        public SynStudentAttendanceReponseModel CreateUpdateStudentAttendance(List<StudentAttendance> lstStudentAttendance, IUnitOfWorkAsync unitOfWorkAsync, int InstituteId, int AcademicSessionId, int userId)
        {
            objSynStudentAttendanceReponseModel.lstObj = new List<object>();

            try
            {
                lstStudentAttendance.ForEach(delegate(StudentAttendance item)
                {

                    var oldStudentAttendance = _studentAttendanceService.GetStudentAttendanceForApps(item);
                    if (oldStudentAttendance == null)
                    {
                        item.Id = 0;
                        objSynStudentAttendanceReponseModel = CreateUpdateStudentAttendanceCommon(item,item.StudentAttendanceDetails.ToList(), unitOfWorkAsync, InstituteId, AcademicSessionId, userId);
                    }
                    else if (item.LastUpdateTime >= oldStudentAttendance.LastUpdateTime)
                    {
                        oldStudentAttendance.StudentAttendanceDetails = item.StudentAttendanceDetails;
                        objSynStudentAttendanceReponseModel = CreateUpdateStudentAttendanceCommon(oldStudentAttendance, item.StudentAttendanceDetails.ToList(), unitOfWorkAsync, InstituteId, AcademicSessionId, userId);
                    }
                    else
                    {
                        objSynStudentAttendanceReponseModel.Message = "Attendacne already taken forward data and Time";
                    }
                    objSynStudentAttendanceReponseModel.lstObj.Add(objSynStudentAttendanceReponseModel.Obj);
                });
                return objSynStudentAttendanceReponseModel;
            }
            catch (Exception ex)
            {
                return objSynStudentAttendanceReponseModel;
            }
        }

        public VmStudentAttendance CreateStudentAttendance(VmStudentAttendance vmStudentAttendance, IUnitOfWorkAsync unitOfWorkAsync, int InstituteId)
        {
            //need to be changed
            // vmStudentAttendance.StudentAttendance.AcademicSessionId = 1;

            try
            {


                vmStudentAttendance.StudentAttendance.LastUpdateTime = DateTime.Now;
                _studentAttendanceService.Insert(vmStudentAttendance.StudentAttendance);
                unitOfWorkAsync.SaveChanges();

                if (vmStudentAttendance.AttendanceDetails != null)
                    foreach (var details in vmStudentAttendance.AttendanceDetails)
                    {
                        StudentAttendanceDetail entityStudent = new StudentAttendanceDetail();
                        entityStudent.StudentAttendanceId = vmStudentAttendance.StudentAttendance.Id;
                        entityStudent.InstituteId = InstituteId;
                        entityStudent.StudentId = details.StudentId;
                        entityStudent.Comments = details.Comments;
                        entityStudent.AttendanceTypeId = details.AttendanceTypeId;
                        entityStudent.IsAbsconding = false;
                        entityStudent.LastUpdateTime = DateTime.Now;
                        _studentAttendanceDetailService.Insert(entityStudent);
                        unitOfWorkAsync.SaveChanges();
                    }


                IEnumerable<StudentAttendanceDetail> stAttDetailList = _studentAttendanceDetailService.GetListwithAttendanceTypeByAttendaceId(vmStudentAttendance.StudentAttendance.Id);
                int presentCount = stAttDetailList.Where(d => d.AttendanceType.IsPresent == true).Count();
                int absentCount = stAttDetailList.Where(d => d.AttendanceType.IsPresent == false).Count();
                int abscondingCount = stAttDetailList.Where(d => d.IsAbsconding == true).Count();
                int totalCount = (vmStudentAttendance.AttendanceDetails != null) ? vmStudentAttendance.AttendanceDetails.Count() : 0;
                decimal presentPercent = (totalCount != 0) ? (presentCount * 100) / totalCount : 0;
                decimal absentPercent = (totalCount != 0) ? (absentCount * 100) / totalCount : 0;
                decimal abscondingPercent = (totalCount != 0) ? (abscondingCount * 100) / totalCount : 0;

                vmStudentAttendance.StudentAttendance.Id = vmStudentAttendance.StudentAttendance.Id;
                vmStudentAttendance.StudentAttendance.InstituteId = InstituteId;
                vmStudentAttendance.StudentAttendance.TotalCount = totalCount;
                vmStudentAttendance.StudentAttendance.PresentCount = presentCount;
                vmStudentAttendance.StudentAttendance.AbsentCount = absentCount;
                vmStudentAttendance.StudentAttendance.AbscondingCount = abscondingCount;
                vmStudentAttendance.StudentAttendance.PresentPercentage = presentPercent;
                vmStudentAttendance.StudentAttendance.AbsentPercentage = absentPercent;
                vmStudentAttendance.StudentAttendance.AbscondingPercentage = abscondingPercent;
                _studentAttendanceService.Update(vmStudentAttendance.StudentAttendance);
                unitOfWorkAsync.SaveChanges();

            }
            catch (Exception ex)
            {

            }
            return vmStudentAttendance;

        }

        public VmSearchAttendance GeAttendanceList(VmSearchAttendance vmSearchAttendance, int instituteId)
        {

            vmSearchAttendance = vmSearchAttendance ?? new VmSearchAttendance();

            var branchList = _branchService.GetKVP(instituteId);
            vmSearchAttendance.BranchList = branchList;
            vmSearchAttendance.BranchId = branchList.FirstOrDefault().Key;

            vmSearchAttendance.ClassList = _classService.GetKVP(instituteId);
            vmSearchAttendance.SectionList = _sectionService.GetKVP(instituteId);
            vmSearchAttendance.TeacherList = _teacherService.GetKVP(instituteId);

            var attList = _studentAttendanceService.GetAllStudentAttendance(vmSearchAttendance);
            var studentAttendanclist = new List<StudentAttendanceListModel>();
            if (attList.Count() > 0)
                foreach (var item in attList)
                {
                   

                    if (item.AcademicPeriod == null)
                    {
                        item.AcademicPeriod = new AcademicPeriod();
                    }
                    StudentAttendanceListModel entityAttendance = new StudentAttendanceListModel();
                    entityAttendance.Id = item.Id;
                    entityAttendance.Date = item.AttendanceDate;
                    entityAttendance.TeacherName = item.Teacher.UserInfo.Name;
                    entityAttendance.ClassName = item.AcademicClass.Name;
                   
                    entityAttendance.PeriodName = item.AcademicPeriod.Name;
                    entityAttendance.SectionName = item.AcademicClassSectionMapping.AcademicSection.Name;
                    entityAttendance.TotalStudent = item.TotalCount;
                    entityAttendance.AbscondingCount = item.AbscondingCount;
                    entityAttendance.PresentCount = item.PresentCount;
                    entityAttendance.AbsentCount = item.AbsentCount;
                    entityAttendance.PresentPercent = item.PresentPercentage;
                    entityAttendance.AbsentPercent = item.AbsentPercentage;
                    entityAttendance.AbscondingPercent = item.AbscondingPercentage;
                    studentAttendanclist.Add(entityAttendance);
                }
            vmSearchAttendance.SearchData = studentAttendanclist;

            return vmSearchAttendance;
        }
        public VmSearchAttendance GeAttendanceSheetPrint(VmSearchAttendance vmSearchAttendance, int instituteId)
        {

            vmSearchAttendance = vmSearchAttendance ?? new VmSearchAttendance();

            var branchList = _branchService.GetKVP(instituteId);
            vmSearchAttendance.BranchList = branchList;
            vmSearchAttendance.BranchId = branchList.FirstOrDefault().Key;
            vmSearchAttendance.ClassList = _classService.GetKVP(instituteId);
            vmSearchAttendance.SectionList = _sectionService.GetKVP(instituteId);
            vmSearchAttendance.SearchData = null;
            vmSearchAttendance.instituteName = _instituteService.GetActiveInstituteById(instituteId).Name;
            int RefTypeId = (int)utility.RefCode.Institute_Logo;
            Image instituteLogo = _imageService.GetImageByRefTypeIdAndRefPrimaryKey(RefTypeId, instituteId).FirstOrDefault();
            vmSearchAttendance.instituteLogo = instituteLogo;
            if (vmSearchAttendance.BranchId != 0 && vmSearchAttendance.ClassId != 0)
            {
                IEnumerable<Student> studentList = _academicStudentService.GetAllStudentBranchClassSectionWiseForPrint(vmSearchAttendance, instituteId);
                var studentAttendanceDetail = new List<StudentAttendanceDetail>();
                if (studentList.Count() > 0)
                    foreach (var item in studentList)
                    {
                        StudentAttendanceDetail entityStudent = new StudentAttendanceDetail();
                        entityStudent.StudentName = item.UserInfo.Name;
                        entityStudent.StudentRoll = item.CurrentRollNo;
                        studentAttendanceDetail.Add(entityStudent);
                    }
                vmSearchAttendance.studentList = studentAttendanceDetail;
            }
            else { vmSearchAttendance.studentList = null; }

            return vmSearchAttendance;
        }
        public VmStudentAttendance GetVmStudentAttendanceEdit(int attendanceId, int instituteId)
        {

            VmStudentAttendance vmStudentAttendance = new VmStudentAttendance();
            var attendanceTypeList = _attendanceTypeService.GetAttendanceTypesForStudent(instituteId).Select(c => new AttendanceTypeModel { AttTypeId = c.Id, IsDefaultAsSelected = c.IsDefault == true ? true : false, Flag = c.Flag, colorCode = c.Colour.ColorCode });
            var tempAttendace = _studentAttendanceService.GetStudentAttendanceById(attendanceId, instituteId);
            vmStudentAttendance.StudentAttendance = new StudentAttendance();
            vmStudentAttendance.StudentAttendance.Id = attendanceId;
            vmStudentAttendance.StudentAttendance.InstituteId = instituteId;
            vmStudentAttendance.StudentAttendance.TeacherId = tempAttendace.TeacherId;
            vmStudentAttendance.StudentAttendance.AttendanceDate = tempAttendace.AttendanceDate;
            vmStudentAttendance.StudentAttendance.AcademicBranchId = tempAttendace.AcademicBranchId;
            vmStudentAttendance.StudentAttendance.BranchName = tempAttendace.AcademicBranch.Name;
            vmStudentAttendance.StudentAttendance.AcademicClassId = tempAttendace.AcademicClassId;
            vmStudentAttendance.StudentAttendance.ClassName = tempAttendace.AcademicClass.Name;

            vmStudentAttendance.StudentAttendance.AcademicSectionId = tempAttendace.AcademicSectionId;

            vmStudentAttendance.StudentAttendance.SectionName = tempAttendace.AcademicClassSectionMapping.AcademicSection.Name;

            vmStudentAttendance.StudentAttendance.AcademicPeriodId = tempAttendace.AcademicPeriodId;

            vmStudentAttendance.StudentAttendance.PeriodName = tempAttendace.AcademicPeriod.Name;

            vmStudentAttendance.StudentAttendance.AcademicBranchList = _branchService.GetKVP(instituteId);
            vmStudentAttendance.StudentAttendance.AcademicClassList = _classService.GetKVP(instituteId);
            // vmStudentAttendance.StudentAttendance.AcademicSectionList = //_sectionService.GetKVP(instituteId);
            var lstAcademicSectionList = new List<KeyValuePair<int, string>>();

            if (tempAttendace.AcademicClassId > 0)
            {
                lstAcademicSectionList = _academicClassSectionMappingService.Getkvp(instituteId, tempAttendace.AcademicClassId);

            }

            vmStudentAttendance.StudentAttendance.AcademicSectionList = lstAcademicSectionList;
            vmStudentAttendance.StudentAttendance.AcademicPeriodId = tempAttendace.AcademicPeriodId;
            vmStudentAttendance.StudentAttendance.AcademicPeriodList = _academicPeriodService.GetKVP(instituteId);
            var data = _teacherService.GetAllTeacher(instituteId, "", vmStudentAttendance.StudentAttendance.AcademicBranchId).ToList();
            var teacherList = new List<KeyValuePair<int, string>>();
            data.ForEach(c => teacherList.Add(new KeyValuePair<int, string>(c.TeacherId, c.UserInfo.Name)));
            vmStudentAttendance.StudentAttendance.TeacherList = teacherList;

            IEnumerable<StudentAttendanceDetail> studentAttendanceDetailList = _studentAttendanceDetailService.GetListByAttendaceId(attendanceId);

            var studentAttendanceDetail = new List<StudentAttendanceDetail>();

            if (studentAttendanceDetailList.Count() > 0)
            {
                foreach (StudentAttendanceDetail item in studentAttendanceDetailList)
                {
                    StudentAttendanceDetail entityStudent = new StudentAttendanceDetail();
                    entityStudent.Id = item.Id;
                    entityStudent.InstituteId = instituteId;
                    entityStudent.StudentName = item.Student.UserInfo.Name;
                    entityStudent.StudentRoll = item.Student.CurrentRollNo;
                    entityStudent.AttendanceTypeId = item.AttendanceTypeId;
                    entityStudent.Comments = item.Comments;
                    entityStudent.StudentId = item.StudentId;
                    entityStudent.StudentAttendanceId = attendanceId;
                    entityStudent.AttendanceTypes = attendanceTypeList;
                    studentAttendanceDetail.Add(entityStudent);
                }
                vmStudentAttendance.AttendanceDetails = studentAttendanceDetail;
            }
            return vmStudentAttendance;
        }

        public VmStudentAttendance UpdateStudentAttendance(VmStudentAttendance vmStudentAttendance, IUnitOfWorkAsync unitOfWorkAsync, int InstituteId)
        {

            if (vmStudentAttendance.AttendanceDetails != null)
                foreach (var details in vmStudentAttendance.AttendanceDetails)
                {
                    StudentAttendanceDetail entityStudent = new StudentAttendanceDetail();
                    entityStudent.Id = details.Id;
                    entityStudent.InstituteId = InstituteId;
                    entityStudent.StudentAttendanceId = details.StudentAttendanceId;
                    entityStudent.Comments = details.Comments;
                    entityStudent.StudentId = details.StudentId;
                    entityStudent.AttendanceTypeId = details.AttendanceTypeId;
                    entityStudent.IsAbsconding = false;
                    entityStudent.LastUpdateTime = DateTime.Now;
                    _studentAttendanceDetailService.Update(entityStudent);
                    unitOfWorkAsync.SaveChanges();
                }

            IEnumerable<StudentAttendanceDetail> stAttDetailList = _studentAttendanceDetailService.GetListwithAttendanceTypeByAttendaceId(vmStudentAttendance.StudentAttendance.Id);
            int presentCount = stAttDetailList.Where(d => d.AttendanceType.IsPresent == true).Count();
            int absentCount = stAttDetailList.Where(d => d.AttendanceType.IsPresent == false).Count();
            int abscondingCount = stAttDetailList.Where(d => d.IsAbsconding == true).Count();
            int totalCount = (vmStudentAttendance.AttendanceDetails != null) ? vmStudentAttendance.AttendanceDetails.Count() : 0;
            decimal presentPercent = (totalCount != 0) ? (presentCount * 100) / totalCount : 0;
            decimal absentPercent = (totalCount != 0) ? (absentCount * 100) / totalCount : 0;
            decimal abscondingPercent = (totalCount != 0) ? (abscondingCount * 100) / totalCount : 0;

            vmStudentAttendance.StudentAttendance.Id = vmStudentAttendance.StudentAttendance.Id;
            vmStudentAttendance.StudentAttendance.InstituteId = InstituteId;
            vmStudentAttendance.StudentAttendance.TotalCount = totalCount;
            vmStudentAttendance.StudentAttendance.PresentCount = presentCount;
            vmStudentAttendance.StudentAttendance.AbsentCount = absentCount;
            vmStudentAttendance.StudentAttendance.AbscondingCount = abscondingCount;
            vmStudentAttendance.StudentAttendance.PresentPercentage = presentPercent;
            vmStudentAttendance.StudentAttendance.AbsentPercentage = absentPercent;
            vmStudentAttendance.StudentAttendance.AbscondingPercentage = abscondingPercent;
            vmStudentAttendance.StudentAttendance.LastUpdateTime = DateTime.Now;

            _studentAttendanceService.Update(vmStudentAttendance.StudentAttendance);
            unitOfWorkAsync.SaveChanges();

            return vmStudentAttendance;

        }

        public VmStudentAttendance UpdateAsscondingStudentAttendance(VmStudentAttendance vmStudentAttendance, int instituteId, IUnitOfWorkAsync unitOfWorkAsync)
        {

            IEnumerable<StudentAttendanceDetail> initialsStAttDetailList = _studentAttendanceDetailService.GetListwithAttendanceTypeByAttendaceId(vmStudentAttendance.StudentAttendance.Id);

            if (vmStudentAttendance.AttendanceDetails != null)
                foreach (var details in vmStudentAttendance.AttendanceDetails)
                {
                    StudentAttendanceDetail tempStudent = initialsStAttDetailList.Where(r => r.Id == details.Id).FirstOrDefault();
                    tempStudent.InstituteId = instituteId;
                    tempStudent.IsAbsconding = details.IsAbsconding;
                    tempStudent.LastUpdateTime = DateTime.Now;
                    _studentAttendanceDetailService.Update(tempStudent);

                }
            unitOfWorkAsync.SaveChanges();

            IEnumerable<StudentAttendanceDetail> stAttDetailList = _studentAttendanceDetailService.GetListwithAttendanceTypeByAttendaceId(vmStudentAttendance.StudentAttendance.Id);
            int abscondingCount = stAttDetailList.Where(d => d.IsAbsconding == true).Count();
            int totalCount = (vmStudentAttendance.AttendanceDetails != null) ? vmStudentAttendance.AttendanceDetails.Count() : 0;
            decimal abscondingPercent = (totalCount != 0) ? (abscondingCount * 100) / totalCount : 0;


            StudentAttendance studentAttendanceModel = _studentAttendanceService.GetStudentAttendanceById(vmStudentAttendance.StudentAttendance.Id, instituteId);
            //need to be changed
            studentAttendanceModel.AbscondingCount = abscondingCount;
            studentAttendanceModel.AbscondingPercentage = abscondingPercent;
            studentAttendanceModel.LastUpdateTime = DateTime.Now;

            _studentAttendanceService.Update(studentAttendanceModel);
            unitOfWorkAsync.SaveChanges();

            return vmStudentAttendance;

        }

        public VmStudentAttendance GetVmStudentAttendanceDetails(int attendanceId, int instituteId)
        {

            VmStudentAttendance vmStudentAttendance = new VmStudentAttendance();

            var tempAttendace = _studentAttendanceService.GetStudentAttendanceForDetailsById(attendanceId, instituteId);

            vmStudentAttendance.StudentAttendance = new StudentAttendance();
            vmStudentAttendance.StudentAttendance.Id = tempAttendace.Id;
            vmStudentAttendance.StudentAttendance.InstituteId = tempAttendace.InstituteId;
            vmStudentAttendance.StudentAttendance.TotalCount = tempAttendace.TotalCount;
            vmStudentAttendance.StudentAttendance.PresentCount = tempAttendace.PresentCount;
            vmStudentAttendance.StudentAttendance.PresentPercentage = tempAttendace.PresentPercentage;
            vmStudentAttendance.StudentAttendance.AbsentCount = tempAttendace.AbsentCount;
            vmStudentAttendance.StudentAttendance.AbsentPercentage = tempAttendace.AbsentPercentage;
            vmStudentAttendance.StudentAttendance.AbscondingCount = tempAttendace.AbscondingCount;
            vmStudentAttendance.StudentAttendance.AbscondingPercentage = tempAttendace.AbscondingPercentage;
            vmStudentAttendance.StudentAttendance.TeacherName = tempAttendace.Teacher.UserInfo.Name;
            vmStudentAttendance.StudentAttendance.AttendanceDate = tempAttendace.AttendanceDate;
            vmStudentAttendance.StudentAttendance.BranchName = tempAttendace.AcademicBranch.Name;
            vmStudentAttendance.StudentAttendance.ClassName = tempAttendace.AcademicClass.Name;
            vmStudentAttendance.StudentAttendance.SectionName = tempAttendace.AcademicClassSectionMapping.AcademicSection.Name;
            vmStudentAttendance.StudentAttendance.SessionName = tempAttendace.AcademicSession.Name;
            vmStudentAttendance.StudentAttendance.LastUpdateTime = tempAttendace.LastUpdateTime;

            IEnumerable<StudentAttendanceDetail> studentAttendanceDetailList = _studentAttendanceDetailService.GetListwithAttendanceTypeByAttendaceId(attendanceId);

            var studentAttendanceDetail = new List<StudentAttendanceDetail>();

            if (studentAttendanceDetailList.Count() > 0)
            {
                foreach (StudentAttendanceDetail item in studentAttendanceDetailList)
                {
                    StudentAttendanceDetail entityStudent = new StudentAttendanceDetail();
                    entityStudent.Id = item.Id;
                    entityStudent.InstituteId = item.InstituteId;
                    entityStudent.StudentName = item.Student.UserInfo.Name;
                    entityStudent.Comments = item.Comments;
                    entityStudent.StudentRoll = item.Student.CurrentRollNo;
                    entityStudent.Status = item.AttendanceType.Flag;
                    entityStudent.IsAbsconding = item.IsAbsconding;
                    entityStudent.StatusColor = item.AttendanceType.Colour.ColorCode;
                    studentAttendanceDetail.Add(entityStudent);

                }

                vmStudentAttendance.AttendanceDetails = studentAttendanceDetail;
            }


            return vmStudentAttendance;
        }

    }
}
