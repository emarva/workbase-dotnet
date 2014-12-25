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
    public partial class fMapa : Form
    {
        public fMapa()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            

   
           GMapsMarker marc;

            marc = new GMapsMarker();

            marc.Nombre = "MARC1";
            marc.Icono = "http://www.transportesolivares.cl/app/images/gps/camion.png";
            marc.Titulo = "ddddd";
            marc.Contenido = "hola mundo";
            marc.Latitud = txtLatitud.Text;
            marc.Longitud = txtLongitud.Text;

            gMapsViewer1.AgregarMarcador(marc);

            //col.Add(marc);

            marc = new GMapsMarker();

            marc.Nombre = "MARC2";
            //marc.Icono = "http://www.transportesolivares.cl/app/images/gps/camion.png";
            marc.Titulo = "NORMAL";
            marc.Contenido = "hola mundo";
            marc.Latitud = "-33.759646";
            marc.Longitud = "-70.741096";

            //col.Add(marc);

            gMapsViewer1.AgregarMarcador(marc);

            marc = new GMapsMarker();

            marc.Nombre = "MARC3";
            //marc.Icono = "http://www.transportesolivares.cl/app/images/gps/camion.png";
            marc.Titulo = "APROXIMADO";
            marc.Contenido = "hola mundo";
            marc.Latitud = "-34.759620";
            marc.Longitud = "-70.074400";

            gMapsViewer1.AgregarMarcador(marc);

            //col.Add(marc);

           // gMapsViewer1.Marcadores = col;

            //for (int i = 0; i < col.Count; i++)
            //{
            //    gMapsViewer1.Marcadores.Add(col[i]);
            //}

            
        }

        private void fMapa_Load_1(object sender, EventArgs e)
        {
            gMapsViewer1.Cargar();
        }

        private void fMapa_Resize(object sender, EventArgs e)
        {
            //gMapsViewer1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gMapsViewer1.Zoom = Convert.ToInt32(textBox1.Text);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
                gMapsViewer1.TipoMapa =  GMapsTipoMapa.Mapa;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
                gMapsViewer1.TipoMapa = GMapsTipoMapa.Satelite;

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
                gMapsViewer1.TipoMapa = GMapsTipoMapa.Hibrido;

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true)
                gMapsViewer1.TipoMapa = GMapsTipoMapa.Relieve;            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            gMapsViewer1.MostrarControlesNavegacion = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            gMapsViewer1.MostrarControlesTipoMapa = checkBox2.Checked;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            gMapsViewer1.MostrarControlesEscala = checkBox3.Checked;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<GMapsLatLng> colCoordenadas = new List<GMapsLatLng>();
            colCoordenadas.Add(new GMapsLatLng("37.772323", "-122.214897"));
            colCoordenadas.Add(new GMapsLatLng("21.291982", "-157.821856"));
            colCoordenadas.Add(new GMapsLatLng("-18.142599", "178.431"));
            colCoordenadas.Add(new GMapsLatLng("-27.46758", "153.027892"));

            gMapsViewer1.AgregarPolilinea(colCoordenadas, "#ff0000", "1.0", "2");            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<GMapsLatLng> colCoordenadas = new List<GMapsLatLng>();
            colCoordenadas.Add(new GMapsLatLng("25.774252", "-80.190262"));
            colCoordenadas.Add(new GMapsLatLng("18.466465", "-66.118292"));
            colCoordenadas.Add(new GMapsLatLng("32.321384", "-64.75737"));
            colCoordenadas.Add(new GMapsLatLng("25.774252", "-80.190262"));

            gMapsViewer1.AgregarPoligono(colCoordenadas, "#ff0000", "0.8", "2", "#0000ff", "0.35");

            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            gMapsViewer1.EstablecerCentro(new GMapsLatLng(txtLatitud.Text, txtLongitud.Text));
        }
    }
}
