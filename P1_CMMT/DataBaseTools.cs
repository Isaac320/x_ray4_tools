using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Data;

namespace P1_CMMT
{

    public struct Model_INST
    {
        public string lotGUID;
        public string lotNo;
        public DateTime startTime;
        public DateTime endTime;
        public DateTime endLotTime;
        public string operatorID;
        public string attrib;
        public string runAttrib;
        public string reportType;
    }

    public struct Model_FRAME
    {
        public string lotGUID;
        public int trayIndex;
        public int frameIndex;
        public string unitContent;
    }


    class DataBaseTools
    {

        static string mDbPath = "d:\\data\\test.db3";  //数据库地址

        static object _lock = new object();

        private static void CreateDB(string dbPath, string tableName, string columnName)
        {
            using (SQLiteConnection sqliteConn = new SQLiteConnection("Data Source="+dbPath))
            {
                if (sqliteConn.State != System.Data.ConnectionState.Open)
                {
                    sqliteConn.Open();
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.Connection = sqliteConn;
                    cmd.CommandText = "CREATE TABLE " + tableName + columnName; ;
                    cmd.ExecuteNonQuery();
                }

            }
        }

        string ssss = "(Name varchar,Team varchar, Number varchar,Time datetime,num int)";

        /// <summary>
        /// 初始化，就是把数据库建立了，然后把两张表也建立了。
        /// </summary>
        private static void Init()
        {
            if(!File.Exists(mDbPath))
            {
                string table1 = "REP_INST";
                string columnName1 = "(lotGUID varchar,lotNo varchar,startTime datetime,endTime datetime,endLotTime datetime,operatorID varchar,attrib varchar,runAttrib varchar,reportType varchar)";
                CreateDB(mDbPath,table1, columnName1);

                string table2 = "REP_FRAME";
                string columnName2 = "(lotGUID varchar,trayIndex int,frameIndex int,unitContent varchar)";
                CreateDB(mDbPath, table2, columnName2);
            }
        }

        /// <summary>
        /// 往INST表里写东西
        /// </summary>
        /// <param name="mModel"></param>
        /// <returns></returns>
        public static int InsertModel_INST(Model_INST mModel)
        {
            
            lock (_lock)
            {
                Init();
                using (SQLiteConnection sqliteConn = new SQLiteConnection("Data Source=" + mDbPath))
                {
                    sqliteConn.Open();
                    using (SQLiteCommand cmd = sqliteConn.CreateCommand())
                    {
                        try
                        {
                            //cmd.CommandText = SQLString;
                            string strSql = "INSERT INTO REP_INST(lotGUID,lotNo,startTime,endTime,endLotTime,operatorID,attrib,runAttrib,reportType) VALUES(@lotGUID,@lotNo,@startTime,@endTime,@endLotTime,@operatorID,@attrib,@runAttrib,@reportType)";
                            cmd.CommandText = strSql;
                            SQLiteParameter[] parameters =
                            {
                                new SQLiteParameter("@lotGUID",mModel.lotGUID),
                                new SQLiteParameter("@lotNo",mModel.lotNo),
                                new SQLiteParameter("@startTime",mModel.startTime),
                                new SQLiteParameter("@endTime",mModel.endTime),
                                new SQLiteParameter("@endLotTime",mModel.endLotTime),
                                new SQLiteParameter("@operatorID",mModel.operatorID),
                                new SQLiteParameter("@attrib",mModel.attrib),
                                new SQLiteParameter("@runAttrib",mModel.runAttrib),
                                new SQLiteParameter("@reportType",mModel.reportType),
                            };

                            foreach (var param in parameters)
                            {
                                cmd.Parameters.Add(param);
                            }

                            int rows = cmd.ExecuteNonQuery();

                            return rows;
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
            }
        }

        /// <summary>
        /// 往FRAME表里写东西
        /// </summary>
        /// <param name="mModel"></param>
        /// <returns></returns>
        public static int InsertModel_FRAME(Model_FRAME mModel)
        {
            lock (_lock)
            {
                Init();
                using (SQLiteConnection sqliteConn = new SQLiteConnection("Data Source=" + mDbPath))
                {
                    sqliteConn.Open();
                    using (SQLiteCommand cmd = sqliteConn.CreateCommand())
                    {
                        try
                        {
                            //cmd.CommandText = SQLString;
                            string strSql = "INSERT INTO REP_FRAME(lotGUID,trayIndex,frameIndex,unitContent) VALUES(@lotGUID,@trayIndex,@frameIndex,@unitContent)";
                            cmd.CommandText = strSql;
                            SQLiteParameter[] parameters =
                            {
                                new SQLiteParameter("@lotGUID",mModel.lotGUID),
                                new SQLiteParameter("@trayIndex",mModel.trayIndex),
                                new SQLiteParameter("@frameIndex",mModel.frameIndex),
                                new SQLiteParameter("@unitContent",mModel.unitContent),
                                
                            };

                            foreach (var param in parameters)
                            {
                                cmd.Parameters.Add(param);
                            }

                            int rows = cmd.ExecuteNonQuery();

                            return rows;
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
            }
        }

        /// <summary>
        /// sql语句查询数据库 返回DataTable
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns></returns>
        public static DataTable Query(string sql)
        {
            lock (_lock)
            {
                using (SQLiteConnection sqliteConn = new SQLiteConnection("Data Source=" + mDbPath))
                {
                    try
                    {
                        if (sqliteConn.State != System.Data.ConnectionState.Open)
                        {
                            DataSet myds = new DataSet();
                            sqliteConn.Open();

                            SQLiteDataAdapter myda = new SQLiteDataAdapter(sql, sqliteConn);
                            myda.Fill(myds, "Table1");
                            return myds.Tables[0];
                        }
                        return null;
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
        }       

    }
}
