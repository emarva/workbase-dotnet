using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using WorkBase.Shared;

namespace WorkBase.UI
{
    #region GMapsTipoMapa
    public enum GMapsTipoMapa
    {
        Mapa,
        Satelite,
        Hibrido,
        Relieve
    }
    #endregion

    #region GMapsTipoCursor
    public enum GMapsTipoCursor
    {
        Auto = 0,
        Arrow = 1,
        Crosshair = 2,
        Default = 3,
        Help = 4,
        Move = 5,
        Pointer = 6,
        Text = 7,
        Wait = 8
    }
    #endregion

    public partial class GMapsViewer : UserControl
    {
        #region Variables
        private bool propUsarSensor;
        private string propLatitud = "0";
        private string propLongitud = "0";
        private int propZoom;
        private GMapsTipoMapa propTipoMapa;
        private GMapsTipoCursor propTipoCursor = GMapsTipoCursor.Default;
        private bool propMostrarControlesNavegacion;
        private bool propMostrarControlesTipoMapa;
        private bool propMostrarControlesEscala;
        private bool propCargado;
        
        private HtmlDocument hd;
        private bool eventoHDAsociado;
        private ArrayList marcadores = new ArrayList();
        private ArrayList poligonos = new ArrayList();
        private ArrayList polilineas = new ArrayList();
        #endregion

        #region Delegados
        public delegate void ClickMapEventHandler(GMapsLatLng coordenada);
        #endregion        

        #region Eventos
        public event ClickMapEventHandler ClickMap;
        #endregion

        #region Constructor
        public GMapsViewer()
        {
            InitializeComponent();
        }        
        #endregion

