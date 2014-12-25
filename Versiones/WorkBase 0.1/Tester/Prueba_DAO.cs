using System;
using System.Collections.Generic;
using System.Text;

namespace Tester
{
    class Prueba_DAO
    {
        private int? propId;
        private string propNombre;
        private string propDireccion;

        public int? Id
        {
            get { return propId; }
            set { propId = value; }
        }

        public string Nombre
        {
            get { return propNombre; }
            set { propNombre = value; }
        }

        public string Direccion
        {
            get { return propDireccion; }
            set { propDireccion = value; }
        }
    }
}
