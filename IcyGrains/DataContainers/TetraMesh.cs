using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.IO;
using System.Diagnostics;
using System.Drawing;

namespace IcyGrains
{
    class TetraMesh
    {
        public class Node
        {
            public Vector3d vec;
            public int id;
            public Node(double x, double y, double z, int id)
            {
                vec = new Vector3d(x, y, z);
                this.id = id;
            }
        }

        public class Tetra
        {
            public Node[] nodes = new Node[4];
            public Tetra(Node n0, Node n1, Node n2, Node n3, int tag)
            {
                this.tag = tag;
                nodes[0] = n0;
                nodes[1] = n1;
                nodes[2] = n2;
                nodes[3] = n3;
                center = (n0.vec + n1.vec + n2.vec + n3.vec) * 0.25;
            }
            public bool marked; // marked for removal 
            public Vector3d center;
            public int tag;

            // for removal of facets and edges
            public (int, int, int)[] faces = new(int, int, int)[4];
            public (int, int)[] edges = new(int, int)[6];

            public void RegenerateFacetsAndEdges()
            {
                faces[0] = SortedTriple(nodes[0].id, nodes[1].id, nodes[2].id);
                faces[1] = SortedTriple(nodes[0].id, nodes[1].id, nodes[3].id);
                faces[2] = SortedTriple(nodes[0].id, nodes[2].id, nodes[3].id);
                faces[3] = SortedTriple(nodes[1].id, nodes[2].id, nodes[3].id);

                edges[0] = SortedTuple(nodes[0].id, nodes[1].id);
                edges[1] = SortedTuple(nodes[0].id, nodes[2].id);
                edges[2] = SortedTuple(nodes[0].id, nodes[3].id);
                edges[3] = SortedTuple(nodes[1].id, nodes[3].id);
                edges[4] = SortedTuple(nodes[1].id, nodes[2].id);
                edges[5] = SortedTuple(nodes[2].id, nodes[3].id);
            }
        }

        public class Triangle
        {
            public Node[] nodes = new Node[3];
            public Triangle(Node n0, Node n1, Node n2)
            {
                nodes[0] = n0;
                nodes[1] = n1;
                nodes[2] = n2;
            }
            public bool marked; // for removal
            public (int,int,int) asTriple { get { return SortedTriple(nodes[0].id, nodes[1].id, nodes[2].id); } }

            public List<Tetra> connectedTetra = new List<Tetra>();
        }

        public class Edge
        {
            public Node[] nodes = new Node[2];
            public Edge(Node n0, Node n1)
            {
                nodes[0] = n0;
                nodes[1] = n1;
            }
            public bool marked; // for removal
            public (int, int) asTuple { get { return SortedTuple(nodes[0].id, nodes[1].id); } }
        }


        public static (int, int, int) SortedTriple(int i1, int i2 ,int i3)
        {
            int[] arr = new int[3];
            arr[0] = i1;
            arr[1] = i2;
            arr[2] = i3;
            Array.Sort(arr);
            return (arr[0], arr[1], arr[2]);
        }

        public static (int,int) SortedTuple(int i1, int i2)
        {
            if (i2 > i1) return (i1, i2);
            else return (i2, i1);
        }


        public List<Node> allNodes;
        public List<Tetra> allTetra = new List<Tetra>();
        public int tetraCount { get { return allTetra.Count; } }
        public List<Triangle> allTriangles;
        public List<Edge> allEdges;

