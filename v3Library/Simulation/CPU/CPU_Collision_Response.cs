using System;
namespace icFlow
{
    public static class CPU_Collision_Response
    {
        class CPResult
        {
            public double[] fi = new double[12];
            public double[,] dfi = new double[12, 12];
        }


        static CPResult OneCollision(Node nd, Face fc)
        {
            CPResult res = new CPResult();

            return res;
        }


        public static void CollisionResponse((Node, Face)[] cp, LinearSystem ls, ref FrameInfo cf, MeshCollection mc, ModelPrms prms)
        {
            int nCollisions = cp.Length;
            cf.nCollisions = nCollisions;
            CPResult[] cpr = new CPResult[nCollisions];
        }

        /*
         * 
         * 
// ker_ElementElasticityForce compute and distributes the stiffness matrix and the rhs per element
// h: timestep
extern "C" __global__ void kCollisionResponseForce(
    const int *icr, double *dn, const double h,
    double *global_matrix, double *global_rhs,
    const int nImpacts, const int cr_stride, const int nd_stride,
    const double k, const double distanceEpsilon)
{
    int idx = threadIdx.x + blockIdx.x * blockDim.x;
    if (idx >= nImpacts) return;

    // filter out rigid-rigid collisions
    const int *pcsr = &icr[idx + cr_stride * 4];
    int ndp[4];
    for (int i = 0; i < 4; i++) ndp[i] = pcsr[cr_stride * (16 + i)];
    if (ndp[0] < 0 && ndp[1] < 0 && ndp[2] < 0 && ndp[3] < 0) return;

    // transfer values from buffer to local arrays
    double xc[12];
    // retrieve node coordinates from global memory

    int nid[4];
    for (int i = 0; i < 4; i++) {           // node
        int nn = icr[idx + cr_stride * i]; // node id
        nid[i] = nn;
        for (int j = 0; j < 3; j++)     // coordinate
        {
            int idx1 = j + i * 3;
            xc[idx1] = dn[nn + nd_stride*(X_CURRENT_OFFSET + j)];
        }
    }

    double w[3] = {};
    double sqrdistd[12] = {};
    double sqrdistdd[12][12] = {};

    double output_s, output_t;

    double dsq = pt(xc, sqrdistd, sqrdistdd, output_s, output_t);

    w[1] = output_s; w[2] = output_t; w[0] = 1 - (output_s + output_t);

    if (dsq > distanceEpsilon*distanceEpsilon) {
        double fx, fy, fz;
        fx = k * 0.5 * sqrdistd[0];
        fy = k * 0.5 * sqrdistd[1];
        fz = k * 0.5 * sqrdistd[2];

        double fi[12];
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

        double dfij[12][12];
        for (int i = 0; i < 12; i++)
            for (int j = i; j < 12; j++)
                dfij[i][j] = dfij[j][i] = k * sqrdistdd[i][j] / 2;

        // distribute computed forces to fx fy fz (for analysis and visualization)
        for (int i = 0; i < 4; i++) {           // node
            int nn = nid[i]; // pre-fetched node id
            for (int j = 0; j < 3; j++)     // coordinate
                atomicAdd2(&dn[nn + nd_stride*(F_OFFSET + j)], fi[i * 3 + j]);
        }

        // distribute result
        AssembleBCSR12(pcsr, global_matrix, global_rhs, cr_stride, dfij, fi);
    }
         * 
         * 
                public void collisionResponse(bool nonSymmetric)
        {
            if (nImpacts == 0) return;
            sw.Restart();
            CSRDictionary csrd = linearSystem.csrd;
            
            // allocate memory
            int cr_stride = grid(nImpacts) * block_size;
            int size_icr = cr_stride * 28;
            if (c_icr == null || c_icr.Length < size_icr)
            {
                if (g_icr != null) g_icr.Dispose();
                c_icr = new int[size_icr*2];
                g_icr = new CudaDeviceVariable<int>(size_icr*2);
            }

            const int pcsr_offset = 4;
            // populate and transfer to GPU
            Parallel.For(0, nImpacts, i_im => {

                for (int i = 0; i < 4; i++)
                {
                    Node ni = mc.allNodes[c_itet[i_im + i*tet_stride]];
                    c_icr[i_im + cr_stride * i] = ni.globalNodeId;
                    c_icr[i_im + cr_stride * (pcsr_offset + 16 + i)] = ni.anchored ? -1 : ni.altId;
//                    c_icz[i_im + cz_stride * (pcsr_offset + 20 + i)] = -1;    // count entries in the row (used for CSR format)

                    for (int j = 0; j < 4; j++)
                    {
                        int pcsr_ij;
                        Node nj = mc.allNodes[c_itet[i_im + j * tet_stride]];
                        if (!ni.anchored && !nj.anchored && (nonSymmetric || nj.altId >= ni.altId))
                            pcsr_ij = csrd[ni.altId, nj.altId];
                        else pcsr_ij = -1; // ni is anchored => ignore
                        c_icr[i_im + cr_stride * (pcsr_offset + (i * 4 + j))] = pcsr_ij;
                    }
                }
            });
            g_icr.CopyToDevice(c_icr, 0, 0, sizeof(int) * cr_stride * 28);

            // set grid size, execute kernel
            kCollisionResponseForce.GridDimensions = new dim3(grid(nImpacts), 1, 1);

            kCollisionResponseForce.Run(g_icr.DevicePointer, g_dn.DevicePointer, cf.TimeStep,
                g_dvals.DevicePointer, g_drhs.DevicePointer, nImpacts, cr_stride, nd_stride, prms.penaltyK, 
                prms.DistanceEpsilon);
            
            sw.Stop();
            cf.CollForce += sw.ElapsedMilliseconds;
        }
        */
    }
}
