using System;
using System.Collections.Generic;
using System.Text;

namespace DevO2.UI
{
    public class GEKmlLineString
    {
        private List<GEKmlCoord> _coordenadas = new List<GEKmlCoord>();
        private GEKmlAltitudeMode _modoAltitud = GEKmlAltitudeMode.RelativeToGround;
        private double _desplazarAltitud;        
        private bool _extrudir;
        private bool _teselar;

        public List<GEKmlCoord> Coordenadas
        {
            get { return _coordenadas; }
            set { _coordenadas = value; }
        }

        public GEKmlAltitudeMode ModoAltitud
        {
            get { return _modoAltitud; }
            set { _modoAltitud = value; }
        }

        public double DesplazarAltitud
        {
            get { return _desplazarAltitud; }
            set { _desplazarAltitud = value; }
        }

        public bool Extrudir
        {
            get { return _extrudir; }
            set { _extrudir = value; }
        }

        public bool Teselar
        {
            get { return _teselar; }
            set { _teselar = value; }
        }
    }
}
