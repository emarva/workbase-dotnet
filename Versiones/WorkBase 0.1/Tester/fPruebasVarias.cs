using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
//using WindowsFormsApplication1.servicio1;
using System.Net;
using System.Net.NetworkInformation;
using WorkBase.DataLayer;
using WorkBase.Shared;

namespace Tester
{
    public partial class fPruebasVarias : Form
    {
        private string texto;

        public fPruebasVarias()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            XmlDocument xd = new XmlDocument();

            xd.Load("test.xml");
            XmlNode xn = xd.SelectSingleNode("/config/" + txtSeccion.Text + "/" + txtPropiedad.Text);

            textBox1.Text = xn.InnerText != null ? xn.InnerText : "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*DataSet ds = new DataSet();
            ds.ReadXml("test.xml");*/
            //ds.Tables.
            //ds.Tables.Add(txtSeccion.Text);
            //DataRow dr = ds.Tables[txtSeccion.Text].NewRow();

            /*dr[txtPropiedad.Text] = txtValor.Text;

            ds.Tables[txtSeccion.Text].Rows.Add(dr);*/
            //ds.Tables[txtSeccion.Text].Rows[0][txtPropiedad.Text] = txtValor.Text;
            //ds.WriteXml("test.xml");

            XmlDocument xd = new XmlDocument();
            //XmlNode xn;

            xd.Load("test.xml");

            XmlNode xn = xd.SelectSingleNode("/config/" + txtSeccion.Text);

            // La seccion no existe hay que crearla
            if (xn == null) 
            {
                XmlElement nuevaSeccion = xd.CreateElement(txtSeccion.Text);

                //XmlElement nuevaPropiedad = xd.CreateElement(txtPropiedad.Text);
                //nuevaPropiedad.InnerText = txtValor.Text;

                //nuevaSeccion.AppendChild(nuevaPropiedad);
                
                xd.DocumentElement.AppendChild(nuevaSeccion);
                /*XmlNode xnSeccion = xd.CreateNode(XmlNodeType.Element, txtSeccion.Text, "");
                XmlElement xeRoot = xd.DocumentElement;

                xeRoot.AppendChild(xnSeccion);*/

                //xd.Save("test.xml");

                xn = xd.SelectSingleNode("/config/" + txtSeccion.Text);
                //return;
            }

            //bool EstaNodo = false;
            /*XmlNodeList nodos = xd.SelectNodes("/config/" + txtSeccion.Text);
            foreach (XmlNode nodo in nodos)
            {
                string o = nodo["ddd"].Name;


                if (nodo.Name == txtPropiedad.Text)
                {
                    nodo.InnerText = txtValor.Text;
                    EstaNodo = true;
                }
            }*/

            xn = xd.SelectSingleNode("/config/" + txtSeccion.Text + "/" + txtPropiedad.Text);
            if (xn != null)
            {
                xn.InnerText = txtValor.Text;
            }
            else
            {
                XmlElement xe = xd.CreateElement(txtPropiedad.Text);
                xe.InnerText = txtValor.Text;

                xn.AppendChild(xe);
                //xd.AppendChild(xn);
            }

            xd.Save("test.xml");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Valor");
                        
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Service1 s1 = new Service1();
            //MessageBox.Show(s1.HelloWorld());
        }


        private void textBoxEx1_KeyDown(object sender, KeyEventArgs e)
        {
            /*if (e.KeyCode == Keys.Back)
                MessageBox.Show("testing");*/
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            gMapsViewer1.Cargar();

        }

        private void button4_Click(object sender, EventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string s = String.Format(textBox3.Text, Convert.ToInt64(textBox2.Text));
            textBox2.Text = s;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //textBoxEx3.Text = Security2.Codificar(textBoxEx3.Text, TipoCodificacion.Base64);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //textBoxEx3.Text = Security2.Decodificar(textBoxEx3.Text, TipoDecodificacion.Base64);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string[] p = new string[1];
            PruebaRef(ref p);
        }

        public void PruebaRef(ref string[] dato)
        {
            dato[0] = "ddd";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox4.Text = String.Format("{0:MM/dd/yyyy}", DateTime.Now); 
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string hostNombre = Dns.GetHostName();

            IPHostEntry host = Dns.GetHostEntry(hostNombre);
            IPAddress[] ip = host.AddressList;
            txtIP.Text = ip[0].ToString();

            txtHost.Text = hostNombre;



/*            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            string msj = "";

            foreach (NetworkInterface adapter in adapters)
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                msj += adapter.Description;
                msj += string.Format("  DNS suffix .............................. : {0}",
                    properties.DnsSuffix);
                msj += string.Format("  DNS enabled ............................. : {0}",
                    properties.IsDnsEnabled);
                msj += string.Format("  Dynamically configured DNS .............. : {0}",
                    properties.IsDynamicDnsEnabled);
            }

            textBox1.AppendText(msj);*/

        }

