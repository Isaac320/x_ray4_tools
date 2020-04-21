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
        Frame_Info myFInfo;
        DataTable mTable = new DataTable();

        

        string myGuid;

        public DataShowCtr(TabControl tc,TabPage page,string myGuid)
        {
            InitializeComponent();
            this.tc = tc;
            this.page = page;
            this.myGuid = myGuid;
        }

        private void bt_Close_Click(object sender, EventArgs e)
        {
            tc.Controls.Remove(page);
        }



        private void init()   //初始化产品排列信息
        {
            myFInfo.ImageXNum = 3;
            myFInfo.ImageYNum = 2;
            myFInfo.FrameXNum = 6;
            myFInfo.FrameYNum = 42;

            string sql = "SELECT DISTINCT trayIndex FROM REP_FRAME WHERE lotGUID='"+myGuid+"'";
            DataTable mydt = DataBaseTools.Query(sql);

            int length = mydt.Rows.Count;
            for(int i=0;i<length;i++)
            {
                comboBox1.Items.Add(mydt.Rows[i][0].ToString());
            }
            comboBox1.SelectedIndex = 0;      
            
            string sql2= "SELECT lotNo FROM REP_INST WHERE lotGUID = '"+myGuid+"'";
            DataTable mydt2 = DataBaseTools.Query(sql2);
            lb_LOT.Text = "LOT: " + mydt2.Rows[0][0].ToString();

            string sql3 = "SELECT operatorID FROM REP_INST WHERE lotGUID = '" + myGuid + "'";
            DataTable mydt3 = DataBaseTools.Query(sql3);
            lb_Operator.Text = "Operator: " + mydt3.Rows[0][0].ToString();

        }



        public static Point GetPosition(int Num, Frame_Info f)
        {
            Point p = new Point();
            p.X = ((Num - 1) % f.ImageXNum + 1) + (Num - 1) / (f.ImageXNum * f.FrameYNum) * 3;
            p.Y = ((Num - 1) % (f.ImageXNum * f.FrameYNum)) / f.ImageXNum + 1;
            return p;
        }

        private void DataShowCtr_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 50; i++)
            {
                mTable.Columns.Add(((i % 10) * 10 + i / 10).ToString());
            }
            init();            
            dataGridView1.DataSource = mTable;

            dataGridView1.Columns[0].Width = 90;
            for (int i = 1; i < 50; i++)
            {
                dataGridView1.Columns[i].Width = 20;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            mTable.Clear();

            

            string trayIndex=comboBox1.SelectedItem.ToString();
            string sql = "SELECT frameIndex,unitContent from REP_FRAME WHERE lotGUID='" + myGuid + "' AND trayIndex='" + trayIndex + "'";
            DataTable mydt = DataBaseTools.Query(sql);
            //textBox1.Text = mydt.Rows.Count.ToString() + "," + mydt.Rows[3][0].ToString();
            int length = mydt.Rows.Count;

            for (int i = 0; i < length* (myFInfo.FrameXNum+1); i++)
            {
                mTable.Rows.Add("");
            }

            for (int i=0;i<length;i++)
            {

                int startP = i * (myFInfo.FrameXNum + 1);
                string ss = mydt.Rows[i][0].ToString();

                mTable.Rows[startP][0]="Frame"+ss;

                string ss2 = mydt.Rows[i][1].ToString();              

                int[] tempI = Array.ConvertAll(ss2.TrimEnd(',').Split(','), int.Parse);

                for (int j = 0; j < tempI.Length; j++)
                {
                    Point p = GetPosition((j + 1), myFInfo);

                    mTable.Rows[p.X - 1+startP][p.Y] = tempI[j];
                }
            }
            

            


        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            int rowL = dataGridView1.Rows.Count;
            int colL = dataGridView1.ColumnCount;

            for(int i=0;i<rowL;i++)
            {
                for(int j=0;j<colL;j++)
                {
                    if(dataGridView1.Rows[i].Cells[j].Value!=null)
                    {
                        switch(dataGridView1.Rows[i].Cells[j].Value.ToString())
                        {
                            case "0":
                                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Yellow;
                                break;
                            case "1":
                                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Lime;
                                break;
                            case "2":
                                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Red;
                                break;
                            case "3":
                                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Red;
                                break;
                            case "4":
                                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Red;
                                break;
                            case "5":
                                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Red;
                                break;
                            case "6":
                                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Red;
                                break;
                            default:
                                break;

                        }
                    }
                }
            }
        }
    }
}
