using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DevO2.Shared
{
    public sealed class Logger
    {
        #region Campos
        private static string _archivoLogger;
        private static bool _anteponerFecha;
        private static bool _anteponerHora;
        private static int _largoLinea = 40;
        private static string _fuente;
        #endregion

        #region Propiedades
        public static string ArchivoLog
        {
            get { return _archivoLogger; }
            set { _archivoLogger = value; }
        }

        public static bool AnteponerFecha
        {
            get { return _anteponerFecha; }
            set { _anteponerFecha = value; }
        }

        public static bool AnteponerHora
        {
            get { return _anteponerHora; }
            set { _anteponerHora = value; }
        }

        public static int LargoLinea
        {
            get { return _largoLinea; }
            set { _largoLinea = value; }
        }

        public static string Fuente
        {
            get { return _fuente; }
            set { _fuente = value; }
        }
        #endregion

        #region Metodos
        private static void AgregarLinea(string cadena, TipoEntradaLogger tipo, bool linea)
        {
            try
            {
                StreamWriter swLog;

                // Si no se define el nombre del archivo Log, se lanza una excepcion
                if (string.IsNullOrEmpty(_archivoLogger))
                    throw new ArgumentNullException();
             
                // Repito la cadena
                if (linea)
                {
                    string caracteres = "";

                    for (int i = 0; i < _largoLinea; i++)
                        caracteres += cadena;

                    cadena = caracteres;
                }

                // Verifico si hay que anteponer la fecha y hora
                if (cadena.Length != 0)
                {          
                    string anteponer = "";

                    if (_anteponerFecha)
                        anteponer = "[" + DateTime.Now.ToString("dd/MM/yyyy");

                    if (_anteponerHora && anteponer.Length != 0)
                        anteponer += " " + DateTime.Now.ToString("HH:mm:ss") + "]";
                    else if (!_anteponerHora && anteponer.Length != 0)
                        anteponer += "]";
                    else if (_anteponerHora)
                        anteponer = "[" + DateTime.Now.ToString("HH:mm:ss") + "]";

                    // Agrego la aplicacion, solo si el parametro aplicacion no esta vacio
                    if (!string.IsNullOrEmpty(_fuente))
                        anteponer += "[" + _fuente + "]";

                    // Agrego el tipo de entrada
                    switch (tipo)
                    {
                        case TipoEntradaLogger.Advertencia: anteponer += "[WARNING]"; break;
                        case TipoEntradaLogger.Error: anteponer += "[ERROR]"; break;
                        case TipoEntradaLogger.Informacion: anteponer += "[INFO]"; break;
                    }

                    cadena = anteponer + " " + cadena;
                }

                if (!File.Exists(_archivoLogger))
                {
                    swLog = new StreamWriter(_archivoLogger, false, Encoding.UTF8);
                }
                else
                {
                    swLog = new StreamWriter(_archivoLogger, true, Encoding.UTF8);
                }                  

                swLog.WriteLine(cadena);
                swLog.Close();

                return;
            }
            catch { }
        }

        public static void AgregarCadena(string cadena, TipoEntradaLogger tipo)
        {
            AgregarLinea(cadena, tipo, false);
        }

        public static void AgregarCadena(string cadena)
        {
            AgregarLinea(cadena, TipoEntradaLogger.Ninguno, false);
        }

        public static void AgregarLineaEnBlanco()
        {
            AgregarLinea(string.Empty, TipoEntradaLogger.Ninguno, true);
        }

        public static void AgregarLineaDoble()
        {
            AgregarLinea("=", TipoEntradaLogger.Ninguno, true);
        }

        public static void AgregarLineaSimple()
        {
            AgregarLinea("-", TipoEntradaLogger.Ninguno, true);
        }
        #endregion
    }
}
