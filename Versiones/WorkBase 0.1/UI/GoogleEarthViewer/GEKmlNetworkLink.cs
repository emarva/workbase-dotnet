using System;
using System.Collections.Generic;
using System.Text;

namespace WorkBase.UI
{
    public class GEKmlNetworkLink
    {
        #region Variables
        private string _nombre;
        private string _enlace;
        private bool _actualizarVisibilidad;
        private bool _volarParaVer;
        private GEKmlRefreshMode _modoActualizacion = GEKmlRefreshMode.None;
        private float _intervaloActualizacion;
        #endregion

        #region Constructores
        public GEKmlNetworkLink(string nombre, string enlace, bool actualizarVisibilidad, bool volarParaVer, GEKmlRefreshMode modoActualizacion, float intervaloActualizacion)
        {
            this._nombre = nombre;
            this._enlace = enlace;
            this._actualizarVisibilidad = actualizarVisibilidad;
            this._volarParaVer = volarParaVer;
            this._modoActualizacion = modoActualizacion;
            this._intervaloActualizacion = intervaloActualizacion;
        }

        public GEKmlNetworkLink(string nombre, string enlace, bool actualizarVisibilidad, bool volarParaVer)
            : this(nombre, enlace, actualizarVisibilidad, volarParaVer, GEKmlRefreshMode.None, 0)
        {
        }

        public GEKmlNetworkLink(string nombre, string enlace)
            : this(nombre, enlace, false, false)
        {
        }

        public GEKmlNetworkLink() { }
        #endregion

        #region Propiedades
        public string Nombre
        {
            get { return this._nombre; }
            set { this._nombre = value; }
        }

        public string Enlace
        {
            get { return this._enlace; }
            set { this._enlace = value; }
        }

        public bool ActualizarVisibilidad
        {
            get { return this._actualizarVisibilidad; }
            set { this._actualizarVisibilidad = value; }
        }

        public bool VolarParaVer
        {
            get { return this._volarParaVer; }
            set { this._volarParaVer = value; }
        }

        public GEKmlRefreshMode ModoActualizacion
        {
            get { return this._modoActualizacion; }
            set { this._modoActualizacion = value; }
        }

        public float IntervaloActualizacion
        {
            get { return this._intervaloActualizacion; }
            set { this._intervaloActualizacion = value; }
        }
        #endregion
    }
}
