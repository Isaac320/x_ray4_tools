using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P1_CMMT
{
    public partial class SplashFrm : Form
    {
        public SplashFrm()
        {
            InitializeComponent();
        }


        public static int num = 0;
        public static string info = "载入中...";      

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (num >= 0 && num < 100)
            {
                progressBar1.Value = num;
                label1.Text = info;
            }
            else
            {
                this.Close();
            }

        }

       
    }
}
