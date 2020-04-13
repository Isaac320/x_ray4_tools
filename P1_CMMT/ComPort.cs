using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

using System.Windows.Forms;

namespace P1_CMMT
{
    class ComPort
    {
        private SerialPort Port = new SerialPort();

        public delegate void PortHandler(byte[] data);
        public event PortHandler DataGot;
       
        /// <summary>
        /// 初始化串口
        /// </summary>
        public void Init()
        {
            int num=SerialPort.GetPortNames().Length;
            if(num>0)
            {

            }
            else
            {
                MessageBox.Show("找不到串口");
            }

            Port.PortName = "COM4";
            Port.BaudRate = 38400;
            Port.Parity = Parity.None;
            Port.DataBits = 8;
            Port.StopBits = StopBits.Two;
            Port.DtrEnable = true;

            try
            {
                Port.Open();
                Port.DataReceived += Port_DataReceived;
            }
            catch(Exception ex)
            {
                //Apintec.Log.APLog.Write(Apintec.Log.Priority.ALERT, "串口打开失败");
                MessageBox.Show("串口打开失败");
                LogManager.WriteLog("串口打开失败" + ex.ToString());
            }

        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] ReDatas = new byte[Port.BytesToRead];
            Port.Read(ReDatas, 0, ReDatas.Length);
            DataGot(ReDatas);
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SendData(byte[] data)
        {
            if (!Port.IsOpen)
            {
                Init();
                
            }
            
            try
            {
                Port.Write(data, 0, data.Length);
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "错误");
            }

            return false;
                    
        }


        
        



    }
}
