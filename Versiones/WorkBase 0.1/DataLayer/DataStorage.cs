using System;
using System.Collections.Generic;
using System.Text;
using WorkBase.Shared;

namespace WorkBase.DataLayer
{
    public sealed class DataStorage : IDisposable
    {
        #region Variables
        private Filas propFilas = null;
        private int propTotalFilas;
        private bool propTieneFilas;
        private bool propEsEOF;

        private int filaActual;
        private bool leyendo;
        private bool disposed;
        #endregion

        #region Constructor
        public DataStorage()
        {
            this.propFilas = new Filas();
        }
        #endregion

        #region Propiedades
        public Filas Filas
        {
            get { return this.propFilas; }
            set { this.propFilas = value; }
        }

        public int TotalFilas
        {
            get { return this.propTotalFilas; }
            set
            {
                this.propTotalFilas = value; 

                if (this.propTotalFilas > 0 && this.filaActual == 0)
                    this.filaActual = 1; 
            }
        }

        public bool TieneFilas
        {
            get { return this.propTieneFilas; }
            set { this.propTieneFilas = value; }
        }

        public bool EsEOF
        {
            get { return this.propEsEOF; }
            set { this.propEsEOF = value; }
        }
        #endregion

        #region Indexadores
        public Object2 this[int indice]
        {
            get { return new Object2(this.propFilas.Find(this.filaActual).Campos.Find(indice).Valor); }
        }

        public Object2 this[string campo]
        {
            get { return new Object2(this.propFilas.Find(this.filaActual).Campos.Find(campo).Valor); }
        }       
        #endregion

        #region Metodos
        public void MoverPrimero()
        {
            if (propTieneFilas)
            {
                this.filaActual = 1;
                this.propEsEOF = false;
            }
        }

        public void MoverUltimo()
        {
            if (propTieneFilas)
            {
                this.filaActual = this.propTotalFilas;
                this.propEsEOF = true;
            }
        }

        public void MoverSiguiente()
        {
            if (propTieneFilas)
            {
                if (this.filaActual < this.propTotalFilas)
                    this.filaActual++;
                else
                    this.propEsEOF = true;
            }
        }

        public void MoverAnterior()
        {
            if (propTieneFilas)
            {
                if (this.filaActual > 1)
                    this.filaActual--;
            }
        }

        public bool Leer()
        {
            if (propTieneFilas)
            {
                if (this.leyendo)
                {
                    MoverSiguiente();

                    if (this.propEsEOF == true)
                    {
                        this.leyendo = false;
                        return false;
                    }
                }
                else
                {
                    this.leyendo = true;
                }

                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region IDisposable
        private void Dispose(bool disposing)
        {
            if (disposing && !this.disposed)
            {
                this.propFilas = null;
                this.propTotalFilas = 0;
                this.filaActual = 0;
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
