using Newtonsoft.Json;
using deepp.Entities.Models;
using deepp.Entities.ViewModels;
using deepp.WindowsService.ApiService;
using deepp.WindowsService.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.SqlServer;
using System.IO;
using deepp.Entities.AttendanceMachineModel;

namespace deepp.WindowsService
{
    public partial class Service : ServiceBase
    {
        Thread mainThreadObj;             
        readonly string baseUrl = ConfigurationManager.AppSettings["smsServiceUrl"];
        readonly string instituteid = ConfigurationManager.AppSettings["instituteid"];
        readonly string userId = ConfigurationManager.AppSettings["smsServiceUid"];
        readonly string password = ConfigurationManager.AppSettings["smsServicePass"];


        DateTime currentDateTime;

        Thread objProcessThread;

        public Service()
        {
            InitializeComponent();
        }

        public void start()
        {

            string[] args = default(string[]);
            OnStart(args);
        }

        protected override void OnStart(string[] args)
        {
            currentDateTime = DateTime.Now;

            mainThreadObj = new Thread(ProcessThread);

            mainThreadObj.Start();

        }
        public void debug()
        {
            OnStart(null);
        }
        protected override void OnStop()
        {
        }
        private void ProcessThread()
        {



            if (objProcessThread == null ||
            objProcessThread.ThreadState == System.Threading.ThreadState.Stopped ||
            objProcessThread.ThreadState == System.Threading.ThreadState.Unstarted)
            {


                while (DateTime.Now >= currentDateTime.AddSeconds(10))//Every 10 Secs
                {
                    objProcessThread = new Thread(threadProcessMethod);

                    objProcessThread.IsBackground = true;
                    objProcessThread.Start();
                    //   AuthenticationCheck();
                    msaccessDatabase();
                       currentDateTime = currentDateTime.AddSeconds(10);
                    Thread.Sleep(1000 * 10);
                    ProcessThread();
                }
                Thread.Sleep(1000 * 10); //Every 10 Secs

                ProcessThread();
            }
        }

        private void threadProcessMethod()
        {
            Console.WriteLine(" This Method Will Call Every 10 Seconds : " + currentDateTime);
        }

        public bool msaccessDatabase()
        {
            AttendanceService objAttendanceService = new AttendanceService();
          var objLoginReponseModel=  objAttendanceService.AuthenticationCheck(5,"admin","123456");

            if (objLoginReponseModel.Success == "0")
            {
             //   DisplayListOutput(objLoginReponseModel.Message);
                return true;
            }
            if (objLoginReponseModel.Success == "1")
            {
                IEnumerable<MachineInfo> lstMachineInfo = null; //manipulator.GetLogData(objZkeeper, int.Parse(tbxMachineNumber.Text.Trim()), deviceinfo);

                if (lstMachineInfo != null && lstMachineInfo.ToList().Count > 0)
                {

                    var objMachineInfo = objAttendanceService.AttendanceDataPost(lstMachineInfo.ToList(), System.DateTime.Now, objLoginReponseModel);

                 //   BindToGridView(objMachineInfo.lstMachineInfo);
                   // ShowStatusBar(objMachineInfo.lstMachineInfo.ToList().Count + "  " + objMachineInfo.message, true);
                }
                else
                {
                  //  DisplayListOutput("No records found");
                }
                    
            }
            else
            {
              //  DisplayListOutput("Institute Id ,User and Password is not found. ");
            }


            var directoryInfo = new DirectoryInfo("C:\\SFA_WINDOWS_SERVICE_ERROR\\errorlog.txt");
            var folder = "C:\\SFA_WINDOWS_SERVICE_ERROR\\errorlog.txt";
            if (!directoryInfo.Exists)
            {
                Directory.CreateDirectory(folder);
            }
            var myDataTable = new DataTable();
            List<MachineInfo> lstMachineInfo1 = new List<MachineInfo>();
            using (var conection = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;" + "data source=C:\\Program Files (x86)\\ZKTeco\\att2000.mdb;"))
            {
                conection.Open();
                var query = "Select * From CHECKINOUT";
                var command = new OleDbCommand(query, conection);
                var reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    MachineInfo objMachineInfo = new MachineInfo();
                    var userId = reader[0].ToString();
                    objMachineInfo.IndRegID =Convert.ToInt16(userId);
                    var checktime = reader[1].ToString();
                    objMachineInfo.Name=checktime;
                    var machineserialNo = reader[7].ToString();
                    lstMachineInfo1.Add(objMachineInfo);


                }

            }            
            return true;
        }

