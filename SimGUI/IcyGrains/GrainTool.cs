using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using System.IO;
using System.Diagnostics;

namespace IcyGrains
{
    public class GrainTool2
    {
        public class Loop
        {
            public List<Vector2d> region = new List<Vector2d>();
            Vector2d exteriorPoint;

            public void InferExteriorPoint()
            {
                exteriorPoint = new Vector2d(region.Max(v => v.X) + 1, region.Max(v => v.Y) + 1);
            }

            public bool PointInsideLoop(Vector2d p)
            {
                int count = 0;
                for (int i = 0; i < region.Count; i++)
                {
                    Vector2d n0 = region[i];
                    Vector2d n1 = region[(i + 1) % region.Count];
                    if (SegmentSegmentIntersection(n0, n1, p, exteriorPoint)) count++;
                }
                return (count % 2) == 1; // true if interior
            }

            bool SegmentSegmentIntersection(Vector2d p, Vector2d p2, Vector2d q, Vector2d q2)
            {
                double p_zeta;
                double q_zeta;

                Vector2d r = p2 - p;
                Vector2d s = q2 - q;

                double rxs = Cross(r, s);
                if (Math.Abs(rxs) < 1E-30) return false; // collinear (disjoint in our case)

                p_zeta = Cross(q - p, s) / rxs;
                q_zeta = Cross(q - p, r) / rxs;

                if (p_zeta > 0 && p_zeta < 1 && q_zeta > 0 && q_zeta < 1) return true;
                else return false;

                double Cross(Vector2d a, Vector2d b) { return a.X * b.Y - a.Y * b.X; }
            }
        }

        public TetraMesh tmesh = new TetraMesh();
        public TetraMesh indenter_mesh = new TetraMesh();
        public Loop selectedRegion = new Loop();

        public GrainTool2()
        {
            if (!Directory.Exists("tmp")) Directory.CreateDirectory("tmp");
        }

