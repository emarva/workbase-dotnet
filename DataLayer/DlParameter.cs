using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DevO2.DataLayer
{
    public sealed class DlParameter
    {
        #region Variables
        private string _nombreParam;
        private DlTipoColumna _tipoColumna = DlTipoColumna.Char;
        private int _longitud;
        private ParameterDirection? _direccion = null;
        private string _columnaFuente = string.Empty;
        private object _valor = null;        
        #endregion

        #region Constructores
        public DlParameter(string nombreParam, object valor)
        {
            this._nombreParam = nombreParam;
            this._valor = valor;
        }

        public DlParameter(string nombreParam, DlTipoColumna tipoColumna)
            : this(nombreParam, null)
        {
            this._tipoColumna = tipoColumna;
        }

        public DlParameter(string nombreParam, DlTipoColumna tipoColumna, int longitud)
            : this(nombreParam, tipoColumna)
        {
            this._longitud = longitud;
        }

        public DlParameter(string nombreParam, DlTipoColumna tipoColumna, int longitud, string columnaFuente)
            : this(nombreParam, tipoColumna, longitud)
        {
            this._direccion = ParameterDirection.Input;
            this._columnaFuente = columnaFuente;            
        }

        public DlParameter(string nombreParam, DlTipoColumna tipoColumna, int longitud, ParameterDirection direccion, string columnaFuente, object valor)
            : this(nombreParam, tipoColumna, longitud, columnaFuente)
        {
            this._direccion = direccion;
            this._valor = valor;
        }
        #endregion

        #region Propiedades
        public string NombreParam
        {
            get { return this._nombreParam; }
            set { this._nombreParam = value; }
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
