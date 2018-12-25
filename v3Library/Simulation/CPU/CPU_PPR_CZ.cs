using System.Threading.Tasks;
using System.Diagnostics;
using static System.Math;

namespace icFlow
{
    public static class CPU_PPR_CZ
    {
        public class CZParams
        {
            public double G_fn, G_ft;     // phi_n, phi_t (fracture energy)
            public double f_tn, f_tt;     // sigma_max, tau_max
            public double alpha, beta;
            public double rn, rt;         // lambda_n, lambda_t

            public double p_m, p_n;
            public double deln, delt;
            public double pMtn, pMnt;     // < phi_t - phi_n >, < phi_n - phi_t > Macaulay brackets
            public double gam_n, gam_t;
        }

        public class CZResult
        {
            public bool contact, failed, damaged;
            public double avgDn, avgDt, avgTn, avgTt;
            public double pmax_, tmax_;
            public double[] pmax = new double[3], tmax = new double[3], rhs = new double[18];
            public double[,] Keff = new double[18, 18];

            public CZResult(double[] pmax, double[] tmax)
            {
                for (int i = 0; i < 3; i++)
                {
                    this.pmax[i] = pmax[i];
                    this.tmax[i] = tmax[i];
                }
            }
        }

        static double[,,] B;
        static double[,] sf;

        static CPU_PPR_CZ()
        {
            // initialize sf and B arrays
            int nGP = 3;
            sf = new double[3, nGP];

            double GP_coord_1 = 1.0 / 6.0;
            double GP_coord_2 = 2.0 / 3.0;
            sf[0, 0] = 1.0 - GP_coord_1 - GP_coord_2;
            sf[1, 0] = GP_coord_1;
            sf[2, 0] = GP_coord_2;

            GP_coord_1 = 2.0 / 3.0;
            GP_coord_2 = 1.0 / 6.0;
            sf[0, 1] = 1.0 - GP_coord_1 - GP_coord_2;
            sf[1, 1] = GP_coord_1;
            sf[2, 1] = GP_coord_2;

            GP_coord_1 = 1.0 / 6.0;
            GP_coord_2 = 1.0 / 6.0;
            sf[0, 2] = 1.0 - GP_coord_1 - GP_coord_2;
            sf[1, 2] = GP_coord_1;
            sf[2, 2] = GP_coord_2;

            double[][,] B3 = new double[3][,];
            for (int i = 0; i < 3; i++) B3[i] = k_Bmatrix(i);

            B = new double[3, 3, 18];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    for (int k = 0; k < 18; k++)
                        B[i, j, k] = B3[i][j, k];

            double[,] k_Bmatrix(int i)
            {
                double[,] B2 = new double[3, 18];
                B2[0, 0] = sf[0, i];
                B2[1, 1] = sf[0, i];
                B2[2, 2] = sf[0, i];
                B2[0, 9] = -sf[0, i];
                B2[1, 10] = -sf[0, i];
                B2[2, 11] = -sf[0, i];

                B2[0, 3] = sf[1, i];
                B2[1, 4] = sf[1, i];
                B2[2, 5] = sf[1, i];
                B2[0, 12] = -sf[1, i];
                B2[1, 13] = -sf[1, i];
                B2[2, 14] = -sf[1, i];

                B2[0, 6] = sf[2, i];
                B2[1, 7] = sf[2, i];
                B2[2, 8] = sf[2, i];

                B2[0, 15] = -sf[2, i];
                B2[1, 16] = -sf[2, i];
                B2[2, 17] = -sf[2, i];
                return B2;
            }
        }

        #region PPR math
        static double Tn_(double Dn, double Dt, CZParams prms)
        {
            double Dndn = Dn / prms.deln;
            double Dtdt = Dt / prms.delt;
            double expr2 = prms.p_m / prms.alpha + Dndn;
            double pr1 = prms.gam_n / prms.deln;
            double pr2 = (prms.p_m * Pow(1 - Dndn, prms.alpha) * Pow(expr2, prms.p_m - 1)) -
                (prms.alpha * Pow(1 - Dndn, prms.alpha - 1) * Pow(expr2, prms.p_m));
            double pr3 = prms.gam_t * Pow(1 - Dtdt, prms.beta) * Pow(prms.p_n / prms.beta + Dtdt, prms.p_n) + prms.pMtn;
            return pr1 * pr2 * pr3;
        }

