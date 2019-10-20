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
            this.btnAlloc = new System.Windows.Forms.Button();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.tbxFwdDir = new System.Windows.Forms.TextBox();
            this.btnResetImg = new System.Windows.Forms.Button();
            this.numFNum = new System.Windows.Forms.NumericUpDown();
            this.btnFwdDir = new System.Windows.Forms.Button();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.btnLoadFwd = new System.Windows.Forms.Button();
            this.numFH = new System.Windows.Forms.NumericUpDown();
            this.numFW = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.tbxLog = new System.Windows.Forms.TextBox();
            this.pbxDraw = new System.Windows.Forms.PictureBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.dlgFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDraw)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.splitter2);
            this.panel1.Controls.Add(this.tbxLog);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(663, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(336, 600);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnAlloc);
            this.panel2.Controls.Add(this.btnClearLog);
            this.panel2.Controls.Add(this.tbxFwdDir);
            this.panel2.Controls.Add(this.btnResetImg);
            this.panel2.Controls.Add(this.numFNum);
            this.panel2.Controls.Add(this.btnFwdDir);
            this.panel2.Controls.Add(this.numericUpDown2);
            this.panel2.Controls.Add(this.btnLoadFwd);
            this.panel2.Controls.Add(this.numFH);
            this.panel2.Controls.Add(this.numFW);
            this.panel2.Controls.Add(this.numericUpDown1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(336, 411);
            this.panel2.TabIndex = 5;
            // 
            // btnAlloc
            // 
            this.btnAlloc.Location = new System.Drawing.Point(3, 3);
            this.btnAlloc.Name = "btnAlloc";
            this.btnAlloc.Size = new System.Drawing.Size(75, 23);
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
            // 
            // tbxFwdDir
            // 
            this.tbxFwdDir.Location = new System.Drawing.Point(84, 34);
            this.tbxFwdDir.Name = "tbxFwdDir";
            this.tbxFwdDir.Size = new System.Drawing.Size(213, 21);
            this.tbxFwdDir.TabIndex = 4;
            // 
            // btnResetImg
            // 
            this.btnResetImg.Location = new System.Drawing.Point(3, 90);
            this.btnResetImg.Name = "btnResetImg";
            this.btnResetImg.Size = new System.Drawing.Size(75, 23);
            this.btnResetImg.TabIndex = 1;
            this.btnResetImg.Text = "Reset Img";
            this.btnResetImg.UseVisualStyleBackColor = true;
            // 
            // numFNum
            // 
            this.numFNum.Location = new System.Drawing.Point(230, 6);
            this.numFNum.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numFNum.Name = "numFNum";
            this.numFNum.Size = new System.Drawing.Size(67, 21);
            this.numFNum.TabIndex = 3;
            this.numFNum.Value = new decimal(new int[] {
            600,
            0,
            0,
            0});
            // 
            // btnFwdDir
            // 
            this.btnFwdDir.Location = new System.Drawing.Point(3, 32);
            this.btnFwdDir.Name = "btnFwdDir";
            this.btnFwdDir.Size = new System.Drawing.Size(75, 23);
            this.btnFwdDir.TabIndex = 1;
            this.btnFwdDir.Text = "Fwd Dir";
            this.btnFwdDir.UseVisualStyleBackColor = true;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(157, 93);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(67, 21);
            this.numericUpDown2.TabIndex = 3;
            // 
            // btnLoadFwd
            // 
            this.btnLoadFwd.Location = new System.Drawing.Point(3, 61);
            this.btnLoadFwd.Name = "btnLoadFwd";
            this.btnLoadFwd.Size = new System.Drawing.Size(75, 23);
            this.btnLoadFwd.TabIndex = 2;
            this.btnLoadFwd.Text = "Load Fwd";
            this.btnLoadFwd.UseVisualStyleBackColor = true;
            // 
            // numFH
            // 
            this.numFH.Location = new System.Drawing.Point(157, 6);
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
            this.numFW.Location = new System.Drawing.Point(84, 6);
            this.numFW.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numFW.Name = "numFW";
            this.numFW.Size = new System.Drawing.Size(67, 21);
            this.numFW.TabIndex = 3;
            this.numFW.Value = new decimal(new int[] {
            5552,
            0,
            0,
            0});
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(84, 93);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(67, 21);
            this.numericUpDown1.TabIndex = 3;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter2.Location = new System.Drawing.Point(0, 411);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(336, 3);
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
            this.tbxLog.Size = new System.Drawing.Size(336, 186);
            this.tbxLog.TabIndex = 0;
            this.tbxLog.WordWrap = false;
            // 
            // pbxDraw
            // 
            this.pbxDraw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxDraw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxDraw.Location = new System.Drawing.Point(0, 0);
            this.pbxDraw.Name = "pbxDraw";
            this.pbxDraw.Size = new System.Drawing.Size(660, 600);
            this.pbxDraw.TabIndex = 1;
            this.pbxDraw.TabStop = false;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(660, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 600);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
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
            ((System.ComponentModel.ISupportInitialize)(this.numFNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDraw)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnAlloc;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.TextBox tbxFwdDir;
        private System.Windows.Forms.Button btnResetImg;
        private System.Windows.Forms.NumericUpDown numFNum;
        private System.Windows.Forms.Button btnFwdDir;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Button btnLoadFwd;
        private System.Windows.Forms.NumericUpDown numFH;
        private System.Windows.Forms.NumericUpDown numFW;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.TextBox tbxLog;
        private System.Windows.Forms.PictureBox pbxDraw;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.FolderBrowserDialog dlgFolder;
    }
}