        //public bool AuthenticationCheck()
        //{

        //    try
        //    {
        //        LoginReponseModel objLoginReponseModel = new LoginReponseModel();
        //     //   objLoginReponseModel.instituteid = 5;
        //     //   objLoginReponseModel.userid = "admin";
        //      //  objLoginReponseModel.password = "123456";

        //        //  string json = JsonConvert.SerializeObject(loginRequest);
        //        // var reponse = HttpService.deeppt(URLService.URL_LOGIN, json);
        //        string json = JsonConvert.SerializeObject(objLoginReponseModel);
        //        var reponse = HttpService.deeppt(URLService.URL_LOGIN, json, objLoginReponseModel);
        //        LoginReponseModel obj = JsonConvert.DeserializeObject<LoginReponseModel>(reponse);
        //        if (obj == null)
        //        {
        //            obj = new LoginReponseModel();
        //            FileReadWrite.SuccessLogging(reponse);
        //        }
        //        else if (obj.Success == "1")
        //        {
        //            var strAttendanceType = HttpService.get(URLService.URL_GET_ATTENDANCETYPE);

        //            VmUserAttendance objVmUserAttendance = new VmUserAttendance();
                   
        //            objVmUserAttendance.UserAttendance = new UserAttendance();
        //            objVmUserAttendance.UserAttendance.AbsentCount = 23;
        //            objVmUserAttendance.UserAttendance.InstituteId = Convert.ToInt16(instituteid);

        //            deepp.utility.UserInfoType userInfoType = (deepp.utility.UserInfoType.Teacher);

        //            objVmUserAttendance.UserAttendance.UserInfoTypeId =Convert.ToInt16(userInfoType);

        //            UserAttendanceDetail objUserAttendanceDetail = new UserAttendanceDetail();
                    
                    
        //            objUserAttendanceDetail.UserAttendanceId = 45;
        //            objVmUserAttendance.AttendanceDetails = new List<UserAttendanceDetail>();
        //            objVmUserAttendance.AttendanceDetails.ToList().Add(objUserAttendanceDetail);
                  
        //            string json2 = JsonConvert.SerializeObject(objVmUserAttendance);
        //            FileReadWrite.SuccessLogging(json2);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        FileReadWrite.ErrorLogging(ex);
        //        return false;
        //    }
           
        //    return true;
        //}

        //public bool MessageSend(SMSQueue item)
        //{

        //    try
        //    {



        //        //string baseUrl = "https://powersms.banglaphone.net.bd";
        //        //string userId = "shikkhaforall";
        //        //string password = "Shikkhaforall123";


        //        using (var client = new System.Net.Http.HttpClient())
        //        {
        //            client.BaseAddress = new Uri(baseUrl);
        //            client.DefaultRequestHeaders.ExpectContinue = false;
        //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //            var content = new FormUrlEncodedContent(new[]
        //                                             {
        //                                                new KeyValuePair<string, string>("userId", userId)
        //                                                ,new KeyValuePair<string, string>("password", password)
        //                                                ,new KeyValuePair<string, string>("smsText",item.Msg)
        //                                                ,new KeyValuePair<string, string>("commaSeperatedReceiverNumbers", item.CellNo)
        //                                             });

        //            var response = client.PostAsync("/httpapi/sendsms", content).Result;

        //            return response.IsSuccessStatusCode;
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        string connectionString = ConfigurationManager.ConnectionStrings["SMSServiceContext"].ConnectionString;
        //        SqlConnectionStringBuilder SConn = new SqlConnectionStringBuilder(connectionString);
        //        SqlConnection con = new SqlConnection();
        //        con.ConnectionString = connectionString;
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand();
        //        string vComTxt = vComTxt = @"Insert into  SMSError values(" + "'" + ex.Message.ToString() + "'" + "," + DateTime.Now + ")";





