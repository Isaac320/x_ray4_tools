using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace P1_CMMT
{
    class LogManager
    {
        public static object locker = new object();
        public static void WriteLog(string strLog)
        {
            string sFilePath = "D:\\testLog\\";
            string sFileName = "日志" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            sFileName = sFilePath + sFileName;
            if (!Directory.Exists(sFileName))
            {
                Directory.CreateDirectory(sFilePath);
            }
            StreamWriter sw = null;
            lock (locker)
            {
                using (sw = File.AppendText(sFileName))
                {
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss: " + strLog));
                }

            }


        }
    }
}
