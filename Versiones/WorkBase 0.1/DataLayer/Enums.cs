using System;
using System.Collections.Generic;
using System.Text;

namespace WorkBase.DataLayer
{
    #region Connector
    public enum TipoConnector
    {
        Access,
        DB2,
        Firebird,
        MSSQL,
        MySQL, 
        PostgreSQL,
        SQLite
    }
    #endregion

    /*public enum TipoColumna
    {
        Booleano = 0,
        Fecha = 1,        
        Numero = 2,
        Texto = 3        
    }*/
}
