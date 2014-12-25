using System;
using System.Collections.Generic;
using System.Text;

namespace DevO2.Shared
{
    public sealed class Conversion
    {
        #region Metodos
        public static string Byte2Hex(byte[] bytes)
        {
            StringBuilder sbHex = new StringBuilder(bytes.Length);

            for (int i = 0; i < bytes.Length; i++)
            {
                sbHex.Append(bytes[i].ToString("x2"));
            }

            return sbHex.ToString();
        }

        public static byte[] Hex2Byte(string hex)
        {
            // Remueve cualquier espacio de la cadena hexadecimal
            hex.Replace(" ", "");

            byte[] hexBuffer = new byte[hex.Length / 2];

            for (int i = 0; i < hex.Length; i += 2)
            {
                hexBuffer[i / 2] = (byte)Convert.ToByte(hex.Substring(i, 2), 16);
            }

            return hexBuffer;
        }
        #endregion
    }
}
