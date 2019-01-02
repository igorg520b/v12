using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;

namespace icFlow
{
    public static class CPU_NarrowPhase
    {
        const double EPS = 1e-10;

        #region math
        static void Bvalues(double x0, double y0, double z0,
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double x3, double y3, double z3,
            out double b11, out double b12, out double b13,
            out double b21, out double b22, out double b23,
            out double b31, out double b32, out double b33)
        {
            double a11, a12, a13, a21, a22, a23, a31, a32, a33;
            a11 = x1 - x0;
            a12 = x2 - x0;
            a13 = x3 - x0;
            a21 = y1 - y0;
            a22 = y2 - y0;
            a23 = y3 - y0;
            a31 = z1 - z0;
            a32 = z2 - z0;
            a33 = z3 - z0;

            // inverse
            double det = a31 * (-a13 * a22 + a12 * a23) + a32 * (a13 * a21 - a11 * a23) + a33 * (-a12 * a21 + a11 * a22);
            b11 = (-a23 * a32 + a22 * a33) / det;
            b12 = (a13 * a32 - a12 * a33) / det;
            b13 = (-a13 * a22 + a12 * a23) / det;
            b21 = (a23 * a31 - a21 * a33) / det;
            b22 = (-a13 * a31 + a11 * a33) / det;
            b23 = (a13 * a21 - a11 * a23) / det;
            b31 = (-a22 * a31 + a21 * a32) / det;
            b32 = (a12 * a31 - a11 * a32) / det;
            b33 = (-a12 * a21 + a11 * a22) / det;
        }


        static bool ctest(double b11, double b12, double b13,
            double b21, double b22, double b23,
            double b31, double b32, double b33,
            double x, double y, double z)
        {

            double y1 = x * b11 + y * b12 + z * b13;
            double y2 = x * b21 + y * b22 + z * b23;
            double y3 = x * b31 + y * b32 + z * b33;
            return (y1 > EPS && y2 > EPS && y3 > EPS && (y1 + y2 + y3) < (1 - EPS));
        }
        #endregion

        #region narrow phase

        static unsafe int NarrowPhaseTwoElems(Element tetra1, Element tetra2)
        {
            double* nds = stackalloc double[24];
            int* nd_idxs = stackalloc int[8];
            for (int i = 0; i < 4; i++)
            {
                nd_idxs[i] = tetra1.vrts[i].globalNodeId;
                nd_idxs[i + 4] = tetra2.vrts[i].globalNodeId;
                nds[i * 3 + 0] = tetra1.vrts[i].tx;
                nds[i * 3 + 1] = tetra1.vrts[i].ty;
                nds[i * 3 + 2] = tetra1.vrts[i].tz;

                nds[(i + 4) * 3 + 0] = tetra2.vrts[i].tx;
                nds[(i + 4) * 3 + 1] = tetra2.vrts[i].ty;
                nds[(i + 4) * 3 + 2] = tetra2.vrts[i].tz;
            }

            // verify that elements are non-adjacent
            for (int i = 0; i < 4; i++)
                for (int j = 4; j < 8; j++)
                    if (nd_idxs[i] == nd_idxs[j]) return 0;

            // b-values for elements
            double* bv0 = stackalloc double[9]; 
            double* bv1 = stackalloc double[9];

            Bvalues(nds[0], nds[1], nds[2],
            nds[3], nds[4], nds[5],
            nds[6], nds[7], nds[8],
            nds[9], nds[10], nds[11],
            out bv0[0], out bv0[1], out bv0[2],
            out bv0[3], out bv0[4], out bv0[5],
            out bv0[6], out bv0[7], out bv0[8]);

            Bvalues(nds[12], nds[13], nds[14],
            nds[15], nds[16], nds[17],
            nds[18], nds[19], nds[20],
            nds[21], nds[22], nds[23],
            out bv1[0], out bv1[1], out bv1[2],
            out bv1[3], out bv1[4], out bv1[5],
            out bv1[6], out bv1[7], out bv1[8]);

            int result = 0;
            // perform tests
            bool bres;
            double x0 = nds[0];
            double y0 = nds[1];
            double z0 = nds[2];

            // test if 1 element nodes are inside element 0
            bres = ctest(bv0[0], bv0[1], bv0[2],
            bv0[3], bv0[4], bv0[5],
            bv0[6], bv0[7], bv0[8],
            nds[12] - x0, nds[13] - y0, nds[14] - z0);
            if (bres) result |= (1);

            bres = ctest(bv0[0], bv0[1], bv0[2],
            bv0[3], bv0[4], bv0[5],
            bv0[6], bv0[7], bv0[8],
            nds[15] - x0, nds[16] - y0, nds[17] - z0);
            if (bres) result |= (2);

            bres = ctest(bv0[0], bv0[1], bv0[2],
            bv0[3], bv0[4], bv0[5],
            bv0[6], bv0[7], bv0[8],
            nds[18] - x0, nds[19] - y0, nds[20] - z0);
            if (bres) result |= (4);

            bres = ctest(bv0[0], bv0[1], bv0[2],
            bv0[3], bv0[4], bv0[5],
            bv0[6], bv0[7], bv0[8],
            nds[21] - x0, nds[22] - y0, nds[23] - z0);
            if (bres) result |= (8);

            // test if 0 element nodes are inside element 1
            x0 = nds[12];
            y0 = nds[13];
            z0 = nds[14];

            bres = ctest(bv1[0], bv1[1], bv1[2],
            bv1[3], bv1[4], bv1[5],
            bv1[6], bv1[7], bv1[8],
            nds[0] - x0, nds[1] - y0, nds[2] - z0);
            if (bres) result |= (16);

            bres = ctest(bv1[0], bv1[1], bv1[2],
            bv1[3], bv1[4], bv1[5],
            bv1[6], bv1[7], bv1[8],
            nds[3] - x0, nds[4] - y0, nds[5] - z0);
            if (bres) result |= (32);

            bres = ctest(bv1[0], bv1[1], bv1[2],
            bv1[3], bv1[4], bv1[5],
            bv1[6], bv1[7], bv1[8],
            nds[6] - x0, nds[7] - y0, nds[8] - z0);
            if (bres) result |= (64);

            bres = ctest(bv1[0], bv1[1], bv1[2],
            bv1[3], bv1[4], bv1[5],
            bv1[6], bv1[7], bv1[8],
            nds[9] - x0, nds[10] - y0, nds[11] - z0);
            if (bres) result |= (128);

            return result;
        }

