//using pnsms.Entities.AttendanceMachineModel;
using deepp.Entities.AttendanceMachineModel;
using deepp.Entities.Models;
using deepp.Entities.ResponseModel;
using deepp.Entities.ViewModels;

using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Service.Attendance
{

    public interface IAttendanceMachineService
    {

        ReturnModel SaveUpdateStudentAttendanceFromDevice(int InstituteId, List<MachineInfo> lstMachineInfoStudent, IUnitOfWorkAsync unitOfWorkAsync);

    }

    public class AttendanceMachineService : IAttendanceMachineService
    {

        public static SynStudentAttendanceReponseModel objSynStudentAttendanceReponseModel = new SynStudentAttendanceReponseModel();


        private readonly IAcademicBranchService _branchService;
        private readonly IAcademicClassService _classService;
        private readonly IAcademicGroupService _groupService;
        private readonly IAcademicSectionService _sectionService;
        private readonly IAcademicShiftService _shiftService;

        private readonly IStudentService _academicStudentService;
        private readonly IAttendanceTypeService _attendanceTypeService;
        private readonly IStudentAttendanceService _studentAttendanceService;
        private readonly IStudentAttendanceDetailService _studentAttendanceDetailService;
        private readonly ITeacherService _teacherService;
        private readonly IInstituteService _instituteService;
        private readonly IImageService _imageService;
        private readonly IStudentService _studentService;

        public AttendanceMachineService(
              IAcademicBranchService branchService
            , IAcademicClassService classService
            , IAcademicGroupService groupService
            , IAcademicSectionService sectionService

            , IAcademicShiftService shiftService

            , IStudentService academicStudentService
            , IAttendanceTypeService attendanceTypeService
            , IStudentAttendanceService studentAttendanceService
            , IStudentAttendanceDetailService studentAttendanceDetailService
            , ITeacherService teacherService
            , IInstituteService instituteService
            , IImageService imageService
            , IStudentService studentService

            )
        {
            _branchService = branchService;
            _classService = classService;
            _sectionService = sectionService;

            _groupService = groupService;
            _shiftService = shiftService;

            _academicStudentService = academicStudentService;
            _attendanceTypeService = attendanceTypeService;
            _studentAttendanceService = studentAttendanceService;
            _studentAttendanceDetailService = studentAttendanceDetailService;
            _teacherService = teacherService;
            _instituteService = instituteService;
            _imageService = imageService;
            _studentService = studentService;
        }



        public ReturnModel SaveUpdateStudentAttendanceFromDevice(int InstituteId, List<MachineInfo> lstMachineInfoStudent, IUnitOfWorkAsync unitOfWorkAsync)
        {
            ReturnModel objReturnModel = new ReturnModel();

            try
            {
               // unitOfWorkAsync.BeginTransaction(isolationLevel: System.Data.IsolationLevel.Unspecified);
                VmStudentAttendance vmUserAttendance = new VmStudentAttendance();
                VmSearchAttendance vmSearchAttendance = new VmSearchAttendance();
                List<vmMachineInfo> lstvmMachineInfo = new List<vmMachineInfo>();
                var lstAttendanceType = _attendanceTypeService.GetAttendanceTypes(InstituteId);
                var branch = _branchService.GetAcademicBranchsByInstituteId(InstituteId);
                var lstLastAttendanceDistinctValue = lstMachineInfoStudent.GroupBy(x => x.DateOnlyRecord).Select(grp => grp.First());//lstLastAttendanceData.Distinct().Select(x => x.DateOnlyRecord);   //GroupBy(x => x.DateOnlyRecord).Select(y=>y.);

                // int RefTypeId = (int)utility.UserInfoType.Student;

                lstLastAttendanceDistinctValue.ToList().ForEach(delegate (MachineInfo item)
                {
                    vmMachineInfo objvmMachineInfo = new vmMachineInfo();
                    MachineInfo objnewMachineInfo = new MachineInfo();
                    List<MachineInfo> lstnewMachineInfo = new List<MachineInfo>();
                    objnewMachineInfo = item;
                    var lstAttenuser = lstMachineInfoStudent.Where(x => x.DateOnlyRecord == item.DateOnlyRecord);
                    lstnewMachineInfo = lstAttenuser.ToList();
                    objvmMachineInfo.objMachineInfo = objnewMachineInfo;
                    objvmMachineInfo.lstMachineInfo = lstnewMachineInfo;
                    lstvmMachineInfo.Add(objvmMachineInfo);
                });
               // lstvmMachineInfo.ToList().ForEach(delegate (vmMachineInfo item)

               
                    //if (objAttendance != null)
                    //{
                    //    IEnumerable<StudentAttendanceDetail> attDetailList = _studentAttendanceDetailService.GetListwithAttendanceTypeByAttendaceId(objAttendance.Id);
                    //    int presentCount = attDetailList.Where(d => d.AttendanceType.IsPresent == true).Count();
                    //    int absentCount = attDetailList.Where(d => d.AttendanceType.IsPresent == false).Count();
                    //    int totalCount = (objAttendance.StudentAttendanceDetails != null) ? objAttendance.StudentAttendanceDetails.Count() : 0;
                    //    decimal presentPercent = (totalCount != 0) ? (presentCount * 100) / totalCount : 0;
                    //    decimal absentPercent = (totalCount != 0) ? (absentCount * 100) / totalCount : 0;
                    //    objAttendance.TotalCount = totalCount;
                    //    objAttendance.PresentCount = presentCount;
                    //    objAttendance.AbsentCount = absentCount;
                    //    objAttendance.PresentPercentage = presentPercent;
                    //    objAttendance.AbsentPercentage = absentPercent;
                    //    // objAttendance.UserAttendanceDetails = null;
                    //    _studentAttendanceService.Update(objAttendance);
                    //    unitOfWorkAsync.SaveChanges();
                    //}
                    //else
                    //{

                    //    IEnumerable<StudentAttendanceDetail> attDetailList = _studentAttendanceDetailService.GetListwithAttendanceTypeByAttendaceId(vmUserAttendance.StudentAttendance.Id);
                    //    int presentCount = attDetailList.Where(d => d.AttendanceType.IsPresent == true).Count();
                    //    int absentCount = attDetailList.Where(d => d.AttendanceType.IsPresent == false).Count();
                    //    int totalCount = (attDetailList != null) ? attDetailList.Count() : 0;
                    //    decimal presentPercent = (totalCount != 0) ? (presentCount * 100) / totalCount : 0;
                    //    decimal absentPercent = (totalCount != 0) ? (absentCount * 100) / totalCount : 0;

                    //    vmUserAttendance.StudentAttendance.TotalCount = totalCount;
                    //    vmUserAttendance.StudentAttendance.PresentCount = presentCount;
                    //    vmUserAttendance.StudentAttendance.AbsentCount = absentCount;
                    //    vmUserAttendance.StudentAttendance.PresentPercentage = presentPercent;
                    //    vmUserAttendance.StudentAttendance.AbsentPercentage = absentPercent;
                    //    //vmUserAttendance.UserAttendance.UserAttendanceDetails = null;
                    //    _studentAttendanceService.Update(vmUserAttendance.StudentAttendance);
                    //    unitOfWorkAsync.SaveChanges();
                    //}

            
                unitOfWorkAsync.SaveChanges();
                objReturnModel.IsSuccess = true;
                objReturnModel.Message = "Successfully All data Uploaded";
              //  unitOfWorkAsync.Commit();
                return objReturnModel;


            }

            catch (Exception ex)
            {

                objReturnModel.IsSuccess = false;
                objReturnModel.Message = ex.InnerException.Message.ToString();

                return objReturnModel;

            }
        }
    }
}
