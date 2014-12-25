using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WorkBase.UI
{
    public class GMapsLatLng
    {
        #region Variables
        private string propLatitud;
        private string propLongitud;
        #endregion

        #region Constructores
        public GMapsLatLng() { }

        public GMapsLatLng(string latitud, string longitud)
        {
            this.propLatitud = latitud;
            this.propLongitud = longitud;
        }
        #endregion

        #region Propiedades
        public string Latitud
        {
            get { return this.propLatitud; }
            set { this.propLatitud = value; }
        }

        public string Longitud
        {
            get { return this.propLongitud; }
            set { this.propLongitud = value; }
        }
        #endregion
    }
}