        static double Tt_(double Dn, double Dt, CZParams prms)
        {
            double Dndn = Dn / prms.deln;
            double Dtdt = Dt / prms.delt;
            double expr1 = 1 - Dtdt;
            double expr2 = prms.p_n / prms.beta + Dtdt;
            double pr1 = prms.gam_t / prms.delt;
            double pr2 = prms.p_n * Pow(expr1, prms.beta) * Pow(expr2, prms.p_n - 1) - prms.beta * Pow(expr1, prms.beta - 1) * Pow(expr2, prms.p_n);
            double pr3 = prms.gam_n * Pow(1 - Dndn, prms.alpha) * Pow(prms.p_m / prms.alpha + Dndn, prms.p_m) + prms.pMnt;
            return pr1 * pr2 * pr3;
        }

        static double Dnn_(double opn, double opt, CZParams prms)
        {
            double coeff = prms.gam_n / (prms.deln * prms.deln);
            double expr1 = (prms.p_m * prms.p_m - prms.p_m) * Pow(1.0 - (opn / prms.deln), prms.alpha) * Pow((prms.p_m / prms.alpha) + (opn / prms.deln), prms.p_m - 2.0);
            double expr2 = (prms.alpha * prms.alpha - prms.alpha) * Pow(1.0 - (opn / prms.deln), prms.alpha - 2.0) * Pow((prms.p_m / prms.alpha) + (opn / prms.deln), prms.p_m);
            double expr3 = 2.0 * prms.alpha * prms.p_m * Pow(1.0 - (opn / prms.deln), prms.alpha - 1.0) * Pow((prms.p_m / prms.alpha) + (opn / prms.deln), prms.p_m - 1.0);
            double expr4 = prms.gam_t * Pow((1.0 - (opt / prms.delt)), prms.beta) * Pow(((prms.p_n / prms.beta) + (opt / prms.delt)), prms.p_n) + prms.pMtn;
            double result = coeff * (expr1 + expr2 - expr3) * expr4;
            return result;
        }

        static double Dtt_(double opn, double opt, CZParams prms)
        {
            double coeff = prms.gam_t / (prms.delt * prms.delt);
            double expr1 = (prms.p_n * prms.p_n - prms.p_n) * Pow(1.0 - (opt / prms.delt), prms.beta) * Pow((prms.p_n / prms.beta) + (opt / prms.delt), prms.p_n - 2.0);
            double expr2 = (prms.beta * prms.beta - prms.beta) * Pow(1.0 - (opt / prms.delt), prms.beta - 2.0) * Pow((prms.p_n / prms.beta) + (opt / prms.delt), prms.p_n);
            double expr3 = 2.0 * prms.beta * prms.p_n * Pow(1.0 - (opt / prms.delt), prms.beta - 1.0) * Pow((prms.p_n / prms.beta) + (opt / prms.delt), prms.p_n - 1.0);
            double expr4 = prms.gam_n * Pow(1.0 - (opn / prms.deln), prms.alpha) * Pow((prms.p_m / prms.alpha) + (opn / prms.deln), prms.p_m) + prms.pMnt;
            double result = coeff * (expr1 + expr2 - expr3) * expr4;
            return result;
        }

        static double Dnt_(double opn, double opt, CZParams prms)
        {
            double coeff = prms.gam_n * prms.gam_t / (prms.deln * prms.delt);
            double expr1 = prms.p_m * Pow(1.0 - (opn / prms.deln), prms.alpha) * Pow((prms.p_m / prms.alpha) + (opn / prms.deln), prms.p_m - 1.0);
            double expr2 = prms.alpha * Pow(1.0 - (opn / prms.deln), prms.alpha - 1.0) * Pow((prms.p_m / prms.alpha) + (opn / prms.deln), prms.p_m);
            double expr3 = prms.p_n * Pow(1.0 - (opt / prms.delt), prms.beta) * Pow((prms.p_n / prms.beta) + (opt / prms.delt), prms.p_n - 1.0);
            double expr4 = prms.beta * Pow(1.0 - (opt / prms.delt), prms.beta - 1.0) * Pow((prms.p_n / prms.beta) + (opt / prms.delt), prms.p_n);
            double result = coeff * (expr1 - expr2) * (expr3 - expr4);
            return result;
        }

        #endregion

        #region matrix math

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

