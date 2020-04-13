using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Drawing.Imaging;
using System.Reflection;
using HalconDotNet;

namespace P1_CMMT
{
    public partial class MainForm : Form
    {
        VideoFrm videofrm;
        SettingFrm settingfrm;
        ReportFrm reportfrm;

        ComPort port = new ComPort();
        IOFrm iofrm;

        Thread ImageProcessTh;  //图像处理总线程 


        SoftReg mySoftReg = new SoftReg();


        public MainForm()
        {
            InitializeComponent();
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (this.ActiveMdiChild == videofrm)
                {
                    Cursor.Current = Cursors.Arrow;
                    return;

                }
                videofrm.MdiParent = this;
                videofrm.Dock = DockStyle.Fill;
                videofrm.Show();
                videofrm.Activate();
                Cursor.Current = Cursors.Arrow;

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                //MyConfig.LoadData();
                videofrm = new VideoFrm();

                SplashFrm.num = 30;
                // Thread.Sleep(1000);

                Global.MNum = mySoftReg.GetMNum();

                SplashFrm.num = 60;




                settingfrm = new SettingFrm();
                iofrm = new IOFrm();

                reportfrm = new ReportFrm();




                SplashFrm.num = 99;
                Thread.Sleep(200);
                SplashFrm.num = 100;

                this.Visible = true;
                videofrm.MdiParent = this;
                videofrm.Dock = DockStyle.Fill;

                videofrm.Show();
                videofrm.Activate();
                this.WindowState = FormWindowState.Maximized;

                Init();  //主要处理线程开始跑起来

            }
            catch (Exception eee)
            {
                MessageBox.Show(eee.ToString());
            }
            Opacity = 100;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (this.ActiveMdiChild == settingfrm)
                {
                    Cursor.Current = Cursors.Arrow;
                    return;

                }
                settingfrm.MdiParent = this;
                settingfrm.Dock = DockStyle.Fill;
                settingfrm.Show();
                settingfrm.Activate();
                Cursor.Current = Cursors.Arrow;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }

        }
        protected override void OnClosing(CancelEventArgs e)
        {
            if (MessageBox.Show("退出本系统?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (this.ActiveMdiChild == iofrm)
                {
                    Cursor.Current = Cursors.Arrow;
                    return;

                }
                iofrm.MdiParent = this;
                iofrm.Dock = DockStyle.Fill;
                iofrm.Show();
                iofrm.Activate();
                Cursor.Current = Cursors.Arrow;

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }

        }


        /// <summary>
        /// 程序一启动需要跑的线程
        /// </summary>
        public void machineRun()
        {
            while (Global.BigFlag)
            {
                switch (Global.mMState)
                {
                    case MachineState.Run:
                        Run();  //处理方法
                        break;
                    case MachineState.Free:
                        Thread.Sleep(100);  //空跑
                        break;
                    default:
                        Thread.Sleep(100);  //空跑
                        break;
                }
            }
        }



        /// <summary>
        /// 一个总的图像处理逻辑
        /// </summary>
        public void Run()
        {
            //清楚上一次的所有结果
            Global.AllFrameReslut.Clear();

            Global.startTime = DateTime.Now;  //记录开始时间
            Global.lotGUID = Guid.NewGuid().ToString("N").ToUpper();  //得到一个新GUID            

            Global.FrameNum = 0; //frameNum清0

            while (Global.FrameNum < Global.TotalFrame)
            {


                //复制远程电脑上的图片文件夹到自己电脑的目录下
               // GetImages();

                //获取托盘号
                GetTrayID();

                List<int[]> TrayResult = new List<int[]>();  //用来记录该托盘结果数据

                //获取本次托盘有几个frame
                //frame总数减去当前检测过的frame数,除以每个托盘能容纳的frame数，得到temp1，余数是temp2
                //如果temp1>0，那这个num就是托盘所能容纳的数
                //等于0就是余数了
                int temp1 = (Global.TotalFrame - Global.FrameNum) / Global.FrameNumPerTray;
                int temp2 = (Global.TotalFrame - Global.FrameNum) % Global.FrameNumPerTray;
                int tempFrameNum;   //代表那个文件夹包含了几个frame
                if (temp1 > 0)
                {
                    tempFrameNum = Global.FrameNumPerTray;
                }
                else
                {
                    tempFrameNum = temp2;
                }

                //图片  名字命名1_1_1.jpg  第一个1是指frameNum  第二个是1， 第三个是序号  目前这个产品是2*3的 一张图六个，得做成通用的
                //循环各个frame 就是第一个序号
                for (int imageFrameNum = 1; imageFrameNum < tempFrameNum + 1; imageFrameNum++)
                {

                    //进度条作用,每次文件夹里处理图像的百分比
                    Global.WorkProgressLabel = "处理图像";
                    Global.WorkProgressNum = imageFrameNum * 100 / tempFrameNum;

                    //new一个n=0 用来存结果保存在int数组里 长度为每个图片数量乘以每个图片里的产品数量
                    int[] FrameResultArr = new int[Global.FrameImageNum * Global.ImageRegionNum];

                    //上面那个数组的序号
                    int numFrameResultArr = 0;

                    //循环各个frame下的每张图片 就是最后一个序号
                    for (int imageNum = 1; imageNum < Global.FrameImageNum + 1; imageNum++)
                    {
                        //得到图片名字
                        string ImageName = imageFrameNum.ToString() + "_" + "1" +"_"+ imageNum.ToString() + ".jpg";
                        string ImageFullName = Global.TempImagePath +"\\"+ ImageName;

                        if (File.Exists(ImageFullName))
                        {
                            //读取图像
                            HImage hImage = new HImage(ImageFullName);


                            
                            //循环读取region，并处理
                            foreach (HObject hRegion in Global.imageRegions)
                            {                                
                                
                                //处理图像，调用图像处理方法，获取返回结果,填入一个int数组里
                                int index;
                                string message;
                                HObject xld;
                                HOperatorSet.GenEmptyObj(out xld);
                                xld.Dispose();

                                //HObject tempRegion;
                                //HOperatorSet.GenEmptyObj(out tempRegion);
                                //tempRegion.Dispose();

                                //tempRegion = hRegion.Clone();

                                bool isOK=ImageProcess.run(hImage, hRegion, out xld, out index, out message);

                                
                                //显示图像
                                videofrm.showImage(hImage,1);

                                //显示region的框
                                if (hRegion != null)
                                {                                    
                                    videofrm.showImage(hRegion,1);
                                }                              

                                //在界面上展示xld
                                if (xld != null)
                                {
                                    videofrm.showImage(xld,1);
                                }
                                //在界面上展示NG信息
                                videofrm.showNGMeaage(message);

                                videofrm.showPostion((Global.FrameNum + 1).ToString() + "/" + Global.TotalFrame.ToString());
                              
                                //判断是否为NG
                                if (index != 1) 
                                {
                                    //显示NG  还没想好怎么显示

                                    //这里判是否要弹出一个对话框，人工判断
                                    if (Global.needLook)
                                    {
                                        //这里弹对话框，让人工选择
                                        using (CheckFrm checkFrm = new CheckFrm())
                                        {
                                            if(checkFrm.ShowDialog()==DialogResult.OK)
                                            {
                                                index = Global.needLookNum;   //这边index等于人工选择的num
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    //显示OK
                                }

                                //这边填写那个int数组
                                FrameResultArr[numFrameResultArr] = index;

                                numFrameResultArr++;

                                //判断是否暂停
                                if(Global.mySwitch1)
                                {
                                    Global.mySwitch2 = true;
                                    while(Global.mySwitch2&&Global.mySwitch1)
                                    {
                                        Thread.Sleep(100);
                                    }
                                }
                                //释放xld
                                if (xld != null)
                                {
                                    xld.Dispose();
                                }
                            }
                            //释放图像
                            hImage.Dispose();                            
                        }
                        else
                        {
                            foreach (HObject hRegion in Global.imageRegions)
                            {
                                numFrameResultArr++;
                            }
                        }
                    }

                    //把结果保存到托盘数据里
                    TrayResult.Add(FrameResultArr);

                    ////这里是一个fram处理完成,可以保存数据了  将那个填充好的数组加入到list里
                    //Global.AllFrameReslut.Add(FrameResultArr);

                    //FrameNum+1
                    Global.FrameNum++;


                }

                //这里是一个Tray处理完成,把数据保存到大list里
                Global.AllFrameReslut.Add(TrayResult);

                //这里是写每个托盘的结束处理，对AllFrameResult进行处理 EndTray方法  把trayresult结果给它，它显示下东西
                EndTray(TrayResult);

            }            
            //这里是整个lot处理完了  EndLot方法
            EndLot();

        }

        /// <summary>
        /// 从远程电脑上获取图片,要判断远程电脑上那个文件夹下是否有多个文件,必须是一个文件，否则报错不进行下面的工作。
        /// </summary>
        private void GetImages()
        {
            bool flag = true;
            while (flag)
            {
                if(!Directory.Exists(Global.XRayImagePath))
                {
                    Directory.CreateDirectory(Global.XRayImagePath);
                }
                DirectoryInfo xRayPath = new DirectoryInfo(Global.XRayImagePath);                
                DirectoryInfo[] info = xRayPath.GetDirectories();
                int tempLength = info.Length;
                if (tempLength != 1)
                {
                    MessageBox.Show("远程电脑文件夹数量为:" + tempLength.ToString());
                }
                else
                {
                    //删除本地目录
                    videofrm.listBoxShowMessage("开始删除本地临时图片目录..");
                    Tools.DeleteDir(Global.TempImagePath);
                    videofrm.listBoxShowMessage("删除完毕");

                    videofrm.listBoxShowMessage("开始从远程电脑复制图片...");
                    Tools.CopyDirectory(info[0].FullName, Global.TempImagePath, true);   //复制文件夹到本地电脑指定目录下
                    flag = false;
                    videofrm.listBoxShowMessage("复制完毕");

                    //删除远程电脑目录下的文件
                    videofrm.listBoxShowMessage("开始删除远程电脑图片...");
                    Tools.DeleteDir(Global.XRayImagePath);
                    videofrm.listBoxShowMessage("删除完毕");
                }
            }
        }


        /// <summary>
        /// 一个托盘跑完了,弹个框让处理并继续，如果没处理，就等处理
        /// </summary>
        private void EndTray(List<int[]> tempResult)
        {
            videofrm.listBoxShowMessage("托盘完成");

            ////写点墨文件
            Tools.WriteInkPointTxt(tempResult);  //写文件


            //弹框
            using (EndTrayFrm endTrayFrm = new EndTrayFrm(tempResult))
            {
                if(endTrayFrm.ShowDialog()==DialogResult.OK)
                {
                    //啥都不干了
                }
            }

        }

        /// <summary>
        /// 一个lot跑完了 记录数据啥的
        /// </summary>
        private void EndLot()
        {
            videofrm.listBoxShowMessage("Lot完成");


            //写Lot Summary
            Tools.WriteLotSummary();
            videofrm.listBoxShowMessage("已写入Lot Summary");

            Global.mMState = MachineState.Free;   //工作停止

            Global.ready2Go = false;  //开始按钮不再有用

            Global.endTime = DateTime.Now;
            //弹出个对话框 显示一些信息

            using (EndLotFrm endLotFrm = new EndLotFrm())
            {
                if(endLotFrm.ShowDialog()==DialogResult.OK)
                {
                    //啥都不干
                }
            }

            //记录时间
            Global.endLotTime = DateTime.Now;

            //下面的100代表产品数量，99pass数量 1fail数等  需要修正
            string s1 = Global.TotalFrame + "," + 100 + "," + "99" + "," + 1 + "," + 0 + "," + "1" + "," + 99;

            Global.attrib = s1;

            //这边记录下frame里排列组合啥样，需要显示根据这个来
            string s2 = Global.ImageXNum + "," + Global.ImageYNum + "," + Global.FrameXNum + "," + Global.FrameYNum;

            Global.runAttrib = s2;
                        
            //保存INST数据
            Model_INST model1 = new Model_INST();
            model1.lotGUID = Global.lotGUID;
            model1.lotNo = Global.LotNum;
            model1.startTime = Global.startTime;
            model1.endTime = Global.endTime;
            model1.endLotTime = Global.endLotTime;
            model1.operatorID = Global.OperatorID;
            model1.attrib = Global.attrib;
            model1.runAttrib = Global.runAttrib;
            model1.reportType = Global.reportType;
            DataBaseTools.InsertModel_INST(model1);


            Global.frameIndex = 0;
            //保存frame数据
            foreach (var resultList in Global.AllFrameReslut)
            {
                Global.frameIndex++;
                Global.trayIndex = Global.frameIndex;  //反正就是一个list一个index
                foreach (var result in resultList)
                {
                    StringBuilder ss = new StringBuilder();
                    foreach (var t in result)
                    {
                        ss.Append(t + ",");
                    }
                    string frameResult = ss.ToString();
                    Model_FRAME model2 = new Model_FRAME();
                    model2.lotGUID = Global.lotGUID;
                    model2.trayIndex = Global.trayIndex;
                    model2.frameIndex = Global.frameIndex;
                    model2.unitContent = frameResult;
                    DataBaseTools.InsertModel_FRAME(model2);
                }

            }

            videofrm.listBoxShowMessage("数据已写入数据库");

        }

        /// <summary>
        /// 获取托盘号
        /// </summary>
        private void GetTrayID()
        {
            //需要弹窗，让人填写trayID
            using (TrayIDFrm trayIDFrm = new TrayIDFrm())
            {
                if (trayIDFrm.ShowDialog() == DialogResult.OK)
                {
                    //啥都不干了，继续跑呗
                }


            }

        }


       private void Init()
       {
            ImageProcessTh = new Thread(machineRun);
            ImageProcessTh.IsBackground = true;
            ImageProcessTh.Start();

       }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (this.ActiveMdiChild == reportfrm)
                {
                    Cursor.Current = Cursors.Arrow;
                    return;

                }
                reportfrm.MdiParent = this;
                reportfrm.Dock = DockStyle.Fill;
                reportfrm.Show();
                reportfrm.Activate();
                Cursor.Current = Cursors.Arrow;

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            StateLb_WorkName.Text = Global.WorkProgressLabel;
            toolStripProgressBar1.Value = Global.WorkProgressNum;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            using (UserLevelFrm myLevelFrm = new UserLevelFrm())
            {
                if(myLevelFrm.ShowDialog()==DialogResult.OK)
                {

                }
            }
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutFrm aboutFrm = new AboutFrm())
            {
                aboutFrm.ShowDialog();
            }
        }
    }


}

