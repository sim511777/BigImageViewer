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
            this.tbxLog = new System.Windows.Forms.TextBox();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnAlloc = new System.Windows.Forms.Button();
            this.numFW = new System.Windows.Forms.NumericUpDown();
            this.chkFromBack = new System.Windows.Forms.CheckBox();
            this.numFwd = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numHolePitchX = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numHoleLeft = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numHoleDx = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numHoleW = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numFH = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numFwdOverlap = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numHolePitchY = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numHoleTop = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numHoleDy = new System.Windows.Forms.NumericUpDown();
            this.btnInitHoles = new System.Windows.Forms.Button();
            this.numHoleH = new System.Windows.Forms.NumericUpDown();
            this.btnLoadFwd = new System.Windows.Forms.Button();
            this.numFNum = new System.Windows.Forms.NumericUpDown();
            this.tbxSurfDir = new System.Windows.Forms.TextBox();
            this.btnLoadSurf = new System.Windows.Forms.Button();
            this.tbxFwdDir = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chkDrawPixelValue = new System.Windows.Forms.CheckBox();
            this.chkDrawInfo = new System.Windows.Forms.CheckBox();
            this.chkDrawCenterLine = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkDrawFrame = new System.Windows.Forms.CheckBox();
            this.chkDrawHoles = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdoHoleInfoFWD = new System.Windows.Forms.RadioButton();
            this.rdoHoleInfoIndexY = new System.Windows.Forms.RadioButton();
            this.rdoHoleInfoIndexX = new System.Windows.Forms.RadioButton();
            this.rdoHoleInfoNone = new System.Windows.Forms.RadioButton();
            this.chkDrawCursorHole = new System.Windows.Forms.CheckBox();
            this.btnResetZoom = new System.Windows.Forms.Button();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pbxDraw = new ShimLib.ZoomPictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFwd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHolePitchX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHoleLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHoleDx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHoleW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFwdOverlap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHolePitchY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHoleTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHoleDy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHoleH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFNum)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbxLog);
            this.panel1.Controls.Add(this.splitter2);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(566, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(435, 666);
            this.panel1.TabIndex = 0;
            // 
            // tbxLog
            // 
            this.tbxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxLog.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tbxLog.Location = new System.Drawing.Point(0, 483);
            this.tbxLog.Multiline = true;
            this.tbxLog.Name = "tbxLog";
            this.tbxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbxLog.Size = new System.Drawing.Size(435, 183);
            this.tbxLog.TabIndex = 0;
            this.tbxLog.WordWrap = false;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter2.Location = new System.Drawing.Point(0, 480);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(435, 3);
            this.splitter2.TabIndex = 6;
            this.splitter2.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.btnClearLog);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5);
            this.panel2.Size = new System.Drawing.Size(435, 480);
            this.panel2.TabIndex = 5;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnAlloc);
            this.groupBox3.Controls.Add(this.numFW);
            this.groupBox3.Controls.Add(this.chkFromBack);
            this.groupBox3.Controls.Add(this.numFwd);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.numHolePitchX);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.numHoleLeft);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.numHoleDx);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.numHoleW);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.numFH);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.numFwdOverlap);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.numHolePitchY);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.numHoleTop);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.numHoleDy);
            this.groupBox3.Controls.Add(this.btnInitHoles);
            this.groupBox3.Controls.Add(this.numHoleH);
            this.groupBox3.Controls.Add(this.btnLoadFwd);
            this.groupBox3.Controls.Add(this.numFNum);
            this.groupBox3.Controls.Add(this.tbxSurfDir);
            this.groupBox3.Controls.Add(this.btnLoadSurf);
            this.groupBox3.Controls.Add(this.tbxFwdDir);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(5, 207);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(425, 232);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Data";
            // 
            // btnAlloc
            // 
            this.btnAlloc.Location = new System.Drawing.Point(6, 29);
            this.btnAlloc.Name = "btnAlloc";
            this.btnAlloc.Size = new System.Drawing.Size(91, 23);
            this.btnAlloc.TabIndex = 1;
            this.btnAlloc.Text = "Alloc Buf";
            this.btnAlloc.UseVisualStyleBackColor = true;
            this.btnAlloc.Click += new System.EventHandler(this.btnAlloc_Click);
            // 
            // numFW
            // 
            this.numFW.Location = new System.Drawing.Point(103, 31);
            this.numFW.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numFW.Name = "numFW";
            this.numFW.Size = new System.Drawing.Size(55, 21);
            this.numFW.TabIndex = 3;
            this.numFW.Value = new decimal(new int[] {
            12000,
            0,
            0,
            0});
            // 
            // chkFromBack
            // 
            this.chkFromBack.AutoSize = true;
            this.chkFromBack.Location = new System.Drawing.Point(330, 89);
            this.chkFromBack.Name = "chkFromBack";
            this.chkFromBack.Size = new System.Drawing.Size(88, 16);
            this.chkFromBack.TabIndex = 5;
            this.chkFromBack.Text = "포워드 역순";
            this.chkFromBack.UseVisualStyleBackColor = true;
            this.chkFromBack.CheckedChanged += new System.EventHandler(this.chkDrawFrame_CheckedChanged);
            // 
            // numFwd
            // 
            this.numFwd.Location = new System.Drawing.Point(286, 31);
            this.numFwd.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numFwd.Name = "numFwd";
            this.numFwd.Size = new System.Drawing.Size(55, 21);
            this.numFwd.TabIndex = 3;
            this.numFwd.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(103, 204);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "Diameter";
            // 
            // numHolePitchX
            // 
            this.numHolePitchX.DecimalPlaces = 4;
            this.numHolePitchX.Location = new System.Drawing.Point(163, 175);
            this.numHolePitchX.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numHolePitchX.Name = "numHolePitchX";
            this.numHolePitchX.Size = new System.Drawing.Size(67, 21);
            this.numHolePitchX.TabIndex = 3;
            this.numHolePitchX.Value = new decimal(new int[] {
            31218,
            0,
            0,
            196608});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(103, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "Left Top";
            // 
            // numHoleLeft
            // 
            this.numHoleLeft.DecimalPlaces = 4;
            this.numHoleLeft.Location = new System.Drawing.Point(163, 148);
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(103, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "Pitch";
            // 
            // numHoleDx
            // 
            this.numHoleDx.DecimalPlaces = 4;
            this.numHoleDx.Location = new System.Drawing.Point(163, 202);
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
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(345, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 12);
            this.label9.TabIndex = 7;
            this.label9.Text = "Fwd Overlap";
            // 
            // numHoleW
            // 
            this.numHoleW.Location = new System.Drawing.Point(163, 121);
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
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(284, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "Fwd Num";
            // 
            // numFH
            // 
            this.numFH.Location = new System.Drawing.Point(164, 31);
            this.numFH.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numFH.Name = "numFH";
            this.numFH.Size = new System.Drawing.Size(55, 21);
            this.numFH.TabIndex = 3;
            this.numFH.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(223, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 12);
            this.label7.TabIndex = 7;
            this.label7.Text = "Frm Num";
            // 
            // numFwdOverlap
            // 
            this.numFwdOverlap.Location = new System.Drawing.Point(347, 31);
            this.numFwdOverlap.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numFwdOverlap.Name = "numFwdOverlap";
            this.numFwdOverlap.Size = new System.Drawing.Size(55, 21);
            this.numFwdOverlap.TabIndex = 3;
            this.numFwdOverlap.Value = new decimal(new int[] {
            72,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(162, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "Frm H";
            // 
            // numHolePitchY
            // 
            this.numHolePitchY.DecimalPlaces = 4;
            this.numHolePitchY.Location = new System.Drawing.Point(236, 175);
            this.numHolePitchY.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numHolePitchY.Name = "numHolePitchY";
            this.numHolePitchY.Size = new System.Drawing.Size(67, 21);
            this.numHolePitchY.TabIndex = 3;
            this.numHolePitchY.Value = new decimal(new int[] {
            30725,
            0,
            0,
            196608});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(101, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "Frm W";
            // 
            // numHoleTop
            // 
            this.numHoleTop.DecimalPlaces = 4;
            this.numHoleTop.Location = new System.Drawing.Point(236, 148);
            this.numHoleTop.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numHoleTop.Name = "numHoleTop";
            this.numHoleTop.Size = new System.Drawing.Size(67, 21);
            this.numHoleTop.TabIndex = 3;
            this.numHoleTop.Value = new decimal(new int[] {
            7279,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(103, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "Num";
            // 
            // numHoleDy
            // 
            this.numHoleDy.DecimalPlaces = 4;
            this.numHoleDy.Location = new System.Drawing.Point(236, 202);
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
            // btnInitHoles
            // 
            this.btnInitHoles.Location = new System.Drawing.Point(6, 119);
            this.btnInitHoles.Name = "btnInitHoles";
            this.btnInitHoles.Size = new System.Drawing.Size(91, 23);
            this.btnInitHoles.TabIndex = 1;
            this.btnInitHoles.Text = "Init Holes";
            this.btnInitHoles.UseVisualStyleBackColor = true;
            this.btnInitHoles.Click += new System.EventHandler(this.btnInitHoles_Click);
            // 
            // numHoleH
            // 
            this.numHoleH.Location = new System.Drawing.Point(236, 121);
            this.numHoleH.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numHoleH.Name = "numHoleH";
            this.numHoleH.Size = new System.Drawing.Size(67, 21);
            this.numHoleH.TabIndex = 3;
            this.numHoleH.Value = new decimal(new int[] {
            6788,
            0,
            0,
            0});
            // 
            // btnLoadFwd
            // 
            this.btnLoadFwd.Location = new System.Drawing.Point(6, 58);
            this.btnLoadFwd.Name = "btnLoadFwd";
            this.btnLoadFwd.Size = new System.Drawing.Size(91, 23);
            this.btnLoadFwd.TabIndex = 2;
            this.btnLoadFwd.Text = "Load Fwd";
            this.btnLoadFwd.UseVisualStyleBackColor = true;
            this.btnLoadFwd.Click += new System.EventHandler(this.btnLoadFwd_Click);
            // 
            // numFNum
            // 
            this.numFNum.Location = new System.Drawing.Point(225, 31);
            this.numFNum.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numFNum.Name = "numFNum";
            this.numFNum.Size = new System.Drawing.Size(55, 21);
            this.numFNum.TabIndex = 3;
            this.numFNum.Value = new decimal(new int[] {
            215,
            0,
            0,
            0});
            // 
            // tbxSurfDir
            // 
            this.tbxSurfDir.Location = new System.Drawing.Point(103, 87);
            this.tbxSurfDir.Name = "tbxSurfDir";
            this.tbxSurfDir.Size = new System.Drawing.Size(221, 21);
            this.tbxSurfDir.TabIndex = 4;
            this.tbxSurfDir.Text = "C:\\test\\대외비_이영철_190712_Rib";
            // 
            // btnLoadSurf
            // 
            this.btnLoadSurf.Location = new System.Drawing.Point(6, 85);
            this.btnLoadSurf.Name = "btnLoadSurf";
            this.btnLoadSurf.Size = new System.Drawing.Size(91, 23);
            this.btnLoadSurf.TabIndex = 2;
            this.btnLoadSurf.Text = "Load Surf";
            this.btnLoadSurf.UseVisualStyleBackColor = true;
            this.btnLoadSurf.Click += new System.EventHandler(this.btnLoadSurf_Click);
            // 
            // tbxFwdDir
            // 
            this.tbxFwdDir.Location = new System.Drawing.Point(103, 60);
            this.tbxFwdDir.Name = "tbxFwdDir";
            this.tbxFwdDir.Size = new System.Drawing.Size(299, 21);
            this.tbxFwdDir.TabIndex = 4;
            this.tbxFwdDir.Text = "C:\\test\\대외비_이영철_190712_Rib\\Fwd0";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.btnResetZoom);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(425, 202);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Disp Option";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.chkDrawPixelValue);
            this.groupBox5.Controls.Add(this.chkDrawInfo);
            this.groupBox5.Controls.Add(this.chkDrawCenterLine);
            this.groupBox5.Location = new System.Drawing.Point(6, 20);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(412, 44);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "ZoomPictureBox";
            // 
            // chkDrawPixelValue
            // 
            this.chkDrawPixelValue.AutoSize = true;
            this.chkDrawPixelValue.Checked = true;
            this.chkDrawPixelValue.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDrawPixelValue.Location = new System.Drawing.Point(6, 20);
            this.chkDrawPixelValue.Name = "chkDrawPixelValue";
            this.chkDrawPixelValue.Size = new System.Drawing.Size(88, 16);
            this.chkDrawPixelValue.TabIndex = 5;
            this.chkDrawPixelValue.Text = "Pixel Value";
            this.chkDrawPixelValue.UseVisualStyleBackColor = true;
            this.chkDrawPixelValue.CheckedChanged += new System.EventHandler(this.chkDrawFrame_CheckedChanged);
            // 
            // chkDrawInfo
            // 
            this.chkDrawInfo.AutoSize = true;
            this.chkDrawInfo.Checked = true;
            this.chkDrawInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDrawInfo.Location = new System.Drawing.Point(100, 20);
            this.chkDrawInfo.Name = "chkDrawInfo";
            this.chkDrawInfo.Size = new System.Drawing.Size(86, 16);
            this.chkDrawInfo.TabIndex = 5;
            this.chkDrawInfo.Text = "Cursur Info";
            this.chkDrawInfo.UseVisualStyleBackColor = true;
            this.chkDrawInfo.CheckedChanged += new System.EventHandler(this.chkDrawFrame_CheckedChanged);
            // 
            // chkDrawCenterLine
            // 
            this.chkDrawCenterLine.AutoSize = true;
            this.chkDrawCenterLine.Checked = true;
            this.chkDrawCenterLine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDrawCenterLine.Location = new System.Drawing.Point(192, 20);
            this.chkDrawCenterLine.Name = "chkDrawCenterLine";
            this.chkDrawCenterLine.Size = new System.Drawing.Size(89, 16);
            this.chkDrawCenterLine.TabIndex = 5;
            this.chkDrawCenterLine.Text = "Center Line";
            this.chkDrawCenterLine.UseVisualStyleBackColor = true;
            this.chkDrawCenterLine.CheckedChanged += new System.EventHandler(this.chkDrawFrame_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkDrawFrame);
            this.groupBox4.Controls.Add(this.chkDrawHoles);
            this.groupBox4.Controls.Add(this.groupBox2);
            this.groupBox4.Controls.Add(this.chkDrawCursorHole);
            this.groupBox4.Location = new System.Drawing.Point(6, 70);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(410, 96);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Application Draw";
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
            // chkDrawHoles
            // 
            this.chkDrawHoles.AutoSize = true;
            this.chkDrawHoles.Checked = true;
            this.chkDrawHoles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDrawHoles.Location = new System.Drawing.Point(72, 20);
            this.chkDrawHoles.Name = "chkDrawHoles";
            this.chkDrawHoles.Size = new System.Drawing.Size(56, 16);
            this.chkDrawHoles.TabIndex = 5;
            this.chkDrawHoles.Text = "Holes";
            this.chkDrawHoles.UseVisualStyleBackColor = true;
            this.chkDrawHoles.CheckedChanged += new System.EventHandler(this.chkDrawFrame_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdoHoleInfoFWD);
            this.groupBox2.Controls.Add(this.rdoHoleInfoIndexY);
            this.groupBox2.Controls.Add(this.rdoHoleInfoIndexX);
            this.groupBox2.Controls.Add(this.rdoHoleInfoNone);
            this.groupBox2.Location = new System.Drawing.Point(6, 42);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(410, 46);
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
            // chkDrawCursorHole
            // 
            this.chkDrawCursorHole.AutoSize = true;
            this.chkDrawCursorHole.Checked = true;
            this.chkDrawCursorHole.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDrawCursorHole.Location = new System.Drawing.Point(134, 20);
            this.chkDrawCursorHole.Name = "chkDrawCursorHole";
            this.chkDrawCursorHole.Size = new System.Drawing.Size(91, 16);
            this.chkDrawCursorHole.TabIndex = 5;
            this.chkDrawCursorHole.Text = "Cursor Hole";
            this.chkDrawCursorHole.UseVisualStyleBackColor = true;
            this.chkDrawCursorHole.CheckedChanged += new System.EventHandler(this.chkDrawFrame_CheckedChanged);
            // 
            // btnResetZoom
            // 
            this.btnResetZoom.Location = new System.Drawing.Point(6, 172);
            this.btnResetZoom.Name = "btnResetZoom";
            this.btnResetZoom.Size = new System.Drawing.Size(91, 23);
            this.btnResetZoom.TabIndex = 1;
            this.btnResetZoom.Text = "Reset Zoom";
            this.btnResetZoom.UseVisualStyleBackColor = true;
            this.btnResetZoom.Click += new System.EventHandler(this.btnResetZoom_Click);
            // 
            // btnClearLog
            // 
            this.btnClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClearLog.Location = new System.Drawing.Point(11, 446);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(75, 23);
            this.btnClearLog.TabIndex = 1;
            this.btnClearLog.Text = "Clear Log";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(563, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 666);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // pbxDraw
            // 
            this.pbxDraw.BackColor = System.Drawing.Color.Gray;
            this.pbxDraw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxDraw.Location = new System.Drawing.Point(0, 0);
            this.pbxDraw.Name = "pbxDraw";
            this.pbxDraw.PtPanning = new System.Drawing.Point(0, 0);
            this.pbxDraw.Size = new System.Drawing.Size(563, 666);
            this.pbxDraw.TabIndex = 3;
            this.pbxDraw.Text = "zoomPictureBox1";
            this.pbxDraw.UseDrawCenterLine = true;
            this.pbxDraw.UseDrawDrawTime = true;
            this.pbxDraw.UseDrawInfo = true;
            this.pbxDraw.UseDrawPixelValue = true;
            this.pbxDraw.UseMouseMove = true;
            this.pbxDraw.UseMouseWheelZoom = true;
            this.pbxDraw.ZoomLevel = -10;
            this.pbxDraw.Paint += new System.Windows.Forms.PaintEventHandler(this.pbxDraw_Paint);
            this.pbxDraw.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbxDraw_MouseMove);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 666);
            this.Controls.Add(this.pbxDraw);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Name = "FormMain";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFwd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHolePitchX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHoleLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHoleDx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHoleW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFwdOverlap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHolePitchY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHoleTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHoleDy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHoleH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFNum)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnAlloc;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.TextBox tbxFwdDir;
        private System.Windows.Forms.NumericUpDown numFNum;
        private System.Windows.Forms.Button btnLoadFwd;
        private System.Windows.Forms.NumericUpDown numFH;
        private System.Windows.Forms.NumericUpDown numFW;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.TextBox tbxLog;
        private System.Windows.Forms.Splitter splitter1;
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
        private System.Windows.Forms.NumericUpDown numFwdOverlap;
        private System.Windows.Forms.NumericUpDown numFwd;
        private System.Windows.Forms.TextBox tbxSurfDir;
        private System.Windows.Forms.Button btnLoadSurf;
        private System.Windows.Forms.CheckBox chkFromBack;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkDrawCenterLine;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox4;
        private ShimLib.ZoomPictureBox pbxDraw;
    }
}

