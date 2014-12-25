using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text;
//using WorkBase.Shared;

namespace DevO2.DataLayer.SQLServer
{
    public sealed class DlConnection
    {
        #region Variables
        private DlConnectionString _cadenaConexion;
        private DlEstadoConexion _estado = DlEstadoConexion.Cerrada;

        internal IConnector connector;
        private DlParameterCollection parametros;
        private bool disposed;
        #endregion

        #region Constructores
        public DlConnection() { }

        public DlConnection(DlConnectionString cadenaConexion)
        {
            if (cadenaConexion.Host != null)
                _cadenaConexion = cadenaConexion;
        }
        #endregion

        #region Propiedades
        public DlConnectionString CadenaConexion
        {
            get { return _cadenaConexion; }
            set 
            {
                if (_cadenaConexion == null)
                    _cadenaConexion = new DlConnectionString();

                _cadenaConexion = value;
            }
        }

        public bool TieneFilas
        {
            get { return connector.TieneFilas; }
        }

        public int FilasAfectadas
        {
            get { return connector.FilasAfectadas; }
        }

        public DlEstadoConexion Estado
        {
            get { return _estado; }
        }

        /*
         * NO SE UTILIZA MAS EN CAMBIO SE HACE LO SIGUIENTE PARA CONTROLAR LA REAPERTURA DE LA CONEXION
         * 
         * if (cnn.Estado == DLEstadoConexion.Cerrada)
         * {
         *      se abre la conexion
         *      aperturaInterna = true;
         * }
         * 
         * if (aperturaInterna)
         *      se cierra la conexion
         * 
         */
        /*public bool BloquearConexion
        {
            get { return _bloquearConexion; }
            set { this._bloquearConexion = value; }
        }*/
        #endregion

        #region Metodos
        public void Abrir()
        {
            try
            {
                switch (_cadenaConexion.Conector)
                {
                    case DlTipoConnector.Access:
                        connector = new AccessConnector(_cadenaConexion);
                        break;
                    case DlTipoConnector.Firebird:
                        Assembly asmFb = Assembly.LoadFrom("WorkBase.FirebirdConnector.dll");
                        Type tipoFb = asmFb.GetType("WorkBase.Connectors.FirebirdConnector");
                        connector = (IConnector)Activator.CreateInstance(tipoFb, new object[] { _cadenaConexion });
                        break;
                    case DlTipoConnector.MSSQL:
                        connector = new MSSQLConnector(_cadenaConexion);
                        break;
                    case DlTipoConnector.MySQL:
                        Assembly asmMy = Assembly.LoadFrom("WorkBase.MySQLConnector.dll");
                        Type tipoMy = asmMy.GetType("WorkBase.Connectors.MySQLConnector");
                        connector = (IConnector)Activator.CreateInstance(tipoMy, new object[] { _cadenaConexion });
                        break;
                    case DlTipoConnector.PostgreSQL:
                        Assembly asmPg = Assembly.LoadFrom("WorkBase.PostgreSQLConnector.dll");
                        Type tipoPg = asmPg.GetType("WorkBase.Connectors.PostgreSQLConnector");
                        connector = (IConnector)Activator.CreateInstance(tipoPg, new object[] { _cadenaConexion });
                        break;
                    case DlTipoConnector.SQLite:
                        System.Windows.Forms.MessageBox.Show("Este conector no se encuentra disponible.");
                        _estado = DlEstadoConexion.Cerrada;
                        return;
                }

                if (connector.Conectar())
                    _estado = DlEstadoConexion.Abierta;
                else
                    _estado = DlEstadoConexion.Cerrada;
            }
            catch (Exception ex)
            {
                _estado = DlEstadoConexion.Cerrada;
            }
        }

        public void Cerrar()
        {
            try
            {
                if (connector.Desconectar())
                    _estado = DlEstadoConexion.Cerrada;
                else
                    _estado = DlEstadoConexion.Abierta;
            }
            catch 
            {
                _estado = DlEstadoConexion.Abierta;
            }
        }

        public void CrearBaseDatos(string nombre, Hashtable parametros)
        {
            connector.CrearBaseDatos(nombre, parametros);
        }

        public void CrearBaseDatos(string nombre)
        {
            connector.CrearBaseDatos(nombre, null);
        }
              
        public void CambiarBaseDatos(string nombreBaseDatos)
        {
            connector.Ejecutar("USE " + nombreBaseDatos);
        }

        public bool IniciarTransaccion()
        {
            return connector.IniciarTransaccion();
        }

        public bool DestinarTransaccion()
        {
            return connector.DestinarTransaccion();
        }

        public bool RestaurarTransaccion()
        {
            return connector.RestaurarTransaccion();
        }
        #endregion

        #region IDisposable
        private void Dispose(bool disposing)
        {
            if (disposing && !disposed)
            {
                Cerrar();
                if (_estado == DlEstadoConexion.Cerrada)
                    connector.Dispose();                
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
