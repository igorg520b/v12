using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using OpenTK.Graphics;

namespace IcyGrains
{
    public partial class SetupGeneratorForm : Form
    {
        int lastX, lastY;   // mouse last position
        double aspectRatio = 1;
        public BeamParams rprm;
        GrainTool2 gt = new GrainTool2();
        public MemoryStream memStr;
        public MemoryStream memStrIndenter;

        public SetupGeneratorForm()
        {
            InitializeComponent();
            glControl1.MouseWheel += GlControl1_MouseWheel;
            rprm = BeamParams.Load();

            propertyGrid1.SelectedObject = rprm;
        }

        #region glControl
        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            #region prepare
            GL.DepthMask(true);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Enable(EnableCap.LineSmooth);
            GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
            GL.Disable(EnableCap.PolygonSmooth);
            GL.Enable(EnableCap.Multisample);
            GL.Hint(HintTarget.MultisampleFilterHintNv, HintMode.Nicest);
            GL.Enable(EnableCap.PointSmooth);
            GL.Hint(HintTarget.PointSmoothHint, HintMode.Nicest);

            RenderBackground();

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.Translate(0, 0, -rprm.zOffset);
            GL.Translate(rprm.dx / 1000, rprm.dy / 1000, 0);
            GL.Rotate(-rprm.phi, new Vector3(1, 0, 0));
            GL.Rotate(-rprm.theta, new Vector3(0, 0, 1));
            GL.DepthFunc(DepthFunction.Lequal);

            #endregion

            PaintMesh();

            glControl1.SwapBuffers();
        }


        void PaintMesh()
        {
            GL.Enable(EnableCap.Lighting);
            RenderAxes();

            SetUpLight();
            GL.DepthMask(true);
            GL.Enable(EnableCap.DepthTest);
            
            if (gt.tmesh != null) gt.tmesh.Draw();
            if (gt.indenter_mesh != null) gt.indenter_mesh.Draw();
        }

        void RenderAxes()
        {
            GL.LineWidth(1f);
            GL.Disable(EnableCap.Lighting);
            GL.Disable(EnableCap.DepthTest);
            double d = 10;
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(0.0, 0.0, 0.0);
            GL.Vertex3(d, 0.0, 0.0);
            GL.Color3(Color.Green);
            GL.Vertex3(0.0, 0.0, 0.0);
            GL.Vertex3(0.0, d, 0.0);
            GL.Color3(Color.Blue);
            GL.Vertex3(0.0, 0.0, 0.0);
            GL.Vertex3(0.0, 0.0, d);
            GL.End();
        }

        void SetUpLight()
        {
            GL.Enable(EnableCap.ColorMaterial);
            GL.Enable(EnableCap.Normalize);
            GL.Enable(EnableCap.Lighting);
            GL.ShadeModel(ShadingModel.Smooth);

            if (rprm.Light0)
            {
                GL.Enable(EnableCap.Light0);
                GL.Light(LightName.Light0, LightParameter.Diffuse, new Color4(rprm.L0intensity, rprm.L0intensity, rprm.L0intensity, 1f));
                GL.Light(LightName.Light0, LightParameter.Position, new Color4(rprm.L0x, rprm.L0y, rprm.L0z, 0));
            }
            else
                GL.Disable(EnableCap.Light0);

            if (rprm.Light1)
            {
                GL.Enable(EnableCap.Light1);
                GL.Light(LightName.Light1, LightParameter.Diffuse, new Color4(rprm.L1intensity, rprm.L1intensity, rprm.L1intensity, 1f));
                GL.Light(LightName.Light1, LightParameter.Position, new Color4(rprm.L1x, rprm.L1y, rprm.L1z, 0));
            }
            else
                GL.Disable(EnableCap.Light1);

            if (rprm.Light2)
            {
                GL.Enable(EnableCap.Light2);
                GL.Light(LightName.Light2, LightParameter.Diffuse, new Color4(rprm.L2intensity, rprm.L2intensity, rprm.L2intensity, 1f));
                GL.Light(LightName.Light2, LightParameter.Position, new Color4(rprm.L2x, rprm.L2y, rprm.L2z, 0));
            }
            else
                GL.Disable(EnableCap.Light2);
        }

