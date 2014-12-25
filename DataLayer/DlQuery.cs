using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace DevO2.DataLayer
{
    public sealed class DlQuery : IDisposable
    {
        #region Variables
        private DlConnection _conexion;
        private DlTipoComando _tipoComando = DlTipoComando.Texto;
        private DlParameterCollection _parametros;
        private string _consulta;
        
        private int indiceCmd; // Indice del DbCommand asociado al DlQuery actual
        DbDataAdapter dbDA;
        private bool disposed;
        #endregion

        #region Constructores
        public DlQuery() { }

        public DlQuery(string consulta)
        {
            this._consulta = consulta;
        }

        public DlQuery(string consulta, DlConnection conexion)
            : this(consulta)
        {
            this._conexion = conexion;
        }
        #endregion

        #region Propiedades
        public DlConnection Conexion
        {
            get { return _conexion; }
            set { _conexion = value; }
        }

        public DlTipoComando TipoComando
        {
            get { return _tipoComando; }
            set { _tipoComando = value; }
        }

        public DlParameterCollection Parametros
        {
            get { return _parametros; }
            set { _parametros = value; }
        }

        public string Consulta
        {
            get { return _consulta; }
            set { _consulta = value; }
        }
        #endregion

        #region Metodos
        public DbDataReader EjecutarReader()
        {
            if (_conexion.connector.Conectado)
            {
                _conexion.connector.Parametros = _parametros;
                return _conexion.connector.ObtenerDataReader(_consulta);
            }            
            return null;
        }

        public int EjecutarConsultaAhora()
        {
            if (_conexion.connector.Conectado)
            {
                _conexion.connector.Parametros = _parametros;
                _conexion.connector.Ejecutar(_consulta);
                return _conexion.connector.FilasAfectadas;
            }
            return -1;
        }        

        public void Llenar(DataSet dataSet)
        {
            if (_conexion.connector.Conectado)
            {
                dbDA = _conexion.connector.ObtenerDataAdapter(_consulta, ref indiceCmd);
                dbDA.Fill(dataSet);
            }
        }

        public void Llenar(DataTable dataTable)
        {
            if (_conexion.connector.Conectado)
            {
                dbDA = _conexion.connector.ObtenerDataAdapter(_consulta, ref indiceCmd);
                dbDA.Fill(dataTable);
            }
        }        

        public void Actualizar(DataSet dataSet)
        {
            if (_conexion.Estado == DlEstadoConexion.Abierta)
                _conexion.connector.ActualizarObjetoDatos(indiceCmd, dataSet);
        }

        public void Actualizar(DataTable dataTable)
        {
            if (_conexion.connector.Conectado)
                _conexion.connector.ActualizarObjetoDatos(indiceCmd, dataTable);
        }
        #endregion

        #region IDisposable
        private void Dispose(bool disposing)
        {
            if (disposing && !disposed)
            {
                _parametros.Clear();
                dbDA.Dispose();
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
