using sfa.attendance.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pnsms.Entities.StoredProcedures.Models;
using sfa.attendance.Info;
using pnsms.Entities.Models;
using System.Threading;

namespace sfa.attendance.ApiService
{
    public class AttendanceService
    {
        private Master masterForm;
        public AttendanceService(Master _master)
        {
            this.masterForm = _master;
        }

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
                    var reponse = HttpService.post(URLService.URL_LOGIN, json, objLoginReponseModel);
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
                var response = HttpService.post(URLService.URL_POSTLISTMACHINEINFO, json, objLoginReponseModel);
                if (response == null)
                {
                    response = string.Empty;
                }
                VmUserAttendance objVmUserAttendance = new VmUserAttendance();

                objVmUserAttendance = JsonConvert.DeserializeObject<VmUserAttendance>(response);
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

        public vmMachineInfo AttendanceDataPostBoth(List<MachineInfo> lstMachineInfo, System.DateTime sendingDate, LoginReponseModel objLoginReponseModel)
        {

            try
            {
                string message = string.Empty;
                var IsSuccess = CheckForInternetConnection();
                vmMachineInfo objvmMachineInfo = new vmMachineInfo();
                objvmMachineInfo.lstMachineInfo = new List<MachineInfo>();


                if (!IsSuccess)
                {
                    objvmMachineInfo.message = "No Internet Connection Found";
                    this.masterForm.ShowStatusBar("No Internet Connection Found");
                    return objvmMachineInfo;
                }
                var Machineinfo = lstMachineInfo.FirstOrDefault().deviceinfo;
                var objAttendanceDataSynInfo = HttpService.get(URLService.URL_GET_ATTENDANCEDATASYNINFO_For_Both + Machineinfo, objLoginReponseModel);
                AttendanceConfigurationDetail objVmAttendanceDataSynInfo = JsonConvert.DeserializeObject<AttendanceConfigurationDetail>(objAttendanceDataSynInfo);



                if (objVmAttendanceDataSynInfo.Id == 0)
                {
                    objvmMachineInfo.message = "Institute Attendance Confiure Not Found";
                    this.masterForm.ShowStatusBar("Institute Attendance Confiure Not Found");
                    return objvmMachineInfo;
                }
                else if (objVmAttendanceDataSynInfo.MachineNo == null)
                {
                    objvmMachineInfo.message = "Institute Machine No Confiure Not Found";
                    this.masterForm.ShowStatusBar("Institute Machine No Confiure Not Found");
                    return objvmMachineInfo;
                }
                else if (objVmAttendanceDataSynInfo.AttendanceStartSynDate == null)
                {
                    objvmMachineInfo.message = "Institute Attendance Start Date Confiure Not Found";
                    this.masterForm.ShowStatusBar("Institute Attendance Start Date Confiure Not Found");
                    return objvmMachineInfo;
                }
                else if (objVmAttendanceDataSynInfo.AttendanceLastSynDate == null)
                {
                    objvmMachineInfo.message = "Institute Attendance Start Date Confiure Not Found";
                    this.masterForm.ShowStatusBar("Institute Attendance Start Date Confiure Not Found");
                    return objvmMachineInfo;
                }

                var lstLastAttendanceData = lstMachineInfo.Where(x => Convert.ToDateTime(x.DateTimeRecord) > Convert.ToDateTime(objVmAttendanceDataSynInfo.AttendanceLastSynDate) && Convert.ToDateTime(x.DateTimeRecord) <= DateTime.Now);

                this.masterForm.ShowStatusBar("Processing.....");
                List<MachineInfo> pushlstLastAttendanceData = new List<MachineInfo>();

                pushlstLastAttendanceData = new List<MachineInfo>();


                //// Test Code
                // var lstLastAttendanceData1 = lstLastAttendanceData.ToList()[0];
                // var lstLastAttendanceData2 = new List<MachineInfo>();
                // for (int i = 0; i < 20000; i++)
                // {
                //     lstLastAttendanceData2.Add(lstLastAttendanceData1);
                // }
                // lstLastAttendanceData = lstLastAttendanceData2.AsEnumerable();
                lstLastAttendanceData = lstLastAttendanceData.OrderBy(x=>x.DateOnlyRecord);
                int TotalAttendance = lstLastAttendanceData.Count(), TotalProcessed = 0;
                if(TotalAttendance == 0)
                {
                    this.masterForm.ShowStatusBar("No Records found to push.");
                    Thread.Sleep(1000);
                }
                lstLastAttendanceData.ToList().ForEach(delegate (MachineInfo item)
                {
                    pushlstLastAttendanceData.Add(item);
                    if (pushlstLastAttendanceData.Count() == 500)
                    {
                        objvmMachineInfo= AttendanceApiDataPost(lstMachineInfo, pushlstLastAttendanceData, objVmAttendanceDataSynInfo, objLoginReponseModel);
                        TotalProcessed += 500;
                        this.masterForm.ShowStatusBar(String.Format(successMessage, TotalProcessed, TotalAttendance));
                        pushlstLastAttendanceData = new List<MachineInfo>();

                        Thread.Sleep(1000);
                    }
                });
                if (pushlstLastAttendanceData.Count() > 0)
                {
                    objvmMachineInfo = AttendanceApiDataPost(lstMachineInfo, pushlstLastAttendanceData, objVmAttendanceDataSynInfo, objLoginReponseModel);
                    TotalProcessed += pushlstLastAttendanceData.Count();
                    this.masterForm.ShowStatusBar(String.Format(successMessage, TotalProcessed, TotalAttendance));
                }
                objvmMachineInfo.lstMachineInfo = pushlstLastAttendanceData;
                return objvmMachineInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public vmMachineInfo AttendanceApiDataPost(List<MachineInfo> lstMachineInfo, List<MachineInfo> pushlstLastAttendanceData, AttendanceConfigurationDetail objVmAttendanceDataSynInfo, LoginReponseModel objLoginReponseModel)
        {
            vmMachineInfo objvmMachineInfo = new vmMachineInfo();

            string json = string.Empty;
            if (pushlstLastAttendanceData.Count() > 0)
            {
                json = JsonConvert.SerializeObject(pushlstLastAttendanceData);
                var response = HttpService.post(URLService.URL_POSTLISTMACHINEINFO_For_BOTHDataMigration, json, objLoginReponseModel);
                if (response == null)
                {
                    response = string.Empty;
                }
                VmUserAttendance objVmUserAttendance = new VmUserAttendance();

                objVmUserAttendance = JsonConvert.DeserializeObject<VmUserAttendance>(response);
                if (objVmUserAttendance == null)
                {
                    objVmUserAttendance = new VmUserAttendance();
                }
                objvmMachineInfo.message = objVmUserAttendance.Message;
            }
            else
            {
                objvmMachineInfo.message = "Already All data Uploaded Upto " + Convert.ToDateTime(objVmAttendanceDataSynInfo.AttendanceLastSynDate);
            }
            if (lstMachineInfo.Where(x => Convert.ToDateTime(x.DateTimeRecord) > DateTime.Now).ToList().Count > 0)
            {
                objvmMachineInfo.message = objvmMachineInfo.message + " " + "Some Forward Date information Found Plz check Your Device Date";
            }
            return objvmMachineInfo;
        }

        public String successMessage = "{0} attendance data successfully uploaded from {1} data";

    }
}
