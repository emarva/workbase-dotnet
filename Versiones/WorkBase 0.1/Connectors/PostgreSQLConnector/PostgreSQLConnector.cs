using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Npgsql;
using WorkBase.DataLayer;
using WorkBase.Shared;

namespace WorkBase.Connectors
{
    internal sealed class PostgreSQLConnector : IConnector
    {
        #region Variables
        private NpgsqlConnection sqlConn;
        private NpgsqlTransaction sqlTrans;
        private NpgsqlCommand sqlCmd;
        private NpgsqlCommand sqlCmdExe;

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
        public PostgreSQLConnector() { }

        public PostgreSQLConnector(string host, string basedatos, string usuario, string clave)
            : this(host, 0, basedatos, usuario, clave)
        {
        }

        public PostgreSQLConnector(string host, int puerto, string basedatos, string usuario, string clave)
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
                    cadenaConexion = "Server=" + this.propHost;
                }

                cadenaConexion += ";Database=" + this.propBaseDatos + ";";

                /*if (propSeguridadIntegrada == true)
                {
                    cadenaConexion += "Integrated Security=SSPI;";
                }
                else
                {*/
                cadenaConexion += "User ID=" + this.propUsuario + ";Password=" + this.propClave;
                //}

                /*using (sqlConn = new SqlConnection(cadenaConexion))
                {*/
                this.sqlConn = new NpgsqlConnection(cadenaConexion);
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

                this.sqlCmd = new NpgsqlCommand(consulta, this.sqlConn);

                // Si hay parametros los agrego
                if (parametros != null)
                {
                    this.sqlCmd.CommandType = CommandType.Text;

                    foreach (NpgsqlParameter p in parametros)
                    {
                        this.sqlCmd.Parameters.Add(p);
                    }
                }

                if (this.sqlTrans != null)
                    if (this.sqlTrans.Connection != null)
                        this.sqlCmd.Transaction = this.sqlTrans;

                NpgsqlDataReader sqlDR = this.sqlCmd.ExecuteReader();
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
                    this.sqlCmdExe = new NpgsqlCommand(consulta, this.sqlConn);

                    // Si hay parametros los agrego
                    if (parametros != null)
                    {
                        this.sqlCmdExe.CommandType = CommandType.Text;

                        foreach (NpgsqlParameter p in parametros)
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
                    this.sqlCmdExe = new NpgsqlCommand(consulta, this.sqlConn);

                    // Si hay parametros los agrego
                    if (colParametros != null)
                    {
                        this.sqlCmdExe.CommandType = CommandType.StoredProcedure;
                        NpgsqlParameterCollection sqlParams = this.sqlCmdExe.Parameters;

                        for (int p = 0; p <= colParametros.Count - 1; p++)
                        {
                            NpgsqlParameter parametro = new NpgsqlParameter();

                            parametro.ParameterName = colParametros.Item(p).Nombre;

                            // Busco el tipo de columna
                            if (colParametros.Item(p).TipoColumna != "")
                            {
                                switch (colParametros.Item(p).TipoColumna.ToLower())
                                {
                                    case "array": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
                                    case "bigint": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
                                    case "boolean": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
                                    case "box": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
                                    case "Bytea": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
                                    case "circle": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
                                    case "char": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
                                    case "date": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
                                    case "double": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
                                    case "integer": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
                                    case "line": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
                                    case "lseg": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
                                    case "money": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
                                    case "numeric": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
                                    case "path": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
                                    case "point": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
                                    case "polygon": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
                                    case "real": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
                                    case "smallint": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
                                    case "text": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
                                    case "time": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
                                    case "timestamp": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
                                    case "varchar": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
                                    case "refcursor": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
                                    case "inet": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
                                    case "bit": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
                                    case "timestamptz": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
                                    case "uuid": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
                                    case "xml": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
                                    case "oidvector": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
                                    case "interval": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
                                    case "timetz": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
                                    case "name": parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array; break;
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
                        NpgsqlDataReader dr = this.sqlCmdExe.ExecuteReader();
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
