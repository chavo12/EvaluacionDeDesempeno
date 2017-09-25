using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvaluacionEntidades
{
	public class Logs : LBSFramework.Entitys.Entity 
	{

		#region Atributos Privados
		/******************************************************/
		private int _IdLogs;
		private int _IdEmpleado;
		private string _Pagina;
		private string _Detalle;
		private DateTime _FechayHora;

		/******************************************************/
		#endregion Fin Atributos Privados 
		#region  Propiedades 
		/******************************************************/
		public int	IdLogs
		{
 			get{ return _IdLogs; }
			set{ _IdLogs = value;
			Identificador.identidadAdd("IdLogs", null); } 
		} 

		public int	IdEmpleado
		{
 			get{ return _IdEmpleado; }
			set{ _IdEmpleado = value; } 
		} 

		public string	Pagina
		{
 			get{ return _Pagina; }
			set{ _Pagina = value; } 
		} 

		public string	Detalle
		{
 			get{ return _Detalle; }
			set{ _Detalle = value; } 
		} 

		public DateTime	FechayHora
		{
 			get{ return _FechayHora; }
			set{ _FechayHora = value; } 
		} 


		/******************************************************/
		#endregion Fin de Propiedades 


		#region Constructor 
		/******************************************************/

		public Logs() 
			: base()
		{}
		/******************************************************/
		#endregion Fin Constructor 


		#region Metodos de la Entidad 
		/******************************************************/

		public static LBSFramework.Entitys.PK CrearPK(int IdLogs)
	{
				LBSFramework.Entitys.PK oPK = new LBSFramework.Entitys.PK();
			oPK.identidadAdd("IdLogs",IdLogs);

			return oPK;
		}//Fin CrearPK()

		/******************************************************/
		#endregion Fin Metodos de la Entidad 


}
}