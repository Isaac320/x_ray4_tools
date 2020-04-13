using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P1_CMMT
{
    //class ReadDataMatrix
    //{
    //    // Procedures 
    //    public static void ReadData(HObject ho_Image, out HObject ho_SymbolXLDs, out HTuple hv_flag,
    //        out HTuple hv_DecodedDataStrings, out HTuple hv_Row, out HTuple hv_Column)
    //    {



    //        // Local iconic variables 

    //        HObject ho_GrayImage1 = null;

    //        // Local control variables 

    //        HTuple hv_Grayval = null, hv_channels = null;
    //        HTuple hv_DataCodeHandle = null, hv_ResultHandles = null;
    //        HTuple hv_Area = new HTuple(), hv_PointOrder = new HTuple();
    //        // Initialize local and output iconic variables 
    //        HOperatorSet.GenEmptyObj(out ho_SymbolXLDs);
    //        HOperatorSet.GenEmptyObj(out ho_GrayImage1);
    //        hv_Row = new HTuple();
    //        hv_Column = new HTuple();
    //        hv_flag = 0;
    //        HOperatorSet.GetGrayval(ho_Image, 0, 0, out hv_Grayval);
    //        hv_channels = new HTuple(hv_Grayval.TupleLength());
    //        if ((int)(new HTuple(hv_channels.TupleNotEqual(1))) != 0)
    //        {                
    //            ho_GrayImage1.Dispose();
    //            HOperatorSet.Rgb1ToGray(ho_Image, out ho_GrayImage1);
    //        }
    //        else
    //        {
    //            ho_GrayImage1.Dispose();
    //            HOperatorSet.CopyImage(ho_Image, out ho_GrayImage1);
    //        }
    //        HOperatorSet.CreateDataCode2dModel("Data Matrix ECC 200", "default_parameters",
    //            "enhanced_recognition", out hv_DataCodeHandle);
    //        ho_SymbolXLDs.Dispose();
    //        HOperatorSet.FindDataCode2d(ho_GrayImage1, out ho_SymbolXLDs, hv_DataCodeHandle,
    //            new HTuple(), new HTuple(), out hv_ResultHandles, out hv_DecodedDataStrings);
    //        HOperatorSet.ClearDataCode2dModel(hv_DataCodeHandle);



    //        if ((int)(new HTuple((new HTuple(hv_DecodedDataStrings.TupleLength())).TupleNotEqual(
    //            0))) != 0)
    //        {
    //            hv_flag = 1;
    //            HOperatorSet.AreaCenterXld(ho_SymbolXLDs, out hv_Area, out hv_Row, out hv_Column,
    //                out hv_PointOrder);
    //        }
    //        else
    //        {
    //            hv_flag = 0;
    //        }
    //        ho_GrayImage1.Dispose();

    //        return;
    //    }
    //}
}
