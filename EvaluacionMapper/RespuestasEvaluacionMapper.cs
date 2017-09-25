using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvaluacionEntidades;

namespace EvaluacionMapper
{

	public class RespuestasEvaluacionMapper : LBSFramework.ConexionBD.AbstractMapper
    {		

		public override System.Collections.IList GetList()
        {
            return new List<RespuestasEvaluacion>();
        }

        public override LBSFramework.Entitys.Entity InicializarEntidad()
        {
            return new RespuestasEvaluacion();
        }

        public override LBSFramework.Entitys.PK Insert(LBSFramework.Entitys.Entity oEntity)
        {
            RespuestasEvaluacion oRespuestasEvaluacion = (RespuestasEvaluacion)oEntity;

            CommandName = "RespuestasEvaluacionABM";
			AddParameter("@Accion", "A");
            CargarParametros(oRespuestasEvaluacion);
			if (oRespuestasEvaluacion.Identificador.ExistePK())
            { 
   			AddParameter("@idRespuesta", oRespuestasEvaluacion.Identificador.valorGetPorNombre("idRespuesta" ));
         
            }
            
            return (ExecutePK());
        }

        public override void Update(LBSFramework.Entitys.Entity oEntity)
        {
            RespuestasEvaluacion oRespuestasEvaluacion = (RespuestasEvaluacion)oEntity;

            CommandName = "RespuestasEvaluacionABM";
			AddParameter("@Accion", "M");
            CargarParametros(oRespuestasEvaluacion);
						AddParameter("@idRespuesta", oRespuestasEvaluacion.Identificador.valorGetPorNombre("idRespuesta" ));

            ExecuteNonQuery();
        }

        public void Delete(LBSFramework.Entitys.Entity oEntity)
        {
            RespuestasEvaluacion oRespuestasEvaluacion = (RespuestasEvaluacion)oEntity;

            CommandName = "RespuestasEvaluacionABM";
			AddParameter("@Accion", "B");
        
			//Agregar parametro para PK
			AddParameter("@idRespuesta", oRespuestasEvaluacion.Identificador.valorGetPorNombre("idRespuesta" ));


			ExecuteNonQuery();
        }

        public override Object retornaEntidad(LBSFramework.Entitys.PK oPK)
        {
            CommandName = "RespuestasEvaluacionById";
            //Agregar parametro para PK
			AddParameter("@idRespuesta", oPK.valorGetPorNombre("idRespuesta" ));


            return (RespuestasEvaluacion)GetOne();
        }

		private void CargarParametros(RespuestasEvaluacion oRespuestasEvaluacion)
		{
			AddParameter("@IdEvaluacion", oRespuestasEvaluacion.IdEvaluacion );
			AddParameter("@IdItem", oRespuestasEvaluacion.IdItem );
			AddParameter("@Valor", oRespuestasEvaluacion.Valor );
			AddParameter("@ValorSupervisor", oRespuestasEvaluacion.ValorSupervisor );
		
		}

        public List<RespuestasEvaluacion> GetRespuestasEvaluacion(int idEvaluacion)
        {
            CommandName = "GetRespuestasEvaluacion";
            AddParameter("@IdEvaluacion", idEvaluacion);
            return (List<RespuestasEvaluacion>)GetEntityList();
        }

        public void ModificarValorRespuesta(int idRespuesta, string valor = "", string valorSupervisor = "",string escrito = "", string escritoSupervisor = "")
        {
            CommandName = "ModificarValorRespuesta";
            AddParameter("@idRespuesta", idRespuesta);
            if (!string.IsNullOrEmpty(valor))
            {
                AddParameter("@valor", valor);
               
            }
            AddParameter("@escrito", escrito);
            if (!string.IsNullOrEmpty(valorSupervisor))
            {
                AddParameter("@valorSupervisor", valorSupervisor);
               
            }
            AddParameter("@escritoSupervisor", escritoSupervisor);

            ExecuteNonQuery();
        }

    }
}
