using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DevO2.UI
{    
    public class GMapsMarker
    {
        #region Variables
        private string propNombre;
        private string propTitulo;
        private string propContenido;
        private string propLatitud;
        private string propLongitud;
        private string propIcono;
        #endregion

        #region Propiedades
        public string Nombre
        {
            get { return this.propNombre; }
            set { this.propNombre = value; }
        }

        public string Titulo
        {
            get { return this.propTitulo; }
            set { this.propTitulo = value; }
        }

        public string Contenido
        {
            get { return this.propContenido; }
            set { this.propContenido = value; }
        }

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

        public string Icono
        {
            get { return this.propIcono; }
            set { this.propIcono = value; }
        }
        #endregion
    }
}
