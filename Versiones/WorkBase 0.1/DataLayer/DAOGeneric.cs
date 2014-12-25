using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using WorkBase.Shared;

namespace WorkBase.DataLayer
{
    public sealed class DAOGeneric : IDisposable
    {
        #region Variables
        private string propBaseDatos = null;
        private TipoConnector _connector;

        private Database db;
        private string baseDatosOriginal;
        private bool disposed;
        #endregion

        #region Constructor
        public DAOGeneric(TipoConnector conector, CadenaConexion cadenaConexion)
        {
            this._connector = conector;
            this.baseDatosOriginal = cadenaConexion.BaseDatos;
            this.db = new Database(conector, cadenaConexion);           
        }

        public DAOGeneric(TipoConnector conector, string cadena)
        {
            if (!string.IsNullOrEmpty(cadena))
            {
                db = new Database(conector, cadena);
            }
        }
        #endregion

        #region Propiedades
        public string BaseDatos
        {
            get { return this.propBaseDatos; }
            set { this.propBaseDatos = value; }
        }

        public TipoConnector Conector
        {
            get { return _connector; }
            set { _connector = value; }
        }
        #endregion

        #region Metodos
        private Dictionary<string, object> ObtenerElementos(object objeto, MemberTypes tipoElemento)
        {
            Dictionary<string, object> elementos = new Dictionary<string, object>();

            try
            {
                foreach (MemberInfo infoMiembro in objeto.GetType().GetMembers())
                {
                    if (infoMiembro.MemberType == tipoElemento)
                    {
                        if ((PropertyInfo)infoMiembro != null)
                            elementos.Add(((PropertyInfo)infoMiembro).Name, ((PropertyInfo)infoMiembro).GetValue(objeto, null));
                    }
                }

                return elementos;
            }
            catch (Exception ex)
            {
                //ErrorManager em = new ErrorManager(ex);
                throw ex;
                //return null;
            }
        }

        private ArrayList CrearParametros(object dto)
        {
            ArrayList parametros = new ArrayList();
            Dictionary<string, object> colDatos = new Dictionary<string, object>();

            // Obtengo todas las propiedades del objeto
            colDatos = this.ObtenerElementos(dto, MemberTypes.Property);

            if (colDatos != null)
            {
                foreach (string llave in colDatos.Keys)
                {
                    if (colDatos[llave] != null)
                    {
                        switch (_connector)
                        {
                            case TipoConnector.Access: break; // No se admite
                            case TipoConnector.DB2: break;
                            case TipoConnector.Firebird: break;
                            case TipoConnector.MSSQL:
                                parametros.Add(new SqlParameter("@" + llave, colDatos[llave]));
                                break;
                            case TipoConnector.MySQL:
                                Assembly asmMy = Assembly.LoadFrom("MySql.Data.dll");
                                Type tipoMy = asmMy.GetType("MySql.Data.MySqlClient.MySqlParameter");
                                parametros.Add(Activator.CreateInstance(tipoMy, new object[] { "@" + llave, colDatos[llave] })); 
                                break;
                            case TipoConnector.PostgreSQL:
                                Assembly asmPg = Assembly.LoadFrom("Npgsql.dll");
                                Type tipoPg = asmPg.GetType("Npgsql.NpgsqlParameter");
                                parametros.Add(Activator.CreateInstance(tipoPg, new object[] { "@" + llave, colDatos[llave] })); 
                                //parametros.Add(new Npgsql.NpgsqlParameter("@" + llave, colDatos[llave]));
                                break;
                            case TipoConnector.SQLite: break;
                        }
                    }
                }
            }

            return parametros;
        }

