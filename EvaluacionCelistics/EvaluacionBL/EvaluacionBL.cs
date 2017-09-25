using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvaluacionEntidades;
using EvaluacionMapper;


namespace EvaluacionBL
{
	public class EvaluacionBL
	{
	  
		public static LBSFramework.Entitys.PK Save(Evaluacion oEvaluacion)
		{
			EvaluacionMapper.EvaluacionMapper oEvaluacionMapper = new EvaluacionMapper.EvaluacionMapper();

			if (!oEvaluacion.Identificador.ExistePK())
				return oEvaluacionMapper.Save(oEvaluacion,"A");
			else
				return oEvaluacionMapper.Save(oEvaluacion);
		}

		public static void Delete(Evaluacion oEvaluacion)
		{
            EvaluacionMapper.EvaluacionMapper oEvaluacionMapper = new EvaluacionMapper.EvaluacionMapper();
			oEvaluacionMapper.Delete(oEvaluacion);
		}
	
		public static Evaluacion retornarEntidad(LBSFramework.Entitys.PK oPK)
		{
			EvaluacionMapper.EvaluacionMapper oEvaluacionMapper = new EvaluacionMapper.EvaluacionMapper();
			return (Evaluacion)oEvaluacionMapper.retornaEntidad(oPK);            
		}

        public static void FinalizarEvaluacion(int idEvaluacion)
        {
            var oMapper = new EvaluacionMapper.EvaluacionMapper();
            oMapper.FinalizarEvaluacion(idEvaluacion);
        }

        public static void FinalizarEvaluacionSupervisor(int idEvaluacion)
        {
            var oMapper = new EvaluacionMapper.EvaluacionMapper();
            oMapper.FinalizarEvaluacionSupervisor(idEvaluacion);
        }

        public static Evaluacion GetEvaluacion(int? IdEmpleado = null, DateTime? inicio = null, DateTime? fin = null, DateTime? inicioSupervisor = null, DateTime? finSupervisor = null,int? idEvaluacion = null)
        {
            var oMapper = new EvaluacionMapper.EvaluacionMapper();
            return oMapper.GetEvaluacion(IdEmpleado, inicio, fin, inicioSupervisor, finSupervisor,idEvaluacion);
        }

        public static void BorrarEvaluacion(int idEvaluacion)
        {
            var oMapper = new EvaluacionMapper.EvaluacionMapper();
            oMapper.BorrarEvaluacion(idEvaluacion);
        }

        public static void EditarEvaluacion(int idEvaluacion)
        {
            var oMapper = new EvaluacionMapper.EvaluacionMapper();
            oMapper.EditarEvaluacion(idEvaluacion);
        }

        public static void ModificarFechaEvaluacion(DateTime inicio, DateTime fin, DateTime inicioSupervisor, DateTime finSupervisor)
        {
            var oMapper = new EvaluacionMapper.EvaluacionMapper();
            oMapper.ModificarFechaEvaluacion(inicio, fin, inicioSupervisor, finSupervisor);
        }

    }
}