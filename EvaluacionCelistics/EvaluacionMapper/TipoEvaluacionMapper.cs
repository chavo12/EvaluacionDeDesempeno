using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvaluacionEntidades;

namespace EvaluacionMapper
{

	public class TipoEvaluacionMapper : LBSFramework.ConexionBD.AbstractMapper
    {		

		public override System.Collections.IList GetList()
        {
            return new List<TipoEvaluacion>();
        }

        public override LBSFramework.Entitys.Entity InicializarEntidad()
        {
            return new TipoEvaluacion();
        }

        public override LBSFramework.Entitys.PK Insert(LBSFramework.Entitys.Entity oEntity)
        {
            TipoEvaluacion oTipoEvaluacion = (TipoEvaluacion)oEntity;

            CommandName = "TipoEvaluacionABM";
			AddParameter("@Accion", "A");
            CargarParametros(oTipoEvaluacion);
			if (oTipoEvaluacion.Identificador.ExistePK())
            { 
   			AddParameter("@IdTipoEvaluacion", oTipoEvaluacion.Identificador.valorGetPorNombre("IdTipoEvaluacion" ));
         
            }
            
            return (ExecutePK());
        }

        public override void Update(LBSFramework.Entitys.Entity oEntity)
        {
            TipoEvaluacion oTipoEvaluacion = (TipoEvaluacion)oEntity;

            CommandName = "TipoEvaluacionABM";
			AddParameter("@Accion", "M");
            CargarParametros(oTipoEvaluacion);
						AddParameter("@IdTipoEvaluacion", oTipoEvaluacion.Identificador.valorGetPorNombre("IdTipoEvaluacion" ));

            ExecuteNonQuery();
        }

        public void Delete(LBSFramework.Entitys.Entity oEntity)
        {
            TipoEvaluacion oTipoEvaluacion = (TipoEvaluacion)oEntity;

            CommandName = "TipoEvaluacionABM";
			AddParameter("@Accion", "B");
        
			//Agregar parametro para PK
			AddParameter("@IdTipoEvaluacion", oTipoEvaluacion.Identificador.valorGetPorNombre("IdTipoEvaluacion" ));


			ExecuteNonQuery();
        }

        public override Object retornaEntidad(LBSFramework.Entitys.PK oPK)
        {
            CommandName = "TipoEvaluacionById";
            //Agregar parametro para PK
			AddParameter("@IdTipoEvaluacion", oPK.valorGetPorNombre("IdTipoEvaluacion" ));


            return (TipoEvaluacion)GetOne();
        }

		private void CargarParametros(TipoEvaluacion oTipoEvaluacion)
		{
			AddParameter("@Nombre", oTipoEvaluacion.Nombre );
			AddParameter("@Descripcion", oTipoEvaluacion.Descripcion );
			AddParameter("@Supervisa", oTipoEvaluacion.Supervisa );
			AddParameter("@Idioma", oTipoEvaluacion.Idioma );
		
		}

        public List<TipoEvaluacion> GetEstadoTipoEvaluacion(int IdEvaluacion)
        {
            CommandName = "GetEstadoTipoEvaluacion";
            AddParameter("@IdEvaluacion", IdEvaluacion);
            return (List<TipoEvaluacion>)GetEntityList();
        }


    }
}
