using System;
using System.Diagnostics;

namespace icFlow
{
    public class Face
    {
        public Node[] vrts = new Node[3];
        public Element elem;                    // element that the face belongs to (null unless CZs are inserted)
        public int id;                          // sequential id
        public int globalFaceId;                // in global array
        public int granule;                     // to which granule the element belongs
        public int tag;                         // surface partition id
        public bool exposed = true;             // is this an outside surface
        public bool created = false;            // got exposed at simulation time due to fracture
        public double pnorm;                    // normal pressure on the face from collisions 

        (double x, double y, double z) Cross(double e0x, double e0y, double e0z, double e1x, double e1y, double e1z)
        {
            double x = -(e0z * e1y) + e0y * e1z;
            double y = e0z * e1x - e0x * e1z;
            double z = -(e0y * e1x) + e0x * e1y;
            return (x, y, z);
        }

        public double area { get
            {
                double tx0 = vrts[0].x0;
                double ty0 = vrts[0].y0;
                double tz0 = vrts[0].z0;
                double tx1 = vrts[1].x0;
                double ty1 = vrts[1].y0;
                double tz1 = vrts[1].z0;
                double tx2 = vrts[2].x0;
                double ty2 = vrts[2].y0;
                double tz2 = vrts[2].z0;

                var (x, y, z) = Cross(tx1 - tx0, ty1 - ty0, tz1 - tz0, tx2 - tx0, ty2 - ty0, tz2 - tz0);
                double halfMag = Math.Sqrt(x * x + y * y + z * z)/2;

                return halfMag;
            }
        }

    }
}
