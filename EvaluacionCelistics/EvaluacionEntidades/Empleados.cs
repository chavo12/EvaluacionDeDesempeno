using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace EvaluacionEntidades
{
	public class Empleados : LBSFramework.Entitys.Entity 
	{

		#region Atributos Privados
		/******************************************************/
		private int _IdEmpleado;
		private string _EmpleadoId;
		private string _Nombre;
		private string _1erApellido;
		private string _2doApellido;
		private string _Cargo;
		private string _Departamento;
		private string _Negocio;
		private string _TipoEmpleado;
		private string _RazonSocial;
		private string _Ingreso;
		private string _Nacimiento;
		private string _TipoEvaluacion;
		private string _Pais;
		private string _CorreoElectronico;
		private int? _SupervisorID;
		private string _Localidad;
		private string _CiudadLocalidad;
		private string _Nivel;

		/******************************************************/
		#endregion Fin Atributos Privados 
		#region  Propiedades 
		/******************************************************/
		public int	IdEmpleado
		{
 			get{ return _IdEmpleado; }
			set{ _IdEmpleado = value;
			Identificador.identidadAdd("IdEmpleado", null); } 
		} 

		public string	EmpleadoId
		{
 			get{ return _EmpleadoId; }
			set{ _EmpleadoId = value; } 
		} 

		public string	Nombre
		{
 			get{ return _Nombre; }
			set{ _Nombre = value; } 
		} 

		public string	PApellido
		{
 			get{ return _1erApellido; }
			set{ _1erApellido = value; } 
		} 

		public string	SApellido
		{
 			get{ return _2doApellido; }
			set{ _2doApellido = value; } 
		} 

		public string	Cargo
		{
 			get{ return _Cargo; }
			set{ _Cargo = value; } 
		} 

		public string	Departamento
		{
 			get{ return _Departamento; }
			set{ _Departamento = value; } 
		} 

		public string	Negocio
		{
 			get{ return _Negocio; }
			set{ _Negocio = value; } 
		} 

		public string	TipoEmpleado
		{
 			get{ return _TipoEmpleado; }
			set{ _TipoEmpleado = value; } 
		} 

		public string	RazonSocial
		{
 			get{ return _RazonSocial; }
			set{ _RazonSocial = value; } 
		} 

		public string	Ingreso
		{
 			get{ return _Ingreso; }
			set{ _Ingreso = value; } 
		} 

		public string	Nacimiento
		{
 			get{ return _Nacimiento; }
			set{ _Nacimiento = value; } 
		} 

		public string	TipoEvaluacion
		{
 			get{ return _TipoEvaluacion; }
			set{ _TipoEvaluacion = value; } 
		} 

		public string	Pais
		{
 			get{ return _Pais; }
			set{ _Pais = value; } 
		} 

		public string	CorreoElectronico
		{
 			get{ return _CorreoElectronico; }
			set{ _CorreoElectronico = value; } 
		} 

		public int?	SupervisorID
		{
 			get{ return _SupervisorID; }
			set{ _SupervisorID = value; } 
		} 

		public string	Localidad
		{
 			get{ return _Localidad; }
			set{ _Localidad = value; } 
		} 

		public string	CiudadLocalidad
		{
 			get{ return _CiudadLocalidad; }
			set{ _CiudadLocalidad = value; } 
		} 

		public string	Nivel
		{
 			get{ return _Nivel; }
			set{ _Nivel = value; } 
		}

        public string nombreCompleto { get; set; }

        public string estadoEvaluacion { get; set; }

        public int IdEvaluacion { get; set; }

        public string supervisor { get; set; }

        public string NumPia { get; set; }

        public string Rol { get; set; }

        public DateTime? inicio { get; set; }

        public DateTime? fin { get; set; }

        public DateTime? inicioSuper { get; set; }

        public DateTime? finSuper { get; set; }

        public string desempenoGlobal { get; set; }

        public string desempenoGlobalSuper { get; set; }

        public string mailSupervisor { get; set; }

        /******************************************************/
        #endregion Fin de Propiedades 


        #region Constructor 
        /******************************************************/

        public Empleados() 
			: base()
		{}
		/******************************************************/
		#endregion Fin Constructor 


		#region Metodos de la Entidad 
		/******************************************************/

		public static LBSFramework.Entitys.PK CrearPK(int IdEmpleado)
	{
				LBSFramework.Entitys.PK oPK = new LBSFramework.Entitys.PK();
			oPK.identidadAdd("IdEmpleado",IdEmpleado);

			return oPK;
		}//Fin CrearPK()

		/******************************************************/
		#endregion Fin Metodos de la Entidad 


}
}