using System;
using System.Collections.Generic;
using System.Text;

namespace DevO2.UI
{
    public class GEKmlLookAt
    {
        private double _latitud;        
        private double _longitud;
        private double _altitud;
        private GEKmlAltitudeMode _modoAltitud = GEKmlAltitudeMode.RelativeToGround;
        private double _direccion;
        private double _inclinacion;
        private double _rango;        

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

        public GEKmlAltitudeMode ModoAltitud
        {
            get { return _modoAltitud; }
            set { _modoAltitud = value; }
        }

        public double Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        public double Inclinacion
        {
            get { return _inclinacion; }
            set { _inclinacion = value; }
        }

        public double Rango
        {
            get { return _rango; }
            set { _rango = value; }
        }
    }
}
