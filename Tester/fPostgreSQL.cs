using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevO2.DataLayer;

namespace Tester
{
    public partial class fPostgreSQL : Form
    {
        public fPostgreSQL()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          /*  CadenaConexion ccpg = new CadenaConexion();
            ccpg.Host = "localhost";
            //ccpg.Puerto = 5432;
            ccpg.Usuario = "postgres";
            ccpg.Contrasena = "lordbytes";
            ccpg.BaseDatos = "pruebas_wb";

            DlConnection db = new DlConnection(TipoConnector.PostgreSQL, ccpg);
            db.Abrir();

            if (db.Conectado)
            {
                db.Consultar("select * from clientes", grilla);
            }
            else
            {
                MessageBox.Show("No esta conectado.");
            }

            db.Cerrar();*/
        }
    }
}
