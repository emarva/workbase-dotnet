﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DevO2.DataLayer
{
    #region DlTipoConnector
    public enum DlTipoConnector
    {
        Access,
        Firebird,
        MySQL,
        PostgreSQL,
        SQLite,
        SQLServer
    }
    #endregion

    #region DlEstadoConexion
    public enum DlEstadoConexion
    {
        Abierta,
        Cerrada
    }
    #endregion

    #region DlTipoComando
    public enum DlTipoComando
    {
        ProcedimientoAlmacenado,
        TablaDirecta,
        Texto
    }
    #endregion

    #region DlTipoColumna
    public enum DlTipoColumna
    {
        BigInt,
        Binary,
        Bit,
        Blob,
        Byte,        
        Char,
        Date,
        DateTime,
        Decimal,
        Double,
        Float,
        Image,
        Int,
        MediumBlob,
        MediumInt,
        MediumText,
        Money,
        NChar,
        NVarChar,
        LongBlob,
        LongText,
        Real,
        SmallDateTime,
        SmallInt,
        SmallMoney,
        String,
        Text,
        Time,
        TimeStamp,
        TinyBlob,
        TinyInt,
        TinyText,
        Varchar,
        VarBinary,
        Xml
    }
    #endregion
}
