using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Npgsql;
using WorkBase.DataLayer;

namespace DevO2.Connectors
{
    internal sealed class PostgreSQLConnector : IConnector
    {
        #region Varibales
        private bool _conectado;
        private bool _tieneFilas;
        private int _filasAfectadas;
        private DlParameterCollection _parametros;

        private DlConnectionString dlCS;
        private NpgsqlConnection dbCnn;
        private NpgsqlTransaction dbTrans;
        private List<NpgsqlCommand> dbCmd = new List<NpgsqlCommand>();
        private NpgsqlCommand dbCmdExec;
        private List<NpgsqlDataAdapter> dbDA = new List<NpgsqlDataAdapter>();
        private bool disposed;
        #endregion

        #region Constructor
        public PostgreSQLConnector(DlConnectionString cadenaConexion)
        {
            dlCS = cadenaConexion;
        }
        #endregion

        #region Propiedades
        public bool Conectado
        {
            get { return this._conectado; }
        }

        public bool TieneFilas
        {
            get { return this._tieneFilas; }
        }

        public int FilasAfectadas
        {
            get { return this._filasAfectadas; }
        }

        public DlParameterCollection Parametros
        {
            get 
            {
                if (_parametros == null)
                    _parametros = new DlParameterCollection();

                return _parametros; 
            }
            set { _parametros = value; }
        }
        #endregion

        #region Metodos
        public bool Conectar()
        {
            try
            {
                string cadenaConexion = string.Empty;

                if (dlCS.Puerto != 0)
                    cadenaConexion = "Server=" + dlCS.Host + ";Port=" + dlCS.Puerto;
                else
                    cadenaConexion = "Server=" + dlCS.Host;

                cadenaConexion += ";Database=" + dlCS.BaseDatos + ";";

                if (dlCS.Usuario == null)
                    cadenaConexion += "Integrated Security=true;";
                else
                    cadenaConexion += "User ID=" + dlCS.Usuario + ";Password=" + dlCS.Contrasena + ";";

                this.dbCnn = new NpgsqlConnection(cadenaConexion);
                this.dbCnn.Open();
                this._conectado = true;
                return true;
            }
            catch
            {
                this._conectado = false;
                return false;
            }
        }

        public bool Desconectar()
        {
            try
            {
                this.dbCnn.Close();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                _conectado = false;
                _parametros = null;
                dbTrans.Dispose();
                dbCmd.Clear();
                dbCmdExec.Dispose();                
            }
        }

        public void CrearBaseDatos(string nombre, Hashtable parametros)
        {

        }

        private NpgsqlParameter ConvertirParametro(DlParameter parametro)
        {
            NpgsqlParameter dbParam = new NpgsqlParameter();

            dbParam.ParameterName = parametro.NombreParam;

            switch (parametro.TipoColumna)
            {
                case DlTipoColumna.BigInt: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bigint; break;
                case DlTipoColumna.Binary: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bytea; break;
                case DlTipoColumna.Bit: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bit; break;
                case DlTipoColumna.Blob: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bytea; break;
                case DlTipoColumna.Byte: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bytea; break;
                case DlTipoColumna.Char: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Char; break;
                case DlTipoColumna.Date: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Date; break;
                case DlTipoColumna.DateTime: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Date; break;
                case DlTipoColumna.Decimal: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Double; break;
                case DlTipoColumna.Double: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Double; break;
                case DlTipoColumna.Float: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Real; break;
                case DlTipoColumna.Image: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Text; break;
                case DlTipoColumna.Int: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer; break;
                case DlTipoColumna.MediumBlob: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bytea; break;
                case DlTipoColumna.MediumInt: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Smallint; break;
                case DlTipoColumna.MediumText: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Text; break;
                case DlTipoColumna.Money: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Money; break;
                case DlTipoColumna.NChar: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Char; break;
                case DlTipoColumna.NVarChar: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar; break;
                case DlTipoColumna.LongBlob: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bytea; break;
                case DlTipoColumna.LongText: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Text; break;
                case DlTipoColumna.Real: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Real; break;
                case DlTipoColumna.SmallDateTime: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Date; break;
                case DlTipoColumna.SmallInt: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Smallint; break;
                case DlTipoColumna.SmallMoney: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Money; break;
                case DlTipoColumna.String: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar; break;
                case DlTipoColumna.Text: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Text; break;
                case DlTipoColumna.Time: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Time; break;
                case DlTipoColumna.TimeStamp: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Timestamp; break;
                case DlTipoColumna.TinyBlob: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bytea; break;
                case DlTipoColumna.TinyInt: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Smallint; break;
                case DlTipoColumna.TinyText: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Text; break;
                case DlTipoColumna.Varchar: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar; break;
                case DlTipoColumna.VarBinary: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bytea; break;
                case DlTipoColumna.Xml: dbParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Xml; break;
            }
            
            if (parametro.Longitud != 0) dbParam.Size = parametro.Longitud;
            if (parametro.Direccion != null) dbParam.Direction = (ParameterDirection)parametro.Direccion;
            if (parametro.ColumnaFuente != string.Empty) dbParam.SourceColumn = parametro.ColumnaFuente;
            if (parametro.Valor != null) dbParam.Value = parametro.Valor;

            return dbParam;
        }

