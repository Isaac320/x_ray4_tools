using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace P1_CMMT
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                //处理未捕获的异常   
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                //处理UI线程异常   
                Application.ThreadException += Application_ThreadException;
                //处理非UI线程异常   
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Thread t = new Thread(ShowSplashFrm);
                t.Start();
                Thread.Sleep(100);


                Application.Run(new MainForm());
                foreach (Process process in System.Diagnostics.Process.GetProcesses())
                {
                    if (process.ProcessName.ToUpper().Contains("P1_CMMT"))
                        process.Kill();
                }
            }
            catch (Exception ee)
            {
                LogManager.WriteLog(ee.Message);
                LogManager.WriteLog(ee.StackTrace);
            }

        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var ee = e.ExceptionObject;
                if (ee != null)
                {
                    LogManager.WriteLog(e.ExceptionObject.GetType().ToString());                  
                }
            }
            catch (Exception ee)
            {
                LogManager.WriteLog(ee.Message);
                LogManager.WriteLog(ee.StackTrace);
            }
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            try
            {
                var ex = e.Exception;
                if (ex != null)
                {
                    LogManager.WriteLog(ex.Message);
                    LogManager.WriteLog(ex.StackTrace);
                }
            }
            catch (Exception ee)
            {
                LogManager.WriteLog(ee.Message);
                LogManager.WriteLog(ee.StackTrace);
            }
        }

        static void ShowSplashFrm()
        {
            SplashFrm splash = new SplashFrm();
            splash.ShowDialog();
        }
    }
}
