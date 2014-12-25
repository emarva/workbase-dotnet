using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Demo
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }

        private void dataLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fDataLayer dl = new fDataLayer();

            if (!tabControlEx1.ExisteFormulario(dl, true))
                tabControlEx1.AgregarFormulario(dl);
        }

        private void uIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fUI ui = new fUI();

            if (!tabControlEx1.ExisteFormulario(ui, true))
                tabControlEx1.AgregarFormulario(ui);
        }

        private void googleEarthViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fGoogleEarthViewer gev = new fGoogleEarthViewer();

            if (!tabControlEx1.ExisteFormulario(gev, true))
                tabControlEx1.AgregarFormulario(gev);
        }
    }
}
