using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
using WorkBase.DataLayer;
using WorkBase.Shared;

namespace WorkBase.Connectors
{
    internal sealed class MySQLConnector : IConnector
    {
        #region Variables
        private MySqlConnection sqlConn;
        private MySqlTransaction sqlTrans;
        private MySqlCommand sqlCmd;
        private MySqlCommand sqlCmdExe;

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
        public MySQLConnector() { }

        public MySQLConnector(string host, string basedatos, string usuario, string clave)
            : this(host, 0, basedatos, usuario, clave)
        {
        }

        public MySQLConnector(string host, int puerto, string basedatos, string usuario, string clave)
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
                    cadenaConexion = "Server=" + this.propHost + ";Port=" + this.propPuerto;
                }
                else
                {
                    cadenaConexion = "Data Source=" + this.propHost;
                }

                cadenaConexion += ";Database=" + this.propBaseDatos + ";";

                /*if (propSeguridadIntegrada == true)
                {
                    cadenaConexion += "Integrated Security=SSPI;";
                }
                else
                {*/
                cadenaConexion += "User ID=" + this.propUsuario + ";Password=" + this.propClave + ";Persist Security Info=False";
                //}

                /*using (sqlConn = new SqlConnection(cadenaConexion))
                {*/
                this.sqlConn = new MySqlConnection(cadenaConexion);
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

                this.sqlCmd = new MySqlCommand(consulta, this.sqlConn);

                // Si hay parametros los agrego
                if (parametros != null)
                {
                    this.sqlCmd.CommandType = CommandType.Text;

                    foreach (MySqlParameter p in parametros)
                    {
                        this.sqlCmd.Parameters.Add(p);
                    }
                }

                if (this.sqlTrans != null)
                    if (this.sqlTrans.Connection != null)
                        this.sqlCmd.Transaction = this.sqlTrans;

                MySqlDataReader sqlDR = this.sqlCmd.ExecuteReader();
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
                    this.sqlCmdExe = new MySqlCommand(consulta, this.sqlConn);

                    // Si hay parametros los agrego
                    if (parametros != null)
                    {
                        this.sqlCmdExe.CommandType = CommandType.Text;

                        foreach (MySqlParameter p in parametros)
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
                    this.sqlCmdExe = new MySqlCommand(consulta, this.sqlConn);

                    // Si hay parametros los agrego
                    if (colParametros != null)
                    {
                        this.sqlCmdExe.CommandType = CommandType.StoredProcedure;
                        MySqlParameterCollection sqlParams = this.sqlCmdExe.Parameters;

                        for (int p = 0; p <= colParametros.Count - 1; p++)
                        {
                            MySqlParameter parametro = new MySqlParameter();

                            parametro.ParameterName = colParametros.Item(p).Nombre;

                            // Busco el tipo de columna
                            if (colParametros.Item(p).TipoColumna != "")
                            {
                                switch (colParametros.Item(p).TipoColumna.ToLower())
                                {
                                    case "decimal": parametro.MySqlDbType = MySqlDbType.Decimal; break;
                                    case "byte": parametro.MySqlDbType = MySqlDbType.Byte; break;
                                    case "int16": parametro.MySqlDbType = MySqlDbType.Int16; break;
                                    case "int24": parametro.MySqlDbType = MySqlDbType.Int24; break;
                                    case "int32": parametro.MySqlDbType = MySqlDbType.Int32; break;
                                    case "int64": parametro.MySqlDbType = MySqlDbType.Int64; break;
                                    case "float": parametro.MySqlDbType = MySqlDbType.Float; break;
                                    case "double": parametro.MySqlDbType = MySqlDbType.Double; break;
                                    case "timestamp": parametro.MySqlDbType = MySqlDbType.Timestamp; break;
                                    case "date": parametro.MySqlDbType = MySqlDbType.Date; break;
                                    case "time": parametro.MySqlDbType = MySqlDbType.Time; break;
                                    case "datetime": parametro.MySqlDbType = MySqlDbType.DateTime; break;
                                    case "year": parametro.MySqlDbType = MySqlDbType.Year; break;
                                    case "newdate": parametro.MySqlDbType = MySqlDbType.Newdate; break;
                                    case "varstring": parametro.MySqlDbType = MySqlDbType.VarString; break;
                                    case "bit": parametro.MySqlDbType = MySqlDbType.Bit; break;
                                    case "newdecimal": parametro.MySqlDbType = MySqlDbType.NewDecimal; break;
                                    case "enum": parametro.MySqlDbType = MySqlDbType.Enum; break;
                                    case "set": parametro.MySqlDbType = MySqlDbType.Set; break;
                                    case "tinyblob": parametro.MySqlDbType = MySqlDbType.TinyBlob; break;
                                    case "mediumblob": parametro.MySqlDbType = MySqlDbType.MediumBlob; break;
                                    case "longblob": parametro.MySqlDbType = MySqlDbType.LongBlob; break;
                                    case "blob": parametro.MySqlDbType = MySqlDbType.Blob; break;
                                    case "varchar": parametro.MySqlDbType = MySqlDbType.VarChar; break;
                                    case "string": parametro.MySqlDbType = MySqlDbType.String; break;
                                    case "geometry": parametro.MySqlDbType = MySqlDbType.Geometry; break;
                                    case "ubyte": parametro.MySqlDbType = MySqlDbType.UByte; break;
                                    case "uint16": parametro.MySqlDbType = MySqlDbType.UInt16; break;
                                    case "uint24": parametro.MySqlDbType = MySqlDbType.UInt24; break;
                                    case "uint32": parametro.MySqlDbType = MySqlDbType.UInt32; break;
                                    case "uint64": parametro.MySqlDbType = MySqlDbType.UInt64; break;
                                    case "binary": parametro.MySqlDbType = MySqlDbType.Binary; break;
                                    case "varbinary": parametro.MySqlDbType = MySqlDbType.VarBinary; break;
                                    case "tinytext": parametro.MySqlDbType = MySqlDbType.TinyText; break;
                                    case "mediumtext": parametro.MySqlDbType = MySqlDbType.MediumText; break;
                                    case "longtext": parametro.MySqlDbType = MySqlDbType.LongText; break;
                                    case "text": parametro.MySqlDbType = MySqlDbType.Text; break;
                                    case "guid": parametro.MySqlDbType = MySqlDbType.Guid; break;
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

                    if (this.sqlTrans != null)
                        if (this.sqlTrans.Connection != null)
                            this.sqlCmdExe.Transaction = this.sqlTrans;

                    if (colParametros != null)
                    {
                        MySqlDataReader dr = this.sqlCmdExe.ExecuteReader();
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
                this.sqlTrans = this.sqlConn.BeginTransaction();
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
