using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HalconDotNet;

namespace P1_CMMT
{
    public partial class VideoFrm : Form
    {
        MainForm mainFrm;

        delegate void delegateShowMessage(string s);
        public VideoFrm()
        {
            InitializeComponent();
        }

        public VideoFrm(MainForm mainFrm)
        {
            this.mainFrm = mainFrm;
            InitializeComponent();
            Init();  //初始化一些东西
        }

        private void Init()
        {
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            using (InitFrm initfrm = new InitFrm())
            {
                if (initfrm.ShowDialog() == DialogResult.OK)
                {
                    showList();

                }
            }
        }


        public void listBoxShowMessage(string s)
        {
            if (listBox1.InvokeRequired)
            {
                BeginInvoke(new delegateShowMessage(listBoxShowMessage), new object[] { s });

            }
            else
            {
                string mystring = DateTime.Now.ToString("HH:mm:ss") + " " + s;
                listBox1.Items.Add(mystring);

                //写log
                LogManager.WriteLog(s);

                if (listBox1.Items.Count > 200)
                {
                    for (int i = 80; i > -1; i--)
                    {
                        listBox1.Items.RemoveAt(i);
                    }
                }
                listBox1.TopIndex = listBox1.Items.Count - 1;
            }
        }


        public void showList()
        {
            if(listView1.InvokeRequired)
            {
                listView1.Invoke(new Action(showList), new object[] {  });
            }
            else
            {
                listView1.Items.Clear();
                this.listView1.BeginUpdate();
                ListViewItem lvi = new ListViewItem();
                lvi.ImageIndex = 0;
                lvi.Text = "Lot Num";
                lvi.SubItems.Add(Global.LotNum);
                this.listView1.Items.Add(lvi);

                lvi = new ListViewItem();
                lvi.ImageIndex = 1;
                lvi.Text = "Device";
                lvi.SubItems.Add(Global.Device);
                this.listView1.Items.Add(lvi);

                lvi = new ListViewItem();
                lvi.ImageIndex = 2;
                lvi.Text = "Operator ID";
                lvi.SubItems.Add(Global.OperatorID);
                this.listView1.Items.Add(lvi);

                lvi = new ListViewItem();
                lvi.ImageIndex = 3;
                lvi.Text = "Total Frame";
                lvi.SubItems.Add(Global.TotalFrame.ToString());
                this.listView1.Items.Add(lvi);
                this.listView1.EndUpdate();

                lvi = new ListViewItem();
                lvi.ImageIndex = 4;
                lvi.Text = "Recipe Name";
                lvi.SubItems.Add(Global.RecipeName.ToString());
                this.listView1.Items.Add(lvi);
                this.listView1.EndUpdate();

            }
        }

        /// <summary>
        /// 显示图像
        /// </summary>
        /// <param name="obj">图像</param>
        /// <param name="index">颜色代号</param>
        public void showImage(HObject obj,int index)
        {
            HOperatorSet.SetDraw(hSmartWindowControl1.HalconWindow, "margin");
            HOperatorSet.SetLineWidth(hSmartWindowControl1.HalconWindow, 2);
            if (index == 1)
            {
                HOperatorSet.SetColor(hSmartWindowControl1.HalconWindow, "red");
            }
            else
            {
                HOperatorSet.SetColor(hSmartWindowControl1.HalconWindow, "red");
            }
            hSmartWindowControl1.HalconWindow.DispObj(obj);
        }


        public void clearWindow()
        {
            hSmartWindowControl1.HalconWindow.ClearWindow();
        }


        public void showNGMeaage(string s)
        {
            if (label3.InvokeRequired)
            {
                BeginInvoke(new delegateShowMessage(showNGMeaage), new object[] { s });

            }
            else
            {
                label3.Text = s;
            }

        }

        public void showPostion(string s)
        {
            if (lb_pos.InvokeRequired)
            {
                BeginInvoke(new delegateShowMessage(showPostion), new object[] { s });

            }
            else
            {
                lb_pos.Text = s;
            }

        }

        private void bt_go_Click(object sender, EventArgs e)
        {
            //检查是否已经初始化，要不要弹出初始化窗口
            if(!Global.ready2Go)
            {
                //弹出用来初始化的框
                using (InitFrm initfrm = new InitFrm())
                {
                    if (initfrm.ShowDialog() == DialogResult.OK)
                    {
                        showList();
                    }
                }
            }

            if(Global.ready2Go)
            {
                if(Global.mySwitch1)
                {
                    Global.mySwitch1 = false;
                    listBoxShowMessage("继续工作");
                }

                if (Global.mMState == MachineState.Free)
                {
                    listBoxShowMessage("开始工作");
                    Global.mMState = MachineState.Run;
                }                
            }
            
        }

        private void bt_pause_Click(object sender, EventArgs e)
        {
            if (Global.ready2Go)
            {
                Global.mySwitch1 = true;
                listBoxShowMessage("暂停");
            }
        }

        private void bt_step_Click(object sender, EventArgs e)
        {
            if (Global.ready2Go)
            {
                Global.mySwitch2 = false;
                listBoxShowMessage("单步");
            }
        }

        private void bt_stop_Click(object sender, EventArgs e)
        {
           
        }
    }
}
