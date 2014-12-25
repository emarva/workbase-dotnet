using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace WorkBase.DataLayer
{
    public interface IConnector
    {
        #region Propiedades
        bool SeguridadIntegrada { get; set; }
        string Host { get; set; }
        int Puerto { get; set; }
        string BaseDatos { get; set; }
        string Usuario { get; set; }
        string Clave { get; set; }
        bool Conectado { get; }
        bool TieneFilas { get; }
        int FilasAfectadas { get; }
        ParametroCollection Parametros { get; }
        #endregion

        #region Metodos
        bool Conectar();
        bool Desconectar();
        DbDataReader Consulta(string consulta, ArrayList parametros);
        DbDataReader Consulta(string consulta);
        bool Ejecutar(string consulta);
        bool Ejecutar(string consulta, ArrayList parametros);
        DbDataReader Ejecutar(string consulta, ParametroCollection parametros);
        DbDataReader Ejecutar(string consulta, object valor, ParametroCollection parametros);
        bool IniciarTransaccion();
        bool DestinarTransaccion();
        bool RestaurarTransaccion();
        #endregion
    }
}
