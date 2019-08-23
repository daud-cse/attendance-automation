
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using deepp.Entities.StoredProcedures.Models;
using deepp.WindowsService.Model;
using deepp.Entities.AttendanceMachineModel;
using deepp.WindowsService.ApiService;
using deepp.Entities.ViewModels;

namespace deepp.WindowsService.ApiService
{
    public class AttendanceService
    {

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    using (client.OpenRead("http://clients3.google.com/generate_204"))
                    {
                        return true;
                    }
                }

            }
            catch
            {
                return false;
            }

        }
        public LoginReponseModel AuthenticationCheck(int InstituteId, string UserId, string Password)
        {
            var IsSuccess = CheckForInternetConnection();
            LoginRequestModel loginRequest = new LoginRequestModel();
            LoginReponseModel objLoginReponseModel = new LoginReponseModel();
            if (!IsSuccess)
            {
                objLoginReponseModel.Success = "0";
                objLoginReponseModel.Message = "No Internet Connection Found";
            }
            else
            {
                try
                {
                    loginRequest.instituteid = InstituteId;
                    loginRequest.userid = UserId;
                    loginRequest.password = Password;

                    string json = JsonConvert.SerializeObject(loginRequest);
                    var reponse = HttpService.deeppt(URLService.URL_LOGIN, json, objLoginReponseModel);
                    objLoginReponseModel = JsonConvert.DeserializeObject<LoginReponseModel>(reponse);

                    if (objLoginReponseModel == null)
                    {
                        objLoginReponseModel = new LoginReponseModel();
                        objLoginReponseModel.Success = "0";
                        objLoginReponseModel.Message = "Invalid User Id and Password or Institute Id";
                    }
                    return objLoginReponseModel;
                }
                catch (Exception ex)
                {
                    return objLoginReponseModel;
                }
            }
            return objLoginReponseModel;
        }

        public vmMachineInfo AttendanceDataPost(List<MachineInfo> lstMachineInfo, System.DateTime sendingDate, LoginReponseModel objLoginReponseModel)
        {
            string message = string.Empty;
            var IsSuccess = CheckForInternetConnection();
            vmMachineInfo objvmMachineInfo = new vmMachineInfo();
            objvmMachineInfo.lstMachineInfo = new List<MachineInfo>();

            if (!IsSuccess)
            {
                objvmMachineInfo.message = "No Internet Connection Found";
                return objvmMachineInfo;
            }
            var lstAttendanceDataSynInfo = HttpService.get(URLService.URL_GET_ATTENDANCEDATASYNINFO, objLoginReponseModel);
            VmAttendanceDataSynInfo objVmAttendanceDataSynInfo = JsonConvert.DeserializeObject<VmAttendanceDataSynInfo>(lstAttendanceDataSynInfo);
            if (objVmAttendanceDataSynInfo == null)
            {
                objvmMachineInfo.message = "Institute Attendance Start Date Confiure Not Found";
                return objvmMachineInfo;
            }
            if (objVmAttendanceDataSynInfo.lstLastAttendanceSynDate == null)
            {
                objvmMachineInfo.message = "Institute Attendance Start Date Confiure Not Found";
                return objvmMachineInfo;
            }
            if (objVmAttendanceDataSynInfo.lstLastAttendanceSynDate.Count() == 0)
            {
                objvmMachineInfo.message = "Institute Attendance Start Date Confiure Not Found";
                return objvmMachineInfo;
            }

            var lstLastAttendanceData = lstMachineInfo.Where(x => Convert.ToDateTime(x.DateTimeRecord) > Convert.ToDateTime(objVmAttendanceDataSynInfo.lstLastAttendanceSynDate.FirstOrDefault().LastAttendanceSynDate) && Convert.ToDateTime(x.DateTimeRecord) <= DateTime.Now);
            objvmMachineInfo.lstMachineInfo = lstLastAttendanceData.ToList();
            string json = string.Empty;
            if (lstLastAttendanceData.Count() > 0)
            {
                json = JsonConvert.SerializeObject(lstLastAttendanceData);
                var reponse = HttpService.deeppt(URLService.URL_POSTLISTMACHINEINFO, json, objLoginReponseModel);
                if (reponse == null)
                {
                    reponse = string.Empty;
                }
                VmUserAttendance objVmUserAttendance = new VmUserAttendance();

                objVmUserAttendance = JsonConvert.DeserializeObject<VmUserAttendance>(reponse);
                objvmMachineInfo.message = objVmUserAttendance.Message;


            }
            else
            {
                objvmMachineInfo.message = "Already All data Uploaded Upto " + Convert.ToDateTime(objVmAttendanceDataSynInfo.lstLastAttendanceSynDate.FirstOrDefault().LastAttendanceSynDate);
            }
            if (lstMachineInfo.Where(x => Convert.ToDateTime(x.DateTimeRecord) > DateTime.Now).ToList().Count > 0)
            {
                objvmMachineInfo.message = objvmMachineInfo.message + " " + "Some Forward Date information Found Plz check Your Device Date";
            }
            return objvmMachineInfo;
        }
    }
}
