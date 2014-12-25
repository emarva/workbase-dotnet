using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using FirebirdSql.Data.FirebirdClient;
using WorkBase.DataLayer;

namespace DevO2.Connectors
{
    internal sealed class FirebirdConnector : IConnector
    {
        #region Varibales
        private bool _conectado;
        private bool _tieneFilas;
        private int _filasAfectadas;
        private DlParameterCollection _parametros;

        private DlConnectionString dlCS;
        private FbConnection dbCnn;
        private FbTransaction dbTrans;
        private List<FbCommand> dbCmd = new List<FbCommand>();
        private FbCommand dbCmdExec;
        private List<FbDataAdapter> dbDA = new List<FbDataAdapter>();
        private bool disposed;
        #endregion

        #region Constructor
        public FirebirdConnector(DlConnectionString cadenaConexion)
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

                cadenaConexion = "User Id=" + dlCS.Usuario + ";Password=" + dlCS.Contrasena + ";Database=" + dlCS.BaseDatos + ";Data Source=" + dlCS.Host + ";";

                if (dlCS.Puerto != 0)
                    cadenaConexion += "Port=" + dlCS.Puerto + ";";
                
                //cadenaConexion = "Dialect=3;Charset=NONE;Role=;Connection lifetime=15;Pooling=true;MinPoolSize=0;MaxPoolSize=50;Packet Size=8192;ServerType=0;";

