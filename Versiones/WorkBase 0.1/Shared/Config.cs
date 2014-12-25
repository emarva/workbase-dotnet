using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace WorkBase.Shared
{
    public sealed class Config
    {
        #region Constantes
        // Nombre del elemento raiz del archivo de configuracion
        private const string elementoRaiz = "Configuration";
        #endregion

        #region Variables 
        private static string propArchivoConfiguracion;
        private static string propSeccion;
        #endregion

        #region Propiedades
        public static string ArchivoConfiguracion
        {
            get { return propArchivoConfiguracion; }
            set { propArchivoConfiguracion = value; }
        }

        public static string Seccion
        {
            get { return propSeccion; }
            set { propSeccion = value; }
        }
        #endregion

        #region Metodos
        private static void CrearArchivoXML()
        {
            XmlTextWriter xtwEscritor = new XmlTextWriter(propArchivoConfiguracion, Encoding.Default);

            try
            {
                // Se establece el formato del XML
                xtwEscritor.Formatting = Formatting.Indented;
                xtwEscritor.Indentation = 4;

                // Escribe el encabezado del XML y la version del archivo de configuracion
                xtwEscritor.WriteStartDocument();
                xtwEscritor.WriteComment("Configuration File, version 0.1");

                xtwEscritor.WriteStartElement(elementoRaiz);
                xtwEscritor.WriteEndElement();

                xtwEscritor.Flush();
                xtwEscritor.Close();
            }
            catch (Exception ex)
            {
                throw ex;
                //ErrorManager em = new ErrorManager(ex);
                //return;
            }
        }

        public static bool ExisteSeccion(string seccion)
        {
            XmlDocument xd = new XmlDocument();

            try
            {
                xd.Load(propArchivoConfiguracion);
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
                xd.Load(propArchivoConfiguracion);
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
                xd.Load(propArchivoConfiguracion);
                XmlNode xn = xd.SelectSingleNode(elementoRaiz + "/" + seccion + "/" + variable);

                return xn.InnerText != null ? xn.InnerText : "";
            }
            catch (Exception ex)
            {
                throw ex;
                /*ErrorManager em = new ErrorManager(ex);
                return null;*/
            }
        }

        public static string GetItem(string variable)
        {
            if (propSeccion != null)
                return GetItem(propSeccion, variable);
            else
                return null;
        }

        public static void SetItem(string seccion, string variable, string valor)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                if (!File.Exists(propArchivoConfiguracion))
                    CrearArchivoXML();

                doc.Load(propArchivoConfiguracion);

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

                doc.Save(propArchivoConfiguracion);
            }
            catch (Exception ex)
            {
                throw ex;
                /*ErrorManager em = new ErrorManager(ex);
                return;*/
            }
        }

        public static void SetItem(string variable, string valor)
        {
            if (propSeccion != null)
                SetItem(propSeccion, variable, valor);
            else
                return;
        }
        #endregion
    }
}