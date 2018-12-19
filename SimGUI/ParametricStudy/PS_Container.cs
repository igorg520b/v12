using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IcyGrains;

namespace icFlow
{
    public class PS_Container
    {
        public BeamParams beamParams = new BeamParams();
        public ModelPrms modelParams = new ModelPrms();
        public string className = "default";
        public string parameterName;
        public double parameterValue;

        public double resultForce, resultFlexuralStrength;
        
        public enum Status { Ready, Running, Paused, Success, Failed };
        public Status status = Status.Ready;

        public PS_Container() { }

        public PS_Container(PS_Container other)
        {
            this.beamParams = new BeamParams(other.beamParams);
            this.modelParams = new ModelPrms(other.modelParams);
        }

        public override string ToString()
        {
            return className;
        }
    }
}
