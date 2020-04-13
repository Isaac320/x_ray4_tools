namespace P1_CMMT
{
    partial class EndTrayFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.lb_lot = new System.Windows.Forms.Label();
            this.lb_InFrame = new System.Windows.Forms.Label();
            this.lb_reject = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lb_noProcess = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(255, 455);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 67);
            this.button1.TabIndex = 0;
            this.button1.Text = "继续";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lb_lot
            // 
            this.lb_lot.AutoSize = true;
            this.lb_lot.Location = new System.Drawing.Point(29, 9);
            this.lb_lot.Name = "lb_lot";
            this.lb_lot.Size = new System.Drawing.Size(75, 21);
            this.lb_lot.TabIndex = 1;
            this.lb_lot.Text = "Lot : 123";
            // 
            // lb_InFrame
            // 
            this.lb_InFrame.AutoSize = true;
            this.lb_InFrame.Location = new System.Drawing.Point(29, 43);
            this.lb_InFrame.Name = "lb_InFrame";
            this.lb_InFrame.Size = new System.Drawing.Size(199, 21);
            this.lb_InFrame.TabIndex = 2;
            this.lb_InFrame.Text = "Inspected Frames： 1/16";
            // 
            // lb_reject
            // 
            this.lb_reject.AutoSize = true;
            this.lb_reject.Location = new System.Drawing.Point(29, 77);
            this.lb_reject.Name = "lb_reject";
            this.lb_reject.Size = new System.Drawing.Size(184, 21);
            this.lb_reject.TabIndex = 3;
            this.lb_reject.Text = "Found reject units: 199";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 21;
            this.listBox1.Location = new System.Drawing.Point(33, 112);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(284, 277);
            this.listBox1.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 415);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(345, 21);
            this.label4.TabIndex = 5;
            this.label4.Text = "Please move to the tray to the reject station";
            // 
            // lb_noProcess
            // 
            this.lb_noProcess.AutoSize = true;
            this.lb_noProcess.ForeColor = System.Drawing.Color.Red;
            this.lb_noProcess.Location = new System.Drawing.Point(33, 473);
            this.lb_noProcess.Name = "lb_noProcess";
            this.lb_noProcess.Size = new System.Drawing.Size(202, 21);
            this.lb_noProcess.TabIndex = 6;
            this.lb_noProcess.Text = "托盘未处理，请处理完继续";
            this.lb_noProcess.Visible = false;
            // 
            // EndTrayFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(408, 543);
            this.Controls.Add(this.lb_noProcess);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.lb_reject);
            this.Controls.Add(this.lb_InFrame);
            this.Controls.Add(this.lb_lot);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EndTrayFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "托盘完成";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lb_lot;
        private System.Windows.Forms.Label lb_InFrame;
        private System.Windows.Forms.Label lb_reject;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lb_noProcess;
    }
}