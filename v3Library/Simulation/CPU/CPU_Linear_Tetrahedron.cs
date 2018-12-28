using System;
using System.Diagnostics;
using System.Threading.Tasks;
using static System.Math;

namespace icFlow
{
    public static class CPU_Linear_Tetrahedron
    {
        public class ElementParams
        {
            public double Y; // young's modulus
            public double rho; // density
            public double nu; // poisson ratio
        }
        public class IntegrationParams
        {
            public double NewmarkBeta, NewmarkGamma, dampingMass, dampingStiffness, gravity;
        }
        public class ElementResult
        {
            public Element elem;
            public double[] rhs = new double[12];
            public double[,] LHS = new double[12,12];
            public double[] stress;
            public double[] principalStress = new double[3];
        }

        #region initialization

        static double[,] M;
        static CPU_Linear_Tetrahedron()
        {
            M = new double[12, 12];
            double coeff = 1D / 20D;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    for (int m = 0; m < 3; m++)
                    {
                        int col = i * 3 + m;
                        int row = j * 3 + m;
                        M[col, row] = (col == row) ? 2 * coeff : coeff;
                    }
        }

        #endregion


        #region math

        static void multAX(double a11, double a12, double a13,
    double a21, double a22, double a23,
    double a31, double a32, double a33,
    double x1, double x2, double x3,
    out double y1, out double y2, out double y3)
        {
            y1 = x1 * a11 + x2 * a12 + x3 * a13;
            y2 = x1 * a21 + x2 * a22 + x3 * a23;
            y3 = x1 * a31 + x2 * a32 + x3 * a33;
        }

        // matrix multiplication M = A * B
        static void multABd(double a11, double a12, double a13,
    double a21, double a22, double a23,
    double a31, double a32, double a33,
    double b11, double b12, double b13,
    double b21, double b22, double b23,
    double b31, double b32, double b33,
    out double m11, out double m12, out double m13,
    out double m21, out double m22, out double m23,
    out double m31, out double m32, out double m33)
        {
            m11 = a11 * b11 + a12 * b21 + a13 * b31; m12 = a11 * b12 + a12 * b22 + a13 * b32; m13 = a11 * b13 + a12 * b23 + a13 * b33;
            m21 = a21 * b11 + a22 * b21 + a23 * b31; m22 = a21 * b12 + a22 * b22 + a23 * b32; m23 = a21 * b13 + a22 * b23 + a23 * b33;
            m31 = a31 * b11 + a32 * b21 + a33 * b31; m32 = a31 * b12 + a32 * b22 + a33 * b32; m33 = a31 * b13 + a32 * b23 + a33 * b33;
        }

        static void fastRotationMatrix(
    double p0x, double p0y, double p0z,
    double p1x, double p1y, double p1z,
    double p2x, double p2y, double p2z,
    out double r11, out double r12, out double r13,
    out double r21, out double r22, out double r23,
    out double r31, out double r32, out double r33)
        {
            double d10x = p1x - p0x;
            double d10y = p1y - p0y;
            double d10z = p1z - p0z;

            double mag = Sqrt(d10x * d10x + d10y * d10y + d10z * d10z);
            r11 = d10x / mag;
            r21 = d10y / mag;
            r31 = d10z / mag;

            // p2-p0
            double wx = p2x - p0x;
            double wy = p2y - p0y;
            double wz = p2z - p0z;

            // cross product
            double cx = -d10z * wy + d10y * wz;
            double cy = d10z * wx - d10x * wz;
            double cz = -d10y * wx + d10x * wy;

            mag = Sqrt(cx * cx + cy * cy + cz * cz);
            r12 = cx / mag;
            r22 = cy / mag;
            r32 = cz / mag;

            r13 = r22 * r31 - r21 * r32;
            r23 = -r12 * r31 + r11 * r32;
            r33 = r12 * r21 - r11 * r22;
            mag = Sqrt(r13 * r13 + r23 * r23 + r33 * r33);
            r13 /= mag;
            r23 /= mag;
            r33 /= mag;
        }

