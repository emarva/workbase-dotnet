using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using DevO2.UI;

namespace Modulo1.Modulos
{
    public class Modulo : IModule
    {
        private string propNombre = "Modulo de prueba";
        private string propAutor = "Lord Bytes";
        private string propVersion = "0.1";
        private bool propFormModal = false;
        private bool propCrearMenuBase = false;
        private bool propCrearItemMenu = true;
        private bool propCrearSeparadorItemMenu = false;
        private bool propCrearBoton = true;
        private bool propUsaTabFrame = true;
        private string propTextoMenuBase = "";
        private string propTextoItemMenu = "Modulo";
        private string propTextoBoton = "";
        private Icon propIconoBoton = null;
        private TabControlEx propTabFrame = null;

        public string Nombre
        {
            get { return propNombre; }
        }

        public string Autor
        {
            get { return propAutor; }
        }

        public string Version
        {
            get { return propVersion; }
        }
        
        public bool FormModal
        {
            get { return propFormModal; }
        }

        public bool CrearMenuBase
        {
            get { return propCrearMenuBase; }
        }

        public bool CrearItemMenu
        {
            get { return propCrearItemMenu; }
        }

        public bool CrearSeparadorItemMenu
        {
            get { return propCrearSeparadorItemMenu; }
        }

        public bool CrearBoton
        {
            get { return propCrearBoton; }
        }

        public bool UsaTabFrame
        {
            get { return propUsaTabFrame; }
        }

        public string TextoMenuBase
        {
            get { return propTextoMenuBase; }
        }

        public string TextoItemMenu
        {
            get { return propTextoItemMenu; }
        }

        public string TextoBoton
        {
            get { return propTextoBoton; }
        }

        public Icon IconoBoton
        {
            get { return propIconoBoton; }
        }

        public TabControlEx TabFrame
        {
            set { propTabFrame = value; }
        }

        public void Ejecutar() { }

        public void Menu_Click(object sender, EventArgs e)
        {
            Formulario form = new Formulario();

            if (propUsaTabFrame && propTabFrame != null)
            {
                propTabFrame.AgregarFormulario(form);                
            }
            else
            {
                if (propFormModal)
                {
                    form.ShowDialog();
                }
                else
                {
                    form.Show();
                }
            }
        }

        public void Boton_Click(object sender, EventArgs e)
        {
            Menu_Click(sender, e);
        }
    }
}
