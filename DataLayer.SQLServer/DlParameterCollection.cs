using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DevO2.DataLayer.SQLServer
{
    public sealed class DlParameterCollection : System.Collections.CollectionBase
    {
        #region Metodos
        public void Add(DlParameter parametro)
        {
            List.Add(parametro);
        }

        public void AddWithValue(string nombre, object valor)
        {
            this.Add(new DlParameter(nombre, valor));
        }

        public void Add(string nombre, DlTipoColumna tipoColumna)
        {
            this.Add(new DlParameter(nombre, tipoColumna));
        }

        public void Add(string nombre, DlTipoColumna tipoColumna, int longitud)
        {
            this.Add(new DlParameter(nombre, tipoColumna, longitud));
        }

        public void Add(string nombre, DlTipoColumna tipoColumna, int longitud, string columnaFuente)
        {
            this.Add(new DlParameter(nombre, tipoColumna, longitud, columnaFuente));
        }

        public void Remove(int indice)
        {
            // Verifico si el índice del parámetro es valido
            if (indice > Count - 1 || indice < 0)
                throw new Exception("Índice no valido!");
            else
                List.RemoveAt(indice);
        }

        public DlParameter Item(int indice)
        {
            return (DlParameter)List[indice];
        }
        #endregion
    }
}
