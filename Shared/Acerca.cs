using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DevO2.Shared
{
    internal partial class Acerca : Form
    {
        public Acerca()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Acerca_Load(object sender, EventArgs e)
        {
            lblVersion.Text += Application.ProductVersion;
        }
    }
}
