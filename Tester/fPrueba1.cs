using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevO2.DataLayer;
using DevO2.Shared;

namespace Tester
{
    public partial class fPrueba1 : Form
    {
        private ToolStrip propToolStrip = new ToolStrip();

        public fPrueba1()
        {
            InitializeComponent();

            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);           

            //this.propToolStrip.ResumeLayout(false);
            //this.ResumeLayout(false);
            //this.PerformLayout();
        }

        public ToolStrip ToolStrip
        {
            get { return this.propToolStrip; }
            set { this.propToolStrip = value; }
        }

        private void boton_Click(object sender, EventArgs e)
        {
            this.Text = "prueba boton";
            this.textBoxEx3.Text = "BOTON";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hola mundo!!");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           

            comboBoxEx1.AgregarItem("uno", "uno - 1");
            comboBoxEx1.AgregarItem("dos", "dos - 2");
            comboBoxEx1.AgregarItem("tres", "tres - 3");
            comboBoxEx1.Items.Add("cuatro");
            comboBoxEx1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //textBoxEx1.Text = Security2.Codificar(textBoxEx1.Text, TipoCodificacion.Rijndael);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }

        public void CargarToolStrip()
        {
            bool tb = false;

            // Creo un boton en el toolstrip
            ToolStripButton boton = new ToolStripButton();

            //this.propToolStrip.SuspendLayout();
            //this.SuspendLayout();
            
            if (this.propToolStrip.ImageList != null && this.propToolStrip.ImageList.Images.Count > 0)
                tb = true;

            if (tb == true)
                boton.ImageIndex = 0;

            boton.Name = "prueba_A";
            boton.Text = "prueba";
            boton.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            boton.Click += new EventHandler(this.boton_Click);

            //propToolStrip.Items.Add((ToolStripItem)boton);
            propToolStrip.Items.Add(boton);
        }

        private void fPrueba1_Load(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CadenaConexion cc = new CadenaConexion();
            cc.Host = "localhost";
            cc.Usuario = "postgres";
            cc.Contrasena = "lordbytes";
            cc.BaseDatos = "pruebas_wb";

            DlConnection db = new DlConnection(TipoConnector.PostgreSQL, cc);
            db.Abrir();
            DataStorage ds = db.Consultar("SELECT * FROM clientes");

            if (ds.TieneFilas)
            {
                MessageBox.Show(ds[0].ToString() + " - " + ds[1].ToString());

                if (ds[2].IsNull())
                    MessageBox.Show("Nulo");
                else
                    MessageBox.Show(ds[2].ToDouble().ToString("#,#0"));
            }

            db.Cerrar();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            CadenaConexion cc = new CadenaConexion();
            cc.Host = "localhost";
            cc.Usuario = "postgres";
            cc.Contrasena = "lordbytes";
            cc.BaseDatos = "pruebas_wb";
            DlConnection db = new DlConnection(TipoConnector.PostgreSQL, cc);

            try
            {                
                db.Abrir();

                if (!db.IniciarTransaccion())
                    MessageBox.Show("Error al iniciar transaccion");

                db.Consultar("DELETE FROM clientes");

                db.Consultar("INSERT INTO clientes VALUES ('15509513-K', 'Daniel', 76788)");

                throw new Exception("error");

                if (db.DestinarTransaccion())
                    MessageBox.Show("Transaccion exitosa");
                else
                    MessageBox.Show("Error al destinar transaccion");

                db.Cerrar();
            }
            catch (Exception ex)
            {
                if (!db.RestaurarTransaccion())
                    MessageBox.Show("Error al restaurar transaccion");
            }
        }

    }
}
