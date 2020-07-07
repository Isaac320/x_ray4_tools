using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HalconDotNet;

namespace P1_CMMT.VisionTools.MacthTool
{
    public partial class MatchToolCtr : UserControl
    {
        HDrawingObject rectTrain = null;
        HDrawingObject rectSearch = null;
        HDrawingObject rectMask = null;

        HTuple rectParams = new HTuple("row1", "column1", "row2", "column2");

        MatchTool tool;
        public MatchToolCtr()
        {
            InitializeComponent();
        }

        public MatchToolCtr(MatchTool tool)
        {
            this.tool = tool;
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            HTuple htemp1 = new HTuple(rectTrain.GetDrawingObjectParams(rectParams));
            tool.trainRect = new double[4] { htemp1[0], htemp1[1], htemp1[2], htemp1[3] };

            HTuple htemp2 = new HTuple(rectSearch.GetDrawingObjectParams(rectParams));
            tool.searchRect = new double[4] { htemp2[0], htemp2[1], htemp2[2], htemp2[3] };

            HTuple htemp3 = new HTuple(rectMask.GetDrawingObjectParams(rectParams));
            tool.maskRect = new double[4] { htemp3[0], htemp3[1], htemp3[2], htemp3[3] };

            tool.Train();

            HTuple w = htemp1[2] - htemp1[0];
            HTuple h = htemp1[3] - htemp1[1];

            //HTuple centerX = (htemp1[2] + htemp1[0]) / 2;
            //HTuple centerY = (htemp1[3] + htemp1[1]) / 2;
            //tool.SetOrgin(-centerX, -centerY);
            //hSmartWindowControl1.HalconWindow.SetColor("red");
            //hSmartWindowControl1.HalconWindow.DispObj(tool.Xld);

            hSmartWindowControl2.HalconWindow.SetPart(-w / 2, -h / 2, w / 2, h / 2);

            hSmartWindowControl2.HalconWindow.ClearWindow();
            hSmartWindowControl2.HalconWindow.SetColor("red");
            hSmartWindowControl2.HalconWindow.DispObj(tool.Xld);

        }

        private void MatchToolCtr_Load(object sender, EventArgs e)
        {
            rectTrain = new HDrawingObject(tool.trainRect[0], tool.trainRect[1], tool.trainRect[2], tool.trainRect[3]);
            rectTrain.SetDrawingObjectParams("color", "yellow");

            rectSearch = new HDrawingObject(tool.searchRect[0], tool.searchRect[1], tool.searchRect[2], tool.searchRect[3]);
            rectSearch.SetDrawingObjectParams("color", "green");

            rectMask = new HDrawingObject(tool.maskRect[0], tool.maskRect[1], tool.maskRect[2], tool.maskRect[3]);
            rectMask.SetDrawingObjectParams("color", "red");

            DrawTrainRegions();


            if (tool.Image != null)
            {
                hSmartWindowControl1.HalconWindow.DispObj(tool.Image);
            }

            txtbox_minscore.Text = tool.minScore.ToString();
            txt_numMatch.Text = tool.numMatches.ToString();
        }

        private void DrawTrainRegions()
        {
            hSmartWindowControl1.HalconWindow.AttachDrawingObjectToWindow(rectTrain);
            hSmartWindowControl1.HalconWindow.AttachDrawingObjectToWindow(rectSearch);
            hSmartWindowControl1.HalconWindow.AttachDrawingObjectToWindow(rectMask);
        }


        private void Search()
        {
            tool.Regions.Clear();
            listBox1.Items.Clear();
            hSmartWindowControl1.HalconWindow.ClearWindow();
            hSmartWindowControl1.HalconWindow.DispImage(tool.himage);
            DrawTrainRegions();

            tool.minScore = double.Parse(txtbox_minscore.Text);
            tool.numMatches = int.Parse(txt_numMatch.Text);
            tool.Run();
            int num = tool.Score.Length;
            for (int i = 0; i < num; i++)
            {
                HXLDCont cross = new HXLDCont();
                cross.GenCrossContourXld(tool.Row[i].D, tool.Column[i].D, 66, 0);
                hSmartWindowControl1.HalconWindow.DispXld(cross);
                listBox1.Items.Add("Score" + (i + 1).ToString() + ":" + tool.Score[i].D.ToString());
                HHomMat2D homMat2D = new HHomMat2D();
                homMat2D.VectorAngleToRigid(tool.Row[0].D, tool.Column[0].D, 0, tool.Row[i].D, tool.Column[i].D, 0);
                HRegion rectange = new HRegion(tool.searchRect[0], tool.searchRect[1], tool.searchRect[2], tool.searchRect[3]);
                HRegion rect2 = rectange.AffineTransRegion(homMat2D, "nearest_neighbor");
                tool.Regions.Add(rect2);
            }

            foreach (HRegion r in tool.Regions)
            {
                hSmartWindowControl1.HalconWindow.SetColor("green");
                hSmartWindowControl1.HalconWindow.SetDraw("margin");
                hSmartWindowControl1.HalconWindow.DispRegion(r);
            }
        }

        private void hSmartWindowControl1_Load(object sender, EventArgs e)
        {
            hSmartWindowControl1.MouseWheel += hSmartWindowControl1.HSmartWindowControl_MouseWheel;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void hSmartWindowControl2_Load(object sender, EventArgs e)
        {
            hSmartWindowControl2.MouseWheel += hSmartWindowControl2.HSmartWindowControl_MouseWheel;
        }
    }
}
