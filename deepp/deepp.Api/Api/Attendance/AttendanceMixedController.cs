
using deepp.Entities.Models;
using deepp.Entities.ViewModels;
using deepp.Service;
using deepp.Service.Attendance;
using deepp.Service.SSOLogin;
using deepp.Service.ViewModels;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace deepp.Api.Attendance
{
    public class AttendanceMixedController : ApiController
    {

        private readonly IVmUserAttendanceService _vmUserAdendanceService;
        private readonly IAttendanceConfigurationService _attendanceConfigurationService;
        private readonly IAttendanceConfigurationDetailService _attendanceConfigurationDetailService;
        private readonly IMachineInfoService _machineInfoService;

        private readonly ITeacherService _teacherService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
       
        private readonly IStoredProcedures _storedProcedures;
        private readonly ISSOService _SSOService;
        public AttendanceMixedController(
            IVmUserAttendanceService vmUserAdendanceService,
            ITeacherService teacherService,
            IUnitOfWorkAsync unitOfWorkAsync
              , ISSOService SSOService
            , IStoredProcedures storedProcedures
            
            , IAttendanceConfigurationService attendanceConfigurationService
            , IAttendanceConfigurationDetailService attendanceConfigurationDetailService
            , IMachineInfoService machineInfoService)
        {
            _vmUserAdendanceService = vmUserAdendanceService;
            _teacherService = teacherService;
            _unitOfWorkAsync = unitOfWorkAsync;
            _SSOService = SSOService;
            _storedProcedures = storedProcedures;
           
            _attendanceConfigurationService = attendanceConfigurationService;
            _attendanceConfigurationDetailService = attendanceConfigurationDetailService;
            _machineInfoService = machineInfoService;
        }



        [Route("api/attendanceboth/getattendancedatasyninfoboth")]
        [HttpGet]
        public AttendanceConfigurationDetail GetAttendanceDataSynInfoboth(string Machineinfo)
        {
            AttendanceConfigurationDetail objVmAttendanceDataSynInfo = new AttendanceConfigurationDetail();

            try
            {
                var objSSO = _SSOService.IsTokenValid(this.Request.Headers);
                if (objSSO == null)
                {
                    objVmAttendanceDataSynInfo = new AttendanceConfigurationDetail { Message = "Token Is not found" };
                }
                else if (objSSO.InstituteId == 0)
                {
                    objVmAttendanceDataSynInfo = new AttendanceConfigurationDetail { Message = "Token Is not found" };
                }
                else
                {
                    objVmAttendanceDataSynInfo = _storedProcedures.GetAttendanceDataSynInfoboth(objSSO.InstituteId, 13, Machineinfo);
                }

                return objVmAttendanceDataSynInfo;
            }
            catch (Exception ex)
            {
                return objVmAttendanceDataSynInfo;
            }
        }

      

        [Route("api/attendanceboth/machinedatapostbothattendancedatamigration")]
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


    }
}
