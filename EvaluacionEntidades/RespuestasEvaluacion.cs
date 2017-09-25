using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvaluacionEntidades
{
	public class RespuestasEvaluacion : LBSFramework.Entitys.Entity 
	{

		#region Atributos Privados
		/******************************************************/
		private int _idRespuesta;
		private int _IdEvaluacion;
		private int _IdItem;
		private string _Valor;
		private string _ValorSupervisor;


		/******************************************************/
		#endregion Fin Atributos Privados 
		#region  Propiedades 
		/******************************************************/
		public int	idRespuesta
		{
 			get{ return _idRespuesta; }
			set{ _idRespuesta = value;
			Identificador.identidadAdd("idRespuesta", null); } 
		} 

		public int	IdEvaluacion
		{
 			get{ return _IdEvaluacion; }
			set{ _IdEvaluacion = value; } 
		} 

		public int	IdItem
		{
 			get{ return _IdItem; }
			set{ _IdItem = value; } 
		} 

		public string	Valor
		{
 			get{ return _Valor; }
			set{ _Valor = value; } 
		} 

		public string	ValorSupervisor
		{
 			get{ return _ValorSupervisor; }
			set{ _ValorSupervisor = value; } 
		}

        public string item { get; set; }

        public bool Escrito { get; set; }

        public int idTipoEvaluacion { get; set; }

        public bool Supervisa { get; set; }

        public string TipoEvaluacion { get; set; }

        public string TipoEvaluacionDescrip { get; set; }

        public string escrito { get; set; }

        public string escritoSupervisor { get; set; }

        /******************************************************/
        #endregion Fin de Propiedades 


        #region Constructor 
        /******************************************************/

        public RespuestasEvaluacion() 
			: base()
		{}
		/******************************************************/
		#endregion Fin Constructor 


		#region Metodos de la Entidad 
		/******************************************************/

		public static LBSFramework.Entitys.PK CrearPK(int idRespuesta)
	{
				LBSFramework.Entitys.PK oPK = new LBSFramework.Entitys.PK();
			oPK.identidadAdd("idRespuesta",idRespuesta);

			return oPK;
		}//Fin CrearPK()

		/******************************************************/
		#endregion Fin Metodos de la Entidad 


}
}