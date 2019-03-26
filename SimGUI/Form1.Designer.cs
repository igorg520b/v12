namespace icFlow
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSimulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openClearSimToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addMeshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveInitialStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.takeScreenshotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renderSimulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.writeCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.continueFromLastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oneStepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eraseSubsequentStepsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.pPRRelationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.setupGenerationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.parametricStudyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            this.setUpShearTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssCurrentFrame = new System.Windows.Forms.ToolStripStatusLabel();
            this.glControl1 = new OpenTK.GLControl();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.pg = new System.Windows.Forms.PropertyGrid();
            this.tsDisplayOptions = new System.Windows.Forms.ToolStrip();
            this.tsbRenderPanel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPhi = new System.Windows.Forms.ToolStripButton();
            this.tsbAxes = new System.Windows.Forms.ToolStripButton();
            this.tsbPreviewMode = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbTopView = new System.Windows.Forms.ToolStripButton();
            this.tsbBottomView = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.cmsTranslations = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsOneTranslation = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsMesh = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cZsAndCapsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cZsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.splitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.resizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotate90DegToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.removeToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialogMeshes = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.centerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.tsDisplayOptions.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.cmsTranslations.SuspendLayout();
            this.cmsOneTranslation.SuspendLayout();
            this.cmsMesh.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.simulationToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1012, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openSimulationToolStripMenuItem,
            this.openClearSimToolStripMenuItem,
            this.addMeshToolStripMenuItem,
            this.toolStripMenuItem2,
            this.saveInitialStateToolStripMenuItem,
            this.toolStripMenuItem1,
            this.takeScreenshotToolStripMenuItem,
            this.renderSimulationToolStripMenuItem,
            this.writeCSVToolStripMenuItem,
            this.toolStripMenuItem5,
            this.clearToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openSimulationToolStripMenuItem
            // 
            this.openSimulationToolStripMenuItem.Name = "openSimulationToolStripMenuItem";
            this.openSimulationToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
            this.openSimulationToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.openSimulationToolStripMenuItem.Text = "Open Simulation";
            this.openSimulationToolStripMenuItem.Click += new System.EventHandler(this.openSimulationToolStripMenuItem_Click);
            // 
            // openClearSimToolStripMenuItem
            // 
            this.openClearSimToolStripMenuItem.Name = "openClearSimToolStripMenuItem";
            this.openClearSimToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openClearSimToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.openClearSimToolStripMenuItem.Text = "Open Clear Sim";
            this.openClearSimToolStripMenuItem.Click += new System.EventHandler(this.openClearSimToolStripMenuItem_Click);
            // 
            // addMeshToolStripMenuItem
            // 
            this.addMeshToolStripMenuItem.Name = "addMeshToolStripMenuItem";
            this.addMeshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.addMeshToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.addMeshToolStripMenuItem.Text = "Add Mesh";
            this.addMeshToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(235, 6);
            // 
            // saveInitialStateToolStripMenuItem
            // 
            this.saveInitialStateToolStripMenuItem.Name = "saveInitialStateToolStripMenuItem";
            this.saveInitialStateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveInitialStateToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.saveInitialStateToolStripMenuItem.Text = "Save Initial State";
            this.saveInitialStateToolStripMenuItem.Click += new System.EventHandler(this.saveInitialStateToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(235, 6);
            // 
            // takeScreenshotToolStripMenuItem
            // 
            this.takeScreenshotToolStripMenuItem.Name = "takeScreenshotToolStripMenuItem";
            this.takeScreenshotToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.takeScreenshotToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.takeScreenshotToolStripMenuItem.Text = "Take Screenshot";
            this.takeScreenshotToolStripMenuItem.Click += new System.EventHandler(this.takeScreenshotToolStripMenuItem_Click);
            // 
            // renderSimulationToolStripMenuItem
            // 
            this.renderSimulationToolStripMenuItem.Name = "renderSimulationToolStripMenuItem";
            this.renderSimulationToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F12)));
            this.renderSimulationToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.renderSimulationToolStripMenuItem.Text = "Render Simulation";
            this.renderSimulationToolStripMenuItem.Click += new System.EventHandler(this.renderSimulationToolStripMenuItem_Click);
            // 
            // writeCSVToolStripMenuItem
            // 
            this.writeCSVToolStripMenuItem.Name = "writeCSVToolStripMenuItem";
            this.writeCSVToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.writeCSVToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.writeCSVToolStripMenuItem.Text = "Write CSV";
            this.writeCSVToolStripMenuItem.Click += new System.EventHandler(this.writeCSVToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(235, 6);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // simulationToolStripMenuItem
            // 
            this.simulationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.continueFromLastToolStripMenuItem,
            this.oneStepToolStripMenuItem});
            this.simulationToolStripMenuItem.Name = "simulationToolStripMenuItem";
            this.simulationToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.simulationToolStripMenuItem.Text = "Simulation";
            // 
            // continueFromLastToolStripMenuItem
            // 
            this.continueFromLastToolStripMenuItem.Name = "continueFromLastToolStripMenuItem";
            this.continueFromLastToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.continueFromLastToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.continueFromLastToolStripMenuItem.Text = "Continue/Pause";
            this.continueFromLastToolStripMenuItem.Click += new System.EventHandler(this.continueFromLastToolStripMenuItem_Click);
            // 
            // oneStepToolStripMenuItem
            // 
            this.oneStepToolStripMenuItem.Name = "oneStepToolStripMenuItem";
            this.oneStepToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.oneStepToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.oneStepToolStripMenuItem.Text = "One step";
            this.oneStepToolStripMenuItem.Click += new System.EventHandler(this.oneStepToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.analysisToolStripMenuItem,
            this.eraseSubsequentStepsToolStripMenuItem,
            this.toolStripMenuItem3,
            this.pPRRelationsToolStripMenuItem,
            this.toolStripMenuItem4,
            this.setupGenerationToolStripMenuItem,
            this.toolStripMenuItem8,
            this.parametricStudyToolStripMenuItem,
            this.toolStripMenuItem9,
            this.setUpShearTestToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // analysisToolStripMenuItem
            // 
            this.analysisToolStripMenuItem.Name = "analysisToolStripMenuItem";
            this.analysisToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.analysisToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.analysisToolStripMenuItem.Text = "Analysis";
            this.analysisToolStripMenuItem.Click += new System.EventHandler(this.analysisToolStripMenuItem_Click);
            // 
            // eraseSubsequentStepsToolStripMenuItem
            // 
            this.eraseSubsequentStepsToolStripMenuItem.Name = "eraseSubsequentStepsToolStripMenuItem";
            this.eraseSubsequentStepsToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.eraseSubsequentStepsToolStripMenuItem.Text = "Trim";
            this.eraseSubsequentStepsToolStripMenuItem.Click += new System.EventHandler(this.eraseSubsequentStepsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(213, 6);
            // 
            // pPRRelationsToolStripMenuItem
            // 
            this.pPRRelationsToolStripMenuItem.Name = "pPRRelationsToolStripMenuItem";
            this.pPRRelationsToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.pPRRelationsToolStripMenuItem.Text = "PPR Relations...";
            this.pPRRelationsToolStripMenuItem.Click += new System.EventHandler(this.pPRRelationsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(213, 6);
            // 
            // setupGenerationToolStripMenuItem
            // 
            this.setupGenerationToolStripMenuItem.Name = "setupGenerationToolStripMenuItem";
            this.setupGenerationToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.setupGenerationToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.setupGenerationToolStripMenuItem.Text = "Beam Generation...";
            this.setupGenerationToolStripMenuItem.Click += new System.EventHandler(this.setupGenerationToolStripMenuItem_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(213, 6);
            // 
            // parametricStudyToolStripMenuItem
            // 
            this.parametricStudyToolStripMenuItem.Name = "parametricStudyToolStripMenuItem";
            this.parametricStudyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.parametricStudyToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.parametricStudyToolStripMenuItem.Text = "Parametric Study...";
            this.parametricStudyToolStripMenuItem.Click += new System.EventHandler(this.parametricStudyToolStripMenuItem_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(213, 6);
            // 
            // setUpShearTestToolStripMenuItem
            // 
            this.setUpShearTestToolStripMenuItem.Name = "setUpShearTestToolStripMenuItem";
            this.setUpShearTestToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.setUpShearTestToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.setUpShearTestToolStripMenuItem.Text = "Set up shear test...";
            this.setUpShearTestToolStripMenuItem.Click += new System.EventHandler(this.setUpShearTestToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssStatus,
            this.tssCurrentFrame});
            this.statusStrip1.Location = new System.Drawing.Point(0, 509);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1012, 26);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssStatus
            // 
            this.tssStatus.Name = "tssStatus";
            this.tssStatus.Size = new System.Drawing.Size(16, 21);
            this.tssStatus.Text = "-";
            // 
            // tssCurrentFrame
            // 
            this.tssCurrentFrame.Name = "tssCurrentFrame";
            this.tssCurrentFrame.Size = new System.Drawing.Size(16, 21);
            this.tssCurrentFrame.Text = "-";
            // 
            // glControl1
            // 
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControl1.Location = new System.Drawing.Point(0, 0);
            this.glControl1.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(830, 411);
            this.glControl1.TabIndex = 0;
            this.glControl1.VSync = false;
            this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
            this.glControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseDown);
            this.glControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseMove);
            this.glControl1.Resize += new System.EventHandler(this.glControl1_Resize);
            // 
            // trackBar1
            // 
            this.trackBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.trackBar1.Enabled = false;
            this.trackBar1.LargeChange = 1;
            this.trackBar1.Location = new System.Drawing.Point(0, 25);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(2);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(830, 45);
            this.trackBar1.TabIndex = 0;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // pg
            // 
            this.pg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pg.LineColor = System.Drawing.SystemColors.ControlDark;
            this.pg.Location = new System.Drawing.Point(3, 128);
            this.pg.Name = "pg";
            this.pg.Size = new System.Drawing.Size(172, 354);
            this.pg.TabIndex = 9;
            this.pg.ToolbarVisible = false;
            // 
            // tsDisplayOptions
            // 
            this.tsDisplayOptions.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tsDisplayOptions.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tsDisplayOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRenderPanel,
            this.toolStripSeparator5,
            this.tsbPhi,
            this.tsbAxes,
            this.tsbPreviewMode,
            this.toolStripSeparator1,
            this.tsbTopView,
            this.tsbBottomView});
            this.tsDisplayOptions.Location = new System.Drawing.Point(0, 0);
            this.tsDisplayOptions.Name = "tsDisplayOptions";
            this.tsDisplayOptions.Size = new System.Drawing.Size(830, 25);
            this.tsDisplayOptions.TabIndex = 10;
            this.tsDisplayOptions.Text = "toolStrip3";
            // 
            // tsbRenderPanel
            // 
            this.tsbRenderPanel.Checked = true;
            this.tsbRenderPanel.CheckOnClick = true;
            this.tsbRenderPanel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbRenderPanel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbRenderPanel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRenderPanel.Name = "tsbRenderPanel";
            this.tsbRenderPanel.Size = new System.Drawing.Size(65, 22);
            this.tsbRenderPanel.Text = "Rendering";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbPhi
            // 
            this.tsbPhi.CheckOnClick = true;
            this.tsbPhi.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbPhi.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPhi.Name = "tsbPhi";
            this.tsbPhi.Size = new System.Drawing.Size(28, 22);
            this.tsbPhi.Text = "Phi";
            this.tsbPhi.Click += new System.EventHandler(this.tsbInvalidate_Click);
            // 
            // tsbAxes
            // 
            this.tsbAxes.CheckOnClick = true;
            this.tsbAxes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbAxes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAxes.Name = "tsbAxes";
            this.tsbAxes.Size = new System.Drawing.Size(35, 22);
            this.tsbAxes.Text = "Axes";
            // 
            // tsbPreviewMode
            // 
            this.tsbPreviewMode.CheckOnClick = true;
            this.tsbPreviewMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbPreviewMode.Image = ((System.Drawing.Image)(resources.GetObject("tsbPreviewMode.Image")));
            this.tsbPreviewMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPreviewMode.Name = "tsbPreviewMode";
            this.tsbPreviewMode.Size = new System.Drawing.Size(52, 22);
            this.tsbPreviewMode.Text = "Preview";
            this.tsbPreviewMode.Click += new System.EventHandler(this.tsbPreviewMode_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbTopView
            // 
            this.tsbTopView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbTopView.Image = ((System.Drawing.Image)(resources.GetObject("tsbTopView.Image")));
            this.tsbTopView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTopView.Name = "tsbTopView";
            this.tsbTopView.Size = new System.Drawing.Size(31, 22);
            this.tsbTopView.Text = "Top";
            this.tsbTopView.Click += new System.EventHandler(this.tsbTopView_Click);
            // 
            // tsbBottomView
            // 
            this.tsbBottomView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbBottomView.Image = ((System.Drawing.Image)(resources.GetObject("tsbBottomView.Image")));
            this.tsbBottomView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBottomView.Name = "tsbBottomView";
            this.tsbBottomView.Size = new System.Drawing.Size(51, 22);
            this.tsbBottomView.Text = "Bottom";
            this.tsbBottomView.Click += new System.EventHandler(this.tsbBottomView_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.64706F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82.35294F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pg, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.treeView1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.88997F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 74.11003F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1012, 485);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.trackBar1);
            this.panel1.Controls.Add(this.tsDisplayOptions);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(180, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 2);
            this.panel1.Size = new System.Drawing.Size(830, 481);
            this.panel1.TabIndex = 12;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panel2.Controls.Add(this.glControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 70);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(830, 411);
            this.panel2.TabIndex = 12;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.treeView1.Location = new System.Drawing.Point(2, 2);
            this.treeView1.Margin = new System.Windows.Forms.Padding(2);
            this.treeView1.Name = "treeView1";
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.Size = new System.Drawing.Size(174, 121);
            this.treeView1.TabIndex = 10;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // cmsTranslations
            // 
            this.cmsTranslations.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.cmsTranslations.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem1,
            this.refreshToolStripMenuItem});
            this.cmsTranslations.Name = "cmsTranslations";
            this.cmsTranslations.Size = new System.Drawing.Size(114, 48);
            // 
            // addToolStripMenuItem1
            // 
            this.addToolStripMenuItem1.Name = "addToolStripMenuItem1";
            this.addToolStripMenuItem1.Size = new System.Drawing.Size(113, 22);
            this.addToolStripMenuItem1.Text = "Add";
            this.addToolStripMenuItem1.Click += new System.EventHandler(this.addToolStripMenuItem1_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // cmsOneTranslation
            // 
            this.cmsOneTranslation.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.cmsOneTranslation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem1});
            this.cmsOneTranslation.Name = "cmsTranslations";
            this.cmsOneTranslation.Size = new System.Drawing.Size(118, 26);
            // 
            // removeToolStripMenuItem1
            // 
            this.removeToolStripMenuItem1.Name = "removeToolStripMenuItem1";
            this.removeToolStripMenuItem1.Size = new System.Drawing.Size(117, 22);
            this.removeToolStripMenuItem1.Text = "Remove";
            this.removeToolStripMenuItem1.Click += new System.EventHandler(this.removeToolStripMenuItem1_Click);
            // 
            // cmsMesh
            // 
            this.cmsMesh.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.cmsMesh.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cZsAndCapsToolStripMenuItem,
            this.cZsToolStripMenuItem1,
            this.splitToolStripMenuItem1,
            this.toolStripMenuItem6,
            this.resizeToolStripMenuItem,
            this.rotate90DegToolStripMenuItem,
            this.rotateXToolStripMenuItem,
            this.centerToolStripMenuItem,
            this.toolStripMenuItem7,
            this.removeToolStripMenuItem2});
            this.cmsMesh.Name = "cmsMesh";
            this.cmsMesh.Size = new System.Drawing.Size(181, 214);
            // 
            // cZsAndCapsToolStripMenuItem
            // 
            this.cZsAndCapsToolStripMenuItem.Name = "cZsAndCapsToolStripMenuItem";
            this.cZsAndCapsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cZsAndCapsToolStripMenuItem.Text = "CZs and Caps";
            this.cZsAndCapsToolStripMenuItem.Click += new System.EventHandler(this.cZsAndCapsToolStripMenuItem_Click);
            // 
            // cZsToolStripMenuItem1
            // 
            this.cZsToolStripMenuItem1.Name = "cZsToolStripMenuItem1";
            this.cZsToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.cZsToolStripMenuItem1.Text = "CZs";
            this.cZsToolStripMenuItem1.Click += new System.EventHandler(this.cZsToolStripMenuItem1_Click);
            // 
            // splitToolStripMenuItem1
            // 
            this.splitToolStripMenuItem1.Name = "splitToolStripMenuItem1";
            this.splitToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.splitToolStripMenuItem1.Text = "Split";
            this.splitToolStripMenuItem1.Click += new System.EventHandler(this.splitToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(177, 6);
            // 
            // resizeToolStripMenuItem
            // 
            this.resizeToolStripMenuItem.Name = "resizeToolStripMenuItem";
            this.resizeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.resizeToolStripMenuItem.Text = "Resize";
            this.resizeToolStripMenuItem.Click += new System.EventHandler(this.resizeToolStripMenuItem_Click);
            // 
            // rotate90DegToolStripMenuItem
            // 
            this.rotate90DegToolStripMenuItem.Name = "rotate90DegToolStripMenuItem";
            this.rotate90DegToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.rotate90DegToolStripMenuItem.Text = "Rotate90Deg";
            this.rotate90DegToolStripMenuItem.Click += new System.EventHandler(this.rotate90DegToolStripMenuItem_Click);
            // 
            // rotateXToolStripMenuItem
            // 
            this.rotateXToolStripMenuItem.Name = "rotateXToolStripMenuItem";
            this.rotateXToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.rotateXToolStripMenuItem.Text = "RotateX";
            this.rotateXToolStripMenuItem.Click += new System.EventHandler(this.rotateXToolStripMenuItem_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(177, 6);
            // 
            // removeToolStripMenuItem2
            // 
            this.removeToolStripMenuItem2.Name = "removeToolStripMenuItem2";
            this.removeToolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.removeToolStripMenuItem2.Text = "Remove";
            this.removeToolStripMenuItem2.Click += new System.EventHandler(this.removeToolStripMenuItem2_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // centerToolStripMenuItem
            // 
            this.centerToolStripMenuItem.Name = "centerToolStripMenuItem";
            this.centerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.centerToolStripMenuItem.Text = "Center";
            this.centerToolStripMenuItem.Click += new System.EventHandler(this.centerToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 535);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "icFlow";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.tsDisplayOptions.ResumeLayout(false);
            this.tsDisplayOptions.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.cmsTranslations.ResumeLayout(false);
            this.cmsOneTranslation.ResumeLayout(false);
            this.cmsMesh.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem simulationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem continueFromLastToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oneStepToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStrip tsDisplayOptions;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSimulationToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbPhi;
        private System.Windows.Forms.ToolStripButton tsbAxes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem saveInitialStateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem takeScreenshotToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ContextMenuStrip cmsTranslations;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip cmsOneTranslation;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addMeshToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmsMesh;
        private System.Windows.Forms.ToolStripMenuItem cZsAndCapsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cZsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem splitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem resizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openClearSimToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renderSimulationToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialogMeshes;
        private System.Windows.Forms.ToolStripMenuItem rotate90DegToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem writeCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbRenderPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripMenuItem eraseSubsequentStepsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem pPRRelationsToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem rotateXToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbTopView;
        private System.Windows.Forms.ToolStripButton tsbBottomView;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem setupGenerationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem parametricStudyToolStripMenuItem;
        public System.Windows.Forms.ToolStripStatusLabel tssStatus;
        public System.Windows.Forms.TrackBar trackBar1;
        public System.Windows.Forms.ToolStripButton tsbPreviewMode;
        public OpenTK.GLControl glControl1;
        public System.Windows.Forms.ToolStripStatusLabel tssCurrentFrame;
        public System.Windows.Forms.TreeView treeView1;
        public System.Windows.Forms.PropertyGrid pg;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem setUpShearTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem centerToolStripMenuItem;
    }
}