        public void LoadMsh(Stream str)
        {
            string[] separators = new string[1] { " " };
            StreamReader sr = new StreamReader(str);

            string s;
            do { s = sr.ReadLine(); } while (s != "$Nodes");
            int nNodes = Int32.Parse(sr.ReadLine());
            allNodes = new List<Node>();

            for (int i = 0; i < nNodes; i++)
            {
                s = sr.ReadLine();
                string[] parts = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                int id = int.Parse(parts[0]);
                Debug.Assert(id == (i + 1));
                double x = double.Parse(parts[1]);
                double y = double.Parse(parts[2]);
                double z = double.Parse(parts[3]);
                allNodes.Add(new Node(x, y, z, i));
            }
            s = sr.ReadLine();
            Debug.Assert(s == "$EndNodes");

            s = sr.ReadLine();
            Debug.Assert(s == "$Elements");
            int nElements = Int32.Parse(sr.ReadLine());

            allTetra = new List<Tetra>();
            allTriangles = new List<Triangle>();
            allEdges = new List<Edge>();
            for (int i = 0; i < nElements; i++)
            {
                s = sr.ReadLine();
                string[] parts = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                int id = int.Parse(parts[0]);
                Debug.Assert(id == (i + 1));
                int type = int.Parse(parts[1]);
                int numTags = int.Parse(parts[2]);
                int sidx = 3 + numTags;
                if (type == 4)
                {
                    // tetrahedron
                    int n0 = int.Parse(parts[sidx]) - 1;
                    int n1 = int.Parse(parts[sidx + 1]) - 1;
                    int n2 = int.Parse(parts[sidx + 2]) - 1;
                    int n3 = int.Parse(parts[sidx + 3]) - 1;
                    int tag = int.Parse(parts[4]);
                    allTetra.Add(new Tetra(allNodes[n0], allNodes[n1], allNodes[n2], allNodes[n3], tag));
                } else if(type == 1)
                {
                    // line
                    int n0 = int.Parse(parts[sidx]) - 1;
                    int n1 = int.Parse(parts[sidx + 1]) - 1;
                    allEdges.Add(new Edge(allNodes[n0], allNodes[n1]));
                }
                else if(type == 2)
                {
                    // triangle
                    int n0 = int.Parse(parts[sidx]) - 1;
                    int n1 = int.Parse(parts[sidx + 1]) - 1;
                    int n2 = int.Parse(parts[sidx + 2]) - 1;
                    allTriangles.Add(new Triangle(allNodes[n0], allNodes[n1], allNodes[n2]));
                }
            }
            s = sr.ReadLine();
            Debug.Assert(s == "$EndElements");
            sr.Close();
        }