        #endregion


        #region corotational model


        /// <summary>
        /// the results of this function subsequently go into the equaiton of motion
        /// </summary>
        /// <param name="x0">X0.</param>
        /// <param name="un">Un.</param>
        /// <param name="E">E.</param>
        /// <param name="f">elastic forces acting on nodes</param>
        /// <param name="Df">df/dx</param>
        /// <param name="V">tetrahedron rest volume </param>
        /// <param name="sigma">stress, in local coordinates, 6 components</param>
        static void F_and_Df_Corotational(
            double[] x0, double[] un, double[,] E,
            out double[] f, out double[,] Df, out double V,
            out double[] sigma)
        {
            Debug.Assert(x0.Length == 12 && un.Length == 12);
            // Colorational formulation:
            // f = RK(Rt xc - x0)
            // Df = R K Rt

            double[] xc = new double[12];
            for (int i = 0; i < 12; i++) xc[i] = x0[i] + un[i];

            // calculate K
            double x1, x2, x3, x4, y1, y2, y3, y4, z1, z2, z3, z4;
            double x12, x13, x14, x23, x24, x34, x21, x31, x32, x42, x43, y12, y13, y14, y23, y24, y34;
            double y21, y31, y32, y42, y43, z12, z13, z14, z23, z24, z34, z21, z31, z32, z42, z43;
            double a1, a2, a3, a4, b1, b2, b3, b4, c1, c2, c3, c4;
            double Jdet;
            x1 = x0[0]; y1 = x0[1]; z1 = x0[2];
            x2 = x0[3]; y2 = x0[4]; z2 = x0[5];
            x3 = x0[6]; y3 = x0[7]; z3 = x0[8];
            x4 = x0[9]; y4 = x0[10]; z4 = x0[11];

            x12 = x1 - x2; x13 = x1 - x3; x14 = x1 - x4; x23 = x2 - x3; x24 = x2 - x4; x34 = x3 - x4;
            x21 = -x12; x31 = -x13; x32 = -x23; x42 = -x24; x43 = -x34;
            y12 = y1 - y2; y13 = y1 - y3; y14 = y1 - y4; y23 = y2 - y3; y24 = y2 - y4; y34 = y3 - y4;
            y21 = -y12; y31 = -y13; y32 = -y23; y42 = -y24; y43 = -y34;
            z12 = z1 - z2; z13 = z1 - z3; z14 = z1 - z4; z23 = z2 - z3; z24 = z2 - z4; z34 = z3 - z4;
            z21 = -z12; z31 = -z13; z32 = -z23; z42 = -z24; z43 = -z34;
            Jdet = x21 * (y23 * z34 - y34 * z23) + x32 * (y34 * z12 - y12 * z34) + x43 * (y12 * z23 - y23 * z12);
            V = Jdet / 6D;

            a1 = y42 * z32 - y32 * z42; b1 = x32 * z42 - x42 * z32; c1 = x42 * y32 - x32 * y42;
            a2 = y31 * z43 - y34 * z13; b2 = x43 * z31 - x13 * z34; c2 = x31 * y43 - x34 * y13;
            a3 = y24 * z14 - y14 * z24; b3 = x14 * z24 - x24 * z14; c3 = x24 * y14 - x14 * y24;
            a4 = y13 * z21 - y12 * z31; b4 = x21 * z13 - x31 * z12; c4 = x13 * y21 - x12 * y31;

            a1 /= Jdet; a2 /= Jdet; a3 /= Jdet; a4 /= Jdet;
            b1 /= Jdet; b2 /= Jdet; b3 /= Jdet; b4 /= Jdet;
            c1 /= Jdet; c2 /= Jdet; c3 /= Jdet; c4 /= Jdet;

            double[,] B = {
        { a1, 0, 0, a2, 0, 0, a3, 0, 0, a4, 0, 0 },
        { 0, b1, 0, 0, b2, 0, 0, b3, 0, 0, b4, 0 },
        { 0, 0, c1, 0, 0, c2, 0, 0, c3, 0, 0, c4 },
        { b1, a1, 0, b2, a2, 0, b3, a3, 0, b4, a4, 0 },
        { 0, c1, b1, 0, c2, b2, 0, c3, b3, 0, c4, b4 },
        { c1, 0, a1, c2, 0, a2, c3, 0, a3, c4, 0, a4 } };

            double[,] BtE = new double[12, 6];
            //	double BtE[12][6] = {}; // result of multiplication (Bt x E)
            for (int r = 0; r < 12; r++)
                for (int c = 0; c < 6; c++)
                    for (int i = 0; i < 6; i++) BtE[r, c] += B[i, r] * E[i, c];

            // K = Bt x E x B x V
            double[,] K = new double[12, 12];
            for (int r = 0; r < 12; r++)
                for (int c = 0; c < 12; c++)
                    for (int i = 0; i < 6; i++) K[r, c] += BtE[r, i] * B[i, c] * V;

            double[,] R0 = new double[3, 3], R1 = new double[3, 3], R = new double[3, 3];
            fastRotationMatrix(
                x0[0], x0[1], x0[2],
                x0[3], x0[4], x0[5],
                x0[6], x0[7], x0[8],
                out R0[0, 0], out R0[0, 1], out R0[0, 2],
                out R0[1, 0], out R0[1, 1], out R0[1, 2],
                out R0[2, 0], out R0[2, 1], out R0[2, 2]);

            fastRotationMatrix(
                xc[0], xc[1], xc[2],
                xc[3], xc[4], xc[5],
                xc[6], xc[7], xc[8],
                out R1[0, 0], out R1[0, 1], out R1[0, 2],
                out R1[1, 0], out R1[1, 1], out R1[1, 2],
                out R1[2, 0], out R1[2, 1], out R1[2, 2]);

            multABd(
                R1[0, 0], R1[0, 1], R1[0, 2],
                R1[1, 0], R1[1, 1], R1[1, 2],
                R1[2, 0], R1[2, 1], R1[2, 2],
                R0[0, 0], R0[1, 0], R0[2, 0],
                R0[0, 1], R0[1, 1], R0[2, 1],
                R0[0, 2], R0[1, 2], R0[2, 2],
                out R[0, 0], out R[0, 1], out R[0, 2],
                out R[1, 0], out R[1, 1], out R[1, 2],
                out R[2, 0], out R[2, 1], out R[2, 2]);

            double[,] RK = new double[12, 12];
            double[,] RKRt = new double[12, 12];

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    // RK = R * K
                    for (int k = 0; k < 3; k++)
                        for (int l = 0; l < 3; l++)
                        {
                            for (int m = 0; m < 3; m++)
                                RK[3 * i + k, 3 * j + l] += R[k, m] * K[3 * i + m, 3 * j + l];
                        }

                    // RKRT = RK * R^T
                    for (int k = 0; k < 3; k++)
                        for (int l = 0; l < 3; l++)
                        {
                            for (int m = 0; m < 3; m++)
                                RKRt[3 * i + k, 3 * j + l] += RK[3 * i + k, 3 * j + m] * R[l, m];
                        }
                }

