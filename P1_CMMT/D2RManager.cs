using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace P1_CMMT
{
    /// <summary>
    /// 管理Device到Receipt的对应关系
    /// 就是从一个数据库里去Device对应的Receipt名字
    /// </summary>
    class D2RManager
    {
        static string mDeviceDBPath = "d:\\data\\Device2Receipt.db";
        static object _lock = new object();

        public static string QueryReceipt(string device)
        {
            string rpt = null;
            lock (_lock)
            {
                using (SQLiteConnection sqliteConn = new SQLiteConnection("Data Source=" + mDeviceDBPath))
                {
                    try
                    {
                        sqliteConn.Open();
                        SQLiteCommand cmd = new SQLiteCommand();
                        cmd.Connection = sqliteConn;
                        cmd.CommandText = "SELECT Receipts FROM Table1 WHERE Devices='" + device + "'";
                        rpt = (string)cmd.ExecuteScalar();
                    }
                    catch (SQLiteException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        sqliteConn.Close();
                    }
                }
            }
            return rpt;
        }




    }
}
