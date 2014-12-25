using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WorkBase.UI;

namespace Tester
{
    public partial class fGE : Form
    {
        public fGE()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gevMain.Cargar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GEKmlNetworkLink enlace = new GEKmlNetworkLink("prueba", "http://www.transportesolivares.cl/gps/ubicacion/placa=BV-ZG22/", true, false, GEKmlRefreshMode.Interval, 30);
            gevMain.AgregarEnlaceKML(enlace);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
                gevMain.ControlNavegacion = (GENavigationControlVisibility)comboBox1.SelectedIndex;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            gevMain.MostrarCaminos = checkBox2.Checked;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            gevMain.MostrarEdificios = checkBox3.Checked;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            gevMain.MostrarEdificiosBajaResolucion = checkBox4.Checked;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            gevMain.MostrarFronteras = checkBox5.Checked;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            gevMain.MostrarTerreno = checkBox6.Checked;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            gevMain.EliminarEnlaceKML(0);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            gevMain.EliminarEnlacesKML();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            gevMain.EliminarEnlaceKML("prueba");
        }

    }
}
