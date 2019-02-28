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
    public class BeamParams
    {
        #region beam
        public enum BeamType { Unknown, LBeam, Plain, Plain2 };

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
            XmlSerializer xs = new XmlSerializer(typeof(BeamParams));
            xs.Serialize(sw, this);
            sw.Close();
            Trace.WriteLine("saved RenderParams");
        }

        public static BeamParams Load()
        {
            if (!File.Exists(fileName))
            {
                Trace.WriteLine("using default RenderPrms");
                return new BeamParams(); // use defaults
            }

            Stream str = File.OpenRead(fileName);
            XmlSerializer xs = new XmlSerializer(typeof(BeamParams));
            try
            {
                Trace.WriteLine("attempting to load RenderPrms");

                BeamParams prms = (BeamParams)xs.Deserialize(str);
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
                return new BeamParams();
            }
        }

        #endregion

        #region copy constructor

        public BeamParams() { }

        public BeamParams(BeamParams other)
        {
            this.type = other.type;
            this.name = other.name;
            this.beamA = other.beamA;
            this.beamB = other.beamB;
            this.beamL1 = other.beamL1;
            this.beamL2 = other.beamL2;
            this.beamGap = other.beamGap;
            this.beamMargin = other.beamMargin;
            this.beamThickness = other.beamThickness;
            this.CharacteristicLengthMax = other.CharacteristicLengthMax;
            this.RefinementMultiplier = other.RefinementMultiplier;
            this.IndenterSize = other.IndenterSize;
        }

        #endregion

        #region presets

        public void PresetPlain(int presetNumber)
        {
            type = BeamParams.BeamType.Plain;
            beamGap = 0.1;
            beamMargin = 0.5;
            CharacteristicLengthMax = 0.2;
            RefinementMultiplier = 0.065;

            switch (presetNumber)
            {
                case 1:
                    name = "P1";
                    beamL2 = 1.97;
                    beamA = 0.64;
                    beamThickness = 0.23;
                    break;
                case 2:
                    name = "P2";
                    beamL2 = 1.27;
                    beamA = 0.58;
                    beamThickness = 0.22;
                    break;
            }
        }

        public void PresetL(int preset)
        {
            beamGap = 0.1;
            beamMargin = 0.35;
            type = BeamParams.BeamType.LBeam;
            CharacteristicLengthMax = 0.6;
            RefinementMultiplier = 0.085;
            switch (preset)
            {
                case 1:
                    name = "L1";
                    beamL1 = 1.05;
                    beamL2 = 1.95;
                    beamA = 0.55;
                    beamB = 0.55;
                    beamThickness = 0.56;
                    break;
                case 2:
                    name = "L2";
                    beamL1 = 1.2;
                    beamL2 = 2.1;
                    beamA = 0.6;
                    beamB = 0.6;
                    beamThickness = 0.53;
                    break;
                case 3:
                    name = "L3";
                    beamL1 = 1.2;
                    beamL2 = 2.08;
                    beamA = 0.6;
                    beamB = 0.65;
                    beamThickness = 0.6;
                    break;
                case 7:
                    name = "L7";
                    beamL1 = 1.3;
                    beamL2 = 2;
                    beamA = 0.62;
                    beamB = 0.65;
                    beamThickness = 0.59;
                    break;
                case 8:
                    name = "L8";
                    beamL1 = 1.15;
                    beamL2 = 1.45;
                    beamA = 0.55;
                    beamB = 0.6;
                    beamThickness = 0.59;
                    break;
                case 9:
                    name = "L9";
                    beamL1 = 1.57;
                    beamL2 = 1.22;
                    beamA = 0.9;
                    beamB = 0.57;
                    beamThickness = 0.59;
                    break;
            }
        }

        #endregion
    }
}