        public bool Insert(object dto)
        {
            StringBuilder sql = new StringBuilder();
            StringBuilder campos = new StringBuilder();
            StringBuilder valores = new StringBuilder();
            ArrayList parametros = new ArrayList();

            try
            {
                // Compruebo que el DTO existe
                if (dto == null)
                    throw new NullReferenceException("DAOGeneric.Insert(dto)");

                Dictionary<string, object> colDatos = new Dictionary<string, object>();

                // Obtengo todas las propiedades del objeto
                colDatos = this.ObtenerElementos(dto, MemberTypes.Property);

                if (colDatos != null)
                {
                    foreach (string llave in colDatos.Keys)
                    {
                        if (colDatos[llave] != null)
                        {
                            campos.Append(llave).Append(", ");
                            valores.Append("@").Append(llave).Append(", ");
                            //parametros.Add(new SqlParameter("@" + llave, colDatos[llave]));
                        }
                    }

                    // Se crea la clausula INSERT
                    sql.Append("INSERT INTO ").Append((dto.GetType()).Name).Append(" (");

                    // Agrego los campos a insertar
                    if (campos.Length > 0)
                    {
                        // Como la cadena termina con una coma, elimino los ultimos 2 caracteres                    
                        sql.Append(campos.ToString().Substring(0, campos.ToString().Length - 2)).Append(") VALUES (");
                    }

                    // Agrego los valores a insertar
                    if (valores.Length > 0)
                    {
                        sql.Append(valores.ToString().Substring(0, valores.ToString().Length - 2)).Append(")");
                    }
                }

                // Se crean los parametros
                parametros = this.CrearParametros(dto);

                if (this.db.Abrir())
                {
                    if (this.propBaseDatos != null)
                    {
                        this.db.CambiarBaseDatos(this.propBaseDatos);
                        this.propBaseDatos = null;
                    }

                    this.db.Consultar(sql.ToString(), parametros);
                }

                this.db.Cerrar();
                return true;
            }
            catch (Exception ex)
            {
                if (this.db.EstaAbierta)
                    this.db.Cerrar();

                //ErrorManager em = new ErrorManager(ex);
                throw ex;
                //return false;
            }
        }

        private DataStorage Select(object dto, DataGridView grilla, ComboBox combo, ToolStripComboBox comboToolStrip, string campoNombre, string campoItemData, string ordenarPor)
        {
            StringBuilder sql = new StringBuilder();
            StringBuilder campos = new StringBuilder();
            ArrayList parametros = new ArrayList();
            DataStorage ds = null;

            try
            {
                // Compruebo que el DTO existe
                if (dto == null)
                    throw new NullReferenceException("DAOGeneric.Select(dto)");

                Dictionary<string, object> colDatos = new Dictionary<string, object>();

                // Obtengo todas las propiedades del objeto
                colDatos = this.ObtenerElementos(dto, MemberTypes.Property);

                if (colDatos != null)
                {
                    foreach (string llave in colDatos.Keys)
                    {
                        if (colDatos[llave] != null)
                        {
                            campos.Append(llave).Append(" = @").Append(llave).Append(" AND ");
                            //parametros.Add(this.db.CrearParametro("@" + llave, colDatos[llave]));
                        }
                    }
                }

                // Se crea la clausula SELECT
                sql.Append("SELECT * FROM ").Append((dto.GetType()).Name);

                // Se agrega la clausula WHERE en caso de que sea necesario
                if (campos.Length > 0)
                {
                    sql.Append(" WHERE ");

                    // Como la cadena acabará en 'AND ', eliminamos los últimos 4 caracteres para cerrar la sentencia
                    sql.Append(campos.ToString().Substring(0, campos.ToString().Length - 4));
                }

                // Se crean los parametros
                parametros = this.CrearParametros(dto);

                // Verifica si es que hay que ordenar los resultados
                if (ordenarPor != null)
                    sql.Append(" ORDER BY " + ordenarPor);

                this.db.Abrir();

                if (this.db.EstaAbierta)
                {
                    if (this.propBaseDatos != null)
                    {
                        this.db.CambiarBaseDatos(this.propBaseDatos);
                        this.propBaseDatos = null;
                    }

                    if (grilla != null)
                        ds = this.db.Consultar(sql.ToString(), grilla, parametros);
                    else if (combo != null)
                        ds = this.db.Consultar(sql.ToString(), combo, campoNombre, parametros);
                    else if (comboToolStrip != null)
                        ds = this.db.Consultar(sql.ToString(), comboToolStrip, campoNombre, parametros);
                    else
                        ds = this.db.Consultar(sql.ToString(), parametros);
                }

                this.db.Cerrar();
                return ds;
            }
            catch (Exception ex)
            {
                if (this.db.EstaAbierta)
                    this.db.Cerrar();

                //ErrorManager em = new ErrorManager(ex);
                throw ex;
                //return null;
            }
        }

        public DataStorage Select(object dto)
        {
            return this.Select(dto, null, null, null, null, null, null);
        }

        public DataStorage Select(object dto, string ordenarPor)
        {
            return this.Select(dto, null, null, null, null, null, ordenarPor);
        }

