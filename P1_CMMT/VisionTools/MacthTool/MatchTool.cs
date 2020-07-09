using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;

namespace P1_CMMT.VisionTools.MacthTool
{
    [Serializable]
    public class MatchTool
    {
        //[NonSerialized]
        public HImage himage = null;

        public double[] searchRect = new double[4] { 50, 50, 200, 200 };
        public double[] trainRect = new double[4] { 100, 100, 210, 210 };
        public double[] maskRect = new double[4] { 120, 120, 230, 230 };

        private HTuple row;
        private HTuple column, score, angle;
        private string message = null;
        private HShapeModel myShapeModel = null;

        public double minScore = 0.5;
        public int numMatches = 1;

        private HTuple row1, column1, angle1, score1;

        /// <summary>
        /// 这几个region是储存n个搜索区域
        /// </summary>
        public List<HRegion> Regions = new List<HRegion>();

        #region  属性
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get { return message; } }
        public HTuple Row { get { return row; } }
        public HTuple Column { get { return column; } }
        public HTuple Angle { get { return angle; } }
        public HTuple Score { get { return score; } }
        public HXLDCont Xld { get { return myShapeModel.GetShapeModelContours(1); } }

        public HImage Image
        {
            get { return himage; }
            set { himage = value; }
        }

        #endregion

        /// <summary>
        /// 搜索
        /// </summary>
        /// <returns></returns>
        public bool Run()
        {
            try
            {
                myShapeModel.FindShapeModel(himage, 0, 0, minScore, numMatches, 0.5, "least_squares", 0, 0.9, out row, out column, out angle, out score);

                if (score.TupleLength() == 0)
                {
                    message = "Not Find";
                    return false;
                }

                message = null;
                return true;
            }
            catch (Exception ee)
            {
                message = "FindShapeModel Error " + ee.ToString(); ;
                return false;
            }

        }

        /// <summary>
        /// 带region的匹配
        /// </summary>
        /// <param name="hRegion"></param>
        /// <returns></returns>
        public bool Run(HRegion hRegion,out HImage outImage,out HHomMat2D homMat)
        {
            try
            {
                HImage ROIImage = himage.ReduceDomain(hRegion);
                ROIImage.FindShapeModel(myShapeModel, 0, 0, minScore, 1, 0.5, "least_squares", 0, 0.9, out row, out column, out angle, out score);
                if (score.TupleLength() == 0)
                {
                    message = "Not Find";
                    outImage = null;
                    homMat = null;
                    return false;
                }
                homMat = new HHomMat2D();
                homMat.VectorAngleToRigid(row1, column1, 0, row, column, 0);

                HHomMat2D homMat2 = homMat.HomMat2dInvert();
                outImage = homMat2.AffineTransImage(ROIImage, "constant", "false");
                              
                message = null;
                return true;
            }
            catch (Exception ee)
            {
                message = "FindShapeModel Error " + ee.ToString();
                outImage = null;
                homMat = null;
                return false;
            }
        }



        /// <summary>
        /// 训练
        /// </summary>
        /// <returns></returns>
        public bool Train()
        {
            try
            {
                if (myShapeModel != null)
                {
                    myShapeModel.Dispose();
                }
                //myShapeModel = new HShapeModel();

                HRegion hTrain = new HRegion(trainRect[0], trainRect[1], trainRect[2], trainRect[3]);
                HRegion hSearch = new HRegion(searchRect[0], searchRect[1], searchRect[2], searchRect[3]);
                HRegion hMask = new HRegion(maskRect[0], maskRect[1], maskRect[2], maskRect[3]);

                //HObject ho_ImageReduced;
                //HOperatorSet.GenEmptyObj(out ho_ImageReduced);
                //ho_ImageReduced.Dispose();
                //HOperatorSet.ReduceDomain(himage, hTrain, out ho_ImageReduced);
                //hTrain.Difference(hMask);


                HImage ROIImage = himage.ReduceDomain(hTrain.Difference(hMask));

                

                myShapeModel = ROIImage.CreateShapeModel("auto", -0.39, 0.79, "auto", "auto", "use_polarity", "auto", "auto");

                ROIImage.Dispose();

                //myShapeModel.CreateShapeModel(ROIImage, "auto", -0.39, 0.79, "auto", "auto", "use_polarity", "auto", "auto");

                //HObject imagePart;
                //HOperatorSet.CropDomain(ho_ImageReduced, out imagePart);

                //HImage mymyImage = new HImage();
                //HObjectToHImage(imagePart, ref mymyImage);
                //myShapeModel.CreateShapeModel(mymyImage, "auto", -0.39, 0.79, "auto", "auto", "use_polarity", "auto", "auto");

                HImage ROIImage2 = himage.ReduceDomain(hSearch);
                ROIImage2.FindShapeModel(myShapeModel, 0, 0, 0.5, 1, 0.5, "least_squares", 0, 0.9, out row1, out column1, out angle1, out score1);


                return true;
            }
            catch (Exception ee)
            {
                message = ee.ToString();
                return false;
            }

        }

        private void HObjectToHImage(HObject obj, ref HImage img)
        {
            HTuple pointer, type, width, heght;
            HOperatorSet.GetImagePointer1(obj, out pointer, out type, out width, out heght);
            img.GenImage1(type, width, heght, pointer);
        }


        public void SetOrgin(double x, double y)
        {
            myShapeModel.SetShapeModelOrigin(x, y);
        }

    }
}
