using System;
using System.Collections.Generic;
using System.Text;

namespace DevO2.DataLayer.SQLServer.Atributos
{
    public class DlCampo : Attribute
    {
        #region Campos
        private string _nombre;
        private DlTipoColumna _tipo = DlTipoColumna.Char;
        private int _largo;
        private bool _permitirNulos;
        #endregion

        #region Constructores
        public DlCampo(string nombre, DlTipoColumna tipo)
        {
            _nombre = nombre;
            _tipo = tipo;
        }

        public DlCampo(string nombre, DlTipoColumna tipo, int largo)
            : this(nombre, tipo)
        {
            _largo = largo;
        }

        public DlCampo(string nombre, DlTipoColumna tipo, bool permitirNulos)
         : this (nombre, tipo)
        {
            _permitirNulos = permitirNulos;
        }

        public DlCampo(string nombre, DlTipoColumna tipo, int largo, bool permitirNulos)
            : this(nombre, tipo, largo)
        {
            _permitirNulos = permitirNulos;
        }
        #endregion

        #region Propiedades
        public string Nombre
        {
            get { return _nombre; }
        }

        public DlTipoColumna Tipo
        {
            get { return _tipo; }
        }

        public int Largo
        {
            get { return _largo; }
        }

        public bool PermitirNulos
        {
            get { return _permitirNulos; }
        }
        #endregion
    }
}
