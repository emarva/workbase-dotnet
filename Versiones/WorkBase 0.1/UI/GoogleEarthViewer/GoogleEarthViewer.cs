using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using WorkBase.Shared;

namespace WorkBase.UI
{
    #region GENavigationControlVisibility
    public enum GENavigationControlVisibility
    {
        Hide,
        Show,
        Auto
    }
    #endregion

    #region GEKmlRefreshMode
    public enum GEKmlRefreshMode
    {
        None,
        Change,
        Interval,
        Expire
    }
    #endregion

    #region GEKmlAltitudeMode
    public enum GEKmlAltitudeMode
    {
        Absolute,
        RelativeToGround
    }
    #endregion

    public partial class GoogleEarthViewer : UserControl
    {
        #region Campos
        private GENavigationControlVisibility _controlNavegacion = GENavigationControlVisibility.Hide;
        private bool _mostrarCaminos;
        private bool _mostrarEdificios;
        private bool _mostrarEdificiosBajaResolucion;
        private bool _mostrarFronteras;
        private bool _mostrarTerreno;
        #endregion

        #region Variables
        private bool cargado;
        private ArrayList marcas = new ArrayList();
        private ArrayList enlacesRed = new ArrayList();
        private ArrayList poligonos = new ArrayList();
        #endregion

        #region Constructor
        public GoogleEarthViewer()
        {
            InitializeComponent();
        }
        #endregion

        #region Propiedades
        public bool Cargado
        {
            get 
            {
                try
                {
                    cargado = (bool)wbbGE.Document.InvokeScript("estaPlanetaCargado");
                }
                catch { cargado = false; }
                return cargado;
            }
        }

        public GENavigationControlVisibility ControlNavegacion
        {
            get { return _controlNavegacion; }
            set
            {
                try
                {
                    if (Cargado)
                        wbbGE.Document.InvokeScript("controlNavegacion", new object[1] { value });

                    _controlNavegacion = value;
                }
                catch { }
            }
        }
        public bool MostrarCaminos
        {
            get { return _mostrarCaminos; }
            set 
            {
                try
                {
                    if (Cargado)
                        wbbGE.Document.InvokeScript("mostrarCaminos", new object[1] { value });

                    _mostrarCaminos = value;
                }
                catch { }
            }
        }

        public bool MostrarEdificios
        {
            get { return _mostrarEdificios; }
            set
            {
                try
                {
                    if (Cargado)
                        wbbGE.Document.InvokeScript("mostrarEdificios", new object[1] { value });

                    _mostrarEdificios = value;
                }
                catch { }
            }
        }

        public bool MostrarEdificiosBajaResolucion
        {
            get { return _mostrarEdificiosBajaResolucion; }
            set
            {
                try
                {
                    if (Cargado)
                        wbbGE.Document.InvokeScript("mostrarEdificiosBajaResolucion", new object[1] { value });

                    _mostrarEdificiosBajaResolucion = value;
                }
                catch { }
            }
        }

        public bool MostrarFronteras
        {
            get { return _mostrarFronteras; }
            set
            {
                try
                {
                    if (Cargado)
                        wbbGE.Document.InvokeScript("mostrarFronteras", new object[1] { value });

                    _mostrarFronteras = value;
                }
                catch { }
            }
        }

        public bool MostrarTerreno
        {
            get { return _mostrarTerreno; }
            set
            {
                try
                {
                    if (Cargado)
                        wbbGE.Document.InvokeScript("mostrarTerreno", new object[1] { value });

                    _mostrarTerreno = value;
                }
                catch { }
            }
        }        
        #endregion