        public void SaveMsh2(Stream str)
        {
            HashSet<Node> presentNodes = new HashSet<Node>();
            foreach (Tetra t in allTetra)
            {
                t.RegenerateFacetsAndEdges();
                foreach (Node nd in t.nodes) presentNodes.Add(nd);
            }
            allNodes = presentNodes.ToList();
            for (int i = 0; i < allNodes.Count; i++) allNodes[i].id = i; 


            // (1) remove faces and edges that aren't connected to any elements
            HashSet<(int, int, int)> presentFaces = new HashSet<(int, int, int)>();
            HashSet<(int, int)> presentEdges = new HashSet<(int, int)>();
            // make a big pile of facets and edges
            foreach (Tetra t in allTetra)
            {
                t.RegenerateFacetsAndEdges();
                foreach ((int, int, int) face in t.faces) presentFaces.Add(face);
                foreach ((int, int) e in t.edges) presentEdges.Add(e);
            }

            Dictionary<(int, int, int), Triangle> tdictionary = new Dictionary<(int, int, int), Triangle>();
            foreach ((int, int, int) tr in presentFaces)
                tdictionary.Add(tr,new Triangle(allNodes[tr.Item1], allNodes[tr.Item2], allNodes[tr.Item3]));

            foreach (Tetra t in allTetra)
            {
                foreach ((int, int, int) face in t.faces)
                {
                    Triangle tr = tdictionary[face];
                    tr.connectedTetra.Add(t);
                }
            }
            allTriangles = tdictionary.Values.ToList();
            Debug.Assert(allTriangles.Max(t => t.connectedTetra.Count) == 2, "max connected tetra");
            Debug.Assert(allTriangles.Min(t => t.connectedTetra.Count) == 1, "min connected tetra");

            foreach(Triangle tr in allTriangles)
            {
                if (tr.connectedTetra.Count == 1) continue;
                if (tr.connectedTetra[0].tag == tr.connectedTetra[1].tag) tr.marked = true;
            }

            allTriangles.RemoveAll(tr => tr.marked);

            // (2) save
            StreamWriter sw = new StreamWriter(str);
            sw.WriteLine(@"$MeshFormat
2 0 8
$EndMeshFormat
$Nodes");
            // write nodes
            sw.WriteLine(allNodes.Count.ToString());
            for (int i = 0; i < allNodes.Count; i++)
            {
                Vector3d v = allNodes[i].vec;
                sw.WriteLine($"{i + 1} {v.X} {v.Y} {v.Z}");
            }
            sw.WriteLine(@"$EndNodes");
            sw.WriteLine(@"$Elements");
            int nelems = allEdges.Count + allTriangles.Count + allTetra.Count;
            sw.WriteLine($"{nelems}");
            int count = 1;
            foreach (Edge e in allEdges)
                sw.WriteLine($"{count++} 1 0 {e.nodes[0].id + 1} {e.nodes[1].id + 1}");
            foreach (Triangle t in allTriangles)
                sw.WriteLine($"{count++} 2 0 {t.nodes[0].id + 1} {t.nodes[1].id + 1} {t.nodes[2].id + 1}");
            foreach (Tetra t in allTetra)
                sw.WriteLine($"{count++} 4 2 0 {t.tag} {t.nodes[0].id + 1} {t.nodes[1].id + 1} {t.nodes[2].id + 1} {t.nodes[3].id + 1}");
            sw.WriteLine("$EndElements");
            sw.Flush();
//            sw.Close();
        }

 

        static Color[] colors;
        static void InitializeColors()
        {
            colors = new Color[17];
            //            colors[0] = Color.FromArgb(244, 154, 194);
            colors[0] = Color.White;
            colors[1] = Color.FromArgb(203, 153, 201);
            colors[2] = Color.FromArgb(194, 59, 34);
            colors[3] = Color.FromArgb(255, 209, 220);
            colors[4] = Color.FromArgb(222, 165, 164);
            colors[5] = Color.FromArgb(174, 198, 207);
            colors[6] = Color.FromArgb(119, 190, 119);
            colors[7] = Color.FromArgb(207, 207, 196);
            colors[8] = Color.FromArgb(179, 158, 181);
            colors[9] = Color.FromArgb(255, 179, 71);
            colors[10] = Color.FromArgb(100, 20, 100);
            colors[11] = Color.FromArgb(255, 105, 97);
            colors[12] = Color.FromArgb(3, 192, 60);
            colors[13] = Color.FromArgb(253, 253, 150);
            colors[14] = Color.FromArgb(130, 105, 83);
            colors[15] = Color.FromArgb(119, 158, 203);
            colors[16] = Color.FromArgb(150, 111, 214);
        }
        static TetraMesh() { InitializeColors(); }

        int[,] pr = new int[4, 3] { { 1, 2, 3 }, { 1, 2, 4 }, { 1, 3, 4 }, { 2, 3, 4 } };

        public void Draw()
        {
            GL.LineWidth(1f);
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(System.Drawing.Color.LightCyan);
            lock (allTetra)
            {
                foreach (Tetra t in allTetra)
                {
                    GL.Color3(colors[t.tag % colors.Length]);
                    for (int i = 0; i < 4; i++)
                        for (int j = 0; j < 3; j++)
                            GL.Vertex3(t.nodes[pr[i, j] - 1].vec);
                }
            }
            GL.End();

            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Black);
            lock (allTetra)
            {
                foreach (Tetra t in allTetra)
                {
                    for (int i = 0; i < 4; i++)
                        for (int j = 0; j < 3; j++)
                        {
                            GL.Vertex3(t.nodes[pr[i, j] - 1].vec);
                            GL.Vertex3(t.nodes[pr[i, (j + 1) % 3] - 1].vec);
                        }
                }
            }
            GL.End();
        }

    }
}
