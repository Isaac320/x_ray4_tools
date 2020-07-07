namespace P1_CMMT.VisionTools.MacthTool
{
    partial class MatchToolCtr
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.hSmartWindowControl1 = new HalconDotNet.HSmartWindowControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.hSmartWindowControl2 = new HalconDotNet.HSmartWindowControl();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_numMatch = new System.Windows.Forms.TextBox();
            this.txtbox_minscore = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.21706F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.78295F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1500, 900);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.hSmartWindowControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1167, 894);
            this.panel1.TabIndex = 0;
            // 
            // hSmartWindowControl1
            // 
            this.hSmartWindowControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.hSmartWindowControl1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.hSmartWindowControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hSmartWindowControl1.HDoubleClickToFitContent = true;
            this.hSmartWindowControl1.HDrawingObjectsModifier = HalconDotNet.HSmartWindowControl.DrawingObjectsModifier.None;
            this.hSmartWindowControl1.HImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hSmartWindowControl1.HKeepAspectRatio = true;
            this.hSmartWindowControl1.HMoveContent = true;
            this.hSmartWindowControl1.HZoomContent = HalconDotNet.HSmartWindowControl.ZoomContent.WheelForwardZoomsIn;
            this.hSmartWindowControl1.Location = new System.Drawing.Point(0, 0);
            this.hSmartWindowControl1.Margin = new System.Windows.Forms.Padding(0);
            this.hSmartWindowControl1.Name = "hSmartWindowControl1";
            this.hSmartWindowControl1.Size = new System.Drawing.Size(1167, 894);
            this.hSmartWindowControl1.TabIndex = 0;
            this.hSmartWindowControl1.WindowSize = new System.Drawing.Size(1167, 894);
            this.hSmartWindowControl1.Load += new System.EventHandler(this.hSmartWindowControl1_Load);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.listBox1);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.hSmartWindowControl2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(1176, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(321, 894);
            this.panel2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(52, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 41);
            this.label1.TabIndex = 0;
            this.label1.Text = "匹配定位工具";
            // 
            // hSmartWindowControl2
            // 
            this.hSmartWindowControl2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.hSmartWindowControl2.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.hSmartWindowControl2.HDoubleClickToFitContent = true;
            this.hSmartWindowControl2.HDrawingObjectsModifier = HalconDotNet.HSmartWindowControl.DrawingObjectsModifier.None;
            this.hSmartWindowControl2.HImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hSmartWindowControl2.HKeepAspectRatio = true;
            this.hSmartWindowControl2.HMoveContent = true;
            this.hSmartWindowControl2.HZoomContent = HalconDotNet.HSmartWindowControl.ZoomContent.WheelForwardZoomsIn;
            this.hSmartWindowControl2.Location = new System.Drawing.Point(21, 75);
            this.hSmartWindowControl2.Margin = new System.Windows.Forms.Padding(0);
            this.hSmartWindowControl2.Name = "hSmartWindowControl2";
            this.hSmartWindowControl2.Size = new System.Drawing.Size(272, 187);
            this.hSmartWindowControl2.TabIndex = 1;
            this.hSmartWindowControl2.WindowSize = new System.Drawing.Size(272, 187);
            this.hSmartWindowControl2.Load += new System.EventHandler(this.hSmartWindowControl2_Load);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(94, 285);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 49);
            this.button1.TabIndex = 2;
            this.button1.Text = "Train";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_numMatch);
            this.groupBox1.Controls.Add(this.txtbox_minscore);
            this.groupBox1.Location = new System.Drawing.Point(32, 375);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(247, 127);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "参数";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "numMatches";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "minScore";
            // 
            // txt_numMatch
            // 
            this.txt_numMatch.Location = new System.Drawing.Point(96, 78);
            this.txt_numMatch.Name = "txt_numMatch";
            this.txt_numMatch.Size = new System.Drawing.Size(100, 21);
            this.txt_numMatch.TabIndex = 13;
            // 
            // txtbox_minscore
            // 
            this.txtbox_minscore.Location = new System.Drawing.Point(96, 39);
            this.txtbox_minscore.Name = "txtbox_minscore";
            this.txtbox_minscore.Size = new System.Drawing.Size(100, 21);
            this.txtbox_minscore.TabIndex = 12;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(94, 523);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(102, 49);
            this.button2.TabIndex = 4;
            this.button2.Text = "Search";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(32, 596);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(247, 268);
            this.listBox1.TabIndex = 14;
            // 
            // MatchToolCtr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MatchToolCtr";
            this.Size = new System.Drawing.Size(1500, 900);
            this.Load += new System.EventHandler(this.MatchToolCtr_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private HalconDotNet.HSmartWindowControl hSmartWindowControl1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_numMatch;
        private System.Windows.Forms.TextBox txtbox_minscore;
        private System.Windows.Forms.Button button1;
        private HalconDotNet.HSmartWindowControl hSmartWindowControl2;
        private System.Windows.Forms.Label label1;
    }
}
