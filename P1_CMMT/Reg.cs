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
    public partial class Reg : Form
    {
        TestMyXXX tt = new TestMyXXX();
        public Reg()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            tt.save(textBox2.Text.ToString());
            int days = tt.TimeLeft();
            if (days > 100)
            {
                Global.IsRegister = true;
                Global.TimeLeft = days;
                MessageBox.Show("注册成功");
                this.Close();
            }
            else
            {
                MessageBox.Show("注册码错误");
            }
            Cursor.Current = Cursors.Arrow;

        }

        private void Reg_Load(object sender, EventArgs e)
        {
            textBox1.Text = Global.MNum;
            textBox2.Text = "";
            
        }

        private void Reg_Activated(object sender, EventArgs e)
        {
            textBox2.Focus();
        }
    }
}
