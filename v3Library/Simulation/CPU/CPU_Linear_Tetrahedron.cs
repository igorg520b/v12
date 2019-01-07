using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using static System.Math;

namespace icFlow
{
    public static class CPU_Linear_Tetrahedron
    {
        // element params
        public static double Y; // young's modulus
        public static double rho; // density
        public static double nu; // poisson ratio
        // integration params
        public static double NewmarkBeta, NewmarkGamma, dampingMass, dampingStiffness, gravity;
        static double h; // timestep

        readonly static double[,] E = new double[6, 6];
        readonly static double[,] M = new double[12,12]; // mass matrix (rho=1)

        public class ElementExtension
        {
            public double[] rhs = new double[12];
            public double[,] lhs = new double[12,12];

            public void Clear()
            {
                Array.Clear(rhs, 0, rhs.Length);
            }
        }

        #region initialization

        static CPU_Linear_Tetrahedron()
        {
            double coeff = 1D / 20D;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    for (int m = 0; m < 3; m++)
                    {
                        int col = i * 3 + m;
                        int row = j * 3 + m;
                        M[col,row] = (col == row) ? 2 * coeff : coeff;
                    }
        }

        static void InitializeElasticityTensor()
        {
            double coeff1 = Y / ((1D + nu) * (1D - 2D * nu));
            E[0, 0] = E[1, 1] = E[2, 2] = (1D - nu) * coeff1;
            E[0, 1] = E[0, 2] = E[1, 2] = E[1, 0] = E[2, 0] = E[2, 1] = nu * coeff1;
            E[3, 3] = E[4, 4] = E[5, 5] = (0.5 - nu) * coeff1;
        }

        #endregion

        #region math

        unsafe static void fastRotationMatrixI(double* x, double* R)
        {
            R[0] = 1;
            R[1] = R[2] = 0;
            R[3] = R[5] = 0;
            R[4] = R[8] = 1;
            R[6] = R[7] = 0;
        }


        unsafe static void fastRotationMatrix(double* x, double *R)
        {
            double d10x = x[3] - x[0];
            double d10y = x[4] - x[1];
            double d10z = x[5] - x[2];

            double mag = Sqrt(d10x * d10x + d10y * d10y + d10z * d10z);
            R[0] = d10x / mag; //11
            R[3] = d10y / mag; //21
            R[6] = d10z / mag; //31

            // p2-p0
            double wx = x[6] - x[0];
            double wy = x[7] - x[1];
            double wz = x[8] - x[2];

            // cross product
            double cx = -d10z * wy + d10y * wz;
            double cy = d10z * wx - d10x * wz;
            double cz = -d10y * wx + d10x * wy;

            mag = Sqrt(cx * cx + cy * cy + cz * cz);
            R[1] = cx / mag; //12
            R[4] = cy / mag; //22
            R[7] = cz / mag; //32

            R[2] = R[4] * R[6] - R[3] * R[7];
            R[5] = -R[1] * R[6] + R[0] * R[7];
            R[8] = R[1] * R[3] - R[0] * R[4];
            mag = Sqrt(R[2] * R[2] + R[5] * R[5] + R[8] * R[8]);
            R[2] /= mag;
            R[5] /= mag;
            R[8] /= mag;
        }

        unsafe static void transposeAndMultiplyByVector3(double* R, 
double x1, double x2, double x3,
out double y1, out double y2, out double y3)
        {
            y1 = x1 * R[0] + x2 * R[3] + x3 * R[6];
            y2 = x1 * R[1] + x2 * R[4] + x3 * R[7];
            y3 = x1 * R[2] + x2 * R[5] + x3 * R[8];
        }

        unsafe static void matrixMult(int row1, int col1, double* m1,
int row2, int col2, double* m2,
    double* res)
        {
            Debug.Assert(col1 == row2);
            // resulting matrix is [row1 x col2]
            for (int i = 0; i < row1 * col2; i++) res[i] = 0;

            for (int r = 0; r < row1; r++)
                for (int c = 0; c < col2; c++)
                    for (int i = 0; i < col1; i++)
                        res[r * col2 + c] += m1[r * col1 + i] * m2[i * col2 + c];
        }

        // res = m1^T x m2
        unsafe static void matrixTransposeMult(int row1, int col1, double* m1,
int row2, int col2, double* m2,
    double* res)
        {
            Debug.Assert(row1 == row2);
            // resulting matrix is [col1 x col2]
            for (int i = 0; i < col1 * col2; i++) res[i] = 0;

            for (int r = 0; r < col1; r++)
                for (int c = 0; c < col2; c++)
                    for (int i = 0; i < row1; i++)
                        res[r * col2 + c] += m1[i * row1 + r] * m2[i * col2 + c];
        }

