using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using DevO2.Shared;

namespace DevO2.DataLayer
{
    internal sealed class AccessConnector : IConnector
    {
        #region Varibales
        private bool _conectado;
        private bool _tieneFilas;
        private int _filasAfectadas;
        private DlParameterCollection _parametros;

        private DlConnectionString dlCS;
        private OleDbConnection dbCnn;
        private OleDbTransaction dbTrans;
        private List<OleDbCommand> dbCmd = new List<OleDbCommand>();
        private OleDbCommand dbCmdExec;
        private List<OleDbDataAdapter> dbDA = new List<OleDbDataAdapter>();
        private bool disposed;
        #endregion

        #region Constructor
        public AccessConnector(DlConnectionString cadenaConexion)
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

                cadenaConexion = "Provider=Microsoft.Jet.OLEDB.4.0;Data source=" + dlCS.BaseDatos;

                if (string.IsNullOrEmpty(dlCS.Contrasena))
                    cadenaConexion += ";User Id=admin;Password=;";
                else
                    cadenaConexion += ";Jet OLEDB:Database Password=" + dlCS.Contrasena + ";";
                                
                this.dbCnn = new OleDbConnection(cadenaConexion);
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

        private OleDbParameter ConvertirParametro(DlParameter parametro)
        {
            OleDbParameter dbParam = new OleDbParameter();

            dbParam.ParameterName = parametro.NombreParam;

            switch (parametro.TipoColumna)
            {
                case DlTipoColumna.BigInt: dbParam.OleDbType = OleDbType.BigInt; break;
                case DlTipoColumna.Binary: dbParam.OleDbType = OleDbType.Binary; break;
                case DlTipoColumna.Bit: dbParam.OleDbType = OleDbType.Binary; break;
                case DlTipoColumna.Blob: dbParam.OleDbType = OleDbType.Binary; break;
                case DlTipoColumna.Byte: dbParam.OleDbType = OleDbType.Char; break;
                case DlTipoColumna.Char: dbParam.OleDbType = OleDbType.Char; break;
                case DlTipoColumna.Date: dbParam.OleDbType = OleDbType.Date; break;
                case DlTipoColumna.DateTime: dbParam.OleDbType = OleDbType.DBDate; break;
                case DlTipoColumna.Decimal: dbParam.OleDbType = OleDbType.Decimal; break;
                case DlTipoColumna.Double: dbParam.OleDbType = OleDbType.Double; break;
                case DlTipoColumna.Float: dbParam.OleDbType = OleDbType.Double; break;
                case DlTipoColumna.Image: dbParam.OleDbType = OleDbType.VarChar; break;
                case DlTipoColumna.Int: dbParam.OleDbType = OleDbType.Integer; break;
                case DlTipoColumna.MediumBlob: dbParam.OleDbType = OleDbType.Binary; break;
                case DlTipoColumna.MediumInt: dbParam.OleDbType = OleDbType.SmallInt; break;
                case DlTipoColumna.MediumText: dbParam.OleDbType = OleDbType.VarChar; break;
                case DlTipoColumna.Money: dbParam.OleDbType = OleDbType.Currency; break;
                case DlTipoColumna.NChar: dbParam.OleDbType = OleDbType.WChar; break;
                case DlTipoColumna.NVarChar: dbParam.OleDbType = OleDbType.VarWChar; break;
                case DlTipoColumna.LongBlob: dbParam.OleDbType = OleDbType.Binary; break;
                case DlTipoColumna.LongText: dbParam.OleDbType = OleDbType.VarChar; break;
                case DlTipoColumna.Real: dbParam.OleDbType = OleDbType.Double; break;
                case DlTipoColumna.SmallDateTime: dbParam.OleDbType = OleDbType.DBDate; break;
                case DlTipoColumna.SmallInt: dbParam.OleDbType = OleDbType.SmallInt; break;
                case DlTipoColumna.SmallMoney: dbParam.OleDbType = OleDbType.Currency; break;
                case DlTipoColumna.String: dbParam.OleDbType = OleDbType.VarChar; break;
                case DlTipoColumna.Text: dbParam.OleDbType = OleDbType.VarChar; break;
                case DlTipoColumna.Time: dbParam.OleDbType = OleDbType.DBTime; break;
                case DlTipoColumna.TimeStamp: dbParam.OleDbType = OleDbType.DBTimeStamp; break;
                case DlTipoColumna.TinyBlob: dbParam.OleDbType = OleDbType.Binary; break;
                case DlTipoColumna.TinyInt: dbParam.OleDbType = OleDbType.SmallInt; break;
                case DlTipoColumna.TinyText: dbParam.OleDbType = OleDbType.VarChar; break;
                case DlTipoColumna.Varchar: dbParam.OleDbType = OleDbType.VarChar; break;
                case DlTipoColumna.VarBinary: dbParam.OleDbType = OleDbType.VarBinary; break;
                case DlTipoColumna.Xml: dbParam.OleDbType = OleDbType.VarChar; break;
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

                dbCmdExec = new OleDbCommand(consulta, dbCnn);

                // Si hay parametros los agrego
                if (_parametros != null && _parametros.Count > 0)
                {
                    this.dbCmdExec.CommandType = CommandType.Text;
                    OleDbParameterCollection sqlParams = this.dbCmdExec.Parameters;

                    foreach (DlParameter param in this._parametros)
                    {                        
                        this.dbCmdExec.Parameters.Add(this.ConvertirParametro(param));
                    }
                }

                OleDbDataReader dr = dbCmdExec.ExecuteReader();
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

                dbCmd.Add(new OleDbCommand(consulta, dbCnn));
                indiceCmd = dbCmd.Count - 1;

                // Si hay parametros los agrego
                if (_parametros != null)
                {
                    dbCmd[indiceCmd].CommandType = CommandType.Text;
                    OleDbParameterCollection sqlParams = dbCmd[indiceCmd].Parameters;

                    foreach (DlParameter param in this._parametros)
                    {
                        dbCmd[indiceCmd].Parameters.Add(ConvertirParametro(param));
                    }
                }

                dbDA.Add(new OleDbDataAdapter());
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
                OleDbCommandBuilder cb = new OleDbCommandBuilder(dbDA[indiceCmd]);
                
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
                    this.dbCmdExec = new OleDbCommand(consulta, this.dbCnn);

                    // Si hay parametros los agrego
                    if (this._parametros != null)
                    {
                        this.dbCmdExec.CommandType = CommandType.Text;
                        OleDbParameterCollection sqlParams = this.dbCmdExec.Parameters;

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
