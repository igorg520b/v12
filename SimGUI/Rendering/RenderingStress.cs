using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using System.Drawing;
using System.Diagnostics;

namespace icFlow.Rendering
{
    public static class RenderingStress
    {


        // render exposed, non-created surfaces with transparency
        public static (double,double,double) RenderStress(this List<Face> faces,
            RenderPrms.StressType stressType, int palette)
        {
            double min, max;
            if(stressType == RenderPrms.StressType.FirstP)
            {
                max = faces.Max(f => f.elem.principal_stresses[0]);
                min = faces.Min(f => f.elem.principal_stresses[0]);
            } else if(stressType == RenderPrms.StressType.XX)
            {
                max = faces.Max(f => f.elem.stress[0]);
                min = faces.Min(f => f.elem.stress[0]);
            } else throw new Exception("drawing unknown stress type");

            // make min and max a round value
            int orderOfMagnitude = (int)Math.Ceiling(Math.Log10(max - min));
            double step = Math.Pow(10, orderOfMagnitude) / 20;
            max = ((int)Math.Ceiling(max / step)) * step;
            min = ((int)Math.Floor(min / step)) * step;

            GL.Disable(EnableCap.Blend);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.DepthMask(true);

            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(Color.Green);
            foreach (Face f in faces)
            {
                if (f.exposed && !f.created)
                {
                    if (f.elem == null) continue;
                    double stressValue = 0;
                    if (stressType == RenderPrms.StressType.FirstP)
                        stressValue = f.elem.principal_stresses[0];
                    else if (stressType == RenderPrms.StressType.XX)
                        stressValue = f.elem.stress[0];
                    Color col = MC.c3(stressValue, min, max, palette);
                    GL.Color3(col);
                    for (int i = 0; i < 3; i++) GL.Vertex3(f.vrts[i].ToVec());
                }
            }
            GL.End();
            return (min, max, step);
        }

        public static Vector3d ToVec(this Node nd)
        {
            return new Vector3d(nd.cx, nd.cy, nd.cz);
        }
        public static Vector3d ToVec0(this Node nd)
        {
            return new Vector3d(nd.x0, nd.y0, nd.z0);
        }

    }
}
