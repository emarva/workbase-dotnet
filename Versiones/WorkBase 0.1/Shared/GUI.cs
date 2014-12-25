using System;
using System.Collections.Generic;
using System.Text;

namespace WorkBase.Shared
{
    public class GUI
    {
        #region Metodos
        public static bool CambiarResolucionPantalla()
        {
            return false;
        }

        public static void DibujarImagen(System.Windows.Forms.Control control, int x, int y, int ancho, int alto, string imagen)
        {
            
        }

        public static void DibujarImagen(System.Windows.Forms.Control control, int x, int y, string imagen)
        {
            DibujarImagen(control, x, y, 0, 0, imagen);
        }
        #endregion
    }
}
