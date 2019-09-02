///
///    Experimented By : Ozesh Thapa
///    Email: dablackscarlet@gmail.com
///
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Threading;
using System.Data;
using System.Data.OleDb;
using deepp.Entities.Models;
using deepp.attendance.ApiService;

namespace deepp.attendance
{
    public partial class Master : Form
    {
        private Thread t;
        DeviceManipulator manipulator = new DeviceManipulator();
        public ZkemClient objZkeeper;
        private bool isDeviceConnected = false;

        public bool IsDeviceConnected
        {
            get { return isDeviceConnected; }
            set
            {
                isDeviceConnected = value;
                if (isDeviceConnected)
                {
                    ShowStatusBar("The device is connected !!", true);
                    btnConnect.Text = "Disconnect";
                    ToggleControls(true);
                }
                else
                {
                    ShowStatusBar("The device is diconnected !!", true);
                    objZkeeper.Disconnect();
                    btnConnect.Text = "Connect";
                    ToggleControls(false);
                }
            }
        }


        private void ToggleControls(bool value)
        {
            btnBeep.Enabled = value;
            btnDownloadFingerPrint.Enabled = value;
            btnExcelExport.Enabled = value;
            btnPullData.Enabled = value;
            btnPowerOff.Enabled = value;
            btnRestartDevice.Enabled = value;
            btnGetDeviceTime.Enabled = value;
            btnDataPush.Enabled = value;
            btnDataPushBoth.Enabled = value;
            btnGetDeviceTime.Enabled = value;

            btnEnableDevice.Enabled = value;
            btnDisableDevice.Enabled = value;
            btnGetAllUserID.Enabled = value;
            btnUploadUserInfo.Enabled = value;
            tbxMachineNumber.Enabled = !value;
            tbxPort.Enabled = !value;
            tbxDeviceIP.Enabled = !value;


        }

        public Master()
        {
            InitializeComponent();
            ToggleControls(false);
            ShowStatusBar(string.Empty, true);
            DisplayEmpty();
        }


        /// <summary>
        /// Your Events will reach here if implemented
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="actionType"></param>
        private void RaiseDeviceEvent(object sender, string actionType)
        {
            switch (actionType)
            {
                case UniversalStatic.acx_Disconnect:
                    {
                        ShowStatusBar("The device is switched off", true);
                        DisplayEmpty();
                        btnConnect.Text = "Connect";
                        ToggleControls(false);
                        break;
                    }

                default:
                    break;
            }

        }


        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ShowStatusBar(string.Empty, true);

                if (IsDeviceConnected)
                {
                    IsDeviceConnected = false;
                    this.Cursor = Cursors.Default;

                    return;
                }

                string ipAddress = tbxDeviceIP.Text.Trim();
                string port = tbxPort.Text.Trim();
                if (ipAddress == string.Empty || port == string.Empty)
                    throw new Exception("The Device IP Address and Port is mandotory !!");

                int portNumber = 4370;
                if (!int.TryParse(port, out portNumber))
                    throw new Exception("Not a valid port number");

                bool isValidIpA = UniversalStatic.ValidateIP(ipAddress);
                if (!isValidIpA)
                    throw new Exception("The Device IP is invalid !!");

                isValidIpA = UniversalStatic.PingTheDevice(ipAddress);
                if (!isValidIpA)
                    throw new Exception("The device at " + ipAddress + ":" + port + " did not respond!!");

                objZkeeper = new ZkemClient(RaiseDeviceEvent);
                IsDeviceConnected = objZkeeper.Connect_Net(ipAddress, portNumber);