        #region indenter
        void GenerateIndenter2(BeamParams prms)
        {
            // prepare .geo file
            string filename = $"tmp//indenter.geo";
            Stream str = File.Create(filename);
            StreamWriter sw = new StreamWriter(str);

            sw.WriteLine(@"SetFactory(""OpenCASCADE"");");
            sw.WriteLine($"a = {prms.beamA};");
            sw.WriteLine($"b = {prms.beamB};");
            sw.WriteLine($"l1 = {prms.beamL1};");
            sw.WriteLine($"l2 = {prms.beamL2};");
            sw.WriteLine($"c = {prms.beamGap};");
            sw.WriteLine($"d = {prms.beamMargin};");
            sw.WriteLine($"h = {prms.beamThickness};");
            sw.WriteLine($"indsize = {prms.IndenterSize};");
            sw.WriteLine("dy = l1 + c + d;");
            sw.WriteLine($"yoffset = dy-a/2-indsize/2;");
            sw.WriteLine("dx = c + d;");
            sw.WriteLine(@"Point(3) = { dx + l2, yoffset, 0+2*h, 1.0};
Point(4) = { dx + l2-indsize, yoffset, 0+2*h, 1.0};
Point(5) = { dx + l2-indsize/2, yoffset, h+2*h, 1.0};
Point(6) = { dx + l2-indsize, yoffset, c/2+2*h, 1.0};
Point(7) = { dx + l2, yoffset, c/2+2*h, 1.0};
Circle(1) = { 3, 5, 4};
Line(2) = { 4, 6};
Line(3) = { 6, 7};
Line(4) = { 7, 3};
Curve Loop(2) = {1,2,3,4};
Plane Surface(1) = {2};
Extrude {0, indsize, 0} {
  Surface{1}; 
}");

            sw.WriteLine($"Mesh.CharacteristicLengthMax = {prms.CharacteristicLengthIndenter};");
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

            // load .msh -> tmesh
            lock (indenter_mesh.allTetra)
            {
                indenter_mesh.LoadMsh(File.OpenRead($"tmp//indenter.msh"));
            }
        }
        #endregion

        #region LBeam

        public void LBeamGeneration(BeamParams prms)
        {
            // prepare .geo file
            string filename = $"tmp//LBeam.geo";
            Stream str = File.Create(filename);
            StreamWriter sw = new StreamWriter(str);

            sw.WriteLine(@"SetFactory(""OpenCASCADE"");");
            sw.WriteLine($"a = {prms.beamA};");
            sw.WriteLine($"b = {prms.beamB};");
            sw.WriteLine($"l1 = {prms.beamL1};");
            sw.WriteLine($"l2 = {prms.beamL2};");
            sw.WriteLine($"c = {prms.beamGap};");
            sw.WriteLine($"d = {prms.beamMargin};");
            sw.WriteLine($"h = {prms.beamThickness};");
            sw.WriteLine("dx = c + d;");
            sw.WriteLine("dy = l1 + c + d;");
            sw.WriteLine($"Point(1) = {{ dx + 0, dy + 0, 0, {prms.RefinementMultiplier}}};");
            sw.WriteLine(@"Point(2) = { dx + l2, dy + 0, 0, 1.0};");
            sw.WriteLine($"Point(22) = {{ dx + l2 / 2,dy + 0, 0, {prms.RefinementMultiplier}}};");
            sw.WriteLine(@"Point(3) = { dx + l2, dy - a, 0, 1.0};");
            sw.WriteLine($"Point(4) = {{ dx + 0,dy - l1 + c / 2, 0, {prms.RefinementMultiplier/2}}};");
            sw.WriteLine($"Point(5) = {{ dx - c,dy - l1 + c / 2, 0, {prms.RefinementMultiplier / 2}}};");
            sw.WriteLine($"Point(6) = {{ dx - c / 2,dy - l1 + c / 2, 0, {prms.RefinementMultiplier / 2}}};");
            sw.WriteLine($"Point(7) = {{ dx - c, dy + c, 0, 1.0}};");
            sw.WriteLine(@"Point(8) = { dx + l2 + c, dy + c, 0, 1.0};");
            sw.WriteLine(@"Point(9) = { dx + l2 + c, dy - a - c, 0, 1.0};");
            sw.WriteLine($"Point(10) = {{ dx + b + 2 * c,dy - a, 0, {prms.RefinementMultiplier / 2}}};");
            sw.WriteLine($"Point(11) = {{ dx + b,dy - a - 2 * c, 0, {prms.RefinementMultiplier / 2}}};");
            sw.WriteLine($"Point(12) = {{ dx + b + 2 * c,dy - a - 2 * c, 0, {prms.RefinementMultiplier}}};");
            sw.WriteLine($"Point(13) = {{ dx + b + 2 * c,dy - a - c, 0, {prms.RefinementMultiplier}}};");
            sw.WriteLine($"Point(14) = {{ dx + b + c,dy - a - 2 * c, 0, {prms.RefinementMultiplier}}};");
            sw.WriteLine($"Point(15) = {{ dx + b,dy - l1 + c / 2, 0, {prms.RefinementMultiplier / 2}}};");
            sw.WriteLine($"Point(16) = {{ dx + b + c,dy - l1 + c / 2, 0, {prms.RefinementMultiplier / 2}}};");
            sw.WriteLine($"Point(17) = {{ dx + b + c / 2,dy - l1 + c / 2, 0, {prms.RefinementMultiplier / 2}}};");
            sw.WriteLine(@"Point(18) = { -d, dy + c + d, 0, 1.0};
Point(19) = { dx + l2 + c + d, dy + c + d, 0, 1.0};
Point(20) = { dx + l2 + c + d, dy - l1 - c - 2*d, 0, 1.0};
");

            sw.WriteLine($"Point(21) = {{ -d, -d, 0, {prms.RefinementMultiplier}}};");
            sw.WriteLine($"Point(23) = {{  dx/4 + l2/4 + c/4 + d/4, -d , 0, {prms.RefinementMultiplier*0.75}}};");
            sw.WriteLine($"Point(24) = {{ -d, dy/4 + c/4 + d/4, 0, {prms.RefinementMultiplier}}};");
            sw.WriteLine($"Point(25) = {{ -d , dy/2 + c/2 + d/2, 0, 1.0}};");
            sw.WriteLine($"Point(26) = {{  dx/2 + l2/2 + c/2 + d/2, -d, 0, 1.0}};");

            sw.WriteLine(@"Circle(1) = { 4, 6, 5};
Circle(8) = { 10, 12, 11};
Circle(11) = { 13, 12, 14};
Circle(14) = { 16, 17, 15};
Line(2) = { 1, 22};
Line(19) = { 22, 2};
Line(3) = { 2, 3};
Line(4) = { 4, 1};
Line(5) = { 5, 7};
Line(6) = { 7, 8};
Line(7) = { 8, 9};
Line(9) = { 3, 10};
Line(10) = { 9, 13};
Line(12) = { 11, 15};
Line(13) = { 14, 16};
Line(15) = { 18, 19};
Line(16) = { 19, 20};
Line(17) = { 20, 26};
Line(18) = { 26, 23};
Line(20) = { 23, 21};
Line(21) = { 21, 24}; 
Line(22) = { 24, 25}; 
Line(23) = { 25, 18}; 
Curve Loop(2) = {15, 16, 17, 18,20,21,22,23};
Curve Loop(3) = {6, 7, 10, 11, 13, 14, -12, -8, -9, -3, -19,-2, -4, 1, 5};
Plane Surface(1) = {2, 3};
Extrude {0, 0, h} {
  Surface{1}; 
}");
            sw.WriteLine($"Mesh.CharacteristicLengthMax = {prms.CharacteristicLengthMax};");
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

            // load .msh -> tmesh
            lock (tmesh.allTetra)
            {
                tmesh.LoadMsh(File.OpenRead($"tmp//LBeam.msh"));
            }

            GenerateSelectedRegionLBeam(prms);

            // now, enumerate tetrahedra inside the region
            int count = 2;
            foreach(TetraMesh.Tetra t in tmesh.allTetra)
            {
                if (selectedRegion.PointInsideLoop(t.center.Xy)) t.tag = count++;
                else t.tag = 1;
            }

            GenerateIndenter2(prms);
        }

        void GenerateSelectedRegionLBeam(BeamParams prms)
        {
            double a = prms.beamA;
            double b = prms.beamB;
            double l1 = prms.beamL1;
            double l2 = prms.beamL2;
            double c = prms.beamGap;
            double d = prms.beamMargin;
            double dx = c + d;
            double dy = l1 + c + d;

            selectedRegion.region = new List<Vector2d>();
            List<Vector2d> s = selectedRegion.region;

            s.Add(new Vector2d(dx + l2*0.6, dy+c/2));
            s.Add(new Vector2d(dx + b + 2*c, dy -a-c/2));
            s.Add(new Vector2d(dx + b + c/2, dy - a - 2*c));
            s.Add(new Vector2d(dx + b + c/2, dy - l1 - c - d/2));
            s.Add(new Vector2d(dx - c/2, dy - l1 - c - d/2)); // pt 21
            s.Add(new Vector2d(dx - c/2, dy - l1 / 2));
            s.Add(new Vector2d(dx - c / 2, dy + c / 2));
            selectedRegion.InferExteriorPoint();
        }

 


        #endregion

        #region PlainBeam



        public void PlainBeamGeneration(BeamParams prms)
        {
            // prepare .geo file
            string filename = $"tmp//PBeam.geo";
            Stream str = File.Create(filename);
            StreamWriter sw = new StreamWriter(str);

            sw.WriteLine(@"SetFactory(""OpenCASCADE"");");
            sw.WriteLine($"a = {prms.beamA};");
            sw.WriteLine($"l2 = {prms.beamL2};");
            sw.WriteLine($"c = {prms.beamGap};");
            sw.WriteLine($"d = {prms.beamMargin};");
            sw.WriteLine($"h = {prms.beamThickness};");
            sw.WriteLine($"Point(1) = {{ d, -a/2-c/2, 0, {prms.RefinementMultiplier}}};");
            sw.WriteLine($"Point(2) = {{ d, a/2+c/2, 0, {prms.RefinementMultiplier}}};");
            sw.WriteLine($"Point(3) = {{ d+l2+c, -a/2-c, 0, 1.0}};");
            sw.WriteLine($"Point(10) = {{ d+l2+c, a/2+c, 0, 1.0}};");
            sw.WriteLine($"Point(4) = {{ d, -a/2-c, 0, {prms.RefinementMultiplier}}};");
            sw.WriteLine($"Point(9) = {{ d, a/2+c, 0, {prms.RefinementMultiplier}}};");
            sw.WriteLine($"Point(5) = {{ d, -a/2, 0, {prms.RefinementMultiplier}}};");
            sw.WriteLine($"Point(8) = {{ d, a/2, 0, {prms.RefinementMultiplier}}};");
            sw.WriteLine($"Point(6) = {{ d+l2, -a/2, 0, 1.0}};");
            sw.WriteLine($"Point(7) = {{ d+l2, a/2, 0, 1.0}};");
            sw.WriteLine($"Point(11) = {{ d+l2/2, -a/2, 0, {prms.RefinementMultiplier}}};");
            sw.WriteLine($"Point(12) = {{ d+l2/2, a/2, 0, {prms.RefinementMultiplier}}};");

            sw.WriteLine($"Point(20) = {{ d+l2+c+d, -a/2-c-d, 0, 1.0}};");
            sw.WriteLine($"Point(25) = {{ d+l2+c+d, a/2+c+d, 0, 1.0}};");
            sw.WriteLine($"Point(21) = {{ 0, -a/2-c-d, 0, 1.0}};");
            sw.WriteLine($"Point(24) = {{ 0, a/2+c+d, 0, 1.0}};");
            sw.WriteLine($"Point(22) = {{ 0, -a/2-c, 0, {prms.RefinementMultiplier}}};");
            sw.WriteLine($"Point(23) = {{ 0, a/2+c, 0, {prms.RefinementMultiplier}}};");

            sw.WriteLine(@"
Line(1) = { 3, 4};
Circle(2) = { 4, 1, 5};
Line(3) = { 5, 11};
Line(4) = { 11, 6};
Line(5) = { 6, 7};
Line(6) = { 7, 12};
Line(7) = { 12, 8};
Circle(8) = { 8, 2, 9};
Line(9) = { 9, 10};
Line(10) = { 10, 3};
//
Line(20) = { 20, 21};
Line(21) = { 21, 22};
Line(22) = { 22, 23};
Line(23) = { 23, 24};
Line(24) = { 24, 25};
Line(25) = { 25, 20};

Curve Loop(2) = {1, 2, 3, 4, 5, 6, 7, 8,9,10};
Curve Loop(3) = {20,21,22,23,24,25};
Plane Surface(1) = {2, 3};
Extrude {0, 0, h} {
  Surface{1}; 
}");
            sw.WriteLine($"Mesh.CharacteristicLengthMax = {prms.CharacteristicLengthMax};");
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

            // load .msh -> tmesh
            lock (tmesh.allTetra)
            {
                tmesh.LoadMsh(File.OpenRead($"tmp//PBeam.msh"));
            }

            GenerateSelectedRegionPBeam();

            // now, enumerate tetrahedra inside the region
            int count = 2;
            foreach (TetraMesh.Tetra t in tmesh.allTetra)
            {
                if (selectedRegion.PointInsideLoop(t.center.Xy)) t.tag = count++;
                else t.tag = 1;
            }

            PlainBeamGenerateIndenter(prms);

            void GenerateSelectedRegionPBeam()
            {
                double a = prms.beamA;
                double b = prms.beamB;
                double l1 = prms.beamL1;
                double l2 = prms.beamL2;
                double c = prms.beamGap;
                double d = prms.beamMargin;
                double dx = c + d;
                double dy = l1 + c + d;

                selectedRegion.region = new List<Vector2d>();
                List<Vector2d> s = selectedRegion.region;

                s.Add(new Vector2d(d + l2 * 0.3, -a/2-c/2));
                s.Add(new Vector2d(d, -a/2-c-d/2));
                s.Add(new Vector2d(d/3, -a / 2 - c - d / 2));
                s.Add(new Vector2d(d / 3, a / 2 + c + d / 2));
                s.Add(new Vector2d(d, a / 2 + c + d / 2));
                s.Add(new Vector2d(d + l2 * 0.3, a / 2 + c / 2));
                selectedRegion.InferExteriorPoint();
            }

        }

        void PlainBeamGenerateIndenter(BeamParams prms)
        {
            // prepare .geo file
            string filename = $"tmp//indenter.geo";
            Stream str = File.Create(filename);
            StreamWriter sw = new StreamWriter(str);

            sw.WriteLine(@"SetFactory(""OpenCASCADE"");");
            sw.WriteLine($"a = {prms.beamA};");
            sw.WriteLine($"l2 = {prms.beamL2};");
            sw.WriteLine($"c = {prms.beamGap};");
            sw.WriteLine($"d = {prms.beamMargin};");
            sw.WriteLine($"h = {prms.beamThickness};");
            sw.WriteLine($"indsize = {prms.IndenterSize};");
            sw.WriteLine("dy = l1 + c + d;");
            sw.WriteLine($"yoffset = -indsize/2;");
            sw.WriteLine("dx = d;");
            sw.WriteLine(@"Point(3) = { dx + l2, yoffset, 0+2*h, 1.0};
Point(4) = { dx + l2-indsize, yoffset, 0+2*h, 1.0};
Point(5) = { dx + l2-indsize/2, yoffset, h+2*h, 1.0};
Point(6) = { dx + l2-indsize, yoffset, c/2+2*h, 1.0};
Point(7) = { dx + l2, yoffset, c/2+2*h, 1.0};
Circle(1) = { 3, 5, 4};
Line(2) = { 4, 6};
Line(3) = { 6, 7};
Line(4) = { 7, 3};
Curve Loop(2) = {1,2,3,4};
Plane Surface(1) = {2};
Extrude {0, indsize, 0} {
  Surface{1}; 
}");

            sw.WriteLine($"Mesh.CharacteristicLengthMax = {prms.CharacteristicLengthIndenter};");
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

            // load .msh -> tmesh
            lock (indenter_mesh.allTetra)
            {
                indenter_mesh.LoadMsh(File.OpenRead($"tmp//indenter.msh"));
            }
        }

        #endregion
    }
}


