using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.Common;

namespace DevO2.DataLayer
{
    public interface IConnector
    {
        #region Propiedades        
        bool Conectado { get; }
        bool TieneFilas { get; }
        int FilasAfectadas { get; }
        DlParameterCollection Parametros { get; set; }
        #endregion

        #region Metodos
        bool Conectar();
        bool Desconectar();
        void CrearBaseDatos(string nombre, Hashtable parametros);
        DbDataReader ObtenerDataReader(string consulta);
        DbDataAdapter ObtenerDataAdapter(string consulta, ref int indiceCmd);
        void ActualizarObjetoDatos(int indiceCmd, object objetoDatos);
        bool Ejecutar(string consulta);
        bool IniciarTransaccion();
        bool DestinarTransaccion();
        bool RestaurarTransaccion();
        void Dispose();
        #endregion
    }
}
