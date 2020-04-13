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
    public partial class UserLevelFrm : Form
    {
        public UserLevelFrm()
        {
            InitializeComponent();
            init();
            
        }


        private void init()
        {
            if (Global.myLevel == UserLevel.SuperAdmin)
            {
                this.Size = new Size(1090, 368);
            }
            else
            {
                this.Size = new Size(316, 368);
            }

            showList();

            comboBox1.SelectedIndex = 1;


        }


        private void showList()
        {
            List<string> list = DBPassword.ReadUsers();
            listBox1.Items.Clear();
            foreach (var item in list)
            {
                if (item != "s")
                {
                    listBox1.Items.Add(item);
                }
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            switch(DBPassword.Query(tb_username.Text,tb_password.Text))
            {
                case "1":
                    Global.myLevel = UserLevel.SuperAdmin;
                    break;
                case "2":
                    Global.myLevel = UserLevel.Admin;
                    break;
                default:
                    Global.myLevel = UserLevel.User;
                    break;
            }
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(tb_addUserName.Text.Trim()!=""&&tb_addPassword.Text.Trim()!="")
            {
                string level = "";
                switch(comboBox1.SelectedIndex)
                {
                    case 1:
                        level = "3";
                        break;
                    case 0:
                        level = "2";
                        break;
                    default:
                        level = "3";
                        break;
                }
                DBPassword.AddUser(tb_addUserName.Text, tb_addPassword.Text, level);
                showList();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedItem!=null)
            {
                string name = listBox1.SelectedItem.ToString();                
                DBPassword.DeleteUser(name);
                showList();
            }
        }
    }
}
