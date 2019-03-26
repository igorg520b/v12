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
using IcyGrains;

namespace icFlow
{
    public partial class ShearTestSetupForm : Form
    {
        public ShearTestSetupForm()
        {
            InitializeComponent();
        }

        public double InnerIndenterLocation = 0, OuterIndenterLocation = 0;
        public double IndentationRate = 0;

        public MemoryStream memStrIndenter;
        public MemoryStream memStrSideDisk;
        public MemoryStream memStrHoldingPin;

        void GenerateAdditionalMeshes()
        {
            if (!Directory.Exists("tmp")) Directory.CreateDirectory("tmp");
            GenerateIndenter();
            GenerateSideDisk();
            GenerateHoldingPin();
        }

        void GenerateHoldingPin()
        {
            // prepare .geo file
            string filename = $"tmp//pin.geo";
            Stream str = File.Create(filename);
            StreamWriter sw = new StreamWriter(str);

            sw.WriteLine(@"SetFactory(""OpenCASCADE"");");
            sw.WriteLine(@"Circle(1) = {0, 0, 0, 0.008, 0, 2*Pi};");
//            sw.WriteLine(@"Rotate {{1, 0, 0}, {0, 0, 0}, Pi/2} { Curve{1}; }");
            sw.WriteLine(@"Curve Loop(1) = {1};");
            sw.WriteLine(@"Plane Surface(1) = {1};");
            sw.WriteLine(@"Extrude {0, 0, 0.01} { Surface{1}; }");
            sw.WriteLine($"Mesh.CharacteristicLengthMax = {0.0025};");
            sw.Close();

            // invoke gmsh
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "gmsh.exe";
            startInfo.Arguments = $"{filename} -format msh2 -3";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();

            TetraMesh tmesh = new TetraMesh();
            tmesh.LoadMsh(File.OpenRead($"tmp//pin.msh"));

            memStrHoldingPin = new MemoryStream(5000000);
            tmesh.SaveMsh2(memStrHoldingPin);
            memStrHoldingPin.Seek(0, SeekOrigin.Begin);
        }

        void GenerateSideDisk()
        {
            // prepare .geo file
            string filename = $"tmp//disk.geo";
            Stream str = File.Create(filename);
            StreamWriter sw = new StreamWriter(str);

            sw.WriteLine(@"SetFactory(""OpenCASCADE"");");
            sw.WriteLine(@"Circle(1) = {0, 0, 0, 0.04, 0, 2*Pi};");
            sw.WriteLine(@"Rotate {{0, 1, 0}, {0, 0, 0}, Pi/2} { Curve{1}; }");
            sw.WriteLine(@"Curve Loop(1) = {1};");
            sw.WriteLine(@"Plane Surface(1) = {1};");
            sw.WriteLine(@"Extrude {0.01, 0, 0} { Surface{1}; }");
            sw.WriteLine($"Mesh.CharacteristicLengthMax = {0.009};");
            sw.Close();

            // invoke gmsh
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "gmsh.exe";
            startInfo.Arguments = $"{filename} -format msh2 -3";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();

            TetraMesh tmesh = new TetraMesh();
            tmesh.LoadMsh(File.OpenRead($"tmp//disk.msh"));

            memStrSideDisk = new MemoryStream(5000000);
            tmesh.SaveMsh2(memStrSideDisk);
            memStrSideDisk.Seek(0, SeekOrigin.Begin);
        }

        void GenerateIndenter()
        {
            // prepare .geo file
            string filename = $"tmp//Indenter.geo";
            Stream str = File.Create(filename);
            StreamWriter sw = new StreamWriter(str);

            sw.WriteLine(@"SetFactory(""OpenCASCADE"");");
            sw.WriteLine(@"Circle(1) = {0, 0, 0, 0.008, 0, 2*Pi};");
            sw.WriteLine(@"Rotate {{1, 0, 0}, {0, 0, 0}, Pi/2} { Curve{1}; }");
            sw.WriteLine(@"Curve Loop(1) = {1};");
            sw.WriteLine(@"Plane Surface(1) = {1};");
            sw.WriteLine(@"Extrude {0, 0.03, 0} { Surface{1}; }");
            sw.WriteLine($"Mesh.CharacteristicLengthMax = {0.0025};");
            sw.Close();

            // invoke gmsh
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "gmsh.exe";
            startInfo.Arguments = $"{filename} -format msh2 -3";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();

            TetraMesh tmesh = new TetraMesh();
            tmesh.LoadMsh(File.OpenRead($"tmp//Indenter.msh"));
            
            memStrIndenter = new MemoryStream(5000000);
            tmesh.SaveMsh2(memStrIndenter);
            memStrIndenter.Seek(0, SeekOrigin.Begin);
        }


        private void Setup_Click(object sender, EventArgs e)
        {
            InnerIndenterLocation = (double)nudInner.Value / 1000;
            OuterIndenterLocation = (double)nudOuter.Value / 1000;
            if (rbPoint2.Checked) IndentationRate = 0.0002;
            else if (rb2.Checked) IndentationRate = 0.002;
            else if (rb20.Checked) IndentationRate = 0.02;
            else throw new Exception("indentation rate");

            GenerateAdditionalMeshes();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void ShearTestSetupForm_Load(object sender, EventArgs e)
        {

        }
    }
}
