using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Diagnostics;

namespace IcyGrains
{
    public class AllParams
    {
        #region beam
        public enum BeamType { Unknown, LBeam, Plain };

        [Category("Beam")]
        public BeamType type { get; set; } = BeamType.Unknown;

        [Category("Beam")]
        public string name { get; set; } = "default";

        [Category("Beam")]
        public double beamA { get; set; } = 0.55; // a

        [Category("Beam")]
        public double beamB { get; set; } = 0.55; // b

        [Category("Beam")]
        public double beamL1 { get; set; } = 1.05; // l1

        [Category("Beam")]
        public double beamL2 { get; set; } = 1.95; // l2

        [Category("Beam")]
        public double beamGap { get; set; } = 0.1; // c

        [Category("Beam")]
        public double beamMargin { get; set; } = 0.35; // d

        [Category("Beam")]
        public double beamThickness { get; set; } = 0.56; // h

        [Category("Beam")]
        public double CharacteristicLengthMax { get; set; } = 0.2;

        [Category("Beam")]
        public double CharacteristicLengthIndenter { get; set; } = 0.02;


        [Category("Beam")]
        public double RefinementMultiplier { get; set; } = 0.07;


        [Category("Indenter")]
        public double IndenterSize { get; set; } = 0.15;
        #endregion


        #region view angle, offset and scale

        public float theta = 0;
        public float phi = 0;           // view angle

        public float dx = 0;
        public float dy = 0;           // rendering offset

        public double scale2D = 0.5;
        #endregion


        #region perspective

        [Category("Perspective")]
        [Description("In degrees")]
        public double fovY { get; set; } = 20;

        [Category("Perspective")]
        public double zNear { get; set; } = 0.1;

        [Category("Perspective")]
        public double zFar { get; set; } = 10000;

        [Category("Perspective")]
        public double zOffset { get; set; } = 15;

        #endregion

        #region light

        [Category("Light")]
        public bool Light0 { get; set; } = true;
        [Category("Light")]
        public bool Light1 { get; set; } = true;
        [Category("Light")]
        public bool Light2 { get; set; } = true;


        [Category("Light")]
        public float L0x { get; set; } = -0.5f;
        [Category("Light")]
        public float L0y { get; set; } = 0.5f;
        [Category("Light")]
        public float L0z { get; set; } = 0.7f;
        [Category("Light")]
        public float L0intensity { get; set; } = 0.7f;

        [Category("Light")]
        public float L1x { get; set; } = -0.5f;
        [Category("Light")]
        public float L1y { get; set; } = 0;
        [Category("Light")]
        public float L1z { get; set; } = -0.4f;
        [Category("Light")]
        public float L1intensity { get; set; } = 0.7f;

        [Category("Light")]
        public float L2x { get; set; } = 1;
        [Category("Light")]
        public float L2y { get; set; } = 0;
        [Category("Light")]
        public float L2z { get; set; } = -1;
        [Category("Light")]
        public float L2intensity { get; set; } = 0.5f;
        #endregion

        #region colors
        [Category("Colors")]
        [XmlIgnore]
        public Color GrainsColor { get; set; } = Color.Lime;
        public string GrainsColorHtml;


        [Category("Colors")]
        [XmlIgnore]
        public Color IcebergColor { get; set; } = Color.LightCyan;
        public string IcebergColorHtml;

        public double transparency = 0.25;

        #endregion

        #region load/save

        const string fileName = "RenderParams";
        public void Save()
        {

            Stream str;
            try
            {
                str = File.Create(fileName);
            }
            catch
            {
                Trace.WriteLine("could not write RenderParams");
                return;
            }

            GrainsColorHtml = ColorTranslator.ToHtml(GrainsColor);
            IcebergColorHtml = ColorTranslator.ToHtml(IcebergColor);

            StreamWriter sw = new StreamWriter(str);
            XmlSerializer xs = new XmlSerializer(typeof(AllParams));
            xs.Serialize(sw, this);
            sw.Close();
            Trace.WriteLine("saved RenderParams");
        }

        public static AllParams Load()
        {
            if (!File.Exists(fileName))
            {
                Trace.WriteLine("using default RenderPrms");
                return new AllParams(); // use defaults
            }

            Stream str = File.OpenRead(fileName);
            XmlSerializer xs = new XmlSerializer(typeof(AllParams));
            try
            {
                Trace.WriteLine("attempting to load RenderPrms");

                AllParams prms = (AllParams)xs.Deserialize(str);
                str.Close();
                prms.GrainsColor = ColorTranslator.FromHtml(prms.GrainsColorHtml);
                prms.IcebergColor = ColorTranslator.FromHtml(prms.IcebergColorHtml);

                return prms;
            }
            catch (Exception e)
            {
                Trace.WriteLine("using default RenderPrms because exception occurred:" + e.Message);
                str.Close();
                File.Delete(fileName);
                return new AllParams();
            }
        }

        #endregion

    }
}
