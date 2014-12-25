using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using DevO2.Shared;

namespace DevO2.liveConnector
{
    public class liveUsuario
    {
        #region Variables
        private string propUsuario;
        private string propTipo;
        private string propDominio;
        private string propNombre;
        private string propDepartamento;
        private string propCorreo;
        #endregion

        #region Propiedades
        public string Usuario
        {
            get { return this.propUsuario; }
            set { this.propUsuario = value; }
        }

        public string Tipo
        {
            get { return this.propTipo; }
            set { this.propTipo = value; }
        }

        public string Dominio
        {
            get { return this.propDominio; }
            set { this.propDominio = value; }
        }

        public string Nombre
        {
            get { return this.propNombre; }
            set { this.propNombre = value; }
        }

        public string Departamento
        {
            get { return this.propDepartamento; }
            set { this.propDepartamento = value; }
        }

        public string Correo
        {
            get { return this.propCorreo; }
            set { this.propCorreo = value; }
        }
        #endregion
    }

    public class live
    {
        #region Variables
        private string propURLServicio;
        private string propError;
        private string propMensajeError;
        #endregion

        #region Propiedades
        public string URLServicio
        {
            get { return this.propURLServicio; }
            set { this.propURLServicio = value; }
        }

        public string Error
        {
            get { return this.propError; }
        }

        public string MensajeError
        {
            get { return this.propMensajeError; }
        }
        #endregion

        #region Metodos
        private string ConsultarLive(string consulta)
        {
            try
            {
                if (this.propURLServicio.LastIndexOf("/") != this.propURLServicio.Length - 1)
                    this.propURLServicio += "/";

                if (consulta.LastIndexOf("/") == -1)
                    consulta += "/";

                // Crea la solicitud de la URL.
                WebRequest request = WebRequest.Create(this.propURLServicio + consulta);

                // Obtener la respuesta.
                WebResponse response = request.GetResponse();

                // Abrir el stream de la respuesta recibida.
                StreamReader reader = new StreamReader(response.GetResponseStream());

                // Leer el contenido.
                string res = reader.ReadToEnd();

                // Cerrar los streams abiertos.
                reader.Close();
                response.Close();

                return Security.Base64Decodificar(res);
            }
            catch { return "-1"; }
        }

        public bool EstadoServicio()
        {
            if (this.propURLServicio == null)
            {
                this.propError = "No se ha especificado la URL del servicio.";
                return false;
            }

            int res = Convert.ToInt32(ConsultarLive("service_state"));

            switch (res)
            {
                case -1: this.propError = "No se puede acceder al servicio"; return false;
                case 0: return true;
                case 1: this.propError = "El servicio está inactivo. Inténtelo más tarde."; return false;
                case 2: this.propError = "El servicio se encuentra en mantención. Inténtelo más tarde."; return false;
                default: this.propError = "El servicio no está disponible. Inténtelo más tarde."; return false;
            }
        }

        public bool IniciarSesion(string usuario, string contrasena, int idApp, ref string[] retorno)
        {
            try
            {
                if (this.propURLServicio == null)
                {
                    this.propError = "No se ha especificado la URL del servicio.";
                    return false;
                }

                if (!EstadoServicio())
                    return false;

                // Obtengo el Host y la IP del cliente
                string hostCodificado = Security.Base64Codificar(Dns.GetHostName());

                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress[] ip = host.AddressList;
                string ipCodificada = Security.Base64Codificar(ip[0].ToString());

                string res = this.ConsultarLive("login/usuario=" + Security.Base64Codificar(usuario) +
                                                "/contrasena=" + Security.Base64Codificar(contrasena) +
                                                "/id_app=" + idApp + "/host=" + hostCodificado + "/ip=" + ipCodificada);
                string[] ret = res.Split('_');

                if (res.Contains("lv_login_ok"))
                {
                    retorno = new string[4];

                    retorno[0] = ret[3]; // Id seguro
                    retorno[1] = ret[4]; // Tipo
                    retorno[2] = ret[5]; // Nombre
                    retorno[3] = ret[6]; // Dominio

                    /*if (ret[4] == "root" || ret[4] == "admin")
                        retorno[3] = ret[6];
                    else
                        retorno[3] = "";*/

                    return true;
                }
                else if (res.Contains("lv_error"))
                {
                    this.propError = ret[2];

                    switch (ret[2])
                    {
                        case "li01": this.propMensajeError = "El dominio no existe."; break;
                        case "li02": this.propMensajeError = "El usuario esta inactivo."; break;
                        case "li03": this.propMensajeError = "El usuario no tiene acceso a esta aplicación."; break;
                        case "li04": this.propMensajeError = "El usuario ya inicio sesión en esta aplicación."; break;
                        case "li05": this.propMensajeError = "El usuario no existe."; break;
                        case "li06": this.propMensajeError = "Error en los parámetros entregados."; break;
                    }                    
                }

                return false;
            }
            catch { return false; }
        }

