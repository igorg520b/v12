using System;
using System.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections.Generic;

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

        public void Solve(FrameInfo cf, bool nonSymmetric)
        {
            bool symmetric = !nonSymmetric;
            Assert(nonSymmetric);   // comment this out 
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
        }


        // used to check convergence/divergence of the solution
        public double NormOfDx()
        {
            double result = 0;
            for (int i = 0; i < dxSize; i++) result += dx[i] * dx[i];
            return result;
        }


        #region working with values directly

        /// <summary>
        /// Add vector of length 3 to RHS at a specified index
        /// </summary>
        /// <param name="values">values to add, must have the length of 3</param>
        /// <param name="atWhichIndex">index at which to add, will be multiplied by 3</param>
        public void AddToRHS(double[] values, int atWhichIndex)
        {
            Debug.Assert(values.Length == 3);
            for (int i = 0; i < 3; i++) rhs[atWhichIndex * 3 + i] += values[i];
        }

        /// <summary>
        /// Add 3x3 submatrix to matrix at specified row and column
        /// </summary>
        /// <param name="values">3x3 submatrix to add, row-major</param>
        /// <param name="row">Row.</param>
        /// <param name="column">Column.</param>
        public void AddToLHS(double[,] values, int row, int column)
        {
            Debug.Assert(values.GetLength(0) == 3);
            Debug.Assert(values.GetLength(1) == 3);
            int offset = csrd[row, column];
            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 3; c++)
                    vals[offset + c * 3 + r] += values[r, c];
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
