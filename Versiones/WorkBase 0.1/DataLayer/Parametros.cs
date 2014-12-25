using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace WorkBase.DataLayer
{
    public sealed class Parametro
    {
        #region Variables
        private string propNombre;
        private string propTipoColumna;
        private int propLongitud;
        private object propValor;
        private ParameterDirection? propDireccion;
        #endregion

        #region Propiedades
        public string Nombre
        {
            get { return this.propNombre; }
            set { this.propNombre = value; }
        }

        public string TipoColumna
        {
            get { return this.propTipoColumna; }
            set { this.propTipoColumna = value; }
        }

        public int Longitud
        {
            get { return this.propLongitud; }
            set { this.propLongitud = value; }
        }

        public object Valor
        {
            get { return this.propValor; }
            set { this.propValor = value; }
        }

        public ParameterDirection? Direccion
        {
            get { return this.propDireccion; }
            set { this.propDireccion = value; }
        }
        #endregion
    }

    public sealed class ParametroCollection : System.Collections.CollectionBase
    {
        #region Metodos
        public void Add(Parametro parametro)
        {
            List.Add(parametro);
        }

        public void Add(string nombre, string tipoColumna, int longitud, object valor, ParameterDirection? direccion)
        {
            Parametro param = new Parametro();

            param.Nombre = nombre;
            param.TipoColumna = tipoColumna;
            param.Longitud = longitud;
            param.Valor = valor;
            param.Direccion = direccion;

            List.Add(param);
        }

        public void Add(string nombre, object valor)
        {
            this.Add(nombre, "varchar", 0, valor, null);
        }

        public void Add(string nombre, string tipoColumna, object valor)
        {
            this.Add(nombre, tipoColumna, 0, valor, null);
        }

        public void Add(string nombre, string tipoColumna, int longitud, object valor)
        {
            this.Add(nombre, tipoColumna, longitud, valor, null);
        }

        public void Remove(int indice)
        {
            // Verifico si el índice del parámetro es valido
            if (indice > Count - 1 || indice < 0)
                System.Windows.Forms.MessageBox.Show("Índice no valido!");
            else
                List.RemoveAt(indice);
        }

        public Parametro Item(int indice)
        {
            return (Parametro)List[indice];
        }
        #endregion
    }
}
