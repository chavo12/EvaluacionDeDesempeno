using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvaluacionEntidades
{
    public class resumen : LBSFramework.Entitys.Entity
    {
        public string Estado { get; set; }

        public int? comunicacion { get; set; }

        public int? comunicacionSuper { get; set; }

        public int? gestion { get; set; }

        public int? gestionSuper { get; set; }

        public int? orientacion { get; set; }

        public int? orientacionSuper { get; set; }

        public int? satifaccion { get; set; }

        public int? satifaccionSuper { get; set; }

        public int? trabajo { get; set; }

        public int? trabajoSuper { get; set; }

        public int? integridad { get; set; }

        public int? integridadSuper { get; set; }

        public int? desarrollo { get; set; }

        public int? desarrolloSuper { get; set; }

        public int? liderazgo { get; set; }

        public int? liderazgoSuper { get; set; }

        public int? vision { get; set; }

        public int? visionSuper { get; set; }

        public int? desempeno { get; set; }

        public int? desempenoSuper { get; set; }


    }
}
