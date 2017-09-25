using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Web;
using System.Collections.Generic;
using LBSFramework.Entitys;
using System;
using System.Text;
using System.Reflection;

namespace LBSFramework.ConexionBD
{
	public abstract class AbstractMapper
	{
		#region Atributos
		/********************************************************/

		private static string _CadenaConexion = string.Empty;

		private static List<LBSSqlConnection> _LstConexion=new List<LBSSqlConnection>();
		private SqlCommand  _command = null;
		protected Boolean _isStoreProcedure = true;
		private static string IDUNICO = "idUnico";

		private SqlCommand _rdCmd= new SqlCommand();
		private SqlDataReader _rdId;

		private static Boolean _TransacActiva;
		private static SqlTransaction _Transaccion;


		/********************************************************/
		#endregion Fin Atributos

		#region Metodos Abstractos que se deben implementar en las clases Hijas
		/********************************************************/

		//public abstract void agregarPK(Entity oEntity);

		//public abstract object DoLoad(SqlDataReader reader);

        public abstract IList GetList();
        public abstract Entity InicializarEntidad();
        public abstract Object retornaEntidad(PK oPK);

		public abstract PK Insert(Entity oEntity);
		public abstract void Update(Entity oEntity);


		/********************************************************/
		#endregion Fin Metodos Abstractos que se deben implementar en las clases Hijas


        public Object DoLoad(SqlDataReader reader) 
        {
            Entity oEntidad = InicializarEntidad();
            
           
            for (int i = 0; i < reader.FieldCount; i++)
            {
                string nombreCol = reader.GetName(i);
                
                //me ubico en la propiedad
                PropertyInfo _Prop = oEntidad.GetType().GetProperty(nombreCol);

                if (_Prop != null)
                {
                    //Actualizo el Valor, confirmar que funcione el Casteo del Tipo
                    //Obtengo el Tipo de la prtopiedad para realizar el Casteo correspondianete
                    switch (_Prop.PropertyType.FullName)
                    {
                        case "System.Int32":
                            _Prop.SetValue(oEntidad, GetInt(reader, nombreCol), null);
                            break;
                        case "System.Boolean":
                            _Prop.SetValue(oEntidad, GetBoolean(reader, nombreCol), null);
                            break;
                        case "System.Char":
                            _Prop.SetValue(oEntidad, GetChar(reader, nombreCol), null);
                            break;
                        case "System.DateTime":
                            _Prop.SetValue(oEntidad, GetDateTime(reader, nombreCol), null);
                            break;
                        case "System.Decimal":
                            _Prop.SetValue(oEntidad, decimal.Parse(GetDecimal(reader, nombreCol).ToString("F")), null);
                            break;
                        case "System.Double":
                            _Prop.SetValue(oEntidad, GetDouble(reader, nombreCol), null);
                            break;
                        case "System.Guid":
                            _Prop.SetValue(oEntidad, GetUniqueID(reader, nombreCol), null);
                            break;
                        case "System.String":
                            _Prop.SetValue(oEntidad, GetString(reader, nombreCol), null);
                            break;
                        default:
                            if (_Prop.PropertyType.FullName.IndexOf("System.Decimal") > -1) _Prop.SetValue(oEntidad, GetNullableDecimal(reader, nombreCol), null);
                            else if (_Prop.PropertyType.FullName.IndexOf("System.Int32") > -1) _Prop.SetValue(oEntidad,GetIntNullable(reader, nombreCol), null);
                                else _Prop.SetValue(oEntidad, GetObject(reader, nombreCol), null);
                            break;
                    }//Fin Switch

                    if (oEntidad.Identificador.esPK(nombreCol))
                    {
                        oEntidad.Identificador.valorSetPorNombre(nombreCol, _Prop.GetValue(oEntidad, null));
                    }

                }
                else
                {
                    throw new Exception("La Columna retornada no pertenece a la Entidad" + " (" + nombreCol + ")");
                }
            }            

            return oEntidad;
        }



		public AbstractMapper()
		{ }

		#region Propiedades
		/********************************************************/
			