        public DataStorage Select(object dto, ComboBox combo, string campoItem)
        {
            return this.Select(dto, null, combo, null, campoItem, null, null);
        }

        public DataStorage Select(object dto, ComboBox combo, string campoItem, string ordenarPor)
        {
            return this.Select(dto, null, combo, null, campoItem, null, ordenarPor);
        }

        public DataStorage Select(object dto, ToolStripComboBox comboToolStrip, string campoItem)
        {
            return this.Select(dto, null, null, comboToolStrip, campoItem, null, null);
        }

        public DataStorage Select(object dto, ToolStripComboBox comboToolStrip, string campoItem, string ordenarPor)
        {
            return this.Select(dto, null, null, comboToolStrip, campoItem, null, ordenarPor);
        }

        public DataStorage Select(object dto, DataGridView grilla, string ordenarPor)
        {
            return this.Select(dto, grilla, null, null, null, null, ordenarPor);
        }

        public DataTable SelectDataTable(object dto, string ordenarPor)
        {
            StringBuilder sql = new StringBuilder();
            StringBuilder campos = new StringBuilder();
            ArrayList parametros = new ArrayList();
            DataTable dt = null;

            try
            {
                // Compruebo que el DTO existe
                if (dto == null)
                    throw new NullReferenceException("DAOGeneric.Select(dto)");

                Dictionary<string, object> colDatos = new Dictionary<string, object>();

                // Obtengo todas las propiedades del objeto
                colDatos = this.ObtenerElementos(dto, MemberTypes.Property);

                if (colDatos != null)
                {
                    foreach (string llave in colDatos.Keys)
                    {
                        if (colDatos[llave] != null)
                        {
                            campos.Append(llave).Append(" = @").Append(llave).Append(" AND ");
                            //parametros.Add(this.db.CrearParametro("@" + llave, colDatos[llave]));
                        }
                    }
                }

                // Se crea la clausula SELECT
                sql.Append("SELECT * FROM ").Append((dto.GetType()).Name);

                // Se agrega la clausula WHERE en caso de que sea necesario
                if (campos.Length > 0)
                {
                    sql.Append(" WHERE ");

                    // Como la cadena acabará en 'AND ', eliminamos los últimos 4 caracteres para cerrar la sentencia
                    sql.Append(campos.ToString().Substring(0, campos.ToString().Length - 4));
                }

                // Se crean los parametros
                parametros = this.CrearParametros(dto);

                // Verifica si es que hay que ordenar los resultados
                if (ordenarPor != null)
                    sql.Append(" ORDER BY " + ordenarPor);

                this.db.Abrir();

                if (this.db.EstaAbierta)
                {
                    if (this.propBaseDatos != null)
                    {
                        this.db.CambiarBaseDatos(this.propBaseDatos);
                        this.propBaseDatos = null;
                    }

                    //dt = this.db.ConsultarDataTable(sql.ToString(), parametros);
                }

                this.db.Cerrar();
                return dt;
            }
            catch (Exception ex)
            {
                if (this.db.EstaAbierta)
                    this.db.Cerrar();

                //ErrorManager em = new ErrorManager(ex);
                throw ex;
                //return null;
            }
        }        

        public DataTable SelectDataTable(object dto)
        {
            return this.SelectDataTable(dto, null);
        }

