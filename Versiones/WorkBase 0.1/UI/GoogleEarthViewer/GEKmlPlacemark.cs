using System;
using System.Collections.Generic;
using System.Text;

namespace WorkBase.UI
{
    public class GEKmlPlacemark
    {
        #region Campos
        private string _nombre;
        private string _icono;
        private double _escalaIcono = 1.0;
        private double _latitud;
        private double _longitud;
        #endregion

        #region Constructores
        public GEKmlPlacemark(double latitud, double longitud)
        {
            _latitud = latitud;
            _longitud = longitud;
        }

        public GEKmlPlacemark(string nombre, double latitud, double longitud)
            : this(latitud, longitud)
        {
            _nombre = nombre;
        }

        public GEKmlPlacemark(string nombre, string icono, double latitud, double longitud)
            : this(nombre, latitud, longitud)
        {
            _icono = icono;
        }

        public GEKmlPlacemark(string nombre, string icono, double escalaIcono, double latitud, double longitud)
            : this (nombre, icono, latitud, longitud)
        {
            _escalaIcono = escalaIcono;
        }
        public GEKmlPlacemark() { }
        #endregion

        #region Propiedades
        public string Nombre
        {
            get { return this._nombre; }
            set { this._nombre = value; }
        }

        public string Icono
        {
            get { return this._icono; }
            set { this._icono = value; }
        }

        public double EscalaIcono
        {
            get { return this._escalaIcono; }
            set { this._escalaIcono = value; }
        }

        public double Latitud
        {
            get { return this._latitud; }
            set { this._latitud = value; }
        }

        public double Longitud
        {
            get { return this._longitud; }
            set { this._longitud = value; }
        }
        #endregion
    }
}
