//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Basler.Pylon;
//using HalconDotNet;
//using System.Windows.Forms;
////using Apintec.Log;


//namespace P1_CMMT
//{
//    class BalserCamera
//    {
//        public delegate void ImageHandler(HImage image);
//        public event ImageHandler ImageGrabbed;
//        Camera camera = null;
//        public BalserCamera()
//        {
//            initAcq();
//        }

//       private  void initAcq()
//        {
//            try
//            {
//                camera = new Camera();
//                camera.CameraOpened += Configuration.AcquireContinuous;
//                camera.StreamGrabber.ImageGrabbed += StreamGrabber_ImageGrabbed;
//                camera.Open();
//                camera.Parameters[PLCameraInstance.MaxNumBuffer].SetValue(5);
//            }
//            catch(Exception ee)
//            {
//                // APLog.Write(Priority.ALERT, "相机初始化失败."+ee.ToString());
//                LogManager.WriteLog("相机初始化失败." + ee.ToString());
//            }
//        }

//        private void StreamGrabber_ImageGrabbed(object sender, ImageGrabbedEventArgs e)
//        {
//            try
//            {
//                IGrabResult grabResult = e.GrabResult;
//                HImage hImage = new HImage();
//                byte[] buffer;
//                using (grabResult)
//                {
//                    if (grabResult.GrabSucceeded)
//                    {
//                        buffer = grabResult.PixelData as byte[];
//                        //IntPtr pData = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
//                        //hImage.GenImage1("byte", grabResult.Width, grabResult.Height, pData);

//                        unsafe
//                        {
//                            fixed (byte* tmp = &buffer[0])
//                            {
//                                IntPtr p = (IntPtr)tmp;
//                                hImage.GenImage1("byte", grabResult.Width, grabResult.Height, p);
//                            }
//                        }
//                        ImageGrabbed(hImage);
//                    }
//                }                
//            }
//            catch (Exception eee)
//            {
//                //APLog.Write(Priority.ALERT, "相机采集图像问题."+eee.ToString());
//                LogManager.WriteLog("相机采集图像问题." + eee.ToString());
//            }
//        }


//        public void snap()
//        {
//            try
//            {
//                camera.Parameters[PLCamera.TriggerMode].SetValue(PLCamera.TriggerMode.Off);
//                camera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.SingleFrame);
//                camera.StreamGrabber.Start(1, GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
//            }
//            catch(Exception ee)
//            {
//                //APLog.Write(Priority.ALERT, "snap问题，" + ee.ToString());
//                LogManager.WriteLog("snap问题，" + ee.ToString());
//            }
//        }


//        public void close()
//        {
//            try
//            {
//                camera.CameraOpened -= Configuration.AcquireContinuous;
//                camera.StreamGrabber.ImageGrabbed -= StreamGrabber_ImageGrabbed;
//                camera.Close();
//                camera.Dispose();
//            }
//            catch(Exception ee)
//            {
//                // APLog.Write(Priority.ALERT, "关闭问题," + ee.ToString());
//                LogManager.WriteLog("相机关闭问题," + ee.ToString());
//            }
//        }

//    }

//}

