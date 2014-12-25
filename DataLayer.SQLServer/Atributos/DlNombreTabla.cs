using System;
using System.Collections.Generic;
using System.Text;

namespace DevO2.DataLayer.SQLServer.Atributos
{
    public class DlNombreTabla : Attribute
    {
        private string _nombre;
                
        public DlNombreTabla(string nombre)
        {
            _nombre = nombre;
        }

        public string Nombre
        {
            get { return _nombre; }
        }
    }
}