        //        cmd.CommandText = vComTxt;
        //        cmd.CommandType = CommandType.Text;
        //        cmd.Connection = con;
        //        cmd.ExecuteNonQuery();
        //        return false;
        //    }



        //}
        //public string MessageSend()
        //{
        //    string connectionString = ConfigurationManager.ConnectionStrings["SMSServiceContext"].ConnectionString;
        //    SqlConnectionStringBuilder SConn = new SqlConnectionStringBuilder(connectionString);
        //    SqlConnection con = new SqlConnection();
        //    con.ConnectionString = connectionString;

        //    con.Open();
        //    SqlCommand cmd = new SqlCommand();
        //    try
        //    {

        //        string vComTxt = @"SELECT SQ.Id, SQ.CellNo                             
        //                      ,SQ.Msg,SQ.RetryCount   
        //                      FROM  [SMSQueue] as SQ "
        //                        + @" WHERE  SQ.IsSent=0  and RetryCount<=3";
        //        List<SMSQueue> lstSMSQueue = new List<SMSQueue>();
        //        cmd.CommandText = vComTxt;
        //        cmd.CommandType = CommandType.Text;
        //        cmd.Connection = con;
        //        using (IDataReader dr = cmd.ExecuteReader())
        //        {
        //            while (dr.Read())
        //            {
        //                SMSQueue objSMSQueue = new SMSQueue();
        //                if (!String.IsNullOrEmpty(dr["Id"].ToString()))
        //                    objSMSQueue.Id = Convert.ToInt32(dr["Id"].ToString());
        //                if (!String.IsNullOrEmpty(dr["CellNo"].ToString()))
        //                    objSMSQueue.CellNo = dr["CellNo"].ToString();
        //                if (!String.IsNullOrEmpty(dr["Msg"].ToString()))
        //                    objSMSQueue.Msg = dr["Msg"].ToString();
        //                if (!String.IsNullOrEmpty(dr["RetryCount"].ToString()))
        //                    objSMSQueue.RetryCount = Convert.ToInt32(dr["RetryCount"].ToString());
        //                lstSMSQueue.Add(objSMSQueue);

        //            }
        //        }
        //        lstSMSQueue.ForEach(delegate (SMSQueue item)
        //        {
        //            var IsSent = MessageSend(item);
        //            item.RetryCount = item.RetryCount + 1;
        //            if (IsSent)
        //            {
        //                vComTxt = @"Update  SMSQueue set IsSent=1"
        //                     + @" WHERE  Id=" + item.Id;
        //            }
        //            else
        //            {
        //                vComTxt = @"Update  SMSQueue set IsSent=0,RetryCount=" + item.RetryCount + ""
        //                     + @" WHERE  Id=" + item.Id;
        //            }

        //            cmd.CommandText = vComTxt;
        //            cmd.CommandType = CommandType.Text;
        //            cmd.Connection = con;
        //            cmd.ExecuteNonQuery();
        //        });
        //        string vComTxt1 = @"Insert into  SMSError values(" + "'" + "Successfully Send" + "'" + "," + "'" + DateTime.Now + "'" + ")";

        //        cmd.CommandText = vComTxt1;
        //        cmd.CommandType = CommandType.Text;
        //        cmd.Connection = con;
        //        cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {

        //        string vComTxt = @"Insert into  SMSError values(" + "'" + ex.Message.ToString().Replace("'", " ") + "'" + "," + "'" + DateTime.Now + "'" + ")";

        //        cmd.CommandText = vComTxt;
        //        cmd.CommandType = CommandType.Text;
        //        cmd.Connection = con;
        //        cmd.ExecuteNonQuery();
        //    }
        //    string Msg = "";
        //    con.Close();
        //    con.Dispose();
        //    cmd.Dispose();
        //    return Msg;
        //}

    }
}