        unsafe static void matrixMultByTranspose(int row1, int col1, double* m1,
int row2, int col2, double* m2,
    double* res)
        {
            Debug.Assert(col1 == col2);
            // resulting matrix is [row1 x row2]
            for (int i = 0; i < row1 * row2; i++) res[i] = 0;

            for (int r = 0; r < row1; r++)
                for (int c = 0; c < row2; c++)
                    for (int i = 0; i < col1; i++)
                        res[r * col2 + c] += m1[r * col1 + i] * m2[c * row2 + i];
        }


        unsafe static void matrixScalarMult(int row, int col, double* m, double val)
        {
            for (int i = 0; i < row * col; i++) m[i] *= val;
        }

        #endregion

        #region corotational model


        unsafe static void F_and_Df_Corotational2(double *x0, double* xc,
    double* f, double* Df, double[] sigma, out double V)
        {
            // Colorational formulation:
            // f = RK(Rt xc - x0)
            // Df = R K Rt

            // calculate K
            double x1, x2, x3, x4, y1, y2, y3, y4, z1, z2, z3, z4;
            double x12, x13, x14, x23, x24, x34;
            double y12, y13, y14, y23, y24, y34;
            double z12, z13, z14, z23, z24, z34;
            double a1, a2, a3, a4, b1, b2, b3, b4, c1, c2, c3, c4;
            double Jdet;
            x1 = x0[0]; y1 = x0[1]; z1 = x0[2];
            x2 = x0[3]; y2 = x0[4]; z2 = x0[5];
            x3 = x0[6]; y3 = x0[7]; z3 = x0[8];
            x4 = x0[9]; y4 = x0[10]; z4 = x0[11];

            x12 = x0[0] - x0[3]; x13 = x0[0] - x0[6]; x14 = x0[0] - x0[9]; x23 = x0[3] - x0[6]; x24 = x0[3] - x0[9]; x34 = x0[6] - x0[9];
            y12 = x0[1] - x0[4]; y13 = x0[1] - x0[7]; y14 = x0[1] - x0[10]; y23 = x0[4] - x0[7]; y24 = x0[4] - x0[10]; y34 = x0[7] - x0[10];
            z12 = x0[2] - x0[5]; z13 = x0[2] - x0[8]; z14 = x0[2] - x0[11]; z23 = x0[5] - x0[8]; z24 = x0[5] - x0[11]; z34 = x0[8] - x0[11];

            Jdet = -(x12 * (y23 * z34 - y34 * z23) + x23 * (y34 * z12 - y12 * z34) + x34 * (y12 * z23 - y23 * z12));
            V = Jdet / 6.0;

            a1 = y24 * z23 - y23 * z24; b1 = x23 * z24 - x24 * z23; c1 = x24 * y23 - x23 * y24;
            a2 = y13 * z34 - y34 * z13; b2 = x34 * z13 - x13 * z34; c2 = x13 * y34 - x34 * y13;
            a3 = y24 * z14 - y14 * z24; b3 = x14 * z24 - x24 * z14; c3 = x24 * y14 - x14 * y24;
            a4 = -y13 * z12 + y12 * z13; b4 = -x12 * z13 + x13 * z12; c4 = -x13 * y12 + x12 * y13;

            a1 /= Jdet; a2 /= Jdet; a3 /= Jdet; a4 /= Jdet;
            b1 /= Jdet; b2 /= Jdet; b3 /= Jdet; b4 /= Jdet;
            c1 /= Jdet; c2 /= Jdet; c3 /= Jdet; c4 /= Jdet;

            double z = 0;
            // [6,12]
            double* B = stackalloc double[] {
         a1, z, z, a2, z, z, a3, z, z, a4, z, z ,
         z, b1, z, z, b2, z, z, b3, z, z, b4, z ,
         z, z, c1, z, z, c2, z, z, c3, z, z, c4 ,
         b1, a1, z, b2, a2, z, b3, a3, z, b4, a4, z ,
         z, c1, b1, z, c2, b2, z, c3, b3, z, c4, b4 ,
         c1, z, a1, c2, z, a2, c3, z, a3, c4, z, a4 };

            // [12,6] result of multiplication (Bt x E)
            double* BtE = stackalloc double[72];

                        for (int r = 0; r < 12; r++)
                for (int c = 0; c < 6; c++)
                    for (int i = 0; i < 6; i++) BtE[r*6+c] += B[i*12+r] * E[i,c];
            
            /*
            fixed (double* ElT = E)
            {
                matrixTransposeMult(6, 12, B, 6, 6, ElT, BtE);
            }
            */
            // [12,12]; K = Bt x E x B x V
            double* K = stackalloc double[144];
/*            for (int r = 0; r < 12; r++)
                for (int c = 0; c < 12; c++)
                    for (int i = 0; i < 6; i++) K[r*12+c] += BtE[r*6+i] * B[i*12+c] * V;
                    */
            
            matrixMult(12, 6, BtE, 6, 12, B, K);
            matrixScalarMult(12, 12, K, V);

            // rotation matrices
            double* R0 = stackalloc double[9];
            double* R1 = stackalloc double[9];
            fastRotationMatrix(x0, R0);
            fastRotationMatrix(xc, R1);
            
            double* R = stackalloc double[9]; // R = R1 x R0^T
            matrixMultByTranspose(3, 3, R1, 3, 3, R0, R);
            
            // both are 12x12
            double* RK = stackalloc double[144];
            double* RKRt = stackalloc double[144];

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    // RK = R * K
                    for (int k = 0; k < 3; k++)
                        for (int l = 0; l < 3; l++)
                        {
                            for (int m = 0; m < 3; m++)
                                RK[(3 * i + k)*12 + (3 * j + l)] += R[k * 3 + m] * K[(3 * i + m)*12 + (3 * j + l)];
                        }

                    // Df = RKRT = RK * R^T
                    for (int k = 0; k < 3; k++)
                        for (int l = 0; l < 3; l++)
                            for (int m = 0; m < 3; m++)
                                Df[(3 * i + k)*12 +(3 * j + l)] += RK[(3 * i + k)*12 +(3 * j + m)] * R[l*3+ m];
                }


