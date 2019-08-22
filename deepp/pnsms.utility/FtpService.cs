using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pnsms.utility
{
    public class FtpFileInfo
    {
        public string fileName;
        public string creationDate;
        public string size;
    }
    public class FtpDirectoryInfo
    {
        public string directoryName;
        public string creationDate;
    }
    public class FtpService
    {
        public string FtpHost { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Port { get; set; }
        public string CurrentDirectory { get; set; }
        public string ServerMessage { get; set; }
        public FtpDirectoryInfo[] Directoreies { get; set; }
        public FtpFileInfo[] FileName { get; set; }

        public bool IsChangeDirectorySuccess { get; set; }
        public bool IsLoginSuccess { get; set; }

        bool IsWeb;

        public FtpService(bool isWeb)
        {
            IsWeb = isWeb;
        }

        public bool UploadFile()
        {
            return true;
        }
    }
}
