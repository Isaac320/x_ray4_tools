using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace P1_CMMT
{
    [Serializable]
    class MyXXX
    {
        public DateTime mydata;
        public bool IsRegister;
        public string MNum;
        public string RNum;
    }
    class TestMyXXX
    {
        SoftReg rr = new SoftReg();
        MyXXX myXXX = new MyXXX();
        DateTime my1stTime = new DateTime(2020, 01, 01);
        public void save(string str)
        {

            myXXX.mydata = my1stTime;
            myXXX.MNum = rr.GetMNum();
            myXXX.IsRegister = string.Equals(str, rr.GetNum());
            myXXX.RNum = str;
            Stream s = File.Open("xx.dat", FileMode.OpenOrCreate);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(s, myXXX);
            s.Close();

        }

        public int TimeLeft()
        {
            if (File.Exists("xx.dat"))
            {
                Stream s = File.Open("xx.dat", FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                object o = bf.Deserialize(s);
                s.Close();
                MyXXX p = o as MyXXX;
                bool b1 = string.Equals(p.RNum, rr.GetNum());
                if (p.IsRegister && b1)
                {
                    return 10000;
                }
                else
                {
                    TimeSpan ts = DateTime.Now - p.mydata;
                    return Global.NeedDays - ts.Days;
                }
            }
            return -1;
        }


    }
}
