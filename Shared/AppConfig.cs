using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace DevO2.Shared
{
    public sealed class AppConfig
    {
        #region Constantes
        // Nombre del elemento raiz del archivo de configuracion
        private const string elementoRaiz = "AppConfig";
        #endregion

/*        #region Variables
        private static AppConfig instance = null;
        private static readonly object padLock = new object();
        #endregion*/

        #region Campos
        private static string _archivoConfiguracion;
        private static string _seccion;
        #endregion

        #region Propiedades
        public static string ArchivoConfiguracion
        {
            get { return _archivoConfiguracion; }
            set { _archivoConfiguracion = value; }
        }

        public static string Seccion
        {
            get { return _seccion; }
            set { _seccion = value; }
        }
        #endregion

/*        #region Constructor
        public AppConfig AppConfig()
        {
            return GetInstance();
        }
        #endregion*/

        #region Metodos
        /*
         * Obtiene la instancia actual de Appconfig
         */
        /*private static AppConfig GetInstance()
        {
            if (instance == null)
            {
                lock (padLock)
                {
                    if (instance == null)
                        instance = new AppConfig();
                }
            }
            return instance;
        }*/

        private static void CrearArchivoXML()
        {
            XmlTextWriter xtwEscritor = new XmlTextWriter(_archivoConfiguracion, Encoding.Default);

            try
            {
                // Se establece el formato del XML
                xtwEscritor.Formatting = Formatting.Indented;
                xtwEscritor.Indentation = 4;

                // Escribe el encabezado del XML y la version del archivo de configuracion
                xtwEscritor.WriteStartDocument();
                xtwEscritor.WriteComment("AppConfig File, version 0.2");

                xtwEscritor.WriteStartElement(elementoRaiz);
                xtwEscritor.WriteEndElement();

                xtwEscritor.Flush();
                xtwEscritor.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool ExisteSeccion(string seccion)
        {
            XmlDocument xd = new XmlDocument();

            try
            {
                xd.Load(_archivoConfiguracion);
                XmlNode xn = xd.SelectSingleNode(elementoRaiz + "/" + seccion);

                if (xn != null)
                    return true;

                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool ExisteItem(string seccion, string variable)
        {
            XmlDocument xd = new XmlDocument();

            try
            {
                xd.Load(_archivoConfiguracion);
                XmlNode xn = xd.SelectSingleNode(elementoRaiz + "/" + seccion + "/" + variable);

                if (xn != null)
                    return true;

                return false;
            }
            catch
            {
                return false;
            }
        }

        public static string GetItem(string seccion, string variable)
        {            
            XmlDocument xd = new XmlDocument();

            try
            {
                xd.Load(_archivoConfiguracion);
                XmlNode xn = xd.SelectSingleNode(elementoRaiz + "/" + seccion + "/" + variable);

                return xn.InnerText != null ? xn.InnerText : "";
            }
            catch (Exception e)
            {
                throw e;
                /*ErrorManager em = new ErrorManager(ex);
                return null;*/
            }
        }

        public static string GetItem(string variable)
        {
            if (_seccion != null)
                return GetItem(_seccion, variable);
            else
                return null;
        }

        public static void SetItem(string seccion, string variable, string valor)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                if (!File.Exists(_archivoConfiguracion))
                    CrearArchivoXML();

                doc.Load(_archivoConfiguracion);

                XmlNode nodo = doc.SelectSingleNode(elementoRaiz + "/" + seccion);

                // La seccion no existe hay que crearla
                if (nodo == null)
                {
                    XmlElement elementoSeccion = doc.CreateElement(seccion);
                    doc.DocumentElement.AppendChild(elementoSeccion);

                    nodo = doc.SelectSingleNode(elementoRaiz + "/" + seccion);
                }

                XmlNode nodoEditar = doc.SelectSingleNode(elementoRaiz + "/" + seccion + "/" + variable);

                if (nodoEditar != null)
                {
                    nodoEditar.InnerText = valor;
                }
                else
                {
                    XmlElement elemento = doc.CreateElement(variable);
                    elemento.InnerText = valor;

                    nodo.AppendChild(elemento);
                }

                doc.Save(_archivoConfiguracion);
            }
            catch (Exception e)
            {
                throw e;
                /*ErrorManager em = new ErrorManager(ex);
                return;*/
            }
        }

        public static void SetItem(string variable, string valor)
        {
            if (_seccion != null)
                SetItem(_seccion, variable, valor);
            else
                return;
        }
        #endregion
    }
}