using System;
using System.Collections.Generic;
using System.Text;
using WorkBase.Shared;

namespace WorkBase.DataLayer
{
    public sealed class Campo
    {
        #region Variables
        private int propIndice;
        private string propNombre;
        private object propValor = null;
        #endregion

        #region Propiedades
        public int Indice
        {
            get { return this.propIndice; }
            set { this.propIndice = value; }
        }

        public string Nombre
        {
            get { return this.propNombre; }
            set { this.propNombre = value; }
        }

        public object Valor
        {
            get { return this.propValor; }
            set { this.propValor = value; }
        }
        #endregion       
    }

    public sealed class Campos : System.Collections.CollectionBase
    {
        #region Variables
        private int[] indiceCampoRepetido;
        #endregion

        #region Metodos
        public void Add(Campo campo)
        {
            // Aplicar el filtro de campo repetido
            this.Add(campo.Nombre, campo.Valor);
        }

        public void Add(string nombre, object valor)
        {   
            Campo campo = new Campo();
            Campo campoIgual = new Campo();

            campo.Indice = this.List.Count;

            // Verifico si existe otro campo con el mismo nombre
            campoIgual = this.Find(nombre);

            if (campoIgual != null)
            {
                if (this.indiceCampoRepetido == null)
                    this.indiceCampoRepetido = new int[this.List.Count];
                else
                    Array.Resize(ref this.indiceCampoRepetido, this.List.Count);

                this.indiceCampoRepetido[campoIgual.Indice] += 1;
                nombre += this.indiceCampoRepetido[campoIgual.Indice];
            }

            campo.Nombre = nombre;
            campo.Valor = valor;

            this.List.Add(campo);
        }

        public void Remove(Campo campo)
        {
            this.List.Remove(campo);
        }

        public Campo Find(int indice)
        {
            Campo aDevolver = null;

            // Recorro todos los campos
            foreach (Campo campo in this.List)
            {
                if (campo.Indice == indice)
                {
                    aDevolver = campo;
                    break;
                }
            }

            return aDevolver;
        }

        public Campo Find(string nombre)
        {
            Campo aDevolver = null;

            // Recorro todos los campos
            foreach (Campo campo in this.List)
            {
                if (campo.Nombre == nombre)
                {
                    aDevolver = campo;
                    break;
                }
            }

            return aDevolver;
        }
        #endregion
    }
}