                dbCnn = new FbConnection(cadenaConexion);
                dbCnn.Open();
                _conectado = true;
                return true;
            }
            catch
            {
                _conectado = false;
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
            try
            {
                string cadenaConexion = string.Empty;
                int tamanoPagina = 16384;
                bool forzarEscritura = false;
                bool sobreEscribir = false;

                cadenaConexion += "User Id=" + dlCS.Usuario + ";";
                cadenaConexion += "Password=" + dlCS.Contrasena + ";";
                cadenaConexion += "Database=" + nombre + ";";

                if (parametros != null)
                {
                    if (parametros.ContainsKey("PageSize"))
                        tamanoPagina = Convert.ToInt32(parametros["PageSize"]);

                    if (parametros.ContainsKey("ForcedWrite"))
                        forzarEscritura = Convert.ToBoolean(parametros["ForcedWrite"]);

                    if (parametros.ContainsKey("Overwrite"))
                        sobreEscribir = Convert.ToBoolean(parametros["Overwrite"]);
                }

                FbConnection.CreateDatabase(cadenaConexion, tamanoPagina, forzarEscritura, sobreEscribir);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private FbParameter ConvertirParametro(DlParameter parametro)
        {
            FbParameter dbParam = new FbParameter();

            dbParam.ParameterName = parametro.NombreParam;

            switch (parametro.TipoColumna)
            {
                case DlTipoColumna.BigInt: dbParam.FbDbType = FbDbType.BigInt; break;
                case DlTipoColumna.Binary: dbParam.FbDbType = FbDbType.Binary; break;
                case DlTipoColumna.Bit: dbParam.FbDbType = FbDbType.Binary; break;
                case DlTipoColumna.Blob: dbParam.FbDbType = FbDbType.Text; break;
                case DlTipoColumna.Byte: dbParam.FbDbType = FbDbType.Char; break;
                case DlTipoColumna.Char: dbParam.FbDbType = FbDbType.Char; break;
                case DlTipoColumna.Date: dbParam.FbDbType = FbDbType.Date; break;
                case DlTipoColumna.DateTime: dbParam.FbDbType = FbDbType.Date; break;
                case DlTipoColumna.Decimal: dbParam.FbDbType = FbDbType.Decimal; break;
                case DlTipoColumna.Double: dbParam.FbDbType = FbDbType.Double; break;
                case DlTipoColumna.Float: dbParam.FbDbType = FbDbType.Float; break;
                case DlTipoColumna.Image: dbParam.FbDbType = FbDbType.Binary; break;
                case DlTipoColumna.Int: dbParam.FbDbType = FbDbType.Integer; break;
                case DlTipoColumna.MediumBlob: dbParam.FbDbType = FbDbType.Binary; break;
                case DlTipoColumna.MediumInt: dbParam.FbDbType = FbDbType.Integer; break;
                case DlTipoColumna.MediumText: dbParam.FbDbType = FbDbType.Text; break;
                case DlTipoColumna.Money: dbParam.FbDbType = FbDbType.Decimal; break;
                case DlTipoColumna.NChar: dbParam.FbDbType = FbDbType.Char; break;
                case DlTipoColumna.NVarChar: dbParam.FbDbType = FbDbType.VarChar; break;
                case DlTipoColumna.LongBlob: dbParam.FbDbType = FbDbType.Binary; break;
                case DlTipoColumna.LongText: dbParam.FbDbType = FbDbType.Text; break;
                case DlTipoColumna.Real: dbParam.FbDbType = FbDbType.Float; break;
                case DlTipoColumna.SmallDateTime: dbParam.FbDbType = FbDbType.Date; break;
                case DlTipoColumna.SmallInt: dbParam.FbDbType = FbDbType.SmallInt; break;
                case DlTipoColumna.SmallMoney: dbParam.FbDbType = FbDbType.Decimal; break;
                case DlTipoColumna.String: dbParam.FbDbType = FbDbType.VarChar; break;
                case DlTipoColumna.Text: dbParam.FbDbType = FbDbType.Text; break;
                case DlTipoColumna.Time: dbParam.FbDbType = FbDbType.Time; break;
                case DlTipoColumna.TimeStamp: dbParam.FbDbType = FbDbType.TimeStamp; break;
                case DlTipoColumna.TinyBlob: dbParam.FbDbType = FbDbType.Binary; break;
                case DlTipoColumna.TinyInt: dbParam.FbDbType = FbDbType.SmallInt; break;
                case DlTipoColumna.TinyText: dbParam.FbDbType = FbDbType.Text; break;
                case DlTipoColumna.Varchar: dbParam.FbDbType = FbDbType.VarChar; break;
                case DlTipoColumna.VarBinary: dbParam.FbDbType = FbDbType.Binary; break;
                case DlTipoColumna.Xml: dbParam.FbDbType = FbDbType.Text; break;
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

                dbCmdExec = new FbCommand(consulta, dbCnn);

                // Si hay parametros los agrego
                if (_parametros != null && _parametros.Count > 0)
                {
                    this.dbCmdExec.CommandType = CommandType.Text;
                    FbParameterCollection sqlParams = this.dbCmdExec.Parameters;

                    foreach (DlParameter param in this._parametros)
                    {                        
                        this.dbCmdExec.Parameters.Add(this.ConvertirParametro(param));
                    }
                }

                FbDataReader dr = dbCmdExec.ExecuteReader();
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

                dbCmd.Add(new FbCommand(consulta, dbCnn));
                indiceCmd = dbCmd.Count - 1;

                // Si hay parametros los agrego
                if (_parametros != null)
                {
                    dbCmd[indiceCmd].CommandType = CommandType.Text;
                    FbParameterCollection sqlParams = dbCmd[indiceCmd].Parameters;

                    foreach (DlParameter param in this._parametros)
                    {
                        dbCmd[indiceCmd].Parameters.Add(ConvertirParametro(param));
                    }
                }

                dbDA.Add(new FbDataAdapter());
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
                FbCommandBuilder cb = new FbCommandBuilder(dbDA[indiceCmd]);
                
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
                    this.dbCmdExec = new FbCommand(consulta, this.dbCnn);

                    // Si hay parametros los agrego
                    if (this._parametros != null)
                    {
                        this.dbCmdExec.CommandType = CommandType.Text;
                        FbParameterCollection sqlParams = this.dbCmdExec.Parameters;

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
