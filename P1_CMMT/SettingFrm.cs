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
    public partial class SettingFrm : Form
    {
        public SettingFrm()
        {
            InitializeComponent();
            init();
        }


        private void init()
        {
            lb_ConfigPath.Text = Global.ConfigPath;
            lb_SaveImagePath.Text = Global.SaveImagePath;
            lb_TempImagePath.Text = Global.TempImagePath;
            lb_XRayImagePath.Text = Global.XRayImagePath;
            lb_ReceiptPath.Text = Global.RecipePath;
            lb_inkPointPath.Text = Global.InkPointPath;
            lb_lotSummaryPath.Text = Global.LotSummaryPath;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.Description = "选择数据保存路径";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    lb_ConfigPath.Text = fbd.SelectedPath;
                    Global.ConfigPath = fbd.SelectedPath;
                }
            }
            catch (Exception eee)
            {
                MessageBox.Show(eee.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.Description = "选择数据保存路径";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    lb_TempImagePath.Text = fbd.SelectedPath;
                    Global.TempImagePath = fbd.SelectedPath;
                }
            }
            catch (Exception eee)
            {
                MessageBox.Show(eee.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.Description = "选择数据保存路径";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    lb_SaveImagePath.Text = fbd.SelectedPath;
                    Global.SaveImagePath = fbd.SelectedPath;
                }
            }
            catch (Exception eee)
            {
                MessageBox.Show(eee.ToString());
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.Description = "选择数据保存路径";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    lb_XRayImagePath.Text = fbd.SelectedPath;
                    Global.XRayImagePath = fbd.SelectedPath;
                }
            }
            catch (Exception eee)
            {
                MessageBox.Show(eee.ToString());
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.Description = "选择数据保存路径";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    lb_XRayImagePath.Text = fbd.SelectedPath;
                    Global.RecipePath = fbd.SelectedPath;
                }
            }
            catch (Exception eee)
            {
                MessageBox.Show(eee.ToString());
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                Global.needLook = true;
            }
            else
            {
                Global.needLook = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.Description = "选择数据保存路径";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    lb_XRayImagePath.Text = fbd.SelectedPath;
                    Global.InkPointPath = fbd.SelectedPath;
                }
            }
            catch (Exception eee)
            {
                MessageBox.Show(eee.ToString());
            }
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.Description = "选择数据保存路径";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    lb_lotSummaryPath.Text = fbd.SelectedPath;
                    Global.LotSummaryPath = fbd.SelectedPath;
                }
            }
            catch (Exception eee)
            {
                MessageBox.Show(eee.ToString());
            }
        }
    }
}
