using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using MitsubishiPLCCommunication;
using System.Threading;


//namespace P1_CMMT
//{
//    class PC2PLC
//    {
//        /*

//            W1收到1，第一次拍照，清0.   结果返回  OK则W2写1 NG则W2写2 
                        
//            W3收到1第二次拍照，清0，  结果返回 OK则W4写1， NG则W4写2


//            W5收到1 读表，清0，  结果返回 W6  微米

//            W7收到1 表清零，结果返回W8 写1

//        */


//        public bool writeflag = true;

//        public delegate void delegateReadW1();

//        public event delegateReadW1 W1Readed;


//        public delegate void delegateReadW3();

//        public event delegateReadW3 W3Readed;



//        public delegate void delegateReadW5();
//        public event delegateReadW5 W5Readed;

//        public delegate void delegateReadW7();
//        public event delegateReadW7 W7Readed;


//        Thread t1;
//        Thread t2;
//        Thread t3;
//        Thread t4;


//        Thread t5;

//        IMitsubishiCommunication PLC  = new MitsubishiCPUCommunication();
//        /// <summary>
//        /// 返回0打开成功，非0失败
//        /// </summary>
//        /// <returns></returns>
//        public int Open()
//        {
           
//            MitsubishiCPUSettings setting = new MitsubishiCPUSettings()
//            {
//                UnitType = 0x0052,
//                ProtocolType = 0x0005,
//                StationNumber = 255,
//                IONumber = 0x03FF,
//                CPUType = 0x00A1,
//                HostAddress = "192.168.3.39",
//                TimeOut = 1000
//            };
//            int ret = PLC.Open(setting);
            
//            return ret;
//        }





//        /// <summary>
//        /// 开始W1读取线程，读到1触发事件W1Readed，并将w1清0
//        /// </summary>
//        public void BeginW1Thread()
//        {
//            t1 = new Thread(W1Th);
//            t1.Start();
//        }


//        private void W1Th()
//        {
//            while(true)
//            {
//                short[] data = new short[1];
//                PLCRead("W01", 1, out data);
//                if(data[0]==1)
//                {
//                    short[] data2 = new short[1];
//                    data2[0] = 0;
//                    PlcWrite("W01", 1, ref data2);
//                    W1Readed();
//                }
//                Thread.Sleep(200);                
//            }
//        }

//        /// <summary>
//        ///  开始W3读取线程，读到1触发事件W3Readed，并将w3清0
//        /// </summary>
//        public void BeginW3Thread()
//        {
//            t2 = new Thread(W3Th);
//            t2.Start();
//        }

        
//        private void W3Th()
//        {
//            while (true)
//            {
//                short[] data = new short[1];
//                PLCRead("W03", 1, out data);
//                if (data[0] == 1)
//                {
//                    short[] data2 = new short[1];
//                    data2[0] = 0;
//                    PlcWrite("W03", 1, ref data2);
//                    W3Readed();
//                }
//                Thread.Sleep(200);
//            }
//        }


//        public int WriteW2(bool f)
//        {
//            int a;
//            if(f)
//            {
//                short[] data = new short[1];
//                data[0] = 1;
//                a = PlcWrite("W02", 1, ref data);
//            }
//            else
//            {
//                short[] data = new short[1];
//                data[0] = 2;
//                a = PlcWrite("W02", 1, ref data);
//            }

//            return a;
//        }


//        public int WriteW4(bool f)
//        {
//            int a;
//            if (f)
//            {
//                short[] data = new short[1];
//                data[0] = 1;
//                a = PlcWrite("W04", 1, ref data);
//            }
//            else
//            {
//                short[] data = new short[1];
//                data[0] = 2;
//                a = PlcWrite("W04", 1, ref data);
//            }

//            return a;

//        }



//        public void BeginW5Thread()
//        {
//            t3 = new Thread(W5Th);
//            t3.Start();
//        }


//        private void W5Th()
//        {
//            while (true)
//            {
//                short[] data = new short[1];
//                PLCRead("W05", 1, out data);
//                if (data[0] == 1)
//                {
//                    short[] data2 = new short[1];
//                    data2[0] = 0;
//                    PlcWrite("W05", 1, ref data2);
//                    W5Readed();
//                }
//                Thread.Sleep(200);
//            }
//        }



//        public void BeginW7Thread()
//        {
//            t4 = new Thread(W7Th);
//            t4.Start();
//        }


//        private void W7Th()
//        {
//            while (true)
//            {
//                short[] data = new short[1];
//                PLCRead("W07", 1, out data);
//                if (data[0] == 1)
//                {
//                    short[] data2 = new short[1];
//                    data2[0] = 0;
//                    PlcWrite("W07", 1, ref data2);
//                    W7Readed();
//                }
//                Thread.Sleep(200);
//            }
//        }


//        /// <summary>
//        /// W6写入长度，微米
//        /// </summary>
//        /// <param name="length">长度</param>
//        /// <returns></returns>
//        public int WriteW6(short length)
//        {
//            int a;
//            short[] data = new short[1];
//            data[0] = length;
//            a = PlcWrite("W06", 1, ref data);

//             short[] data2 = new short[1];
//            data2[0] = 1;
//            PlcWrite("W09", 1, ref data2);

//            return a;

//        }


//        public int WriteW8()
//        {
//            int a;
//            short[] data = new short[1];
//            data[0] = 1;
//            a = PlcWrite("W08", 1, ref data);
//            return a;
//        }


//        /// <summary>
//        /// 开始所有读去W1,W3,W5,W7的线程
//        /// </summary>
//        public void BeginAllTH()
//        {
//            BeginW1Thread();
//            BeginW3Thread();
//            BeginW5Thread();
//            BeginW7Thread();

//           // beginTh5();
//        }

//        private object _syn = new object();

//        private int PlcWrite(string szDevice,int lSize,ref short[] lpdata )
//        {
//            lock (_syn)
//            {
//                int rtn = PLC.WriteDeviceBlock2(szDevice, lSize, ref lpdata);
//                if(rtn!=0)
//                {
//                    LogManager.WriteLog("PLC写入异常");
//                }
//                return rtn;
//            }
//        }

//        private int PLCRead(string szDevice,int lSize ,out short[] lpdata)
//        {
//            lock (_syn)
//            {
//                int rtn = PLC.ReadDeviceBlock2(szDevice, lSize, out lpdata);

//                if (rtn != 0)
//                {
//                    //Open();            //没连上就尝试重连
//                    //Thread.Sleep(5000);

//                    if (writeflag)
//                    {
//                        LogManager.WriteLog("PLC读取异常");
//                        writeflag = false;
//                    }
//                }
//                return rtn;
//            }
//        }


//        //private void beginTh5()
//        //{
//        //    t5 = new Thread(CheckLink);
//        //    t5.Start();
//        //}


//        /// <summary>
//        /// 间隔10秒检测是否PLC是否连接，没连就重连。
//        /// </summary>
//        //private void CheckLink()
//        //{
//        //    while(true)
//        //    {
//        //        Thread.Sleep(2000);
//        //        if (PLC.IsOpen)
//        //        {
//        //            int a=Open();
//        //            if(a!=0)
//        //            {
//        //                Thread.Sleep(8000);
//        //            }
//        //        }                
//        //    }
//        //}

//    }
//}
