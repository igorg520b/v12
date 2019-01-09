namespace icFlow
{
    partial class ParametricStudyForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParametricStudyForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pgBeamParams = new System.Windows.Forms.PropertyGrid();
            this.pgModelParams = new System.Windows.Forms.PropertyGrid();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDefaultP = new System.Windows.Forms.Button();
            this.btnDefaultL = new System.Windows.Forms.Button();
            this.tbClassName = new System.Windows.Forms.TextBox();
            this.btnAssignName = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnDuplicate = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbParameters = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.nudSteps = new System.Windows.Forms.NumericUpDown();
            this.tbFrom = new System.Windows.Forms.TextBox();
            this.tbTo = new System.Windows.Forms.TextBox();
            this.tbStudyName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnInitialize = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.panelSetup = new System.Windows.Forms.Panel();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.panelRun = new System.Windows.Forms.Panel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lbSimulations = new System.Windows.Forms.ListBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbRun = new System.Windows.Forms.ToolStripButton();
            this.tsbExport = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tss1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSteps)).BeginInit();
            this.panel3.SuspendLayout();
            this.panelSetup.SuspendLayout();
            this.panelRun.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Cornsilk;
            this.panel1.Controls.Add(this.pgBeamParams);
            this.panel1.Controls.Add(this.pgModelParams);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(211, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(404, 354);
            this.panel1.TabIndex = 0;
            // 
            // pgBeamParams
            // 
            this.pgBeamParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgBeamParams.Location = new System.Drawing.Point(190, 136);
            this.pgBeamParams.Name = "pgBeamParams";
            this.pgBeamParams.Size = new System.Drawing.Size(214, 218);
            this.pgBeamParams.TabIndex = 2;
            // 
            // pgModelParams
            // 
            this.pgModelParams.Dock = System.Windows.Forms.DockStyle.Left;
            this.pgModelParams.Location = new System.Drawing.Point(0, 136);
            this.pgModelParams.Name = "pgModelParams";
            this.pgModelParams.Size = new System.Drawing.Size(190, 218);
            this.pgModelParams.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Controls.Add(this.btnDefaultP);
            this.panel2.Controls.Add(this.btnDefaultL);
            this.panel2.Controls.Add(this.tbClassName);
            this.panel2.Controls.Add(this.btnAssignName);
            this.panel2.Controls.Add(this.btnRemove);
            this.panel2.Controls.Add(this.btnDuplicate);
            this.panel2.Controls.Add(this.listBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(404, 136);
            this.panel2.TabIndex = 0;
            // 
            // btnDefaultP
            // 
            this.btnDefaultP.Location = new System.Drawing.Point(222, 14);
            this.btnDefaultP.Name = "btnDefaultP";
            this.btnDefaultP.Size = new System.Drawing.Size(75, 23);
            this.btnDefaultP.TabIndex = 12;
            this.btnDefaultP.Text = "Def-P";
            this.btnDefaultP.UseVisualStyleBackColor = true;
            this.btnDefaultP.Click += new System.EventHandler(this.btnDefaultP_Click);
            // 
            // btnDefaultL
            // 
            this.btnDefaultL.Location = new System.Drawing.Point(141, 14);
            this.btnDefaultL.Name = "btnDefaultL";
            this.btnDefaultL.Size = new System.Drawing.Size(75, 23);
            this.btnDefaultL.TabIndex = 11;
            this.btnDefaultL.Text = "Def-L";
            this.btnDefaultL.UseVisualStyleBackColor = true;
            this.btnDefaultL.Click += new System.EventHandler(this.btnDefaultL_Click);
            // 
            // tbClassName
            // 
            this.tbClassName.Location = new System.Drawing.Point(222, 101);
            this.tbClassName.Name = "tbClassName";
            this.tbClassName.Size = new System.Drawing.Size(100, 20);
            this.tbClassName.TabIndex = 10;
            // 
            // btnAssignName
            // 
            this.btnAssignName.Location = new System.Drawing.Point(141, 98);
            this.btnAssignName.Name = "btnAssignName";
            this.btnAssignName.Size = new System.Drawing.Size(75, 23);
            this.btnAssignName.TabIndex = 3;
            this.btnAssignName.Text = "Assign name";
            this.btnAssignName.UseVisualStyleBackColor = true;
            this.btnAssignName.Click += new System.EventHandler(this.btnAssignName_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(141, 69);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnDuplicate
            // 
            this.btnDuplicate.Location = new System.Drawing.Point(141, 40);
            this.btnDuplicate.Name = "btnDuplicate";
            this.btnDuplicate.Size = new System.Drawing.Size(75, 23);
            this.btnDuplicate.TabIndex = 1;
            this.btnDuplicate.Text = "Duplicate";
            this.btnDuplicate.UseVisualStyleBackColor = true;
            this.btnDuplicate.Click += new System.EventHandler(this.btnDuplicate_Click);
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(135, 136);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Parameter";
            // 
            // lbParameters
            // 
            this.lbParameters.FormattingEnabled = true;
            this.lbParameters.Items.AddRange(new object[] {
            "sigma",
            "ratio",
            "length",
            "width",
            "thickness",
            "resolution"});
            this.lbParameters.Location = new System.Drawing.Point(16, 72);
            this.lbParameters.Name = "lbParameters";
            this.lbParameters.Size = new System.Drawing.Size(93, 108);
            this.lbParameters.TabIndex = 2;
            this.lbParameters.SelectedIndexChanged += new System.EventHandler(this.lbParameters_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 202);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "From";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 228);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "To";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 256);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Steps";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(16, 280);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 7;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // nudSteps
            // 
            this.nudSteps.Location = new System.Drawing.Point(53, 254);
            this.nudSteps.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudSteps.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudSteps.Name = "nudSteps";
            this.nudSteps.Size = new System.Drawing.Size(81, 20);
            this.nudSteps.TabIndex = 8;
            this.nudSteps.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // tbFrom
            // 
            this.tbFrom.Location = new System.Drawing.Point(53, 202);
            this.tbFrom.Name = "tbFrom";
            this.tbFrom.Size = new System.Drawing.Size(100, 20);
            this.tbFrom.TabIndex = 9;
            this.tbFrom.Text = "100000";
            // 
            // tbTo
            // 
            this.tbTo.Location = new System.Drawing.Point(53, 228);
            this.tbTo.Name = "tbTo";
            this.tbTo.Size = new System.Drawing.Size(100, 20);
            this.tbTo.TabIndex = 10;
            this.tbTo.Text = "150000";
            // 
            // tbStudyName
            // 
            this.tbStudyName.Location = new System.Drawing.Point(89, 11);
            this.tbStudyName.Name = "tbStudyName";
            this.tbStudyName.Size = new System.Drawing.Size(100, 20);
            this.tbStudyName.TabIndex = 12;
            this.tbStudyName.Text = "study1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Study name:";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Moccasin;
            this.panel3.Controls.Add(this.btnLoad);
            this.panel3.Controls.Add(this.btnInitialize);
            this.panel3.Controls.Add(this.btnClear);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.tbStudyName);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.lbParameters);
            this.panel3.Controls.Add(this.tbTo);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.tbFrom);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.nudSteps);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.btnGenerate);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(211, 354);
            this.panel3.TabIndex = 13;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(97, 309);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 15;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnInitialize
            // 
            this.btnInitialize.Location = new System.Drawing.Point(97, 280);
            this.btnInitialize.Name = "btnInitialize";
            this.btnInitialize.Size = new System.Drawing.Size(75, 23);
            this.btnInitialize.TabIndex = 14;
            this.btnInitialize.Text = "Initialize Study";
            this.btnInitialize.UseVisualStyleBackColor = true;
            this.btnInitialize.Click += new System.EventHandler(this.btnInitialize_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(16, 309);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 13;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // panelSetup
            // 
            this.panelSetup.BackColor = System.Drawing.Color.LightBlue;
            this.panelSetup.Controls.Add(this.listBox2);
            this.panelSetup.Controls.Add(this.panel1);
            this.panelSetup.Controls.Add(this.panel3);
            this.panelSetup.Location = new System.Drawing.Point(12, 12);
            this.panelSetup.Name = "panelSetup";
            this.panelSetup.Size = new System.Drawing.Size(880, 354);
            this.panelSetup.TabIndex = 14;
            // 
            // listBox2
            // 
            this.listBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(615, 0);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(135, 354);
            this.listBox2.TabIndex = 14;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // panelRun
            // 
            this.panelRun.BackColor = System.Drawing.Color.PowderBlue;
            this.panelRun.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelRun.Controls.Add(this.chart1);
            this.panelRun.Controls.Add(this.lbSimulations);
            this.panelRun.Controls.Add(this.toolStrip1);
            this.panelRun.Location = new System.Drawing.Point(338, 383);
            this.panelRun.Name = "panelRun";
            this.panelRun.Padding = new System.Windows.Forms.Padding(2);
            this.panelRun.Size = new System.Drawing.Size(554, 305);
            this.panelRun.TabIndex = 15;
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(162, 27);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(388, 274);
            this.chart1.TabIndex = 5;
            this.chart1.Text = "chart1";
            // 
            // lbSimulations
            // 
            this.lbSimulations.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbSimulations.FormattingEnabled = true;
            this.lbSimulations.Location = new System.Drawing.Point(2, 27);
            this.lbSimulations.Name = "lbSimulations";
            this.lbSimulations.Size = new System.Drawing.Size(160, 274);
            this.lbSimulations.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRun,
            this.tsbExport});
            this.toolStrip1.Location = new System.Drawing.Point(2, 2);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(548, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbRun
            // 
            this.tsbRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbRun.Image = ((System.Drawing.Image)(resources.GetObject("tsbRun.Image")));
            this.tsbRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRun.Name = "tsbRun";
            this.tsbRun.Size = new System.Drawing.Size(68, 22);
            this.tsbRun.Text = "Run/Pause";
            this.tsbRun.Click += new System.EventHandler(this.tsbRun_Click);
            // 
            // tsbExport
            // 
            this.tsbExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbExport.Image = ((System.Drawing.Image)(resources.GetObject("tsbExport.Image")));
            this.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExport.Name = "tsbExport";
            this.tsbExport.Size = new System.Drawing.Size(44, 22);
            this.tsbExport.Text = "Export";
            this.tsbExport.Click += new System.EventHandler(this.tsbExport_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tss1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 723);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1165, 22);
            this.statusStrip1.TabIndex = 16;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tss1
            // 
            this.tss1.Name = "tss1";
            this.tss1.Size = new System.Drawing.Size(27, 17);
            this.tss1.Text = "tss1";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "XML|*.xml";
            // 
            // ParametricStudyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1165, 745);
            this.Controls.Add(this.panelRun);
            this.Controls.Add(this.panelSetup);
            this.Controls.Add(this.statusStrip1);
            this.Name = "ParametricStudyForm";
            this.Text = "PS_Setup";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PS_Setup_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSteps)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panelSetup.ResumeLayout(false);
            this.panelRun.ResumeLayout(false);
            this.panelRun.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PropertyGrid pgBeamParams;
        private System.Windows.Forms.PropertyGrid pgModelParams;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnDuplicate;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbParameters;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.NumericUpDown nudSteps;
        private System.Windows.Forms.TextBox tbFrom;
        private System.Windows.Forms.TextBox tbTo;
        private System.Windows.Forms.TextBox tbClassName;
        private System.Windows.Forms.Button btnAssignName;
        private System.Windows.Forms.TextBox tbStudyName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnDefaultP;
        private System.Windows.Forms.Button btnDefaultL;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panelSetup;
        private System.Windows.Forms.Panel panelRun;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbRun;
        private System.Windows.Forms.ToolStripButton tsbExport;
        private System.Windows.Forms.ListBox lbSimulations;
        public System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button btnInitialize;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tss1;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}