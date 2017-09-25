using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvaluacionEntidades
{
	public class ItemsEvaluacion : LBSFramework.Entitys.Entity 
	{

		#region Atributos Privados
		/******************************************************/
		private int _IdItem;
		private string _Descripcion;
		private int _idTipoEvalucion;
		private string _Nivel;
		private bool? _Escrito;
		private string _Idioma;

		/******************************************************/
		#endregion Fin Atributos Privados 
		#region  Propiedades 
		/******************************************************/
		public int	IdItem
		{
 			get{ return _IdItem; }
			set{ _IdItem = value;
			Identificador.identidadAdd("IdItem", null); } 
		} 

		public string	Descripcion
		{
 			get{ return _Descripcion; }
			set{ _Descripcion = value; } 
		} 

		public int	idTipoEvalucion
		{
 			get{ return _idTipoEvalucion; }
			set{ _idTipoEvalucion = value; } 
		} 

		public string	Nivel
		{
 			get{ return _Nivel; }
			set{ _Nivel = value; } 
		} 

		public bool?	Escrito
		{
 			get{ return _Escrito; }
			set{ _Escrito = value; } 
		} 

		public string	Idioma
		{
 			get{ return _Idioma; }
			set{ _Idioma = value; } 
		}

        public string tipoEvaluacion { get; set; }

        /******************************************************/
        #endregion Fin de Propiedades 


        #region Constructor 
        /******************************************************/

        public ItemsEvaluacion() 
			: base()
		{}
		/******************************************************/
		#endregion Fin Constructor 


		#region Metodos de la Entidad 
		/******************************************************/

		public static LBSFramework.Entitys.PK CrearPK(int IdItem)
	{
				LBSFramework.Entitys.PK oPK = new LBSFramework.Entitys.PK();
			oPK.identidadAdd("IdItem",IdItem);

			return oPK;
		}//Fin CrearPK()

		/******************************************************/
		#endregion Fin Metodos de la Entidad 


}
}