            // xr = Rt xc
            double[] xr = new double[12];
            multAX(R[0, 0], R[1, 0], R[2, 0],
                R[0, 1], R[1, 1], R[2, 1],
                R[0, 2], R[1, 2], R[2, 2],
                xc[0], xc[1], xc[2],
                out xr[0], out xr[1], out xr[2]);
            multAX(R[0, 0], R[1, 0], R[2, 0],
                R[0, 1], R[1, 1], R[2, 1],
                R[0, 2], R[1, 2], R[2, 2],
                xc[3], xc[4], xc[5],
                out xr[3], out xr[4], out xr[5]);
            multAX(R[0, 0], R[1, 0], R[2, 0],
                R[0, 1], R[1, 1], R[2, 1],
                R[0, 2], R[1, 2], R[2, 2],
                xc[6], xc[7], xc[8],
                out xr[6], out xr[7], out xr[8]);
            multAX(R[0, 0], R[1, 0], R[2, 0],
                R[0, 1], R[1, 1], R[2, 1],
                R[0, 2], R[1, 2], R[2, 2],
                xc[9], xc[10], xc[11],
                out xr[9], out xr[10], out xr[11]);

            for (int i = 0; i < 12; i++) xr[i] -= x0[i];

            // f = RK(Rt pm - mx)
            // Df = RKRt
            f = new double[12];
            Df = new double[12, 12];
            for (int i = 0; i < 12; i++)
                for (int j = 0; j < 12; j++)
                {
                    f[i] += RK[i, j] * xr[j];
                    Df[i, j] = RKRt[i, j];
                }