        public DbDataReader ObtenerDataReader(string consulta)
        {
            try
            {
                if (!consulta.ToLower().Contains("select"))
                    return null;

                dbCmdExec = new NpgsqlCommand(consulta, dbCnn);

                // Si hay parametros los agrego
                if (_parametros != null && _parametros.Count > 0)
                {
                    this.dbCmdExec.CommandType = CommandType.Text;
                    NpgsqlParameterCollection sqlParams = this.dbCmdExec.Parameters;

                    foreach (DlParameter param in this._parametros)
                    {                        
                        this.dbCmdExec.Parameters.Add(this.ConvertirParametro(param));
                    }
                }

                NpgsqlDataReader dr = dbCmdExec.ExecuteReader();
                _tieneFilas = dr.HasRows;
                dbCmdExec.Dispose();

                // Filas Afectadas para un SELECT siempre es -1
                _filasAfectadas = -1;
                return dr;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        public DbDataAdapter ObtenerDataAdapter(string consulta, ref int indiceCmd)
        {
            try
            {
                if (!consulta.ToLower().Contains("select"))
                    return null;

                dbCmd.Add(new NpgsqlCommand(consulta, dbCnn));
                indiceCmd = dbCmd.Count - 1;

                // Si hay parametros los agrego
                if (_parametros != null)
                {
                    dbCmd[indiceCmd].CommandType = CommandType.Text;
                    NpgsqlParameterCollection sqlParams = dbCmd[indiceCmd].Parameters;

                    foreach (DlParameter param in this._parametros)
                    {
                        dbCmd[indiceCmd].Parameters.Add(ConvertirParametro(param));
                    }
                }

                dbDA.Add(new NpgsqlDataAdapter());
                dbDA[indiceCmd].SelectCommand = dbCmd[indiceCmd];
              
                // Filas Afectadas para un SELECT siempre es -1
                this._filasAfectadas = -1;
                return dbDA[indiceCmd];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarObjetoDatos(int indiceCmd, object objetoDatos)
        {
            try
            {
                NpgsqlCommandBuilder cb = new NpgsqlCommandBuilder(dbDA[indiceCmd]);
                
                if (objetoDatos.GetType().Name == "DataSet")
                    dbDA[indiceCmd].Update((DataSet)objetoDatos);
                else if (objetoDatos.GetType().Name == "DataTable")
                    dbDA[indiceCmd].Update((DataTable)objetoDatos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Ejecutar(string consulta)
        {
            try
            {
                if (consulta.ToLower().Contains("insert") || consulta.ToLower().Contains("update") ||
                    consulta.ToLower().Contains("delete"))
                {
                    this.dbCmdExec = new NpgsqlCommand(consulta, this.dbCnn);

                    // Si hay parametros los agrego
                    if (this._parametros != null)
                    {
                        this.dbCmdExec.CommandType = CommandType.Text;
                        NpgsqlParameterCollection sqlParams = this.dbCmdExec.Parameters;

                        foreach (DlParameter param in this._parametros)
                        {
                            this.dbCmdExec.Parameters.Add(this.ConvertirParametro(param));
                        }
                    }

                    if (this.dbTrans != null)
                        if (this.dbTrans.Connection != null)
                            this.dbCmdExec.Transaction = this.dbTrans;

                    // FilasAfectadas con valor 0, significa que no hay filas afectadas o se produjo un error
                    this._filasAfectadas = this.dbCmdExec.ExecuteNonQuery();
                    this.dbCmdExec.Dispose();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                if (this.dbTrans != null)
                    if (this.dbTrans.Connection != null)
                        this.dbTrans.Rollback();

                // No hay filas
                this._filasAfectadas = -1;

                throw ex;
            }
        }

        public bool IniciarTransaccion()
        {
            try
            {
                this.dbTrans = dbCnn.BeginTransaction();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DestinarTransaccion()
        {
            try
            {
                if (this.dbTrans != null)
                    if (this.dbTrans.Connection == null)
                        return false;

                this.dbTrans.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RestaurarTransaccion()
        {
            try
            {
                if (this.dbTrans != null)
                    if (this.dbTrans.Connection == null)
                        return false;

                this.dbTrans.Rollback();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region IDisposable
        private void Dispose(bool disposing)
        {
            if (disposing && !disposed)
            {
                _parametros.Clear();
                dlCS = null;
                dbCnn.Dispose();
                dbTrans.Dispose();
                dbCmd.Clear();
                dbCmdExec.Dispose();
                dbDA.Clear();
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
