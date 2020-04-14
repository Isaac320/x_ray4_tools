using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace P1_CMMT
{
    public partial class DataShowCtr : UserControl
    {
        TabControl tc;
        TabPage page;
        public DataShowCtr(TabControl tc,TabPage page)
        {
            InitializeComponent();
            this.tc = tc;
            this.page = page;
        }

        private void bt_Close_Click(object sender, EventArgs e)
        {
            tc.Controls.Remove(page);
        }
    }
}
