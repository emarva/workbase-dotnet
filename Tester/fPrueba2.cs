using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tester
{
    public partial class fPrueba2 : Form
    {
        public fPrueba2()
        {
            InitializeComponent();

            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Text = "Nuevo titulo 1";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Text = "Orden de trabajo [11818]";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