		private LBSSqlConnection ConexionHabilitada
		{
			get {
				 // Si estoy en una transaccion devuelvo la conexion afectada
				if (LBSSqlConnection.ConexionTransaccion != null)
					return LBSSqlConnection.ConexionTransaccion;

				LBSSqlConnection retConex = null;

				foreach (LBSSqlConnection oSqlCon in _LstConexion)
				{
					if (oSqlCon != null && !oSqlCon.Lock)
						retConex = oSqlCon;
				}

				if (retConex == null)
				{
					_LstConexion.Add(new LBSSqlConnection(new SqlConnection(CadenaConexion)));
					retConex = _LstConexion[_LstConexion.Count - 1];
					retConex.Lock = true;
				}

				return retConex;
			}//Fin Get
		}//Fin Propiedad ConexionHabilitada
        
		public void ClearConexionList()
		{ 
			if (_LstConexion!=null )
			{
				_CadenaConexion = String.Empty;
				_LstConexion.Clear();
			}
		}//Fin ClearConexionList
        
		private string CadenaConexion
		{
			get
			{
				if (_CadenaConexion == String.Empty)
				{
					if (Ambiente.EsClienteEscritorio())
                        return _CadenaConexion = Ambiente.ConexionEscritorio();

					if (Ambiente.EsWebService())
                        return _CadenaConexion = Ambiente.ConexionWebSQL();

					if (Ambiente.EsSitioWeb())
                        return _CadenaConexion = Ambiente.ConexionSitioWeb();

					if (Ambiente.EsServicio())
                        return _CadenaConexion = Ambiente.ConexionServicio();
					else
					    throw new Exception("No se a ha determinado el 'Ambiente' para determinar la Cadena de Conexion");

				}//Fin if _CadenaConexion == String.Empty               

				return _CadenaConexion;
			}//Fin Get
		}//Fin Propiedad ConexionHabilitada

		protected String CommandName
		{
			get 
			{
				if (_command != null)
					return _command.CommandText;
				else
					return "";
			}
			set 
			{
                if (_command == null)
                {
                    _command = new SqlCommand();
                    if (_TransacActiva)
                        _command.Transaction = LBSSqlConnection.Transaction;

                    _command.CommandText = value;
                    _command.Parameters.Clear();
                }
			}
		}//Fin CommandName

		protected Boolean TransaccionActiva
		{
			get 
			{
				return _TransacActiva;
			}
		}

		/********************************************************/
		#endregion Fin Propiedades

		#region Conectar y Desconectar
		/********************************************************/	

		private void Conectar(Boolean ComprobarStore = true, int? timeout = null)
		{
			try
			{
				if (_command != null)
				{
					_command.Connection = ConexionHabilitada.Open().Conexion;
                    if (timeout.HasValue) _command.CommandTimeout = timeout.Value;
					if (ComprobarStore)
					{
						if (_isStoreProcedure)
							_command.CommandType = CommandType.StoredProcedure;
						else
							_command.CommandType = CommandType.Text;
					}//fin ComprobarStore
				}
			}//fin try
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}//Fin catch
		}//Fin Metodo Conectar(...)

		private void Desconectar(SqlCommand oCommand = null)
		{
			try
			{
				if (oCommand == null)
					oCommand = _command;

				LBSSqlConnection oLbsSql = LBSSqlConnection.GetInstance(_LstConexion, oCommand.Connection);
				if (oLbsSql != null)
					oLbsSql.Close();
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}//Fin Desconectar
		
		/********************************************************/
		#endregion Fin Conectar y Desconectar

		#region Transaction
		/********************************************************/

		public void BeginTransaction()
		{
			try
			{
				if (_TransacActiva)
					return;

				LBSSqlConnection oLbsSql;
				if (_command != null)
					oLbsSql = LBSSqlConnection.GetInstance(_LstConexion, _command.Connection);
				else
					oLbsSql = ConexionHabilitada.Open();

				oLbsSql.BeginTransaction();

				if (_command != null)
					_command.Transaction = LBSSqlConnection.Transaction;

				_TransacActiva = true;
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}//Fin BeginTransaction

		public void CommitTransaction()
		{
			try
			{
				if (!_TransacActiva)
					return;

				LBSSqlConnection.Transaction.Commit();
				LBSSqlConnection.ConexionTransaccion.Close(true);

				_TransacActiva = false;

			}//Fin Try
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}//Fin Catch
						
		}//Fin CommitTransaction()

		public void RollbackTransaction()
		{
			try
			{
				if(_TransacActiva )
					//return;
                    LBSSqlConnection.Transaction.Rollback();

				LBSSqlConnection.ConexionTransaccion.Close(true);

				_TransacActiva = false;
			}//Fin Try
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}//Fin Catch			
		}//Fin RollbackTransaction()

