using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HalconDotNet;

namespace P1_CMMT.VisionTools.BParamTool
{
    public partial class BParamToolCtr : UserControl
    {
        BParamTool tool;
        public BParamToolCtr()
        {
            InitializeComponent();
        }


        public BParamToolCtr(BParamTool tool)
        {
            InitializeComponent();
            this.tool = tool;
        }

        private void BParamToolCtr_Load(object sender, EventArgs e)
        {
            tb_RecipeName.Text = tool.RecipeName;
            tb_ImageXNum.Text = tool.ImageXNum.ToString();
            tb_ImageYNum.Text = tool.ImageYNum.ToString();
            tb_FrameXNum.Text = tool.FrameXNum.ToString();
            tb_FrameYNum.Text = tool.FrameYNum.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
