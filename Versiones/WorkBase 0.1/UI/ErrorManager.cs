using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace WorkBase.UI
{
    public class ErrorManager : Component
    {
        #region Variables
        private string propAplicacion;
        private string propVersion;
        private string propEnviarErrorA;
        private bool propVerDetalle;
        #endregion

        #region Constructor
        public ErrorManager() { }
        //public ErrorManager(Exception ex) : this(ex, null, null, null) { }

        //public ErrorManager(Exception ex, string titulo) : this(ex, titulo, null, null) { }

        /*public ErrorManager(Exception ex, string titulo, string destinatarioError, string aplicacion)
        {
            ErrorManagerForm error = new ErrorManagerForm();
            
            try
            {
                string tituloError;

                if (titulo != null && titulo.Length != 0)
                    tituloError = titulo;
                else
                    tituloError = "Error";

                // Verifico si se existe la posibilidad de enviar correo
                if (destinatarioError != null && aplicacion != null)
                {
                    error.DestinatarioError = destinatarioError;
                    error.Aplicacion = aplicacion;
                }

                error.Text = tituloError;
                error.Controls["txtMensaje"].Text = ex.Message;
                error.Controls["lblOrigen"].Text = ex.Source;

                error.ShowDialog();
            }
            catch { }
        }*/
        #endregion

        #region Propiedades
        public string Aplicacion
        {
            get { return this.propAplicacion; }
            set { this.propAplicacion = value; }
        }

        public string Version
        {
            get { return this.propVersion; }
            set { this.propVersion = value; }
        }

        public string EnviarErrorA
        {
            get { return this.propEnviarErrorA; }
            set { this.propEnviarErrorA = value; }
        }

        public bool VerDetalle
        {
            get { return this.propVerDetalle; }
            set { this.propVerDetalle = value; }
        }
        #endregion

        #region Metodos
        public void Show(string titulo, Exception ex)
        {
            ErrorManagerForm error = new ErrorManagerForm();

            try
            {
                string tituloError;

                if (titulo != null && titulo.Length != 0)
                    tituloError = titulo;
                else
                    tituloError = "Error";

                // Verifico si se existe la posibilidad de enviar correo
                if (this.propEnviarErrorA != null && this.propAplicacion != null)
                {
                    error.DestinatarioError = this.propEnviarErrorA;
                    error.Aplicacion = this.propAplicacion;
                }

                error.Text = tituloError;
                error.Controls["txtMensaje"].Text = ex.Message;
                error.Controls["lblOrigen"].Text = ex.Source;

                // Cargo el detalle del error
                StringBuilder sbDetalle = new StringBuilder();

                sbDetalle.AppendLine("Aplicación: " + this.propAplicacion);
                sbDetalle.AppendLine("InnerException: " + ex.InnerException);
                sbDetalle.AppendLine("StackTrace: " + ex.StackTrace);
                sbDetalle.AppendLine("TargetSite: " + ex.TargetSite);

                error.Controls["txtDetalle"].Text = sbDetalle.ToString();

                if (this.propVerDetalle)
                    error.Controls["btnVerDetalle"].Visible = true;

                error.ShowDialog();
            }
            catch (Exception ex2) { MessageBox.Show(ex2.Message); }
        }

        public void Show(Exception ex)
        {
            this.Show(null, ex);
        }
        #endregion
    }
}
