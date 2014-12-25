using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevO2.UI;

namespace Demo
{
    public partial class fGoogleEarthViewer : Form
    {
        public fGoogleEarthViewer()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            googleEarthViewer1.Cargar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (googleEarthViewer1.Cargado)
            {
                double lat = -33.4253598;
                double lon = -70.5664659;

                googleEarthViewer1.EliminarMarca("marca1");
                GEKmlPlacemark marca = new GEKmlPlacemark();
                marca.Nombre = "marca1";
                marca.Latitud = lat;
                marca.Longitud = lon;
                googleEarthViewer1.AgregarMarca(marca);
                googleEarthViewer1.LookAt(lat, lon, 0, GEKmlAltitudeMode.RelativeToGround, 0, 0, 52000);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            googleEarthViewer1.Refresh();
        }
    }
}
