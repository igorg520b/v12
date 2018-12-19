namespace icFlow
{
    partial class PS_Setup
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
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSteps)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Cornsilk;
            this.panel1.Controls.Add(this.pgBeamParams);
            this.panel1.Controls.Add(this.pgModelParams);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(404, 476);
            this.panel1.TabIndex = 0;
            // 
            // pgBeamParams
            // 
            this.pgBeamParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgBeamParams.Location = new System.Drawing.Point(190, 136);
            this.pgBeamParams.Name = "pgBeamParams";
            this.pgBeamParams.Size = new System.Drawing.Size(214, 340);
            this.pgBeamParams.TabIndex = 2;
            // 
            // pgModelParams
            // 
            this.pgModelParams.Dock = System.Windows.Forms.DockStyle.Left;
            this.pgModelParams.Location = new System.Drawing.Point(0, 136);
            this.pgModelParams.Name = "pgModelParams";
            this.pgModelParams.Size = new System.Drawing.Size(190, 340);
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
            this.label1.Location = new System.Drawing.Point(439, 53);
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
            this.lbParameters.Location = new System.Drawing.Point(442, 77);
            this.lbParameters.Name = "lbParameters";
            this.lbParameters.Size = new System.Drawing.Size(93, 108);
            this.lbParameters.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(439, 207);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "From";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(439, 242);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "To";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(439, 278);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Steps";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(479, 333);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 7;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // nudSteps
            // 
            this.nudSteps.Location = new System.Drawing.Point(479, 276);
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
            this.tbFrom.Location = new System.Drawing.Point(479, 207);
            this.tbFrom.Name = "tbFrom";
            this.tbFrom.Size = new System.Drawing.Size(100, 20);
            this.tbFrom.TabIndex = 9;
            this.tbFrom.Text = "100000";
            // 
            // tbTo
            // 
            this.tbTo.Location = new System.Drawing.Point(479, 242);
            this.tbTo.Name = "tbTo";
            this.tbTo.Size = new System.Drawing.Size(100, 20);
            this.tbTo.TabIndex = 10;
            this.tbTo.Text = "150000";
            // 
            // tbStudyName
            // 
            this.tbStudyName.Location = new System.Drawing.Point(515, 16);
            this.tbStudyName.Name = "tbStudyName";
            this.tbStudyName.Size = new System.Drawing.Size(100, 20);
            this.tbStudyName.TabIndex = 12;
            this.tbStudyName.Text = "study1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(443, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Study name:";
            // 
            // PS_Setup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 476);
            this.Controls.Add(this.tbStudyName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbTo);
            this.Controls.Add(this.tbFrom);
            this.Controls.Add(this.nudSteps);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbParameters);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "PS_Setup";
            this.Text = "PS_Setup";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PS_Setup_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSteps)).EndInit();
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
    }
}