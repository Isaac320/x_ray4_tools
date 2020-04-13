using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DALSA.SaperaLT.SapClassBasic;
using DALSA.SaperaLT;
using System.Runtime.InteropServices;
using HalconDotNet;

namespace P1_CMMT
{
    class LineScan
    {
        string m_ServerName = "Linea_M8192-7um_1";
        SapLocation m_ServerLocation;
        // SapAcquisition m_Acquisition;

        SapAcqDevice m_AcqDevice;

        SapBuffer m_Buffers;
        //SapAcqToBuf m_Xfer;

        SapAcqDeviceToBuf m_Xfer;

        string mDevice = "12176189";

        string filename = @"D:\ww\test.ccf";

        public delegate void myEventHandler(HImage image);
        public event myEventHandler ImageGrabbed;






        public string M_ServerName           //卡名字
        {
            set { m_ServerName = value; }
            get { return m_ServerName; }
        }

        public string CCFpath            //ccf文件路径
        {
            set { filename = value; }
            get { return filename; }
        }



        public bool init()     //初始化相机
        {
            try
            {
                m_ServerLocation = new SapLocation(m_ServerName, 0);
                // m_Acquisition = new SapAcquisition(m_ServerLocation, filename);
                m_AcqDevice = new SapAcqDevice(m_ServerLocation, filename);
                m_Buffers = new SapBufferWithTrash(2, m_AcqDevice, SapBuffer.MemoryType.ScatterGather);
                m_Xfer = new SapAcqDeviceToBuf(m_AcqDevice, m_Buffers);

                // m_Xfer.Pairs[0].EventType = SapXferPair.XferEventType.EndOfFrame;
                m_Xfer.XferNotify += m_Xfer_XferNotify;

                if (!CreateObjects())
                {
                    DisposeObjects();
                    return false;
                }



            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
            }
            return true;
        }

        private bool CreateObjects()
        {
            // Create acquisition object
            if (m_AcqDevice != null && !m_AcqDevice.Initialized)
            {
                if (m_AcqDevice.Create() == false)
                {
                    DestroyObjects();
                    return false;
                }
            }
            // Create buffer object
            if (m_Buffers != null && !m_Buffers.Initialized)
            {
                if (m_Buffers.Create() == false)
                {
                    DestroyObjects();
                    return false;
                }
                m_Buffers.Clear();
            }


            if (m_Xfer != null && m_Xfer.Pairs[0] != null)
            {
                m_Xfer.Pairs[0].Cycle = SapXferPair.CycleMode.NextWithTrash;
                if (m_Xfer.Pairs[0].Cycle != SapXferPair.CycleMode.NextWithTrash)
                {
                    DestroyObjects();
                    return false;
                }
            }

            // Create Xfer object
            if (m_Xfer != null && !m_Xfer.Initialized)
            {
                if (m_Xfer.Create() == false)
                {
                    DestroyObjects();
                    return false;
                }
            }
            return true;
        }
        private void DestroyObjects()
        {
            if (m_Xfer != null && m_Xfer.Initialized)
                m_Xfer.Destroy();

            if (m_Buffers != null && m_Buffers.Initialized)
                m_Buffers.Destroy();
            if (m_AcqDevice != null && m_AcqDevice.Initialized)
                m_AcqDevice.Destroy();

        }

        private void DisposeObjects()
        {
            if (m_Xfer != null)
            { m_Xfer.Dispose(); m_Xfer = null; }
            if (m_Buffers != null)
            { m_Buffers.Dispose(); m_Buffers = null; }
            if (m_AcqDevice != null)
            { m_AcqDevice.Dispose(); m_AcqDevice = null; }
        }

        private void m_Xfer_XferNotify(object sender, EventArgs e)
        {

            HImage hImage = new HImage();
            IntPtr myptr;
            m_Buffers.GetAddress(out myptr);
            hImage.GenImage1("byte", m_Buffers.Width, m_Buffers.Height, myptr);
            ImageGrabbed(hImage);
        }

        public bool Snap()
        {
            return m_Xfer.Snap();
        }

        public bool Grab()
        {
            return m_Xfer.Grab();
        }


        public bool Freeze()
        {
            return m_Xfer.Freeze();
        }

        public bool Destory()
        {
            return m_Xfer.Destroy();
        }

    }
}
