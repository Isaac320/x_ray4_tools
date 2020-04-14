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
    public partial class ReportFrm : Form
    {
        public ReportFrm()
        {
            InitializeComponent();
            DataTable mydt = DataBaseTools.Query(mySql());
            dataTableToListview(listView1, mydt);
        }

        private void bt_Query_Click(object sender, EventArgs e)
        {
            DataTable mydt = DataBaseTools.Query(mySql());
            dataTableToListview(listView1, mydt);
        }

        private string mySql()
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM REP_INST WHERE 1=1 ");
            if (checkBox1.Checked)
            {
                sql.AppendLine("AND startTime > '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "'");

            }
            if(checkBox2.Checked)
            {
                sql.AppendLine("AND startTime < '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'");
            }
            if(tb_opID.Text.Trim()!="")
            {
                sql.AppendLine("AND operatorID='" + tb_opID.Text.Trim()+"'");
            }
            if(tb_lot.Text.Trim()!="")
            {
                sql.AppendLine("AND lotNo='" + tb_lot.Text.Trim() + "'");
            }                
            return sql.ToString();
        }

        static public void dataTableToListview(ListView lv, DataTable dt)
        {
            if (dt != null)
            {
                lv.Items.Clear();
                lv.Columns.Clear();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    lv.Columns.Add(dt.Columns[i].Caption.ToString());
                }
                foreach (DataRow dr in dt.Rows)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.SubItems[0].Text = dr[0].ToString();
                    for (int i = 1; i < dt.Columns.Count; i++)
                    {
                        lvi.SubItems.Add(dr[i].ToString());
                    }
                    lv.Items.Add(lvi);
                }
                lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            TabPage page = new TabPage("new");
            DataShowCtr mydataCtr = new DataShowCtr(tabControl1,page);
            page.Controls.Add(mydataCtr);
            mydataCtr.Dock = DockStyle.Fill;
            tabControl1.Controls.Add(page);
            tabControl1.SelectedTab = page;
            
        }
    }
}
