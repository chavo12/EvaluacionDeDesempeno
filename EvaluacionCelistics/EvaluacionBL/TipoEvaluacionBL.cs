using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvaluacionEntidades;
using EvaluacionMapper;


namespace EvaluacionBL
{
	public class TipoEvaluacionBL
	{
	  
		public static LBSFramework.Entitys.PK Save(TipoEvaluacion oTipoEvaluacion)
		{
			TipoEvaluacionMapper oTipoEvaluacionMapper = new TipoEvaluacionMapper();

			if (!oTipoEvaluacion.Identificador.ExistePK())
				return oTipoEvaluacionMapper.Save(oTipoEvaluacion,"A");
			else
				return oTipoEvaluacionMapper.Save(oTipoEvaluacion);
		}

		public static void Delete(TipoEvaluacion oTipoEvaluacion)
		{
			TipoEvaluacionMapper oTipoEvaluacionMapper = new TipoEvaluacionMapper();
			oTipoEvaluacionMapper.Delete(oTipoEvaluacion);
		}
	
		public static TipoEvaluacion retornarEntidad(LBSFramework.Entitys.PK oPK)
		{
			TipoEvaluacionMapper oTipoEvaluacionMapper = new TipoEvaluacionMapper();
			return (TipoEvaluacion)oTipoEvaluacionMapper.retornaEntidad(oPK);            
		}

        public static List<TipoEvaluacion> GetEstadoTipoEvaluacion(int IdEvaluacion)
        {
            var oMapper = new TipoEvaluacionMapper();
            return oMapper.GetEstadoTipoEvaluacion(IdEvaluacion);
        }




    }
}