        #region Metodos
        public void Cargar()
        {
            StringBuilder html = new StringBuilder();

            try
            {
                Assembly asm = Assembly.GetExecutingAssembly();
                string nombreRecurso = asm.GetName().Name + ".Resources.geBase.html";

                using (StreamReader sr = new StreamReader(asm.GetManifestResourceStream(nombreRecurso)))
                {
                    html.Append(sr.ReadToEnd());
                }

                // Aplico las propiedades
                int controlNav = 0;

                switch (this._controlNavegacion)
                {
                    case GENavigationControlVisibility.Show: controlNav = 1; break;
                    case GENavigationControlVisibility.Auto: controlNav = 2; break;
                }

                html.Replace("{CONTROL_NAVEGACION}", controlNav.ToString());
                html.Replace("{CAMINOS}", (_mostrarCaminos) ? "true" : "false");
                html.Replace("{EDIFICIOS}", (_mostrarEdificios) ? "true" : "false");
                html.Replace("{EDIFICIOS_BAJA_RESOLUCION}", (_mostrarEdificiosBajaResolucion) ? "true" : "false");
                html.Replace("{FRONTERAS}", (_mostrarFronteras) ? "true" : "false");
                html.Replace("{TERRENO}", (_mostrarTerreno) ? "true" : "false");

                wbbGE.DocumentText = html.ToString();
            }
            catch (Exception ex)
            {
                html.Remove(0, html.Length);
                html.AppendLine("<strong>Error al cargar el mapa.</strong><br><br>");
                html.AppendLine("Excepci&oacute;n: " + ex.Message);
                wbbGE.DocumentText = html.ToString();
            }
        }

        public GEKmlEvent ObtenerClickGlobo()
        {
            try
            {
                GEKmlEvent evento = new GEKmlEvent();
                string[] info = wbbGE.Document.InvokeScript("obtenerClickGlobo").ToString().Split('_');

                evento.Button = Convert.ToInt32(info[0]);
                evento.ClientX = Convert.ToInt32(info[1]);
                evento.ClientY = Convert.ToInt32(info[2]);
                evento.ScreenX = Convert.ToInt32(info[3]);
                evento.ScreenY = Convert.ToInt32(info[4]);
                evento.Latitude = Convert.ToDouble(info[5]);
                evento.Longitude = Convert.ToDouble(info[6]);
                evento.Altitude = Convert.ToDouble(info[7]);
                evento.DidHitGlobe = (info[8] == "1") ? true : false;
                evento.AltKey = (info[9] == "1") ? true : false;
                evento.CtrlKey = (info[10] == "1") ? true : false;
                evento.ShiftKey = (info[11] == "1") ? true : false;
                evento.TimeStamp = Convert.ToInt32(info[12]);

                return evento;
            }
            catch { return null; }
        }

        public GEKmlEvent ObtenerClickVentana()
        {
            try
            {
                GEKmlEvent evento = new GEKmlEvent();
                string[] info = wbbGE.Document.InvokeScript("obtenerClickVentana").ToString().Split('_');

                evento.Button = Convert.ToInt32(info[0]);
                evento.ClientX = Convert.ToInt32(info[1]);
                evento.ClientY = Convert.ToInt32(info[2]);
                evento.ScreenX = Convert.ToInt32(info[3]);
                evento.ScreenY = Convert.ToInt32(info[4]);
                evento.Latitude = Convert.ToDouble(info[5]);
                evento.Longitude = Convert.ToDouble(info[6]);
                evento.Altitude = Convert.ToDouble(info[7]);
                evento.DidHitGlobe = (info[8] == "1") ? true : false;
                evento.AltKey = (info[9] == "1") ? true : false;
                evento.CtrlKey = (info[10] == "1") ? true : false;
                evento.ShiftKey = (info[11] == "1") ? true : false;
                evento.TimeStamp = Convert.ToInt32(info[12]);

                return evento;
            }
            catch { return null; }
        }

        public void AgregarMarca(GEKmlPlacemark marca)
        {
            try
            {
                if (Cargado)
                {                    
                    wbbGE.Document.InvokeScript("agregarMarca", new object[5] { marca.Nombre, marca.Icono, marca.EscalaIcono, marca.Latitud, marca.Longitud });
                    marcas.Add(marca);
                }
            }
            catch { }
        }

        public void EliminarMarca(int indice)
        {
            try
            {
                if (Cargado)
                {
                    wbbGE.Document.InvokeScript("eliminarMarca", new object[1] { indice });
                    marcas.RemoveAt(indice);
                }
            }
            catch { }
        }

        public void EliminarMarca(string nombre)
        {
            try
            {
                if (Cargado)
                {
                    int indice = -1;
                    for (int i = 0; i < marcas.Count; i++)
                    {
                        if (((GEKmlPlacemark)marcas[i]).Nombre == nombre)
                            indice = i;
                    }

                    if (indice != -1)
                    {
                        wbbGE.Document.InvokeScript("eliminarMarca", new object[1] { indice });
                        marcas.RemoveAt(indice);
                    }
                }
            }
            catch { }
        }

