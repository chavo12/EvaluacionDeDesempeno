using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvaluacionEntidades;

namespace EvaluacionBL
{
   public class resumenBL
    {
        public static resumen getResumen(int idEmpleado)
        {
            var oMapper = new EvaluacionMapper.resumenMapper();
            return oMapper.getResumen(idEmpleado);
        }
    }
}
