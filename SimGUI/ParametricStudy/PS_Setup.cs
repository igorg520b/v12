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

        public PS_Setup()
        {
            InitializeComponent();

            classes.Add(new PS_Container());
            UpdateClassList();
        }

        private void PS_Setup_Load(object sender, EventArgs e)
        {

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
            DialogResult = DialogResult.OK;
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
    }
}