                if (IsDeviceConnected)
                {
                    string deviceInfo = manipulator.FetchDeviceInfo(objZkeeper, int.Parse(tbxMachineNumber.Text.Trim()));
                    lblDeviceInfo.Text = deviceInfo;
                }

            }
            catch (Exception ex)
            {
                ShowStatusBar(ex.Message, false);
            }
            this.Cursor = Cursors.Default;

        }


        public void ShowStatusBar(string message, bool type)
        {
            //if (InvokeRequired)
            //{
            //    // this.Invoke(new ShowStatusBar(SetText), new object[] { target, text });
            //    this.Invoke(new Action<string>(ShowStatusBar), new object[] { message, type });
            //    return;

            //}

            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendTextBox), new object[] { message });
                return;
            }

            if (message.Trim() == string.Empty)
            {
                lblStatus.Visible = false;
                return;
            }

            lblStatus.Visible = true;
            lblStatus.Text = message;
            lblStatus.ForeColor = Color.White;

            if (type)
                lblStatus.BackColor = Color.FromArgb(79, 208, 154);
            else
                lblStatus.BackColor = Color.FromArgb(230, 112, 134);
        }

        public void ShowStatusBar(string message)
        {
            //if (InvokeRequired)
            //{
            //    // this.Invoke(new ShowStatusBar(SetText), new object[] { target, text });
            //    this.Invoke(new Action<string>(ShowStatusBar), new object[] { message });
            //    return;

            //}

            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendTextBox), new object[] { message });
                return;
            }
            
            if (message.Trim() == string.Empty)
            {
                lblStatus.Visible = false;
                return;
            }

            lblStatus.Visible = true;
            lblStatus.Text = message;
            lblStatus.ForeColor = Color.White;           
        }
        private void btnPingDevice_Click(object sender, EventArgs e)
        {
            //ShowStatusBar(string.Empty, true);

            //string ipAddress = tbxDeviceIP.Text.Trim();

            //bool isValidIpA = UniversalStatic.ValidateIP(ipAddress);
            //if (!isValidIpA)
            //    throw new Exception("The Device IP is invalid !!");

            //isValidIpA = UniversalStatic.PingTheDevice(ipAddress);
            //if (isValidIpA)
            //    ShowStatusBar("The device is active", true);
            //else
            //    ShowStatusBar("Could not read any response", false);
        }

        private void btnGetAllUserID_Click(object sender, EventArgs e)
        {
            try
            {
                ICollection<UserIDInfo> lstUserIDInfo = manipulator.GetAllUserID(objZkeeper, int.Parse(tbxMachineNumber.Text.Trim()));

                if (lstUserIDInfo != null && lstUserIDInfo.Count > 0)
                {
                    BindToGridView(lstUserIDInfo);
                    ShowStatusBar(lstUserIDInfo.Count + " records found !!", true);
                }
                else
                {
                    DisplayEmpty();
                    DisplayListOutput("No records found");
                }

            }
            catch (Exception ex)
            {
                DisplayListOutput(ex.Message);
            }

        }

        private void btnBeep_Click(object sender, EventArgs e)
        {
            objZkeeper.Beep(100);
        }

        private void btnDownloadFingerPrint_Click(object sender, EventArgs e)
        {
            try
            {
                ShowStatusBar(string.Empty, true);

                ICollection<UserInfo> lstFingerPrintTemplates = manipulator.GetAllUserInfo(objZkeeper, int.Parse(tbxMachineNumber.Text.Trim()));
                if (lstFingerPrintTemplates != null && lstFingerPrintTemplates.Count > 0)
                {
                    BindToGridView(lstFingerPrintTemplates);
                    ShowStatusBar(lstFingerPrintTemplates.Count + " records found !!", true);
                }
                else
                    DisplayListOutput("No records found");
            }
            catch (Exception ex)
            {
                DisplayListOutput(ex.Message);
            }

        }


        private void btnPullData_Click(object sender, EventArgs e)
        {
            try
            {
                ShowStatusBar(string.Empty, true);

                ICollection<MachineInfo> lstMachineInfo = manipulator.GetLogData(objZkeeper, int.Parse(tbxMachineNumber.Text.Trim()), lblDeviceInfo.Text);

                if (lstMachineInfo != null && lstMachineInfo.Count > 0)
                {
                    BindToGridView(lstMachineInfo);
                    ShowStatusBar(lstMachineInfo.Count + " records found !!", true);
                }
                else
                    DisplayListOutput("No records found");
            }
            catch (Exception ex)
            {
                DisplayListOutput(ex.Message);
            }

        }


        private void ClearGrid(string ff)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(ClearGrid), new object[] { ff });
                return;

            }

            if (dgvRecords.Controls.Count > 2)
            { dgvRecords.Controls.RemoveAt(2); }


            dgvRecords.DataSource = null;
            dgvRecords.Controls.Clear();
            dgvRecords.Rows.Clear();
            dgvRecords.Columns.Clear();
        }
        private void BindToGridView(object list)
        {

            ClearGrid("dsd");
            if (InvokeRequired)
            {

                this.Invoke(new Action<string>(BindToGridView), new object[] { list.ToString() });
                return;

            }
            dgvRecords.DataSource = list;
            dgvRecords.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            UniversalStatic.ChangeGridProperties(dgvRecords);
        }



        private void DisplayListOutput(string message)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(DisplayListOutput), new object[] { message });
                return;

            }
            if (dgvRecords.Controls.Count > 2)
            { dgvRecords.Controls.RemoveAt(2); }

            ShowStatusBar(message, false);
        }

        private void DisplayEmpty()
        {
            ClearGrid("dsf");
            dgvRecords.Controls.Add(new DataEmpty());
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        { UniversalStatic.DrawLineInFooter(pnlHeader, Color.FromArgb(204, 204, 204), 2); }



        private void btnPowerOff_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            var resultDia = DialogResult.None;
            resultDia = MessageBox.Show("Do you wish to Power Off the Device ??", "Power Off Device", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultDia == DialogResult.Yes)
            {
                bool deviceOff = objZkeeper.PowerOffDevice(int.Parse(tbxMachineNumber.Text.Trim()));

            }

            this.Cursor = Cursors.Default;
        }

        private void btnRestartDevice_Click(object sender, EventArgs e)
        {

            DialogResult rslt = MessageBox.Show("Do you wish to restart the device now ??", "Restart Device", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rslt == DialogResult.Yes)
            {
                if (objZkeeper.RestartDevice(int.Parse(tbxMachineNumber.Text.Trim())))
                    ShowStatusBar("The device is being restarted, Please wait...", true);
                else
                    ShowStatusBar("Operation failed,please try again", false);
            }

        }

        private void btnGetDeviceTime_Click(object sender, EventArgs e)
        {
            int machineNumber = int.Parse(tbxMachineNumber.Text.Trim());
            int dwYear = 0;
            int dwMonth = 0;
            int dwDay = 0;
            int dwHour = 0;
            int dwMinute = 0;
            int dwSecond = 0;

            bool result = objZkeeper.GetDeviceTime(machineNumber, ref dwYear, ref dwMonth, ref dwDay, ref dwHour, ref dwMinute, ref dwSecond);

            string deviceTime = new DateTime(dwYear, dwMonth, dwDay, dwHour, dwMinute, dwSecond).ToString();
            List<DeviceTimeInfo> lstDeviceInfo = new List<DeviceTimeInfo>();
            lstDeviceInfo.Add(new DeviceTimeInfo() { DeviceTime = deviceTime });
            BindToGridView(lstDeviceInfo);
        }


        private void btnEnableDevice_Click(object sender, EventArgs e)
        {
            // This is of no use since i implemented zkemKeeper the other way
            bool deviceEnabled = objZkeeper.EnableDevice(int.Parse(tbxMachineNumber.Text.Trim()), true);

        }



        private void btnDisableDevice_Click(object sender, EventArgs e)
        {
            // This is of no use since i implemented zkemKeeper the other way
            bool deviceDisabled = objZkeeper.DisableDeviceWithTimeOut(int.Parse(tbxMachineNumber.Text.Trim()), 3000);
        }

        private void tbxPort_TextChanged(object sender, EventArgs e)
        { UniversalStatic.ValidateInteger(tbxPort); }

        private void tbxMachineNumber_TextChanged(object sender, EventArgs e)
        { UniversalStatic.ValidateInteger(tbxMachineNumber); }

        private void btnUploadUserInfo_Click(object sender, EventArgs e)
        {
            // Add you new UserInfo Here and  uncomment the below code
            //List<UserInfo> lstUserInfo = new List<UserInfo>();
            //manipulator.UploadFTPTemplate(objZkeeper, int.Parse(tbxMachineNumber.Text.Trim()), lstUserInfo);
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {


            ShowStatusBar(string.Empty, true);

            ICollection<MachineInfo> lstMachineInfo = manipulator.GetLogData(objZkeeper, int.Parse(tbxMachineNumber.Text.Trim()), lblDeviceInfo.Text);

            if (lstMachineInfo != null && lstMachineInfo.Count > 0)
            {
                BindToGridView(lstMachineInfo);
                ShowStatusBar(lstMachineInfo.Count + " records found !!", true);
            }
            else
                DisplayListOutput("No records found");


            object oMissing = Missing.Value;

            //Microsoft.Office.Interop.Excel.Application app;
            //Microsoft.Office.Interop.Excel.Workbook wkBk;
            //Microsoft.Office.Interop.Excel.Worksheet wkSht;

            string filename = @"D:\test3.xls";
            FileStream fs = new FileStream(filename, FileMode.Create);
            fs.Close();

            //app = new Microsoft.Office.Interop.Excel.Application();
            //app.DisplayAlerts = false;
            //wkBk = app.Workbooks.Open(filename, oMissing, oMissing, oMissing, oMissing, oMissing,
            //    oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);

            //wkSht = (Microsoft.Office.Interop.Excel.Worksheet)wkBk.Sheets.get_Item(1);

            //for (int i = 1; i < lstMachineInfo.ToList().Count; i++)
            //{
            //    //if (i==1)
            //    //{
            //    //    wkSht.Cells[1,]
            //    //}
            //    //else
            //    //{
            //    wkSht.Cells[i, 1] = lstMachineInfo.ToList()[i].IndRegID;
            //    wkSht.Cells[i, 2] = lstMachineInfo.ToList()[i].MachineNumber;
            //    wkSht.Cells[i, 3] = lstMachineInfo.ToList()[i].DateOnlyRecord;
            //    wkSht.Cells[i, 4] = lstMachineInfo.ToList()[i].DateTimeRecord;
            //    wkSht.Cells[i, 5] = lstMachineInfo.ToList()[i].TimeOnlyRecord;
            //    //}         

            //}
            //// wkSht.Cells[RowIndex, ColumnIndex]=...

            //wkBk.Save();
            //app.Visible = true;

            ////  wkBk.Close(oMissing, strFilePath, oMissing);
            //app.Quit();

            ////House keeping work for releasing COM references.
            //Marshal.ReleaseComObject(wkSht);
            //Marshal.ReleaseComObject(wkBk);
            //Marshal.ReleaseComObject(app);

            //wkSht = null;
            //wkBk = null;
            //app = null;
            GC.Collect();
        }

        private void btnDataPush_Click(object sender, EventArgs e)
        {
            if (maskedTextBoxInstitute.Text == string.Empty)
            {
                MessageBox.Show("Please enter a value to Institute Id!");
                return;
            }
            else if (textBoxUserId.Text == string.Empty)
            {
                MessageBox.Show("Please enter a value to User Id!");
                return;
            }
            else if (textBoxPass.Text == string.Empty)
            {
                MessageBox.Show("Please enter a value to Password!");
                return;
            }
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            DateTime dt = DateTime.Now; // Or whatever
                                        //  string s = dt.ToString("yyyyMMddHHmmss");
            string us = dt.ToString(new CultureInfo("en-US"));
            string uk = dt.ToString(new CultureInfo("en-GB"));
            //DateTime dt;


            string deviceinfo = lblDeviceInfo.Text;
            string InstituteId = (maskedTextBoxInstitute.Text);
            string UserId = textBoxUserId.Text;
            string Password = textBoxPass.Text;

            AttendanceService objAttendanceService = new AttendanceService(this);
            var objLoginReponseModel = objAttendanceService.AuthenticationCheck(Convert.ToInt16(InstituteId), UserId, Password);


            if (objLoginReponseModel.Success == "0")
            {
                DisplayListOutput(objLoginReponseModel.Message);
                return;
            }
            if (objLoginReponseModel.Success == "1")
            {
                IEnumerable<MachineInfo> lstMachineInfo = manipulator.GetLogData(objZkeeper, int.Parse(tbxMachineNumber.Text.Trim()), deviceinfo);


                if (lstMachineInfo != null && lstMachineInfo.ToList().Count > 0)
                {

                    var objMachineInfo = objAttendanceService.AttendanceDataPost(lstMachineInfo.ToList().ToList(), System.DateTime.Now, objLoginReponseModel);

                    BindToGridView(objMachineInfo.lstMachineInfo);
                    ShowStatusBar(objMachineInfo.lstMachineInfo.ToList().Count + "  " + objMachineInfo.message, true);
                }
                else
                    DisplayListOutput("No records found");
            }
            else
            {
                DisplayListOutput("Institute Id ,User and Password is not found. ");
            }


        }

        public List<MachineInfo> GetFromMachinedataBase()
        {
            List<MachineInfo> lstMachineInfo = new List<MachineInfo>();

            var directoryInfo = new DirectoryInfo("C:\\SFA_WINDOWS_SERVICE_ERROR\\errorlog.txt");
            var folder = "C:\\SFA_WINDOWS_SERVICE_ERROR\\errorlog.txt";
            if (!directoryInfo.Exists)
            {
                Directory.CreateDirectory(folder);
            }
            var myDataTable = new DataTable();
            using (var conection = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;" + "data source=C:\\Program Files (x86)\\ZKTeco\\att2000.mdb;"))
            {
                conection.Close();

                conection.Open();
                var query = "Select USEINFO.BADGENUMBER,INOUT.CHECKTIME,INOUT.CHECKTYPE,INOUT.VERIFYCODE,INOUT.SENSORID,INOUT.Memoinfo,INOUT.WorkCode,INOUT.sn,TITLE, DEFAULTDEPTID From CHECKINOUT as INOUT inner join USERINFO AS USEINFO ON USEINFO.USERID=INOUT.USERID";
                var command = new OleDbCommand(query, conection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    MachineInfo objMachineInfo = new MachineInfo();
                    var BADGENNUMB = reader[0].ToString();
                    var checktime = reader[1].ToString();
                    var machineserialNo = reader[7].ToString();
                    var TITLE = reader[8].ToString();
                    objMachineInfo.IndRegID = Convert.ToInt16(BADGENNUMB);
                    objMachineInfo.DateTimeRecord = checktime;
                    objMachineInfo.deviceinfo = (machineserialNo);
                    objMachineInfo.StudentOrTeacherId = (TITLE);
                    lstMachineInfo.Add(objMachineInfo);
                }

            }
            return lstMachineInfo;
        }
        private void AttendanceEngine()
        {
            if (maskedTextBoxInstitute.Text == string.Empty)
            {
                MessageBox.Show("Please enter a value to Institute Id!");
                ShowStatusBar("");
                enableDataAttendanceButton(true);
                return;
            }
            else if (textBoxUserId.Text == string.Empty)
            {
                MessageBox.Show("Please enter a value to User Id!");
                ShowStatusBar("");
                enableDataAttendanceButton(true);
                return;
            }
            else if (textBoxPass.Text == string.Empty)
            {
                MessageBox.Show("Please enter a value to Password!");
                ShowStatusBar("");
                enableDataAttendanceButton(true);
                return;
            }
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            DateTime dt = DateTime.Now;

            string us = dt.ToString(new CultureInfo("en-US"));
            string uk = dt.ToString(new CultureInfo("en-GB"));


            string deviceinfo = lblDeviceInfo.Text;
            string InstituteId = (maskedTextBoxInstitute.Text);
            string UserId = textBoxUserId.Text;
            string Password = textBoxPass.Text;
            
            AttendanceService objAttendanceService = new AttendanceService(this);
            var objLoginReponseModel = objAttendanceService.AuthenticationCheck(Convert.ToInt16(InstituteId), UserId, Password);

            if (objLoginReponseModel.Success == "0")
            {
                ShowStatusBar("Authentication Failed! Please try again.");
                DisplayListOutput(objLoginReponseModel.Message);
                enableDataAttendanceButton(true);
                return;
            }
            if (objLoginReponseModel.Success == "1")
            {
                ShowStatusBar("Authentication Success.");
                IEnumerable<MachineInfo> lstMachineInfo = manipulator.GetLogData(objZkeeper, int.Parse(tbxMachineNumber.Text.Trim()), deviceinfo);
                if (lstMachineInfo != null && lstMachineInfo.ToList().Count > 0)
                {
                    var objMachineInfo = objAttendanceService.AttendanceDataPostBoth(lstMachineInfo.ToList(), System.DateTime.Now, objLoginReponseModel);
                    ShowStatusBar("Process Completed.");
                }
                else
                {
                    DisplayListOutput("No records found");
                    ShowStatusBar("No records found to push.");
                }
            }
            else
            {
                DisplayListOutput("Institute Id, User and Password is not found.");
                ShowStatusBar("Institute Id, User and Password is not found.");
            }
            enableDataAttendanceButton(true);
        }

        private void log(String msg)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(log), new object[] { msg + "\r\n" });
                return;
            }
            // label_message.Text = msg;
        }



        private void btnDataPushBoth_Click(object sender, EventArgs e)
        {
            enableDataAttendanceButton(false);
            ShowStatusBar("Engine started..");
            Thread t = new Thread(AttendanceEngine);
            t.Start();
        }
        
        public void enableDataAttendanceButton(bool status)
        {
            btnDataPushBoth.BeginInvoke((Action)delegate () { btnDataPushBoth.Enabled = status; });
        }

        public void AppendTextBox(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendTextBox), new object[] { value });
                return;
            }
            lblStatus.Text = value;
        }        

    }
}
