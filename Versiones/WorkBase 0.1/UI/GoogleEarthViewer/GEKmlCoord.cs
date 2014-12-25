using System;
using System.Collections.Generic;
using System.Text;

namespace WorkBase.UI
{
    public class GEKmlCoord
    {
        private double _latitud;
        private double _longitud;
        private double _altitud;

        public GEKmlCoord(double latitud, double longitud, double altitud)
        {
            _latitud = latitud;
            _longitud = longitud;
            _altitud = altitud;
        }

        public GEKmlCoord() { }

        public double Latitud
        {
            get { return _latitud; }
            set { _latitud = value; }
        }

        public double Longitud
        {
            get { return _longitud; }
            set { _longitud = value; }
        }

        public double Altitud
        {
            get { return _altitud; }
            set { _altitud = value; }
        }
    }
}
