using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using WorkBase.Shared;

namespace WorkBase.DataLayer
{
    internal sealed class AccessConnector : IConnector
    {
        #region Varibales
        private OleDbConnection sqlConn;
        private OleDbCommand sqlCmd;
        private OleDbCommand sqlCmdExe;

        private bool propSeguridadIntegrada;
        private string propHost;
        private int propPuerto;
        private string propBaseDatos;
        private string propUsuario;
        private string propClave;
        private bool propConectado;
        private bool propTieneFilas;
        private int propFilasAfectadas;
        private ParametroCollection propParametros;
        #endregion

        #region Constructores
        public AccessConnector() { }

        public AccessConnector(string host, string basedatos, string usuario, string clave)
            : this(host, 0, basedatos, usuario, clave)
        {
        }

        public AccessConnector(string host, int puerto, string basedatos, string usuario, string clave)
        {
            this.propHost = host;
            this.propPuerto = puerto;
            this.propBaseDatos = basedatos;
            this.propUsuario = usuario;
            this.propClave = clave;
        }
        #endregion

        #region Propiedades
        public bool SeguridadIntegrada
        {
            get { return this.propSeguridadIntegrada; }
            set { this.propSeguridadIntegrada = value; }
        }

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

        public string Clave
        {
            get { return this.propClave; }
            set { this.propClave = value; }
        }

        public bool Conectado
        {
            get { return this.propConectado; }
        }

        public bool TieneFilas
        {
            get { return this.propTieneFilas; }
        }

        public int FilasAfectadas
        {
            get { return this.propFilasAfectadas; }
        }

        public ParametroCollection Parametros
        {
            get { return this.propParametros; }
        }
        #endregion

        #region Metodos
        public bool Conectar()
        {
            try
            {
                string cadenaConexion = string.Empty;

                if (this.propPuerto != 0)
                {
                    cadenaConexion = "Server=" + this.propHost + "," + this.propPuerto;
                }
                else
                {
                    cadenaConexion = "Data Source=" + this.propHost;
                }

                cadenaConexion += ";Database=" + this.propBaseDatos + ";";

                if (this.propSeguridadIntegrada == true)
                {
                    cadenaConexion += "Integrated Security=SSPI;";
                }
                else
                {
                    cadenaConexion += "User ID=" + this.propUsuario + ";Password=" + this.propClave;
                }

                /*using (sqlConn = new SqlConnection(cadenaConexion))
                {*/
                this.sqlConn = new OleDbConnection(cadenaConexion);
                this.sqlConn.Open();
                this.propConectado = true;
                return true;
                //}
            }
            catch
            {
                this.propConectado = false;
                return false;
            }
        }

