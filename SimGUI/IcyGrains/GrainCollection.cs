using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;

namespace IcyGrains
{
    class GrainCollection 
    {
        /*
        public int[][] face_edges;
        public Tuple<int, int>[] edges;
        public Vector3d[] nodes;
        public int[][] polygons;
        public int[][] polyhedra;
        */

        public class Node
        {
            public int id;
            public Vector3d vec;
            public bool isInside;

            public Node(int id, Vector3d vec)
            {
                this.id = id; this.vec = vec;
            }
        }

        public class Poly
        {
            public int id;
            public Node[] nodes;
            public int[] edges; // list of edges (one-based!, with sign!)
        }

        public class Grain
        {
            public Poly[] polygons;
            public bool keep;

            public void InferKeep()
            {
                keep = false;
                foreach(Poly p in polygons)
                    foreach(Node n in p.nodes) 
                        if(n.isInside) { keep = true; return; }
            }
        }

        public List<Node> allNodes;
        public List<Poly> allPolygons = new List<Poly>(); // for drawing and loading
        public List<Grain> allGrains;
        

        static string[] separators = new string[1] { " " };
        public void LoadTess(Stream str)
        {
            Vector3d[] nodes;
            int[][] polygons;
            int[][] polyhedra;
            int[][] face_edges;
            Tuple<int, int>[] edges;

            StreamReader sr = new StreamReader(str);
            String s;

            // read verts
            do { s = sr.ReadLine(); } while (!s.Contains("**vertex"));
            s = sr.ReadLine();
            int node_count = int.Parse(s);
            nodes = new Vector3d[node_count];

            for (int i = 0; i < node_count; i++)
            {
                s = sr.ReadLine();
                string[] parts = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                double x = double.Parse(parts[1]);
                double y = double.Parse(parts[2]);
                double z = double.Parse(parts[3]);
                nodes[i] = new Vector3d(x, y, z);
            }

            // read edges
            do { s = sr.ReadLine(); } while (!s.Contains("**edge"));
            s = sr.ReadLine();
            int edge_count = int.Parse(s);

            edges = new Tuple<int, int>[edge_count + 1]; // one-based index

            for (int i = 1; i < edge_count + 1; i++)
            {
                s = sr.ReadLine();
                string[] parts = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                int edge_id = int.Parse(parts[0]);
                Debug.Assert(edge_id == i);
                int pt1 = int.Parse(parts[1]) - 1;
                int pt2 = int.Parse(parts[2]) - 1;
                edges[i] = new Tuple<int, int>(pt1, pt2);
            }

            // faces
            do { s = sr.ReadLine(); } while (!s.Contains("**face"));
            s = sr.ReadLine();
            int face_count = int.Parse(s);
            polygons = new int[face_count][];
            face_edges = new int[face_count][];

            for (int i = 0; i < face_count; i++)
            {
                s = sr.ReadLine();
                string[] parts = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                int num_verts = int.Parse(parts[1]);
                polygons[i] = new int[num_verts];

                for (int j = 0; j < num_verts; j++)
                {
                    int idx = int.Parse(parts[2 + j]) - 1;
                    polygons[i][j] = idx;
                }

                s = sr.ReadLine(); // edges
                parts = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                int num_edges = int.Parse(parts[0]);
                Debug.Assert(num_verts == num_edges);

                face_edges[i] = new int[num_verts];
                for (int j = 0; j < num_verts; j++)
                {
                    int idx = int.Parse(parts[1 + j]);
                    face_edges[i][j] = idx;
                }


                // parse d a b c
                s = sr.ReadLine();
                //                parts = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                //                p.d = double.Parse(parts[0]);
                //                p.a = double.Parse(parts[1]);
                //                p.b = double.Parse(parts[2]);
                //                p.c = double.Parse(parts[3]);
                //                p.normal = new OpenTK.Vector3d(p.a, p.b, p.c);

                s = sr.ReadLine();
                //                pls.Add(p);
            }

            do { s = sr.ReadLine(); } while (!s.Contains("**polyhedron"));
            s = sr.ReadLine();
            int polyhedra_count = int.Parse(s);
            polyhedra = new int[polyhedra_count][];
            for (int i = 0; i < polyhedra_count; i++)
            {
                s = sr.ReadLine();
                string[] parts = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                int id = int.Parse(parts[0]);
                Debug.Assert(id - 1 == i);
                int num_facets = int.Parse(parts[1]);
                polyhedra[i] = new int[num_facets];
                for (int j = 0; j < num_facets; j++)
                {
                    int idx = int.Parse(parts[2 + j]);
                    polyhedra[i][j] = Math.Abs(idx) - 1;
                }
            }

            sr.Close();


            allNodes = new List<Node>();
            for (int i = 0; i < nodes.Length; i++) allNodes.Add(new Node(i, nodes[i]));

            allPolygons = new List<Poly>(); // for drawing and loading
            for(int i=0;i<polygons.Length;i++)
            {
                Poly p = new Poly();
                p.id = i;
                allPolygons.Add(p);
                p.nodes = new Node[polygons[i].Length];
                for (int j = 0; j < polygons[i].Length; j++) p.nodes[j] = allNodes[polygons[i][j]];
            }

            allGrains = new List<Grain>();
            for(int i=0;i<polyhedra.Length;i++)
            {
                Grain g = new Grain();
                allGrains.Add(g);
                g.polygons = new Poly[polyhedra[i].Length];
                for (int j = 0; j < polyhedra[i].Length; j++) g.polygons[j] = allPolygons[polyhedra[i][j]];
            }
            PrepareEdges();
        }

