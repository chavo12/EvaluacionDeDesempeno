using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvaluacionEntidades;
using EvaluacionMapper;


namespace EvaluacionBL
{
	public class LogsBL
	{
	  
		public static LBSFramework.Entitys.PK Save(Logs oLogs)
		{
			LogsMapper oLogsMapper = new LogsMapper();

			if (!oLogs.Identificador.ExistePK())
				return oLogsMapper.Save(oLogs,"A");
			else
				return oLogsMapper.Save(oLogs);
		}

		public static void Delete(Logs oLogs)
		{
			LogsMapper oLogsMapper = new LogsMapper();
			oLogsMapper.Delete(oLogs);
		}
	
		public static Logs retornarEntidad(LBSFramework.Entitys.PK oPK)
		{
			LogsMapper oLogsMapper = new LogsMapper();
			return (Logs)oLogsMapper.retornaEntidad(oPK);            
		}

        public static void SetLog(int IdEmpleado, string pagina, string detalle)
        {
            try
            {
                var oMapper = new LogsMapper();
                oMapper.SetLog(IdEmpleado, pagina, detalle);
            }
            catch (Exception)
            {
            }
         
        }



    }
}