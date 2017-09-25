using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace LBSFramework.ConexionBD
{
    class LBSSqlConnection
    {
        #region Atributos y Propiedades
        /*********************************/
        //Atributos
        private SqlConnection _conexion;
        private Boolean _lock = false;
        private static LBSSqlConnection _ConexionTransaccion = null;
        private static SqlTransaction _TransaccionActual = null;


        //Propiedades
        public static SqlTransaction Transaction
        {
            get { return LBSSqlConnection._TransaccionActual; }
            //set { LBSSqlConnection._TransaccionActual = value; }
        }

        internal static LBSSqlConnection ConexionTransaccion
        {
            get { return LBSSqlConnection._ConexionTransaccion; }
            //set { LBSSqlConnection._ConexionTransaccion = value; }
        }

        public SqlConnection Conexion
        {
            get { return _conexion; }
            set { _conexion = value; }
        }

        public Boolean Lock
        {
            get { return _lock; }
            set { _lock = value; }
        }

        /*********************************/
        #endregion Fin Atributos y Propiedades

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="oSqlConex"></param>
        public LBSSqlConnection(SqlConnection oSqlConex)
        {
            this.Conexion = oSqlConex;
        }//Fin LBSSqlConnection() //Constructor

        /// <summary>
        /// Este Metodo abre la Conexion
        /// </summary>
        /// <returns></returns>
        public LBSSqlConnection Open()
        {
            try
            {
                if (_conexion != null)
                {
                    _lock = true;
                    if (_conexion.State != ConnectionState.Open)
                        _conexion.Open();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return this;
        }//Fin Open()
        
        /// <summary>
        /// Inicio la Transaccion
        /// </summary>
        public void BeginTransaction()
        {
            try
            {
                Open();

                _TransaccionActual = _conexion.BeginTransaction();
                _ConexionTransaccion = this;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }//Fin BeginTransaction()

        /// <summary>
        /// Hace un Commit de la Transaccion
        /// </summary>
        public void CommitTransaction()
        {
            try
            {
                _TransaccionActual.Commit();
                _ConexionTransaccion = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }//Fin CommitTransaction()
                	
        /// <summary>
        /// Hace un RollBack de la Transaccion
        /// </summary>
        public void RoolbackTransaction()
        {
            try
            {
                _TransaccionActual.Rollback();
                _ConexionTransaccion = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }//Fin RoolbackTransaction()

        /// <summary>
        /// Cierra la Conexion, si se recibe como parametro "true" fuerza el Cierre de la misma
        /// </summary>
        /// <param name="force"></param>
        /// <returns></returns>
        public LBSSqlConnection Close(Boolean force=false)
        {
            try
            {
                if (_conexion != null)
                {
                    if (force)
                        _conexion.Close();

                    _lock = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return this;
        }//Fin Close()

        /// <summary>
        /// De una lista de instancias retorna la que es igual a la recibida como "oSqlCon"
        /// </summary>
        /// <param name="lstLBSSql"></param>
        /// <param name="oSqlCon"></param>
        /// <returns></returns>
        public static LBSSqlConnection GetInstance(List<LBSSqlConnection> lstLBSSql, SqlConnection oSqlCon)
        {
            foreach (LBSSqlConnection oLbsSql in lstLBSSql)
            {
                if (oLbsSql.Conexion.Equals(oSqlCon))
                    return oLbsSql;
            }
            return null;
        }//Fin GetInstance(...)             

    }//----------Fin Clase LBSSqlConnection------------------------
}