            // calculation of strain (rotation excluded) e = B.xr
            // B[6][12]
            double[] e = new double[6];
            for (int j = 0; j < 6; j++)
                for (int i = 0; i < 12; i++)
                    e[j] += B[j, i] * xr[i];

            // calculation of stress (rotation excluded) s = E.e
            // E[6][6]
            sigma = new double[6];
            for (int j = 0; j < 6; j++)
                for (int i = 0; i < 6; i++)
                    sigma[j] += E[i, j] * e[i];
        }

        #endregion


        #region element forces

        static ElementResult ElementElasticity(Element elem, ElementParams prms, IntegrationParams iprms, double h)
        {

            double[] x0, un, vn, an;
            x0 = new double[12];
            un = new double[12];
            vn = new double[12];
            an = new double[12];

            for (int ndidx = 0; ndidx < 4; ndidx++)
            {
                Node nd = elem.vrts[ndidx];
                x0[0 + ndidx * 3] = nd.x0;
                x0[1 + ndidx * 3] = nd.y0;
                x0[2 + ndidx * 3] = nd.z0;

                un[0 + ndidx * 3] = nd.unx;
                un[1 + ndidx * 3] = nd.uny;
                un[2 + ndidx * 3] = nd.unz;

                vn[0 + ndidx * 3] = nd.vnx;
                vn[1 + ndidx * 3] = nd.vny;
                vn[2 + ndidx * 3] = nd.vnz;

                an[0 + ndidx * 3] = nd.anx;
                an[1 + ndidx * 3] = nd.any;
                an[2 + ndidx * 3] = nd.anz;
            }

            // compute E

            double[,] E = new double[6, 6];
            double coeff1 = prms.Y / ((1D + prms.nu) * (1D - 2D * prms.nu));
            E[0, 0] = E[1, 1] = E[2, 2] = (1D - prms.nu) * coeff1;
            E[0, 1] = E[0, 2] = E[1, 2] = E[1, 0] = E[2, 0] = E[2, 1] = prms.nu * coeff1;
            E[3, 3] = E[4, 4] = E[5, 5] = (0.5 - prms.nu) * coeff1;


            ElementResult result = new ElementResult();
            result.elem = elem;
            double[] f;
            double[,] Df;
            double V;
            F_and_Df_Corotational(x0, un, E, out f, out Df, out V, out result.stress);

            double gravityForcePerNode = iprms.gravity * prms.rho * V / 4;
            result.rhs[2] += gravityForcePerNode;
            result.rhs[5] += gravityForcePerNode;
            result.rhs[8] += gravityForcePerNode;
            result.rhs[11] += gravityForcePerNode;


            // assemble the effective stiffness matrix Keff = M/(h^2 beta) + RKRt + D * gamma /(h beta) 
            // where D is the damping matrix D = a M + b K
            double massCoeff = prms.rho * V * (1.0 / (h * h) + iprms.dampingMass * iprms.NewmarkGamma / h) / iprms.NewmarkBeta;
            double stiffCoeff = 1.0 + iprms.dampingStiffness * iprms.NewmarkGamma / (h * iprms.NewmarkBeta);

            // add damping component to rhs
            // D = M[i][j] * V * dampingMass + RKRt[i][j] * dampingStiffness

            for (int i = 0; i < 12; i++)
            {
                result.rhs[i] -= f[i];
                for (int j = 0; j < 12; j++)
                {
                    result.rhs[i] -= (M[i,j] * prms.rho * V * iprms.dampingMass + Df[i,j] * iprms.dampingStiffness) * vn[j] + (M[i,j] * prms.rho * V * an[j]);
                    result.LHS[i,j] = Df[i,j] * stiffCoeff + M[i,j] * massCoeff;
                }
            }
            return result;
        }

