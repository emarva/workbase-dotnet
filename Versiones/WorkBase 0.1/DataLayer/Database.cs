using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text;
using WorkBase.Shared;

namespace WorkBase.DataLayer
{
    public class CadenaConexion
    {
        #region Variables
        private string propHost;
        private int propPuerto;
        private string propBaseDatos;
        private string propUsuario;
        private string propContrasena;
        private bool propSeguridadIntegrada;
        private string _cadenaConexion;
        #endregion

        #region Propiedades
        public string Host
        {
            get { return this.propHost; }
            set { this.propHost = value; } 
        }

        public int Puerto
        {
            get { return this.propPuerto; }
            set { this.propPuerto = value; }
        }

        public string BaseDatos
        {
            get { return this.propBaseDatos; }
            set { this.propBaseDatos = value; }
        }

        public string Usuario
        {
            get { return this.propUsuario; }
            set { this.propUsuario = value; }
        }

        public string Contrasena
        {
            get { return this.propContrasena; }
            set { this.propContrasena = value; }
        }

        public bool SeguridadIntegrada
        {
            get { return this.propSeguridadIntegrada; }
            set { this.propSeguridadIntegrada = value; }
        }

        public string Cadena
        {
            get { return _cadenaConexion; }
            set { _cadenaConexion = value; }
        }
        #endregion
    }

    public sealed class Database
    {
        #region Variables
        private CadenaConexion propCadenaConexion;
        private ParametroCollection propParametros;
        private bool propEstaAbierta;
        private bool propBloquearConexion;

        private IConnector connector;
        private TipoConnector tipoConector;
        private bool disposed;
        #endregion

        #region Constructores
        public Database(TipoConnector conector)
            : this(conector, new CadenaConexion())
        {
        }

        public Database(TipoConnector conector, CadenaConexion cadenaConexion)
        {
            if (cadenaConexion.Host != null)
                propCadenaConexion = cadenaConexion;

            tipoConector = conector;
        }

        public Database(TipoConnector conector, string cadena)
        {
            if (!string.IsNullOrEmpty(cadena))
            {
                propCadenaConexion = new CadenaConexion();
                propCadenaConexion.Cadena = cadena;
            }                    

            tipoConector = conector;
        }
        #endregion

        #region Propiedades
        public CadenaConexion CadenaConexion
        {
            get { return this.propCadenaConexion; }
            set 
            {
                this.propCadenaConexion = new CadenaConexion();
                this.propCadenaConexion = value;
            }
        }

        public ParametroCollection Parametros
        {
            get { return this.propParametros; }       
        }

        public bool TieneFilas
        {
            get { return this.connector.TieneFilas; }
        }

        public int FilasAfectadas
        {
            get { return this.connector.FilasAfectadas; }
        }

        public bool EstaAbierta
        {
            get { return this.propEstaAbierta; }
        }

        public bool BloquearConexion
        {
            get { return this.propBloquearConexion; }
            set { this.propBloquearConexion = value; }
        }
        #endregion

        #region Metodos
        public bool Abrir()
        {
            try
            {
                // Verifica si la conexion esta abierta
                if (this.propBloquearConexion)
                    return true;

                switch (this.tipoConector)
                {
                    case TipoConnector.Access:
                        System.Windows.Forms.MessageBox.Show("Este conector no se encuentra disponible.");
                        //connector = new AccessConnector(propCadenaConexion.Host, propCadenaConexion.Puerto, propCadenaConexion.BaseDatos, propCadenaConexion.Usuario, propCadenaConexion.Contrasena);
                        //connector.SeguridadIntegrada = propCadenaConexion.SeguridadIntegrada;
                        return false;
                    case TipoConnector.DB2:
                        System.Windows.Forms.MessageBox.Show("Este conector no se encuentra disponible.");
                        return false;
                    case TipoConnector.Firebird:
                        System.Windows.Forms.MessageBox.Show("Este conector no se encuentra disponible.");
                        return false;
                    case TipoConnector.MSSQL:
                        if (string.IsNullOrEmpty(propCadenaConexion.Cadena))
                        {
                            this.connector = new MSSQLConnector(this.propCadenaConexion.Host, this.propCadenaConexion.Puerto, this.propCadenaConexion.BaseDatos, this.propCadenaConexion.Usuario, this.propCadenaConexion.Contrasena);
                            this.connector.SeguridadIntegrada = this.propCadenaConexion.SeguridadIntegrada;
                        }
                        else
                        {
                            this.connector = new MSSQLConnector(propCadenaConexion.Cadena);
                        }
                        break;
                    case TipoConnector.MySQL:
                        Assembly asmMy = Assembly.LoadFrom("WorkBase.MySQLConnector.dll");
                        Type tipoMy = asmMy.GetType("WorkBase.Connectors.MySQLConnector");
                        this.connector = (IConnector)Activator.CreateInstance(tipoMy, new object[] { this.propCadenaConexion.Host, this.propCadenaConexion.Puerto, this.propCadenaConexion.BaseDatos, this.propCadenaConexion.Usuario, this.propCadenaConexion.Contrasena });
                        break;
                    case TipoConnector.PostgreSQL:
                        Assembly asmPg = Assembly.LoadFrom("WorkBase.PostgreSQLConnector.dll");
                        Type tipoPg = asmPg.GetType("WorkBase.Connectors.PostgreSQLConnector");
                        this.connector = (IConnector)Activator.CreateInstance(tipoPg, new object[] { this.propCadenaConexion.Host, this.propCadenaConexion.Puerto, this.propCadenaConexion.BaseDatos, this.propCadenaConexion.Usuario, this.propCadenaConexion.Contrasena });
                        this.connector.SeguridadIntegrada = this.propCadenaConexion.SeguridadIntegrada;
                        break;
                    case TipoConnector.SQLite:
                        System.Windows.Forms.MessageBox.Show("Este conector no se encuentra disponible.");
                        return false;
                }

                this.propEstaAbierta = this.connector.Conectar();
                return this.propEstaAbierta;
            }
            catch { return false; }
        }

