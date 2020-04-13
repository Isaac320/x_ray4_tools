using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;

namespace ImageProcess
{
    public interface IImageProcess
    {        
        /// <summary>
        /// 初始化算法函数
        /// </summary>
        /// <returns></returns>
        bool Init();

        /// <summary>
        /// 处理图像方法,返回好坏
        /// </summary>
        /// <param name="hImage">输入图像</param>
        /// <param name="region">输入region</param>
        /// <param name="xld">输出xld</param>
        /// <param name="index">输出代号</param>
        /// <param name="message">输出问题信息</param>
        /// <returns>返回是否成功执行</returns>
        bool Process(HImage hImage, HObject region,out HObject xld, out int index, out string message);
    }
}

