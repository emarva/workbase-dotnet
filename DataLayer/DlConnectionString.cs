using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DevO2.DataLayer
{
    public class DlConnectionString
    {
        #region Variables
        private DlTipoConnector _conector;
        private string _host;
        private int _puerto;
        private string _usuario;
        private string _contrasena;
        private string _baseDatos;
        private Hashtable _parametrosAdicionales;
        #endregion

        #region Constructores
        public DlConnectionString() { }

        public DlConnectionString(DlTipoConnector conector, string host, string baseDatos)
        {
            _conector = conector;
            _host = host;
            _baseDatos = baseDatos;
        }

        public DlConnectionString(DlTipoConnector conector, string host, int puerto, bool seguridadIntegrada, string baseDatos)
            : this(conector, host, baseDatos)
        {
            _puerto = puerto;
        }

        public DlConnectionString(DlTipoConnector conector, string host, string usuario, string contrasena, string baseDatos)
            : this(conector, host, baseDatos)
        {
            _usuario = usuario;
            _contrasena = contrasena;
        }

        public DlConnectionString(DlTipoConnector conector, string host, int puerto, string usuario, string contrasena, string baseDatos)
            : this(conector, host, usuario, contrasena, baseDatos)
        {
            _puerto = puerto;
        }
        #endregion

        #region Propiedades
        public DlTipoConnector Conector
        {
            get { return _conector; }
            set { _conector = value; }
        }

        public string Host
        {
            get { return _host; }
            set { _host = value; }
        }

        public int Puerto
        {
            get { return _puerto; }
            set { _puerto = value; }
        }

        public string Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public string Contrasena
        {
            get { return _contrasena; }
            set { _contrasena = value; }
        }

        public string BaseDatos
        {
            get { return _baseDatos; }
            set { _baseDatos = value; }
        }

        public Hashtable ParametrosAdicionales
        {
            get
            {
                if (_parametrosAdicionales == null)
                    _parametrosAdicionales = new Hashtable();

                return _parametrosAdicionales;
            }
        }
        #endregion
    }
}