        static void CZRotationMatrix(
            double x0, double y0, double z0,
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            out double r00, out double r01, out double r02,
            out double r10, out double r11, out double r12,
            out double r20, out double r21, out double r22,
            out double a_Jacob)
        {

            double p1x, p1y, p1z, p2x, p2y, p2z;
            p1x = x1 - x0;
            p1y = y1 - y0;
            p1z = z1 - z0;

            p2x = x0 - x2;
            p2y = y0 - y2;
            p2z = z0 - z2;

            // normalized p1 goes into 1st row of R
            double p1mag = Sqrt(p1x * p1x + p1y * p1y + p1z * p1z);
            r00 = p1x / p1mag;
            r01 = p1y / p1mag;
            r02 = p1z / p1mag;

            // normalized n = p1 x p2 goes into the 3rd row
            double nx, ny, nz;
            nx = -p1z * p2y + p1y * p2z;
            ny = p1z * p2x - p1x * p2z;
            nz = -p1y * p2x + p1x * p2y;
            double nmag = Sqrt(nx * nx + ny * ny + nz * nz);
            a_Jacob = nmag / 2; // area of the cohesive element
            nx /= nmag;
            ny /= nmag;
            nz /= nmag;
            r20 = nx;
            r21 = ny;
            r22 = nz;

            // normalize p1
            p1x /= p1mag;
            p1y /= p1mag;
            p1z /= p1mag;

            // second row is: r2 = n x p1
            double r2x, r2y, r2z;
            r2x = -nz * p1y + ny * p1z;
            r2y = nz * p1x - nx * p1z;
            r2z = -ny * p1x + nx * p1y;

            nmag = Sqrt(r2x * r2x + r2y * r2y + r2z * r2z);
            r10 = r2x / nmag;
            r11 = r2y / nmag;
            r12 = r2z / nmag;
        }
        #endregion

        #region cohesive law

        public static void cohesive_law(ref bool cz_contact, ref bool cz_failed,
            ref double pmax, ref double tmax, double opn, double opt,
            out double Tn, out double Tt, out double Dnn, out double Dtt, out double Dnt, out double Dtn,
            CZParams prms)
        {
            Tn = Tt = Dnn = Dtt = Dnt = Dtn = 0;
            if (opn > prms.deln || opt > prms.delt)
            {
                cz_failed = true; return;
            }
            cz_contact = (opn < 0);
            const double epsilon = -1e-9;
            const double epsilon2 = 0.05; // if traction is <5% of max, CZ fails
            double threshold_tangential = prms.f_tt * epsilon2;
            double threshold_normal = prms.f_tn * epsilon2;

            if (cz_contact)
            {
                Dnt = 0;
                if (pmax != 0)
                {
                    double peakTn = Tn_(pmax, tmax, prms);
                    Tn = peakTn * opn / pmax;
                    Dnn = peakTn / pmax;
                }
                else
                {
                    Dnn = Dnn_(0, tmax, prms);
                    Tn = Dnn * opn;
                }

                Tt = Tt_(0, opt, prms);
                if (Tt >= epsilon && !(opt > prms.delt * prms.rt / 5 && Tt < threshold_tangential))
                {
                    if (opt >= tmax)
                    {
                        // tangential softening
                        tmax = opt;
                        Dtt = Dtt_(0, opt, prms);
                    }
                    else
                    {
                        // unload/reload
                        double peakTt = Tt_(0, tmax, prms);
                        Tt = peakTt * opt / tmax;
                        Dtt = peakTt / tmax;
                    }

                }
                else
                {
                    // cz failed in tangential direction while in contact
                    Tt = Dtt = Dnt = 0;
                    Tn = Dnn = 0;
                    cz_failed = true;
                }
            }
            else
            {
                // not in contact
                Tt = Tt_(opn, opt, prms);
                Tn = Tn_(opn, opt, prms);
                if (Tt >= epsilon && Tn >= epsilon &&
                    !(opt > prms.delt * prms.rt / 5 && Tt < threshold_tangential) &&
                    !(opn > prms.deln * prms.rn / 5 && Tn < threshold_normal))
                {
                    // tangential component
                    bool tsoft = (opt >= tmax);
                    bool nsoft = (opn >= pmax);
                    if (tsoft && nsoft)
                    {
                        // tangential and normal softening
                        tmax = opt;
                        pmax = opn;
                        Dnn = Dnn_(opn, opt, prms);
                        Dnt = Dnt_(opn, opt, prms);
                        Dtt = Dtt_(opn, opt, prms);
                    }
                    else if (tsoft && !nsoft)
                    {
                        Dnt = 0;
                        if (pmax != 0)
                        {
                            double peakTn = Tn_(pmax, tmax, prms);
                            Tn = peakTn * opn / pmax;
                            Dnn = peakTn / pmax;
                        }
                        else
                        {
                            Tn = 0; Dnn = Dnn_(0, tmax, prms);
                        }

                        // normal unload/reload
                        tmax = opt;
                        Tt = Tt_(pmax, opt, prms);
                        Dtt = Dtt_(pmax, opt, prms);
                    }
                    else if (!tsoft && nsoft)
                    {
                        Dnt = 0;
                        if (tmax != 0)
                        {
                            double peakTt = Tt_(pmax, tmax, prms);
                            Tt = peakTt * opt / tmax;
                            Dtt = peakTt / tmax;
                        }
                        else
                        {
                            Tt = 0; Dtt = Dtt_(pmax, 0, prms);
                        }

                        pmax = opn;
                        Tn = Tn_(pmax, tmax, prms);
                        Dnn = Dnn_(pmax, tmax, prms);

                    }
                    else
                    {
                        Dnt = 0;
                        // reloading in both tangential and normal
                        double peakTn = Tn_(pmax, tmax, prms);
                        if (pmax != 0)
                        {
                            Tn = peakTn * opn / pmax;
                            Dnn = peakTn / pmax;
                        }
                        else
                        {
                            Tn = 0; Dnn = Dnn_(0, tmax, prms);
                        }

                        if (tmax != 0)
                        {
                            double peakTt = Tt_(pmax, tmax, prms);
                            Tt = peakTt * opt / tmax;
                            Dtt = peakTt / tmax;
                        }
                        else
                        {
                            Tt = 0; Dtt = Dtt_(pmax, 0, prms);
                        }
                    }

                }
                else
                {
                    cz_failed = true;
                    Tn = Tt = Dnn = Dtt = Dnt = 0;
                }
            }
            Dtn = Dnt;

        }