        public int Update(object dto, object dtoCondicion)
        {
            StringBuilder sql = new StringBuilder();
            StringBuilder campos = new StringBuilder();
            StringBuilder camposCondicion = new StringBuilder();
            ArrayList parametros = new ArrayList();
            ArrayList parametrosCondicion = new ArrayList();
            
            try
            {
                // Compruebo que el dto existe
                if (dto == null || dtoCondicion == null)
                    throw new NullReferenceException("DAOGeneric.Update(dto, dtoCondicion)");

                Dictionary<string, object> colDatos = new Dictionary<string, object>();
                Dictionary<string, object> colDatosCondicion = new Dictionary<string, object>();

                // Obtengo todas las propiedades de los dto
                colDatos = this.ObtenerElementos(dto, MemberTypes.Property);
                colDatosCondicion = this.ObtenerElementos(dtoCondicion, MemberTypes.Property);

                if (colDatos != null && colDatosCondicion != null)
                {
                    foreach (string llave in colDatos.Keys)
                    {
                        if (colDatos[llave] != null)
                        {
                            campos.Append(llave).Append(" = @").Append(llave).Append(", ");
                            //parametros.Add(new SqlParameter("@" + llave, colDatos[llave]));
                        }
                    }

                    foreach (string llave in colDatosCondicion.Keys)
                    {
                        if (colDatosCondicion[llave] != null)
                        {
                            camposCondicion.Append(llave).Append(" = @").Append(llave).Append(" AND ");
                            //parametrosCondicion.Add(new SqlParameter("@" + llave + "_cond", colDatosCondicion[llave]));
                        }
                    }

                    // Se crea la clausula UPDATE
                    sql.Append("UPDATE ").Append(dto.GetType().Name).Append(" SET ");

                    // Agrego los campos y la clausula WHERE que en este caso es obligatoria
                    if (campos.Length > 0 && camposCondicion.Length > 0)
                    {
                        // Agrego los campos que se actualizaran
                        // Como la cadena acabara en ', ', elimino los ultimos 3 caracteres
                        sql.Append(campos.ToString().Substring(0, campos.ToString().Length - 2));

                        sql.Append(" WHERE ");

                        // Como la cadena acabara en 'AND ', elimino los ultimos 4 caracteres
                        sql.Append(camposCondicion.ToString().Substring(0, camposCondicion.ToString().Length - 4));

                        // Transpaso los items de parametrosCondicion a parametros
                        for (int i = 0; i <= parametrosCondicion.Count - 1; i++)
                        {
                            parametros.Add(parametrosCondicion[i]);
                        }

                        // Se crean los parametros
                        parametros = this.CrearParametros(dto);                        
                        parametrosCondicion = this.CrearParametros(dtoCondicion);
                        parametros.AddRange(parametrosCondicion);

                        this.db.Abrir();

                        if (this.db.EstaAbierta)
                        {
                            if (this.propBaseDatos != null)
                            {
                                this.db.CambiarBaseDatos(this.propBaseDatos);
                                this.propBaseDatos = null;
                            }

                            this.db.Consultar(sql.ToString(), parametros);
                        }

                        this.db.Cerrar();
                        return this.db.FilasAfectadas;
                    }
                }

                return -1;
            }
            catch (Exception ex)
            {
                if (this.db.EstaAbierta)
                    this.db.Cerrar();

                //ErrorManager em = new ErrorManager(ex);
                throw ex;
                //return -1;
            }
        }

        public int Delete(object dto)
        {
            StringBuilder sql = new StringBuilder();
            StringBuilder campos = new StringBuilder();
            ArrayList parametros = new ArrayList();

            try
            {
                // Compruebo que el dto existe
                if (dto == null)
                    throw new NullReferenceException("DAOGeneric.Delete(dto)");

                Dictionary<string, object> colDatos = new Dictionary<string, object>();

                // Obtengo todas las propiedades del objeto
                colDatos = this.ObtenerElementos(dto, MemberTypes.Property);

                if (colDatos != null)
                {
                    foreach (string llave in colDatos.Keys)
                    {
                        if (colDatos[llave] != null)
                        {
                            campos.Append(llave).Append(" = @").Append(llave).Append(" AND ");
                            //parametros.Add(new SqlParameter("@" + llave, colDatos[llave]));
                        }
                    }
                }

                // Se crea la clausula DELETE
                sql.Append("DELETE FROM ").Append((dto.GetType()).Name);

                // Se agrega la clausula WHERE que en este caso es obligatoria
                if (campos.Length > 0)
                {
                    sql.Append(" WHERE ");

                    // Como la cadena acabara en 'AND ', elimino los ultimos 4 caracteres
                    sql.Append(campos.ToString().Substring(0, campos.ToString().Length - 4));

                    // Se crean los parametros
                    parametros = this.CrearParametros(dto);

                    this.db.Abrir();

                    if (this.db.EstaAbierta)
                    {
                        if (this.propBaseDatos != null)
                        {
                            this.db.CambiarBaseDatos(this.propBaseDatos);
                            this.propBaseDatos = null;
                        }

                        this.db.Consultar(sql.ToString(), parametros);
                    }

                    this.db.Cerrar();
                    return this.db.FilasAfectadas;
                }

                return -1;
            }
            catch (Exception ex)
            {
                if (this.db.EstaAbierta)
                    this.db.Cerrar();

                throw ex;
                //ErrorManager em = new ErrorManager(ex);
                //return -1;
            }
        }       
        #endregion

        #region IDisposable
        private void Dispose(bool disposing)
        {
            if (disposed && !this.disposed)
            {
                this.db = null;
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
