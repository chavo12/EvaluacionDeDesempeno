using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvaluacionBL
{
  public  class FechasBL
    {
        public static EvaluacionEntidades.Fechas getFechas()
        {
            var oMapper = new EvaluacionMapper.fechasMapper();
            return oMapper.getFechas();
        }

        public static void guardarfechas(DateTime inicio, DateTime fin, DateTime inicioSuper, DateTime finSuper)
        {
            var oMapper = new EvaluacionMapper.fechasMapper();
            oMapper.guardarfechas(inicio, fin, inicioSuper, finSuper);
        }
    }
}
