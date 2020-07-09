using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using ImageProcess;

namespace P1_CMMT.ImgProcess
{
    public class ImgProcess : IImageProcess
    {
        public static Dictionary<string, object> Dict = new Dictionary<string, object>();

        private VisionTools.MacthTool.MatchTool matchTool;
        private VisionTools.BParamTool.BParamTool bParamTool;

        public List<HRegion> Regions = new List<HRegion>();

        public bool Init()
        {
            throw new NotImplementedException();
        }

        public bool Init(string path)
        {
            //从文件读取配方 存入字典里
            //

            //从字典文件中读取处理
            bParamTool = (VisionTools.BParamTool.BParamTool)Dict[Global.ToolName1];
            matchTool = (VisionTools.MacthTool.MatchTool)Dict[Global.ToolName2];
            Regions = matchTool.Regions;   //给进区域
            return true;
        }

        public bool Process(HImage hImage, HObject region, out HObject xld, out int index, out string message)
        {          
            throw new NotImplementedException();
        }


        private bool FixImage(HImage hImage, HObject region,out HImage outImage,out HHomMat2D homMat)
        {
            matchTool.Image = hImage;
            matchTool.Run((HRegion)region, out outImage, out homMat);            
            outImage = null;
            homMat = null;
            return true;
        }


        private bool CheckLines(HImage hImage, out HObject xld,out int index,out string message)
        {
            xld = null;
            index = 1;
            message = "OK";
            return true;
        }

        
    }
}
