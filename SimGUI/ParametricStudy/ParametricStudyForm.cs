using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using System.Xml.Serialization;
using IcyGrains;

namespace icFlow
{
    public partial class ParametricStudyForm : Form
    {
        List<PSPoint> classes = new List<PSPoint>();
        public List<PSPoint> resultingBatch;
        public Form1 mainWindow;


        public ParametricStudyForm()
        {
            InitializeComponent();

            classes.Add(new PSPoint());
            UpdateClassList();
        }

        private void PS_Setup_Load(object sender, EventArgs e)
        {
            panelRun.Visible = false;
            panelSetup.Dock = DockStyle.Fill;
            listBox1.SelectedIndex = 0;
            lbParameters.SelectedIndex = 0;
            btnDefaultP_Click(sender, e);
        }

        void UpdateClassList()
        {
            listBox1.Items.Clear();
            foreach(PSPoint ps in classes)
            {
                listBox1.Items.Add(ps);
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            resultingBatch = new List<PSPoint>();
            PSPoint firstPsc = (PSPoint)listBox1.Items[0];

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
                        foreach(PSPoint psc in classes)
                        {
                            PSPoint current = new PSPoint(psc);
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

                case "ratio":
                    for (int i = 0; i < steps; i++)
                    {
                        double mix = (double)i / (steps - 1);
                        double ratio1 = fromValue * (1 - mix) + toValue * mix;
                        foreach (PSPoint psc in classes)
                        {
                            PSPoint current = new PSPoint(psc);
                            resultingBatch.Add(current);
                            current.modelParams.tau_max = current.modelParams.sigma_max * ratio1;
                            current.className = psc.className;
                            current.parameterName = "ratio";
                            current.parameterValue = ratio1;
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


                case "length":
                    for (int i = 0; i < steps; i++)
                    {
                        double mix = (double)i / (steps - 1);
                        double length1 = fromValue * (1 - mix) + toValue * mix;
                        foreach (PSPoint psc in classes)
                        {
                            PSPoint current = new PSPoint(psc);
                            resultingBatch.Add(current);
                            current.beamParams.beamL2 = length1;
                            current.className = psc.className;
                            current.parameterName = "length";
                            current.parameterValue = length1;
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


                case "width":
                    for (int i = 0; i < steps; i++)
                    {
                        double mix = (double)i / (steps - 1);
                        double width1 = fromValue * (1 - mix) + toValue * mix;
                        foreach (PSPoint psc in classes)
                        {
                            PSPoint current = new PSPoint(psc);
                            resultingBatch.Add(current);
                            current.beamParams.beamA = width1;
                            current.className = psc.className;
                            current.parameterName = "width";
                            current.parameterValue = width1;
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


                case "thickness":
                    for (int i = 0; i < steps; i++)
                    {
                        double mix = (double)i / (steps - 1);
                        double thickness1 = fromValue * (1 - mix) + toValue * mix;
                        foreach (PSPoint psc in classes)
                        {
                            PSPoint current = new PSPoint(psc);
                            resultingBatch.Add(current);
                            current.beamParams.beamThickness = thickness1;
                            current.className = psc.className;
                            current.parameterName = "thickness";
                            current.parameterValue = thickness1;
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


                case "resolution":
                    double initialRatio = firstPsc.beamParams.RefinementMultiplier/ firstPsc.beamParams.CharacteristicLengthMax;
                    for (int i = 0; i < steps; i++)
                    {
                        double mix = (double)i / (steps - 1);
                        double resolution1 = fromValue * (1 - mix) + toValue * mix;
                        foreach (PSPoint psc in classes)
                        {
                            PSPoint current = new PSPoint(psc);
                            resultingBatch.Add(current);
                            current.beamParams.CharacteristicLengthMax = resolution1;
                            current.beamParams.RefinementMultiplier = initialRatio * resolution1;
                            current.className = psc.className;
                            current.parameterName = "resolution";
                            current.parameterValue = resolution1;
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

            UpdateListbox2();




        }

        void UpdateListbox2()
        {
            listBox2.Items.Clear();
            foreach (PSPoint psp in resultingBatch) listBox2.Items.Add(psp);
        }

        private void btnAssignName_Click(object sender, EventArgs e)
        {
            PSPoint psc = (PSPoint)listBox1.SelectedItem;
            if (psc == null) return;
            psc.className = tbClassName.Text;
            UpdateClassList();
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            PSPoint psc = (PSPoint)listBox1.SelectedItem;
            if (psc == null) return;
            PSPoint newpt = new PSPoint(psc);
            classes.Add(newpt);
            newpt.className = $"c{classes.Count}";
            UpdateClassList();
            listBox1.SelectedIndex = 0;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            PSPoint psc = (PSPoint)listBox1.SelectedItem;
            if (psc == null) return;
            if (listBox1.Items.Count == 1) return;
            classes.Remove(psc);
            UpdateClassList();

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSPoint psc = (PSPoint)listBox1.SelectedItem;
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

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSPoint psc = (PSPoint)listBox2.SelectedItem;
            if (psc == null)
            {
                pgModelParams.SelectedObject = null;
                pgBeamParams.SelectedObject = null;
            }
            else
            {
                pgModelParams.SelectedObject = psc.modelParams;
                pgBeamParams.SelectedObject = psc.beamParams;
            }
        }

        private void btnDefaultL_Click(object sender, EventArgs e)
        {
            PSPoint psc = (PSPoint)listBox1.SelectedItem;
            if (psc == null) return;
            psc.beamParams.PresetL(1);
            psc.modelParams.SelectPreset(ModelPrms.ParameterPresets.LBeam);
            pgBeamParams.Refresh();
            pgModelParams.Refresh();
        }

        private void btnDefaultP_Click(object sender, EventArgs e)
        {
            PSPoint psc = (PSPoint)listBox1.SelectedItem;
            if (psc == null) return;
            psc.beamParams.PresetPlain(1);
            psc.modelParams.SelectPreset(ModelPrms.ParameterPresets.PlainBeam);
            pgBeamParams.Refresh();
            pgModelParams.Refresh();
        }


        void UpdateChart()
        {
            Dictionary<string, List<(double, double)>> pointData = new Dictionary<string, List<(double, double)>>();
            foreach (PSPoint psp in classes) pointData.Add(psp.className, new List<(double, double)>());

            foreach(PSPoint psp in resultingBatch)
            {
                if(psp.status == PSPoint.Status.Success)
                {
                    double X = psp.parameterValue;
                    double Y = psp.beamParams.type == BeamParams.BeamType.LBeam ? psp.resultForce : psp.resultFlexuralStrength;
                    pointData[psp.className].Add((X, Y));
                }
            }

            foreach (PSPoint psp in classes)
            {
                List<(double, double)> lst = pointData[psp.className];
                double[] xValues = new double[lst.Count];
                double[] yValues = new double[lst.Count];
                for(int i = 0; i < lst.Count; i++)
                {
                    xValues[i] = lst[i].Item1;
                    yValues[i] = lst[i].Item2;
                }
                chart1.Series[psp.className].Points.DataBindXY(xValues, yValues);
            }
        }

        private void tsbExport_Click(object sender, EventArgs e)
        {
            // export CSV
            string CSVFolder = $"_sims\\{tbStudyName.Text}\\_CSV";
            if (!Directory.Exists(CSVFolder)) Directory.CreateDirectory(CSVFolder);
            string CSVPath = CSVFolder + "\\parametric_study.csv";

            StreamWriter sw = new StreamWriter(File.Create(CSVPath));
            sw.WriteLine($"class, BeamType, parameter, XValue, Force, FlexuralStrength");

            foreach (PSPoint psp in resultingBatch)
            {
                if (psp.status == PSPoint.Status.Success)
                    sw.WriteLine($"{psp.className},{psp.beamParams.type}, {psp.parameterName}, {psp.parameterValue}, {psp.resultForce}, {psp.resultFlexuralStrength}");
            }

            sw.Close();
        }

        private void lbParameters_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(lbParameters.SelectedItem.ToString())
            {
                case "length":
                    tbFrom.Text = "1.3";
                    tbTo.Text = "2.5";
                    break;
                case "sigma":
                    tbFrom.Text = "100000";
                    tbTo.Text = "150000";
                    break;
                case "ratio":
                    tbFrom.Text = "1";
                    tbTo.Text = "2";
                    break;
                case "width":
                    tbFrom.Text = "0.4";
                    tbTo.Text = "0.8";
                    break;
                case "thickness":
                    tbFrom.Text = "0.15";
                    tbTo.Text = "0.5";
                    break;
                case "resolution":
                    tbFrom.Text = "0.015";
                    tbTo.Text = "0.025";
                    break;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            resultingBatch.Clear();
            UpdateListbox2();
        }

        private async void btnInitialize_Click(object sender, EventArgs e)
        {
            tss1.Text = "initializing";
            foreach (PSPoint psc in resultingBatch)
            {
                GrainTool2 gt2 = new GrainTool2();
                if (psc.beamParams.type == BeamParams.BeamType.LBeam)
                    await Task.Run(() => gt2.LBeamGeneration(psc.beamParams));
                else if (psc.beamParams.type == BeamParams.BeamType.Plain)
                    await Task.Run(() => gt2.PlainBeamGeneration(psc.beamParams));
                else throw new Exception("beam type not set");

                // save to memory stream
                Stream strBeam = new MemoryStream(5000000);
                Stream strIndenter = new MemoryStream(5000000);
                gt2.tmesh.SaveMsh2(strBeam);
                strBeam.Seek(0, SeekOrigin.Begin);

                gt2.indenter_mesh.SaveMsh2(strIndenter);
                strIndenter.Seek(0, SeekOrigin.Begin);

                mainWindow.SetUpBeamSimulation(strBeam, strIndenter, psc.beamParams, psc.modelParams);
                mainWindow.model3.SaveSimulationInitialState();

            }
            tss1.Text = "done";

            SaveStudy();

            // reload from that file
  
        }

        void SaveStudy()
        {
            // save study as XML file into the study folder
            Stream strXML = File.Create($"_sims//{tbStudyName.Text}//study.xml");
            StreamWriter sw = new StreamWriter(strXML);
            XmlSerializer xs = new XmlSerializer(typeof(List<PSPoint>));
            xs.Serialize(sw, resultingBatch);
            sw.Close();
        }

        void LoadStudy(Stream str)
        {

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            // set up simulations for the study
            /*
panelSetup.Visible = false;
panelRun.Visible = true;
panelRun.Dock = DockStyle.Fill;

// populate listbox
foreach (PSPoint psp in resultingBatch) lbSimulations.Items.Add(psp);

// prepare series
foreach (PSPoint psp in classes)
{
    Series s = new Series();
    s.ChartArea = "ChartArea1";
    s.ChartType = SeriesChartType.Point;
    s.Legend = "Legend1";
    s.MarkerSize = 7;
    s.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
    s.Name = psp.className;
    chart1.Series.Add(s);
}
*/
        }

        #region main loop

        bool running = false;
        bool requestToPause = false;
        private async void tsbRun_Click(object sender, EventArgs e)
        {
            if (running == true)
            {
                tsbRun.Enabled = false;
                tsbRun.Text = "Pausing";
                requestToPause = true;
                return;
            }

            tsbRun.Text = "Pause";
            requestToPause = false;
            running = true;

            foreach (PSPoint psc in resultingBatch)
            {
                // check the state

                // if "Success, Failed" then skip
                if (psc.status == PSPoint.Status.Success ||
                    psc.status == PSPoint.Status.Failed) continue;

                // if "Ready", then set up (using mainWindow)
                if (psc.status == PSPoint.Status.Ready)
                {
                    GrainTool2 gt2 = new GrainTool2();
                    if (psc.beamParams.type == BeamParams.BeamType.LBeam)
                        await Task.Run(() => gt2.LBeamGeneration(psc.beamParams));
                    else if (psc.beamParams.type == BeamParams.BeamType.Plain)
                        await Task.Run(() => gt2.PlainBeamGeneration(psc.beamParams));
                    else throw new Exception("beam type not set");

                    // save to memory stream
                    Stream strBeam = new MemoryStream(5000000);
                    Stream strIndenter = new MemoryStream(5000000);
                    gt2.tmesh.SaveMsh2(strBeam);
                    strBeam.Seek(0, SeekOrigin.Begin);

                    gt2.indenter_mesh.SaveMsh2(strIndenter);
                    strIndenter.Seek(0, SeekOrigin.Begin);

                    mainWindow.SetUpBeamSimulation(strBeam, strIndenter, psc.beamParams, psc.modelParams);
                }
                else if (psc.status == PSPoint.Status.Paused)
                {
                    // if "Paused", then load from existing file
                    throw new NotImplementedException();

                }
                else throw new Exception("incorrect psc status");


                // run until completion 
                psc.status = PSPoint.Status.Running;
                lbSimulations.Items.Clear();
                foreach (PSPoint psp in this.resultingBatch) lbSimulations.Items.Add(psp);

                //=============
                mainWindow.tsbPreviewMode.Checked = false;
                mainWindow.tssStatus.Text = "Running";
                mainWindow.trackBar1.Enabled = false;

                bool completed = false;
                do
                {
                    await Task.Run(() => mainWindow.model3.Step());
                    if (mainWindow.model3.cf.StepNumber >= mainWindow.model3.prms.MaxSteps ||
                        mainWindow.model3.prms.DetectFracture && mainWindow.model3.cf.fractureDetected)
                        completed = true;

                    // report progress
                    mainWindow.glControl1.Invalidate();
                    mainWindow.tnCurrentFrame.Tag = mainWindow.model3.cf;
                    if (mainWindow.treeView1.SelectedNode == mainWindow.tnCurrentFrame)
                    {
                        mainWindow.pg.SelectedObject = mainWindow.model3.cf;
                        mainWindow.pg.Refresh();
                    }
                    if (mainWindow.model3.cf != null) mainWindow.tssCurrentFrame.Text = $"{mainWindow.model3.cf.StepNumber}-{mainWindow.model3.cf.IterationsPerformed}-{mainWindow.model3.cf.TimeScaleFactor}";

                } while (!(requestToPause || completed));

                //=============


                if (requestToPause)
                {
                    psc.status = PSPoint.Status.Paused;
                    requestToPause = false;
                    break;
                }
                else if (completed)
                {
                    psc.status = PSPoint.Status.Success;

                    // generate frame summary
                    FrameInfo.FrameSummary smr = new FrameInfo.FrameSummary(mainWindow.model3.allFrames, mainWindow.model3.prms);
                    psc.resultFlexuralStrength = smr.FlexuralStrength;
                    psc.resultForce = smr.MaxForce;

                    // update chart (!)
                    UpdateChart();
                }
                lbSimulations.Items.Clear();
                foreach (PSPoint psp in this.resultingBatch) lbSimulations.Items.Add(psp);
            }

            running = false;
            tsbRun.Text = "Run";
            tsbRun.Enabled = true;
            mainWindow.tssCurrentFrame.Text = "-";
            mainWindow.tssStatus.Text = "done";
        }
        #endregion




    }
}