		/********************************************************/
		#endregion Fin Transaction

		#region Execute
		/********************************************************/

		/// <summary>
		/// OJO!!!!! COMENTAR BIEN QUE LOS STORED TIENEN QUE RETORNAR "NOMBRE" "VALOR" DE LAS PKs
		/// </summary>
		/// <returns></returns>
		protected PK ExecutePK() 
		{
			SqlDataReader retObj = null;
			PK retPK= new PK();
			try
			{
				Conectar();
                try
                {
                    retObj = _command.ExecuteReader();
                }
                catch (Exception e)
                {
                    if (_TransacActiva != null && _TransacActiva) _TransacActiva = false;
                    throw new Exception(e.Message);
                }

				while (retObj.Read())
				{   //Aca esta Nemo!!!!!!!!
                    if (retObj.FieldCount == 2)
                        retPK.identidadAdd(GetString(retObj, "Nombre"), GetObject(retObj, "Valor"));
                    else
                    {
                        string tipo = GetString(retObj, "Tipo");
                        object valor = null;
                        switch (tipo)
                        {
                            case "Int32":
                                valor = GetInt(retObj, "Valor");
                                break;
                            case "Boolean":
                                valor = GetBoolean(retObj, "Valor");
                                break;
                            case "Char":
                                valor = GetChar(retObj, "Valor");
                                break;
                            case "DateTime":
                                valor = GetDateTime(retObj, "Valor");
                                break;
                            case "Decimal":
                                valor = GetDecimal(retObj, "Valor");
                                break;
                            case "Double":
                                valor = GetDouble(retObj, "Valor");
                                break;
                            case "Guid":
                                valor = GetUniqueID(retObj, "Valor");
                                break;
                            case "String":
                                valor = GetString(retObj, "Valor");
                                break;
                            default:
                                valor = GetObject(retObj, "Valor");
                                break;
                        }
                        retPK.identidadAdd(GetString(retObj, "Nombre"), valor);
                    }
				}
			}//Fin Try
			catch(Exception ex)
			{
               
				throw;
			}
			finally
			{
                if (retObj != null)
                {
                    if(!retObj.IsClosed)
                        retObj.Close();

                    retObj.Dispose();
                }
				Desconectar();
			}

			return retPK;
		}//Fin ExecutePK
		/*o*/

        protected decimal ExecuteDecimal()
        {
            decimal retdec = -1;
            try
            {
                Conectar();
                try
                {
                    retdec = Convert.ToDecimal(_command.ExecuteScalar());
                }
                catch (Exception e)
                {
                    if (_TransacActiva != null && _TransacActiva) _TransacActiva = false;
                    throw new Exception(e.Message);
                }
            }//Fin Try
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }//Fin Catch
            finally
            { Desconectar(); }

