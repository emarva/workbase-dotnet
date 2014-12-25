using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DevO2.DataLayer.SQLServer
{
    public sealed class DlParameter
    {
        #region Campos
        private string _nombre;
        private DlTipoColumna _tipoColumna = DlTipoColumna.Char;
        private int _longitud;
        private ParameterDirection? _direccion = null;
        private string _columnaFuente = string.Empty;
        private object _valor = null;        
        #endregion

        #region Constructores
        public DlParameter(string nombre, object valor)
        {
            this._nombre = nombre;
            this._valor = valor;
        }

        public DlParameter(string nombre, DlTipoColumna tipoColumna)
            : this(nombre, null)
        {
            this._tipoColumna = tipoColumna;
        }

        public DlParameter(string nombre, DlTipoColumna tipoColumna, int longitud)
            : this(nombre, tipoColumna)
        {
            this._longitud = longitud;
        }

        public DlParameter(string nombre, DlTipoColumna tipoColumna, int longitud, string columnaFuente)
            : this(nombre, tipoColumna, longitud)
        {
            this._direccion = ParameterDirection.Input;
            this._columnaFuente = columnaFuente;            
        }

        public DlParameter(string nombre, DlTipoColumna tipoColumna, int longitud, ParameterDirection direccion, string columnaFuente, object valor)
            : this(nombre, tipoColumna, longitud, columnaFuente)
        {
            this._direccion = direccion;
            this._valor = valor;
        }
        #endregion

        #region Propiedades
        public string Nombre
        {
            get { return this._nombre; }
            set { this._nombre = value; }
        }

        public DlTipoColumna TipoColumna
        {
            get { return this._tipoColumna; }
            set { this._tipoColumna = value; }
        }

        public int Longitud
        {
            get { return this._longitud; }
            set { this._longitud = value; }
        }

        public ParameterDirection? Direccion
        {
            get { return this._direccion; }
            set { this._direccion = value; }
        }

        public string ColumnaFuente
        {
            get { return this._columnaFuente; }
            set { this._columnaFuente = value; }
        }

        public object Valor
        {
            get { return this._valor; }
            set { this._valor = value; }
        }       
        #endregion
    }
}
