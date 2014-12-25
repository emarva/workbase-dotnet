using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WorkBase.UI
{
    public class GMapsPolygon
    {
        #region Variables
        private string propNombre;
        private List<GMapsLatLng> propCoordenadas = new List<GMapsLatLng>();
        private string propColor;
        private string propOpacidad;
        private string propAncho;
        private string propRellenoColor;
        private string propRellenoOpacidad;
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

        public string RellenoColor
        {
            get { return this.propRellenoColor; }
            set { this.propRellenoColor = value; }
        }

        public string RellenoOpacidad
        {
            get { return this.propRellenoOpacidad; }
            set { this.propRellenoOpacidad = value; }
        }
        #endregion
    }
}
