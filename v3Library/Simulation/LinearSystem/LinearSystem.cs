using System;
using System.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;

namespace icFlow
{
    public class LinearSystem
    {
        // allocations for sparse matrix, RHS, and functionality for solving
        // matrix can be CSR/BCSR, can be symmetric/nonsymmetric
        // diffrent types of matrix require different assembly algorithms

        // initialized when creating structure
        public CSRDictionary csrd = new CSRDictionary();    // dictionary for holding i-j pairs
        public double[] vals, rhs, dx;   // value arrays
        public int dvalsSize { get { return csrd.nnz*9; } }
        public int dxSize { get { return csrd.N*3; } }

        Stopwatch sw = new Stopwatch();

        // MKL specific
        const int mklCriterionExp = 6;
        const int mklPreconditioner = 1;

        public void Clear()
        {
            vals = rhs = dx = null;
            csrd.ClearDynamic();
            csrd.ClearStatic();
        }

        // before this function runs, it is assumed that
        // activeNodes have sequential .altId, .neighbors are filled
        public void CreateStructure(FrameInfo cf)
        {
            sw.Restart();
            csrd.CreateStructure();

            // allocate value arrays
            if (vals == null || vals.Length < dvalsSize) vals = new double[dvalsSize * 2];
            if (dx == null || dx.Length < dxSize)
            {
                rhs = new double[dxSize];
                dx = new double[dxSize];
            }
            // clear values and rhs, even though these arrays may be overwritten
            Array.Clear(vals, 0, vals.Length);
            Array.Clear(rhs, 0, rhs.Length);
            sw.Stop();
            cf.CSRStructure += sw.ElapsedMilliseconds;
        }

        [DllImport("PardisoLoader2.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern int SolveDouble3(int[] ja, int[] ia, double[] a, int n, double[] b, double[] x, int matrixType, int iparam4, int dim, int msglvl, int check);

        long refTime;
        public void Solve(FrameInfo cf)
        {
            bool symmetric = true;
            Assert(false);   
            sw.Restart();

            const int check = 0;
            const int verbosity = 0;

            int mklMatrixType = symmetric ? -2 : 11; // -2 for symmetric indefinite; 11 for nonsymmetric
            int dim = 3;
            const int param4 = 0;
            Array.Clear(dx, 0, dx.Length);
            int mklResult = SolveDouble3(csrd.csr_cols, csrd.csr_rows, vals, csrd.N, rhs, dx, mklMatrixType, param4, dim, verbosity, check);
            if (mklResult != 0) throw new Exception("MKL solver error");

            sw.Stop();
            cf.MKLSolve += sw.ElapsedMilliseconds;
            refTime = sw.ElapsedMilliseconds;
        }


        // used to check convergence/divergence of the solution
        public double NormOfDx()
        {
            double result = 0;
            for (int i = 0; i < dxSize; i++) result += dx[i] * dx[i];
            return result;
        }


        #region save to file

        static int saves = 0;

        byte[] snapshot;
        public void DumpMatrix()
        {
            string dir = "LSs";
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            saves++;
            Stream str = File.Create($"{dir}/ls{saves}.bin");
            BinaryWriter bw = new BinaryWriter(str);


            int byteSizeDVals = dvalsSize * sizeof(double);
            int byteSizeDx = dxSize * sizeof(double);
            int byteSizeCSRCols = csrd.nnz*sizeof(int);
            int byteSizeCSRRows = (csrd.N + 1) * sizeof(int);

            int bufferSize = byteSizeCSRCols + byteSizeCSRRows+ byteSizeDVals + 2 * byteSizeDx;
            if (snapshot == null || snapshot.Length < bufferSize) snapshot = new byte[bufferSize * 2];

            int offset = 0;
            Buffer.BlockCopy(csrd.csr_cols, 0, snapshot, offset, byteSizeCSRCols);
            offset += byteSizeCSRCols;
            Buffer.BlockCopy(csrd.csr_rows, 0, snapshot, offset, byteSizeCSRRows);
            offset += byteSizeCSRRows;
            Buffer.BlockCopy(vals, 0, snapshot, offset, byteSizeDVals);
            offset += byteSizeDVals;
            Buffer.BlockCopy(rhs, 0, snapshot, offset, byteSizeDx);
            offset += byteSizeDx;
            Buffer.BlockCopy(dx, 0, snapshot, offset, byteSizeDx);
            offset += byteSizeDx;
            Assert(offset == bufferSize);
            bw.Write(csrd.nnz);
            bw.Write(csrd.N);
            bw.Write(snapshot, 0, bufferSize);
            bw.Write(refTime);
            bw.Close();
        }

        #endregion

        #region working with values directly

        public void AddToRHS(int atWhichIndex, double d0, double d1, double d2)
        {
            if (atWhichIndex < 0) return;
            int i3 = atWhichIndex*3;
            Debug.Assert(i3 + 2 < csrd.N * 3);
            rhs[i3] += d0;
            rhs[i3 + 1] += d1;
            rhs[i3 + 2] += d2;
        }


        public void AddToLHS_Symmetric(int row, int column, 
        double a00, double a01, double a02,
        double a10, double a11, double a12,
        double a20, double a21, double a22)
        {
            if (row > column || row < 0 || column < 0) return;
            int offset = csrd[row, column];
            Debug.Assert(offset < csrd.nnz);
            offset *= 9;
            vals[offset + 0] += a00;
            vals[offset + 1] += a01;
            vals[offset + 2] += a02;
            vals[offset + 3] += a10;
            vals[offset + 4] += a11;
            vals[offset + 5] += a12;
            vals[offset + 6] += a20;
            vals[offset + 7] += a21;
            vals[offset + 8] += a22;
        }

        #endregion

        #region assertion

        public void Assert(bool nonSymmetric)
        {
            for(int i=0;i<dxSize;i++) Debug.Assert(!double.IsNaN(rhs[i]),"rhs constains NaN");
            for(int i=0;i<dvalsSize;i++) Debug.Assert(!double.IsNaN(vals[i]), "bcsr contains NaN");

            csrd.Assert(nonSymmetric);
        }
        #endregion
    }
}