        void RenderBackground(bool white = false)
        {
            float k = 0.95f;
            float k2 = 0.9f;

            if (white)
            {
                k = 1; k2 = 1;
            }

            GL.Disable(EnableCap.DepthTest);
            GL.Disable(EnableCap.Lighting);
            GL.ClearColor(k, k, k, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.MatrixMode(MatrixMode.Projection);
            GL.PushMatrix();
            GL.LoadIdentity();
            GL.Begin(PrimitiveType.Quads);
            double s = 1.0;
            GL.Color3(k2, k2, k2);
            GL.Vertex3(-s, -s, 0);
            GL.Color3(k, k, k);
            GL.Vertex3(-s, s, 0);
            GL.Vertex3(s, s, 0);
            GL.Color3(k2, k2, k2);
            GL.Vertex3(s, -s, 0);
            GL.End();
            GL.PopMatrix();
            GL.Clear(ClearBufferMask.DepthBufferBit);
        }

        private void glControl1_MouseDown(object sender, MouseEventArgs e)
        {
            lastX = e.X; lastY = e.Y;
        }

        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                rprm.theta += (e.X - lastX);
                rprm.phi += (e.Y - lastY);
                lastX = e.X;
                lastY = e.Y;
            }
            else if (e.Button == MouseButtons.Middle)
            {
                rprm.dx += (e.X - lastX);
                rprm.dy -= (e.Y - lastY);
                lastX = e.X;
                lastY = e.Y;
                reshape();
            }
            glControl1.Invalidate();
        }

        private void GlControl1_MouseWheel(object sender, MouseEventArgs e)
        {
            if(viewSelector.Text == "grains")
                rprm.scale2D += 0.001 * rprm.scale2D * e.Delta;
            else
                rprm.zOffset += 0.001 * rprm.zOffset * e.Delta;

            reshape();
            glControl1.Invalidate();
        }

        private void glControl1_Resize(object sender, EventArgs e)
        {
            reshape();
        }

        void reshape()
        {
            aspectRatio = (double)glControl1.Width / glControl1.Height;
            GL.Viewport(0, 0, glControl1.Width, glControl1.Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            if (viewSelector.Text == "grains")
            {
                GL.Ortho(-rprm.scale2D * aspectRatio,
                        rprm.scale2D * aspectRatio,
                        -rprm.scale2D,
                        rprm.scale2D, -1000, 1000);
            }
            else
            {
                perspectiveGL(rprm.fovY, aspectRatio, rprm.zNear, rprm.zFar);
            }

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            // Replaces gluPerspective. Sets the frustum to perspective mode.
            // fovY     - Field of vision in degrees in the y direction
            // aspect   - Aspect ratio of the viewport
            // zNear    - The near clipping distance
            // zFar     - The far clipping distance
            void perspectiveGL(double fovY, double aspect, double zNear, double zFar)
            {
                double fW, fH;
                fH = System.Math.Tan((fovY / 2) / 180 * Math.PI) * zNear;
                fH = System.Math.Tan(fovY / 360 * Math.PI) * zNear;
                fW = fH * aspect;
                GL.Frustum(-fW, fW, -fH, fH, zNear, zFar);
            }
        }

        #endregion



        #region other

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listBox1.SelectedIndex == 0) propertyGrid1.SelectedObject = rprm;
            else if (listBox1.SelectedIndex == 1) propertyGrid1.SelectedObject = gt.indenter_mesh;
            else if (listBox1.SelectedIndex == 2) propertyGrid1.SelectedObject = gt.tmesh;
        }


        private void l1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int tag = int.Parse((string)((ToolStripMenuItem)sender).Tag);
            rprm.PresetL(tag);
            propertyGrid1.Refresh();
        }

        private void setupSimulationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            memStr = new MemoryStream(5000000);
            memStrIndenter = new MemoryStream(5000000);
            gt.tmesh.SaveMsh2(memStr);
            memStr.Seek(0, SeekOrigin.Begin);

            gt.indenter_mesh.SaveMsh2(memStrIndenter);
            memStrIndenter.Seek(0, SeekOrigin.Begin);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void SetupGeneratorForm_Load(object sender, EventArgs e)
        {
            glControl1.MakeCurrent();
        }

        private void p1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int tag = int.Parse((string)((ToolStripMenuItem)sender).Tag);
            rprm.PresetPlain(tag);
            propertyGrid1.Refresh();
        }

        private async void generateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "generating beam";
            GrainTool2 gt2 = new GrainTool2();
            if (rprm.type == BeamParams.BeamType.LBeam)
                await Task.Run(() => gt2.LBeamGeneration(rprm));
            else if (rprm.type == BeamParams.BeamType.Plain)
                await Task.Run(() => gt2.PlainBeamGeneration(rprm));
            else if (rprm.type == BeamParams.BeamType.Plain2)
                await Task.Run(() => gt2.PlainBeamGeneration2(rprm));
            else throw new Exception("beam type not set");

            gt = gt2;
            toolStripStatusLabel1.Text = "done";
            viewSelector.Text = "mesh";
            reshape();
            glControl1.Invalidate();
        }

        private void plain2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rprm.RefinementMultiplier = 0.1;// 0.07;
            rprm.beamMargin = 0.6;
            rprm.type = BeamParams.BeamType.Plain2;
            propertyGrid1.Refresh();
            generateToolStripMenuItem_Click(null, null);
        }

        private void viewSelector_TextUpdate(object sender, EventArgs e)
        {
            reshape();
            glControl1.Invalidate();
        }


        #endregion

    }
}
