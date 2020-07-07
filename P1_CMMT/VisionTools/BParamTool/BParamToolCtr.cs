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

       
    }
}
