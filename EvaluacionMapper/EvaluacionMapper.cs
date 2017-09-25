using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvaluacionEntidades;

namespace EvaluacionMapper
{

	public class EvaluacionMapper : LBSFramework.ConexionBD.AbstractMapper
    {		

		public override System.Collections.IList GetList()
        {
            return new List<Evaluacion>();
        }

        public override LBSFramework.Entitys.Entity InicializarEntidad()
        {
            return new Evaluacion();
        }

        public override LBSFramework.Entitys.PK Insert(LBSFramework.Entitys.Entity oEntity)
        {
            Evaluacion oEvaluacion = (Evaluacion)oEntity;

            CommandName = "EvaluacionABM";
			AddParameter("@Accion", "A");
            CargarParametros(oEvaluacion);
			if (oEvaluacion.Identificador.ExistePK())
            { 
   			AddParameter("@IdEvaluacion", oEvaluacion.Identificador.valorGetPorNombre("IdEvaluacion" ));
         
            }
            
            return (ExecutePK());
        }

        public override void Update(LBSFramework.Entitys.Entity oEntity)
        {
            Evaluacion oEvaluacion = (Evaluacion)oEntity;

            CommandName = "EvaluacionABM";
			AddParameter("@Accion", "M");
            CargarParametros(oEvaluacion);
						AddParameter("@IdEvaluacion", oEvaluacion.Identificador.valorGetPorNombre("IdEvaluacion" ));

            ExecuteNonQuery();
        }

        public void Delete(LBSFramework.Entitys.Entity oEntity)
        {
            Evaluacion oEvaluacion = (Evaluacion)oEntity;

            CommandName = "EvaluacionABM";
			AddParameter("@Accion", "B");
        
			//Agregar parametro para PK
			AddParameter("@IdEvaluacion", oEvaluacion.Identificador.valorGetPorNombre("IdEvaluacion" ));


			ExecuteNonQuery();
        }

        public override Object retornaEntidad(LBSFramework.Entitys.PK oPK)
        {
            CommandName = "EvaluacionById";
            //Agregar parametro para PK
			AddParameter("@IdEvaluacion", oPK.valorGetPorNombre("IdEvaluacion" ));


            return (Evaluacion)GetOne();
        }

		private void CargarParametros(Evaluacion oEvaluacion)
		{
			AddParameter("@IdEmpleado", oEvaluacion.IdEmpleado );
			AddParameter("@Estado", oEvaluacion.Estado );
			AddParameter("@Inicio", oEvaluacion.Inicio );
			AddParameter("@Fin", oEvaluacion.Fin );
			AddParameter("@FechaEstado", oEvaluacion.FechaEstado );
			AddParameter("@InicioSupervisor", oEvaluacion.InicioSupervisor );
			AddParameter("@FinSupervisor", oEvaluacion.FinSupervisor );
		
		}

        public void FinalizarEvaluacion(int idEvaluacion)
        {
            CommandName = "FinalizarEvaluacion";
            AddParameter("@IdEvaluacion", idEvaluacion);
            ExecuteNonQuery();
        }

        public void FinalizarEvaluacionSupervisor(int idEvaluacion)
        {
            CommandName = "FinalizarEvaluacionSupervisor";
            AddParameter("@IdEvaluacion", idEvaluacion);
            ExecuteNonQuery();
        }

        public Evaluacion GetEvaluacion(int? IdEmpleado, DateTime? inicio, DateTime? fin, DateTime? inicioSupervisor, DateTime? finSupervisor,int? idEvaluacion)
        {
            CommandName = "GetEvaluacion";
            AddParameter("@IdEmpleado", IdEmpleado);
            AddParameter("@inicio", inicio);
            AddParameter("@fin", fin);
            AddParameter("@inicioSupervisor", inicioSupervisor);
            AddParameter("@finSupervisor", finSupervisor);
            AddParameter("@idEvaluacion", idEvaluacion);
            return (Evaluacion)GetOne(); 
        }

        public void BorrarEvaluacion(int idEvaluacion)
        {
            CommandName = "BorrarEvaluacion";
            AddParameter("@idEvaluacion", idEvaluacion);
            ExecuteNonQuery();
        }

        public void EditarEvaluacion(int idEvaluacion)
        {
            CommandName = "EditarEvaluacion";
            AddParameter("@idEvaluacion", idEvaluacion);
            ExecuteNonQuery();
        }

        public void ModificarFechaEvaluacion( DateTime inicio, DateTime fin, DateTime inicioSupervisor, DateTime finSupervisor)
        {
            CommandName = "ModificarFechaEvaluacion";
            AddParameter("@inicio", inicio);
            AddParameter("@fin", fin);
            AddParameter("@inicioSupervisor", inicioSupervisor);
            AddParameter("@finSupervisor", finSupervisor);
            ExecuteNonQuery();
        }





    }
}
