using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;

namespace P1_CMMT
{
    public partial class IOFrm : Form
    {
        Dictionary<string, object> Dict = new Dictionary<string, object>();  //字典，用来存放工具，将它序列化成一个二进制文件

        HImage hImage = null;

        public IOFrm()
        {
            InitializeComponent();
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                switch (listBox1.SelectedItem.ToString())
                {
                    case "预填参数":
                        VisionTools.BParamTool.BParamTool bParamTool = (VisionTools.BParamTool.BParamTool)Dict["预填参数"];
                        VisionTools.BParamTool.BParamToolCtr bParamToolCtr = new VisionTools.BParamTool.BParamToolCtr(bParamTool);
                        panel3.Controls.Clear();
                        panel3.Controls.Add(bParamToolCtr);
                        bParamToolCtr.Dock = DockStyle.Fill;
                        break;
                    case "匹配工具":
                        VisionTools.MacthTool.MatchTool matchTool = (VisionTools.MacthTool.MatchTool)Dict["匹配工具"];
                        if (hImage != null)
                        {
                            matchTool.Image = hImage;
                        }
                        VisionTools.MacthTool.MatchToolCtr matchToolCtr = new VisionTools.MacthTool.MatchToolCtr(matchTool);
                        panel3.Controls.Clear();
                        panel3.Controls.Add(matchToolCtr);
                        matchToolCtr.Dock = DockStyle.Fill;

                        break;
                    default:
                        break;
                }
            }
            catch (Exception ee)
            {

            }
        }

        private void IOFrm_Load(object sender, EventArgs e)
        {
            VisionTools.BParamTool.BParamTool bParamTool = new VisionTools.BParamTool.BParamTool();
            Dict.Add("预填参数", bParamTool);

            VisionTools.MacthTool.MatchTool matchTool = new VisionTools.MacthTool.MatchTool();
            Dict.Add("匹配工具", matchTool);

        }

        private void btn_readImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog opd = new OpenFileDialog();
            if(opd.ShowDialog()==DialogResult.OK)
            {
                hImage = new HImage(opd.FileName);                
                hSmartWindowControl1.HalconWindow.DispImage(hImage);
                hSmartWindowControl1.HalconWindow.SetPart(0, 0, -2, -2);
            }
        }
    }
}
