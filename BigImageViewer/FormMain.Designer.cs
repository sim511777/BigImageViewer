namespace BigImageViewer {
    partial class FormMain {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent() {
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkDrawFrame = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkDrawPixelValue = new System.Windows.Forms.CheckBox();
            this.cbxImageBufferDrawer = new System.Windows.Forms.ComboBox();
            this.chkDrawInfo = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxBmpLoader = new System.Windows.Forms.ComboBox();
            this.btnAlloc = new System.Windows.Forms.Button();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.tbxFwdDir = new System.Windows.Forms.TextBox();
            this.btnResetZoom = new System.Windows.Forms.Button();
            this.numFNum = new System.Windows.Forms.NumericUpDown();
            this.btnFwdDir = new System.Windows.Forms.Button();
            this.btnLoadFwd = new System.Windows.Forms.Button();
            this.numFH = new System.Windows.Forms.NumericUpDown();
            this.numFW = new System.Windows.Forms.NumericUpDown();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.tbxLog = new System.Windows.Forms.TextBox();
            this.pbxDraw = new System.Windows.Forms.PictureBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.dlgFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.chkDrawHoles = new System.Windows.Forms.CheckBox();
            this.btnInitHoles = new System.Windows.Forms.Button();
            this.numHolePitchX = new System.Windows.Forms.NumericUpDown();
            this.numHolePitchY = new System.Windows.Forms.NumericUpDown();
            this.numHoleW = new System.Windows.Forms.NumericUpDown();
            this.numHoleH = new System.Windows.Forms.NumericUpDown();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDraw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHolePitchX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHolePitchY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHoleW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHoleH)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.splitter2);
            this.panel1.Controls.Add(this.tbxLog);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(564, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(435, 600);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.cbxBmpLoader);
            this.panel2.Controls.Add(this.btnInitHoles);
            this.panel2.Controls.Add(this.btnAlloc);
            this.panel2.Controls.Add(this.btnClearLog);
            this.panel2.Controls.Add(this.tbxFwdDir);
            this.panel2.Controls.Add(this.btnResetZoom);
            this.panel2.Controls.Add(this.numFNum);
            this.panel2.Controls.Add(this.btnFwdDir);
            this.panel2.Controls.Add(this.btnLoadFwd);
            this.panel2.Controls.Add(this.numHoleH);
            this.panel2.Controls.Add(this.numHolePitchY);
            this.panel2.Controls.Add(this.numFH);
            this.panel2.Controls.Add(this.numHoleW);
            this.panel2.Controls.Add(this.numHolePitchX);
            this.panel2.Controls.Add(this.numFW);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(435, 411);
            this.panel2.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkDrawFrame);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.chkDrawPixelValue);
            this.groupBox1.Controls.Add(this.cbxImageBufferDrawer);
            this.groupBox1.Controls.Add(this.chkDrawHoles);
            this.groupBox1.Controls.Add(this.chkDrawInfo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(435, 70);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Draw Option";
            // 
            // chkDrawFrame
            // 
            this.chkDrawFrame.AutoSize = true;
            this.chkDrawFrame.Checked = true;
            this.chkDrawFrame.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDrawFrame.Location = new System.Drawing.Point(6, 20);
            this.chkDrawFrame.Name = "chkDrawFrame";
            this.chkDrawFrame.Size = new System.Drawing.Size(93, 16);
            this.chkDrawFrame.TabIndex = 5;
            this.chkDrawFrame.Text = "Draw Frame";
            this.chkDrawFrame.UseVisualStyleBackColor = true;
            this.chkDrawFrame.CheckedChanged += new System.EventHandler(this.chkDrawFrame_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "Image Buffer Drawer";
            // 
            // chkDrawPixelValue
            // 
            this.chkDrawPixelValue.AutoSize = true;
            this.chkDrawPixelValue.Checked = true;
            this.chkDrawPixelValue.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDrawPixelValue.Location = new System.Drawing.Point(105, 20);
            this.chkDrawPixelValue.Name = "chkDrawPixelValue";
            this.chkDrawPixelValue.Size = new System.Drawing.Size(121, 16);
            this.chkDrawPixelValue.TabIndex = 5;
            this.chkDrawPixelValue.Text = "Draw Pixel Value";
            this.chkDrawPixelValue.UseVisualStyleBackColor = true;
            this.chkDrawPixelValue.CheckedChanged += new System.EventHandler(this.chkDrawFrame_CheckedChanged);
            // 
            // cbxImageBufferDrawer
            // 
            this.cbxImageBufferDrawer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxImageBufferDrawer.FormattingEnabled = true;
            this.cbxImageBufferDrawer.Items.AddRange(new object[] {
            "C",
            ".NET Unsafe",
            ".NET Marshal"});
            this.cbxImageBufferDrawer.Location = new System.Drawing.Point(132, 42);
            this.cbxImageBufferDrawer.Name = "cbxImageBufferDrawer";
            this.cbxImageBufferDrawer.Size = new System.Drawing.Size(121, 20);
            this.cbxImageBufferDrawer.TabIndex = 6;
            // 
            // chkDrawInfo
            // 
            this.chkDrawInfo.AutoSize = true;
            this.chkDrawInfo.Checked = true;
            this.chkDrawInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDrawInfo.Location = new System.Drawing.Point(232, 20);
            this.chkDrawInfo.Name = "chkDrawInfo";
            this.chkDrawInfo.Size = new System.Drawing.Size(77, 16);
            this.chkDrawInfo.TabIndex = 5;
            this.chkDrawInfo.Text = "Draw Info";
            this.chkDrawInfo.UseVisualStyleBackColor = true;
            this.chkDrawInfo.CheckedChanged += new System.EventHandler(this.chkDrawFrame_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(105, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "BMP Loader";
            // 
            // cbxBmpLoader
            // 
            this.cbxBmpLoader.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBmpLoader.FormattingEnabled = true;
            this.cbxBmpLoader.Items.AddRange(new object[] {
            "C",
            "OpenCV",
            ".NET Bitmap"});
            this.cbxBmpLoader.Location = new System.Drawing.Point(186, 147);
            this.cbxBmpLoader.Name = "cbxBmpLoader";
            this.cbxBmpLoader.Size = new System.Drawing.Size(121, 20);
            this.cbxBmpLoader.TabIndex = 6;
            // 
            // btnAlloc
            // 
            this.btnAlloc.Location = new System.Drawing.Point(8, 87);
            this.btnAlloc.Name = "btnAlloc";
            this.btnAlloc.Size = new System.Drawing.Size(91, 23);
            this.btnAlloc.TabIndex = 1;
            this.btnAlloc.Text = "Alloc Buf";
            this.btnAlloc.UseVisualStyleBackColor = true;
            this.btnAlloc.Click += new System.EventHandler(this.btnAlloc_Click);
            // 
            // btnClearLog
            // 
            this.btnClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClearLog.Location = new System.Drawing.Point(6, 382);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(75, 23);
            this.btnClearLog.TabIndex = 1;
            this.btnClearLog.Text = "Clear";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // tbxFwdDir
            // 
            this.tbxFwdDir.Location = new System.Drawing.Point(105, 118);
            this.tbxFwdDir.Name = "tbxFwdDir";
            this.tbxFwdDir.Size = new System.Drawing.Size(326, 21);
            this.tbxFwdDir.TabIndex = 4;
            this.tbxFwdDir.Text = "C:\\test\\대외비_이영철_190712_Rib\\Fwd0";
            // 
            // btnResetZoom
            // 
            this.btnResetZoom.Location = new System.Drawing.Point(8, 174);
            this.btnResetZoom.Name = "btnResetZoom";
            this.btnResetZoom.Size = new System.Drawing.Size(91, 23);
            this.btnResetZoom.TabIndex = 1;
            this.btnResetZoom.Text = "Reset Zoom";
            this.btnResetZoom.UseVisualStyleBackColor = true;
            this.btnResetZoom.Click += new System.EventHandler(this.btnResetZoom_Click);
            // 
            // numFNum
            // 
            this.numFNum.Location = new System.Drawing.Point(251, 90);
            this.numFNum.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numFNum.Name = "numFNum";
            this.numFNum.Size = new System.Drawing.Size(67, 21);
            this.numFNum.TabIndex = 3;
            this.numFNum.Value = new decimal(new int[] {
            215,
            0,
            0,
            0});
            // 
            // btnFwdDir
            // 
            this.btnFwdDir.Location = new System.Drawing.Point(8, 116);
            this.btnFwdDir.Name = "btnFwdDir";
            this.btnFwdDir.Size = new System.Drawing.Size(91, 23);
            this.btnFwdDir.TabIndex = 1;
            this.btnFwdDir.Text = "Fwd Dir";
            this.btnFwdDir.UseVisualStyleBackColor = true;
            this.btnFwdDir.Click += new System.EventHandler(this.btnFwdDir_Click);
            // 
            // btnLoadFwd
            // 
            this.btnLoadFwd.Location = new System.Drawing.Point(8, 145);
            this.btnLoadFwd.Name = "btnLoadFwd";
            this.btnLoadFwd.Size = new System.Drawing.Size(91, 23);
            this.btnLoadFwd.TabIndex = 2;
            this.btnLoadFwd.Text = "Load Fwd";
            this.btnLoadFwd.UseVisualStyleBackColor = true;
            this.btnLoadFwd.Click += new System.EventHandler(this.btnLoadFwd_Click);
            // 
            // numFH
            // 
            this.numFH.Location = new System.Drawing.Point(178, 90);
            this.numFH.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numFH.Name = "numFH";
            this.numFH.Size = new System.Drawing.Size(67, 21);
            this.numFH.TabIndex = 3;
            this.numFH.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            // 
            // numFW
            // 
            this.numFW.Location = new System.Drawing.Point(105, 90);
            this.numFW.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numFW.Name = "numFW";
            this.numFW.Size = new System.Drawing.Size(67, 21);
            this.numFW.TabIndex = 3;
            this.numFW.Value = new decimal(new int[] {
            12000,
            0,
            0,
            0});
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter2.Location = new System.Drawing.Point(0, 411);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(435, 3);
            this.splitter2.TabIndex = 6;
            this.splitter2.TabStop = false;
            // 
            // tbxLog
            // 
            this.tbxLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbxLog.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tbxLog.Location = new System.Drawing.Point(0, 414);
            this.tbxLog.Multiline = true;
            this.tbxLog.Name = "tbxLog";
            this.tbxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbxLog.Size = new System.Drawing.Size(435, 186);
            this.tbxLog.TabIndex = 0;
            this.tbxLog.WordWrap = false;
            // 
            // pbxDraw
            // 
            this.pbxDraw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxDraw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxDraw.Location = new System.Drawing.Point(0, 0);
            this.pbxDraw.Name = "pbxDraw";
            this.pbxDraw.Size = new System.Drawing.Size(561, 600);
            this.pbxDraw.TabIndex = 1;
            this.pbxDraw.TabStop = false;
            this.pbxDraw.Paint += new System.Windows.Forms.PaintEventHandler(this.pbxDraw_Paint);
            this.pbxDraw.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbxDraw_MouseDown);
            this.pbxDraw.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbxDraw_MouseMove);
            this.pbxDraw.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbxDraw_MouseUp);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(561, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 600);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // chkDrawHoles
            // 
            this.chkDrawHoles.AutoSize = true;
            this.chkDrawHoles.Checked = true;
            this.chkDrawHoles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDrawHoles.Location = new System.Drawing.Point(315, 20);
            this.chkDrawHoles.Name = "chkDrawHoles";
            this.chkDrawHoles.Size = new System.Drawing.Size(89, 16);
            this.chkDrawHoles.TabIndex = 5;
            this.chkDrawHoles.Text = "Draw Holes";
            this.chkDrawHoles.UseVisualStyleBackColor = true;
            this.chkDrawHoles.CheckedChanged += new System.EventHandler(this.chkDrawFrame_CheckedChanged);
            // 
            // btnInitHoles
            // 
            this.btnInitHoles.Location = new System.Drawing.Point(8, 203);
            this.btnInitHoles.Name = "btnInitHoles";
            this.btnInitHoles.Size = new System.Drawing.Size(91, 23);
            this.btnInitHoles.TabIndex = 1;
            this.btnInitHoles.Text = "Init Holes";
            this.btnInitHoles.UseVisualStyleBackColor = true;
            this.btnInitHoles.Click += new System.EventHandler(this.btnInitHoles_Click);
            // 
            // numHolePitchX
            // 
            this.numHolePitchX.DecimalPlaces = 2;
            this.numHolePitchX.Location = new System.Drawing.Point(107, 203);
            this.numHolePitchX.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numHolePitchX.Name = "numHolePitchX";
            this.numHolePitchX.Size = new System.Drawing.Size(67, 21);
            this.numHolePitchX.TabIndex = 3;
            this.numHolePitchX.Value = new decimal(new int[] {
            31,
            0,
            0,
            0});
            // 
            // numHolePitchY
            // 
            this.numHolePitchY.DecimalPlaces = 2;
            this.numHolePitchY.Location = new System.Drawing.Point(180, 203);
            this.numHolePitchY.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numHolePitchY.Name = "numHolePitchY";
            this.numHolePitchY.Size = new System.Drawing.Size(67, 21);
            this.numHolePitchY.TabIndex = 3;
            this.numHolePitchY.Value = new decimal(new int[] {
            31,
            0,
            0,
            0});
            // 
            // numHoleW
            // 
            this.numHoleW.Location = new System.Drawing.Point(107, 230);
            this.numHoleW.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numHoleW.Name = "numHoleW";
            this.numHoleW.Size = new System.Drawing.Size(67, 21);
            this.numHoleW.TabIndex = 3;
            this.numHoleW.Value = new decimal(new int[] {
            1444,
            0,
            0,
            0});
            // 
            // numHoleH
            // 
            this.numHoleH.Location = new System.Drawing.Point(180, 230);
            this.numHoleH.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numHoleH.Name = "numHoleH";
            this.numHoleH.Size = new System.Drawing.Size(67, 21);
            this.numHoleH.TabIndex = 3;
            this.numHoleH.Value = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 600);
            this.Controls.Add(this.pbxDraw);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Name = "FormMain";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDraw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHolePitchX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHolePitchY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHoleW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHoleH)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnAlloc;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.TextBox tbxFwdDir;
        private System.Windows.Forms.NumericUpDown numFNum;
        private System.Windows.Forms.Button btnFwdDir;
        private System.Windows.Forms.Button btnLoadFwd;
        private System.Windows.Forms.NumericUpDown numFH;
        private System.Windows.Forms.NumericUpDown numFW;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.TextBox tbxLog;
        private System.Windows.Forms.PictureBox pbxDraw;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.FolderBrowserDialog dlgFolder;
        private System.Windows.Forms.Button btnResetZoom;
        private System.Windows.Forms.CheckBox chkDrawInfo;
        private System.Windows.Forms.CheckBox chkDrawPixelValue;
        private System.Windows.Forms.CheckBox chkDrawFrame;
        private System.Windows.Forms.ComboBox cbxBmpLoader;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxImageBufferDrawer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkDrawHoles;
        private System.Windows.Forms.Button btnInitHoles;
        private System.Windows.Forms.NumericUpDown numHoleH;
        private System.Windows.Forms.NumericUpDown numHolePitchY;
        private System.Windows.Forms.NumericUpDown numHoleW;
        private System.Windows.Forms.NumericUpDown numHolePitchX;
    }
}

