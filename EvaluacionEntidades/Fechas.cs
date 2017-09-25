using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvaluacionEntidades
{
   public class Fechas : LBSFramework.Entitys.Entity
    {

        public DateTime Inicio { get; set; }

        public DateTime Fin { get; set; }

        public DateTime InicioSuper { get; set; }

        public DateTime FinSuper { get; set; }
    }
}
