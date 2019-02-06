namespace IcyGrains
{
    partial class SetupGeneratorForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tools2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setupSimulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.l1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.l2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.l3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.l7ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.l8ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.l9ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plainBeamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.p1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.p2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.glControl1 = new OpenTK.GLControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.viewSelector = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.plain2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tools2ToolStripMenuItem,
            this.beamToolStripMenuItem,
            this.plainBeamToolStripMenuItem,
            this.plain2ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1001, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tools2ToolStripMenuItem
            // 
            this.tools2ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateToolStripMenuItem,
            this.setupSimulationToolStripMenuItem});
            this.tools2ToolStripMenuItem.Name = "tools2ToolStripMenuItem";
            this.tools2ToolStripMenuItem.Size = new System.Drawing.Size(47, 19);
            this.tools2ToolStripMenuItem.Text = "Tools";
            // 
            // generateToolStripMenuItem
            // 
            this.generateToolStripMenuItem.Name = "generateToolStripMenuItem";
            this.generateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.generateToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.generateToolStripMenuItem.Text = "Generate";
            this.generateToolStripMenuItem.Click += new System.EventHandler(this.generateToolStripMenuItem_Click);
            // 
            // setupSimulationToolStripMenuItem
            // 
            this.setupSimulationToolStripMenuItem.Name = "setupSimulationToolStripMenuItem";
            this.setupSimulationToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.setupSimulationToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.setupSimulationToolStripMenuItem.Text = "Setup simulation";
            this.setupSimulationToolStripMenuItem.Click += new System.EventHandler(this.setupSimulationToolStripMenuItem_Click);
            // 
            // beamToolStripMenuItem
            // 
            this.beamToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.l1ToolStripMenuItem,
            this.l2ToolStripMenuItem,
            this.l3ToolStripMenuItem,
            this.l7ToolStripMenuItem,
            this.l8ToolStripMenuItem,
            this.l9ToolStripMenuItem});
            this.beamToolStripMenuItem.Name = "beamToolStripMenuItem";
            this.beamToolStripMenuItem.Size = new System.Drawing.Size(55, 19);
            this.beamToolStripMenuItem.Text = "LBeam";
            // 
            // l1ToolStripMenuItem
            // 
            this.l1ToolStripMenuItem.Name = "l1ToolStripMenuItem";
            this.l1ToolStripMenuItem.Size = new System.Drawing.Size(86, 22);
            this.l1ToolStripMenuItem.Tag = "1";
            this.l1ToolStripMenuItem.Text = "L1";
            this.l1ToolStripMenuItem.Click += new System.EventHandler(this.l1ToolStripMenuItem_Click);
            // 
            // l2ToolStripMenuItem
            // 
            this.l2ToolStripMenuItem.Name = "l2ToolStripMenuItem";
            this.l2ToolStripMenuItem.Size = new System.Drawing.Size(86, 22);
            this.l2ToolStripMenuItem.Tag = "2";
            this.l2ToolStripMenuItem.Text = "L2";
            this.l2ToolStripMenuItem.Click += new System.EventHandler(this.l1ToolStripMenuItem_Click);
            // 
            // l3ToolStripMenuItem
            // 
            this.l3ToolStripMenuItem.Name = "l3ToolStripMenuItem";
            this.l3ToolStripMenuItem.Size = new System.Drawing.Size(86, 22);
            this.l3ToolStripMenuItem.Tag = "3";
            this.l3ToolStripMenuItem.Text = "L3";
            this.l3ToolStripMenuItem.Click += new System.EventHandler(this.l1ToolStripMenuItem_Click);
            // 
            // l7ToolStripMenuItem
            // 
            this.l7ToolStripMenuItem.Name = "l7ToolStripMenuItem";
            this.l7ToolStripMenuItem.Size = new System.Drawing.Size(86, 22);
            this.l7ToolStripMenuItem.Tag = "7";
            this.l7ToolStripMenuItem.Text = "L7";
            this.l7ToolStripMenuItem.Click += new System.EventHandler(this.l1ToolStripMenuItem_Click);
            // 
            // l8ToolStripMenuItem
            // 
            this.l8ToolStripMenuItem.Name = "l8ToolStripMenuItem";
            this.l8ToolStripMenuItem.Size = new System.Drawing.Size(86, 22);
            this.l8ToolStripMenuItem.Tag = "8";
            this.l8ToolStripMenuItem.Text = "L8";
            this.l8ToolStripMenuItem.Click += new System.EventHandler(this.l1ToolStripMenuItem_Click);
            // 
            // l9ToolStripMenuItem
            // 
            this.l9ToolStripMenuItem.Name = "l9ToolStripMenuItem";
            this.l9ToolStripMenuItem.Size = new System.Drawing.Size(86, 22);
            this.l9ToolStripMenuItem.Tag = "9";
            this.l9ToolStripMenuItem.Text = "L9";
            this.l9ToolStripMenuItem.Click += new System.EventHandler(this.l1ToolStripMenuItem_Click);
            // 
            // plainBeamToolStripMenuItem
            // 
            this.plainBeamToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.p1ToolStripMenuItem,
            this.p2ToolStripMenuItem});
            this.plainBeamToolStripMenuItem.Name = "plainBeamToolStripMenuItem";
            this.plainBeamToolStripMenuItem.Size = new System.Drawing.Size(75, 19);
            this.plainBeamToolStripMenuItem.Text = "PlainBeam";
            // 
            // p1ToolStripMenuItem
            // 
            this.p1ToolStripMenuItem.Name = "p1ToolStripMenuItem";
            this.p1ToolStripMenuItem.Size = new System.Drawing.Size(87, 22);
            this.p1ToolStripMenuItem.Tag = "1";
            this.p1ToolStripMenuItem.Text = "P1";
            this.p1ToolStripMenuItem.Click += new System.EventHandler(this.p1ToolStripMenuItem_Click);
            // 
            // p2ToolStripMenuItem
            // 
            this.p2ToolStripMenuItem.Name = "p2ToolStripMenuItem";
            this.p2ToolStripMenuItem.Size = new System.Drawing.Size(87, 22);
            this.p2ToolStripMenuItem.Tag = "2";
            this.p2ToolStripMenuItem.Text = "P2";
            this.p2ToolStripMenuItem.Click += new System.EventHandler(this.p1ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 440);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1001, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.propertyGrid1);
            this.panel1.Controls.Add(this.listBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(225, 390);
            this.panel1.TabIndex = 2;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.HelpVisible = false;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 104);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(225, 286);
            this.propertyGrid1.TabIndex = 1;
            this.propertyGrid1.ToolbarVisible = false;
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Items.AddRange(new object[] {
            "params",
            "indenter",
            "mesh"});
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(225, 104);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.Controls.Add(this.glControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(225, 50);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(776, 390);
            this.panel2.TabIndex = 3;
            // 
            // glControl1
            // 
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControl1.Location = new System.Drawing.Point(0, 0);
            this.glControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(776, 390);
            this.glControl1.TabIndex = 0;
            this.glControl1.VSync = false;
            this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
            this.glControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseDown);
            this.glControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseMove);
            this.glControl1.Resize += new System.EventHandler(this.glControl1_Resize);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewSelector,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1001, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // viewSelector
            // 
            this.viewSelector.Items.AddRange(new object[] {
            "surface",
            "grains",
            "mesh"});
            this.viewSelector.Name = "viewSelector";
            this.viewSelector.Size = new System.Drawing.Size(100, 25);
            this.viewSelector.Text = "grains";
            this.viewSelector.SelectedIndexChanged += new System.EventHandler(this.viewSelector_TextUpdate);
            this.viewSelector.TextUpdate += new System.EventHandler(this.viewSelector_TextUpdate);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // plain2ToolStripMenuItem
            // 
            this.plain2ToolStripMenuItem.Name = "plain2ToolStripMenuItem";
            this.plain2ToolStripMenuItem.Size = new System.Drawing.Size(51, 19);
            this.plain2ToolStripMenuItem.Text = "Plain2";
            this.plain2ToolStripMenuItem.Click += new System.EventHandler(this.plain2ToolStripMenuItem_Click);
            // 
            // SetupGeneratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 462);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "SetupGeneratorForm";
            this.Text = "IcyGrains3: L-shaped beam";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SetupGeneratorForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Panel panel2;
        private OpenTK.GLControl glControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox viewSelector;
        private System.Windows.Forms.ToolStripMenuItem tools2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem l1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem l2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem l3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem l7ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem l8ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem l9ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setupSimulationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem plainBeamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem p1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem p2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem plain2ToolStripMenuItem;
    }
}

