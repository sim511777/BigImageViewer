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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdoHoleInfoFWD = new System.Windows.Forms.RadioButton();
            this.rdoHoleInfoIndexY = new System.Windows.Forms.RadioButton();
            this.rdoHoleInfoIndexX = new System.Windows.Forms.RadioButton();
            this.rdoHoleInfoNone = new System.Windows.Forms.RadioButton();
            this.chkDrawFrame = new System.Windows.Forms.CheckBox();
            this.chkDrawPixelValue = new System.Windows.Forms.CheckBox();
            this.chkDrawHoles = new System.Windows.Forms.CheckBox();
            this.chkDrawInfo = new System.Windows.Forms.CheckBox();
            this.btnResetZoom = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnInitHoles = new System.Windows.Forms.Button();
            this.btnAlloc = new System.Windows.Forms.Button();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.tbxFwdDir = new System.Windows.Forms.TextBox();
            this.numFNum = new System.Windows.Forms.NumericUpDown();
            this.btnFwdDir = new System.Windows.Forms.Button();
            this.btnLoadFwd = new System.Windows.Forms.Button();
            this.numHoleH = new System.Windows.Forms.NumericUpDown();
            this.numHoleDy = new System.Windows.Forms.NumericUpDown();
            this.numHoleTop = new System.Windows.Forms.NumericUpDown();
            this.numHolePitchY = new System.Windows.Forms.NumericUpDown();
            this.numFH = new System.Windows.Forms.NumericUpDown();
            this.numHoleW = new System.Windows.Forms.NumericUpDown();
            this.numHoleDx = new System.Windows.Forms.NumericUpDown();
            this.numHoleLeft = new System.Windows.Forms.NumericUpDown();
            this.numHolePitchX = new System.Windows.Forms.NumericUpDown();
            this.numFW = new System.Windows.Forms.NumericUpDown();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.tbxLog = new System.Windows.Forms.TextBox();
            this.pbxDraw = new System.Windows.Forms.PictureBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.dlgFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.chkDrawCursorHole = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHoleH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHoleDy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHoleTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHolePitchY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHoleW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHoleDx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHoleLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHolePitchX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDraw)).BeginInit();
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
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.btnInitHoles);
            this.panel2.Controls.Add(this.btnAlloc);
            this.panel2.Controls.Add(this.btnClearLog);
            this.panel2.Controls.Add(this.tbxFwdDir);
            this.panel2.Controls.Add(this.numFNum);
            this.panel2.Controls.Add(this.btnFwdDir);
            this.panel2.Controls.Add(this.btnLoadFwd);
            this.panel2.Controls.Add(this.numHoleH);
            this.panel2.Controls.Add(this.numHoleDy);
            this.panel2.Controls.Add(this.numHoleTop);
            this.panel2.Controls.Add(this.numHolePitchY);
            this.panel2.Controls.Add(this.numFH);
            this.panel2.Controls.Add(this.numHoleW);
            this.panel2.Controls.Add(this.numHoleDx);
            this.panel2.Controls.Add(this.numHoleLeft);
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
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.chkDrawFrame);
            this.groupBox1.Controls.Add(this.chkDrawPixelValue);
            this.groupBox1.Controls.Add(this.chkDrawCursorHole);
            this.groupBox1.Controls.Add(this.chkDrawHoles);
            this.groupBox1.Controls.Add(this.chkDrawInfo);
            this.groupBox1.Controls.Add(this.btnResetZoom);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(435, 123);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Draw Option";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdoHoleInfoFWD);
            this.groupBox2.Controls.Add(this.rdoHoleInfoIndexY);
            this.groupBox2.Controls.Add(this.rdoHoleInfoIndexX);
            this.groupBox2.Controls.Add(this.rdoHoleInfoNone);
            this.groupBox2.Location = new System.Drawing.Point(8, 42);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(415, 46);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Disp Hole Info";
            // 
            // rdoHoleInfoFWD
            // 
            this.rdoHoleInfoFWD.AutoSize = true;
            this.rdoHoleInfoFWD.Location = new System.Drawing.Point(201, 20);
            this.rdoHoleInfoFWD.Name = "rdoHoleInfoFWD";
            this.rdoHoleInfoFWD.Size = new System.Drawing.Size(48, 16);
            this.rdoHoleInfoFWD.TabIndex = 0;
            this.rdoHoleInfoFWD.Text = "FWD";
            this.rdoHoleInfoFWD.UseVisualStyleBackColor = true;
            this.rdoHoleInfoFWD.CheckedChanged += new System.EventHandler(this.chkDrawFrame_CheckedChanged);
            // 
            // rdoHoleInfoIndexY
            // 
            this.rdoHoleInfoIndexY.AutoSize = true;
            this.rdoHoleInfoIndexY.Location = new System.Drawing.Point(133, 20);
            this.rdoHoleInfoIndexY.Name = "rdoHoleInfoIndexY";
            this.rdoHoleInfoIndexY.Size = new System.Drawing.Size(62, 16);
            this.rdoHoleInfoIndexY.TabIndex = 0;
            this.rdoHoleInfoIndexY.Text = "IndexY";
            this.rdoHoleInfoIndexY.UseVisualStyleBackColor = true;
            this.rdoHoleInfoIndexY.CheckedChanged += new System.EventHandler(this.chkDrawFrame_CheckedChanged);
            // 
            // rdoHoleInfoIndexX
            // 
            this.rdoHoleInfoIndexX.AutoSize = true;
            this.rdoHoleInfoIndexX.Checked = true;
            this.rdoHoleInfoIndexX.Location = new System.Drawing.Point(65, 20);
            this.rdoHoleInfoIndexX.Name = "rdoHoleInfoIndexX";
            this.rdoHoleInfoIndexX.Size = new System.Drawing.Size(62, 16);
            this.rdoHoleInfoIndexX.TabIndex = 0;
            this.rdoHoleInfoIndexX.TabStop = true;
            this.rdoHoleInfoIndexX.Text = "IndexX";
            this.rdoHoleInfoIndexX.UseVisualStyleBackColor = true;
            this.rdoHoleInfoIndexX.CheckedChanged += new System.EventHandler(this.chkDrawFrame_CheckedChanged);
            // 
            // rdoHoleInfoNone
            // 
            this.rdoHoleInfoNone.AutoSize = true;
            this.rdoHoleInfoNone.Location = new System.Drawing.Point(6, 20);
            this.rdoHoleInfoNone.Name = "rdoHoleInfoNone";
            this.rdoHoleInfoNone.Size = new System.Drawing.Size(53, 16);
            this.rdoHoleInfoNone.TabIndex = 0;
            this.rdoHoleInfoNone.Text = "None";
            this.rdoHoleInfoNone.UseVisualStyleBackColor = true;
            this.rdoHoleInfoNone.CheckedChanged += new System.EventHandler(this.chkDrawFrame_CheckedChanged);
            // 
            // chkDrawFrame
            // 
            this.chkDrawFrame.AutoSize = true;
            this.chkDrawFrame.Checked = true;
            this.chkDrawFrame.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDrawFrame.Location = new System.Drawing.Point(6, 20);
            this.chkDrawFrame.Name = "chkDrawFrame";
            this.chkDrawFrame.Size = new System.Drawing.Size(60, 16);
            this.chkDrawFrame.TabIndex = 5;
            this.chkDrawFrame.Text = "Frame";
            this.chkDrawFrame.UseVisualStyleBackColor = true;
            this.chkDrawFrame.CheckedChanged += new System.EventHandler(this.chkDrawFrame_CheckedChanged);
            // 
            // chkDrawPixelValue
            // 
            this.chkDrawPixelValue.AutoSize = true;
            this.chkDrawPixelValue.Checked = true;
            this.chkDrawPixelValue.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDrawPixelValue.Location = new System.Drawing.Point(72, 20);
            this.chkDrawPixelValue.Name = "chkDrawPixelValue";
            this.chkDrawPixelValue.Size = new System.Drawing.Size(88, 16);
            this.chkDrawPixelValue.TabIndex = 5;
            this.chkDrawPixelValue.Text = "Pixel Value";
            this.chkDrawPixelValue.UseVisualStyleBackColor = true;
            this.chkDrawPixelValue.CheckedChanged += new System.EventHandler(this.chkDrawFrame_CheckedChanged);
            // 
            // chkDrawHoles
            // 
            this.chkDrawHoles.AutoSize = true;
            this.chkDrawHoles.Checked = true;
            this.chkDrawHoles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDrawHoles.Location = new System.Drawing.Point(258, 20);
            this.chkDrawHoles.Name = "chkDrawHoles";
            this.chkDrawHoles.Size = new System.Drawing.Size(56, 16);
            this.chkDrawHoles.TabIndex = 5;
            this.chkDrawHoles.Text = "Holes";
            this.chkDrawHoles.UseVisualStyleBackColor = true;
            this.chkDrawHoles.CheckedChanged += new System.EventHandler(this.chkDrawFrame_CheckedChanged);
            // 
            // chkDrawInfo
            // 
            this.chkDrawInfo.AutoSize = true;
            this.chkDrawInfo.Checked = true;
            this.chkDrawInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDrawInfo.Location = new System.Drawing.Point(166, 20);
            this.chkDrawInfo.Name = "chkDrawInfo";
            this.chkDrawInfo.Size = new System.Drawing.Size(86, 16);
            this.chkDrawInfo.TabIndex = 5;
            this.chkDrawInfo.Text = "Cursur Info";
            this.chkDrawInfo.UseVisualStyleBackColor = true;
            this.chkDrawInfo.CheckedChanged += new System.EventHandler(this.chkDrawFrame_CheckedChanged);
            // 
            // btnResetZoom
            // 
            this.btnResetZoom.Location = new System.Drawing.Point(6, 94);
            this.btnResetZoom.Name = "btnResetZoom";
            this.btnResetZoom.Size = new System.Drawing.Size(91, 23);
            this.btnResetZoom.TabIndex = 1;
            this.btnResetZoom.Text = "Reset Zoom";
            this.btnResetZoom.UseVisualStyleBackColor = true;
            this.btnResetZoom.Click += new System.EventHandler(this.btnResetZoom_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(105, 294);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "Diameter";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(105, 240);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "Left Top";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(105, 267);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "Pitch";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(105, 214);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "Num";
            // 
            // btnInitHoles
            // 
            this.btnInitHoles.Location = new System.Drawing.Point(8, 209);
            this.btnInitHoles.Name = "btnInitHoles";
            this.btnInitHoles.Size = new System.Drawing.Size(91, 23);
            this.btnInitHoles.TabIndex = 1;
            this.btnInitHoles.Text = "Init Holes";
            this.btnInitHoles.UseVisualStyleBackColor = true;
            this.btnInitHoles.Click += new System.EventHandler(this.btnInitHoles_Click);
            // 
            // btnAlloc
            // 
            this.btnAlloc.Location = new System.Drawing.Point(8, 140);
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
            this.tbxFwdDir.Location = new System.Drawing.Point(105, 171);
            this.tbxFwdDir.Name = "tbxFwdDir";
            this.tbxFwdDir.Size = new System.Drawing.Size(299, 21);
            this.tbxFwdDir.TabIndex = 4;
            this.tbxFwdDir.Text = "C:\\test\\대외비_이영철_190712_Rib\\Fwd0";
            // 
            // numFNum
            // 
            this.numFNum.Location = new System.Drawing.Point(251, 143);
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
            this.btnFwdDir.Location = new System.Drawing.Point(404, 169);
            this.btnFwdDir.Name = "btnFwdDir";
            this.btnFwdDir.Size = new System.Drawing.Size(25, 23);
            this.btnFwdDir.TabIndex = 1;
            this.btnFwdDir.Text = "...";
            this.btnFwdDir.UseVisualStyleBackColor = true;
            this.btnFwdDir.Click += new System.EventHandler(this.btnFwdDir_Click);
            // 
            // btnLoadFwd
            // 
            this.btnLoadFwd.Location = new System.Drawing.Point(8, 169);
            this.btnLoadFwd.Name = "btnLoadFwd";
            this.btnLoadFwd.Size = new System.Drawing.Size(91, 23);
            this.btnLoadFwd.TabIndex = 2;
            this.btnLoadFwd.Text = "Load Fwd";
            this.btnLoadFwd.UseVisualStyleBackColor = true;
            this.btnLoadFwd.Click += new System.EventHandler(this.btnLoadFwd_Click);
            // 
            // numHoleH
            // 
            this.numHoleH.Location = new System.Drawing.Point(238, 211);
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
            // numHoleDy
            // 
            this.numHoleDy.DecimalPlaces = 4;
            this.numHoleDy.Location = new System.Drawing.Point(238, 292);
            this.numHoleDy.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numHoleDy.Name = "numHoleDy";
            this.numHoleDy.Size = new System.Drawing.Size(67, 21);
            this.numHoleDy.TabIndex = 3;
            this.numHoleDy.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // numHoleTop
            // 
            this.numHoleTop.DecimalPlaces = 4;
            this.numHoleTop.Location = new System.Drawing.Point(238, 238);
            this.numHoleTop.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numHoleTop.Name = "numHoleTop";
            this.numHoleTop.Size = new System.Drawing.Size(67, 21);
            this.numHoleTop.TabIndex = 3;
            this.numHoleTop.Value = new decimal(new int[] {
            7280,
            0,
            0,
            0});
            // 
            // numHolePitchY
            // 
            this.numHolePitchY.DecimalPlaces = 4;
            this.numHolePitchY.Location = new System.Drawing.Point(238, 265);
            this.numHolePitchY.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numHolePitchY.Name = "numHolePitchY";
            this.numHolePitchY.Size = new System.Drawing.Size(67, 21);
            this.numHolePitchY.TabIndex = 3;
            this.numHolePitchY.Value = new decimal(new int[] {
            307273,
            0,
            0,
            262144});
            // 
            // numFH
            // 
            this.numFH.Location = new System.Drawing.Point(178, 143);
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
            // numHoleW
            // 
            this.numHoleW.Location = new System.Drawing.Point(165, 211);
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
            // numHoleDx
            // 
            this.numHoleDx.DecimalPlaces = 4;
            this.numHoleDx.Location = new System.Drawing.Point(165, 292);
            this.numHoleDx.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numHoleDx.Name = "numHoleDx";
            this.numHoleDx.Size = new System.Drawing.Size(67, 21);
            this.numHoleDx.TabIndex = 3;
            this.numHoleDx.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // numHoleLeft
            // 
            this.numHoleLeft.DecimalPlaces = 4;
            this.numHoleLeft.Location = new System.Drawing.Point(165, 238);
            this.numHoleLeft.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numHoleLeft.Name = "numHoleLeft";
            this.numHoleLeft.Size = new System.Drawing.Size(67, 21);
            this.numHoleLeft.TabIndex = 3;
            this.numHoleLeft.Value = new decimal(new int[] {
            3340,
            0,
            0,
            0});
            // 
            // numHolePitchX
            // 
            this.numHolePitchX.DecimalPlaces = 4;
            this.numHolePitchX.Location = new System.Drawing.Point(165, 265);
            this.numHolePitchX.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numHolePitchX.Name = "numHolePitchX";
            this.numHolePitchX.Size = new System.Drawing.Size(67, 21);
            this.numHolePitchX.TabIndex = 3;
            this.numHolePitchX.Value = new decimal(new int[] {
            31216,
            0,
            0,
            196608});
            // 
            // numFW
            // 
            this.numFW.Location = new System.Drawing.Point(105, 143);
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
            // chkDrawCursorHole
            // 
            this.chkDrawCursorHole.AutoSize = true;
            this.chkDrawCursorHole.Checked = true;
            this.chkDrawCursorHole.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDrawCursorHole.Location = new System.Drawing.Point(320, 20);
            this.chkDrawCursorHole.Name = "chkDrawCursorHole";
            this.chkDrawCursorHole.Size = new System.Drawing.Size(91, 16);
            this.chkDrawCursorHole.TabIndex = 5;
            this.chkDrawCursorHole.Text = "Cursor Hole";
            this.chkDrawCursorHole.UseVisualStyleBackColor = true;
            this.chkDrawCursorHole.CheckedChanged += new System.EventHandler(this.chkDrawFrame_CheckedChanged);
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
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHoleH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHoleDy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHoleTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHolePitchY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHoleW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHoleDx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHoleLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHolePitchX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDraw)).EndInit();
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkDrawHoles;
        private System.Windows.Forms.Button btnInitHoles;
        private System.Windows.Forms.NumericUpDown numHoleH;
        private System.Windows.Forms.NumericUpDown numHolePitchY;
        private System.Windows.Forms.NumericUpDown numHoleW;
        private System.Windows.Forms.NumericUpDown numHolePitchX;
        private System.Windows.Forms.NumericUpDown numHoleDy;
        private System.Windows.Forms.NumericUpDown numHoleDx;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numHoleTop;
        private System.Windows.Forms.NumericUpDown numHoleLeft;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdoHoleInfoFWD;
        private System.Windows.Forms.RadioButton rdoHoleInfoIndexY;
        private System.Windows.Forms.RadioButton rdoHoleInfoIndexX;
        private System.Windows.Forms.RadioButton rdoHoleInfoNone;
        private System.Windows.Forms.CheckBox chkDrawCursorHole;
    }
}