        public static void NarrowPhase(List<Element> broadList, 
        ModelPrms prms, ref FrameInfo cf, MeshCollection mc,
            ExtendableList<CPU_Collision_Response.CPResult> cprList)
        {
            if (prms.CollisionScheme == ModelPrms.CollisionSchemes.None || broadList.Count == 0) 
            { 
                cf.nCollisions = 0;
                cprList.actualCount = 0;
                return;
            }

            int nTetra = broadList.Count;
            int nPairs = nTetra / 2;
            Debug.Assert(nTetra % 2 == 0, "broadList length is not even");

            int[] resultingList = new int[nPairs];

            Parallel.For(0, nPairs, item => {
                Element tetra1 = broadList[item * 2];
                Element tetra2 = broadList[item * 2 + 1];
                resultingList[item] = NarrowPhaseTwoElems(tetra1, tetra2);
            });

            // Tuple ( node# inside element, which element)
            HashSet<(int,int)> NL2set = new HashSet<(int,int)>();

            for (int i = 0; i < nPairs; i++)
                if (resultingList[i] != 0)
                {
                    int bits = resultingList[i];
                    if ((bits & 1) != 0) NL2set.Add((broadList[i * 2 + 1].vrts[0].globalNodeId, broadList[i * 2].globalElementId));
                    if ((bits & 2) != 0) NL2set.Add((broadList[i * 2 + 1].vrts[1].globalNodeId, broadList[i * 2].globalElementId));
                    if ((bits & 4) != 0) NL2set.Add((broadList[i * 2 + 1].vrts[2].globalNodeId, broadList[i * 2].globalElementId));
                    if ((bits & 8) != 0) NL2set.Add((broadList[i * 2 + 1].vrts[3].globalNodeId, broadList[i * 2].globalElementId));

                    if ((bits & 16) != 0) NL2set.Add((broadList[i * 2].vrts[0].globalNodeId, broadList[i * 2 + 1].globalElementId));
                    if ((bits & 32) != 0) NL2set.Add((broadList[i * 2].vrts[1].globalNodeId, broadList[i * 2 + 1].globalElementId));
                    if ((bits & 64) != 0) NL2set.Add((broadList[i * 2].vrts[2].globalNodeId, broadList[i * 2 + 1].globalElementId));
                    if ((bits & 128) != 0) NL2set.Add((broadList[i * 2].vrts[3].globalNodeId, broadList[i * 2 + 1].globalElementId));
                }

            nPairs = NL2set.Count;
            if (nPairs == 0)
            {
                cf.nCollisions = 0;
                cprList.actualCount = 0;
                return;
            }

            (int, int)[] arr = NL2set.ToArray();
            cprList.actualCount = arr.Length;
            Parallel.For(0, arr.Length, i => {
                Node nd = mc.allNodes[arr[i].Item1];
                Element elem = mc.surfaceElements[arr[i].Item2];
                Face fc = FindClosestFace(nd, elem);
                cprList[i].nd = nd;
                cprList[i].fc = fc;
            });
        }

