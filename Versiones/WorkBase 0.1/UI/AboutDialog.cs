using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using WorkBase.Shared;

namespace WorkBase.UI
{
    public partial class AboutDialog : Component
    {
        #region Variables
        private string propAplicacion;
        private System.Drawing.Image propImagen;
        private string propVersion;
        private string propAno;
        private string propAutor;
        private string propUsuario;
        #endregion

        #region Constructor
        public AboutDialog()
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

        public string Aplicacion
        {
            get { return this.propAplicacion; }
            set { this.propAplicacion = value; }
        }

        public System.Drawing.Image Imagen
        {
            get { return propImagen; }
            set { propImagen = value; }
        }

        public string Version
        {
            get { return this.propVersion; }
            set { this.propVersion = value; }
        }

        public string Ano
        {
            get { return this.propAno; }
            set { this.propAno = value; }
        }

        public string Autor
        {
            get { return this.propAutor; }
            set { this.propAutor = value; }
        }

        public string Usuario
        {
            get { return this.propUsuario; }
            set { this.propUsuario = value; }
        }
        #endregion

        #region Metodos
        public void Show()
        {
            AboutDialogForm acerca = new AboutDialogForm();

            // Aplica las propiedades al formulario
            acerca.Text = acerca.Text.Replace("#app", this.propAplicacion);
            ((System.Windows.Forms.PictureBox)acerca.Controls["picIcono"]).Image = propImagen;
            acerca.Controls["lblApp"].Text = acerca.Controls["lblApp"].Text.Replace("#app", this.propAplicacion);
            acerca.Controls["lblVersion"].Text = acerca.Controls["lblVersion"].Text.Replace("#version", this.propVersion);
            acerca.Controls["lblCopyright"].Text = acerca.Controls["lblCopyright"].Text.Replace("#ano", this.propAno);
            acerca.Controls["lblCopyright"].Text = acerca.Controls["lblCopyright"].Text.Replace("#autor", this.propAutor);
            acerca.Controls["lblAutorizacion"].Text = acerca.Controls["lblAutorizacion"].Text.Replace("#usuario", this.propUsuario);

            acerca.ShowDialog();
        }
        #endregion
    }
}
