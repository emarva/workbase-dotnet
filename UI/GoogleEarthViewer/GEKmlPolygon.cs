using System;
using System.Collections.Generic;
using System.Text;

namespace DevO2.UI
{
    public class GEKmlPolygon
    {
        #region Campos
        private string _nombre;
        private List<GEKmlCoord> _coordenadas = new List<GEKmlCoord>();
        private string _color = "64ff0000"; // Formato AABBGGRR
        private float _anchoLinea;
        private string _colorLinea = "64000000"; // Formato AABBGGRR
        #endregion

        #region Propiedades
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public List<GEKmlCoord> Coordenadas
        {
            get { return _coordenadas; }
            set { _coordenadas = value; }
        }

        public string Color
        {
            get { return _color; }
            set { _color = value; }
        }
        
        public float AnchoLinea
        {
            get { return _anchoLinea; }
            set { _anchoLinea = value; }
        }

        public string ColorLinea
        {
            get { return _colorLinea; }
            set { _colorLinea = value; }
        }
        #endregion
    }
}
