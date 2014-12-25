using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevO2.DataLayer;

namespace Demo
{
    public partial class fDataLayer : Form
    {
        private DlConnectionString dlCS;
        private DlConnection dlCnn;

        public fDataLayer()
        {
            InitializeComponent();

            dlCS = new DlConnectionString();
            dlCS.Conector = DlTipoConnector.SQLServer;
            dlCS.Host = @"DANIELNUNEZ\SQLEXPRESS";
            dlCS.BaseDatos = "pruebas";

           // dlCnn.CrearBaseDatos(
        }

        DlQuery dlQryDT = new DlQuery("SELECT * FROM clientes_p");
        DataTable dt = new DataTable();
        private void button1_Click(object sender, EventArgs e)
        {
            if (dlCnn.Estado == DlEstadoConexion.Abierta)
            {
                dlQryDT.Conexion = dlCnn;
                dlQryDT.Llenar(dt);
                dataGridView1.DataSource = dt;
            }
        }

        DlQuery dlQryDS = new DlQuery("SELECT * FROM clientes_p");
        DataSet ds = new DataSet();
        private void button2_Click(object sender, EventArgs e)
        {
            if (dlCnn.Estado == DlEstadoConexion.Abierta)
            {
                dlQryDS.Conexion = dlCnn;
                dlQryDS.Llenar(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dlCnn.Estado == DlEstadoConexion.Abierta)
            {
                DlQuery dlQry = new DlQuery("SELECT * FROM clientes_p", dlCnn);
                DbDataReader dr = dlQry.EjecutarReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dataGridView1.Rows.Add(new string[2] { dr[0].ToString(), dr[1].ToString() });
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dlQryDT.Actualizar(dt);
        }
        
        private void button5_Click(object sender, EventArgs e)
        {
            dlQryDS.Actualizar(ds);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            dlCnn = new DlConnection(dlCS);
            dlCnn.Abrir();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dlCnn.Cerrar();
        }
    }
}
