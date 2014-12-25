using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DevO2.UI
{
    public interface IModule
    {
        #region Propiedades
        string Nombre { get; }
        string Autor { get; }
        string Version { get; }
        bool FormModal { get; }
        bool CrearMenuBase { get; }
        bool CrearItemMenu { get; }
        bool CrearSeparadorItemMenu { get; }
        bool CrearBoton { get; }
        bool UsaTabFrame { get; }
        string TextoMenuBase { get; }
        string TextoItemMenu { get; }
        string TextoBoton { get; }
        Icon IconoBoton { get; }
        TabControlEx TabFrame { set; }
        #endregion

        #region Metodos
        void Ejecutar();
        void Menu_Click(object sender, EventArgs e);
        void Boton_Click(object sender, EventArgs e);
        #endregion
    }
}
