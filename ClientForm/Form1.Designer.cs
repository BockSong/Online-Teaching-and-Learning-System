namespace ClientForm
{
    partial class Form1
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
            try
            {
                base.Dispose(disposing);
            }
            catch
            { }
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cameraConnector1 = new OMCS.Passive.Video.CameraConnector();
            this.whiteBoardConnector1 = new OMCS.Passive.WhiteBoard.WhiteBoardConnector();
            this.NCsplitContainer = new System.Windows.Forms.SplitContainer();
            this.label5 = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            this.NCskinListBox = new CCWin.SkinControl.SkinListBox();
            ((System.ComponentModel.ISupportInitialize)(this.NCsplitContainer)).BeginInit();
            this.NCsplitContainer.Panel1.SuspendLayout();
            this.NCsplitContainer.Panel2.SuspendLayout();
            this.NCsplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 332);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "init";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(7, 382);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "0000";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(105, 382);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "1501";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(103, 337);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "port:";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(7, 420);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "1502";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(7, 451);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 5;
            this.button5.Text = "createroom";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(105, 451);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 6;
            this.button6.Text = "joinroom";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 571);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "label2";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(9, 480);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(73, 21);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = "TestRoom";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(105, 480);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(75, 21);
            this.textBox2.TabIndex = 9;
            this.textBox2.Text = "1234";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(103, 420);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "id:";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(7, 507);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 11;
            this.button7.Text = "exitroom";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(103, 512);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "roomid:";
            // 
            // cameraConnector1
            // 
            this.cameraConnector1.AutoSynchronizeVideoToAudio = true;
            this.cameraConnector1.BackColor = System.Drawing.Color.White;
            this.cameraConnector1.DisplayVideoParameters = false;
            this.cameraConnector1.Dock = System.Windows.Forms.DockStyle.Top;
            this.cameraConnector1.Location = new System.Drawing.Point(0, 0);
            this.cameraConnector1.Name = "cameraConnector1";
            this.cameraConnector1.Size = new System.Drawing.Size(334, 309);
            this.cameraConnector1.TabIndex = 13;
            this.cameraConnector1.WaitOwnerOnlineSpanInSecs = 0;
            // 
            // whiteBoardConnector1
            // 
            this.whiteBoardConnector1.AutoReconnect = true;
            this.whiteBoardConnector1.BackImageOfPage = null;
            this.whiteBoardConnector1.ContextMenuEnglish = false;
            this.whiteBoardConnector1.CoursewareEnabled = true;
            this.whiteBoardConnector1.Cursor = System.Windows.Forms.Cursors.No;
            this.whiteBoardConnector1.DisplayPageBorder = false;
            this.whiteBoardConnector1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.whiteBoardConnector1.FocusOnNewViewByOther = true;
            this.whiteBoardConnector1.IsManager = true;
            this.whiteBoardConnector1.Location = new System.Drawing.Point(0, 0);
            this.whiteBoardConnector1.MinimumSize = new System.Drawing.Size(530, 200);
            this.whiteBoardConnector1.Name = "whiteBoardConnector1";
            this.whiteBoardConnector1.PageSize = new System.Drawing.Size(800, 600);
            this.whiteBoardConnector1.Size = new System.Drawing.Size(666, 624);
            this.whiteBoardConnector1.TabIndex = 14;
            this.whiteBoardConnector1.Timeout4LoadContent = 120;
            this.whiteBoardConnector1.ToolBarVisiable = true;
            this.whiteBoardConnector1.WaitOwnerOnlineSpanInSecs = 0;
            this.whiteBoardConnector1.WatchingOnly = false;
            this.whiteBoardConnector1.ZoomFactor = OMCS.Passive.WhiteBoard.WhiteBoardZoomFactor.Percent100;
            // 
            // NCsplitContainer
            // 
            this.NCsplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NCsplitContainer.Location = new System.Drawing.Point(0, 0);
            this.NCsplitContainer.Name = "NCsplitContainer";
            // 
            // NCsplitContainer.Panel1
            // 
            this.NCsplitContainer.Panel1.Controls.Add(this.label5);
            this.NCsplitContainer.Panel1.Controls.Add(this.button8);
            this.NCsplitContainer.Panel1.Controls.Add(this.NCskinListBox);
            this.NCsplitContainer.Panel1.Controls.Add(this.button3);
            this.NCsplitContainer.Panel1.Controls.Add(this.cameraConnector1);
            this.NCsplitContainer.Panel1.Controls.Add(this.label4);
            this.NCsplitContainer.Panel1.Controls.Add(this.button1);
            this.NCsplitContainer.Panel1.Controls.Add(this.button7);
            this.NCsplitContainer.Panel1.Controls.Add(this.button2);
            this.NCsplitContainer.Panel1.Controls.Add(this.label3);
            this.NCsplitContainer.Panel1.Controls.Add(this.label1);
            this.NCsplitContainer.Panel1.Controls.Add(this.textBox2);
            this.NCsplitContainer.Panel1.Controls.Add(this.button4);
            this.NCsplitContainer.Panel1.Controls.Add(this.textBox1);
            this.NCsplitContainer.Panel1.Controls.Add(this.button5);
            this.NCsplitContainer.Panel1.Controls.Add(this.label2);
            this.NCsplitContainer.Panel1.Controls.Add(this.button6);
            // 
            // NCsplitContainer.Panel2
            // 
            this.NCsplitContainer.Panel2.Controls.Add(this.whiteBoardConnector1);
            this.NCsplitContainer.Size = new System.Drawing.Size(1004, 624);
            this.NCsplitContainer.SplitterDistance = 334;
            this.NCsplitContainer.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 362);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 16;
            this.label5.Text = "一键登录";
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(7, 536);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 15;
            this.button8.Text = "举手发言";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // NCskinListBox
            // 
            this.NCskinListBox.Back = null;
            this.NCskinListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.NCskinListBox.FormattingEnabled = true;
            this.NCskinListBox.Location = new System.Drawing.Point(186, 332);
            this.NCskinListBox.Name = "NCskinListBox";
            this.NCskinListBox.Size = new System.Drawing.Size(145, 199);
            this.NCskinListBox.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 624);
            this.Controls.Add(this.NCsplitContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.NCsplitContainer.Panel1.ResumeLayout(false);
            this.NCsplitContainer.Panel1.PerformLayout();
            this.NCsplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NCsplitContainer)).EndInit();
            this.NCsplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label4;
        private OMCS.Passive.Video.CameraConnector cameraConnector1;
        private OMCS.Passive.WhiteBoard.WhiteBoardConnector whiteBoardConnector1;
        private System.Windows.Forms.SplitContainer NCsplitContainer;
        private CCWin.SkinControl.SkinListBox NCskinListBox;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label5;
    }
}

