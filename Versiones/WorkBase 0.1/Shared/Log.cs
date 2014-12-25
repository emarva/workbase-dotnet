using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WorkBase.Shared
{
    public sealed class Log
    {
        #region Variables
        private static string propArchivoLog;
        private static bool propAnteponerFecha;
        private static bool propAnteponerHora;
        private static int propLargoLinea = 40;
        #endregion

        #region Propiedades
        public static string ArchivoLog
        {
            get { return propArchivoLog; }
            set { propArchivoLog = value; }
        }

        public static bool AnteponerFecha
        {
            get { return propAnteponerFecha; }
            set { propAnteponerFecha = value; }
        }

        public static bool AnteponerHora
        {
            get { return propAnteponerHora; }
            set { propAnteponerHora = value; }
        }

        public static int LargoLinea
        {
            get { return propLargoLinea; }
            set { propLargoLinea = value; }
        }
        #endregion

        #region Metodos
        private static void CrearEntrada(string cadena, bool linea)
        {
            try
            {
                StreamWriter swLog;

                // Si no se define el nombre del archivo Log, se lanza una excepcion
                if (string.IsNullOrEmpty(propArchivoLog))
                    throw new ArgumentNullException();
             
                // Repito la cadena
                if (linea)
                {
                    string caracteres = "";

                    for (int i = 0; i < propLargoLinea; i++)
                        caracteres += cadena;

                    cadena = caracteres;
                }

                // Verifico si hay que anteponer la fecha y hora
                if (cadena.Length != 0)
                {          
                    string anteponer = "";

                    if (propAnteponerFecha)
                        anteponer = "(" + DateTime.Now.ToString("dd/MM/yyyy");

                    if (propAnteponerHora && anteponer.Length != 0)
                        anteponer += " " + DateTime.Now.ToString("HH:mm:ss") + ") ";
                    else if (!propAnteponerHora && anteponer.Length != 0)
                        anteponer += ")";
                    else if (propAnteponerHora)
                        anteponer = "(" + DateTime.Now.ToString("HH:mm:ss") + ") ";

                    cadena = anteponer + cadena;
                }

                if (!File.Exists(propArchivoLog))
                {
                    swLog = new StreamWriter(propArchivoLog, false, Encoding.UTF8);
                }
                else
                {
                    swLog = new StreamWriter(propArchivoLog, true, Encoding.UTF8);
                }                

                swLog.WriteLine(cadena);
                swLog.Close();

                return;
            }
            catch { }
        }

        public static void CrearCadena(string cadena)
        {
            CrearEntrada(cadena, false);
        }

        public static void CrearLineaEnBlanco()
        {
            CrearEntrada("", false);
        }

        public static void CrearLineaPuntos()
        {
            CrearEntrada("=", true);
        }

        public static void CrearLineaSimple()
        {
            CrearEntrada("-", true);
        }
        #endregion
    }
}
