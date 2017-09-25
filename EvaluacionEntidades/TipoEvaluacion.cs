using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvaluacionEntidades
{
	public class TipoEvaluacion : LBSFramework.Entitys.Entity 
	{

		#region Atributos Privados
		/******************************************************/
		private int _IdTipoEvaluacion;
		private string _Nombre;
		private string _Descripcion;
		private bool? _Supervisa;
		private string _Idioma;

		/******************************************************/
		#endregion Fin Atributos Privados 
		#region  Propiedades 
		/******************************************************/
		public int	IdTipoEvaluacion
		{
 			get{ return _IdTipoEvaluacion; }
			set{ _IdTipoEvaluacion = value;
			Identificador.identidadAdd("IdTipoEvaluacion", null); } 
		} 

		public string	Nombre
		{
 			get{ return _Nombre; }
			set{ _Nombre = value; } 
		} 

		public string	Descripcion
		{
 			get{ return _Descripcion; }
			set{ _Descripcion = value; } 
		} 

		public bool?	Supervisa
		{
 			get{ return _Supervisa; }
			set{ _Supervisa = value; } 
		} 

		public string	Idioma
		{
 			get{ return _Idioma; }
			set{ _Idioma = value; } 
		}

        public string completado { get; set; }


        /******************************************************/
        #endregion Fin de Propiedades 


        #region Constructor 
        /******************************************************/

        public TipoEvaluacion() 
			: base()
		{}
		/******************************************************/
		#endregion Fin Constructor 


		#region Metodos de la Entidad 
		/******************************************************/

		public static LBSFramework.Entitys.PK CrearPK(int IdTipoEvaluacion)
	{
				LBSFramework.Entitys.PK oPK = new LBSFramework.Entitys.PK();
			oPK.identidadAdd("IdTipoEvaluacion",IdTipoEvaluacion);

			return oPK;
		}//Fin CrearPK()

		/******************************************************/
		#endregion Fin Metodos de la Entidad 


}
}