using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WorkBase.DataLayer;

namespace Tester
{
    public partial class fMySQL : Form
    {
        public fMySQL()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CadenaConexion ccmy = new CadenaConexion();
            ccmy.Host = "localhost";
            ccmy.Puerto = 3306;
            ccmy.Usuario = "root";
            ccmy.Contrasena = "";
            ccmy.BaseDatos = "tecnico";

            Database db = new Database(TipoConnector.MySQL, ccmy);
            db.Abrir();

            if (db.Conectado)
            {
                db.Consultar("select * from alumnos", grilla);
            }
            else
            {
                MessageBox.Show("sin conexion.");
            }

            db.Cerrar();
        }
    }
}
