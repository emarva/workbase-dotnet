using System;
using System.Collections.Generic;
using System.Text;

namespace WorkBase.Shared
{
    #region TipoCompresion
    public enum TipoCompresion
    {
        Deflate,
        GZip
    }
    #endregion

    #region TipoHash
    public enum TipoHash
    {
        MD5,
        SHA1,
        SHA256,
        SHA384,
        SHA512
    }
    #endregion

    #region TipoSHA
    public enum TipoSHA
    {
        SHA1,
        SHA256,
        SHA384,
        SHA512
    }
    #endregion

    #region FormatoCadenaEncriptada
    public enum FormatoCadenaEncriptada
    {   
        Base64,
        Hexadecimal
    }
    #endregion

    #region MetodoEncriptacion
    public enum MetodoEncriptacion
    {
        DES,        
        RC2,
        Rijndael,
        TripleDES
    }
    #endregion

    #region PrinterAlineacionLinea
    public enum PrinterAlineacionLinea
    {
        Izquierda = 0,
        //Centro = 1,
        Derecha = 2
    }
    #endregion

    #region PrinterTipoGuillotina
    public enum PrinterTipoGuillotina
    {
        Epson = 0,
        Star = 1
    }
    #endregion
}
