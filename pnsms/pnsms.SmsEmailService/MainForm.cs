using Microsoft.Practices.Unity;
using pnsms.Entities.Models;
using pnsms.Service;
using pnsms.utility;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pnsms.SmsEmailService
{
    public partial class MainForm : Form
    {
        IDataContext dataContext;
        IUnityContainer container;
        IShortMessageOuterService service;

        public int SmsThreadCount = 0;
        public int EmailThreadCount = 0;
        bool isSmsTimerStart = false;
        bool isEmailTimerStart = false;

        #region Forms
        public MainForm(IUnityContainer container)
        {
            InitializeComponent();
            this.container = container;
            service = container.Resolve<ShortMessageOuterService>();

            dataContext = container.Resolve<PNSMSContext>();
        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing) return;
            e.Cancel = true;

            notifyIconMain.Visible = true;
            notifyIconMain.ShowBalloonTip(3000);
            this.ShowInTaskbar = false;
            this.Hide();
        }

        private void notifyIconMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            notifyIconMain.Visible = false;
        }
        #endregion

        #region Buttons

        private void btnSms_Click(object sender, EventArgs e)
        {
            isSmsTimerStart = !isSmsTimerStart;

            if (isSmsTimerStart)
            {
                tmrSms.Enabled = true;
                tmrSms.Start();
                btnSms.Text = "Stop";
                lblSms.Text = "SMS Service is running";
            }
            else
            {
                tmrSms.Enabled = false;
                tmrSms.Stop();
                btnSms.Enabled = false;
                btnSms.Text = "Start";
                lblSms.Text = "SMS Service is stopping all threads.";

                tbSms.Text = "Currently running threads : " + SmsThreadCount;

                if (SmsThreadCount <= 0 && !isSmsTimerStart)
                {
                    btnSms.Enabled = true;
                    lblSms.Text = "SMS Service is stopped";
                    tbSms.Text = "SMS Service notification";
                }
            }
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Timer

        private void tmrSms_Tick(object sender, EventArgs e)
        {
            tmrSms.Stop();
            tmrSms.Enabled = false;

            Task[] tasks = new Task[4];
            tasks[0] = Task.Factory.StartNew(() => SendSMS());
            tasks[1] = Task.Factory.StartNew(() => GetDeliveryStatus());
            tasks[2] = Task.Factory.StartNew(() => Check());
            tasks[3] = Task.Factory.StartNew(() => GenerateSMS());
            Task.WaitAll(tasks);

            tmrSms.Enabled = true;
            tmrSms.Start();

            for (int i = 0; i < 4; i++)
            {
                tasks[i].Dispose();
                tasks[i] = null;
            }

            GC.Collect();

        }

        private void tmrEmail_Tick(object sender, EventArgs e)
        {

        }

        #endregion

        private void SendSMS()
        {
            int takeAtATime = 500;
            DateTime dt = DateTime.Now;
            IEnumerable<ShortMessage> smsList = null;
            using (var unitOfWork = container.Resolve<UnitOfWork>())
            {
                var repoSms = unitOfWork.RepositoryAsync<ShortMessage>();

                smsList = repoSms
                    .Query(c => !c.IsSent && c.IsGenerated && c.SendAt <= dt)
                    .Include(c => c.ShortMessageDetails)
                    .Select();

                if (smsList.Count() <= 0)
                {
                    NotficationInvokeMethod("No SMS Found at " + DateTime.Now.ToShortTimeString(), false);
                    return;
                }



                foreach (var row in smsList)
                {
                    if (row.IsStaticSms)
                    {
                        var idata = row.ShortMessageDetails.Where(c => !c.IsSent);
                        List<ShortMessageDetail> data = new List<ShortMessageDetail>();

                        if (idata.Count() > 0)
                        {
                            data = idata.ToList();
                        }


                        for (int i = 0; i < data.Count(); i++)
                        {
                            BanglaPhoneSmsParams param = new BanglaPhoneSmsParams();
                            BanglaPhoneSms bngSmsSender = new BanglaPhoneSms();
                            param.MaskText = row.Mask;

                            //get top 500
                            var selectRows = data.Skip(i).Take(takeAtATime);
                            i = i + takeAtATime - 1;

                            //get the 500 ids
                            var ids = selectRows.Select(c => c.Id).ToList();

                            param.MobileNumbers = selectRows
                                .Select(c => c.MobileNumber)
                                .ToList();
                            param.Sms = row.SmsTemplate;
                            param.Entities = selectRows.ToList();

                            Thread th = new Thread(new ParameterizedThreadStart(bngSmsSender.SendSms));
                            th.Start(param);
                            SmsThreadCount++;
                            bngSmsSender.ThreadDone += SMS_ThreadDone;

                            Thread.Sleep(50);
                        }
                    }
                    else
                    {
                        var data = row.ShortMessageDetails.Where(c => !c.IsSent);
                        foreach (var selectRow in data)
                        {
                            BanglaPhoneSmsParams param = new BanglaPhoneSmsParams();
                            BanglaPhoneSms bngSmsSender = new BanglaPhoneSms();

                            param.MobileNumbers = new List<string> { selectRow.MobileNumber };
                            param.Sms = selectRow.SmsText;
                            param.Entities = new List<ShortMessageDetail> { selectRow };
                            param.MaskText = row.Mask;

                            Thread th = new Thread(new ParameterizedThreadStart(bngSmsSender.SendSms));
                            th.Start(param);
                            SmsThreadCount++;
                            bngSmsSender.ThreadDone += SMS_ThreadDone;

                            Thread.Sleep(50);
                        }


                    }

                    row.IsSent = true;
                    repoSms.Update(row);
                }

                unitOfWork.SaveChanges();
            }
        }

        private void GetDeliveryStatus()
        {
            using (var unitOfWork = container.Resolve<UnitOfWork>())
            {
                var repositoryDetail = unitOfWork.RepositoryAsync<ShortMessageDetail>();

                List<string> gatewayIds = repositoryDetail
                                    .Query(c => c.IsSent && c.ShortMessageStatusId == null)
                                    .Select(c => c.GatewayIdentificationNo)
                                    .Distinct<string>()
                                    .ToList();

                BanglaPhoneSms bngSmsSender = new BanglaPhoneSms();
                BanglaPhoneDLRParams param = new BanglaPhoneDLRParams();

                param.GatewayId = gatewayIds;

                if (gatewayIds.Count() > 0)
                {
                    Thread th = new Thread(new ParameterizedThreadStart(bngSmsSender.GetDeliveryStatus));

                    th.Start(param);
                    SmsThreadCount++;
                    bngSmsSender.ThreadDone += DLR_ThreadDone;
                }
                else
                {
                    NotficationInvokeMethod("No DLR Found at " + DateTime.Now.ToShortTimeString(),false);
                }

                Thread.Sleep(50);
            }
        }

        private void Check()
        {
            using (var unitOfWork = container.Resolve<UnitOfWork>())
            {
                var repoSms = unitOfWork.RepositoryAsync<ShortMessage>();
                var repoSmsDetail = unitOfWork.RepositoryAsync<ShortMessageDetail>();

                var shortMessages = repoSms.Query(c => c.IsChecked == null && c.IsSent).Include(c => c.ShortMessageDetails).Select();

                foreach (var entity in shortMessages)
                {
                    if (entity.ShortMessageDetails.Any(c => !c.IsSent))
                    {
                        entity.IsSent = false;
                    }
                    else
                    {
                        entity.IsChecked = true;
                    }

                    repoSms.Update(entity);
                }

                unitOfWork.SaveChanges();
                NotficationInvokeMethod("Checking completed at " + DateTime.Now.ToShortTimeString(),false);
                
            }
        }

        private void GenerateSMS()
        {
            string str = service.GenerateSms();
            NotficationInvokeMethod("SMS Generator completed at " + DateTime.Now.ToShortTimeString() + " | Status : " + str, false);
        }

        void SMS_ThreadDone(object senderObj, EventArgs e)
        {
            SmsThreadCount--;

            Sender sender = (Sender)senderObj;
            using (var unitOfWork = container.Resolve<UnitOfWork>())
            {
                var repoSmsDetail = unitOfWork.RepositoryAsync<ShortMessageDetail>();

                var ids = sender.Entitities.Select(c => c.Id).ToList();

                var ent = repoSmsDetail.Query(c => ids.Contains(c.Id)).Select();

                foreach (var entity in ent)
                {
                    entity.IsSent = true;
                    entity.GatewayIdentificationNo = sender.GatewayIds;

                    repoSmsDetail.Update(entity);
                }

                unitOfWork.SaveChanges();
            }

            NotficationInvokeMethod(sender.Msg, false);

            if (!isSmsTimerStart)
            {                
                NotficationInvokeMethod("Currently running threads : " + SmsThreadCount, true);
            }
            if (SmsThreadCount <= 0 && !isSmsTimerStart)
            {
                this.btnSms.BeginInvoke((MethodInvoker)delegate() { this.btnSms.Enabled = true; });
                this.lblSms.BeginInvoke((MethodInvoker)delegate() { this.lblSms.Text = ""; });
                NotficationInvokeMethod("", true);
            }            
        }

        void DLR_ThreadDone(object senderObj, EventArgs e)
        {
            SmsThreadCount--;

            Sender sender = (Sender)senderObj;
            var dlrs = sender.DLRs;

            if (dlrs != null)
            {
                using (var unitOfWork = container.Resolve<UnitOfWork>())
                {
                    var repoSmsDetail = unitOfWork.RepositoryAsync<ShortMessageDetail>();

                    var gatewayIds = dlrs.Select(c => c.SmsId.ToString()).ToList();
                    var mobileNumbers = dlrs.Select(c => c.CellNumber).ToList();

                    var entities = repoSmsDetail
                        .Query(c => gatewayIds.Contains(c.GatewayIdentificationNo)
                            && mobileNumbers.Contains(c.MobileNumber))
                         .Select();

                    var dt = DateTime.Now;

                    foreach (var entity in entities)
                    {
                        var single = dlrs.Where(c => c.CellNumber == entity.MobileNumber && c.SmsId.ToString() == entity.GatewayIdentificationNo).FirstOrDefault();

                        if (single != null)
                        {
                            int st = Status(single.Status);

                            if (st != (int)ShortMessageStstus.Pending)
                            {
                                entity.IsStatusUpdated = true;
                                entity.ShortMessageStatusId = st;
                            }

                            entity.StatusUpdatedAt = dt;
                            //entity.Comments = single.ReasonCode.ToString();

                            repoSmsDetail.Update(entity);
                        }
                    }

                    unitOfWork.SaveChanges();
                }
            }
                        
            NotficationInvokeMethod(sender.Msg, false);

            if (!isSmsTimerStart)
            {                
                NotficationInvokeMethod("Currently running threads : " + SmsThreadCount, true);
            }
            if (SmsThreadCount <= 0 && !isSmsTimerStart)
            {
                this.btnSms.BeginInvoke((MethodInvoker)delegate() { this.btnSms.Enabled = true; });
                this.lblSms.BeginInvoke((MethodInvoker)delegate() { this.lblSms.Text = ""; });
                NotficationInvokeMethod("", true);
            }
        }

        int Status(int st)
        {
            if (st == 2) return (int)ShortMessageStstus.Undelivered;
            else if (st == 1) return (int)ShortMessageStstus.Delivered;
            return (int)ShortMessageStstus.Pending;
        }

        void NotficationInvokeMethod(string message, bool isCleanBeforeTextAppend)
        {
            this.tbSms.BeginInvoke((MethodInvoker)delegate()
            {
                if (isCleanBeforeTextAppend)
                {
                    this.tbSms.Text = "";
                }

                var lines = this.tbSms.Lines;
                int lc = lines.Count();
                if (lc > 20)
                {
                    var newLines = lines.Skip(lc - 20);
                    this.tbSms.Lines = newLines.ToArray();
                }
                this.tbSms.AppendText(Environment.NewLine + message);                
            });
        }
    }
}
