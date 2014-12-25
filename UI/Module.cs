using System;
using System.Collections.Generic;
using System.Text;

namespace DevO2.UI
{
    public class Module
    {
        #region Variables
        private IModule propInstancia;
        private string propRutaAssembly;
        #endregion

        #region Propiedades
        public IModule Instancia
        {
            get { return this.propInstancia; }
            set { this.propInstancia = value; }
        }

        public string RutaAssembly
        {
            get { return this.propRutaAssembly; }
            set { this.propRutaAssembly = value; }
        }
        #endregion
    }

    public class ModuleCollection : System.Collections.CollectionBase
    {
        #region Metodos
        public void Add(Module modulo)
        {
            this.List.Add(modulo);
        }

        public void Remove(Module modulo)
        {
            this.List.Remove(modulo);
        }

        public Module Find(string nombreModuloORuta)
        {
            Module aDevolver = null;

            // Recorro todos los modulos
            foreach (Module moduloEn in this.List)
            {
                if (moduloEn.RutaAssembly.Equals(nombreModuloORuta))
                {
                    aDevolver = moduloEn;
                    break;
                }
            }

            return aDevolver;
        }
        #endregion
    }
}
