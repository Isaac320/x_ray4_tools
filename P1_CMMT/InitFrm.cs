using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HalconDotNet;
using System.IO;

namespace P1_CMMT
{
    public partial class InitFrm : Form
    {
        public InitFrm()
        {
            InitializeComponent();
        }

        private void Confirm_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                //先测试这些值是否满足条件
                bool tempFlag = true;
                if (textBox1.Text.Trim() == "") tempFlag = false;
                if (textBox2.Text.Trim() == "") tempFlag = false;
                if (textBox3.Text.Trim() == "") tempFlag = false;
                if (textBox4.Text.Trim() == "") tempFlag = false;


                if (tempFlag)
                {
                    //测试是否有相应的receipt
                    string rpt = D2RManager.QueryReceipt(textBox2.Text);
                    if (rpt != null)
                    {
                        //加载receipt  读取regions 和 图像处理的dll,就图像处理类初始化一下就行
                        HOperatorSet.SetSystem("clip_region", "false");
                        string regionFilesPath = Global.RecipePath +"\\"+ rpt + @"\regions\";
                        string[] regionFiles = Directory.GetFiles(regionFilesPath, "*.hobj");

                        Global.imageRegions.Clear();
                        foreach(var name in regionFiles)
                        {
                            HObject reg;
                            HOperatorSet.GenEmptyObj(out reg);
                            reg.Dispose();
                            HOperatorSet.ReadRegion(out reg, name);
                            Global.imageRegions.Add(reg);
                        }


                        string processDllPath = Global.RecipePath +"\\"+ rpt;
                        string[] processDlls = Directory.GetFiles(processDllPath, "*.dll");

                        ImageProcess.init(processDlls[0]);   //加载dll


                        //都满足则就运行下面的。
                        Global.LotNum = textBox1.Text;
                        Global.Device = textBox2.Text;
                        Global.OperatorID = textBox3.Text;
                        Global.TotalFrame = int.Parse(textBox4.Text);
                        Global.RecipeName = rpt;

                        Global.ready2Go = true;    //准备就绪，开始按钮可以跑

                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("未查询到该Device对应的Receipt");
                    }
                }
                else
                {
                    MessageBox.Show("有空的");
                }

            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            
        }

        private void Cancel_Btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
