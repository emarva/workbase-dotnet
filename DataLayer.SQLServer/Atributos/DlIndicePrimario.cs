using System;
using System.Collections.Generic;
using System.Text;

namespace DevO2.DataLayer.SQLServer.Atributos
{
    public class DlIndicePrimario : Attribute
    {
        private bool _valor;

        public DlIndicePrimario(bool valor)
        {
            _valor = valor;
        }

        public bool Valor
        {
            get { return _valor; }
        }
    }
}
