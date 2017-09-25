using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvaluacionEntidades
{
	public class Evaluacion : LBSFramework.Entitys.Entity 
	{

		#region Atributos Privados
		/******************************************************/
		private int _IdEvaluacion;
		private int _IdEmpleado;
		private string _Estado;
		private DateTime _Inicio;
		private DateTime? _Fin;
		private DateTime _FechaEstado;
		private DateTime _InicioSupervisor;
		private DateTime? _FinSupervisor;

		/******************************************************/
		#endregion Fin Atributos Privados 
		#region  Propiedades 
		/******************************************************/
		public int	IdEvaluacion
		{
 			get{ return _IdEvaluacion; }
			set{ _IdEvaluacion = value;
			Identificador.identidadAdd("IdEvaluacion", null); } 
		} 

		public int	IdEmpleado
		{
 			get{ return _IdEmpleado; }
			set{ _IdEmpleado = value; } 
		} 

		public string	Estado
		{
 			get{ return _Estado; }
			set{ _Estado = value; } 
		} 

		public DateTime	Inicio
		{
 			get{ return _Inicio; }
			set{ _Inicio = value; } 
		} 

		public DateTime?	Fin
		{
 			get{ return _Fin; }
			set{ _Fin = value; } 
		} 

		public DateTime	FechaEstado
		{
 			get{ return _FechaEstado; }
			set{ _FechaEstado = value; } 
		} 

		public DateTime	InicioSupervisor
		{
 			get{ return _InicioSupervisor; }
			set{ _InicioSupervisor = value; } 
		} 

		public DateTime?	FinSupervisor
		{
 			get{ return _FinSupervisor; }
			set{ _FinSupervisor = value; } 
		}

        public int completado { get; set; }


        /******************************************************/
        #endregion Fin de Propiedades 


        #region Constructor 
        /******************************************************/

        public Evaluacion() 
			: base()
		{}
		/******************************************************/
		#endregion Fin Constructor 


		#region Metodos de la Entidad 
		/******************************************************/

		public static LBSFramework.Entitys.PK CrearPK(int IdEvaluacion)
	{
				LBSFramework.Entitys.PK oPK = new LBSFramework.Entitys.PK();
			oPK.identidadAdd("IdEvaluacion",IdEvaluacion);

			return oPK;
		}//Fin CrearPK()

		/******************************************************/
		#endregion Fin Metodos de la Entidad 


}
}