        #endregion

        // not to be invoked on failed CZ
        static CZResult CZForce(CZ cz, double h, CZParams prms)
        {
            double[] x0 = new double[18];
            double[] un = new double[18];
            double[] xc = new double[18]; // current node positions

            for (int i=0;i<6;i++)
            {
                Node nd = cz.vrts[i];
                x0[i * 3 + 0] = nd.x0;
                x0[i * 3 + 1] = nd.y0;
                x0[i * 3 + 2] = nd.z0;

                un[i * 3 + 0] = nd.unx;
                un[i * 3 + 1] = nd.uny;
                un[i * 3 + 2] = nd.unz;

                xc[i * 3 + 0] = nd.tx;
                xc[i * 3 + 1] = nd.ty;
                xc[i * 3 + 2] = nd.tz;
            }

            double[] mpc = new double[9]; // midplane
            for (int i = 0; i < 9; i++) mpc[i] = (xc[i] + xc[i + 9]) * 0.5;

            // rotation matrix of midplane
            double[,] R = new double[3, 3];
            double a_Jacob;

            CZRotationMatrix(
                mpc[0], mpc[1], mpc[2],
                mpc[3], mpc[4], mpc[5],
                mpc[6], mpc[7], mpc[8],
                out R[0, 0], out R[0, 1], out R[0, 2],
                out R[1, 0], out R[1, 1], out R[1, 2],
                out R[2, 0], out R[2, 1], out R[2, 2],
                out a_Jacob);

            // rotate xc to local system 
            double[] xr = new double[18];
            for (int i = 0; i < 6; i++)
                multAX(
                    R[0, 0], R[0, 1], R[0, 2],
                    R[1, 0], R[1, 1], R[1, 2],
                    R[2, 0], R[2, 1], R[2, 2],
                    xc[i * 3 + 0], xc[i * 3 + 1], xc[i * 3 + 2],
                    out xr[i * 3 + 0], out xr[i * 3 + 1], out xr[i * 3 + 2]);


            bool[] cz_contact_gp = new bool[3];
            bool[] cz_failed_gp = new bool[3];

            double avgDn, avgDt, avgTn, avgTt; // preserve average traction-separations for analysis
            avgDn = avgDt = avgTn = avgTt = 0;

            CZResult r = new CZResult(cz.pmax, cz.tmax);

            // loop over 3 Gauss points
            for (int gpt = 0; gpt < 3; gpt++)
            {
                // shear and normal local opening displacements
                double dt1, dt2, dn;
                dt1 = dt2 = dn = 0;
                for (int i = 0; i < 3; i++)
                {
                    dt1 += (xr[i * 3 + 0] - xr[i * 3 + 9]) * sf[i, gpt];
                    dt2 += (xr[i * 3 + 1] - xr[i * 3 + 10]) * sf[i, gpt];
                    dn += (xr[i * 3 + 2] - xr[i * 3 + 11]) * sf[i, gpt];
                }
                double opn = dn;
                double opt = Sqrt(dt1 * dt1 + dt2 * dt2);

                double Tn, Tt, Dnn, Dtt, Dnt, Dtn;

                cohesive_law(
                ref cz_contact_gp[gpt],
                ref cz_failed_gp[gpt],
                ref r.pmax[gpt],
                ref r.tmax[gpt],
                opn, opt,
                out Tn, out Tt, out Dnn, out Dtt, out Dnt, out Dtn, prms);

                // preserve average traction-separations for analysis
                avgDn += opn / 3;
                avgDt += opt / 3;
                avgTn += Tn / 3;
                avgTt += Tt / 3;

                double[] T = new double[3];
                double[,] T_d = new double[3, 3];

                if (opt < 1e-20)
                {
                    T[2] = Tn;
                    T_d[0, 0] = Dtt;
                    T_d[1, 1] = Dtt;
                    T_d[2, 2] = Dnn;

                    T_d[1, 0] = T_d[0, 1] = 0;

                    T_d[2, 0] = Dtn;
                    T_d[0, 2] = Dnt;
                    T_d[2, 1] = Dtn;
                    T_d[1, 2] = Dnt;
                }
                else
                {
                    T[0] = Tt * dt1 / opt;
                    T[1] = Tt * dt2 / opt;
                    T[2] = Tn;

                    double opt_sq = opt * opt;
                    double opt_cu = opt_sq * opt;
                    double delu00 = dt1 * dt1;
                    double delu10 = dt2 * dt1;
                    double delu11 = dt2 * dt2;

                    T_d[0, 0] = Dtt * delu00 / opt_sq + Tt * delu11 / opt_cu;
                    T_d[1, 1] = Dtt * delu11 / opt_sq + Tt * delu00 / opt_cu;
                    T_d[2, 2] = Dnn;

                    T_d[1, 0] = T_d[0, 1] = Dtt * delu10 / opt_sq - Tt * delu10 / opt_cu;

                    T_d[2, 0] = Dtn * dt1 / opt;
                    T_d[0, 2] = Dnt * dt1 / opt;
                    T_d[2, 1] = Dtn * dt2 / opt;
                    T_d[1, 2] = Dnt * dt2 / opt;
                }

                // RHS
                // BtT = Bt x T x (-GP_W)
                const double GP_W = 1.0 / 3.0; // Gauss point weight

                double[] BtT = new double[18];
                for (int i = 0; i < 18; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        BtT[i] += B[gpt, j, i] * T[j];
                    }
                    BtT[i] *= -(GP_W * a_Jacob);
                }

                // rotate BtT
                double[] rhs_gp = new double[18];
                for (int i = 0; i < 6; i++)
                {
                    multAX(R[0, 0], R[1, 0], R[2, 0],
                        R[0, 1], R[1, 1], R[2, 1],
                        R[0, 2], R[1, 2], R[2, 2],
                        BtT[i * 3 + 0], BtT[i * 3 + 1], BtT[i * 3 + 2],
                        out rhs_gp[i * 3 + 0], out rhs_gp[i * 3 + 1], out rhs_gp[i * 3 + 2]);
                }

                // add to rhs
                for (int i = 0; i < 18; i++) r.rhs[i] += rhs_gp[i];

                // STIFFNESS MATRIX
                // compute Bt x T_d x GP_W
                double[,] BtTd = new double[18, 3];
                for (int row = 0; row < 18; row++)
                    for (int col = 0; col < 3; col++)
                    {
                        for (int k = 0; k < 3; k++) BtTd[row, col] += B[gpt, k, row] * T_d[k, col];
                        BtTd[row, col] *= (GP_W * a_Jacob);
                    }

                // BtTdB = BtTd x B
                double[,] BtTdB = new double[18, 18];
                for (int row = 0; row < 18; row++)
                    for (int col = 0; col < 18; col++)
                        for (int k = 0; k < 3; k++)
                            BtTdB[row, col] += BtTd[row, k] * B[gpt, k, col];

                double[,] TrMtBtTdB = new double[18, 18];

                // Keff
                for (int i = 0; i < 6; i++)
                    for (int j = 0; j < 6; j++)
                    {
                        // TrMtBtTdB = TrMt x BtTdB
                        for (int k = 0; k < 3; k++)
                            for (int l = 0; l < 3; l++)
                            {
                                for (int m = 0; m < 3; m++)
                                    TrMtBtTdB[3 * i + k, 3 * j + l] += R[m, k] * BtTdB[3 * i + m, 3 * j + l];
                            }

                        // Keff = TrMt x BtTdB x TrM
                        for (int k = 0; k < 3; k++)
                            for (int l = 0; l < 3; l++)
                            {
                                for (int m = 0; m < 3; m++)
                                    r.Keff[3 * i + k, 3 * j + l] += TrMtBtTdB[3 * i + k, 3 * j + m] * R[m, l];
                            }
                    }
            }