        #endregion

        public static ElementResult[] AssembleElems(LinearSystem ls, ref FrameInfo cf, MeshCollection mc, ModelPrms prms)
        {
            int nElems = mc.elasticElements.Length;
            cf.nElems = nElems;
            ElementResult[] elemResults = new ElementResult[nElems];

            ElementParams ep = new ElementParams();
            ep.nu = prms.nu;
            ep.rho = prms.rho;
            ep.Y = prms.Y;

            IntegrationParams ip = new IntegrationParams();
            ip.dampingMass = prms.dampingMass;
            ip.dampingStiffness = prms.dampingStiffness;
            ip.gravity = prms.gravity;
            ip.NewmarkBeta = prms.NewmarkBeta;
            ip.NewmarkGamma = prms.NewmarkGamma;

            double h = cf.TimeStep;

            // compute results per element in parallel
            Parallel.For(0, nElems, i => {
                Element elem = mc.elasticElements[i];
                elemResults[i] = ElementElasticity(elem, ep, ip, h);
                });

            // distribute results into linear system
            for(int idx=0;idx<nElems;idx++)
            {
                ElementResult eres = elemResults[idx];
                double[,] lhs = eres.LHS;
                Element elem = mc.elasticElements[idx];
                for(int r=0;r<4;r++)
                {
                    int ni = elem.vrts[r].altId;
                    ls.AddToRHS(ni, eres.rhs[r * 3 + 0], eres.rhs[r * 3 + 1], eres.rhs[r * 3 + 2]);
                    for (int c=0;c<4;c++)
                    {
                        int nj = elem.vrts[c].altId;
                        ls.AddToLHS_Symmetric(ni, nj,
                        lhs[r * 3 + 0, c * 3 + 0], lhs[r * 3 + 0, c * 3 + 1], lhs[r * 3 + 0, c * 3 + 2],
                        lhs[r * 3 + 1, c * 3 + 0], lhs[r * 3 + 1, c * 3 + 1], lhs[r * 3 + 1, c * 3 + 2],
                        lhs[r * 3 + 2, c * 3 + 0], lhs[r * 3 + 2, c * 3 + 1], lhs[r * 3 + 2, c * 3 + 2]);
                    }
                }
            }
            return elemResults;
        }

        public static void TransferUpdatedState(ElementResult[] eresults, MeshCollection mc)
        {
            // compute principal stresses (not implemented)



            // update forces per node
            foreach(Node nd in mc.activeNodes) nd.fx = nd.fy = nd.fz = 0;

            foreach (ElementResult eres in eresults)
            {
                Node[] vrts = eres.elem.vrts;
                for (int r = 0; r < 4; r++)
                {
                    Node nd = vrts[r];
                    int ni = nd.altId;
                    nd.fx += eres.rhs[r * 3 + 0];
                    nd.fy += eres.rhs[r * 3 + 1];
                    nd.fz += eres.rhs[r * 3 + 2];
                }
            }

        }
    }
}
