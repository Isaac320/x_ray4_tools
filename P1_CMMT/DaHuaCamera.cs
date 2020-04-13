using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using System.Windows.Forms;


//namespace P1_CMMT
//{
////    class DaHuaCamera
////    {
////        public delegate void ImageHandler(Bitmap image);
////        public event ImageHandler ImageGrabbed;
////        private IDevice m_dev;

////        public DaHuaCamera()
////        {
////            initAcq();
////        }

////        private void initAcq()
////        {
////            try
////            {
////                /* 设备搜索 */
////                List<IDeviceInfo> li = Enumerator.EnumerateDevices();
////                if (li.Count > 0)
////                {
////                    /* 获取搜索到的第一个设备 */
////                    m_dev = Enumerator.GetDeviceByIndex(0);

////                    m_dev.ConnectionLost += OnConnectLoss;



////                    /* 打开设备 */
////                    if (!m_dev.Open())
////                    {
////                        MessageBox.Show(@"连接相机失败");
////                        return;
////                    }
////                    m_dev.TriggerSet.Open(TriggerSourceEnum.Software);




////                    /* 注册码流回调事件 */
////                    m_dev.StreamGrabber.ImageGrabbed += StreamGrabber_ImageGrabbed;



////                    if (!m_dev.GrabUsingGrabLoopThread())
////                    {
////                        MessageBox.Show(@"开启码流失败");
////                        return;
////                    }

////                }
////            }
////            catch (Exception exception)
////            {
////                Catcher.Show(exception);
////            }
////        }

////        private void StreamGrabber_ImageGrabbed(object sender, GrabbedEventArgs e)
////        {
////            IGrabbedRawData frame = e.GrabResult.Clone();
////            Bitmap bitmap = frame.ToBitmap(false);
////            ImageGrabbed(bitmap);

////        }

////        private void OnConnectLoss(object sender, EventArgs e)
////        {
////            m_dev.ShutdownGrab();
////            m_dev.Dispose();
////            m_dev = null;

////        }


////        public void snap()
////        {
////            if (m_dev == null)
////            {
////                throw new InvalidOperationException("Device is invalid");
////            }

////            try
////            {
////                m_dev.ExecuteSoftwareTrigger();
////            }
////            catch (Exception exception)
////            {
////                Catcher.Show(exception);
////            }
////        }


////        public void close()
////        {
////            if (m_dev != null)
////            {
////                m_dev.StreamGrabber.ImageGrabbed -= StreamGrabber_ImageGrabbed;
////                m_dev.Dispose();
////                m_dev = null;
////            }

////        }
////    }
////}
