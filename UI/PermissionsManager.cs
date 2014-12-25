using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
//using WorkBase.DataLayer;

namespace DevO2.UI
{
    public partial class PermissionsManager : Component
    {
        #region Variables
        private string propBDHost;
        private int propBDPuerto = 0;
        private string propBDUsuario;
        private string propBDContrasena;
        private string propBaseDatos;
        //private DlTipoConnector propTipoConnector;        
        #endregion

        #region Constructor
        public PermissionsManager()
        {
            InitializeComponent();
        }
        #endregion

        #region Propiedades
        public string BDHost
        {
            get { return this.propBDHost; }
            set { this.propBDHost = value; }
        }

        public int BDPuerto
        {
            get { return this.propBDPuerto; }
            set { this.propBDPuerto = value; }
        }

        public string BDUsuario
        {
            get { return this.propBDUsuario; }
            set { this.propBDUsuario = value; }
        }

        [PasswordPropertyText(true)]
        public string BDContrasena
        {
            get { return this.propBDContrasena; }
            set { this.propBDContrasena = value; }
        }

        public string BaseDatos
        {
            get { return this.propBaseDatos; }
            set { this.propBaseDatos = value; }
        }

       /*public DlTipoConnector TipoConnector
        {
            get { return this.propTipoConnector; }
            set { this.propTipoConnector = value; }
        }*/ 
        #endregion

        #region Metodos
        public void CrearTablas()
        {
            
        }
        #endregion
    }
}
