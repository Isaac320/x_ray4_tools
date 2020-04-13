using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcess;
using System.Reflection;
using HalconDotNet;
using System.Windows.Forms;
using System.Threading;

namespace P1_CMMT
{
    public class ImageProcess
    {
        static Assembly ass;   //用来加载dll

        string filename = null;

        static IImageProcess imageProcess;  //图像处理类
        public static bool init(string filename)
        {
            try
            {
                ass = Assembly.LoadFrom(filename);
                foreach (var t in ass.GetTypes())
                {
                    if (t.GetInterface("IImageProcess") != null)
                    {
                        imageProcess = (IImageProcess)Activator.CreateInstance(t);
                    }
                }
                imageProcess.Init();
            }
            catch
            {
                MessageBox.Show("加载配方初始化失败");
                return false;
            }
            return true;
        }
        

        public static bool run(HImage himage,HObject region,out HObject xld,out int index,out string message)
        {
           // Thread.Sleep(30);  //算法运行时间
            xld = null;
            index = 999;
            message = "图像处理失败";
            return true;
        }


    }
}