            // xr = Rt xc
            double* xr = stackalloc double[12];
            transposeAndMultiplyByVector3(R, xc[0], xc[1], xc[2], out xr[0], out xr[1], out xr[2]);
            transposeAndMultiplyByVector3(R, xc[3], xc[4], xc[5], out xr[3], out xr[4], out xr[5]);
            transposeAndMultiplyByVector3(R, xc[6], xc[7], xc[8], out xr[6], out xr[7], out xr[8]);
            transposeAndMultiplyByVector3(R, xc[9], xc[10], xc[11], out xr[9], out xr[10], out xr[11]);

            for (int i = 0; i < 12; i++) xr[i] -= x0[i];

            // f = RK(Rt pm - mx)
            for (int i = 0; i < 12; i++)
                for (int j = 0; j < 12; j++)
                    f[i] += RK[i*12 + j] * xr[j];

            // calculation of strain (rotation excluded) e = B.xr
            // B[6][12]
            double* e = stackalloc double[6];
            for (int j = 0; j < 6; j++)
                for (int i = 0; i < 12; i++)
                    e[j] += B[j*12 + i] * xr[i];

            // calculation of stress (rotation excluded) s = E.e
            // E[6][6]
            Array.Clear(sigma, 0, sigma.Length);
            for (int j = 0; j < 6; j++)
                for (int i = 0; i < 6; i++)
                    sigma[j] += E[i, j] * e[i];
        }
        #endregion

