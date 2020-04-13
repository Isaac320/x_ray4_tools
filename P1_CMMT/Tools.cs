using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Threading;

namespace P1_CMMT
{
    class Tools
    {

        private static object _lock = new object();

        /// <summary>
        /// 文件夹下所有内容copy
        /// </summary>
        /// <param name="SourcePath">要Copy的文件夹</param>
        /// <param name="DestinationPath">要复制到哪个地方</param>
        /// <param name="overwriteexisting">是否覆盖</param>
        /// <returns></returns>
        public static bool CopyDirectory(string SourcePath, string DestinationPath, bool overwriteexisting)
        {           
            bool ret = false;
            try
            {
                //开始复制图像
                Global.WorkProgressLabel = "复制图像";
                Global.WorkProgressNum = 0; //初始化这个数

                SourcePath = SourcePath.EndsWith(@"\") ? SourcePath : SourcePath + @"\";
                DestinationPath = DestinationPath.EndsWith(@"\") ? DestinationPath : DestinationPath + @"\";

                if (Directory.Exists(SourcePath))
                {
                    if (Directory.Exists(DestinationPath) == false)
                        Directory.CreateDirectory(DestinationPath);

                    int FilesNum = Directory.GetFiles(SourcePath).Length;

                    int tempI = 1;

                    foreach (string fls in Directory.GetFiles(SourcePath))
                    {
                        FileInfo flinfo = new FileInfo(fls);
                        flinfo.CopyTo(DestinationPath + flinfo.Name, overwriteexisting);

                        //下面加个这个来显示复制了多少了
                        Global.WorkProgressNum = tempI * 100 / FilesNum;
                        tempI++;
                    }
                    foreach (string drs in Directory.GetDirectories(SourcePath))
                    {
                        DirectoryInfo drinfo = new DirectoryInfo(drs);
                        if (CopyDirectory(drs, DestinationPath + drinfo.Name, overwriteexisting) == false)
                            ret = false;
                    }
                }
                ret = true;
            }
            catch (Exception ex)
            {
                ret = false;
            }
            return ret;
        }


        /// <summary>
        /// 删除目录下所有东西
        /// </summary>
        /// <param name="file">目录地址</param>
        public static void DeleteDir(string file)
        {
            try
            {
                //开始删除图像
                Global.WorkProgressLabel = "删除图像";
                Global.WorkProgressNum = 0; //初始化这个数

                //去除文件夹和子文件的只读属性
                //去除文件夹的只读属性
                System.IO.DirectoryInfo fileInfo = new DirectoryInfo(file);
                fileInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;

                //去除文件的只读属性
                System.IO.File.SetAttributes(file, System.IO.FileAttributes.Normal);

                //判断文件夹是否还存在
                if (Directory.Exists(file))
                {
                    int FilesNum = Directory.GetFiles(file).Length;
                    int tempI = 1;
                    
                    foreach (string f in Directory.GetFileSystemEntries(file))
                    {
                        if (File.Exists(f))
                        {
                            //如果有子文件删除文件
                            File.Delete(f);
                            Console.WriteLine(f);

                            //下面加个这个来显示删除制了多少了
                            Global.WorkProgressNum = tempI * 100 / FilesNum;
                            tempI++;
                        }
                        else
                        {
                            //循环递归删除子文件夹
                            DeleteDir(f);
                        }
                    }

                    //删除空文件夹
                    //Directory.Delete(file);
                    //Console.WriteLine(file);
                }

            }
            catch (Exception ex) // 异常处理
            {
               
            }
        }

        /// <summary>
        /// 写点墨文件的txt
        /// </summary>
        /// <param name="path">txt文件路径</param>
        /// <param name="txt">txt内容</param>
        public static void WriteTxt(string path,string txt)
        {           
            FileStream fs = new FileStream(path, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            sw.WriteLine(txt);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }


        /// <summary>
        /// 返回序号所在位置，序号从1开始,返回xy坐标
        /// </summary>
        /// <param name="Num">序号从1开始</param>
        /// <returns>返回行列坐标</returns>
        public static Point GetPosition(int Num)
        {
            Point p = new Point();
            p.X = ((Num-1) % Global.ImageXNum+1) + (Num-1) / (Global.ImageXNum * Global.FrameYNum) * 3;
            p.Y = ((Num - 1)%(Global.ImageXNum*Global.FrameYNum)) / Global.ImageXNum + 1;
            return p;
        }

        /// <summary>
        /// 写点墨文件
        /// </summary>
        /// <param name="list">输入的list</param>
        public static void WriteInkPointTxt(List<int[]> list)
        {
            if(!Directory.Exists(Global.InkPointPath))
            {
                Directory.CreateDirectory(Global.InkPointPath);
            }
            string path = Global.InkPointPath +"\\"+ Global.LotNum + "_" + Global.TrayID + ".txt";

            string s1 = "RECIPE:" + Global.RecipeName;
            string s2 = "TRAYID:" + Global.TrayID;
            string s3 = "TRAY:" + Global.TrayXNum + "," + Global.TrayYNum;
            string s4 = "FRAME:" + "1," + Global.FrameXNum + "," + Global.FrameYNum;

            WriteTxt(path, s1);
            WriteTxt(path, s2);
            WriteTxt(path, s3);
            WriteTxt(path, s4);

            int frameNum = 1;
            foreach(var t in list)
            {
                int length = t.Length;
                for (int i = 0; i < length; i++)
                {
                    if (t[i] != 1)
                    {
                        Point p = GetPosition(i + 1);
                        string s = frameNum + ",1," + p.X + "," + p.Y;
                        WriteTxt(path, s);
                    }
                }
                frameNum++;
            }
        }

        /// <summary>
        /// 写csv文件
        /// </summary>
        /// <param name="text"></param>
        /// <param name="path"></param>
        public static void WriteCSV(string text,string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filename = path +"\\"+ Global.LotNum + ".csv";
            //if (!File.Exists(filename))
            //{
            //    string tempss = "日期,时间,产品编号,产品名称,检测日期,标签类型,标签模板类型,机台,制造订单,检测流水号码,检测时间,检测结果,故障";
            //    File.AppendAllText(filename, tempss, Encoding.Default);
            //}
            lock (_lock)
            {
                try
                {

                    //File.AppendAllText(filename, "\n" + DateTime.Now.ToString("yyyy-MM-dd,HH:mm:ss:,") + text, Encoding.Default);
                    File.AppendAllText(filename, text+ "\n", Encoding.Default);
                }
                catch
                {
                    foreach (Process process in System.Diagnostics.Process.GetProcesses())
                    {
                        if (process.ProcessName.ToUpper().Equals("EXCEL"))
                            process.Kill();
                    }
                    GC.Collect();
                    Thread.Sleep(200);
                    File.AppendAllText(filename, text, Encoding.Default);
                }
            }

        }


        /// <summary>
        /// 写LotSummary
        /// </summary>
        public static void WriteLotSummary()
        {
            string[] s = new string[12];
            s[0] = "Lot Number," + Global.LotNum;
            s[1] = "Operator," + Global.OperatorID;
            s[2] = "Recipe," + Global.RecipeName;
            s[3] = "Start Time," + Global.startTime;
            s[4] = "End Time," + Global.endTime;
            s[5] = "Total Frames," + Global.TotalFrame;
            s[6] = "Total Pass,";
            s[7] = "Total Fail,";
            s[8] = "Total Marginal,";
            s[9] = "Total No Wire,";
            s[10] = "Yield,";
            s[11] = "Lot Completed," + Global.endLotTime;

            int length = s.Length;
            for(int i=0;i<length;i++)
            {
                WriteCSV(s[i],Global.LotSummaryPath);
            }            
        }
    }
   
}
