using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace P1_CMMT
{
    public partial class EndTrayFrm : Form
    {
        
        public EndTrayFrm()
        {
            InitializeComponent();
        }

        public EndTrayFrm(List<int[]> tempList)
        {
            InitializeComponent();
            init(tempList);
            
        }


        public void init(List<int[]> tempList)
        {
            lb_lot.Text= "Lot : " + Global.LotNum;
            lb_InFrame.Text = "Inspected Frames：" + Global.FrameNum.ToString() + "/" + Global.TotalFrame.ToString();

            int length = tempList.Count;

            int[] tempInt = new int[length];
            int sum = 0;
            for(int i=0;i<length;i++)
            { 
                tempInt[i] = GetNGNum(tempList[i]);
                sum = sum + tempInt[i];
            }

            lb_reject.Text = "Found reject units: " + sum.ToString();

            for (int i = 0; i < length; i++)
            {
                listBox1.Items.Add("Frame[" + (i + 1).ToString() + "]: " + tempInt[i].ToString());
            }

        }

        /// <summary>
        /// 获得每个数组里的NG数
        /// </summary>
        /// <param name="a">输入数组</param>
        /// <returns></returns>
        private int GetNGNum(int[] a)
        {
            int sum = 0;
            for(int i=0;i<a.Length;i++)
            {
                if(a[i]!=1)
                {
                    sum = sum + 1;
                }
            }
            return sum;
        }


        /// <summary>
        /// 检查激光那边文件是否存在，即有没有经过激光处理
        /// </summary>
        /// <returns></returns>
        public bool CheckLaserOK()
        {
            if (!Directory.Exists(Global.InkPointPath))
            {
                Directory.CreateDirectory(Global.InkPointPath);
            }
            DirectoryInfo inkPath = new DirectoryInfo(Global.InkPointPath);
            FileInfo[] info = inkPath.GetFiles("*.txt");
            int tempLength = info.Length;
            if (tempLength != 0)
            {
                //MessageBox.Show("点墨文件数量为:" + tempLength.ToString());
                return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!CheckLaserOK())
            {
                lb_noProcess.Visible = true;
            }
            else
            {
                DialogResult = DialogResult.OK;
            }
        }
        
    }
}