        #endregion

        #region dtn math
        static double DOT(double[] v1, double[] v2) { return (v1[0] * v2[0] + v1[1] * v2[1] + v1[2] * v2[2]); }

        static void SUB(ref double[] dest, double[] v1, double[] v2) 
        { 
            dest[0] = v1[0] - v2[0];
            dest[1] = v1[1] - v2[1];
            dest[2] = v1[2] - v2[2]; 
            }

        static double clamp(double n)
        {
            return n <= 0 ? 0 : n >= 1 ? 1 : n;
        }

        // distance from triangle to node
        static double dtn(
            double f1x, double f1y, double f1z,
            double f2x, double f2y, double f2z,
            double f3x, double f3y, double f3z,
            double ndx, double ndy, double ndz)
        {

            double[] t0 = new double[3], t1 = new double[3], t2 = new double[3];
            double[] edge0 = new double[3], edge1 = new double[3];
            double[] v0 = new double[3], sourcePosition = new double[3];

            t0[0] = f1x; t0[1] = f1y; t0[2] = f1z;
            t1[0] = f2x; t1[1] = f2y; t1[2] = f2z;
            t2[0] = f3x; t2[1] = f3y; t2[2] = f3z;
            sourcePosition[0] = ndx; sourcePosition[1] = ndy; sourcePosition[2] = ndz;

            SUB(ref edge0, t1, t0);
            SUB(ref edge1, t2, t0);
            SUB(ref v0, t0, sourcePosition);

            double a = DOT(edge0, edge0);
            double b = DOT(edge0, edge1);
            double c = DOT(edge1, edge1);
            double d = DOT(edge0, v0);
            double e = DOT(edge1, v0);

            double det = a * c - b * b;
            double s = b * e - c * d;
            double t = b * d - a * e;

            if (s + t < det)
            {
                if (s < 0)
                {
                    if (t < 0)
                    {
                        if (d < 0)
                        {
                            s = clamp(-d / a);
                            t = 0;
                        }
                        else
                        {
                            s = 0;
                            t = clamp(-e / c);
                        }
                    }
                    else
                    {
                        s = 0;
                        t = clamp(-e / c);
                    }
                }
                else if (t < 0)
                {
                    s = clamp(-d / a);
                    t = 0;
                }
                else
                {
                    double invDet = 1.0 / det;
                    s *= invDet;
                    t *= invDet;
                }
            }
            else
            {
                if (s < 0)
                {
                    double tmp0 = b + d;
                    double tmp1 = c + e;
                    if (tmp1 > tmp0)
                    {
                        double numer = tmp1 - tmp0;
                        double denom = a - 2 * b + c;
                        s = clamp(numer / denom);
                        t = 1 - s;
                    }
                    else
                    {
                        t = clamp(-e / c);
                        s = 0;
                    }
                }
                else if (t < 0)
                {
                    if (a + d > b + e)
                    {
                        double numer = c + e - b - d;
                        double denom = a - 2 * b + c;
                        s = clamp(numer / denom);
                        t = 1 - s;
                    }
                    else
                    {
                        s = clamp(-e / c);
                        t = 0;
                    }
                }
                else
                {
                    double numer = c + e - b - d;
                    double denom = a - 2 * b + c;
                    s = clamp(numer / denom);
                    t = 1 - s;
                }
            }

            double[] d1 = new double[3];

            d1[0] = t0[0] + s * edge0[0] + t * edge1[0] - sourcePosition[0];
            d1[1] = t0[1] + s * edge0[1] + t * edge1[1] - sourcePosition[1];
            d1[2] = t0[2] + s * edge0[2] + t * edge1[2] - sourcePosition[2];

            double sqdist = d1[0] * d1[0] + d1[1] * d1[1] + d1[2] * d1[2];

            return sqdist; // squared
        }

        #endregion

        #region closest face

        static Face FindClosestFace(Node nd, Element elem)
        {
            double smallestDistance = double.MaxValue;
            Face closestFace = null;

            foreach(Face f in elem.adjFaces)
            {
                double dist = dtn(f.vrts[0].tx, f.vrts[0].ty, f.vrts[0].tz,
                f.vrts[1].tx, f.vrts[1].ty, f.vrts[1].tz,
                    f.vrts[2].tx, f.vrts[2].ty, f.vrts[2].tz,
                nd.tx, nd.ty, nd.tz);
                if (smallestDistance > dist)
                {
                    smallestDistance = dist;
                    closestFace = f;
                }
            }
            return closestFace;
        }

        #endregion

    }
}


