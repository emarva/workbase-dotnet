using System;
using System.Collections.Generic;
using System.Text;

namespace WorkBase.Shared
{
    public sealed class Compression
    {
        #region Metodos
        public static bool ComprimirArchivo(string archivo, TipoCompresion tipo)
        {
            try
            {
                switch (tipo)
                {
                    case TipoCompresion.Deflate: break;
                    case TipoCompresion.GZip: break;
                }
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        public static bool ComprimirArchivos(string[] archivos, TipoCompresion tipo)
        {
            return false;
        }

        public static bool DescomprimirArchivo(string archivo, TipoCompresion tipo)
        {
            try
            {
                switch (tipo)
                {
                    case TipoCompresion.Deflate: break;
                    case TipoCompresion.GZip: break;
                }
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        public static bool DescomprimirArchivos(string[] archivos, TipoCompresion tipo)
        {
            return false;
        }
        #endregion
    }
}
