﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Linq;

namespace icFlow
{
    // identifies part of a surface as a collection of Face objects
    // allows to define the behavior, e.g. application of force or displacement to surface
    [Serializable]
    public class SurfaceFragment
    {
        public string name { get; set; }
        public int id { get; set; }
        public bool sensor { get; set; }
        public List<int> faces = new List<int>();
        public enum SurfaceRole { Free, Anchored, Forced, Rotated };
        public SurfaceRole role { get; set; } = SurfaceRole.Free;
        [Category("Stress")]
        public double px { get; set; }
        [Category("Stress")]
        public double py { get; set; }
        [Category("Stress")]
        public double pz { get; set; }

        [Category("Displacement")]
        public double dx { get; set; }
        [Category("Displacement")]
        public double dy { get; set; }
        [Category("Displacement")]
        public double dz { get; set; }

        [Category("Computed")]
        public double fx { get; set; }
        [Category("Computed")]
        public double fy { get; set; }
        [Category("Computed")]
        public double fz { get; set; }

        public double area { get; set; }

        [Category("Application")]
        [Description("If nonzero, apply force/displacement values gradually")]
        public double applicationTime { get; set; } = 1000;

        public override string ToString() {
            return name;
        }

        [NonSerialized]
        public List<Face> allFaces;

        public int nodeCount { get { return nodes.Count; } }

        [NonSerialized]
        public HashSet<Node> _nodes;
        public HashSet<Node> nodes { get {
                if(_nodes == null)
                {
                    _nodes = new HashSet<Node>();
                    foreach (int idx in faces)
                    {
                        Face f = allFaces[idx];
                        foreach (Node nd in f.vrts) nodes.Add(nd);
                    }
                }
                return _nodes;
            } }

        int[] nodalIndices;

        [OnSerializing()]
        internal void OnSerializingMethod(StreamingContext context)
        {
            nodalIndices = nodes.Select(nd => nd.id).ToArray();
        }

        public void RestoreNodes(List<Node> nds)
        {
            _nodes = new HashSet<Node>();
            foreach (int i in nodalIndices) _nodes.Add(nds[i]); 
        }

        public void ComputeTotalForce()
        {
            fx = fy = fz = 0;
            foreach(Node nd in nodes)
            {
                fx += nd.fx;
                fy += nd.fy;
                fz += nd.fz;
            }
        }
        public void ComputeArea()
        {
            area = 0;
            foreach (int i in faces)
            {
                Face f = allFaces[i];
                area += f.area;
            }
        }

        public double AverageVerticalDisplacement()
        {
            double result = 0;
            foreach (Node nd in nodes) result += nd.uz;
            result /= nodes.Count;
            return result;
        }
    }
}
