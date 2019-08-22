using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sfa.WindowsService
{
   public  class FileReadWrite
    {
        public static void ErrorLogging(Exception ex)
        {
            string strPath = @"E:\Shikkhaforall\ErrorLog\Log.txt";
            if (!File.Exists(strPath))
            {
                File.Create(strPath).Dispose();
            }
            using (StreamWriter sw = File.AppendText(strPath))
            {
                sw.WriteLine("=============Error Logging ===========");
                sw.WriteLine("===========Start============= " + DateTime.Now);
                sw.WriteLine("Error Message: " + ex.Message);
                sw.WriteLine("Stack Trace: " + ex.StackTrace);
                sw.WriteLine("===========End============= " + DateTime.Now);

            }
        }
        public static void SuccessLogging(string obj)
        {
            string strPath = @"E:\Shikkhaforall\ErrorLog\Log.txt";
            if (!File.Exists(strPath))
            {
                File.Create(strPath).Dispose();
            }
            using (StreamWriter sw = File.AppendText(strPath))
            {
                sw.WriteLine("=============Success Logging ===========");
                sw.WriteLine("Message: " + obj);


            }
        }

        public static void ReadError()
        {
            string strPath = @"E:\Shikkhaforall\ErrorLog\Log.txt";
            using (StreamReader sr = new StreamReader(strPath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }
    }
}
