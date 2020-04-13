using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace P1_CMMT
{
    public class ecat_io
    {
        #region ecat_io接口中用到的结构体

        public struct EC_RES
        {
            public int SlaveNum;    //从站个数
            public int DiNum;       //数字量输入通道数
            public int DoNum;       //数字量输出通道数
            public int AiNum;       //模拟量输入通道数
            public int AoNum;       //模拟量输出通道数
        };

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct EC_SLAVE_INFO
        {
            public int VendorID;        //厂家ID
            public int ProductCode;     //产品编号
            public int RevisionNo;      //版本号
            public int ModuleNum;       //该从站的模块数量
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I4)]
            public int[] ModuleId;      //模块ID int[32]
            public int DiNum;           //该从站的数字量输入通道数
            public int DoNum;           //该从站的数字量输出通道数
            public int AiNum;           //该从站的模拟量输入通道数
            public int AoNum;           //该从站的模拟量输出通道数
        };

        #endregion

        [DllImport("ecat_io.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short EC_Open(short card, short param);

        [DllImport("ecat_io.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short EC_Close(short card);

        [DllImport("ecat_io.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern short EC_GetVersion(out char pVersion, short size, short card);

        [DllImport("ecat_io.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short EC_GetSlaveResource(out EC_RES pResScan, out EC_RES pResOnline, short card);

        [DllImport("ecat_io.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short EC_GetSlaveInfo(out EC_SLAVE_INFO pInfo, short slaveNo, short card);

        [DllImport("ecat_io.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short EC_LoadEni(string eniPath, short card);

        [DllImport("ecat_io.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short EC_ConnectECAT(short option, short card);

        [DllImport("ecat_io.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short EC_DisconnectECAT(short card);

        [DllImport("ecat_io.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short EC_EcatSDODownload(short slaveNo, short index, short subindex, short data_size, uint data, short card);

        [DllImport("ecat_io.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short EC_EcatSDOUpload(short slaveNo, short index, short subindex, short data_size, out uint pBuf, short count, short card);

        [DllImport("ecat_io.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short EC_SetPdoData(short slaveNo, short moduleNo, short index, short subindex, short data_size, uint data, short card);

        [DllImport("ecat_io.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short EC_GetPdoData(short slaveNo, short moduleNo, short index, short subindex, short data_size, out uint pData, short card);

        [DllImport("ecat_io.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short EC_Set_Digital_Chn_Out(short slaveNo, short channel, short value, short card);

        [DllImport("ecat_io.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short EC_Set_Digital_Port_Out(short slaveNo, short chnBegin, uint lValue, uint lMask, short card);

        [DllImport("ecat_io.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short EC_Get_Digital_Chn_Out(short slaveNo, short channel, ref short value, short card);

        [DllImport("ecat_io.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short EC_Get_Digital_Port_Out(short slaveNo, short chnBegin, ref uint lValue, short card);

        [DllImport("ecat_io.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short EC_Get_Digital_Chn_In(short slaveNo, short channel, ref short value, short card);

        [DllImport("ecat_io.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short EC_Get_Digital_Port_In(short slaveNo, short chnBegin, ref uint lValue, short card);

        [DllImport("ecat_io.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short EC_Set_Analog_Chn_Out(short slaveNo, short channel, out short pValue, short count, short card);

        [DllImport("ecat_io.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short EC_Get_Analog_Chn_Out(short slaveNo, short channel, out short pValue, short count, short card);

        [DllImport("ecat_io.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short EC_Get_Analog_Chn_In(short slaveNo, short channel, out short pValue, short count, short card);


    }
}
