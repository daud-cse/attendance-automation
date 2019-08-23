using deepp.Entities.AttendanceMachineModel;
using deepp.Entities.Models;
using deepp.Entities.ViewModels;
using deepp.Service.ViewModels;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.IO;
using DocumentStaticData = deepp.utility.StaticData.Document;
using System.Reflection;
using System.Configuration;
using deepp.erp;

namespace deepp.erp.Api.Attendance
{
    public class AttendanceDataUploadController : ApiController
    {
        private readonly IVmUserAttendanceService _vmUserAdendanceService;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        private readonly IStoredProcedures _storedProcedures;

        readonly int instituteId = Sessions.InstituteId;
        readonly int userId = Sessions.UserId;

        public AttendanceDataUploadController(
            IVmUserAttendanceService vmUserAdendanceService,
            IUnitOfWorkAsync unitOfWorkAsync
           , IStoredProcedures storedProcedures)
        {
            _vmUserAdendanceService = vmUserAdendanceService;
            _unitOfWorkAsync = unitOfWorkAsync;
            _storedProcedures = storedProcedures;
        }



        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/AttendanceDataUpload/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/AttendanceDataUpload
        [Route("api/AttendanceExcelUpload")]
        [HttpPost]
        public VmUserAttendance AttendanceExcelUpload(List<Document> Documents)
        {

           

            VmUserAttendance objVmTeacherDetails = new VmUserAttendance();
            try
            {
                string TEMP_FOLDER_LOCATION = ConfigurationManager.AppSettings["Excelpath"].ToString();
                var objVmAttendanceDataSynInfo = _storedProcedures.GetAttendanceDataSynInfo(instituteId, 13, string.Empty);

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
                var doc = Documents;
                System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
                var array = Convert.FromBase64String(doc.FirstOrDefault().base64Blob);
                var foldername = Guid.NewGuid().ToString();
                string fileLocation = TEMP_FOLDER_LOCATION + foldername + DocumentStaticData.BACK_SLASH + doc.FirstOrDefault().FileName;

                if (!System.IO.Directory.Exists(TEMP_FOLDER_LOCATION + foldername))
                {
                    System.IO.Directory.CreateDirectory(TEMP_FOLDER_LOCATION + foldername);
                }
                System.IO.File.WriteAllBytes(fileLocation, array);
                var lstAttendanceExcel = ProcessExcel(4, fileLocation);


                //if (System.IO.Directory.Exists(TEMP_FOLDER_LOCATION + foldername))
                //{
                //    DirectoryInfo dir = new DirectoryInfo(fileLocation);

                //    foreach (FileInfo fi in dir.GetFiles())
                //    {
                //        fi.Delete();
                //    }
                //}

                if (!string.IsNullOrEmpty(lstAttendanceExcel.FirstOrDefault().Messsage))
                {
                    objVmTeacherDetails.Message = lstAttendanceExcel.FirstOrDefault().Messsage;
                    return objVmTeacherDetails;
                }
                var lstLastAttendanceData = lstAttendanceExcel.Where(x => Convert.ToDateTime(x.DateOnlyRecord) > Convert.ToDateTime(objVmAttendanceDataSynInfo.lstLastAttendanceSynDate.FirstOrDefault().LastAttendanceSynDate) && Convert.ToDateTime(x.DateTimeRecord) <= DateTime.Now);
                if (lstLastAttendanceData.Any()){
                    objVmTeacherDetails = _vmUserAdendanceService.SaveUpdateTeacherAttendanceFromExcel(instituteId, lstLastAttendanceData.ToList(), _unitOfWorkAsync);
                    objVmTeacherDetails.Message = "Successfully Data Uploaded";
                }
                else
                {
                    objVmTeacherDetails.Message = "No Data Found for Uploaded Already all data uploaded upto ("+ objVmAttendanceDataSynInfo.lstLastAttendanceSynDate.FirstOrDefault().LastAttendanceSynDate +")";
                }

              
              
                return objVmTeacherDetails;
            }
            catch (Exception ex)
            {
                objVmTeacherDetails.Message = ex.Message.ToString();
                return objVmTeacherDetails;
            }

        }
        private List<AttendanceExcel> ProcessExcel(int sheetNo, string fileLocation)
        {
            List<AttendanceExcel> lstAttendanceExcel = new List<AttendanceExcel>();
            try
            {
                List<string> columns = new List<string>();
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                if (!string.IsNullOrEmpty(fileLocation))
                {
                    Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(fileLocation);//(@"D:/C.xlsx");
                    Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[sheetNo];
                    Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;
                    int rowCount = xlRange.Rows.Count;
                    int numSheets = xlWorkbook.Sheets.Count;                    
                    if (numSheets!=4)
                    {
                        AttendanceExcel objAttendanceExcel = new AttendanceExcel();
                        objAttendanceExcel.Messsage = "Invalid Excel File !! Something worng";
                        lstAttendanceExcel.Add(objAttendanceExcel);
                        return lstAttendanceExcel;
                    }
                    //int rowCount = 20;
                    int colCount = 6;//xlRange.Columns.Count;
                    int colOrder = 0;
                    var excelSheet = xlRange.Cells;                    
                    for (int i = 5; i <= excelSheet.Rows.Count; i++)
                    {
                        AttendanceExcel objMachineInfo = new AttendanceExcel();
                        for (int j = 1; j <= colCount; j++)
                        {
                            var property = Convert.ToString(excelSheet.Cells[3, j].Value2);
                            string value = string.Empty;
                            string onduty = string.Empty;
                            string offduty = string.Empty;
                            if (property == "First time zone")
                            {
                                property = Convert.ToString(excelSheet.Cells[4, j].Value2);
                                onduty = Convert.ToString(excelSheet.Cells[i, j].Value2);
                                objMachineInfo = DataPrepare(objMachineInfo, property, onduty);
                                if (!String.IsNullOrEmpty(onduty))
                                {
                                    int index = onduty.IndexOf(":");
                                    string hour = onduty.Substring(0, index);
                                    string minutes = onduty.Substring(index + 1);
                                    onduty = objMachineInfo.DateOnlyRecord.Date.AddHours(Convert.ToDouble(hour)).AddMinutes(Convert.ToDouble(minutes)).ToString();
                                }
                                objMachineInfo = DataPrepare(objMachineInfo, property, onduty);
                                j = j + 1;
                                property = Convert.ToString(excelSheet.Cells[4, j].Value2);
                                offduty = Convert.ToString(excelSheet.Cells[i, j].Value2);
                                if (!String.IsNullOrEmpty(offduty))
                                {
                                    int index = offduty.IndexOf(":");
                                    string hour = offduty.Substring(0, index);
                                    string minutes = offduty.Substring(index + 1);
                                    offduty = objMachineInfo.DateOnlyRecord.Date.AddHours(Convert.ToDouble(hour)).AddMinutes(Convert.ToDouble(minutes)).ToString();
                                }
                                objMachineInfo = DataPrepare(objMachineInfo, property, offduty);

                            }
                            else
                            {
                                value = Convert.ToString(excelSheet.Cells[i, j].Value2);
                                objMachineInfo = DataPrepare(objMachineInfo, property, value);

                            }
                        }
                        lstAttendanceExcel.Add(objMachineInfo);
                        int progress = i * 40 / rowCount;

                    }


                    return lstAttendanceExcel;
                }
                else
                {
                    return lstAttendanceExcel;
                }
            }
            catch (Exception ex)
            {
                AttendanceExcel objAttendanceExcel = new AttendanceExcel();
                objAttendanceExcel.Messsage = ex.Message.ToString();               
                return lstAttendanceExcel;
            }
        }
        public AttendanceExcel DataPrepare(AttendanceExcel objMachineInfo, string property, string value)
        {
            PropertyInfo prop = objMachineInfo.GetType().GetProperty(PeopertyMapping(property), BindingFlags.Public | BindingFlags.Instance);
            if (null != prop && prop.CanWrite)
            {
                if (prop.PropertyType == typeof(Int32))
                {
                    prop.SetValue(objMachineInfo, Convert.ToInt32(value), null);
                }
                else if (prop.PropertyType == typeof(System.DateTime))
                {
                    if (value == null)
                    {
                        prop.SetValue(objMachineInfo, value, null);
                    }
                    else
                    {
                        prop.SetValue(objMachineInfo, DateTime.Parse(value), null);
                    }
                }
                else if (property == "On-duty" || property == "Off-duty")
                {
                    if (value == null)
                    {
                        prop.SetValue(objMachineInfo, value, null);
                    }
                    else
                    {
                        prop.SetValue(objMachineInfo, DateTime.Parse(value), null);
                    }
                }

                else
                {
                    prop.SetValue(objMachineInfo, value, null);
                }

            }
            return objMachineInfo;
        }
        // PUT: api/AttendanceDataUpload/5
        public void Put(int id, [FromBody]string value)
        {


            VmUserAttendance objVmTeacherDetails = new VmUserAttendance();
            List<VmUserAttendance> lstuserAttendanceModel = new List<VmUserAttendance>();
            try
            {
                var objVmAttendanceDataSynInfo = _storedProcedures.GetAttendanceDataSynInfo(instituteId, 13, string.Empty);

                if (objVmAttendanceDataSynInfo == null)
                {
                    objVmTeacherDetails.Message = "Institute Attendance Date Confiure Not Found";
                    // return objVmTeacherDetails;
                }
                if (objVmAttendanceDataSynInfo.lstLastAttendanceSynDate == null)
                {
                    objVmTeacherDetails.Message = "Institute Attendance Date Confiure Not Found";
                    // return objVmTeacherDetails;
                }

                //if (objVmAttendanceDataSynInfo.lstLastAttendanceSynDate.FirstOrDefault().MachineSerialNo == lstMachineInfoTeacher.FirstOrDefault().deviceinfo || objVmAttendanceDataSynInfo.lstLastAttendanceSynDate.FirstOrDefault().MachineSerialNo1 == lstMachineInfoTeacher.FirstOrDefault().deviceinfo || objVmAttendanceDataSynInfo.lstLastAttendanceSynDate.FirstOrDefault().MachineSerialNo2 == lstMachineInfoTeacher.FirstOrDefault().deviceinfo || objVmAttendanceDataSynInfo.lstLastAttendanceSynDate.FirstOrDefault().MachineSerialNo3 == lstMachineInfoTeacher.FirstOrDefault().deviceinfo || objVmAttendanceDataSynInfo.lstLastAttendanceSynDate.FirstOrDefault().MachineSerialNo4 == lstMachineInfoTeacher.FirstOrDefault().deviceinfo)
                //{
                //    objVmTeacherDetails.Message = "Device Info is not match";
                //    return objVmTeacherDetails;
                //}
                var lstMachineInfoTeacher = new List<MachineInfo>();

               // _vmUserAdendanceService.SaveUpdateTeacherAttendanceFromExcel(instituteId, lstMachineInfoTeacher, _unitOfWorkAsync);
               // objVmTeacherDetails.Message = "Successfully Data Uploaded";



                //  return objVmTeacherDetails;
            }
            catch (Exception ex)
            {
                objVmTeacherDetails = new VmUserAttendance { Message = ex.ToString() };
                // return objVmTeacherDetails;
            }
        }
        private string PeopertyMapping(string name)
        {
            string output = string.Empty;


            if (name == "ID")
            {
                output = "IndRegID";
            }
            else if (name == "Name")
            {
                output = "Name";
            }
            else if (name == "Date")
            {
                output = "DateOnlyRecord";
            }
            else if (name == "On-duty")
            {
                output = "InTime";
            }
            else if (name == "On-duty")
            {
                output = "InTime";
            }

            else if (name == "Off-duty")
            {
                output = "OutTime";
            }

            return output;
        }
        // DELETE: api/AttendanceDataUpload/5
        public void Delete(int id)
        {
        }
    }
}

public class Document
{
    public string FileName { get; set; }
    public string base64Blob { get; set; }
}