        public bool CerrarSesion(int idApp, string sidUsuario)
        {
            try
            {
                if (this.propURLServicio == null)
                {
                    this.propError = "No se ha especificado la URL del servicio.";
                    return false;
                }

                if (!EstadoServicio())
                    return false;

                string res = this.ConsultarLive("logout/id_app=" + idApp + "/sid_usuario=" + sidUsuario);
                string[] ret = res.Split('_');

                if (res.Contains("lv_logout_ok"))
                {
                    return true;
                }
                else if (res.Contains("lv_error"))
                {
                    this.propError = ret[2];

                    switch (ret[2])
                    {
                        case "lo01": this.propMensajeError = "El usuario no ha iniciado sesión en esta aplicación."; break;
                        case "lo02": this.propMensajeError = "Error en los parámetros entregados."; break;
                    }
                }

                return false;
            }
            catch { return false; }
        }

        /*public bool CambiarClave(string usuario, string contrasenaAnterior, string contrasenaNueva)
        {
            return false;
        }*/

        public bool ObtenerListaUsuarios(int idApp, string sid, string dominio, ref liveUsuario[] retorno)
        {
            if (this.propURLServicio == null)
            {
                this.propError = "No se ha especificado la URL del servicio.";
                return false;
            }

            if (!EstadoServicio())
                return false;

            string res = this.ConsultarLive("get_users_id/id_app=" + Security.Base64Codificar(idApp.ToString()) + "/sid=" + Security.Base64Codificar(sid) + "/dominio=" + Security.Base64Codificar(dominio));
            string[] ret = res.Split('_');

            if (res.Contains("lv_ok"))
            {
                liveUsuario[] lista = new liveUsuario[ret.Length - 2];
                
                // CORREGIR OVERFLOW
                //for (int i = 2; i <= ret.Length - 1; i++)
                for (int i = 2; i < ret.Length; i++)
                {
                    if (dominio != "root-domain")
                    {
                        if (!ObtenerUsuario(idApp, sid, int.Parse(ret[i]), ref lista[i - 2]))
                            return false;
                    }
                    else
                    {
                        if (!ObtenerUsuarioComoRoot(dominio, int.Parse(ret[i]), ref lista[i - 2]))
                            return false;
                    }
                }

                retorno = lista;
                return true;
            }
            else if (res.Contains("lv_error"))
            {
                this.propError = ret[2];

                switch (ret[2])
                {
                    case "gui01": this.propMensajeError = "El dominio no existe."; break;
                    case "gui02": this.propMensajeError = "No hay usuarios asignados a esta aplicación."; break;
                    case "gui03": this.propMensajeError = "El usuario esta inactivo."; break;
                    case "gui04": this.propMensajeError = "El usuario no tiene acceso a esta aplicación."; break;
                    case "gui05": this.propMensajeError = "Error en los parámetros entregados."; break;
                }
            }

            return false;
        }

        public bool ObtenerUsuario(int idApp, string sid, int idUsuario, ref liveUsuario usuario)
        {
            if (this.propURLServicio == null)
            {
                this.propError = "No se ha especificado la URL del servicio.";
                return false;
            }

            if (!EstadoServicio())
                return false;

            string res = this.ConsultarLive("get_user/id_app=" + Security.Base64Codificar(idApp.ToString()) + "/sid=" + Security.Base64Codificar(sid) + "/id_usuario=" + Security.Base64Codificar(idUsuario.ToString()));
            string[] ret = res.Split('_');

            if (res.Contains("lv_ok"))
            {
                usuario = new liveUsuario();

                usuario.Usuario = ret[2];
                usuario.Tipo = ret[3];
                usuario.Dominio = ret[4];
                usuario.Nombre = ret[5];
                usuario.Departamento = ret[6];
                usuario.Correo = ret[7];

                return true;
            }
            else if (res.Contains("lv_error"))
            {
                this.propError = ret[2];

                switch (ret[2])
                {
                    case "gu01": this.propMensajeError = "El usuario no existe."; break;
                    case "gu02": this.propMensajeError = "El usuario no ha iniciado sesión en esta aplicación."; break;
                    case "gu03": this.propMensajeError = "Error en los parámetros entregados."; break;
                }
            }

            return false;
        }

        private bool ObtenerUsuarioComoRoot(string dominio, int idUsuario, ref liveUsuario usuario)
        {
            if (this.propURLServicio == null)
            {
                this.propError = "No se ha especificado la URL del servicio.";
                return false;
            }

            if (!EstadoServicio())
                return false;

            string res = this.ConsultarLive("get_user_like_root/dominio=" + Security.Base64Codificar(dominio) + "/id_usuario=" + Security.Base64Codificar(idUsuario.ToString()));
            string[] ret = res.Split('_');

            if (res.Contains("lv_ok"))
            {
                usuario = new liveUsuario();

                usuario.Usuario = ret[2];
                usuario.Tipo = ret[3];
                usuario.Dominio = ret[4];
                usuario.Nombre = ret[5];
                usuario.Departamento = ret[6];
                usuario.Correo = ret[7];

                return true;
            }
            else if (res.Contains("lv_error"))
            {
                this.propError = ret[2];

                switch (ret[2])
                {
                    case "gulr01": this.propMensajeError = "El dominio root es inválido."; break;
                    case "gulr02": this.propMensajeError = "El usuario no ha iniciado sesión en esta aplicación."; break;
                    case "gulr03": this.propMensajeError = "Error en los parámetros entregados."; break;
                }
            }

            return false;
        }
        #endregion
    }
}
