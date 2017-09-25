using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvaluacionEntidades;
using EvaluacionMapper;


namespace EvaluacionBL
{
	public class ItemsEvaluacionBL
	{
	  
		public static LBSFramework.Entitys.PK Save(ItemsEvaluacion oItemsEvaluacion)
		{
			ItemsEvaluacionMapper oItemsEvaluacionMapper = new ItemsEvaluacionMapper();

			if (!oItemsEvaluacion.Identificador.ExistePK())
				return oItemsEvaluacionMapper.Save(oItemsEvaluacion,"A");
			else
				return oItemsEvaluacionMapper.Save(oItemsEvaluacion);
		}

		public static void Delete(ItemsEvaluacion oItemsEvaluacion)
		{
			ItemsEvaluacionMapper oItemsEvaluacionMapper = new ItemsEvaluacionMapper();
			oItemsEvaluacionMapper.Delete(oItemsEvaluacion);
		}
	
		public static ItemsEvaluacion retornarEntidad(LBSFramework.Entitys.PK oPK)
		{
			ItemsEvaluacionMapper oItemsEvaluacionMapper = new ItemsEvaluacionMapper();
			return (ItemsEvaluacion)oItemsEvaluacionMapper.retornaEntidad(oPK);            
		}

        public static void GuardarResponsabilidad(ItemsEvaluacion oItemsEvaluacion)
        {
            oItemsEvaluacion.idTipoEvalucion = 1;
            oItemsEvaluacion.Escrito = true;
            var oMapper = new ItemsEvaluacionMapper();
            oMapper.GuardarItems(oItemsEvaluacion);
        }

        public static void GuardarCompetencias(ItemsEvaluacion oItemsEvaluacion)
        {
            oItemsEvaluacion.Escrito = false;
            var oMapper = new ItemsEvaluacionMapper();
            oMapper.GuardarItems(oItemsEvaluacion);
        }




    }
}