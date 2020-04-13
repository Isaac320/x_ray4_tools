using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using ImageProcess;

namespace ProcessDll
{
    public class Class1 : IImageProcess
    {
        HTuple hv_ModelIDOut;
        HTuple hv_flag;
        public bool Init()
        {
            //throw new NotImplementedException();
            // Local iconic variables 

            // Local control variables 

            HTuple hv_ModelID1 = null, hv_ModelID2 = null;
            HTuple hv_ModelID3 = null, hv_ModelID4 = null;
            // Initialize local and output iconic variables 
            //HOperatorSet.ReadShapeModel("D:/scindtec/STS/simulation/halcon/ModelID1", out hv_ModelID1);
            //HOperatorSet.ReadShapeModel("D:/scindtec/STS/simulation/halcon/ModelID2", out hv_ModelID2);
            //HOperatorSet.ReadShapeModel("D:/scindtec/STS/simulation/halcon/ModelID3", out hv_ModelID3);
            HOperatorSet.ReadShapeModel("D:/scindtec/STS/simulation/halcon/ModelID4", out hv_ModelID4);
            hv_ModelIDOut = hv_ModelID4.Clone();

            return true;


        }

        public bool Process(HImage hImage, HObject region, out HObject xld, out int index, out string message)
        {

            // Local iconic variables 

            HObject xld2 = null;
            HObject ho_Contours = null;
            HObject ho_SelectedRegions = null;
            // Local control variables 


            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out xld2);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_Contours);

            // throw new NotImplementedException();



