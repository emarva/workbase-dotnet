using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Demo
{
    public partial class fUI : Form
    {
        public fUI()
        {
            InitializeComponent();
        }

        private void fUI_Load(object sender, EventArgs e)
        {
            tabControlEx1.AgregarFormulario(new fFormTabIcono());
        }
    }
}
