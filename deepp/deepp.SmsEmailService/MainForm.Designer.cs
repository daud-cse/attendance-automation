namespace deepp.SmsEmailService
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disdeepping">true if managed resources should be disdeepped; otherwise, false.</param>
        protected override void Dispose(bool disdeepping)
        {
            if (disdeepping && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disdeepping);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.notifyIconMain = new System.Windows.Forms.NotifyIcon(this.components);
            this.tbSms = new System.Windows.Forms.TextBox();
            this.tbEmail = new System.Windows.Forms.TextBox();
            this.btnSms = new System.Windows.Forms.Button();
            this.btnEmail = new System.Windows.Forms.Button();
            this.lblSms = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tmrSms = new System.Windows.Forms.Timer(this.components);
            this.tmrEmail = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // notifyIconMain
            // 
            this.notifyIconMain.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.notifyIconMain.BalloonTipText = "SMS & Email Service";
            this.notifyIconMain.BalloonTipTitle = "Service is running";
            this.notifyIconMain.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconMain.Icon")));
            this.notifyIconMain.Text = "SMS Service";
            this.notifyIconMain.Visible = true;
            this.notifyIconMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIconMain_MouseDoubleClick);
            // 
            // tbSms
            // 
            this.tbSms.BackColor = System.Drawing.SystemColors.WindowText;
            this.tbSms.ForeColor = System.Drawing.Color.GreenYellow;
            this.tbSms.Location = new System.Drawing.Point(12, 80);
            this.tbSms.Multiline = true;
            this.tbSms.Name = "tbSms";
            this.tbSms.ReadOnly = true;
            this.tbSms.Size = new System.Drawing.Size(368, 260);
            this.tbSms.TabIndex = 0;
            this.tbSms.Text = "SMS Service Notification";
            // 
            // tbEmail
            // 
            this.tbEmail.BackColor = System.Drawing.SystemColors.WindowText;
            this.tbEmail.ForeColor = System.Drawing.Color.GreenYellow;
            this.tbEmail.Location = new System.Drawing.Point(433, 80);
            this.tbEmail.Multiline = true;
            this.tbEmail.Name = "tbEmail";
            this.tbEmail.ReadOnly = true;
            this.tbEmail.Size = new System.Drawing.Size(376, 260);
            this.tbEmail.TabIndex = 1;
            this.tbEmail.Text = "Email Service Notification";
            // 
            // btnSms
            // 
            this.btnSms.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSms.Location = new System.Drawing.Point(13, 42);
            this.btnSms.Name = "btnSms";
            this.btnSms.Size = new System.Drawing.Size(75, 32);
            this.btnSms.TabIndex = 2;
            this.btnSms.Text = "Start";
            this.btnSms.UseVisualStyleBackColor = true;
            this.btnSms.Click += new System.EventHandler(this.btnSms_Click);
            // 
            // btnEmail
            // 
            this.btnEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmail.Location = new System.Drawing.Point(433, 41);
            this.btnEmail.Name = "btnEmail";
            this.btnEmail.Size = new System.Drawing.Size(75, 33);
            this.btnEmail.TabIndex = 3;
            this.btnEmail.Text = "Start";
            this.btnEmail.UseVisualStyleBackColor = true;
            this.btnEmail.Click += new System.EventHandler(this.btnEmail_Click);
            // 
            // lblSms
            // 
            this.lblSms.AutoSize = true;
            this.lblSms.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSms.ForeColor = System.Drawing.Color.Blue;
            this.lblSms.Location = new System.Drawing.Point(12, 344);
            this.lblSms.Name = "lblSms";
            this.lblSms.Size = new System.Drawing.Size(157, 17);
            this.lblSms.TabIndex = 4;
            this.lblSms.Text = "SMS Service is stopped";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.ForeColor = System.Drawing.Color.Blue;
            this.lblEmail.Location = new System.Drawing.Point(439, 343);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(160, 17);
            this.lblEmail.TabIndex = 5;
            this.lblEmail.Text = "Email service is stopped";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "SMS Service";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Maroon;
            this.label2.Location = new System.Drawing.Point(429, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Email Service";
            // 
            // tmrSms
            // 
            this.tmrSms.Interval = 5000;
            this.tmrSms.Tick += new System.EventHandler(this.tmrSms_Tick);
            // 
            // tmrEmail
            // 
            this.tmrEmail.Interval = 5000;
            this.tmrEmail.Tick += new System.EventHandler(this.tmrEmail_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 370);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblSms);
            this.Controls.Add(this.btnEmail);
            this.Controls.Add(this.btnSms);
            this.Controls.Add(this.tbEmail);
            this.Controls.Add(this.tbSms);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "SMS & Email Service";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIconMain;
        private System.Windows.Forms.TextBox tbSms;
        private System.Windows.Forms.TextBox tbEmail;
        private System.Windows.Forms.Button btnSms;
        private System.Windows.Forms.Button btnEmail;
        private System.Windows.Forms.Label lblSms;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer tmrSms;
        private System.Windows.Forms.Timer tmrEmail;
    }
}