            findtemp(hImage, region, out HObject ho_GrayImage,
     out ho_Contours, hv_ModelIDOut, out HTuple hv_Row, out HTuple hv_Column,
     out HTuple hv_flag, out HTuple hv_string);
            if ((int)(new HTuple(hv_flag.TupleEqual(1))) != 0)
            {

                ho_Contours.Dispose();
                ForeignMaterial(ho_GrayImage, out ho_Contours, hv_Row, hv_Column, out hv_string,
                    out hv_flag);
            }
            else
            {
                message = hv_string;
                index = (int)hv_flag;
                xld = ho_Contours;
                return false;
            }
            if ((int)(new HTuple(hv_flag.TupleEqual(1))) != 0)
            {
                ho_Contours.Dispose(); ho_SelectedRegions.Dispose();
                detectNoWire(ho_GrayImage, out ho_Contours, out ho_SelectedRegions, hv_Row,
                    hv_Column, out hv_string, out hv_flag);
            }
            else
            {
                message = hv_string;
                index = (int)hv_flag;
                xld = ho_Contours;
                return false;
            }
            if ((int)(new HTuple(hv_flag.TupleEqual(1))) != 0)
            {
                message = "OK";
                index = 1;
                xld = xld2;
                return true;
            }
            else
            {
                message = hv_string;
                index = (int)hv_flag;
                xld = ho_Contours;
                return false;
            }




        }
        public void findtemp(HObject ho_Image, HObject ho_ROI, out HObject ho_GrayImage,
           out HObject ho_Contours, HTuple hv_ModelIDOut, out HTuple hv_Row, out HTuple hv_Column,
           out HTuple hv_flag, out HTuple hv_string)
        {




            // Local iconic variables 

            HObject ho_Image_Cut;

            // Local control variables 

            HTuple hv_Angle = null, hv_ScaleR = null, hv_ScaleC = null;
            HTuple hv_Score = null, hv_Model = null, hv_num = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_GrayImage);
            HOperatorSet.GenEmptyObj(out ho_Contours);
            HOperatorSet.GenEmptyObj(out ho_Image_Cut);
            hv_flag = new HTuple();
            hv_string = new HTuple();
            ho_Image_Cut.Dispose();
            HOperatorSet.ReduceDomain(ho_Image, ho_ROI, out ho_Image_Cut);
            ho_GrayImage.Dispose();
            HOperatorSet.Rgb1ToGray(ho_Image_Cut, out ho_GrayImage);
            HOperatorSet.FindAnisoShapeModels(ho_GrayImage, hv_ModelIDOut, -1.57, 3.14, 0.6,
                1.2, 0.6, 1.21, 0.5, 1, 0.5, "least_squares", 0, 0.9, out hv_Row, out hv_Column,
                out hv_Angle, out hv_ScaleR, out hv_ScaleC, out hv_Score, out hv_Model);
            ho_Contours.Dispose();
            HOperatorSet.GenContourRegionXld(ho_ROI, out ho_Contours, "border");
            hv_num = new HTuple(hv_Score.TupleLength());
            if ((int)(new HTuple(hv_num.TupleEqual(1))) != 0)
            {
                hv_flag = 1;
            }
            else
            {
                hv_flag = 4;
                hv_string = "Find Pattern Wrong";
            }
            ho_Image_Cut.Dispose();

            return;
        }
        public void detectNoWire(HObject ho_GrayImage, out HObject ho_Contours, out HObject ho_SelectedRegions,
            HTuple hv_Row, HTuple hv_Column, out HTuple hv_string, out HTuple hv_flag)
        {




            // Local iconic variables 

            HObject ho_Rectangle_FoV, ho_ImageReduced_Fov;
            HObject ho_Characters, ho_ConnectedRegions, ho_RegionUnion;

            // Local control variables 

            HTuple hv_Row_st = null, hv_Col_st = null;
            HTuple hv_Threshold = null, hv_Area = null, hv_Row3 = null;
            HTuple hv_Column3 = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Contours);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_Rectangle_FoV);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced_Fov);
            HOperatorSet.GenEmptyObj(out ho_Characters);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionUnion);
            hv_string = "";
            hv_flag = 0;
            hv_Row_st = hv_Row.Clone();
            hv_Col_st = hv_Column.Clone();
            ho_Rectangle_FoV.Dispose();
            HOperatorSet.GenRectangle1(out ho_Rectangle_FoV, hv_Row_st - 20, hv_Col_st - 20,
                hv_Row_st + 90, hv_Col_st + 70);
            ho_ImageReduced_Fov.Dispose();
            HOperatorSet.ReduceDomain(ho_GrayImage, ho_Rectangle_FoV, out ho_ImageReduced_Fov
                );
            //detect No Wire
            ho_Characters.Dispose();
            HOperatorSet.CharThreshold(ho_ImageReduced_Fov, ho_Rectangle_FoV, out ho_Characters,
                2, 90, out hv_Threshold);
            ho_ConnectedRegions.Dispose();
            HOperatorSet.Connection(ho_Characters, out ho_ConnectedRegions);
            ho_SelectedRegions.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                "and", 50, 99999);
            ho_RegionUnion.Dispose();
            HOperatorSet.Union1(ho_SelectedRegions, out ho_RegionUnion);

            HOperatorSet.AreaCenter(ho_RegionUnion, out hv_Area, out hv_Row3, out hv_Column3);

            if ((int)(new HTuple(hv_Area.TupleLess(200))) != 0)
            {
                ho_Contours.Dispose();
                HOperatorSet.GenContourRegionXld(ho_Rectangle_FoV, out ho_Contours, "border");
                hv_string = "No Wire";
                hv_flag = 3;

                //flag=3:NoWire
            }
            else
            {
                hv_flag = 1;

            }


            ho_Rectangle_FoV.Dispose();
            ho_ImageReduced_Fov.Dispose();
            ho_Characters.Dispose();
            ho_ConnectedRegions.Dispose();
            ho_RegionUnion.Dispose();

            return;
        }
        public void ForeignMaterial(HObject ho_GrayImage, out HObject ho_Contours, HTuple hv_Row,
          HTuple hv_Column, out HTuple hv_string, out HTuple hv_flag)
        {




            // Local iconic variables 

            HObject ho_Rectangle_cut, ho_ImageReduced_cut;
            HObject ho_Region, ho_RegionErosion1, ho_ConnectedRegions1;
            HObject ho_RegionFillUp2, ho_SelectedRegions, ho_RegionDilation1;
            HObject ho_RegionFillUp, ho_RegionErosion, ho_ConnectedRegions;
            HObject ho_SelectedRegions1, ho_RegionUnion, ho_ImageReduced1;
            HObject ho_Region1;

            // Local control variables 

            HTuple hv_Row_st = null, hv_Col_st = null;
            HTuple hv_UsedThreshold = null, hv_Area = null, hv_Row1 = null;
            HTuple hv_Column1 = null, hv_area = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Contours);
            HOperatorSet.GenEmptyObj(out ho_Rectangle_cut);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced_cut);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_RegionErosion1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp2);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionDilation1);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_RegionErosion);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_RegionUnion);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced1);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            hv_string = "";
            hv_flag = 0;
            hv_Row_st = hv_Row.Clone();
            hv_Col_st = hv_Column.Clone();
            ho_Rectangle_cut.Dispose();
            HOperatorSet.GenRectangle1(out ho_Rectangle_cut, hv_Row_st - 150, hv_Col_st - 200,
                hv_Row_st + 180, hv_Col_st + 230);
            ho_ImageReduced_cut.Dispose();
            HOperatorSet.ReduceDomain(ho_GrayImage, ho_Rectangle_cut, out ho_ImageReduced_cut
                );
            ho_Region.Dispose();
            HOperatorSet.BinaryThreshold(ho_ImageReduced_cut, out ho_Region, "max_separability",
                "light", out hv_UsedThreshold);
            ho_RegionErosion1.Dispose();
            HOperatorSet.ErosionRectangle1(ho_Region, out ho_RegionErosion1, 5, 1);
            ho_ConnectedRegions1.Dispose();
            HOperatorSet.Connection(ho_RegionErosion1, out ho_ConnectedRegions1);
            ho_RegionFillUp2.Dispose();
            HOperatorSet.FillUp(ho_ConnectedRegions1, out ho_RegionFillUp2);
            ho_SelectedRegions.Dispose();
            HOperatorSet.SelectShape(ho_RegionFillUp2, out ho_SelectedRegions, (new HTuple("width")).TupleConcat(
                "area"), "and", (new HTuple(100)).TupleConcat(2000), (new HTuple(400)).TupleConcat(
                999999));
            ho_RegionDilation1.Dispose();
            HOperatorSet.DilationCircle(ho_SelectedRegions, out ho_RegionDilation1, 3);
            ho_RegionFillUp.Dispose();
            HOperatorSet.FillUp(ho_RegionDilation1, out ho_RegionFillUp);
            ho_RegionErosion.Dispose();
            HOperatorSet.ErosionCircle(ho_RegionFillUp, out ho_RegionErosion, 7);
            ho_ConnectedRegions.Dispose();
            HOperatorSet.Connection(ho_RegionErosion, out ho_ConnectedRegions);
            ho_SelectedRegions1.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions1, (new HTuple("width")).TupleConcat(
                "area"), "and", (new HTuple(100)).TupleConcat(2000), (new HTuple(400)).TupleConcat(
                999999));
            ho_RegionUnion.Dispose();
            HOperatorSet.Union1(ho_SelectedRegions1, out ho_RegionUnion);
            ho_ImageReduced1.Dispose();
            HOperatorSet.ReduceDomain(ho_GrayImage, ho_RegionUnion, out ho_ImageReduced1);
            ho_Region1.Dispose();
            HOperatorSet.Threshold(ho_ImageReduced1, out ho_Region1, 0, 40);
            HOperatorSet.AreaCenter(ho_Region1, out hv_Area, out hv_Row1, out hv_Column1);
            ho_Contours.Dispose();
            HOperatorSet.GenContourRegionXld(ho_Region1, out ho_Contours, "border");
            hv_area = hv_Area.TupleSum();
            if ((int)(new HTuple(hv_area.TupleGreater(100))) != 0)
            {
                hv_string = "Foreign material";
                hv_flag = 2;

                //错误2：外来物检测
            }
            else
            {
                hv_flag = 1;
            }
            ho_Rectangle_cut.Dispose();
            ho_ImageReduced_cut.Dispose();
            ho_Region.Dispose();
            ho_RegionErosion1.Dispose();
            ho_ConnectedRegions1.Dispose();
            ho_RegionFillUp2.Dispose();
            ho_SelectedRegions.Dispose();
            ho_RegionDilation1.Dispose();
            ho_RegionFillUp.Dispose();
            ho_RegionErosion.Dispose();
            ho_ConnectedRegions.Dispose();
            ho_SelectedRegions1.Dispose();
            ho_RegionUnion.Dispose();
            ho_ImageReduced1.Dispose();
            ho_Region1.Dispose();

            return;
        }
    }

}

