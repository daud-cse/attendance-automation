namespace deepp.WindowsService
{
    partial class Service
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.ServiceName = "Service1";
        }

        #endregion
    }
}
