using System;
using System.Collections.Generic;
using System.Text;

namespace WorkBase.Shared
{
    public class Object2 : object
    {
        private object objeto = null;

        #region Constructor
        public Object2 (object objeto)
        {
            this.objeto = objeto;
        }
        #endregion

        #region Metodos
        public bool IsNull()
        {
            if (string.IsNullOrEmpty(this.objeto.ToString()))
                return true;

            return false;
        }

        public Boolean ToBoolean()
        {
            return Boolean.Parse(objeto.ToString());
        }

        public Byte ToByte()
        {
            return Byte.Parse(objeto.ToString());
        }

        public Char ToChar()
        {
            return Char.Parse(objeto.ToString());
        }

        public DateTime ToDateTime()
        {
            return DateTime.Parse(objeto.ToString());            
        }

        public Decimal ToDecimal()
        {
            return Decimal.Parse(objeto.ToString());
        }

        public Double ToDouble()
        {
            return Double.Parse(objeto.ToString());
        }

        public Object ToObject()
        {
            return objeto;
        }

        public SByte ToSByte()
        {
            return SByte.Parse(objeto.ToString());
        }

        public Single ToSingle()
        {
            return Single.Parse(objeto.ToString());
        }

        public override string ToString()
        {
            return objeto.ToString();
        }

        public Int16 ToInt16()
        {
            return Int16.Parse(objeto.ToString());
        }

        public Int32 ToInt32()
        {
            return Int32.Parse(objeto.ToString());
        }

        public Int64 ToInt64()
        {
            return Int64.Parse(objeto.ToString());
        }

        public UInt16 ToUInt16()
        {
            return UInt16.Parse(objeto.ToString());
        }

        public UInt32 ToUInt32()
        {
            return UInt32.Parse(objeto.ToString());
        }

        public UInt64 ToUInt64()
        {
            return UInt64.Parse(objeto.ToString());
        }
        #endregion
    }
}
