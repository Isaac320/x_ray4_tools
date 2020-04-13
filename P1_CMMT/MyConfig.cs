using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P1_CMMT
{
    class MyConfig
    {
        static IniFile myini = new IniFile(Global.ConfigPath + "\\Config.ini");

        public static void SaveData()
        {
            try
            {
                //myini.IniWriteValue("Threshold", "mean1", Global.Threshold1.ToString());
                //myini.IniWriteValue("Threshold", "mean2", Global.Threshold2.ToString());
                //myini.IniWriteValue("OFFSET", "height", Global.Offset.ToString());

                myini.IniWriteValue("Path", "configPath", Global.ConfigPath);
                myini.IniWriteValue("Path", "tempImagePath", Global.TempImagePath);
                myini.IniWriteValue("Path", "saveImagePath", Global.SaveImagePath);
                myini.IniWriteValue("Path", "xRayImagePath", Global.XRayImagePath);
                myini.IniWriteValue("Path", "receiptPath", Global.RecipePath);
                myini.IniWriteValue("Path", "inkPointPath", Global.InkPointPath);
                myini.IniWriteValue("Path", "lotSummaryPath", Global.LotSummaryPath);

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());

                LogManager.WriteLog("保存参数错误" + ee.ToString());
            }
        }


        public static void LoadData()
        {
            try
            {
                //Global.Threshold1 = double.Parse(myini.IniReadValue("Threshold", "mean1"));
                //Global.Threshold2 = int.Parse(myini.IniReadValue("Threshold", "mean2"));
                //Global.Offset = short.Parse(myini.IniReadValue("OFFSET", "height"));

                Global.ConfigPath = myini.IniReadValue("Path", "configPath");
                Global.TempImagePath = myini.IniReadValue("Path", "tempImagePath");
                Global.SaveImagePath = myini.IniReadValue("Path", "saveImagePath");
                Global.XRayImagePath = myini.IniReadValue("Path", "xRayImagePath");
                Global.RecipePath = myini.IniReadValue("Path", "receiptPath");
                Global.InkPointPath = myini.IniReadValue("Path", "inkPointPath");
                Global.LotSummaryPath = myini.IniReadValue("Path", "lotSummaryPath");
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                LogManager.WriteLog("载入参数出错" + ee.ToString());
            }
        }


    }

}