        #region Propiedades
        [DesignOnly(true),
        EditorBrowsable(EditorBrowsableState.Never),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        ParenthesizePropertyName(true),
        Editor(typeof(AboutEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public object Acerca { get { return null; } }

        public bool UsarSensor
        {
            get { return this.propUsarSensor; }
            set { this.propUsarSensor = value; }
        }

        public string Latitud
        {
            get { return this.propLatitud; }
            set { this.propLatitud = value; }
        }

        public string Longitud
        {
            get { return this.propLongitud; }
            set { this.propLongitud = value; }
        }

        public int Zoom
        {
            get { return this.propZoom; }
            set 
            {
                if (this.propCargado == true)
                    wbbGMaps.Document.InvokeScript("establecerZoom", new object[1] { value });

                this.propZoom = value;                
            }
        }

        public GMapsTipoMapa TipoMapa
        {
            get { return this.propTipoMapa; }
            set 
            {
                if (this.propCargado == true)                
                {
                    switch (value)
                    {
                        case GMapsTipoMapa.Mapa: wbbGMaps.Document.InvokeScript("cambiarTipoMapa", new object[1] { "ROADMAP" }); break;
                        case GMapsTipoMapa.Satelite: wbbGMaps.Document.InvokeScript("cambiarTipoMapa", new object[1] { "SATELLITE" }); break;
                        case GMapsTipoMapa.Hibrido: wbbGMaps.Document.InvokeScript("cambiarTipoMapa", new object[1] { "HYBRID" }); break;
                        case GMapsTipoMapa.Relieve: wbbGMaps.Document.InvokeScript("cambiarTipoMapa", new object[1] { "TERRAIN" }); break;
                    }
                }

                this.propTipoMapa = value;
            }
        }

        public GMapsTipoCursor TipoCursor
        {
            get { return this.propTipoCursor; }
            set
            {
                if (this.propCargado == true) 
                    wbbGMaps.Document.InvokeScript("cambiarCursor", new object[1] { value });

                this.propTipoCursor = value;
            }
        }

        public bool MostrarControlesNavegacion
        {
            get { return this.propMostrarControlesNavegacion; }
            set 
            {
                try
                {
                    if (this.propCargado)
                        wbbGMaps.Document.InvokeScript("mostrarControlesNavegacion", new object[1] { value });

                    this.propMostrarControlesNavegacion = value;
                }
                catch { }
            }
        }

        public bool MostrarControlesTipoMapa
        {
            get { return this.propMostrarControlesTipoMapa; }
            set
            {
                try
                {
                    if (this.propCargado)
                        wbbGMaps.Document.InvokeScript("mostrarControlesTipoMapa", new object[1] { value });

                    this.propMostrarControlesTipoMapa = value;
                }
                catch { }
            }
        }

        public bool MostrarControlesEscala
        {
            get { return this.propMostrarControlesEscala; }
            set
            {
                try
                {
                    if (this.propCargado)
                        wbbGMaps.Document.InvokeScript("mostrarControlesEscala", new object[1] { value });

                    this.propMostrarControlesEscala = value;
                }
                catch { }
            }
        }

        public bool Cargado
        {
            get { return this.propCargado; }
        }
        #endregion

        #region Metodos
        private string ConsultarURL(string url)
        {
            // Crea la solicitud de la URL.
            WebRequest request = WebRequest.Create(url);

            // Obtener la respuesta.
            WebResponse response = request.GetResponse();

            // Abrir el stream de la respuesta recibida.
            StreamReader reader = new StreamReader(response.GetResponseStream());

            // Leer el contenido.
            string res = reader.ReadToEnd();

            // Cerrar los streams abiertos.
            reader.Close();
            response.Close();

            return res;
        }

        public void Cargar()
        {
            StringBuilder html = new StringBuilder();
            string cursor = "";

            try
            {
                Assembly asm = Assembly.GetExecutingAssembly();
                string nombreRecurso = asm.GetName().Name + ".Resources.gmBase.html";

                using (StreamReader sr = new StreamReader(asm.GetManifestResourceStream(nombreRecurso)))
                {
                    html.Append(sr.ReadToEnd());

                    html.Replace("{SENSOR}", this.propUsarSensor == true ? "true" : "false");

                    if (this.propLatitud == null || this.propLongitud == null ||
                        this.propLatitud.Length == 0 || this.propLongitud.Length == 0)
                        throw new ArgumentNullException();

                    html.Replace("{LATITUD}", this.propLatitud);
                    html.Replace("{LONGITUD}", this.propLongitud);
                    html.Replace("{ZOOM}", this.propZoom.ToString());

                    switch (this.propTipoCursor)
                    {
                        case GMapsTipoCursor.Auto: cursor = "auto"; break;
                        case GMapsTipoCursor.Arrow: cursor = "default"; break;
                        case GMapsTipoCursor.Crosshair: cursor = "crosshair"; break;
                        case GMapsTipoCursor.Default: cursor = ""; break;
                        case GMapsTipoCursor.Help: cursor = "help"; break;
                        case GMapsTipoCursor.Move: cursor = "move"; break;
                        case GMapsTipoCursor.Pointer: cursor = "pointer"; break;
                        case GMapsTipoCursor.Text: cursor = "text"; break;
                        case GMapsTipoCursor.Wait: cursor = "wait"; break;
                    }

                    html.Replace("{CURSOR}", cursor);

                    string tipoMapa = string.Empty;

                    switch (this.propTipoMapa)
                    {
                        case GMapsTipoMapa.Mapa: tipoMapa = "ROADMAP"; break;
                        case GMapsTipoMapa.Satelite: tipoMapa = "SATELLITE"; break;
                        case GMapsTipoMapa.Hibrido: tipoMapa = "HYBRID"; break;
                        case GMapsTipoMapa.Relieve: tipoMapa = "TERRAIN"; break;
                    }

                    html.Replace("{TIPO_MAPA}", tipoMapa);
                    html.Replace("{CONTROLES_NAVEGACION}", this.propMostrarControlesNavegacion == true ? "true" : "false");
                    html.Replace("{CONTROLES_TIPO_MAPA}", this.propMostrarControlesTipoMapa == true ? "true" : "false");
                    html.Replace("{CONTROLES_ESCALA}", this.propMostrarControlesEscala == true ? "true" : "false");
                }
                
                wbbGMaps.DocumentText = html.ToString();     

                if (wbbGMaps.Document != null && !this.eventoHDAsociado)
                {
                    this.hd = wbbGMaps.Document;
                    this.hd.Click += new HtmlElementEventHandler(hd_Click);
                    this.eventoHDAsociado = true;
                }       
            }
            catch (Exception ex)
            {
                html.Remove(0, html.Length);
                html.AppendLine("<strong>Error al cargar el mapa.</strong><br><br>");
                html.AppendLine("Excepci&oacute;n: " + ex.Message);
                wbbGMaps.DocumentText = html.ToString();

                this.propCargado = false;                
            }
        }
        
        public void EstablecerCentro(GMapsLatLng coordenada)
        {            
            wbbGMaps.Document.InvokeScript("establecerCentro", new object[2] { coordenada.Latitud, coordenada.Longitud });
        }

        public void AgregarMarcador(GMapsMarker marcador)
        {
            try
            {
                if (this.propCargado)
                {
                    if (marcador.Nombre == null)
                        marcador.Nombre = "Marcador" + this.marcadores.Count;

                    wbbGMaps.Document.InvokeScript("agregarMarcador", new object[5] {marcador.Latitud, marcador.Longitud, marcador.Icono, marcador.Titulo, marcador.Contenido });
                    this.marcadores.Add(marcador);
                }
            }
            catch { }
        }

        public void AgregarMarcador(string nombre, string latitud, string longitud, string icono, string titulo, string contenido)
        {
            GMapsMarker marcador = new GMapsMarker();

            marcador.Nombre = nombre;
            marcador.Latitud = latitud;
            marcador.Longitud = longitud;
            marcador.Icono = icono;
            marcador.Titulo = titulo;
            marcador.Contenido = contenido;

            this.AgregarMarcador(marcador);
        }

        public void AgregarMarcador(string latitud, string longitud, string icono, string titulo, string contenido)
        {
            this.AgregarMarcador(null, latitud, longitud, icono, titulo, contenido);
        }

        public void AgregarMarcador(string latitud, string longitud, string icono, string titulo)
        {
            this.AgregarMarcador(null, latitud, longitud, icono, titulo, null);
        }

        public void AgregarMarcador(string latitud, string longitud, string icono)
        {
            this.AgregarMarcador(null, latitud, longitud, icono, null, null);
        }

        public void AgregarMarcador(string latitud, string longitud)
        {
            this.AgregarMarcador(null, latitud, longitud, null, null, null);
        }

        public void EliminarMarcador(int indice)
        {
            try
            {
                if (this.propCargado)
                {
                    wbbGMaps.Document.InvokeScript("eliminarMarcador", new object[1] { indice });
                    this.marcadores.RemoveAt(indice);
                }
            }
            catch { }
        }

        public void EliminarMarcador(string nombre)
        {
            try
            {
                if (this.propCargado)
                {
                    int indice = -1;
                    
                    for (int i = 0; i < this.marcadores.Count; i++)
                    {
                        if (((GMapsMarker)this.marcadores[i]).Nombre == nombre)
                            indice = i;
                    }

                    if (indice != -1)
                    {
                        wbbGMaps.Document.InvokeScript("eliminarMarcador", new object[] { indice });
                        this.marcadores.RemoveAt(indice);
                    }
                }
            }
            catch { }
        }        

        public void EliminarMarcadores()
        {
            try
            {
                if (this.propCargado)
                {
                    wbbGMaps.Document.InvokeScript("eliminarMarcadores");
                    this.marcadores.Clear();
                }
            }
            catch { }
        }

        public void AgregarPolilinea(GMapsPolyline polilinea)
        {
            try
            {
                if (this.propCargado)
                {
                    foreach (GMapsLatLng latlng in polilinea.Coordenadas)
                    {
                        wbbGMaps.Document.InvokeScript("agregarCoordenada", new object[2] { latlng.Latitud, latlng.Longitud });
                    }

                    wbbGMaps.Document.InvokeScript("agregarPolilinea", new object[3] { polilinea.Color, polilinea.Opacidad, polilinea.Ancho });
                    this.polilineas.Add(polilinea);
                }
            }
            catch { }
        }

        public void AgregarPolilinea(string nombre, List<GMapsLatLng> coordenadas, string color, string opacidad, string ancho)
        {
            GMapsPolyline polilinea = new GMapsPolyline();

            polilinea.Nombre = nombre;

            foreach (GMapsLatLng latlng in coordenadas)
            {
                polilinea.Coordenadas.Add(latlng);
            }            

            polilinea.Color = color;
            polilinea.Opacidad = opacidad;
            polilinea.Ancho = ancho;

            this.AgregarPolilinea(polilinea);
        }

        public void AgregarPolilinea(List<GMapsLatLng> coordenadas, string color, string opacidad, string ancho)
        {
            this.AgregarPolilinea(null, coordenadas, color, opacidad, null);
        }

        public void AgregarPolilinea(List<GMapsLatLng> coordenadas, string color, string opacidad)
        {
            this.AgregarPolilinea(coordenadas, color, opacidad, null);
        }

        public void AgregarPolilinea(List<GMapsLatLng> coordenadas, string color)
        {
            this.AgregarPolilinea(coordenadas, color, null, null);
        }

        public void EliminarPolilinea(int indice)
        {
            try
            {
                if (this.propCargado)
                {
                    wbbGMaps.Document.InvokeScript("eliminarPolilinea", new object[1] { indice });
                    this.polilineas.RemoveAt(indice);
                }
            }
            catch { }
        }

        public void EliminarPolilinea(string nombre)
        {
            try
            {
                if (this.propCargado)
                {
                    int indice = -1;

                    for (int i = 0; i < this.polilineas.Count; i++)
                    {
                        if (((GMapsPolyline)this.polilineas[i]).Nombre == nombre)
                            indice = i;
                    }

                    if (indice != -1)
                    {
                        wbbGMaps.Document.InvokeScript("eliminarPolilinea", new object[1] { indice });
                        this.polilineas.RemoveAt(indice);
                    }
                }
            }
            catch { }
        }

        public void EliminarPolilineas()
        {
            try
            {
                if (this.propCargado)
                {
                    wbbGMaps.Document.InvokeScript("eliminarPolilineas");
                    this.polilineas.Clear();
                }
            }
            catch { }
        }
        
        public void AgregarPoligono(GMapsPolygon poligono)
        {
            try
            {
                if (this.propCargado)
                {
                    foreach (GMapsLatLng latlng in poligono.Coordenadas)
                    {
                        wbbGMaps.Document.InvokeScript("agregarCoordenada", new object[2] { latlng.Latitud, latlng.Longitud });
                    }

                    wbbGMaps.Document.InvokeScript("agregarPoligono", new object[5] { poligono.Color, poligono.Opacidad, poligono.Ancho, poligono.RellenoColor, poligono.RellenoOpacidad });
                    this.poligonos.Add(poligono);
                }
            }
            catch { }
        }

        public void AgregarPoligono(string nombre, List<GMapsLatLng> coordenadas, string color, string opacidad, string ancho, string rellenoColor, string rellenoOpacidad)
        {
            GMapsPolygon poligono = new GMapsPolygon();

            poligono.Nombre = nombre;

            foreach (GMapsLatLng latlng in coordenadas)
            {
                poligono.Coordenadas.Add(latlng);
            }
            
            poligono.Color = color;
            poligono.Opacidad = opacidad;
            poligono.Ancho = ancho;
            poligono.RellenoColor = rellenoColor;
            poligono.RellenoOpacidad = rellenoOpacidad;

            this.AgregarPoligono(poligono);
        }

        public void AgregarPoligono(List<GMapsLatLng> coordenadas, string color, string opacidad, string ancho, string rellenoColor, string rellenoOpacidad)
        {
            this.AgregarPoligono(null, coordenadas, color, opacidad, ancho, rellenoColor, null);
        }

        public void AgregarPoligono(List<GMapsLatLng> coordenadas, string color, string opacidad, string ancho, string rellenoColor)
        {
            this.AgregarPoligono(coordenadas, color, opacidad, ancho, rellenoColor, null);
        }

        public void AgregarPoligono(List<GMapsLatLng> coordenadas, string color, string opacidad, string rellenoColor)
        {
            this.AgregarPoligono(coordenadas, color, opacidad, null, rellenoColor, null);
        }

        public void AgregarPoligono(List<GMapsLatLng> coordenadas, string color, string rellenoColor)
        {
            this.AgregarPoligono(coordenadas, color, null, null, rellenoColor, null);
        }

        public void EliminarPoligono(int indice)
        {
            try
            {
                if (this.propCargado)
                {
                    wbbGMaps.Document.InvokeScript("eliminarPoligono", new object[1] { indice });
                    this.poligonos.RemoveAt(indice);
                }
            }
            catch { }
        }

        public void EliminarPoligono(string nombre)
        {
            try
            {
                if (this.propCargado)
                {
                    int indice = -1;

                    for (int i = 0; i < this.poligonos.Count; i++)
                    {
                        if (((GMapsPolygon)this.poligonos[i]).Nombre == nombre)
                            indice = i;
                    }

                    if (indice != -1)
                    {
                        wbbGMaps.Document.InvokeScript("eliminarPoligono", new object[1] { indice });
                        this.poligonos.RemoveAt(indice);
                    }
                }
            }
            catch { }
        }

        public void EliminarPoligonos()
        {
            try
            {
                if (this.propCargado)
                {
                    wbbGMaps.Document.InvokeScript("eliminarPoligonos");
                    this.poligonos.Clear();
                }
            }
            catch { }
        }
        
        public int ObtenerZoom()
        {
            try
            {
                return int.Parse(wbbGMaps.Document.InvokeScript("obtenerZoom").ToString());
            }
            catch { return 0; }
        }

        public GMapsLatLng ObtenerCentro()
        {
            try
            {
                string[] latLng = wbbGMaps.Document.InvokeScript("obtenerCentro").ToString().Split(',');
                return new GMapsLatLng(latLng[0], latLng[1]);
            }
            catch { return null; }
        }        

        public string GeoposicionPorCoordenada(string latitud, string longitud)
        {
            try
            {
                string res = this.ConsultarURL("http://maps.google.com/maps/geo?q=" + latitud + "," + longitud + "&output=csv");
                int pos = res.IndexOf('"');

                return res.Substring(pos + 1, (res.Length - pos) - 2);
            }
            catch { return null; }
        }

        public GMapsLatLng GeoposicionPorDireccion(string direccion)
        {
            try
            {
                string[] res = this.ConsultarURL("http://maps.google.com/maps/geo?q=" + direccion + "&output=csv").Split(',');
                GMapsLatLng latLng = new GMapsLatLng(res[2], res[3]);

                return latLng;
                
            }
            catch { return null; }
        }

        private string ObtenerPosicionClick()
        {
            try
            {
                return wbbGMaps.Document.InvokeScript("obtenerPosicion").ToString();
            }
            catch (Exception ex) { return null; }
        }

        private void hd_Click(object sender, HtmlElementEventArgs e)
        {
            try
            {
                if (ClickMap != null)
                {
                    string[] latLng = ObtenerPosicionClick().Split(',');

                    ClickMap(new GMapsLatLng(latLng[0], latLng[1]));
                }
            }
            catch { }
        }

        private void wbbGMaps_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.propCargado = true;
        }
        #endregion

        #region Overrides
        public override void Refresh()
        {
            wbbGMaps.Refresh();    
        }      
        #endregion        
    }
}
