using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvaluacionEntidades;

namespace EvaluacionMapper
{

	public class LogsMapper : LBSFramework.ConexionBD.AbstractMapper
    {		

		public override System.Collections.IList GetList()
        {
            return new List<Logs>();
        }

        public override LBSFramework.Entitys.Entity InicializarEntidad()
        {
            return new Logs();
        }

        public override LBSFramework.Entitys.PK Insert(LBSFramework.Entitys.Entity oEntity)
        {
            Logs oLogs = (Logs)oEntity;

            CommandName = "LogsABM";
			AddParameter("@Accion", "A");
            CargarParametros(oLogs);
			if (oLogs.Identificador.ExistePK())
            { 
   			AddParameter("@IdLogs", oLogs.Identificador.valorGetPorNombre("IdLogs" ));
         
            }
            
            return (ExecutePK());
        }

        public override void Update(LBSFramework.Entitys.Entity oEntity)
        {
            Logs oLogs = (Logs)oEntity;

            CommandName = "LogsABM";
			AddParameter("@Accion", "M");
            CargarParametros(oLogs);
						AddParameter("@IdLogs", oLogs.Identificador.valorGetPorNombre("IdLogs" ));

            ExecuteNonQuery();
        }

        public void Delete(LBSFramework.Entitys.Entity oEntity)
        {
            Logs oLogs = (Logs)oEntity;

            CommandName = "LogsABM";
			AddParameter("@Accion", "B");
        
			//Agregar parametro para PK
			AddParameter("@IdLogs", oLogs.Identificador.valorGetPorNombre("IdLogs" ));


			ExecuteNonQuery();
        }

        public override Object retornaEntidad(LBSFramework.Entitys.PK oPK)
        {
            CommandName = "LogsById";
            //Agregar parametro para PK
			AddParameter("@IdLogs", oPK.valorGetPorNombre("IdLogs" ));


            return (Logs)GetOne();
        }

		private void CargarParametros(Logs oLogs)
		{
			AddParameter("@IdEmpleado", oLogs.IdEmpleado );
			AddParameter("@Pagina", oLogs.Pagina );
			AddParameter("@Detalle", oLogs.Detalle );
			AddParameter("@FechayHora", oLogs.FechayHora );
		
		}

        public void SetLog(int IdEmpleado, string pagina, string detalle)
        {
            CommandName = "SetLog";
            AddParameter("@IdEmpleado", IdEmpleado);
            AddParameter("@pagina", pagina);
            AddParameter("@detalle", detalle);
            ExecuteNonQuery();
        }

	}
}
