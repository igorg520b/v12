using System.Diagnostics;
using static System.Math;

namespace icFlow
{
    class CPU_Distance_Derivatives
    {
        #region constants
        static double[,] a2 = {
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 2, 0, 0, -2, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 2, 0, 0, -2, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 2, 0, 0, -2, 0, 0, 0 },
    { 0, 0, 0, -2, 0, 0, 2, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, -2, 0, 0, 2, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, -2, 0, 0, 2, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };

        // second derivatives of b
        static double[,] b2 = {
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 2, 0, 0, -1, 0, 0, -1, 0, 0 },
    { 0, 0, 0, 0, 2, 0, 0, -1, 0, 0, -1, 0 },
    { 0, 0, 0, 0, 0, 2, 0, 0, -1, 0, 0, -1 },
    { 0, 0, 0, -1, 0, 0, 0, 0, 0, 1, 0, 0 },
    { 0, 0, 0, 0, -1, 0, 0, 0, 0, 0, 1, 0 },
    { 0, 0, 0, 0, 0, -1, 0, 0, 0, 0, 0, 1 },
    { 0, 0, 0, -1, 0, 0, 1, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, -1, 0, 0, 1, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, -1, 0, 0, 1, 0, 0, 0 } };

        // second derivatives of c
        static double[,] c2 = {
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 2, 0, 0, 0, 0, 0, -2, 0, 0 },
    { 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, -2, 0 },
    { 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, -2 },
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, -2, 0, 0, 0, 0, 0, 2, 0, 0 },
    { 0, 0, 0, 0, -2, 0, 0, 0, 0, 0, 2, 0 },
    { 0, 0, 0, 0, 0, -2, 0, 0, 0, 0, 0, 2 } };

        // second derivatives of d
        static double[,] d2 = {
    { 0, 0, 0, 1, 0, 0, -1, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 1, 0, 0, -1, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 1, 0, 0, -1, 0, 0, 0 },
    { 1, 0, 0, -2, 0, 0, 1, 0, 0, 0, 0, 0 },
    { 0, 1, 0, 0, -2, 0, 0, 1, 0, 0, 0, 0 },
    { 0, 0, 1, 0, 0, -2, 0, 0, 1, 0, 0, 0 },
    { -1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, -1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, -1, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };

        // second derivatives of e
        static double[,] e2 = {
    { 0, 0, 0, 1, 0, 0, 0, 0, 0, -1, 0, 0 },
    { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, -1, 0 },
    { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, -1 },
    { 1, 0, 0, -2, 0, 0, 0, 0, 0, 1, 0, 0 },
    { 0, 1, 0, 0, -2, 0, 0, 0, 0, 0, 1, 0 },
    { 0, 0, 1, 0, 0, -2, 0, 0, 0, 0, 0, 1 },
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
    { -1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, -1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, -1, 0, 0, 1, 0, 0, 0, 0, 0, 0 } };

        #endregion

        #region point-line

        /// <summary>
        /// dot product of the form (p1-p0)(p2-p1)
        /// </summary>
        /// <returns>dot product</returns>
        /// <param name="x">coords of p0,p1,p2</param>
        /// <param name="fd">first derivatives</param>
        /// <param name="sd">second derivatives 9x9</param>
        static unsafe double sp_dot3(double* x, double* fd, double* sd)
        {

            double x0 = x[0];
            double y0 = x[1];
            double z0 = x[2];

            double x1 = x[3];
            double y1 = x[4];
            double z1 = x[5];

            double x2 = x[6];
            double y2 = x[7];
            double z2 = x[8];

            fd[0] = x1 - x2;
            fd[1] = y1 - y2;
            fd[2] = z1 - z2;
            fd[3] = x0 - 2 * x1 + x2;
            fd[4] = y0 - 2 * y1 + y2;
            fd[5] = z0 - 2 * z1 + z2;
            fd[6] = x1 - x0;
            fd[7] = y1 - y0;
            fd[8] = z1 - z0;

            // second derivs
            // sd is 9x9
            for (int k = 0; k < 3; k++)
            {
                sd[(k + 3)*9+ k + 3] = -2;
                sd[k*9+ (k + 6)] = sd[(k + 6)*9+ k] = -1;
                sd[k*9+ (k + 3)] = sd[(k + 3)*9+ k] = sd[(k + 6)*9+ (k + 3)] = sd[(k + 3)*9+ (k + 6)] = 1;
            }
            return (x1 - x0) * (x2 - x1) + (y1 - y0) * (y2 - y1) + (z1 - z0) * (z2 - z1);
        }

        /// <summary>
        /// calculate the first and the second derivatives of f^2, given that f' and f'' are known
        /// </summary>
        /// <returns>The squared.</returns>
        /// <param name="f">value at point</param>
        /// <param name="fd">input first derivatives</param>
        /// <param name="sd">input second derivatives</param>
        /// <param name="fdOut">output first derivatives 9</param>
        /// <param name="sdOut">second derivatives 9x9</param>
        static unsafe double function_squared(double f, double* fd, double* sd, double* fdOut, double* sdOut)
        {
            for (int i = 0; i < 9; i++)
            {
                fdOut[i] = 2 * f * fd[i];
                for (int j = 0; j < 9; j++) sdOut[i*9+j] = 2 * (fd[i] * fd[j] + f * sd[i*9+j]);
            }
            return f * f;
        }

        // arrays are 9x9
        static unsafe double sp_dot3_squared(double* x, double* fd, double* sd)
        {
            double* sp_dot3_fd = stackalloc double[9];
            double* sp_dot3_sd = stackalloc double[81];

            double sp_dot3_value = sp_dot3(x, sp_dot3_fd, sp_dot3_sd);

            double result = function_squared(sp_dot3_value, sp_dot3_fd, sp_dot3_sd, fd, sd);
            return result;
        }

        /// <summary>
        /// value, 1st and 2nd derivatives of the squared distance between points selected by idx1 and idx2
        /// </summary>
        /// <returns>The vertex distance and derivs.</returns>
        /// <param name="idx1">Idx1.</param>
        /// <param name="idx2">Idx2.</param>
        /// <param name="x">coordinates</param>
        /// <param name="sdd">first derivs 9</param>
        /// <param name="sdd2">second derivs 9x9</param>
        static unsafe double vertex_vertex_distance_and_derivs(int idx1, int idx2,
double* x, double* sdd, double* sdd2)
        {

            int ix0 = idx1 * 3 + 0;
            int iy0 = idx1 * 3 + 1;
            int iz0 = idx1 * 3 + 2;

            int ix1 = idx2 * 3 + 0;
            int iy1 = idx2 * 3 + 1;
            int iz1 = idx2 * 3 + 2;

            double x0 = x[ix0];
            double y0 = x[iy0];
            double z0 = x[iz0];

            double x1 = x[ix1];
            double y1 = x[iy1];
            double z1 = x[iz1];

            sdd[ix0] = 2 * (x0 - x1);
            sdd[iy0] = 2 * (y0 - y1);
            sdd[iz0] = 2 * (z0 - z1);

            sdd[ix1] = -sdd[ix0];
            sdd[iy1] = -sdd[iy0];
            sdd[iz1] = -sdd[iz0];

            sdd2[ix0*9+ ix0] = sdd2[iy0 * 9 + iy0] = sdd2[iz0 * 9 + iz0] = 2;
            sdd2[ix1 * 9 + ix1] = sdd2[iy1 * 9 + iy1] = sdd2[iz1 * 9 + iz1] = 2;
            sdd2[ix0 * 9 + ix1] = sdd2[iy0 * 9 + iy1] = sdd2[iz0 * 9 + iz1] = -2;
            sdd2[ix1 * 9 + ix0] = sdd2[iy1 * 9 + iy0] = sdd2[iz1 * 9 + iz0] = -2;

            return (x0 - x1) * (x0 - x1) + (y0 - y1) * (y0 - y1) + (z0 - z1) * (z0 - z1);
        }

        /// <summary>
        /// value, 1st and 2nd derivatives of the squared distance between points selected by idx1 and idx2
        /// </summary>
        /// <returns>The vertex distance and derivs 12.</returns>
        /// <param name="idx1">Idx1.</param>
        /// <param name="idx2">Idx2.</param>
        /// <param name="x">The x coordinate.</param>
        /// <param name="fd">Fd  12</param>
        /// <param name="sd">Sd  12x12</param>
        static unsafe double vertex_vertex_distance_and_derivs_12(int idx1, int idx2,
        double* x, double* fd, double* sd)
        {

            int ix0 = idx1 * 3 + 0;
            int iy0 = idx1 * 3 + 1;
            int iz0 = idx1 * 3 + 2;

            int ix1 = idx2 * 3 + 0;
            int iy1 = idx2 * 3 + 1;
            int iz1 = idx2 * 3 + 2;

            double x0 = x[ix0];
            double y0 = x[iy0];
            double z0 = x[iz0];

            double x1 = x[ix1];
            double y1 = x[iy1];
            double z1 = x[iz1];

            fd[ix0] = 2 * (x0 - x1);
            fd[iy0] = 2 * (y0 - y1);
            fd[iz0] = 2 * (z0 - z1);

            fd[ix1] = -fd[ix0];
            fd[iy1] = -fd[iy0];
            fd[iz1] = -fd[iz0];

            sd[ix0*12+ ix0] = sd[iy0 * 12 + iy0] = sd[iz0 * 12 + iz0] = 2;
            sd[ix1 * 12 + ix1] = sd[iy1 * 12 + iy1] = sd[iz1 * 12 + iz1] = 2;
            sd[ix0 * 12 + ix1] = sd[iy0 * 12 + iy1] = sd[iz0 * 12 + iz1] = -2;
            sd[ix1 * 12 + ix0] = sd[iy1 * 12 + iy0] = sd[iz1 * 12 + iz0] = -2;

            return (x0 - x1) * (x0 - x1) + (y0 - y1) * (y0 - y1) + (z0 - z1) * (z0 - z1);
        }

        // in: x 9
        // out: sdd 9; sdd2 9x9
        static unsafe double vertex_edge_distance_and_derivs(double* x, double* sdd, double* sdd2)
        {
            double x0 = x[0];
            double x1 = x[1];
            double x2 = x[2];
            double x3 = x[3];
            double x4 = x[4];
            double x5 = x[5];
            double x6 = x[6];
            double x7 = x[7];
            double x8 = x[8];

            double edge_length_sq = (-x3 + x6) * (-x3 + x6) + (-x4 + x7) * (-x4 + x7) + (-x5 + x8) * (-x5 + x8);
            // pc = p1(1-t)+p2(t)
            double t = -((-x0 + x3) * (-x3 + x6) + (-x1 + x4) * (-x4 + x7) +
            (-x2 + x5) * (-x5 + x8)) / edge_length_sq;

            double u = 1 - t;
            double sqrDist = (x0 - u * x3 - t * x6) * (x0 - u * x3 - t * x6) +
            (x1 - u * x4 - t * x7) * (x1 - u * x4 - t * x7) +
            (x2 - u * x5 - t * x8) * (x2 - u * x5 - t * x8);
            double edge_length = Sqrt(edge_length_sq);
            double dist = Sqrt(sqrDist);
            double ratio = sqrDist / edge_length;

            // |(x1-x0)(x2-x1)|^2 and its derivatives
            double* g_fd = stackalloc double[9];
            double* g_sd = stackalloc double[81];
            double g = sp_dot3_squared(x, g_fd, g_sd);

            // f1 = |x1-x0|^2
            double* f1fd = stackalloc double[9];
            double* f1sd = stackalloc double[81];
            double f1 = vertex_vertex_distance_and_derivs(1, 0, x, f1fd, f1sd);

            // f2 = |x2-x1|^2
            double* f2fd = stackalloc double[9];
            double* f2sd = stackalloc double[81];
            double f2 = vertex_vertex_distance_and_derivs(2, 1, x, f2fd, f2sd);

            // combine together
            double f2sq = f2 * f2;
            double f2cube = f2sq * f2;
            for (int i = 0; i < 9; i++)
            {
                sdd[i] = f1fd[i] + (g * f2fd[i] - f2 * g_fd[i]) / f2sq;

                for (int j = 0; j < 9; j++)
                {
                    double term1 = -2 * g * f2fd[i] * f2fd[j] / f2cube;
                    double term2 = (g_fd[i] * f2fd[j] + g_fd[j] * f2fd[i]) / f2sq;
                    double term3 = f1sd[i*9+ j];
                    double term4 = g * f2sd[i * 9 + j] / f2sq;
                    double term5 = -g_sd[i * 9 + j] / f2;
                    sdd2[i * 9 + j] = term1 + term2 + term3 + term4 + term5;
                }
            }
            return sqrDist;
        }

        // version for arrays with 12 elements
        // input: x: 12
        // output: fd 12, sd 12x12
        static unsafe double vertex_edge_distance_and_derivs_12(double* x,
        int idx1, int idx2, double* fd, double* sd)
        {
            double* _x = stackalloc double[9];
            _x[0] = x[0];
            _x[1] = x[1];
            _x[2] = x[2];

            idx1 *= 3; idx2 *= 3;

            double p01 = (x[0] - x[0 + idx1]) * (x[0] - x[0 + idx1]) + (x[1] - x[1 + idx1]) * (x[1] - x[1 + idx1]) + (x[2] - x[2 + idx1]) * (x[2] - x[2 + idx1]);
            double p02 = (x[0] - x[0 + idx2]) * (x[0] - x[0 + idx2]) + (x[1] - x[1 + idx2]) * (x[1] - x[1 + idx2]) + (x[2] - x[2 + idx2]) * (x[2] - x[2 + idx2]);

            if (p01 > p02)
            {
                // swap indices
                int tmp_idx = idx1;
                idx1 = idx2;
                idx2 = tmp_idx;
            }

            _x[3] = x[0 + idx1];
            _x[4] = x[1 + idx1];
            _x[5] = x[2 + idx1];

            _x[6] = x[0 + idx2];
            _x[7] = x[1 + idx2];
            _x[8] = x[2 + idx2];

            double* _fd = stackalloc double[9];
            double* _sd = stackalloc double[81];
            double result = vertex_edge_distance_and_derivs(_x, _fd, _sd);

            // distribute _fd and _sd
            for (int i = 0; i < 3; i++)
            {
                fd[i] = _fd[i];
                fd[idx1 + i] = _fd[3 + i];
                fd[idx2 + i] = _fd[6 + i];

                for (int j = 0; j < 3; j++)
                {
                    sd[i*12+ j] = _sd[i*9+ j];
                    sd[(i + idx1) * 12 + j] = _sd[(3 + i) * 9 + j];
                    sd[i * 12 + j + idx1] = _sd[i * 9 + 3 + j];
                    sd[(i + idx1) * 12 + j + idx1] = _sd[(i + 3) * 9 + j + 3];
                    sd[(i + idx1) * 12 + j + idx2] = _sd[(i + 3) * 9 + j + 6];
                    sd[(i + idx2) * 12 + j + idx1] = _sd[(i + 6) * 9 + j + 3];
                    sd[i * 12 + j + idx2] = _sd[i * 9 + j + 6];
                    sd[(i + idx2) * 12 + j] = _sd[(i + 6) * 9 + j];
                    sd[(i + idx2) * 12 + j + idx2] = _sd[(i + 6) * 9 + j + 6];
                }
            }
            return result;
        }

        #endregion

        #region point-plane

        // Kronecker delta
        static double xd(int idx1, int idx2) { return idx1 == idx2 ? 1D : 0; }

        // input: x[12] array of p0,p1,p2,p3
        // output: first derivatives of squared distance fd[12]
        // second derivatives of squared distance sd[12][12]
        // return value is the squared distance
        // note: don't call this function directly
        static unsafe double point_plane_distance(double* x, double* fd, double* sd)
        {
            double output_s, output_t; // for testing
            double x0 = x[0];
            double x1 = x[1];
            double x2 = x[2];
            double x3 = x[3];
            double x4 = x[4];
            double x5 = x[5];
            double x6 = x[6];
            double x7 = x[7];
            double x8 = x[8];
            double x9 = x[9];
            double x10 = x[10];
            double x11 = x[11];

            double a = (-x3 + x6) * (-x3 + x6) + (-x4 + x7) * (-x4 + x7) + (-x5 + x8) * (-x5 + x8);
            double b = (x10 - x4) * (-x4 + x7) + (x11 - x5) * (-x5 + x8) + (-x3 + x6) * (-x3 + x9);
            double c = (x10 - x4) * (x10 - x4) + (x11 - x5) * (x11 - x5) + (-x3 + x9) * (-x3 + x9);
            double d = (-x0 + x3) * (-x3 + x6) + (-x1 + x4) * (-x4 + x7) + (-x2 + x5) * (-x5 + x8);
            double e = (x10 - x4) * (-x1 + x4) + (x11 - x5) * (-x2 + x5) + (-x0 + x3) * (-x3 + x9);
            double f = (-x0 + x3) * (-x0 + x3) + (-x1 + x4) * (-x1 + x4) + (-x2 + x5) * (-x2 + x5);

            double det = a * c - b * b;
            double detsq = det * det;
            double detcube = detsq * det;

            double s = b * e - c * d;
            double t = b * d - a * e;

            double invDet = 1 / det;
            s *= invDet;
            t *= invDet;
            output_s = s;
            output_t = t;
            double u = 1 - (s + t);

            double sqrDistance = (-x0 + x6 * s + x9 * t + x3 * u) * (-x0 + x6 * s + x9 * t + x3 * u) +
                (-x1 + x7 * s + x10 * t + x4 * u) * (-x1 + x7 * s + x10 * t + x4 * u) +
                (-x2 + x8 * s + x11 * t + x5 * u) * (-x2 + x8 * s + x11 * t + x5 * u);

            // u = zeta1; s = zeta2; t = zeta3;
            //        double s2[12][12], t2[12][12], det2[12][12];
            //    double u1[12], u2[12][12];
            double* s2 = stackalloc double[12* 12];
            double* t2 = stackalloc double[12* 12];
            double* det2 = stackalloc double[12* 12];
            double* u2 = stackalloc double[12* 12];
            double* u1 = stackalloc double[12];

            // derivatives of s and t
            // first derivatives of the above quantities
            double z = 0;
            double* a1 = stackalloc double[] { z, z, z, -2 * (-x3 + x6), -2 * (-x4 + x7), -2 * (-x5 + x8), 2 * (-x3 + x6), 2 * (-x4 + x7), 2 * (-x5 + x8), z, z, z };
            double* b1 = stackalloc double[] { z, z, z, 2 * x3 - x6 - x9, -x10 + 2 * x4 - x7, -x11 + 2 * x5 - x8, -x3 + x9, x10 - x4, x11 - x5, -x3 + x6, -x4 + x7, -x5 + x8 };
            double* c1 = stackalloc double[] { z, z, z, -2 * (-x3 + x9), -2 * (x10 - x4), -2 * (x11 - x5), z, z, z, 2 * (-x3 + x9), 2 * (x10 - x4), 2 * (x11 - x5) };
            double* d1 = stackalloc double[] { x3 - x6, x4 - x7, x5 - x8, x0 - 2 * x3 + x6, x1 - 2 * x4 + x7, x2 - 2 * x5 + x8, -x0 + x3, -x1 + x4, -x2 + x5, z, z, z };
            double* e1 = stackalloc double[] { x3 - x9, -x10 + x4, -x11 + x5, x0 - 2 * x3 + x9, x1 + x10 - 2 * x4, x11 + x2 - 2 * x5, z, z, z, -x0 + x3, -x1 + x4, -x2 + x5 };
//            double* f1 = stackalloc double[] { -2 * (-x0 + x3), -2 * (-x1 + x4), -2 * (-x2 + x5), 2 * (-x0 + x3), 2 * (-x1 + x4), 2 * (-x2 + x5), z, z, z, z, z, z };

            //        double s1[12], t1[12], det1[12];
            double* s1 = stackalloc double[12];
            double* t1 = stackalloc double[12];
            double* det1 = stackalloc double[12];

            // first derivatives 
            for (int i = 0; i < 12; i++)
            {
                det1[i] = c * a1[i] + a * c1[i] - 2 * b * b1[i];
                s1[i] = ((c * d - b * e) * det1[i]) / detsq + ((e * b1[i] + b * e1[i]) - (d * c1[i] + c * d1[i])) / det;
                t1[i] = ((a * e - b * d) * det1[i]) / detsq + ((d * b1[i] + b * d1[i]) - (a * e1[i] + e * a1[i])) / det;
                u1[i] = -(s1[i] + t1[i]);

                // expression is machine-generated
                fd[i] = -2 * (x0 - x6 * s - x9 * t - x3 * u) *
                    (x6 * s1[i] + x9 * t1[i] + x3 * u1[i] - xd(0, i) + u * xd(3, i) +
                        s * xd(6, i) + t * xd(9, i)) -
                    2 * (x1 - x7 * s - x10 * t - x4 * u) *
                    (x7 * s1[i] + x10 * t1[i] + x4 * u1[i] - xd(1, i) + u * xd(4, i) +
                        s * xd(7, i) + t * xd(10, i)) -
                    2 * (x2 - x8 * s - x11 * t - x5 * u) *
                    (x8 * s1[i] + x11 * t1[i] + x5 * u1[i] - xd(2, i) + u * xd(5, i) +
                        s * xd(8, i) + t * xd(11, i));
            }

            // loop may be simplified, because all matrices are symmetric
            for (int i = 0; i < 12; i++)
                for (int j = 0; j < 12; j++)
                {
                    det2[i*12+ j] = -2 * b1[i] * b1[j] + a1[j] * c1[i] + a1[i] * c1[j] + c * a2[i, j] - 2 * b * b2[i, j] + a * c2[i, j];

                    s2[i * 12 + j] =
                        +(-(c1[j] * d1[i]) - c1[i] * d1[j] + b1[j] * e1[i] + b1[i] * e1[j] + e * b2[i, j] - d * c2[i, j] - c * d2[i, j] + b * e2[i, j]) / det
                        - ((det1[j] * (e * b1[i] - d * c1[i] - c * d1[i] + b * e1[i])) + (det1[i] * (e * b1[j] - d * c1[j] - c * d1[j] + b * e1[j])) + ((-(c * d) + b * e) * det2[i, j])) / detsq
                        + (2 * (-(c * d) + b * e) * det1[i] * det1[j]) / detcube;

                    t2[i * 12 + j] =
                        +(b1[j] * d1[i] + b1[i] * d1[j] - a1[j] * e1[i] - a1[i] * e1[j] - e * a2[i, j] + d * b2[i, j] + b * d2[i, j] - a * e2[i, j]) / det
                        - ((det1[j] * (-(e * a1[i]) + d * b1[i] + b * d1[i] - a * e1[i])) + (det1[i] * (-(e * a1[j]) + d * b1[j] + b * d1[j] - a * e1[j])) + ((b * d - a * e) * det2[i, j])) / detsq
                        + (2 * (b * d - a * e) * det1[i] * det1[j]) / detcube;

                    u2[i * 12 + j] = -(s2[i * 12 + j] + t2[i * 12 + j]);

                    // this expression was machine-generated
                    sd[i * 12 + j] = 2 * ((x6 * s1[i] + x9 * t1[i] + x3 * u1[i] - xd(0, i) + u * xd(3, i) +
                        s * xd(6, i) + t * xd(9, i)) *
                        (x6 * s1[j] + x9 * t1[j] + x3 * u1[j] - xd(0, j) + u * xd(3, j) +
                            s * xd(6, j) + t * xd(9, j)) -
                            (x0 - x6 * s - x9 * t - x3 * u) *
                        (x6 * s2[i * 12 + j] + x9 * t2[i * 12 + j] +
                            x3 * u2[i * 12 + j] + u1[j] * xd(3, i) + u1[i] * xd(3, j) + s1[j] * xd(6, i) +
                            s1[i] * xd(6, j) + t1[j] * xd(9, i) + t1[i] * xd(9, j)) +
                            (x7 * s1[i] + x10 * t1[i] + x4 * u1[i] - xd(1, i) + u * xd(4, i) +
                                s * xd(7, i) + t * xd(10, i)) *
                                (x7 * s1[j] + x10 * t1[j] + x4 * u1[j] - xd(1, j) + u * xd(4, j) +
                                    s * xd(7, j) + t * xd(10, j)) -
                                    (x1 - x7 * s - x10 * t - x4 * u) *
                        (x7 * s2[i * 12 + j] + x10 * t2[i * 12 + j] +
                            x4 * u2[i * 12 + j] + u1[j] * xd(4, i) + u1[i] * xd(4, j) + s1[j] * xd(7, i) +
                            s1[i] * xd(7, j) + t1[j] * xd(10, i) + t1[i] * xd(10, j)) +
                            (x8 * s1[i] + x11 * t1[i] + x5 * u1[i] - xd(2, i) + u * xd(5, i) +
                                s * xd(8, i) + t * xd(11, i)) *
                                (x8 * s1[j] + x11 * t1[j] + x5 * u1[j] - xd(2, j) + u * xd(5, j) +
                                    s * xd(8, j) + t * xd(11, j)) -
                                    (x2 - x8 * s - x11 * t - x5 * u) *
                        (x8 * s2[i * 12 + j] + x11 * t2[i * 12 + j] +
                            x5 * u2[i * 12 + j] + u1[j] * xd(5, i) + u1[i] * xd(5, j) + s1[j] * xd(8, i) +
                            s1[i] * xd(8, j) + t1[j] * xd(11, i) + t1[i] * xd(11, j)));
                }

            return sqrDistance;
        }

        #endregion

        #region point-triangle

        // fd 12; sd 12x12
        public unsafe static double pt(double* x, double* fd, double* sd, out double zeta2, out double zeta3)
        {
            double x0 = x[0];
            double x1 = x[1];
            double x2 = x[2];
            double x3 = x[3];
            double x4 = x[4];
            double x5 = x[5];
            double x6 = x[6];
            double x7 = x[7];
            double x8 = x[8];
            double x9 = x[9];
            double x10 = x[10];
            double x11 = x[11];

            double a = (-x3 + x6) * (-x3 + x6) + (-x4 + x7) * (-x4 + x7) + (-x5 + x8) * (-x5 + x8);
            double b = (x10 - x4) * (-x4 + x7) + (x11 - x5) * (-x5 + x8) + (-x3 + x6) * (-x3 + x9);
            double c = (x10 - x4) * (x10 - x4) + (x11 - x5) * (x11 - x5) + (-x3 + x9) * (-x3 + x9);
            double d = (-x0 + x3) * (-x3 + x6) + (-x1 + x4) * (-x4 + x7) + (-x2 + x5) * (-x5 + x8);
            double e = (x10 - x4) * (-x1 + x4) + (x11 - x5) * (-x2 + x5) + (-x0 + x3) * (-x3 + x9);

            double det = a * c - b * b;
            double s = b * e - c * d;
            double t = b * d - a * e;
            double sqrDistance;

            if (s + t <= det)
            {
                if (s < 0)
                {
                    if (t < 0)  // region 4
                    {
                        if (d < 0)
                        {
                            t = 0;
                            if (-d >= a)
                            {
                                s = 1;
                                sqrDistance = vertex_vertex_distance_and_derivs_12(0, 2, x, fd, sd);
                            }
                            else
                            {
                                s = -d / a;
                                sqrDistance = vertex_edge_distance_and_derivs_12(x, 2, 1, fd, sd);
                            }
                        }
                        else
                        {
                            s = 0;
                            if (e >= 0)
                            {
                                t = 0;
                                sqrDistance = vertex_vertex_distance_and_derivs_12(0, 1, x, fd, sd);
                            }
                            else if (-e >= c)
                            {
                                t = 1;
                                sqrDistance = vertex_vertex_distance_and_derivs_12(0, 3, x, fd, sd);
                            }
                            else
                            {
                                t = -e / c;
                                sqrDistance = vertex_edge_distance_and_derivs_12(x, 3, 1, fd, sd);
                            }
                        }
                    }
                    else  // region 3
                    {
                        s = 0;
                        if (e >= 0)
                        {
                            t = 0;
                            sqrDistance = vertex_vertex_distance_and_derivs_12(0, 1, x, fd, sd);
                        }
                        else if (-e >= c)
                        {
                            t = 1;
                            sqrDistance = vertex_vertex_distance_and_derivs_12(0, 3, x, fd, sd);
                        }
                        else
                        {
                            t = -e / c;
                            sqrDistance = vertex_edge_distance_and_derivs_12(x, 3, 1, fd, sd);
                        }
                    }
                }
                else if (t < 0)  // region 5
                {
                    t = 0;
                    if (d >= 0)
                    {
                        s = 0;
                        sqrDistance = vertex_vertex_distance_and_derivs_12(0, 1, x, fd, sd);
                    }
                    else if (-d >= a)
                    {
                        s = 1;
                        sqrDistance = vertex_vertex_distance_and_derivs_12(0, 2, x, fd, sd);
                    }
                    else
                    {
                        s = -d / a;
                        sqrDistance = vertex_edge_distance_and_derivs_12(x, 1, 2, fd, sd);
                    }
                }
                else  // region 0
                {
                    double invDet = (1) / det;
                    s *= invDet;
                    t *= invDet;
                    sqrDistance = point_plane_distance(x, fd, sd);
                }
            }
            else
            {
                double tmp0, tmp1, numer, denom;

                if (s < 0)  // region 2
                {
                    tmp0 = b + d;
                    tmp1 = c + e;
                    if (tmp1 > tmp0)
                    {
                        numer = tmp1 - tmp0;
                        denom = a - 2 * b + c;
                        if (numer >= denom)
                        {
                            s = 1;
                            t = 0;
                            sqrDistance = vertex_vertex_distance_and_derivs_12(0, 2, x, fd, sd);
                        }
                        else
                        {
                            s = numer / denom;
                            t = 1 - s;
                            sqrDistance = vertex_edge_distance_and_derivs_12(x, 2, 3, fd, sd);
                        }
                    }
                    else
                    {
                        s = 0;
                        if (tmp1 <= 0)
                        {
                            t = 1;
                            sqrDistance = vertex_vertex_distance_and_derivs_12(0, 3, x, fd, sd);
                        }
                        else if (e >= 0)
                        {
                            t = 0;
                            sqrDistance = vertex_vertex_distance_and_derivs_12(0, 1, x, fd, sd);
                        }
                        else
                        {
                            t = -e / c;
                            sqrDistance = vertex_edge_distance_and_derivs_12(x, 1, 3, fd, sd);
                        }
                    }
                }
                else if (t < 0)  // region 6
                {
                    tmp0 = b + e;
                    tmp1 = a + d;
                    if (tmp1 > tmp0)
                    {
                        numer = tmp1 - tmp0;
                        denom = a - 2 * b + c;
                        if (numer >= denom)
                        {
                            t = 1;
                            s = 0;
                            sqrDistance = vertex_vertex_distance_and_derivs_12(0, 3, x, fd, sd);
                        }
                        else
                        {
                            t = numer / denom;
                            s = 1 - t;
                            sqrDistance = vertex_edge_distance_and_derivs_12(x, 2, 3, fd, sd);
                        }
                    }
                    else
                    {
                        t = 0;
                        if (tmp1 <= 0)
                        {
                            s = 1;
                            sqrDistance = vertex_vertex_distance_and_derivs_12(0, 2, x, fd, sd);
                        }
                        else if (d >= 0)
                        {
                            s = 0;
                            sqrDistance = vertex_vertex_distance_and_derivs_12(0, 1, x, fd, sd);
                        }
                        else
                        {
                            s = -d / a;
                            sqrDistance = vertex_edge_distance_and_derivs_12(x, 1, 2, fd, sd);
                        }
                    }
                }
                else  // region 1
                {
                    numer = c + e - b - d;
                    if (numer <= 0)
                    {
                        s = 0;
                        t = 1;
                        sqrDistance = vertex_vertex_distance_and_derivs_12(0, 3, x, fd, sd);
                    }
                    else
                    {
                        denom = a - 2 * b + c;
                        if (numer >= denom)
                        {
                            s = 1;
                            t = 0;
                            sqrDistance = vertex_vertex_distance_and_derivs_12(0, 2, x, fd, sd);
                        }
                        else
                        {
                            s = numer / denom;
                            t = 1 - s;
                            sqrDistance = vertex_edge_distance_and_derivs_12(x, 2, 3, fd, sd);
                        }
                    }
                }
            }

            zeta2 = s;
            zeta3 = t;
            return sqrDistance;
        }
        #endregion

    }
}