        public bool Cerrar()
        {
            try
            {
                if (!this.propBloquearConexion)
                {
                    this.propParametros = null;
                    return this.connector.Desconectar();
                }
                else
                {
                    return true;
                }
            }
            catch { return false; }
        }

        private DataStorage ConstruirDataStorage(DbDataReader dr)
        {
            DataStorage ds = new DataStorage();
            int contFilas = 0;
            Filas filas = new Filas();

            try
            {
                // Recorro el DataReader
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        Fila fila = new Fila();

                        fila.Numero = contFilas + 1;
                        fila.TotalCampos = dr.VisibleFieldCount - 1;

                        for (int c = 0; c <= dr.VisibleFieldCount - 1; c++)
                        {
                            fila.Campos.Add(dr.GetName(c), dr[c]);
                        }

                        filas.Add(fila);
                        contFilas += 1;
                    }

                    ds.Filas = filas;

                    // Total de filas afectadas
                    ds.TotalFilas = contFilas;

                    // Establesco el EOF y la propiedad TieneFilas
                    if (contFilas == 0)
                    {
                        ds.TieneFilas = false;
                        ds.EsEOF = true;
                    }
                    else
                    {
                        ds.TieneFilas = true;
                        ds.EsEOF = false;
                    }
                }

                return ds;
            }
            catch //(Exception ex)
            {
                return null;
            }
        }

        private DataStorage Consultar(string consulta, System.Windows.Forms.DataGridView grilla, System.Windows.Forms.ComboBox combo, System.Windows.Forms.ToolStripComboBox comboToolStrip, string campoNombre, string campoItemData, ArrayList parametros, ParametroCollection colParametros)
        {
            try
            {                
                DataStorage ds = new DataStorage();

                if (consulta.ToLower().Contains("select"))
                {
                    DbDataReader dr;

                    // Verifico si la consulta se ejecuta con parametros
                    if (parametros != null)
                        dr = this.connector.Consulta(consulta, parametros);
                    else
                        dr = this.connector.Consulta(consulta);

                    if (grilla != null)
                    {
                        DataTable dt = new DataTable();
                        dt.Load(dr);

                        grilla.DataSource = dt;

                        dr.Close();
                        dt.Dispose();
                    }
                    else if (combo != null)
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                combo.Items.Add(dr[campoNombre].ToString());
                            }
                        }
                    }
                    else if (comboToolStrip != null)
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                comboToolStrip.Items.Add(dr[campoNombre].ToString());
                            }
                        }
                    }
                    else
                    {
                        ds = this.ConstruirDataStorage(dr);
                        dr.Close();
                    }

                    return ds;
                }
                else if (consulta.ToLower().Contains("insert") || consulta.ToLower().Contains("update") || consulta.ToLower().Contains("delete"))
                {
                    // Verifico si la consulta se ejecuta con parametros
                    if (parametros != null)
                        this.connector.Ejecutar(consulta, parametros);
                    else
                        this.connector.Ejecutar(consulta);

                    /*if (!connector.Ejecutar(consulta))
                        throw new InvalidOperationException("No se puede ejecutar la consulta");*/
                }
                else // Procedimiento almacenado
                {
                    if (colParametros != null)
                    {
                        DbDataReader drSP = this.connector.Ejecutar(consulta, colParametros);
                        DataStorage dsSP = this.ConstruirDataStorage(drSP);
                        this.propParametros = this.connector.Parametros;

                        drSP.Close();
                        return dsSP;
                    }
                    else
                    {
                        this.connector.Ejecutar(consulta);
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                //ErrorManager em = new ErrorManager(ex);
                throw ex;
                //return null;
            }
        }

        /*private DataTable Consultar(string consulta, ArrayList parametros, ParametroCollection colParametros)
        {
            try
            {
                DbDataReader dr;

                if (consulta.ToLower().Contains("select"))
                {
                    // Verifico si la consulta se ejecuta con parametros
                    if (parametros != null)
                        dr = this.connector.Consulta(consulta, parametros);
                    else
                        dr = this.connector.Consulta(consulta);

                    DataTable dt = new DataTable();
                    dt.Load(dr);

                    dr.Close();
                    return dt;
                }
                else if (consulta.ToLower().Contains("insert") || consulta.ToLower().Contains("update") || consulta.ToLower().Contains("delete"))
                {
                    throw new InvalidOperationException("No se permite este tipo de consultas.");
                }
                else // Procedimiento almacenado
                {
                    if (colParametros != null)
                    {
                        DbDataReader drSP = this.connector.Ejecutar(consulta, colParametros);
                        DataTable dt = new DataTable();

                        dt.Load(drSP);

                        drSP.Close();
                        return dt;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                //ErrorManager em = new ErrorManager(ex);
                throw ex;
                //return null;
            }
        } */     

        public DataStorage Consultar(string consulta)
        {
            return this.Consultar(consulta, null, null, null, null, null, null, null);
        }

        public DataStorage Consultar(string consulta, ParametroCollection parametros)
        {
            return this.Consultar(consulta, null, null, null, null, null, null, parametros);
        }

        public DataStorage Consultar(string consulta, ArrayList parametros)
        {
            return this.Consultar(consulta, null, null, null, null, null, parametros, null);
        }

        public DataStorage Consultar(string consulta, System.Windows.Forms.ComboBox combo, string campoItem)
        {
            return this.Consultar(consulta, null, combo, null, campoItem, null, null, null);
        }

        public DataStorage Consultar(string consulta, System.Windows.Forms.ComboBox combo, string campoItem, ArrayList parametros)
        {
            return this.Consultar(consulta, null, combo, null, campoItem, null, parametros, null);
        }

        public DataStorage Consultar(string consulta, System.Windows.Forms.ToolStripComboBox comboToolStrip, string campoItem)
        {
            return this.Consultar(consulta, null, null, comboToolStrip, campoItem, null, null, null);
        }

        public DataStorage Consultar(string consulta, System.Windows.Forms.ToolStripComboBox comboToolStrip, string campoItem, ArrayList parametros)
        {
            return this.Consultar(consulta, null, null, comboToolStrip, campoItem, null, parametros, null);
        }

        public DataStorage Consultar(string consulta, System.Windows.Forms.DataGridView grilla)
        {
            return this.Consultar(consulta, grilla, null, null, null, null, null, null);
        }

        public DataStorage Consultar(string consulta, System.Windows.Forms.DataGridView grilla, ArrayList parametros)
        {
            return this.Consultar(consulta, grilla, null, null, null, null, parametros, null);
        }

        public DbDataAdapter ConsultarDataAdapter()
        {
            return null;
            //return new DbDataAdapter();
        }

        private DataTable ConsultarDataTable(string consulta, ArrayList parametros, ParametroCollection colParametros)
        {
            try
            {
                DbDataReader dr;

                if (consulta.ToLower().Contains("select"))
                {
                    // Verifico si la consulta se ejecuta con parametros
                    if (parametros != null)
                        dr = this.connector.Consulta(consulta, parametros);
                    else
                        dr = this.connector.Consulta(consulta);

                    DataTable dt = new DataTable();
                    dt.Load(dr);

                    dr.Close();
                    return dt;
                }
                else if (consulta.ToLower().Contains("insert") || consulta.ToLower().Contains("update") || consulta.ToLower().Contains("delete"))
                {
                    throw new InvalidOperationException("No se permite este tipo de consultas.");
                }
                else // Procedimiento almacenado
                {
                    if (colParametros != null)
                    {
                        DbDataReader drSP = this.connector.Ejecutar(consulta, colParametros);
                        DataTable dt = new DataTable();

                        dt.Load(drSP);

                        drSP.Close();
                        return dt;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                //ErrorManager em = new ErrorManager(ex);
                throw ex;
                //return null;
            }
        }      

        public DataTable ConsultarDataTable(string consulta)
        {
            return this.ConsultarDataTable(consulta, null, null);
        }

        public DataTable ConsultarDataTable(string consulta, ParametroCollection parametros)
        {
            return this.ConsultarDataTable(consulta, null, parametros);
        }

        public DataTable ConsultarDataTable(string consulta, ArrayList parametros)
        {
            return this.ConsultarDataTable(consulta, parametros, null);
        }

        public void CambiarBaseDatos(string nombreBaseDatos)
        {
            this.Consultar("USE " + nombreBaseDatos);
        }

        public bool IniciarTransaccion()
        {
            return this.connector.IniciarTransaccion();
        }

        public bool DestinarTransaccion()
        {
            return this.connector.DestinarTransaccion();
        }

        public bool RestaurarTransaccion()
        {
            return this.connector.RestaurarTransaccion();
        }
        #endregion

        #region IDisposable
        private void Dispose(bool disposing)
        {
            if (this.disposed && !this.disposed)
            {
                this.Cerrar();
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
