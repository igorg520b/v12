namespace icFlow
{
    partial class ShearTestSetupForm
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
            this.Setup = new System.Windows.Forms.Button();
            this.rbPoint2 = new System.Windows.Forms.RadioButton();
            this.rb2 = new System.Windows.Forms.RadioButton();
            this.rb20 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudInner = new System.Windows.Forms.NumericUpDown();
            this.nudOuter = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudInner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOuter)).BeginInit();
            this.SuspendLayout();
            // 
            // Setup
            // 
            this.Setup.Location = new System.Drawing.Point(100, 219);
            this.Setup.Name = "Setup";
            this.Setup.Size = new System.Drawing.Size(75, 23);
            this.Setup.TabIndex = 0;
            this.Setup.Text = "Setup";
            this.Setup.UseVisualStyleBackColor = true;
            this.Setup.Click += new System.EventHandler(this.Setup_Click);
            // 
            // rbPoint2
            // 
            this.rbPoint2.AutoSize = true;
            this.rbPoint2.Checked = true;
            this.rbPoint2.Location = new System.Drawing.Point(17, 21);
            this.rbPoint2.Name = "rbPoint2";
            this.rbPoint2.Size = new System.Drawing.Size(63, 17);
            this.rbPoint2.TabIndex = 1;
            this.rbPoint2.TabStop = true;
            this.rbPoint2.Text = ".2 mm/s";
            this.rbPoint2.UseVisualStyleBackColor = true;
            // 
            // rb2
            // 
            this.rb2.AutoSize = true;
            this.rb2.Location = new System.Drawing.Point(17, 44);
            this.rb2.Name = "rb2";
            this.rb2.Size = new System.Drawing.Size(60, 17);
            this.rb2.TabIndex = 2;
            this.rb2.Text = "2 mm/s";
            this.rb2.UseVisualStyleBackColor = true;
            // 
            // rb20
            // 
            this.rb20.AutoSize = true;
            this.rb20.Location = new System.Drawing.Point(17, 67);
            this.rb20.Name = "rb20";
            this.rb20.Size = new System.Drawing.Size(66, 17);
            this.rb20.TabIndex = 3;
            this.rb20.Text = "20 mm/s";
            this.rb20.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rb20);
            this.groupBox1.Controls.Add(this.rb2);
            this.groupBox1.Controls.Add(this.rbPoint2);
            this.groupBox1.Location = new System.Drawing.Point(21, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(154, 101);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Indentation Rate";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 146);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Inner indenter position (mm)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 182);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Outer indenter position (mm)";
            // 
            // nudInner
            // 
            this.nudInner.Location = new System.Drawing.Point(173, 146);
            this.nudInner.Name = "nudInner";
            this.nudInner.Size = new System.Drawing.Size(73, 20);
            this.nudInner.TabIndex = 6;
            this.nudInner.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // nudOuter
            // 
            this.nudOuter.Location = new System.Drawing.Point(173, 180);
            this.nudOuter.Name = "nudOuter";
            this.nudOuter.Size = new System.Drawing.Size(73, 20);
            this.nudOuter.TabIndex = 7;
            this.nudOuter.Value = new decimal(new int[] {
            75,
            0,
            0,
            0});
            // 
            // ShearTestSetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 264);
            this.Controls.Add(this.nudOuter);
            this.Controls.Add(this.nudInner);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Setup);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShearTestSetupForm";
            this.Text = "ShearTestSetupForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ShearTestSetupForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudInner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOuter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Setup;
        private System.Windows.Forms.RadioButton rbPoint2;
        private System.Windows.Forms.RadioButton rb2;
        private System.Windows.Forms.RadioButton rb20;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudInner;
        private System.Windows.Forms.NumericUpDown nudOuter;
    }
}