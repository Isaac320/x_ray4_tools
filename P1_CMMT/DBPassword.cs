using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace P1_CMMT
{
    class DBPassword
    {
        static string mDeviceDBPath = "d:\\data\\password.db";
        static object _lock = new object();

        /// <summary>
        /// 输出level 如果为null 就是没权限
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static string Query(string username,string password)
        {
            string level = null;
            lock (_lock)
            {
                using (SQLiteConnection sqliteConn = new SQLiteConnection("Data Source=" + mDeviceDBPath))
                {
                    try
                    {
                        sqliteConn.Open();
                        SQLiteCommand cmd = new SQLiteCommand();
                        cmd.Connection = sqliteConn;
                        cmd.CommandText = "SELECT Level FROM Table1 WHERE UserName='" +username+ "'" + "AND Password='" + password + "'";
                        level = (string)cmd.ExecuteScalar();

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
            return level;
        }

        /// <summary>
        /// 增加用户
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public static int AddUser(string username,string password,string level)
        {
            int rows = 0;
            lock (_lock)
            {
                using (SQLiteConnection sqliteConn = new SQLiteConnection("Data Source=" + mDeviceDBPath))
                {
                    try
                    {
                        sqliteConn.Open();
                        SQLiteCommand cmd = new SQLiteCommand();
                        cmd.Connection = sqliteConn;
                        cmd.CommandText = "INSERT INTO Table1(Username,Password,Level) VALUES('" + username + "','" + password + "','" + level + "')";
                        rows = cmd.ExecuteNonQuery();
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
            return rows;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static int DeleteUser(string username)
        {
            int rows = 0;
            lock (_lock)
            {
                using (SQLiteConnection sqliteConn = new SQLiteConnection("Data Source=" + mDeviceDBPath))
                {
                    try
                    {
                        sqliteConn.Open();
                        SQLiteCommand cmd = new SQLiteCommand();
                        cmd.Connection = sqliteConn;
                        cmd.CommandText = "DELETE FROM Table1 WHERE Username='"+username+"'";
                        rows = cmd.ExecuteNonQuery();
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
            return rows;
        }


        /// <summary>
        /// 读取user列表
        /// </summary>
        /// <returns></returns>
        public static List<string> ReadUsers()
        {
            List<string> list = new List<string>();

            lock (_lock)
            {
                using (SQLiteConnection sqliteConn = new SQLiteConnection("Data Source=" + mDeviceDBPath))
                {
                    try
                    {
                        sqliteConn.Open();
                        SQLiteCommand cmd = new SQLiteCommand();
                        cmd.Connection = sqliteConn;
                        cmd.CommandText = "SELECT Username FROM Table1";
                        SQLiteDataReader dr = cmd.ExecuteReader();
                        while(dr.Read())
                        {
                            list.Add(dr[0].ToString());
                        }
                        
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


            return list;

        }




    }
}
