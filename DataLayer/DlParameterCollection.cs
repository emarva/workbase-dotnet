using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DevO2.DataLayer
{
    public sealed class DlParameterCollection : System.Collections.CollectionBase
    {
        #region Metodos
        public void Add(DlParameter parametro)
        {
            List.Add(parametro);
        }

        public void AddWithValue(string nombreParam, object valor)
        {
            this.Add(new DlParameter(nombreParam, valor));
        }

        public void Add(string nombreParam, DlTipoColumna tipoColumna)
        {
            this.Add(new DlParameter(nombreParam, tipoColumna));
        }

        public void Add(string nombreParam, DlTipoColumna tipoColumna, int longitud)
        {
            this.Add(new DlParameter(nombreParam, tipoColumna, longitud));
        }

        public void Add(string nombreParam, DlTipoColumna tipoColumna, int longitud, string columnaFuente)
        {
            this.Add(new DlParameter(nombreParam, tipoColumna, longitud, columnaFuente));
        }

        public void Remove(int indice)
        {
            // Verifico si el índice del parámetro es valido
            if (indice > Count - 1 || indice < 0)
                System.Windows.Forms.MessageBox.Show("Índice no valido!");
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