        public void Draw(Color color)
        {
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);
            GL.Enable(EnableCap.Lighting);

            foreach(Poly p in allPolygons)
            {
                GL.Begin(PrimitiveType.Polygon);
                GL.Color3(color);
                for (int j = 0; j < p.nodes.Length; j++)
                    GL.Vertex3(p.nodes[j].vec);
                GL.End();

            }

            GL.LineWidth(1f);
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Black);

            foreach (Poly p in allPolygons)
            {
                for (int j = 0; j < p.nodes.Length; j++)
                {
                    GL.Vertex3(p.nodes[j].vec);
                    GL.Vertex3(p.nodes[(j+1)%p.nodes.Length].vec);
                }
            }

            GL.End();
        }


        #region .geo 

        public int[][] face_edges;
        public (int, int)[] edges;

        void PrepareEdges()
        {
            HashSet<(int, int)> hsEdges = new HashSet<(int, int)>();
            foreach(Poly p in allPolygons) 
                for(int i=0;i<p.nodes.Length;i++)
                {
                    int n1 = p.nodes[i].id;
                    int n2 = p.nodes[(i + 1)%p.nodes.Length].id;
                    (int, int) edge = n1 > n2 ? (n1, n2) : (n2, n1);
                    hsEdges.Add(edge);
                }
            // make array and dictionary
            edges = hsEdges.ToArray();
            Dictionary<(int, int), int> edgeIds = new Dictionary<(int, int), int>();
            for (int i = 0; i < edges.Length; i++) edgeIds.Add(edges[i], i + 1);

            // note that in the dictionary edge indices start from 1, but in the array they are stored from 0
            // populate edges[] in each polygon
            face_edges = new int[allPolygons.Count][];
            foreach (Poly p in allPolygons)
            {
                p.edges = new int[p.nodes.Length];
                for (int i = 0; i < p.nodes.Length; i++)
                {
                    int n1 = p.nodes[i].id;
                    int n2 = p.nodes[(i + 1) % p.nodes.Length].id;
                    int sign;
                    (int, int) edge;
                    if (n1 > n2)
                    {
                        sign = 1;
                        edge = (n1, n2);
                    }
                    else
                    {
                        sign = -1;
                        edge = (n2, n1);
                    }
                    p.edges[i] = edgeIds[edge] * sign;
                }
                face_edges[p.id] = p.edges;
            }
        }

        #endregion
    }

}