        #region element forces
        [DllImport("PardisoLoader2.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void Eigenvalues(double xx, double yy, double zz, double xy, double yz, double zx, double[] eigenvalues);

        // one element
        unsafe static void ElementElasticity(Element elem)
        {
            double* vn = stackalloc double[12];
            double* an = stackalloc double[12];
            double* xc = stackalloc double[12];
            double* x0 = stackalloc double[12];
            for (int i = 0; i < 4; i++)
            {
                Node nd = elem.vrts[i];
                xc[i * 3 + 0] = nd.tx;
                xc[i * 3 + 1] = nd.ty;
                xc[i * 3 + 2] = nd.tz;
                x0[i * 3 + 0] = nd.x0;
                x0[i * 3 + 1] = nd.y0;
                x0[i * 3 + 2] = nd.z0;
                vn[i * 3 + 0] = nd.vnx;
                vn[i * 3 + 1] = nd.vny;
                vn[i * 3 + 2] = nd.vnz;
                an[i * 3 + 0] = nd.anx;
                an[i * 3 + 1] = nd.any;
                an[i * 3 + 2] = nd.anz;
            }

            // out params
            double* f = stackalloc double[12];
            double* Df = stackalloc double[144];
            F_and_Df_Corotational2(x0, xc, f, Df, elem.stress, out double V);

            ElementExtension ex = elem.extension;
            ex.Clear();
            double[] rhs = ex.rhs;
            double[,] lhs = ex.lhs;

            double gravityForcePerNode = gravity * rho * V / 4;
            rhs[2] = gravityForcePerNode;
            rhs[5] = gravityForcePerNode;
            rhs[8] = gravityForcePerNode;
            rhs[11] = gravityForcePerNode;

            // assemble the effective stiffness matrix Keff = M/(h^2 beta) + RKRt + D * gamma /(h beta) 
            // where D is the damping matrix D = a M + b K
            double rhoV = rho * V;
            double massCoeff = rhoV * (1.0 / (h * h) + dampingMass * NewmarkGamma / h) / NewmarkBeta;
            double stiffCoeff = 1.0 + dampingStiffness * NewmarkGamma / (h * NewmarkBeta);

            // add damping component to rhs
            // D = M[i][j] * V * dampingMass + RKRt[i][j] * dampingStiffness
            for (int i = 0; i < 12; i++)
            {
                rhs[i] -= f[i];
                for (int j = 0; j < 12; j++)
                {
                    rhs[i] -= (M[i,j] * rhoV * dampingMass + Df[i*12 + j] * dampingStiffness) * vn[j] + (M[i,j] * rhoV * an[j]);
                    lhs[i,j] = Df[i*12+j] * stiffCoeff + M[i,j] * massCoeff;
                }
            }
        }

        #endregion
 
        public unsafe static void AssembleElems(LinearSystem ls, FrameInfo cf, MeshCollection mc, ModelPrms prms)
        {
            cf.PrincipalStress1Max = cf.PrincipalStress2Max = cf.PrincipalStress3Max = double.MinValue;
            cf.PrincipalStress1Min = cf.PrincipalStress2Min = cf.PrincipalStress3Min = double.MaxValue;

            // update static variables
            h = cf.TimeStep;
            Y = prms.Y;
            nu = prms.nu;
            rho = prms.rho;
            InitializeElasticityTensor();
            NewmarkBeta = prms.NewmarkBeta;
            NewmarkGamma = prms.NewmarkGamma;
            dampingMass = prms.dampingMass;
            dampingStiffness = prms.dampingStiffness;
            gravity = prms.gravity;

            int nElems = mc.elasticElements.Length;
            cf.nElems = nElems;

            foreach (Element elem in mc.elasticElements) if (elem.extension == null) elem.extension = new ElementExtension();

            // compute results per element in parallel
            Parallel.ForEach(mc.elasticElements, elem => { ElementElasticity(elem); });

            // distribute results into linear system
            foreach(Element elem in mc.elasticElements)
            {
                ElementExtension ex = (ElementExtension)elem.extension;
                double[,] lhs = ex.lhs;
                double[] rhs = ex.rhs;
                for(int r=0;r<4;r++)
                {
                    int ni = elem.vrts[r].altId;
                    ls.AddToRHS(ni, rhs[r * 3 + 0], rhs[r * 3 + 1], rhs[r * 3 + 2]);

                    for (int c=0;c<4;c++)
                    {
                        int nj = elem.vrts[c].altId;
                        ls.AddToLHS_Symmetric(ni, nj,
                        lhs[r * 3 + 0, c * 3 + 0], lhs[r * 3 + 0, c * 3 + 1], lhs[r * 3 + 0, c * 3 + 2],
                        lhs[r * 3 + 1, c * 3 + 0], lhs[r * 3 + 1, c * 3 + 1], lhs[r * 3 + 1, c * 3 + 2],
                        lhs[r * 3 + 2, c * 3 + 0], lhs[r * 3 + 2, c * 3 + 1], lhs[r * 3 + 2, c * 3 + 2]);
                    }
                }

                // compute principal stresses
                Element e = elem;
                Eigenvalues(e.stress[0], e.stress[1], e.stress[2], e.stress[3], e.stress[4], e.stress[5], e.principal_stresses);

                if (cf.PrincipalStress1Max < e.principal_stresses[0]) cf.PrincipalStress1Max = e.principal_stresses[0];
                if (cf.PrincipalStress2Max < e.principal_stresses[1]) cf.PrincipalStress2Max = e.principal_stresses[1];
                if (cf.PrincipalStress3Max < e.principal_stresses[2]) cf.PrincipalStress3Max = e.principal_stresses[2];

                if (cf.PrincipalStress1Min > e.principal_stresses[0]) cf.PrincipalStress1Min = e.principal_stresses[0];
                if (cf.PrincipalStress2Min > e.principal_stresses[1]) cf.PrincipalStress2Min = e.principal_stresses[1];
                if (cf.PrincipalStress3Min > e.principal_stresses[2]) cf.PrincipalStress3Min = e.principal_stresses[2];
            }
        }

        public static void TransferUpdatedState(MeshCollection mc)
        {
            // compute principal stresses (not implemented)



            // update forces per node

            foreach (Element elem in mc.elasticElements)
            {
                ElementExtension ex = elem.extension;
                Node[] vrts = elem.vrts;
                for (int r = 0; r < 4; r++)
                {
                    Node nd = vrts[r];
                    nd.fx += ex.rhs[r * 3 + 0];
                    nd.fy += ex.rhs[r * 3 + 1];
                    nd.fz += ex.rhs[r * 3 + 2];
                }
            }
        }
    }
}
