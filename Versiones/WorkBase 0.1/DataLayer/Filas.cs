using System;
using System.Collections.Generic;
using System.Text;

namespace WorkBase.DataLayer
{
    public sealed class Fila
    {
        #region Variables
        private int propNumero;
        private int propTotalCampos;
        private Campos propCampos = new Campos();
        #endregion
        
        #region Propiedades
        public int Numero
        {
            get { return this.propNumero; }
            set { this.propNumero = value; }
        }

        public int TotalCampos
        {
            get { return this.propTotalCampos; }
            set { this.propTotalCampos = value; }
        }

        public Campos Campos
        {
            get { return this.propCampos; }
            set { this.propCampos = value; }
        }
        #endregion
    }

    public sealed class Filas : System.Collections.CollectionBase
    {
        #region Metodos
        public void Add(Fila fila)
        {
            this.List.Add(fila);
        }

        public void Remove(Fila fila)
        {
            this.List.Remove(fila);
        }

        public Fila Find(int numero)
        {
            Fila aDevolver = null;

            // Recorro todas las filas
            foreach (Fila fila in this.List)
            {
                if (fila.Numero == numero)
                {
                    aDevolver = fila;
                    break;
                }

            }

            return aDevolver;
        }
        #endregion
    }
}
