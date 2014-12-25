using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevO2.liveConnector;
using DevO2.DataLayer;
using DevO2.Shared;

namespace Tester
{

    /// <summary>
    /// 
    /// </summary>
    public partial class fMain : Form
    {
        public string EsquemaToolStrip // SOLO PARA TABFRAME
        {
            get
            {
                // EN FORMATO XML
                return "";
            }
        }

        public string EsquemaPermisos
        {
            get
            {
                // EN FORMATO XML
                return "<>";
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="fMain"/> class.
        /// </summary>
        public fMain()
        {
            InitializeComponent();

            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }


        /// <summary>
        /// Handles the Click event of the toolStripButton1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            toolStrip2.ImageList = imageList1;

            fPrueba1 f1 = new fPrueba1();
            tbfMain.AgregarFormulario(f1, true, Color.LightSkyBlue, true);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            fPrueba2 f2 = new fPrueba2();

            if (!tbfMain.ExisteFormulario(f2, true))
                tbfMain.AgregarFormulario(f2, true, Color.LightSkyBlue);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            moduleManager1.RutaModulos = Application.StartupPath + @"\Modulos";
            moduleManager1.CargarModulos();
        }

        /// <summary>
        /// Handles the Click event of the toolStripButton4 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            /*CadenaConexion cc = new CadenaConexion();
            cc.Host = @"192.168.0.201";
            cc.BaseDatos = "pruebas";
            cc.SeguridadIntegrada = true;

            DlConnection db = new DlConnection(TipoConnector.MSSQL,cc);
            
            if (!db.Abrir())
                MessageBox.Show("No se puede conectar.");

            string sql = "SELECT * " +
                         "FROM vista_prueba";

            DataStorage ds;

            ds = db.Consultar(sql);

            db.Consultar(sql, dataGridView1);

            if (db.Conectado)
            {
                if (ds.TieneFilas)
                {
                    ds.MoverPrimero();
                    string sFila = "";

                    while (!ds.EsEOF)
                    {
                        sFila += ds["Id"].ToString() + " - ";
                        sFila += ds["Nombre"].ToString() + "\r\n";
                        sFila += ds["Direccion"].ToString() + "\r\n";

                        ds.MoverSiguiente();
                    }
                   
                    textBox1.Text += sFila;
                }
            }

            ds.Dispose();
            db.Cerrar();*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
//            textBoxEx1.Text = "00111";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //textBox2.Text = Security2.Codificar(textBox2.Text, TipoCodificacion.XOR);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //textBox2.Text = Security2.Decodificar(textBox2.Text, TipoDecodificacion.XOR);
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {
            /*CadenaConexion cc = new CadenaConexion();
            cc.Host = "(local)";
            cc.BaseDatos = "pruebas";
            cc.SeguridadIntegrada = true;

            DAOGeneric dao = new DAOGeneric(TipoConnector.MSSQL, cc);
            Prueba_DAO dto = new Prueba_DAO();

            //dto.Id = 1;

            DataStorage ds = dao.Select(dto);
            
            if (ds.TieneFilas)
            {
                ds.MoverPrimero();
                string sFila = "";

                while (!ds.EsEOF)
                {
                    sFila += ds["Id"].ToString() + " - ";
                    sFila += ds["Nombre"].ToString() + " - ";
                    sFila += ds["Direccion"].ToString() + "\r\n";

                    ds.MoverSiguiente();
                }

                textBox1.Text = "";
                textBox1.AppendText(sFila);
            }*/
        }

        private void button7_Click(object sender, EventArgs e)
        {
            /*CadenaConexion cc = new CadenaConexion();
            cc.Host = "(local)";
            cc.BaseDatos = "pruebas";
            cc.SeguridadIntegrada = true;
            
            DAOGeneric dao = new DAOGeneric(TipoConnector.MSSQL, cc);
            Prueba_DAO dto = new Prueba_DAO();

            dto.Nombre = "prueba";
            dto.Direccion = "Calle siempre viva 01";

            dao.Insert(dto);*/
        }

        private void button8_Click(object sender, EventArgs e)
        {
            /*CadenaConexion cc = new CadenaConexion();
            cc.Host = "(local)";
            cc.BaseDatos = "pruebas";
            cc.SeguridadIntegrada = true;
    
            DAOGeneric dao = new DAOGeneric(TipoConnector.MSSQL, cc);
            Prueba_DAO dto = new Prueba_DAO();
            
            dto.Id = Convert.ToInt32(textBox3.Text);

            int filas = dao.Delete(dto);
            textBox1.AppendText("Filas afectadas: " + filas.ToString());*/
        }

        private void button9_Click(object sender, EventArgs e)
        {
            float f = 5.6f;
            int i;

            i = (int)f; // TRUNCA

            MessageBox.Show(i.ToString());

            i = Convert.ToInt32(f); // REDONDEA

            MessageBox.Show(i.ToString());
        }

        private void button10_Click(object sender, EventArgs e)
        {
            /*CadenaConexion cc = new CadenaConexion();
            cc.Host = "(local)";
            cc.BaseDatos = "pruebas";
            cc.SeguridadIntegrada = true;

            DAOGeneric dao = new DAOGeneric(TipoConnector.MSSQL, cc);
            Prueba_DAO dto = new Prueba_DAO();
            Prueba_DAO dtoCondicion = new Prueba_DAO();

            dto.Nombre = txtNombre.Text;
            dto.Direccion = txtDirec.Text;

            dtoCondicion.Id = Convert.ToInt32(textBox3.Text);
            //dtoCondicion.Direccion = "DIREC 1";

            int filas = dao.Update(dto, dtoCondicion);
            textBox1.AppendText("Filas afectadas: " + filas.ToString());*/
        }

        private void button11_Click(object sender, EventArgs e)
        {
            /*CadenaConexion cc = new CadenaConexion();
            cc.Host = "(local)";
            cc.BaseDatos = "pruebas";
            cc.SeguridadIntegrada = true;

            DlConnection db = new DlConnection(TipoConnector.MSSQL, cc);
            ParametroCollection parametros = new ParametroCollection();

            parametros.Add("@Id", Convert.ToInt32(textBox3.Text));
            parametros.Add("@nom", "VARCHAR", 50, ParameterDirection.Output);
            parametros.Add("@direc", "VARCHAR", 50, ParameterDirection.Output);
            parametros.Add("retval", ParameterDirection.ReturnValue);


            db.Abrir();

            DataStorage ds = db.Consultar("sp_prueba", parametros);

            db.Cerrar();

            string sFila = "";

            if (db.Parametros != null)
            {
                for (int p = 0; p <= db.Parametros.Count - 1; p++)
                {
                    sFila += "Parametro: " + db.Parametros.Item(p).Nombre + " // Valor: " + db.Parametros.Item(p).Valor.ToString() + "\r\n";
                }
            }

            if (ds.TieneFilas)
            {
                ds.MoverPrimero();
                
                while (!ds.EsEOF)
                {
                 //   sFila += ds["Id"].ToString() + " - ";
                    sFila += ds["nom"].ToString() + " - ";
                    sFila += ds["direc"].ToString() + "\r\n";

                    ds.MoverSiguiente();
                }

                
            }

            textBox1.Text = "";
            textBox1.AppendText(sFila);*/
        }

        private void mapaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fMapa mapa = new fMapa();
            mapa.Show();
        }

        private void pruebasVariasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fPruebasVarias fpv = new fPruebasVarias();
            fpv.Show();
        }

        private void mySQÑToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fMySQL my = new fMySQL();
            my.ShowDialog(this);
        }

        private void seguridadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fSeguridad seguridad = new fSeguridad();
            seguridad.ShowDialog();
        }

        private void postgreSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fPostgreSQL pg = new fPostgreSQL();
            pg.ShowDialog(this);
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutDialog1.Show();
        }

        private void permisosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fPermisos permisos = new fPermisos();
            permisos.ShowDialog();
        }

        private void validacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fValidacion validar = new fValidacion();
            validar.ShowDialog();
        }

        private void googleEarthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fGE ge = new fGE();
            ge.ShowDialog();
        }
    }
}
