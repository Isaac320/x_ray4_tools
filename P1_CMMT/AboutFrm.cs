using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace P1_CMMT
{
    public partial class AboutFrm : Form
    {
        public AboutFrm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AboutFrm_Load(object sender, EventArgs e)
        {
            if (Global.IsRegister == true)
            {
                label3.Text = "(已注册版本)";
                label4.Visible = false;
                label6.Visible = false;
                button2.Visible = false;
            }
            else
            {
                label6.Text = Global.TimeLeft.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (RegFrm myReg = new RegFrm())
            {
                myReg.ShowDialog();
            }
        }
    }
}
