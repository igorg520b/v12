using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace icFlow
{
    static class CPU_Functionality
    {
        //        public MeshCollection mc;
        //        public LinearSystem linearSystem;
        //        public ModelPrms prms;
        //        public FrameInfo cf;

        public static int[] NarrowPhaseCollisionDetection(List<Element> narrowList, 
            ModelPrms prms, ref FrameInfo cf)
        {
            if (prms.CollisionScheme == ModelPrms.CollisionSchemes.None || narrowList.Count == 0) { cf.nCollisions = 0; return null; }

            int nTetra = narrowList.Count;
            Debug.Assert(nTetra % 2 == 0, "narrowList size is not even");


            /*
            // detemine the memory needed
            int nPairs = nTetra / 2;
            if (c_itet == null || c_itet.Length < nTetra) c_itet = new int[nTetra * 2];
            if(g_itet == null) g_itet = new CudaDeviceVariable<int>(nTetra * 2);
            else if(g_itet.Size < nTetra) { g_itet.Dispose(); g_itet = new CudaDeviceVariable<int>(nTetra * 2); }

            for (int i = 0; i < nTetra; i++) c_itet[i] = narrowList[i].globalElementId;

            g_itet.CopyToDevice(c_itet, 0, 0, nTetra*sizeof(int));
            kNarrowPhase.GridDimensions = new dim3(grid(nPairs), 1, 1);
            kNarrowPhase.Run(nPairs, g_dn.DevicePointer, g_ie.DevicePointer, g_itet.DevicePointer, el_all_stride, nd_stride);
            g_itet.CopyToHost(c_itet, 0, 0, nTetra * sizeof(int));

            // Tuple ( node# inside element, which element)
            NL2set.Clear();
            for (int i = 0; i < nPairs; i++) 
                if (c_itet[i * 2] != 0)
                {
                    int bits = c_itet[i * 2];
                    if ((bits & 1) != 0) NL2set.Add(new Tuple<int, int>(narrowList[i*2+1].vrts[0].globalNodeId, narrowList[i*2].globalElementId));
                    if ((bits & 2) != 0) NL2set.Add(new Tuple<int, int>(narrowList[i * 2 + 1].vrts[1].globalNodeId, narrowList[i * 2].globalElementId));
                    if ((bits & 4) != 0) NL2set.Add(new Tuple<int, int>(narrowList[i * 2 + 1].vrts[2].globalNodeId, narrowList[i * 2].globalElementId));
                    if ((bits & 8) != 0) NL2set.Add(new Tuple<int, int>(narrowList[i * 2 + 1].vrts[3].globalNodeId, narrowList[i * 2].globalElementId));

                    if ((bits & 16) != 0) NL2set.Add(new Tuple<int, int>(narrowList[i * 2 ].vrts[0].globalNodeId, narrowList[i * 2+1].globalElementId));
                    if ((bits & 32) != 0) NL2set.Add(new Tuple<int, int>(narrowList[i * 2 ].vrts[1].globalNodeId, narrowList[i * 2+1].globalElementId));
                    if ((bits & 64) != 0) NL2set.Add(new Tuple<int, int>(narrowList[i * 2 ].vrts[2].globalNodeId, narrowList[i * 2+1].globalElementId));
                    if ((bits & 128) != 0) NL2set.Add(new Tuple<int, int>(narrowList[i * 2 ].vrts[3].globalNodeId, narrowList[i * 2+1].globalElementId));
                }

            // identify closest face
            // detemine the memory needed
            nPairs = NL2set.Count;
            if(nPairs == 0) { nImpacts = 0; return; }
            tet_stride = grid(nPairs) * block_size;
            if (c_itet.Length < tet_stride*4) c_itet = new int[tet_stride * 6];
            if (g_itet.Size < tet_stride * 4) { g_itet.Dispose(); g_itet = new CudaDeviceVariable<int>(tet_stride * 6); }

            int count = 0;
            foreach(Tuple<int,int> nodeElemPair in NL2set)
            {
                c_itet[count] = nodeElemPair.Item1;
                c_itet[count + tet_stride] = nodeElemPair.Item2;
                count++;
            }
            g_itet.CopyToDevice(c_itet, 0, 0, sizeof(int) * tet_stride * 2);
            kFindClosestFace.GridDimensions = new dim3(grid(nPairs), 1, 1);

            // result written to g_itet
            kFindClosestFace.Run(nPairs, tet_stride, g_itet.DevicePointer,
                g_dn.DevicePointer, g_ie.DevicePointer, g_ifc.DevicePointer, 
                nd_stride, el_all_stride, fc_stride);

            g_itet.CopyToHost(c_itet, 0, 0, sizeof(int) * tet_stride * 4);
            cf.nCollisions = nImpacts = count;
            // at this point c_itet[] contains strided (p_nd - f_nd1 -  f_nd2 -  f_nd3) sequences (global node id)
            mc.TransferFromAnotherArray(c_itet, nImpacts, tet_stride);

            sw.Stop();
            cf.ElT_GPU += sw.ElapsedMilliseconds;

            */
            throw new NotImplementedException();
//                        int[] result;
//            return result;

        }
    }
}