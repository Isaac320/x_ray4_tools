using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;

namespace P1_CMMT.VisionTools.BParamTool
{
    [Serializable]
    public class BParamTool
    {
        public HImage hImage = null;

        public string RecipeName = "recipe";


        public int ImageXNum
        {
            get;
            set;
        }

        public int ImageYNum
        {
            get;
            set;
        }


        public int FrameXNum
        {
            get;
            set;
        }

        public int FrameYNum
        {
            get;
            set;
        }


    }
}
