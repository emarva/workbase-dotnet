using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Modulo1
{
    public partial class Formulario : Form
    {
        public Formulario()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("EL MODULO SE CARGO BIEN");
        }
    }
}
