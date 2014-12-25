using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace DevO2.UI
{
    internal partial class ErrorManagerForm : Form
    {
        private string propDestinatarioError;
        private string propAplicacion;

        public string DestinatarioError
        {
            get { return this.propDestinatarioError; }
            set { this.propDestinatarioError = value; }
        }

        public string Aplicacion
        {
            get { return this.propAplicacion; }
            set { this.propAplicacion = value; }
        }

        public ErrorManagerForm()
        {
            InitializeComponent();
        }

        private void btnNoEnviar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fErrorManager_Load(object sender, EventArgs e)
        {
            if (propDestinatarioError != null && propAplicacion != null)
            {
                btnContinuar.Visible = false;
                btnEnviarCorreo.Visible = true;
                btnNoEnviar.Visible = true;
            }
        }

        private void btnEnviarCorreo_Click(object sender, EventArgs e)
        {
            try
            {
                MailMessage mail = new MailMessage(this.propDestinatarioError, this.propDestinatarioError);
                
                // Cuerpo del mansaje
                StringBuilder sbMensaje = new StringBuilder();
                sbMensaje.AppendLine("<strong>Error:</strong> " + txtMensaje.Text);
                sbMensaje.AppendLine("<strong>Origen:</strong> " + lblOrigen.Text);
                sbMensaje.AppendLine("<strong>Detalle:</strong><br /><br /> " + txtDetalle.Text);
                                
                mail.Subject = "Error en " + this.propAplicacion;
                mail.IsBodyHtml = true;
                mail.Body = sbMensaje.ToString().Replace("\n", "<br />");                
                mail.Priority = MailPriority.Normal;

                // Se configura el servidor SMTP
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;

                NetworkCredential credencial = new NetworkCredential("lordbytes", "god1982");
                smtp.Credentials = credencial;

                // Se envia el correo
                smtp.Send(mail);
                this.Close();
            }
            catch { }
        }

        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            if (btnVerDetalle.Text == "Ver detalle")
            {
                btnVerDetalle.Text = "Ocultar detalle";
                this.Height = 278;
            }
            else
            {
                btnVerDetalle.Text = "Ver detalle";
                this.Height = 173;
            }
        }
    }
}