        public bool Desconectar()
        {
            try
            {
                this.sqlConn.Close();
                this.propConectado = false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public DbDataReader Consulta(string consulta, ArrayList parametros)
        {
            try
            {
                if (!consulta.ToLower().Contains("select"))
                {
                    return null;
                }

                this.sqlCmd = new OleDbCommand(consulta, this.sqlConn);

                // Si hay parametros los agrego
                if (parametros != null)
                {
                    this.sqlCmd.CommandType = CommandType.Text;
                    
                    foreach (OleDbCommand p in parametros)
                    {
                        this.sqlCmd.Parameters.Add(p);
                    }
                }

                OleDbDataReader sqlDR = this.sqlCmd.ExecuteReader();
                this.propTieneFilas = sqlDR.HasRows;
                this.sqlCmd.Dispose();

                // Filas afectadas para un SELECT siempre es -1
                this.propFilasAfectadas = -1;

                return sqlDR;
            }
            catch (Exception ex)
            {
                //ErrorManager em = new ErrorManager(ex);
                throw ex;
                //return null;
            }
        }

        public DbDataReader Consulta(string consulta)
        {
            return this.Consulta(consulta, null);
        }

        private object Ejecutar(string consulta, ArrayList parametros, ParametroCollection colParametros)
        {
            try
            {
                if (consulta.ToLower().Contains("insert") || consulta.ToLower().Contains("update") || consulta.ToLower().Contains("delete"))
                {
                    this.sqlCmdExe = new OleDbCommand(consulta, this.sqlConn);

                    // Si hay parametros los agrego
                    if (parametros != null)
                    {
                        this.sqlCmdExe.CommandType = CommandType.Text;

                        foreach (OleDbParameter p in parametros)
                        {
                            this.sqlCmdExe.Parameters.Add(p);
                        }
                    }

                    /*if (sqlTrans != null)
                        if (sqlTrans.Connection != null)
                            sqlCmdExe.Transaction = sqlTrans;*/

                    // FilasAfectadas con valor 0 significa no hay filas o se produjo un error
                    this.propFilasAfectadas = this.sqlCmdExe.ExecuteNonQuery();
                    this.sqlCmdExe.Dispose();

                    return true;
                }
                else
                {
                    this.sqlCmdExe = new OleDbCommand(consulta, this.sqlConn);

                    // Si hay parametros los agrego
                    if (colParametros != null)
                    {
                        this.sqlCmdExe.CommandType = CommandType.StoredProcedure;
                        OleDbParameterCollection sqlParams = this.sqlCmdExe.Parameters;

                        for (int p = 0; p <= colParametros.Count - 1; p++)
                        {
                            OleDbParameter parametro = new OleDbParameter();

                            parametro.ParameterName = colParametros.Item(p).Nombre;

                            // Busco el tipo de columna
                            if (colParametros.Item(p).TipoColumna != "")
                            {
                                switch (colParametros.Item(p).TipoColumna.ToLower())
                                {
                                    case "bigint": parametro.OleDbType = OleDbType.BigInt; break;
                                    case "binary": parametro.OleDbType = OleDbType.Binary; break;
                                    case "bit": parametro.OleDbType = OleDbType.Binary; break;
                                    case "char": parametro.OleDbType = OleDbType.Char; break;
                                    case "datetime": parametro.OleDbType = OleDbType.Binary; break;
                                    case "decimal": parametro.OleDbType = OleDbType.Decimal; break;
                                    case "float": parametro.OleDbType = OleDbType.Binary; break;
                                    case "image": parametro.OleDbType = OleDbType.Binary; break;
                                    case "int": parametro.OleDbType = OleDbType.Binary; break;
                                    case "money": parametro.OleDbType = OleDbType.Binary; break;
                                    case "nchar": parametro.OleDbType = OleDbType.Binary; break;
                                    case "nvarchar": parametro.OleDbType = OleDbType.Binary; break;
                                    case "real": parametro.OleDbType = OleDbType.Binary; break;
                                    case "uniqueidentifier": parametro.OleDbType = OleDbType.Binary; break;
                                    case "smalldatetime": parametro.OleDbType = OleDbType.Binary; break;
                                    case "smallint": parametro.OleDbType = OleDbType.SmallInt; break;
                                    case "smallmoney": parametro.OleDbType = OleDbType.Binary; break;
                                    case "text": parametro.OleDbType = OleDbType.Binary; break;
                                    case "timestamp": parametro.OleDbType = OleDbType.Binary; break;
                                    case "tinyint": parametro.OleDbType = OleDbType.TinyInt; break;
                                    case "varbinary": parametro.OleDbType = OleDbType.VarBinary; break;
                                    case "varchar": parametro.OleDbType = OleDbType.VarChar; break;
                                    case "variant": parametro.OleDbType = OleDbType.Variant; break;
                                    case "xml": parametro.OleDbType = OleDbType.Binary; break;
                                    case "udt": parametro.OleDbType = OleDbType.Binary; break;
                                    case "structured": parametro.OleDbType = OleDbType.Binary; break;
                                    case "date": parametro.OleDbType = OleDbType.Date; break;
                                    case "time": parametro.OleDbType = OleDbType.Binary; break;
                                    case "datetime2": parametro.OleDbType = OleDbType.Binary; break;
                                    case "datetimeoffset": parametro.OleDbType = OleDbType.Binary; break;
                                    default:
                                        throw new Exception("Tipo de columna no valido.");
                                }
                            }

                            if (colParametros.Item(p).Longitud != 0) parametro.Size = colParametros.Item(p).Longitud;
                            if (colParametros.Item(p).Valor != null) parametro.Value = colParametros.Item(p).Valor;
                            if (colParametros.Item(p).Direccion != null) parametro.Direction = (ParameterDirection)colParametros.Item(p).Direccion;

                            sqlParams.Add(parametro);
                            //sqlCmdExe.Parameters.Add(parametro);
                        }
                    }

                   /* if (sqlTrans != null)
                        if (sqlTrans.Connection != null)
                            sqlCmdExe.Transaction = sqlTrans;*/

                    if (colParametros != null)
                    {
                        OleDbDataReader dr = this.sqlCmdExe.ExecuteReader();
                        propParametros = new ParametroCollection();

                        for (int p = 0; p <= this.sqlCmdExe.Parameters.Count - 1; p++)
                        {
                            if (this.sqlCmdExe.Parameters[p].Direction != ParameterDirection.Input)
                                this.propParametros.Add(this.sqlCmdExe.Parameters[p].ParameterName, this.sqlCmdExe.Parameters[p].Value);
                        }

                        this.sqlCmdExe.Dispose();
                        return dr;                        
                    }
                    else
                    {
                        this.propFilasAfectadas = this.sqlCmdExe.ExecuteNonQuery();
                    }

                    this.sqlCmdExe.Dispose();
                    return true;
                }                
            }
            catch (Exception ex)
            {
                /*if (sqlTrans != null)
                    if (sqlTrans.Connection != null)
                        sqlTrans.Rollback();*/

                // No hay filas
                this.propFilasAfectadas = -1;

                //ErrorManager em = new ErrorManager(ex);
                throw ex;
                //return false;
            }
        }

        public bool Ejecutar(string consulta)
        {
            return (bool)this.Ejecutar(consulta, null, null);
        }

        public bool Ejecutar(string consulta, ArrayList parametros)
        {
            return (bool)this.Ejecutar(consulta, parametros, null);
        }

        public DbDataReader Ejecutar(string consulta, ParametroCollection parametros)
        {
            return (DbDataReader)this.Ejecutar(consulta, null, parametros);
        }

        public DbDataReader Ejecutar(string consulta, object valor, ParametroCollection parametros)
        {
            return (DbDataReader)this.Ejecutar(consulta, valor, parametros);
        }

        public bool IniciarTransaccion()
        {
            return false;
        }

        public bool DestinarTransaccion()
        {
            return false;
        }

        public bool RestaurarTransaccion()
        {
            return false;
        }
        #endregion
    }
}
