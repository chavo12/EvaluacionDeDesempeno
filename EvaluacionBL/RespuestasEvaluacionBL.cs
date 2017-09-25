using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvaluacionEntidades;
using EvaluacionMapper;


namespace EvaluacionBL
{
	public class RespuestasEvaluacionBL
	{
	  
		public static LBSFramework.Entitys.PK Save(RespuestasEvaluacion oRespuestasEvaluacion)
		{
			RespuestasEvaluacionMapper oRespuestasEvaluacionMapper = new RespuestasEvaluacionMapper();

			if (!oRespuestasEvaluacion.Identificador.ExistePK())
				return oRespuestasEvaluacionMapper.Save(oRespuestasEvaluacion,"A");
			else
				return oRespuestasEvaluacionMapper.Save(oRespuestasEvaluacion);
		}

		public static void Delete(RespuestasEvaluacion oRespuestasEvaluacion)
		{
			RespuestasEvaluacionMapper oRespuestasEvaluacionMapper = new RespuestasEvaluacionMapper();
			oRespuestasEvaluacionMapper.Delete(oRespuestasEvaluacion);
		}
	
		public static RespuestasEvaluacion retornarEntidad(LBSFramework.Entitys.PK oPK)
		{
			RespuestasEvaluacionMapper oRespuestasEvaluacionMapper = new RespuestasEvaluacionMapper();
			return (RespuestasEvaluacion)oRespuestasEvaluacionMapper.retornaEntidad(oPK);            
		}

        public static List<RespuestasEvaluacion> GetRespuestasEvaluacion(int idEvaluacion)
        {
            var oMapper = new RespuestasEvaluacionMapper();
            return oMapper.GetRespuestasEvaluacion(idEvaluacion);
        }

        public static void ModificarValorRespuesta(int idRespuesta, string valor = "", string valorSupervisor = "",string escrito = "", string escritoSupervisor = "")
        {
            var oMapper = new RespuestasEvaluacionMapper();
            oMapper.ModificarValorRespuesta(idRespuesta, valor, valorSupervisor,escrito,escritoSupervisor);
        }





    }
}