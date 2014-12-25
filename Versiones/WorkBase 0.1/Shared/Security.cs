using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace WorkBase.Shared
{
    public sealed class Security
    {
        #region Metodos
        public static string Base64Codificar(string cadena)
        {
            try
            {
                byte[] datos = null;
                datos = Encoding.UTF8.GetBytes(cadena);
                return Convert.ToBase64String(datos);
            }
            catch (Exception ex)
            {
                throw ex;
                //ErrorManager em = new ErrorManager(ex);
                //return null;
            }
        }

        public static string Base64Decodificar(string cadena)
        {
            try
            {
                byte[] datos = null;
                datos = Convert.FromBase64String(cadena);
                return Encoding.UTF8.GetString(datos);
            }
            catch (Exception ex)
            {
                throw ex;
                //ErrorManager em = new ErrorManager(ex);
                //return null;
            }
        }

        public static string GenerarMD5(string cadena)
        {            
            MD5 md5 = MD5.Create();
            byte[] datos = null;

            datos = md5.ComputeHash(Encoding.UTF8.GetBytes(cadena));

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < datos.Length; i++)
            {
                sb.Append(datos[i].ToString("x2"));
            }

            return sb.ToString();
        }

        public static string GenerarSHA(string cadena, TipoSHA tipo)
        {
            byte[] datos = null;

            switch (tipo)
            {
                case TipoSHA.SHA1:
                    SHA1 sha = SHA1.Create();
                    datos = sha.ComputeHash(Encoding.UTF8.GetBytes(cadena));
                    break;
                case TipoSHA.SHA256:
                    SHA256 sha2 = SHA256.Create();
                    datos = sha2.ComputeHash(Encoding.UTF8.GetBytes(cadena));
                    break;
                case TipoSHA.SHA384:
                    SHA384 sha3 = SHA384.Create();
                    datos = sha3.ComputeHash(Encoding.UTF8.GetBytes(cadena));
                    break;
                case TipoSHA.SHA512:
                    SHA512 sha5 = SHA512.Create();
                    datos = sha5.ComputeHash(Encoding.UTF8.GetBytes(cadena));
                    break;
            }

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < datos.Length; i++)
            {
                sb.Append(datos[i].ToString("x2"));
            }

            return sb.ToString();
        }

        private static byte[] ConstruirClaveEnBytes(string clave, MetodoEncriptacion metodo)
        {
            switch (metodo)
            {
                case MetodoEncriptacion.DES:
                case MetodoEncriptacion.RC2:
                    if (clave.Length < 8)
                        clave = clave.PadRight(8);
                    else
                        clave = clave.Substring(0, 8);
                    break;
                case MetodoEncriptacion.Rijndael:
                case MetodoEncriptacion.TripleDES:
                    if (clave.Length < 16)
                        clave = clave.PadRight(16);
                    else
                        clave = clave.Substring(0, 16);
                    break;
            }

            return Encoding.UTF8.GetBytes(clave);
        }

        private static byte[] ConstruirIVEnBytes(string iv, MetodoEncriptacion metodo)
        {
            switch (metodo)
            {
                case MetodoEncriptacion.DES:
                case MetodoEncriptacion.RC2:
                    if (iv.Length < 8)
                        iv = iv.PadRight(8);
                    else
                        iv = iv.Substring(0, 8);
                    break;
                case MetodoEncriptacion.Rijndael:
                case MetodoEncriptacion.TripleDES:
                    if (iv.Length < 16)
                        iv = iv.PadRight(16);
                    else
                        iv = iv.Substring(0, 16);
                    break;
            }

            return Encoding.UTF8.GetBytes(iv);
        }
        
        public static string Encriptar(string cadena, string clave, TipoHash hash, int tamanoClave, int iteraciones, MetodoEncriptacion metodo, FormatoCadenaEncriptada formatoRetorno)
        {
            ICryptoTransform transform = null;
            string tipoHash = string.Empty;
            
            try
            {
                if (clave != null)
                {
                    switch (hash)
                    {
                        case TipoHash.MD5: tipoHash = "MD5"; break;
                        case TipoHash.SHA1: tipoHash = "SHA1"; break;
                        case TipoHash.SHA256: tipoHash = "SHA256"; break;
                        case TipoHash.SHA384: tipoHash = "SHA384"; break;
                        case TipoHash.SHA512: tipoHash = "SHA512"; break;
                    }

                    // Convierte en bytes la clave para usarla como IV
                    byte[] iv = ConstruirIVEnBytes(clave, metodo);

                    // Convierte en bytes la cadena
                    byte[] cadenaBytes = Encoding.UTF8.GetBytes(cadena);

                    // Crea la clave derivada
                    PasswordDeriveBytes claveDerivada = new PasswordDeriveBytes(clave, iv, tipoHash, iteraciones);

                    // Obtiene la nueva clave en bytes segun el metodo
                    byte[] claveBytes = null;

                    if (metodo== MetodoEncriptacion.DES || metodo== MetodoEncriptacion.RC2)
                        claveBytes = claveDerivada.GetBytes(tamanoClave / 32);
                    else
                        claveBytes = claveDerivada.GetBytes(tamanoClave / 16);

                    // Genera el encriptador según el metodo
                    switch (metodo)
                    {
                        case MetodoEncriptacion.DES:
                            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                            des.Mode = CipherMode.CBC;
                            transform = des.CreateEncryptor(claveBytes, iv);
                            break;
                        case MetodoEncriptacion.RC2:
                            RC2CryptoServiceProvider rc2 = new RC2CryptoServiceProvider();
                            rc2.Mode = CipherMode.CBC;
                            transform = rc2.CreateEncryptor(claveBytes, iv);
                            break;
                        case MetodoEncriptacion.Rijndael:
                            RijndaelManaged rd = new RijndaelManaged();
                            rd.Mode = CipherMode.CBC;
                            transform = rd.CreateEncryptor(claveBytes, iv);
                            break;
                        case MetodoEncriptacion.TripleDES:
                            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                            tdes.Mode = CipherMode.CBC;
                            transform = tdes.CreateEncryptor(claveBytes, iv);
                            break;
                    }

                    // Instancia el stream de memoria que mantendra los datos encriptados
                    MemoryStream ms = new MemoryStream();

                    // Instancia el stream de encriptacion
                    CryptoStream cs = new CryptoStream(ms, transform, CryptoStreamMode.Write);

                    // Inicia la encriptacion
                    cs.Write(cadenaBytes, 0, cadenaBytes.Length);

                    // Termina la encriptacion
                    cs.FlushFinalBlock();

                    byte[] cadenaEncriptada = ms.ToArray();

                    // Cierre de ambos streams
                    ms.Close();
                    cs.Close();
                   
                    // Devuelve la cadena encriptada según el formato de retorno
                    switch (formatoRetorno)
                    {
                        case FormatoCadenaEncriptada.Base64:
                            return Convert.ToBase64String(cadenaEncriptada);
                        case FormatoCadenaEncriptada.Hexadecimal:
                            return Conversion.Byte2Hex(cadenaEncriptada);
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
                //return null;
            }
        }

        public static string Encriptar(string cadena, string clave, TipoHash hash, int tamanoClave, MetodoEncriptacion metodo, FormatoCadenaEncriptada formatoRetorno)
        {
            return Encriptar(cadena, clave, hash, tamanoClave, 2, metodo, formatoRetorno);
        }

        public static string Encriptar(string cadena, string clave, TipoHash hash, MetodoEncriptacion metodo, FormatoCadenaEncriptada formatoRetorno)
        {
            return Encriptar(cadena, clave, hash, 256, 2, metodo, formatoRetorno);
        }

        public static string Encriptar(string cadena, string clave, MetodoEncriptacion metodo, FormatoCadenaEncriptada formatoRetorno)
        {
            return Encriptar(cadena, clave, TipoHash.SHA1, 256, 2, metodo, formatoRetorno);
        }

        public static string Desencriptar(string cadena, string clave, TipoHash hash, int tamanoClave, int iteraciones, MetodoEncriptacion metodo, FormatoCadenaEncriptada formatoEntrada)
        {
            ICryptoTransform transform = null;
            string tipoHash = string.Empty;

            try
            {
                if (clave != null)
                {
                    switch (hash)
                    {
                        case TipoHash.MD5: tipoHash = "MD5"; break;
                        case TipoHash.SHA1: tipoHash = "SHA1"; break;
                        case TipoHash.SHA256: tipoHash = "SHA256"; break;
                        case TipoHash.SHA384: tipoHash = "SHA384"; break;
                        case TipoHash.SHA512: tipoHash = "SHA512"; break;
                    }

                    // Convierte en bytes la clave para usarla como IV
                    byte[] iv = ConstruirIVEnBytes(clave, metodo);

                    // Convierte la cadena 
                    byte[] cadenaBytes = null;

                    if (formatoEntrada == FormatoCadenaEncriptada.Base64)
                        cadenaBytes = Convert.FromBase64String(cadena);
                    else
                        cadenaBytes = Conversion.Hex2Byte(cadena);

                    // Crea la clave derivada
                    PasswordDeriveBytes claveDerivada = new PasswordDeriveBytes(clave, iv, tipoHash, iteraciones);

                    // Obtiene la nueva clave en bytes segun el metodo
                    byte[] claveBytes = null;

                    if (metodo == MetodoEncriptacion.DES || metodo == MetodoEncriptacion.RC2)
                        claveBytes = claveDerivada.GetBytes(tamanoClave / 32);
                    else
                        claveBytes = claveDerivada.GetBytes(tamanoClave / 16);

                    // Genera el encriptador según el metodo
                    switch (metodo)
                    {
                        case MetodoEncriptacion.DES:
                            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                            des.Mode = CipherMode.CBC;
                            transform = des.CreateDecryptor(claveBytes, iv);
                            break;
                        case MetodoEncriptacion.RC2:
                            RC2CryptoServiceProvider rc2 = new RC2CryptoServiceProvider();
                            rc2.Mode = CipherMode.CBC;
                            transform = rc2.CreateDecryptor(claveBytes, iv);
                            break;
                        case MetodoEncriptacion.Rijndael:
                            RijndaelManaged rd = new RijndaelManaged();
                            rd.Mode = CipherMode.CBC;
                            transform = rd.CreateDecryptor(claveBytes, iv);
                            break;
                        case MetodoEncriptacion.TripleDES:
                            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                            tdes.Mode = CipherMode.CBC;
                            transform = tdes.CreateDecryptor(claveBytes, iv);
                            break;
                    }

                    // Instancia el stream de memoria que mantendra los datos encriptados
                    MemoryStream ms = new MemoryStream(cadenaBytes);

                    // Instancia el stream de encriptacion
                    CryptoStream cs = new CryptoStream(ms, transform, CryptoStreamMode.Read);

                    // Genera el buffer que almacenara la cadena desencriptada
                    byte[] bytesCadena = new byte[cadenaBytes.Length];

                    // Inicia la desencriptacion
                    int contBytes = cs.Read(bytesCadena, 0, bytesCadena.Length);

                    // Cierre de ambos streams
                    ms.Close();
                    cs.Close();

                    // Devuelve la cadena desencriptada
                    return Encoding.UTF8.GetString(bytesCadena);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
                //return null;
            }
        }

        public static string Desencriptar(string cadena, string clave, TipoHash hash, int tamanoClave, MetodoEncriptacion metodo, FormatoCadenaEncriptada formatoEntrada)
        {
            return Desencriptar(cadena, clave, hash, tamanoClave, 2, metodo, formatoEntrada);
        }

        public static string Desencriptar(string cadena, string clave, TipoHash hash, MetodoEncriptacion metodo, FormatoCadenaEncriptada formatoEntrada)
        {
            return Desencriptar(cadena, clave, hash, 256, 2, metodo, formatoEntrada);
        }

        public static string Desencriptar(string cadena, string clave, MetodoEncriptacion metodo, FormatoCadenaEncriptada formatoEntrada)
        {
            return Desencriptar(cadena, clave, TipoHash.SHA1, 256, 2, metodo, formatoEntrada);
        }
        #endregion
    }
}
