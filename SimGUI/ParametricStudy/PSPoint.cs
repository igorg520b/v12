using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IcyGrains;

namespace icFlow
{
    public class PSPoint
    {
        public BeamParams beamParams = new BeamParams();
        public ModelPrms modelParams = new ModelPrms();
        public string className = "c1";
        public string parameterName;
        public double parameterValue;

        public double resultForce, resultFlexuralStrength;
        
        public enum Status { Ready, Running, Paused, Success, Failed };
        public Status status = Status.Ready;

        public PSPoint() { }

        public PSPoint(PSPoint other)
        {
            this.beamParams = new BeamParams(other.beamParams);
            this.modelParams = new ModelPrms(other.modelParams);
        }

        public override string ToString()
        {
            string formatted = parameterValue.ToString("0.00e0");
            return $"{className} {formatted} {status}";
        }
    }
}
