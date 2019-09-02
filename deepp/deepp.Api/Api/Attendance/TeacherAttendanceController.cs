using deepp.Entities.AttendanceMachineModel;
using deepp.Entities.Models;
using deepp.Entities.StoredProcedures.Models;
using deepp.Entities.ViewModels;
using deepp.Service;
using deepp.Service.Attendance;
using deepp.Service.SSOLogin;
using deepp.Service.ViewModels;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace deepp.Api.Api.Attendance
{
    public class TeacherAttendanceController : ApiController
    {

        private readonly IVmUserAttendanceService _vmUserAdendanceService;
        private readonly ITeacherService _teacherService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IMachineInfoService _machineInfoService;
        private readonly IStoredProcedures _storedProcedures;
        private readonly IAttendanceConfigurationDetailService _attendanceConfigurationDetailService;
        readonly int instituteId = 5;//Sessions.InstituteId;
        readonly int userId = 12;// Sessions.UserId;
        private readonly ISSOService _SSOService;
        public TeacherAttendanceController(
            IVmUserAttendanceService vmUserAdendanceService,
            ITeacherService teacherService,
            IUnitOfWorkAsync unitOfWorkAsync
              , ISSOService SSOService
            , IStoredProcedures storedProcedures
             , IMachineInfoService machineInfoService)
        {
            _vmUserAdendanceService = vmUserAdendanceService;
            _teacherService = teacherService;
            _unitOfWorkAsync = unitOfWorkAsync;
            _SSOService = SSOService;
            _storedProcedures = storedProcedures;
            _machineInfoService = machineInfoService;
        }


        [Route("api/teacherattendance/new")]
        [HttpPost]
        public VmUserAttendance GetNew([FromBody]VmUserAttendance userAttendanceModel)
        {
            int branchId = (userAttendanceModel != null) ? userAttendanceModel.UserAttendance.AcademicBranchId : 0;
            var userAttendanceCreate = _vmUserAdendanceService.CreateVmTeacherAttendance(instituteId, branchId);
            return userAttendanceCreate;
        }

        [Route("api/teacherattendance/getattendancedatasyninfo")]
        [HttpGet]
        public VmAttendanceDataSynInfo GetAttendanceDataSynInfo()
        {
            VmAttendanceDataSynInfo objVmAttendanceDataSynInfo = new VmAttendanceDataSynInfo();
            
            try
            {
                var objSSO = _SSOService.IsTokenValid(this.Request.Headers);
                if (objSSO == null)
                {
                    objVmAttendanceDataSynInfo = new VmAttendanceDataSynInfo { Message = "Token Is not found" };
                }
                else if (objSSO.InstituteId == 0)
                {

                }
                else
                {
                    objVmAttendanceDataSynInfo = _storedProcedures.GetAttendanceDataSynInfo(objSSO.InstituteId, 13,string.Empty);
                }

                return objVmAttendanceDataSynInfo;
            }
            catch (Exception ex)
            {
                return objVmAttendanceDataSynInfo;
            }
        }

        [Route("api/teacherattendance/saveteacherattendance")]
        [HttpPost]
        public VmUserAttendance Post([FromBody] List<VmUserAttendance> lstuserAttendanceModel)
        {
            VmUserAttendance objVmTeacherDetails = new VmUserAttendance();
            try
            {
                var objSSO = _SSOService.IsTokenValid(this.Request.Headers);
                if (objSSO == null)
                {
                    objVmTeacherDetails = new VmUserAttendance { Message = "Token Is not found" };

                }
                else
                {
                    var objVmAttendanceDataSynInfo = _storedProcedures.GetAttendanceDataSynInfo(objSSO.InstituteId, 13,string.Empty);

                    //lstuserAttendanceModel.ForEach(delegate (VmUserAttendance item)
                    //{
                    //    item.UserAttendance.InstituteId = objSSO.InstituteId;
                    //    _vmUserAdendanceService.SaveUpdateTeacherAttendance(lstuserAttendanceModel, _unitOfWorkAsync);
                    //    objVmTeacherDetails.Message = "Successfully Data Uploaded";
                    //});

                }

                return objVmTeacherDetails;
            }
            catch (Exception ex)
            {
                objVmTeacherDetails = new VmUserAttendance { Message = ex.ToString() };
                return objVmTeacherDetails;
            }


        }

        [Route("api/teacherattendance/machinedatadeepptteacherattendance")]
        [HttpPost]
        public VmUserAttendance PostMachineData([FromBody] List<MachineInfo> lstMachineInfoTeacher)
        {

            
            VmUserAttendance objVmTeacherDetails = new VmUserAttendance();
            List<VmUserAttendance> lstuserAttendanceModel = new List<VmUserAttendance>();
            try
            {
                var objSSO = _SSOService.IsTokenValid(this.Request.Headers);
               var  objVmAttendanceDataSynInfo = _storedProcedures.GetAttendanceDataSynInfo(objSSO.InstituteId, 13, string.Empty);

                if (objVmAttendanceDataSynInfo == null)
                {
                    objVmTeacherDetails.Message = "Institute Attendance Date Confiure Not Found";
                    return objVmTeacherDetails;
                }
                if (objVmAttendanceDataSynInfo.lstLastAttendanceSynDate == null)
                {
                    objVmTeacherDetails.Message = "Institute Attendance Date Confiure Not Found";
                    return objVmTeacherDetails;
                }

                //if (objVmAttendanceDataSynInfo.lstLastAttendanceSynDate.FirstOrDefault().MachineSerialNo == lstMachineInfoTeacher.FirstOrDefault().deviceinfo || objVmAttendanceDataSynInfo.lstLastAttendanceSynDate.FirstOrDefault().MachineSerialNo1 == lstMachineInfoTeacher.FirstOrDefault().deviceinfo || objVmAttendanceDataSynInfo.lstLastAttendanceSynDate.FirstOrDefault().MachineSerialNo2 == lstMachineInfoTeacher.FirstOrDefault().deviceinfo || objVmAttendanceDataSynInfo.lstLastAttendanceSynDate.FirstOrDefault().MachineSerialNo3 == lstMachineInfoTeacher.FirstOrDefault().deviceinfo || objVmAttendanceDataSynInfo.lstLastAttendanceSynDate.FirstOrDefault().MachineSerialNo4 == lstMachineInfoTeacher.FirstOrDefault().deviceinfo)
                //{
                //    objVmTeacherDetails.Message = "Device Info is not match";
                //    return objVmTeacherDetails;
                //}

                if (objSSO == null)
                {
                    objVmTeacherDetails = new VmUserAttendance { Message = "Token Is not found" };

                }
                else
                {
                    _vmUserAdendanceService.SaveUpdateTeacherAttendanceFromDevice(objSSO.InstituteId, lstMachineInfoTeacher, _unitOfWorkAsync);
                    objVmTeacherDetails.Message = "Successfully Data Uploaded";

                }

                return objVmTeacherDetails;
            }
            catch (Exception ex)
            {
                objVmTeacherDetails = new VmUserAttendance { Message = ex.ToString() };
                return objVmTeacherDetails;
            }


        }
        [Route("api/attendanceboth/machinedatapostbothattendancedatamigrationold")]
        [HttpPost]
        public async Task<IHttpActionResult> PostMachineDataMigration([FromBody] List<MachineInfo> lstMachineInfo)
        {

            VmUserAttendance objVmTeacherDetails = new VmUserAttendance();
            List<VmUserAttendance> lstuserAttendanceModel = new List<VmUserAttendance>();
            try
            {
                var machininfo = lstMachineInfo.FirstOrDefault().deviceinfo;
                var objSSO = _SSOService.IsTokenValid(this.Request.Headers);
                var objVmAttendanceDataSynInfo = _storedProcedures.GetAttendanceDataSynInfoboth(objSSO.InstituteId, 13, machininfo);

                if (objVmAttendanceDataSynInfo == null)
                {
                    objVmTeacherDetails.Message = "Institute Attendance Date Confiure Not Found";
                    return Ok(objVmTeacherDetails);
                }
                if (objSSO == null)
                {
                    objVmTeacherDetails = new VmUserAttendance { Message = "Token Is not found" };
                }
                else
                {
                    ReturnModel objReturnModel = new ReturnModel();
                    objReturnModel = _machineInfoService.Inserts(objSSO.InstituteId, objSSO.AcademicSessionId, objSSO.UserId, _unitOfWorkAsync, lstMachineInfo);
                    objReturnModel.Count = lstMachineInfo.Count.ToString();

                    objVmTeacherDetails.IsSuccess = objReturnModel.IsSuccess;
                    objVmTeacherDetails.Message = objReturnModel.Message;

                    objVmTeacherDetails.Message = objVmTeacherDetails.Message;
                    objVmTeacherDetails.IsSuccess = objReturnModel.IsSuccess;
                    if (objVmTeacherDetails.IsSuccess)
                    {
                        objVmAttendanceDataSynInfo.AttendanceLastSynDate = lstMachineInfo.Max(x => Convert.ToDateTime(x.DateTimeRecord));
                        objVmAttendanceDataSynInfo.SetDate = DateTime.Now;
                        objVmAttendanceDataSynInfo.SetBy = objSSO.UserId.ToString();
                        _attendanceConfigurationDetailService.Update(_unitOfWorkAsync, objVmAttendanceDataSynInfo);
                    }
                    return Ok(objVmTeacherDetails);

                }
                return Ok(objVmTeacherDetails);
            }
            catch (Exception ex)
            {
                objVmTeacherDetails = new VmUserAttendance { Message = ex.InnerException.Message.ToString() };

                return Ok(objVmTeacherDetails);
            }


        }

        [Route("api/teacherattendance/updateteacherattendance")]
        [HttpPost]
        public HttpResponseMessage Update([FromBody]VmUserAttendance userAttendanceModel)
        {
            _vmUserAdendanceService.UpdateUserAttendance(userAttendanceModel, _unitOfWorkAsync);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [Route("api/teacherattendance/list")]
        [HttpPost]
        public VmSearchAttendance GetAllTeacherAttendance([FromBody]VmSearchAttendance searchAttendanceModel)
        {
            var attendanceList = _vmUserAdendanceService.GetTeacherAttendanceList(searchAttendanceModel, instituteId);
            return attendanceList;
        }

        [Route("api/teacherattendance/getsingle/")]
        public VmUserAttendance GetSingleAttendanceById(int id)
        {
            var vmStudentAdendance = _vmUserAdendanceService.GetVmTeacherDetailsById(id, instituteId);
            vmStudentAdendance.AttendanceDetails.OrderBy(x => x.UserInfo.Teacher.Designation.Ordering);
            return vmStudentAdendance;
        }


    }
}