            // the following approach to pmax, tmax is somewhat experimental
            r.pmax_ = Max(Max(r.pmax[0], r.pmax[1]), r.pmax[2]);
            r.tmax_ = Max(Max(r.tmax[0], r.tmax[1]), r.tmax[2]);

            // detect damaged state
            r.damaged = false;
            for (int i = 0; i < 3; i++)
            {
                if (r.pmax[i] >= prms.deln * prms.rn || r.tmax[i] >= prms.delt * prms.rt)
                {
                    r.damaged = true;
                    break;
                }
            }

            r.failed = cz_failed_gp[0] || cz_failed_gp[1] || cz_failed_gp[2];
            r.contact = cz_contact_gp[0] || cz_contact_gp[1] || cz_contact_gp[2];
            if (r.failed) r.damaged = false;

            return r;
        }

        public static CZResult[] AssembleCZs(LinearSystem ls, ref FrameInfo cf, MeshCollection mc, ModelPrms prms)
        {
            int nCZs = cf.nCZ = mc.nonFailedCZs.Length;
            CZResult[] czResults = new CZResult[nCZs];

            CZParams czp = new CZParams();
            czp.G_fn = prms.G_fn;
            czp.G_ft = prms.G_ft;
            czp.f_tn = prms.f_tn;
            czp.f_tt = prms.f_tt;
            czp.alpha = prms.alpha;
            czp.beta = prms.beta;
            czp.rn = prms.rn;
            czp.rt = prms.rt;
            czp.p_m = prms.p_m;
            czp.p_n = prms.p_n;
            czp.deln = prms.deln;
            czp.delt = prms.delt;
            czp.pMtn = prms.pMtn;
            czp.pMnt = prms.pMnt;
            czp.gam_n = prms.gam_n;
            czp.gam_t = prms.gam_t;

            double h = cf.TimeStep;
            Parallel.For(0, nCZs, i => {
                CZ cz = mc.nonFailedCZs[i];
                Debug.Assert(!cz.failed);
                czResults[i] = CZForce(cz, h, czp);
                });

            // distribute results into linear system
            for (int idx = 0; idx < nCZs; idx++)
            {
                CZResult czr = czResults[idx];
                double[,] lhs = czr.Keff;
                Element elem = mc.elasticElements[idx];
                for (int r = 0; r < 6; r++)
                {
                    int ni = elem.vrts[r].altId;
                    ls.AddToRHS(ni, czr.rhs[r * 3 + 0], czr.rhs[r * 3 + 1], czr.rhs[r * 3 + 2]);
                    for (int c = 0; c < 6; c++)
                    {
                        int nj = elem.vrts[c].altId;
                        ls.AddToLHS_Symmetric(ni, nj,
                        lhs[r * 3 + 0, c * 3 + 0], lhs[r * 3 + 0, c * 3 + 1], lhs[r * 3 + 0, c * 3 + 2],
                        lhs[r * 3 + 1, c * 3 + 0], lhs[r * 3 + 1, c * 3 + 1], lhs[r * 3 + 1, c * 3 + 2],
                        lhs[r * 3 + 2, c * 3 + 0], lhs[r * 3 + 2, c * 3 + 1], lhs[r * 3 + 2, c * 3 + 2]);
                    }
                }
            }

            return czResults;
        }

    }
}