        private void button13_Click(object sender, EventArgs e)
        {
            Log.ArchivoLog = @"c:\milog.txt";

            Log.CrearLineaPuntos();
            Log.CrearLineaSimple();
            Log.CrearLineaPuntos();
            Log.CrearLineaEnBlanco();
            Log.CrearCadena("HOLA MUNDO");
            Log.CrearLineaEnBlanco();
            Log.CrearLineaPuntos();
            Log.CrearLineaSimple();
            Log.CrearLineaPuntos();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Printer printer = new Printer();

            printer.Impresora = txtImpresora.Text;
            printer.LargoMaximoCaracteres = 42;

            printer.AgregarLineaTexto("Titulo");
            printer.AgregarLineaPuntos();
            printer.AgregarLineaEnBlanco();
            printer.AgregarLineaTexto("Texto subtitulo", false, PrinterAlineacionLinea.Derecha);
            printer.AgregarLineaSimple();
            printer.AgregarLineaTexto("Hola, mundo!!!");
            printer.AgregarLineaTexto("Hola, mundo!!!");
            printer.AgregarLineaTexto("Hola, mundo!!!");
            printer.Imprimir(true);
            printer.Limpiar();

            printer.AgregarLineaEnBlanco();
            printer.AgregarLineaEnBlanco();
            printer.AgregarLineaTexto("Titulo");
            printer.AgregarLineaPuntos();
            printer.AgregarLineaEnBlanco();
            printer.AgregarLineaTexto("Texto subtitulo");
            printer.AgregarLineaSimple();
            printer.AgregarLineaTexto("Hola, mundo!!!");
            printer.AgregarLineaTexto("Hola, mundo!!!");
            printer.AgregarLineaTexto("Hola, mundo!!!");
            printer.AgregarLineaEnBlanco();
            printer.AgregarLineaEnBlanco();
            printer.AgregarLineaEnBlanco();
            printer.AgregarLineaEnBlanco();
            printer.AgregarLineaEnBlanco();
            printer.Imprimir(true, true);
        }

        private void button15_Click(object sender, EventArgs e)
        {
        }

        private void button15_Click_1(object sender, EventArgs e)
        {
            //WorkBase.UI.GMapsLatLng la = gMapsViewer1.ObtenerCentro();
            gMapsViewer1.Latitud = "";
            gMapsViewer1.Longitud = "";
            gMapsViewer1.Refresh();
        }

        private void gMapsViewer1_ClickMap(WorkBase.UI.GMapsLatLng coordenada)
        {
            MessageBox.Show(coordenada.Latitud.ToString());
        }

        private void button16_Click(object sender, EventArgs e)
        {
            gMapsViewer1.Latitud = "-30";
            gMapsViewer1.Longitud = "-70";
            gMapsViewer1.Refresh();

            List<WorkBase.UI.GMapsLatLng> col = new List<WorkBase.UI.GMapsLatLng>();

            col.Add(new WorkBase.UI.GMapsLatLng("12", "11"));

            gMapsViewer1.AgregarMarcador("-30", "-70");
            gMapsViewer1.AgregarMarcador("-31", "-71", "file:///" + @"D:\Desarrollo\GPS Server\GPS Server\bin\Debug\Marcadores\marcador1.png".Replace(@"\", "/"));
            gMapsViewer1.AgregarMarcador("-31", "-70", "file:///" + @"D:\Desarrollo\GPS Server\GPS Server\bin\Debug\Marcadores\marcador1.png".Replace(@"\", "/"), "prueba");
            gMapsViewer1.AgregarMarcador("-30", "-71", "file:///" + @"D:\Desarrollo\GPS Server\GPS Server\bin\Debug\Marcadores\marcador1.png".Replace(@"\", "/"), "prueba", "hola mundo");
            gMapsViewer1.AgregarPoligono(col, "#00f", "#f00");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            gMapsViewer1.EliminarMarcador(2);

        }

        private void button18_Click(object sender, EventArgs e)
        {
            gMapsViewer1.EliminarMarcadores();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            try
            {
                throw new Exception("Error generado");
            }
            catch (Exception ex)
            {
                em.Show("Titulo 2", ex);
            }
        }
    }
}