            return retdec;         
        }

      
		protected int ExecuteScalar()
		{
			int retInt = -1;
			try
			{ 
				Conectar();
                try
                {
                    retInt = (int)(_command.ExecuteScalar());
                }
                catch (Exception e)
                {
                    if (_TransacActiva != null && _TransacActiva) _TransacActiva = false;
                    throw new Exception(e.Message);
                }
			}//Fin Try
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}//Fin Catch
			finally
			{    Desconectar();}

			return retInt;            
		}//Fin ExecuteScalar (int)

		protected String ExecuteStringScalar()
		{
			String retStr = String.Empty;
			try
			{
				Conectar();
                try
                {
                    retStr = (String)(_command.ExecuteScalar());
                }
                catch (Exception e)
                {
                    if (_TransacActiva != null && _TransacActiva) _TransacActiva = false;
                    throw new Exception(e.Message);
                }
			}//Fin Try
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}//Fin Catch
			finally
			{ Desconectar(); }

			return retStr;
		}//Fin ExecuteScalar (String)

		protected DataTable ReturnDataset()
		{
			DataTable TablaResultado = new DataTable();
			SqlDataAdapter Adaptador = new SqlDataAdapter();

			try
			{
				Conectar();
				Adaptador.SelectCommand = _command;
				Adaptador.Fill(TablaResultado);
			}//Fin Try
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}//Fin Catch
			finally 
			{
				Adaptador.Dispose();
				Desconectar();
			}

			return TablaResultado;
		}//Fin ReturnDataset

		private SqlDataReader ExecuteReader()
		{
			SqlDataReader retObj = null;

			try
			{
				Conectar();
                try
                {
                    retObj = _command.ExecuteReader();
                }
                catch (Exception e)
                {
                    if (_TransacActiva != null && _TransacActiva) _TransacActiva = false;
                    throw new Exception(e.Message);
                }

			}//Fin Try
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}//Fin Catch
			finally
			{
				Desconectar();
			}
			return retObj;
		}//Fin ExecuteReader

		protected void ExecuteNonQuery(int? timeout = null)
		{
			try
			{
				Conectar(timeout:timeout);
                try
                {
                    _command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    if (_TransacActiva != null && _TransacActiva) _TransacActiva = false;
                    throw new Exception(e.Message);
                }
			}//Fin Try
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}//Fin Catch
			finally
			{
				Desconectar();
			}			
		}//Fin ExecuteNonQuery




        protected object ExecuteScalarObj(bool esStoredProcedure = true)
        {
            object ret;
            try
            {
                Conectar(esStoredProcedure);

                try
                {
                    ret = _command.ExecuteScalar();
                }
                catch (Exception e)
                {
                    if (_TransacActiva != null && _TransacActiva) _TransacActiva = false;
                    throw new Exception(e.Message);
                }
            }//Fin Try
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }//Fin Catch
            finally
            { Desconectar(); }

            return ret;
        }//Fin ExecuteScalarObj




		/********************************************************/
		#endregion Fin Execute

		#region Acciones (Save)
		/********************************************************/
		
		public  PK Save(Entity oEntidad, string accion="",object valor = null)
		{
			if (oEntidad != null)
			{
				try
				{
					//Si el metodo ExistePK() retorna false quiere decir que no esta cargada la Entidad y hay que hacer el Insert
                    if (!oEntidad.Identificador.ExistePK() || accion.ToUpper() == "A" )
					{
						oEntidad.Identificador = Insert(oEntidad);
                        oEntidad.Identificador.actualizarEntidadDesdePk(oEntidad);
					}
					else//Implica que ya existe la Entida y hay que Actualizarla
						Update(oEntidad);

				}//Fin Try
				catch (Exception ex)
				{
                    if (accion.ToUpper() == "A")
                    {
                        oEntidad.Identificador = new PK();
                    }

					throw new Exception(ex.Message);
				}//Fin Catch
				return oEntidad.Identificador;			
			}
			//En este caso la Entidad no Existe
			return null;
		}//Fin Save()
        
		/********************************************************/
		#endregion Fin Acciones (Save)
		
		#region GETs
		/********************************************************/

		#region GETs Entidades
		/********************************************************/

		/// <summary>
		/// Retorna una Entidad
		/// </summary>
		/// <returns></returns>
		protected Object GetOne()
		{ 
			Object  returnObject= null;
			SqlDataReader rd = null;
			try
			{
				rd = ExecuteReader();
				if (rd.Read())
                    returnObject = DoLoad(rd);
			}//Fin Try
			catch (Exception ex)
			{
                throw ex;
			}//Fin Catch
			finally
			{
				if (rd != null)
				{
					if (!rd.IsClosed)
						rd.Close();
					rd.Dispose();
				}
				Desconectar();
			}
			return returnObject;
		}//Fin GetOne

		protected IList GetEntityList()
		{ 
			 IList returnObject   = null;
			 SqlDataReader rd = null;

			 try
			 {
				 rd = ExecuteReader();

				 if (rd.HasRows)
					 returnObject = LoadAll(rd);
				 else
					 returnObject = GetList();
			 }//Fin Try
			 catch (Exception ex)
			 {
                 throw;
			 }//Fin Catch
			 finally
			 {
				 if (rd != null)
				 {
					 if (!rd.IsClosed)
						 rd.Close();
					 rd.Dispose();
				 }
				 Desconectar();
			 }
			 return returnObject;
		}//Fin GetEntityList

		protected IList<int> GetListOfIntegers(string ColName)
		{
			SqlDataReader rd = null;
			IList<int> returnList =new List<int>();
			try
			{
				rd = ExecuteReader();
				while (rd.Read())
				{
					returnList.Add((Convert.ToInt32(rd[ColName])));
				}
			}//Fin Try
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}//Fin Catch
			finally
			{
				if (rd != null)
				{
					if (!rd.IsClosed)
						rd.Close();
					rd.Dispose();
				}
				Desconectar();
			}
			return returnList;
		}//Fin GetEntityList

		protected IList<String> GetListOfString(string ColName)
		{
			SqlDataReader rd = null;
			IList<String> returnList = new List<String>();
			try
			{
				rd = ExecuteReader();
				while (rd.Read())
				{
					returnList.Add(((rd[ColName]).ToString()));
				}
			}//Fin Try
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}//Fin Catch
			finally
			{
				if (rd != null)
				{
					if (!rd.IsClosed)
						rd.Close();
					rd.Dispose();
				}
				Desconectar();
			}
			return returnList;
		}//Fin GetListOfString

		protected IList LoadAll(SqlDataReader oReader)
		{
			IList oListRet = GetList();
			while (oReader.Read())
			{
				AddObjectToList(oListRet, oReader);                
			}
			return oListRet;
		}//Fin LoadAll

		private void AddObjectToList(IList oLst, SqlDataReader oRd)
		{
            Object obj = DoLoad(oRd);
			if (obj != null)
				oLst.Add(obj);
		}//Fin AddObjectToList
		
		/********************************************************/
		#endregion Fin GETs Entidades

		#region Gets Values SqlDataReader
		/********************************************************/

		private Boolean HasColumn(SqlDataReader reader, String colName)
		{
			for (int i = 0; i < reader.FieldCount -1; i++)
			{
				if (reader.GetName(i).Equals(colName, StringComparison.InvariantCultureIgnoreCase))
					return true;
			}
			return false;
		}//Fin HasColumn

		protected Object GetColumnValue(SqlDataReader reader, int colNum, Object nullValue=null)
		{
			if (reader[colNum] == DBNull.Value)
				return nullValue;
			else
				return reader[colNum];
		}//Fin GetColumnValue

		protected String GetString(SqlDataReader reader, String colName, String nullValue = null)
		{
			if (reader[colName] == null || reader[colName] == DBNull.Value)
				return nullValue;
			else
				return reader[colName].ToString();
		}//Fin GetString

		protected Object GetObject(SqlDataReader reader, String colName, Object nullValue = null)
		{
			if (reader[colName] == null || reader[colName] == DBNull.Value)
				return nullValue;
			else
				return reader[colName];
		}//Fin GetObject

		protected char GetChar(SqlDataReader reader, String colName, char nullValue ='\0')
		{
			if (reader[colName] == null || reader[colName] == DBNull.Value || reader[colName].ToString().Length<1)
				return nullValue;
			else
				return reader[colName].ToString()[0];
		}//Fin GetChar

		protected char? GetCharNullable(SqlDataReader reader, String colName, char? nullValue = null)
		{
			if (reader[colName] == null || reader[colName] == DBNull.Value || reader[colName].ToString().Length < 1)
				return nullValue;
			else
				return Convert.ToChar(reader[colName].ToString());
		}//Fin GetCharNullable

		protected int GetInt(SqlDataReader reader, String colName, int nullValue = -1)
		{
			if (reader[colName] == null || reader[colName] == DBNull.Value)
				return nullValue;
			else
				return int.Parse(reader[colName].ToString());
		}//Fin GetInt

		protected int? GetIntNullable(SqlDataReader reader, String colName, int? nullValue = null)
		{
			if (reader[colName] == null || reader[colName] == DBNull.Value)
				return nullValue;
			else
				return int.Parse(reader[colName].ToString());
		}//Fin GetIntNullable

		protected DateTime? GetDateTime(SqlDataReader reader, String colName, DateTime? nullValue=null)
		{
			if (reader[colName] == null || reader[colName] == DBNull.Value)
				return nullValue;
			else
				return DateTime.Parse(reader[colName].ToString());
		}//Fin GetDateTime

		protected DateTime? GetDateTimeNullable(SqlDataReader reader, String colName, DateTime? nullValue=null)
		{
			if (reader[colName] == null || reader[colName] == DBNull.Value)
				return nullValue;
			else
				return DateTime.Parse(reader[colName].ToString());
		}//Fin GetDateTimeNullable

		protected Boolean GetBoolean(SqlDataReader reader, String colName, Boolean nullValue=false)
		{
            if (reader[colName] == null || reader[colName] == DBNull.Value)
                return nullValue;
            else
                return Convert.ToBoolean(reader[colName]);//Boolean.Parse(reader[colName].ToString());
                //return Convert.ToBoolean(Convert.ToInt16(reader[colName].ToString()));//Boolean.Parse(reader[colName].ToString());
		}//Fin GetBoolean

		protected Boolean? GetNullableBoolean(SqlDataReader reader, String colName, Boolean? nullValue = null)
		{
			if (reader[colName] == null || reader[colName] == DBNull.Value)
				return nullValue;
			else
				return Boolean.Parse(reader[colName].ToString());
		}//Fin GetNullableBoolean

		protected float GetFloat(SqlDataReader reader, String colName, float nullValue = 0)
		{
			if (reader[colName] == null || reader[colName] == DBNull.Value)
				return nullValue;
			else
				return float.Parse(reader[colName].ToString());
		}//Fin GetFloat

		protected Double GetDouble(SqlDataReader reader, String colName, Double nullValue = 0)
		{
			if (reader[colName] == null || reader[colName] == DBNull.Value)
				return nullValue;
			else
				return Double.Parse(reader[colName].ToString());
		}//Fin GetDouble

		protected Decimal GetDecimal(SqlDataReader reader, String colName, Decimal nullValue = 0)
		{
			if (reader[colName] == null || reader[colName] == DBNull.Value)
				return nullValue;
			else
				return Decimal.Parse(reader[colName].ToString());
		}//Fin GetDecimal

		protected Decimal? GetNullableDecimal(SqlDataReader reader, String colName, Decimal? nullValue = null)
		{
			if (reader[colName] == null || reader[colName] == DBNull.Value)
				return nullValue;
			else
				return Decimal.Parse(reader[colName].ToString());
		}//Fin GetNullableDecimal

		protected Byte[] GetByteArray(SqlDataReader reader, String colName)
		{
			if (reader[colName] == null || reader[colName] == DBNull.Value)
				return null;
			else
				return Encoding.Unicode.GetBytes(reader[colName].ToString());
		}//Fin GetByteArray

		protected Guid GetUniqueID(SqlDataReader reader, String colName)
		{
			if (reader[colName] == null || reader[colName] == DBNull.Value)
				return Guid.Empty;
			else
				return Guid.Parse(reader[colName].ToString());
		}//Fin GetUniqueID

		protected Guid? GetNullableUniqueID(SqlDataReader reader, String colName)
		{
			if (reader[colName] == null || reader[colName] == DBNull.Value)
				return null;
			else
				return Guid.Parse(reader[colName].ToString());
		}//Fin GetNullableUniqueID

		protected int? GetOutIntNullable(String paramName)
		{
			if(_command.Parameters[paramName].Value==DBNull.Value)
				return null;
			else
				return int.Parse(_command.Parameters[paramName].Value.ToString());
		}//Fin GetOutIntNullable

		protected String GetOutString(String paramName)
		{
			if (_command.Parameters[paramName].Value == DBNull.Value)
				return null;
			else
				return (_command.Parameters[paramName].Value.ToString());
		}//Fin GetOutString


		/********************************************************/
		#endregion Fin Gets Values SqlDataReader


        
			

		#region Gets Values DataTable
		/********************************************************/

		private Boolean HasColumn(DataRow Fila , String colName)
		{
			return Fila.Table.Columns.Contains(colName);
		}//Fin HasColumn

		protected Object GetObject(DataRow Fila, String colName, Object nullValue=null)
		{
			if (!HasColumn(Fila, colName) || Fila[colName] == DBNull.Value)
				return nullValue;
			else
				return Fila[colName];
		}//Fin GetObject

		protected String GetString(DataRow Fila, String colName, String nullValue = null)
		{
			if (!HasColumn(Fila, colName) || Fila[colName] == DBNull.Value)
				return nullValue;
			else
				return Fila[colName].ToString();
		}//Fin GetString

		protected Char GetChar(DataRow Fila, String colName, Char nullValue = '\0')
		{
			if (!HasColumn(Fila, colName) || Fila[colName] == DBNull.Value || Fila[colName].ToString().Length < 1)
				return nullValue;
			else
				return Convert.ToChar(Fila[colName].ToString());
		}//Fin GetChar        

		protected Char? GetCharNullable(DataRow Fila, String colName, Char? nullValue = null)
		{
			if (!HasColumn(Fila, colName) || Fila[colName] == DBNull.Value || Fila[colName].ToString().Length < 1)
				return nullValue;
			else
				return Convert.ToChar(Fila[colName].ToString());
		}//Fin GetCharNullable

		protected int GetInt(DataRow Fila, String colName, int nullValue = -1)
		{
			if (!HasColumn(Fila, colName) || Fila[colName] == DBNull.Value)
				return nullValue;
			else
				return int.Parse(Fila[colName].ToString());
		}//Fin GetInt

		protected int? GetIntNullable(DataRow Fila, String colName, int? nullValue = null)
		{
			if (!HasColumn(Fila, colName) || Fila[colName] == DBNull.Value)
				return nullValue;
			else
				return Convert.ToInt32(Fila[colName].ToString());
		}//Fin GetIntNullable

		protected DateTime GetDateTime(DataRow Fila, String colName, DateTime nullValue)
		{
			if (!HasColumn(Fila, colName) || Fila[colName] == DBNull.Value)
				return nullValue;
			else
				return DateTime.Parse(Fila[colName].ToString());
		}//Fin GetDateTime

		protected DateTime? GetDateTimeNullable(DataRow Fila, String colName, DateTime? nullValue=null)
		{
			if (!HasColumn(Fila, colName) || Fila[colName] == DBNull.Value)
				return nullValue;
			else
				return DateTime.Parse(Fila[colName].ToString());
		}//Fin GetDateTimeNullable

		protected Boolean GetBoolean(DataRow Fila, String colName, Boolean nullValue=false)
		{
			if (!HasColumn(Fila, colName) || Fila[colName] == DBNull.Value)
				return nullValue;
			else
				return Boolean.Parse(Fila[colName].ToString());
		}//Fin GetBoolean

		protected Boolean? GetNullableBoolean(DataRow Fila, String colName, Boolean? nullValue = null)
		{
			if (!HasColumn(Fila, colName) || Fila[colName] == DBNull.Value)
				return nullValue;
			else
				return Boolean.Parse(Fila[colName].ToString());
		}//Fin GetNullableBoolean

		protected float GetFloat(DataRow Fila, String colName,float nullValue)
		{
			if (!HasColumn(Fila, colName) || Fila[colName] == DBNull.Value )
				return nullValue;
			else
				return float.Parse(Fila[colName].ToString());
		}//Fin GetFloat

		protected double GetDouble(DataRow Fila, String colName, double nullValue)
		{
			if (!HasColumn(Fila, colName) || Fila[colName] == DBNull.Value)
				return nullValue;
			else
				return double.Parse(Fila[colName].ToString());
		}//Fin GetDouble

		protected Decimal GetDecimal(DataRow Fila, String colName, Decimal nullValue=0)
		{
			if (!HasColumn(Fila, colName) || Fila[colName] == DBNull.Value)
				return nullValue;
			else
				return Decimal.Parse(Fila[colName].ToString());
		}//Fin GetDecimal

		protected Decimal? GetNullableDecimal(DataRow Fila, String colName, Decimal? nullValue = null)
		{
			if (!HasColumn(Fila, colName) || Fila[colName] == DBNull.Value)
				return nullValue;
			else
				return Decimal.Parse(Fila[colName].ToString());
		}//Fin GetNullableDecimal

		protected Byte[] GetByteArray(DataRow Fila, String colName, string nullValue)
		{
			if (!HasColumn(Fila, colName) || Fila[colName] == DBNull.Value)
				return null;
			else
				return Encoding.Unicode.GetBytes(Fila[colName].ToString());
		}//Fin GetByteArray

		protected Guid GetUniqueID(DataRow Fila, String colName)
		{
			if (!HasColumn(Fila, colName) || Fila[colName] == DBNull.Value)
				return Guid.Empty;
			else
				return Guid.Parse(Fila[colName].ToString());
		}//Fin GetUniqueID

		protected Guid? GetNullableUniqueID(DataRow Fila, String colName)
		{
			if (!HasColumn(Fila, colName) || Fila[colName] == DBNull.Value)
				return null;
			else
				return Guid.Parse(Fila[colName].ToString());
		}//Fin GetNullableUniqueID

		/********************************************************/
		#endregion Fin Gets Values DataTable
				
		/********************************************************/
		#endregion Fin GETs

		#region Add Parameters
		/********************************************************/

        private void AddParameterValue(String paramName, Object value)
		{
			if (!_command.Parameters.Contains(paramName))
			{
                _command.Parameters.Add(new SqlParameter(paramName,  (value==null ? DBNull.Value : value)));                
			}
        }//Fin AddParameterValue

        protected void AddParameterInt(string paramName, int value)
        {
            AddParameterValue(paramName, value);
        }//Fin AddParameterInt

        protected void AddParameter(string paramName, int value)
        {
            if (value >= 0)
                AddParameterValue(paramName, value);
            else
                AddNullParameter(paramName);
        }//Fin AddParameter

        protected void AddParameter(string paramName, object value)
        {
            if (value != null)
                AddParameterValue(paramName, value);
            else
                AddNullParameter(paramName);
        }//Fin AddParameter

        protected void AddParameter(string paramName, int? value)
        {
            if (value.HasValue)
                AddParameterValue(paramName, value.Value);
            else
                AddNullParameter(paramName);
        }//Fin AddParameter int

        protected void AddParameter(string paramName, Char? value)
        {
            if (value.HasValue)
                AddParameterValue(paramName, value.Value.ToString());
            else
                AddNullParameter(paramName);
        }//Fin AddParameter Char

        protected void AddParameter(string paramName, Boolean? value)
        {
            if (value.HasValue)
                AddParameterValue(paramName, value.Value);
            else
                AddNullParameter(paramName);
        }//Fin AddParameter Boolean

        protected void AddParameter(string paramName, Byte[] value)
        {
                AddParameterValue(paramName, value);
        }//Fin AddParameter Byte

        protected void AddParameter(string paramName, Boolean value)
        {
            AddParameterValue(paramName, value);
        }//Fin AddParameter Boolean

        protected void AddParameter(string paramName, String value)
        {
            AddParameterValue(paramName, value);
        }//Fin AddParameter String

        protected void AddParameter(string paramName, DateTime? value)
        {
            if (value.HasValue)
                AddParameterValue(paramName, value.Value);
            else
                AddNullParameter(paramName);
        }//Fin AddParameter DateTime

        protected void AddParameter(string paramName, DateTime value)
        {
            AddParameterValue(paramName, value);
        }//Fin AddParameter DateTime

        protected void AddParameter(string paramName, Double value)
        {
            AddParameterValue(paramName, value);
        }//Fin AddParameter Double

        protected void AddParameter(string paramName, Guid value)
        {            
                AddParameterValue(paramName, value);
        }//Fin AddParameter Guid

        protected void AddParameter(string paramName, Decimal value)
        {
            AddParameterValue(paramName, value);
        }//Fin AddParameter Decimal

        protected void AddParameter(string paramName, Decimal? value)
        {
            if (value.HasValue)
                AddParameterValue(paramName, value.Value);
            else
                AddNullParameter(paramName);
        }//Fin AddParameter Decimal


        protected void AddParameter(string paramName, long value)
        {
            AddParameterValue(paramName, value);
        }//Fin AddParameter Long

        protected void AddParameter(string paramName, long? value)
        {
            if (value.HasValue)
                AddParameterValue(paramName, value.Value);
            else
                AddNullParameter(paramName);
        }//Fin AddParameter Long


        protected void AddParameter(string paramName, Guid? value)
        {
            if (value.HasValue)
                AddParameterValue(paramName, value.Value);
            else
                AddNullParameter(paramName);
        }//Fin AddParameter Guid

        protected void AddNullParameter(string paramName)
        {
            if (!_command.Parameters.Contains(paramName))
            {
                //_command.Parameters.Add(paramName, DBNull.Value);
                _command.Parameters.AddWithValue(paramName, DBNull.Value);
                //_command.Parameters[paramName].Value = DBNull.Value;
            }
        }//Fin AddNullDateTimeParameter

        protected void AddParameterOut(string paramName)
        {
            if (!_command.Parameters.Contains(paramName))
            {
                SqlParameter paramResultado = new SqlParameter(paramName,null);
                paramResultado.Direction = ParameterDirection.Output;
                _command.Parameters.Add(paramResultado);
            }
        }//Fin AddParameterOut

        protected void AddStringParameterOut(string paramName)
        {
            if (!_command.Parameters.Contains(paramName))
            {
                SqlParameter paramResultado = new SqlParameter(paramName, SqlDbType.VarChar, 120);
                paramResultado.Direction = ParameterDirection.Output;
                _command.Parameters.Add(paramResultado);
            }
        }//Fin AddStringParameterOut
        
		/********************************************************/
		#endregion Fin Add Parameters
	}
}
