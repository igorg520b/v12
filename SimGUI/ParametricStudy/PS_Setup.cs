using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace icFlow
{
    public partial class PS_Setup : Form
    {
        List<PS_Container> classes = new List<PS_Container>();
        public List<PS_Container> resultingBatch;


        public PS_Setup()
        {
            InitializeComponent();

            classes.Add(new PS_Container());
            UpdateClassList();
        }

        private void PS_Setup_Load(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = 0;
            lbParameters.SelectedIndex = 0;
            btnDefaultP_Click(sender, e);
        }

        void UpdateClassList()
        {
            listBox1.Items.Clear();
            foreach(PS_Container ps in classes)
            {
                listBox1.Items.Add(ps);
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            resultingBatch = new List<PS_Container>();
            PS_Container firstPsc = (PS_Container)listBox1.Items[0];

            string parameter = (string)lbParameters.SelectedItem;
            if (!double.TryParse(tbFrom.Text, out double fromValue)) return;
            if (!double.TryParse(tbTo.Text, out double toValue)) return;
            int steps = (int)nudSteps.Value;

            switch (parameter)
            {
                case "sigma":
                    double ratio = firstPsc.modelParams.tau_max / firstPsc.modelParams.sigma_max;
                    for(int i=0;i<steps;i++)
                    {
                        double mix = (double)i / (steps - 1);
                        double sigma = fromValue * (1 - mix) + toValue * mix;
                        foreach(PS_Container psc in classes)
                        {
                            PS_Container current = new PS_Container(psc);
                            resultingBatch.Add(current);
                            current.modelParams.sigma_max = sigma;
                            current.modelParams.tau_max = sigma * ratio;
                            current.className = psc.className;
                            current.parameterName = "sigma";
                            current.parameterValue = sigma;
                            current.modelParams.name = $"{tbStudyName.Text}/{psc.className}-{i}";

                            if (psc.beamParams.type == IcyGrains.BeamParams.BeamType.Plain)
                            {
                                current.modelParams.BeamLength = current.beamParams.beamL2;
                                current.modelParams.BeamThickness = current.beamParams.beamThickness;
                                current.modelParams.BeamWidth = current.beamParams.beamA;
                            }
                        }
                    }


                    break;
                default:
                    return;
            }
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnAssignName_Click(object sender, EventArgs e)
        {
            PS_Container psc = (PS_Container)listBox1.SelectedItem;
            if (psc == null) return;
            psc.className = tbClassName.Text;
            UpdateClassList();
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            PS_Container psc = (PS_Container)listBox1.SelectedItem;
            if (psc == null) return;
            classes.Add(new PS_Container(psc));
            UpdateClassList();

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            PS_Container psc = (PS_Container)listBox1.SelectedItem;
            if (psc == null) return;
            if (listBox1.Items.Count == 1) return;
            classes.Remove(psc);
            UpdateClassList();

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PS_Container psc = (PS_Container)listBox1.SelectedItem;
            if (psc == null)
            {
                pgModelParams.SelectedObject = null;
                pgBeamParams.SelectedObject = null;
            } else
            {
                pgModelParams.SelectedObject = psc.modelParams;
                pgBeamParams.SelectedObject = psc.beamParams;
            }

        }

        private void btnDefaultL_Click(object sender, EventArgs e)
        {
            PS_Container psc = (PS_Container)listBox1.SelectedItem;
            if (psc == null) return;
            psc.beamParams.PresetL(1);
            psc.modelParams.SelectPreset(ModelPrms.ParameterPresets.LBeam);
            pgBeamParams.Refresh();
            pgModelParams.Refresh();
        }

        private void btnDefaultP_Click(object sender, EventArgs e)
        {
            PS_Container psc = (PS_Container)listBox1.SelectedItem;
            if (psc == null) return;
            psc.beamParams.PresetPlain(1);
            psc.modelParams.SelectPreset(ModelPrms.ParameterPresets.PlainBeam);
            pgBeamParams.Refresh();
            pgModelParams.Refresh();
        }
    }
}
