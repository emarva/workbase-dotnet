using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using WorkBase.Shared;

namespace WorkBase.DataLayer
{
    internal sealed class MSSQLConnector : IConnector
    {
        #region Varibales
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

        private string cadenaConexion = string.Empty;
        private SqlConnection sqlConn;
        private SqlCommand sqlCmd;
        private SqlCommand sqlCmdExe;
        private SqlTransaction sqlTrans;
        private string baseDatosOriginal;
        private string mCadenaConexion;
        #endregion

        #region Constructores
        public MSSQLConnector(string host, int puerto, string basedatos, string usuario, string clave)
        {
            this.propHost = host;
            this.propPuerto = puerto;
            this.propBaseDatos = basedatos;
            this.propUsuario = usuario;
            this.propClave = clave;
        }

        public MSSQLConnector(string host, string basedatos, string usuario, string clave)
            : this(host, 0, basedatos, usuario, clave)
        {
        }

        public MSSQLConnector(string cadena)
        {
            mCadenaConexion = cadena;
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
                if (string.IsNullOrEmpty(mCadenaConexion))
                {
                    if (this.propPuerto != 0)
                    {
                        this.cadenaConexion = "Server=" + this.propHost + "," + this.propPuerto;
                    }
                    else
                    {
                        this.cadenaConexion = "Data Source=" + this.propHost;
                    }

                    this.cadenaConexion += ";Database=" + this.propBaseDatos + ";";

                    if (this.propSeguridadIntegrada == true)
                    {
                        this.cadenaConexion += "Integrated Security=SSPI;";
                    }
                    else
                    {
                        this.cadenaConexion += "User ID=" + this.propUsuario + ";Password=" + this.propClave;
                    }
                }
                else
                {
                    cadenaConexion = mCadenaConexion;
                }

                /*using (sqlConn = new SqlConnection(cadenaConexion))
                {*/
                this.sqlConn = new SqlConnection(this.cadenaConexion);
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

                this.sqlCmd = new SqlCommand(consulta, this.sqlConn);

                // Si hay parametros los agrego
                if (parametros != null)
                {
                    this.sqlCmd.CommandType = CommandType.Text;
                    
                    foreach (SqlParameter p in parametros)
                    {
                        this.sqlCmd.Parameters.Add(p);
                    }
                }

                if (this.sqlTrans != null)
                    if (this.sqlTrans.Connection != null)
                        sqlCmd.Transaction = this.sqlTrans;

                SqlDataReader sqlDR = this.sqlCmd.ExecuteReader();
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
                    this.sqlCmdExe = new SqlCommand(consulta, this.sqlConn);

                    // Si hay parametros los agrego
                    if (parametros != null)
                    {
                        this.sqlCmdExe.CommandType = CommandType.Text;

                        foreach (SqlParameter p in parametros)
                        {
                            this.sqlCmdExe.Parameters.Add(p);
                        }
                    }

                    if (this.sqlTrans != null)
                        if (this.sqlTrans.Connection != null)
                            this.sqlCmdExe.Transaction = this.sqlTrans;

                    // FilasAfectadas con valor 0 significa no hay filas o se produjo un error
                    this.propFilasAfectadas = this.sqlCmdExe.ExecuteNonQuery();
                    this.sqlCmdExe.Dispose();

                    return true;
                }
                else
                {
                    this.sqlCmdExe = new SqlCommand(consulta, this.sqlConn);

                    // Si hay parametros los agrego
                    if (colParametros != null)
                    {
                        this.sqlCmdExe.CommandType = CommandType.StoredProcedure;
                        SqlParameterCollection sqlParams = this.sqlCmdExe.Parameters;

                        for (int p = 0; p <= colParametros.Count - 1; p++)
                        {
                            SqlParameter parametro = new SqlParameter();

                            parametro.ParameterName = colParametros.Item(p).Nombre;

                            // Busco el tipo de columna
                            if (colParametros.Item(p).TipoColumna != "")
                            {
                                switch (colParametros.Item(p).TipoColumna.ToLower())
                                {
                                    case "bigint": parametro.SqlDbType = SqlDbType.BigInt; break;
                                    case "binary": parametro.SqlDbType = SqlDbType.Binary; break;
                                    case "bit": parametro.SqlDbType = SqlDbType.Bit; break;
                                    case "char": parametro.SqlDbType = SqlDbType.Char; break;
                                    case "datetime": parametro.SqlDbType = SqlDbType.DateTime; break;
                                    case "decimal": parametro.SqlDbType = SqlDbType.Decimal; break;
                                    case "float": parametro.SqlDbType = SqlDbType.Float; break;
                                    case "image": parametro.SqlDbType = SqlDbType.Image; break;
                                    case "int": parametro.SqlDbType = SqlDbType.Int; break;
                                    case "money": parametro.SqlDbType = SqlDbType.Money; break;
                                    case "nchar": parametro.SqlDbType = SqlDbType.NChar; break;
                                    case "nvarchar": parametro.SqlDbType = SqlDbType.NVarChar; break;
                                    case "real": parametro.SqlDbType = SqlDbType.Real; break;
                                    case "uniqueidentifier": parametro.SqlDbType = SqlDbType.UniqueIdentifier; break;
                                    case "smalldatetime": parametro.SqlDbType = SqlDbType.SmallDateTime; break;
                                    case "smallint": parametro.SqlDbType = SqlDbType.SmallInt; break;
                                    case "smallmoney": parametro.SqlDbType = SqlDbType.SmallMoney; break;
                                    case "text": parametro.SqlDbType = SqlDbType.Text; break;
                                    case "timestamp": parametro.SqlDbType = SqlDbType.Timestamp; break;
                                    case "tinyint": parametro.SqlDbType = SqlDbType.TinyInt; break;
                                    case "varbinary": parametro.SqlDbType = SqlDbType.VarBinary; break;
                                    case "varchar": parametro.SqlDbType = SqlDbType.VarChar; break;
                                    case "variant": parametro.SqlDbType = SqlDbType.Variant; break;
                                    case "xml": parametro.SqlDbType = SqlDbType.Xml; break;
                                    case "udt": parametro.SqlDbType = SqlDbType.Udt; break;
                                    case "structured": parametro.SqlDbType = SqlDbType.Structured; break;
                                    case "date": parametro.SqlDbType = SqlDbType.Date; break;
                                    case "time": parametro.SqlDbType = SqlDbType.Time; break;
                                    case "datetime2": parametro.SqlDbType = SqlDbType.DateTime2; break;
                                    case "datetimeoffset": parametro.SqlDbType = SqlDbType.DateTimeOffset; break;
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

                    if (this.sqlTrans != null && colParametros == null)
                        if (this.sqlTrans.Connection != null)
                            this.sqlCmdExe.Transaction = this.sqlTrans;

                    if (colParametros != null)
                    {
                        SqlDataReader dr = this.sqlCmdExe.ExecuteReader();
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
                if (this.sqlTrans != null)
                    if (this.sqlTrans.Connection != null)
                        this.sqlTrans.Rollback();

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
            try
            {
                this.sqlTrans = sqlConn.BeginTransaction();
                //this.sqlCmdExe.Transaction = this.sqlTrans;

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }
        }

        public bool DestinarTransaccion()
        {
            try
            {
                if (this.sqlTrans != null)
                    if (this.sqlTrans.Connection == null)
                        return false;

                this.sqlTrans.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }
        }

        public bool RestaurarTransaccion()
        {
            try
            {
                if (this.sqlTrans != null)
                    if (this.sqlTrans.Connection == null)
                        return false;

                this.sqlTrans.Rollback();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
