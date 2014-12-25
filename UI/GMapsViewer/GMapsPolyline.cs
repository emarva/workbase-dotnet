using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DevO2.UI
{
    public class GMapsPolyline
    {
        #region Variables
        private string propNombre;
        private List<GMapsLatLng> propCoordenadas = new List<GMapsLatLng>();
        private string propColor;
        private string propOpacidad;
        private string propAncho;
        #endregion

        #region Propiedades
        public string Nombre
        {
            get { return this.propNombre; }
            set { this.propNombre = value; }
        }

        public List<GMapsLatLng> Coordenadas
        {
            get { return this.propCoordenadas; }
            set { this.propCoordenadas = value; }
        }

        public string Color
        {
            get { return this.propColor; }
            set { this.propColor = value; }
        }

        public string Opacidad
        {
            get { return this.propOpacidad; }
            set { this.propOpacidad = value; }
        }

        public string Ancho
        {
            get { return this.propAncho; }
            set { this.propAncho = value; }
        }
        #endregion
    }
}