        public void EliminarMarcas()
        {
            try
            {
                if (Cargado)
                {
                    wbbGE.Document.InvokeScript("eliminarMarcas");
                    marcas.Clear();
                }
            }
            catch { }
        }

        public void AgregarEnlaceRed(GEKmlNetworkLink enlaceRed)
        {
            try
            {
                if (Cargado)
                {                    
                    wbbGE.Document.InvokeScript("agregarEnlaceRed", new object[5] { enlaceRed.Enlace, enlaceRed.ActualizarVisibilidad, enlaceRed.VolarParaVer, enlaceRed.ModoActualizacion, enlaceRed.IntervaloActualizacion });
                    enlacesRed.Add(enlaceRed);
                }
            }
            catch { }
        }

        public void EliminarEnlaceRed(int indice)
        {
            try
            {
                if (Cargado)
                {
                    wbbGE.Document.InvokeScript("eliminarEnlaceRed", new object[1] { indice });
                    enlacesRed.RemoveAt(indice);
                }
            }
            catch { }
        }

        public void EliminarEnlaceRed(string nombre)
        {
            try
            {
                if (Cargado)
                {
                    int indice = -1;
                    for (int i = 0; i < this.enlacesRed.Count; i++)
                    {
                        if (((GEKmlNetworkLink)enlacesRed[i]).Nombre == nombre)
                            indice = i;
                    }

                    if (indice != -1)
                    {
                        wbbGE.Document.InvokeScript("eliminarEnlaceRed", new object[1] { indice });
                        enlacesRed.RemoveAt(indice);
                    }
                }
            }
            catch { }
        }

        public void EliminarEnlacesRed()
        {
            try
            {
                if (Cargado)
                {
                    wbbGE.Document.InvokeScript("eliminarEnlacesRed");
                    enlacesRed.Clear();
                }
            }
            catch { }
        }

        public void AgregarPoligono(GEKmlPolygon poligono)
        {
            try
            {
                if (Cargado)
                {
                    foreach (GEKmlCoord coordenada in poligono.Coordenadas)
                    {
                        wbbGE.Document.InvokeScript("agregarCoordenada", new object[] { coordenada.Latitud, coordenada.Longitud, coordenada.Altitud });
                    }

                    wbbGE.Document.InvokeScript("agregarPoligono", new object[] { poligono.Color, poligono.AnchoLinea, poligono.ColorLinea });
                    poligonos.Add(poligono);
                }
            }
            catch { }
        }

        public void EliminarPoligono(int indice)
        {
            try
            {
                if (Cargado)
                {
                    wbbGE.Document.InvokeScript("eliminarPoligono", new object[] { indice });
                    poligonos.RemoveAt(indice);
                }
            }
            catch { }
        }

        public void EliminarPoligono(string nombre)
        {
            try
            {
                if (Cargado)
                {
                    int indice = -1;

                    for (int i = 0; i < poligonos.Count; i++)
                    {
                        if (((GMapsPolygon)poligonos[i]).Nombre == nombre)
                            indice = i;
                    }

                    if (indice != -1)
                    {
                        wbbGE.Document.InvokeScript("eliminarPoligono", new object[] { indice });
                        poligonos.RemoveAt(indice);
                    }
                }
            }
            catch { }
        }

        public void EliminarPoligonos()
        {
            try
            {
                if (Cargado)
                {
                    wbbGE.Document.InvokeScript("eliminarPoligonos");
                    poligonos.Clear();
                }
            }
            catch { }
        }

        public void LookAt(double latitud, double longitud, double altitud, GEKmlAltitudeMode modoAltitud, double direccion, double inclinacion, double rango)
        {
            try
            {
                if (Cargado)
                {
                    wbbGE.Document.InvokeScript("lookAt", new object[]{ latitud, longitud, altitud, modoAltitud, direccion, inclinacion, rango });
                }
            }
            catch { }
        }

        private void wbbGE_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //_cargado = true;
        }
        #endregion

        #region Overrides
        public override void Refresh()
        {
            wbbGE.Refresh();
        }
        #endregion        
    }
}
