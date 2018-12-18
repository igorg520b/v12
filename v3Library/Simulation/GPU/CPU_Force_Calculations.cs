using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace icFlow
{
    class CPU_Force_Calculations
    {
        public MeshCollection mc;
        public LinearSystem linearSystem;
        public ModelPrms prms;
        public FrameInfo cf;
        Stopwatch sw = new Stopwatch();


        public void AssembleElemsAndCZs()
        {
            sw.Restart();
            sw.Stop();
            cf.KerElemForce += sw.ElapsedMilliseconds;
        }

        public void NarrowPhaseCollisionDetection(List<Element> narrowList)
        {
            sw.Restart();
            // kNarrowPhase
            sw.Stop();

            cf.ElT_GPU += sw.ElapsedMilliseconds;
        }

        public void TransferLinearSystem()
        {
        }

        public void TransferUpdatedState()
        {
        }

        }
}