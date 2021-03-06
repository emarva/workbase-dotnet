﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using DevO2.Shared;

namespace DevO2.DataLayer
{
    internal sealed class MSSQLConnector : IConnector
    {
        #region Varibales
        private bool _conectado;
        private bool _tieneFilas;
        private int _filasAfectadas;
        private DlParameterCollection _parametros;

        private DlConnectionString dlCS;
        private SqlConnection dbCnn;
        private SqlTransaction dbTrans;
        private List<SqlCommand> dbCmd = new List<SqlCommand>();
        private SqlCommand dbCmdExec;
        private List<SqlDataAdapter> dbDA = new List<SqlDataAdapter>();
        private bool disposed;
        #endregion

        #region Constructor
        public MSSQLConnector(DlConnectionString cadenaConexion)
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
                    cadenaConexion = "Server=" + dlCS.Host + "," + dlCS.Puerto;
                else
                    cadenaConexion = "Data Source=" + dlCS.Host;

                cadenaConexion += ";Database=" + dlCS.BaseDatos + ";";

                if (dlCS.Usuario == null)
                    cadenaConexion += "Integrated Security=SSPI;";
                else
                    cadenaConexion += "User ID=" + dlCS.Usuario + ";Password=" + dlCS.Contrasena + ";";

                this.dbCnn = new SqlConnection(cadenaConexion);
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

        private SqlParameter ConvertirParametro(DlParameter parametro)
        {
            SqlParameter dbParam = new SqlParameter();

            dbParam.ParameterName = parametro.NombreParam;

            switch (parametro.TipoColumna)
            {
                case DlTipoColumna.BigInt: dbParam.SqlDbType = SqlDbType.BigInt; break;
                case DlTipoColumna.Binary: dbParam.SqlDbType = SqlDbType.Binary; break;
                case DlTipoColumna.Bit: dbParam.SqlDbType = SqlDbType.Bit; break;
                case DlTipoColumna.Blob: dbParam.SqlDbType = SqlDbType.Binary; break;
                case DlTipoColumna.Byte: dbParam.SqlDbType = SqlDbType.Binary; break;
                case DlTipoColumna.Char: dbParam.SqlDbType = SqlDbType.Char; break;
                case DlTipoColumna.Date: dbParam.SqlDbType = SqlDbType.Date; break;
                case DlTipoColumna.DateTime: dbParam.SqlDbType = SqlDbType.DateTime; break;
                case DlTipoColumna.Decimal: dbParam.SqlDbType = SqlDbType.Decimal; break;
                case DlTipoColumna.Double: dbParam.SqlDbType = SqlDbType.Float; break;
                case DlTipoColumna.Float: dbParam.SqlDbType = SqlDbType.Float; break;
                case DlTipoColumna.Image: dbParam.SqlDbType = SqlDbType.Image; break;
                case DlTipoColumna.Int: dbParam.SqlDbType = SqlDbType.Int; break;
                case DlTipoColumna.MediumBlob: dbParam.SqlDbType = SqlDbType.Binary; break;
                case DlTipoColumna.MediumInt: dbParam.SqlDbType = SqlDbType.SmallInt; break;
                case DlTipoColumna.MediumText: dbParam.SqlDbType = SqlDbType.Text; break;
                case DlTipoColumna.Money: dbParam.SqlDbType = SqlDbType.Money; break;
                case DlTipoColumna.NChar: dbParam.SqlDbType = SqlDbType.NChar; break;
                case DlTipoColumna.NVarChar: dbParam.SqlDbType = SqlDbType.NVarChar; break;
                case DlTipoColumna.LongBlob: dbParam.SqlDbType = SqlDbType.Binary; break;
                case DlTipoColumna.LongText: dbParam.SqlDbType = SqlDbType.Text; break;
                case DlTipoColumna.Real: dbParam.SqlDbType = SqlDbType.Real; break;
                case DlTipoColumna.SmallDateTime: dbParam.SqlDbType = SqlDbType.SmallDateTime; break;
                case DlTipoColumna.SmallInt: dbParam.SqlDbType = SqlDbType.SmallInt; break;
                case DlTipoColumna.SmallMoney: dbParam.SqlDbType = SqlDbType.SmallMoney; break;
                case DlTipoColumna.String: dbParam.SqlDbType = SqlDbType.Text; break;
                case DlTipoColumna.Text: dbParam.SqlDbType = SqlDbType.Text; break;
                case DlTipoColumna.Time: dbParam.SqlDbType = SqlDbType.Time; break;
                case DlTipoColumna.TimeStamp: dbParam.SqlDbType = SqlDbType.Timestamp; break;
                case DlTipoColumna.TinyBlob: dbParam.SqlDbType = SqlDbType.Binary; break;
                case DlTipoColumna.TinyInt: dbParam.SqlDbType = SqlDbType.TinyInt; break;
                case DlTipoColumna.TinyText: dbParam.SqlDbType = SqlDbType.Text; break;
                case DlTipoColumna.Varchar: dbParam.SqlDbType = SqlDbType.VarChar; break;
                case DlTipoColumna.VarBinary: dbParam.SqlDbType = SqlDbType.VarBinary; break;
                case DlTipoColumna.Xml: dbParam.SqlDbType = SqlDbType.Xml; break;
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

                dbCmdExec = new SqlCommand(consulta, dbCnn);

                // Si hay parametros los agrego
                if (_parametros != null && _parametros.Count > 0)
                {
                    this.dbCmdExec.CommandType = CommandType.Text;
                    SqlParameterCollection sqlParams = this.dbCmdExec.Parameters;

                    foreach (DlParameter param in this._parametros)
                    {                        
                        this.dbCmdExec.Parameters.Add(this.ConvertirParametro(param));
                    }
                }

                SqlDataReader dr = dbCmdExec.ExecuteReader();
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

                dbCmd.Add(new SqlCommand(consulta, dbCnn));
                indiceCmd = dbCmd.Count - 1;

                // Si hay parametros los agrego
                if (_parametros != null)
                {
                    dbCmd[indiceCmd].CommandType = CommandType.Text;
                    SqlParameterCollection sqlParams = dbCmd[indiceCmd].Parameters;

                    foreach (DlParameter param in this._parametros)
                    {
                        dbCmd[indiceCmd].Parameters.Add(ConvertirParametro(param));
                    }
                }

                dbDA.Add(new SqlDataAdapter());
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
                SqlCommandBuilder cb = new SqlCommandBuilder(dbDA[indiceCmd]);
                
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
                    this.dbCmdExec = new SqlCommand(consulta, this.dbCnn);

                    // Si hay parametros los agrego
                    if (this._parametros != null)
                    {
                        this.dbCmdExec.CommandType = CommandType.Text;
                        SqlParameterCollection sqlParams = this.dbCmdExec.Parameters;

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
