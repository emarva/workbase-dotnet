using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace WorkBase.Shared
{
    public sealed class PrinterLinea
    {
        #region Variables
        private string propTexto;
        private float propTamanoFuente;
        private bool propNegrita;
        private bool propCursiva;
        private PrinterAlineacionLinea propAlineacion = PrinterAlineacionLinea.Izquierda;
        #endregion

        #region Constructor
        public PrinterLinea(string texto, float tamanoFuente, bool negrita, bool cursiva, PrinterAlineacionLinea alineacion)
        {
            this.propTexto = texto;
            this.propTamanoFuente = tamanoFuente;
            this.propNegrita = negrita;
            this.propCursiva = cursiva;
            this.propAlineacion = alineacion;
        }
        #endregion

        #region Propiedades
        public string Texto
        {
            get { return this.propTexto; }
            set { this.propTexto = value; }
        }

        public float TamanoFuente
        {
            get { return this.propTamanoFuente; }
            set { this.propTamanoFuente = value; }
        }

        public bool Negrita
        {
            get { return this.propNegrita; }
            set { this.propNegrita = value; }
        }

        public bool Cursiva
        {
            get { return this.propCursiva; }
            set { this.propCursiva = value; }
        }

        public PrinterAlineacionLinea Alineacion
        {
            get { return this.propAlineacion; }
            set { this.propAlineacion = value; }
        }
        #endregion
    }

    public sealed class Printer
    {
        #region Constantes
        private const short FILE_ATTRIBUTE_NORMAL = 0x80;
        private const short INVALID_HANDLE_VALUE = -1;
        private const uint GENERIC_READ = 0x80000000;
        private const uint GENERIC_WRITE = 0x40000000;
        private const uint CREATE_NEW = 1;
        private const uint CREATE_ALWAYS = 2;
        private const uint OPEN_EXISTING = 3;
        #endregion       

        #region Variables
        private string propImpresora;
        private Font propFuente = null;
        private int propLargoMaximoCaracteres = 40;
        private Image propImagenCabecera = null;
        private int propAltoImagen = 0;
        private int propMargenIzquierdo = 0;
        private int propMargenSuperior = 3;
        private short propCopias = 1;
        private PrinterTipoGuillotina propTipoGuillotina = PrinterTipoGuillotina.Epson;

        private ArrayList lineas = new ArrayList();
        private Graphics gfx = null;
        private int contador = 0;
        #endregion

        #region DLL Import
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern SafeFileHandle CreateFile(string lpFileName, uint dwDesiredAccess,
            uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition,
            uint dwFlagsAndAttributes, IntPtr hTemplateFile);
        #endregion

        #region Propiedades
        public string Impresora
        {
            get { return this.propImpresora; }
            set { this.propImpresora = value; }
        }

        public Font Fuente
        {
            get { return this.propFuente; }
            set { this.propFuente = value; }
        }

        public int LargoMaximoCaracteres
        {
            get { return this.propLargoMaximoCaracteres; }
            set { this.propLargoMaximoCaracteres = value; }
        }

        public Image ImagenCabecera
        {
            get { return this.propImagenCabecera; }
            set { this.propImagenCabecera = value; }
        }

        public int AltoImagen
        {
            get { return this.propAltoImagen; }
            set { this.propAltoImagen = value; }
        }

        public int MargenIzquierdo
        {
            get { return this.propMargenIzquierdo; }
            set { this.propMargenIzquierdo = value; }
        }

        public int MargenSuperior
        {
            get { return this.propMargenSuperior; }
            set { this.propMargenSuperior = value; }
        }

        public short Copias
        {
            get { return this.propCopias; }
            set { this.propCopias = value; }
        }

        public PrinterTipoGuillotina TipoGuillotina
        {
            get { return this.propTipoGuillotina; }
            set { this.propTipoGuillotina = value; }
        }
        #endregion

        #region Metodos
        public bool ExisteImpresora(string impresora)
        {
            foreach (string _impresora in PrinterSettings.InstalledPrinters)
            {
                if (impresora == _impresora)
                    return true;
            }

            return false;
        }

        public bool ExisteImpresora()
        {
            return this.ExisteImpresora(propImpresora);
        }

        public void AgregarLineaTexto(string texto, float tamanoFuente, bool negrita, bool cursiva, PrinterAlineacionLinea alineacion)
        {
            if (texto.Length > this.propLargoMaximoCaracteres)
                texto = texto.Substring(0, this.propLargoMaximoCaracteres);
            //else
            //    texto = string.Format("{0,-" + this.propLargoMaximoCaracteres + "}", texto);

            PrinterLinea linea = new PrinterLinea(texto, tamanoFuente, negrita, cursiva, alineacion);
            this.lineas.Add(linea);
        }

        public void AgregarLineaTexto(string texto, float tamanoFuente, bool negrita, PrinterAlineacionLinea alineacion)
        {
            this.AgregarLineaTexto(texto, tamanoFuente, negrita, false, alineacion);
        }

        public void AgregarLineaTexto(string texto, bool negrita, PrinterAlineacionLinea alineacion)
        {
            this.AgregarLineaTexto(texto, 0, negrita, false, alineacion);
        }

        public void AgregarLineaTexto(string texto, bool negrita)
        {
            this.AgregarLineaTexto(texto, 0, negrita, false, PrinterAlineacionLinea.Izquierda);
        }

        public void AgregarLineaTexto(string texto)
        {
            this.AgregarLineaTexto(texto, 0, false, false, PrinterAlineacionLinea.Izquierda);
        }

        public void AgregarLineaEnBlanco()
        {
            PrinterLinea linea = new PrinterLinea("", 0, false, false, PrinterAlineacionLinea.Izquierda);
            this.lineas.Add(linea);
        }

        public void AgregarLineaSimple(bool negrita)
        {
            string lineaSimple = "";

            for (int i = 0; i < this.propLargoMaximoCaracteres; i++)
            {
                lineaSimple += "-";
            }

            this.AgregarLineaTexto(lineaSimple, negrita);
        }

        public void AgregarLineaSimple()
        {
            this.AgregarLineaSimple(false);
        }

        public void AgregarLineaPuntos(bool negrita)
        {
            string lineaPuntos = "";

            for (int i = 0; i < this.propLargoMaximoCaracteres; i++)
            {
                lineaPuntos += "=";
            }

            this.AgregarLineaTexto(lineaPuntos, negrita);
        }

        public void AgregarLineaPuntos()
        {
            this.AgregarLineaPuntos(false);
        }

        private void ImpresionTermica(string puerto, bool cortarPapel)
        {
            SafeFileHandle sfh = CreateFile(puerto, GENERIC_WRITE, 0, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);

            /* Is bad handle? INVALID_HANDLE_VALUE */
            if (sfh.IsInvalid)
            {
                /* ask the framework to marshall the win32 error code to an exception */
                Marshal.ThrowExceptionForHR(Marshal.GetLastWin32Error());
            }
            else
            {
                //This is the cut command on Star TSP 600 Printer
                //                char[] prnCmdCut = { (char)29, (char)86, (char)1 };

                FileStream port = new FileStream(sfh, FileAccess.ReadWrite);
                Byte[] buffer = new Byte[propLargoMaximoCaracteres];

                for (int i = 0; i < this.propCopias; i++)
                {
                    foreach (PrinterLinea linea in lineas)
                    {
                        // Establesco la alineacion
                        //int x = this.propMargenIzquierdo;

                        switch (linea.Alineacion)
                        {
                            /*case TicketAlineacionLinea.Izquierda:
                                linea.Texto = string.Format("|{0:-" + propLargoMaximoCaracteres.ToString() + "}|", linea.Texto);

                                break;
                            case TicketAlineacionLinea.Centro:
                                x = (int)(gfx.ClipBounds.Width - gfx.MeasureString(linea.Texto, fuente).Width) / 2;

                                break;*/
                            case PrinterAlineacionLinea.Derecha:
                                string espacios = "";
                                int largo = linea.Texto.Trim().Length;

                                if (largo != this.propLargoMaximoCaracteres)
                                    largo = this.propLargoMaximoCaracteres - largo;

                                for (int x = 0; x < largo; x++)
                                    espacios += " ";

                                linea.Texto = espacios + linea.Texto;

                                break;
                        }

                        buffer = System.Text.Encoding.ASCII.GetBytes(linea.Texto + "\n");
                        port.Write(buffer, 0, buffer.Length);
                    }

                    if (cortarPapel)
                    {
                        // Verifica el tipo de guillotina termica
                        if (this.propTipoGuillotina == PrinterTipoGuillotina.Epson)
                        {
                            buffer = System.Text.Encoding.ASCII.GetBytes(Convert.ToChar(29).ToString() + Convert.ToChar(86).ToString() + "1");
                            port.Write(buffer, 0, buffer.Length);
                        }
                        else
                        {
                            buffer = System.Text.Encoding.ASCII.GetBytes(Convert.ToChar(27).ToString() + Convert.ToChar(100).ToString() + Convert.ToChar(51).ToString());
                            port.Write(buffer, 0, buffer.Length);
                        }
                    }                    
                }

                port.Close();
            }
        }

        public void Imprimir(bool esTermica, bool cortarPapel)
        {
            if (!esTermica)
            {
                PrintDocument pd = new PrintDocument();
                pd.PrintController = new StandardPrintController();
                pd.PrinterSettings.PrinterName = this.propImpresora;
                pd.PrinterSettings.Copies = this.propCopias;
                pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                pd.Print();
            }
            else
            {
                ImpresionTermica(propImpresora, cortarPapel);
            }
        }

        public void Imprimir(bool esTermica)
        {
            this.Imprimir(esTermica, false);
        }

        public void Imprimir()
        {
            this.Imprimir(false, false);
        }

        public void Limpiar()
        {
            this.lineas.Clear();
        }        

        private float PosicionY()
        {
            return this.propMargenSuperior + (contador * this.propFuente.GetHeight(gfx) + this.propAltoImagen);
        }

        private void DibujarImagen()
        {
            if (this.propImagenCabecera != null)
            {
                try
                {
                    gfx.DrawImage(this.propImagenCabecera, new Point(this.propMargenIzquierdo, (int)PosicionY()));
                    double alto = ((double)this.propImagenCabecera.Height / 58) * 15;
                    this.propAltoImagen = (int)Math.Round(alto) + 3;
                }
                catch { }
            }
        }

        private void DibujarLineas()
        {
            foreach (PrinterLinea linea in lineas)
            {
                float tamanoFuente = propFuente.Size;
                FontStyle estiloFuente = new FontStyle();

                if (linea.TamanoFuente != 0)
                    tamanoFuente = linea.TamanoFuente;

                if (linea.Negrita)
                    estiloFuente = FontStyle.Bold;

                if (linea.Cursiva)
                    estiloFuente = FontStyle.Italic;

                if (linea.Negrita && linea.Cursiva)
                    estiloFuente = FontStyle.Bold | FontStyle.Italic;

                // Construyo la fuente especifica para la linea
                Font fuente = new Font(propFuente.FontFamily, tamanoFuente, estiloFuente);

                // Establesco la alineacion
                int x = this.propMargenIzquierdo;

                switch (linea.Alineacion)
                {
                    /*case TicketAlineacionLinea.Izquierda:
                        linea.Texto = string.Format("|{0:-" + propLargoMaximoCaracteres.ToString() + "}|", linea.Texto);

                        break;
                    case TicketAlineacionLinea.Centro:
                        x = (int)(gfx.ClipBounds.Width - gfx.MeasureString(linea.Texto, fuente).Width) / 2;

                        break;*/
                    case PrinterAlineacionLinea.Derecha:
                        string espacios = "";
                        int largo = linea.Texto.Length;

                        if (largo != this.propLargoMaximoCaracteres)
                            largo = this.propLargoMaximoCaracteres - largo;

                        for (int i = 0; i < largo; i++)
                            espacios += " ";

                        linea.Texto = espacios + linea.Texto;

                        break;
                }

                // Dibujo la linea
                gfx.DrawString(linea.Texto, fuente, new SolidBrush(Color.Black), x, PosicionY(), new StringFormat());

                contador++;
            }
        }
        #endregion

        #region Eventos
        private void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            gfx = e.Graphics;

            DibujarImagen();
            DibujarLineas();

            if (this.propImagenCabecera != null)
                this.propImagenCabecera.Dispose();

            //throw new NotImplementedException();
        }
        #endregion
    }
}
