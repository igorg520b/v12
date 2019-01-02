using System;
using System.Threading.Tasks;
namespace icFlow
{
    public static class CPU_Collision_Response
    {
        public class CPResult
        {
            public double[] fi = new double[12]; 
            public double[,] dfi = new double[12,12];
            public int[] idxs = new int[4];

            public void Clear()
            {
                Array.Clear(fi, 0, fi.Length);
                Array.Clear(dfi, 0, dfi.Length);
                Array.Clear(idxs, 0, idxs.Length);
            }
        }

        static unsafe CPResult OneCollision(Node nd, Face fc, double distanceEpsilonSqared, double k, CPResult res)
        {
            res.Clear();
            double* x = stackalloc double[12];
            x[0] = nd.tx;
            x[1] = nd.ty;
            x[2] = nd.tz;
            x[3] = fc.vrts[0].tx;
            x[4] = fc.vrts[0].ty;
            x[5] = fc.vrts[0].tz;
            x[6] = fc.vrts[1].tx;
            x[7] = fc.vrts[1].ty;
            x[8] = fc.vrts[1].tz;
            x[9] = fc.vrts[2].tx;
            x[10] = fc.vrts[2].ty;
            x[11] = fc.vrts[2].tz;

            res.idxs[0] = nd.altId;
            res.idxs[1] = fc.vrts[0].altId;
            res.idxs[2] = fc.vrts[1].altId;
            res.idxs[3] = fc.vrts[2].altId;

            // exclude colliding rigid surfaces
            if (res.idxs[0] < 0 && res.idxs[1] < 0 && res.idxs[2] < 0 && res.idxs[3] < 0) return null;

            double* fd = stackalloc double[12];
            double* sd = stackalloc double[144]; //12x12
            double* w = stackalloc double[3];
            double dsq = CPU_Distance_Derivatives.pt(x, fd, sd, out w[1], out w[2]);

            if (dsq < distanceEpsilonSqared) return null;

            w[0] = 1 - (w[1] + w[2]);

            double fx, fy, fz;
            fx = k * 0.5 * fd[0];
            fy = k * 0.5 * fd[1];
            fz = k * 0.5 * fd[2];

            double[] fi = new double[12];
            fi[0] = -fx;
            fi[1] = -fy;
            fi[2] = -fz;
            fi[3] = w[0] * fx;
            fi[4] = w[0] * fy;
            fi[5] = w[0] * fz;
            fi[6] = w[1] * fx;
            fi[7] = w[1] * fy;
            fi[8] = w[1] * fz;
            fi[9] = w[2] * fx;
            fi[10] = w[2] * fy;
            fi[11] = w[2] * fz;
            res.fi = fi;

            double[,] dfij = new double[12, 12];
            for (int i = 0; i < 12; i++)
                for (int j = i; j < 12; j++)
                    dfij[i, j] = dfij[j, i] = k * sd[i*12+ j] / 2;
            res.dfi = dfij;
            return res;
        }

        public static void CollisionResponse((Node, Face)[] cp, LinearSystem ls, ref FrameInfo cf, 
        MeshCollection mc, ModelPrms prms, ExtendableList<CPResult> cprList)
        {
            if (cp == null) return;
            int nCollisions = cp.Length;
            cf.nCollisions = nCollisions;
            cprList.actualCount = nCollisions;
            double distanceEpsilonSqared = prms.DistanceEpsilon * prms.DistanceEpsilon;
            double k = prms.penaltyK;

            Parallel.For(0, nCollisions, i => {
                (Node nd, Face fc) = cp[i];
                CPResult cpr = cprList[i];
                OneCollision(nd, fc, distanceEpsilonSqared, k, cpr);
                });

            for(int i=0;i<nCollisions;i++)
            {
                CPResult res = cprList[i];
                if (res == null) continue;
                double[,] lhs = res.dfi;
                for (int r = 0; r < 4; r++)
                {
                    int ni = res.idxs[r];
                    ls.AddToRHS(ni, res.fi[r * 3 + 0], res.fi[r * 3 + 1], res.fi[r * 3 + 2]);
                    for (int c = 0; c < 4; c++)
                    {
                        int nj = res.idxs[r];
                        ls.AddToLHS_Symmetric(ni, nj,
                        lhs[r * 3 + 0, c * 3 + 0], lhs[r * 3 + 0, c * 3 + 1], lhs[r * 3 + 0, c * 3 + 2],
                        lhs[r * 3 + 1, c * 3 + 0], lhs[r * 3 + 1, c * 3 + 1], lhs[r * 3 + 1, c * 3 + 2],
                        lhs[r * 3 + 2, c * 3 + 0], lhs[r * 3 + 2, c * 3 + 1], lhs[r * 3 + 2, c * 3 + 2]);
                    }
                }
            }

        }

    }
}
