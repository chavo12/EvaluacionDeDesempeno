using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvaluacionEntidades;
using LBSFramework.Entitys;

namespace EvaluacionMapper
{
    public class resumenMapper : LBSFramework.ConexionBD.AbstractMapper
    {
        public override IList GetList()
        {
            return new List<resumen>();
        }

        public override Entity InicializarEntidad()
        {
            return new resumen();
        }

        public override PK Insert(Entity oEntity)
        {
            throw new NotImplementedException();
        }

        public override object retornaEntidad(PK oPK)
        {
            throw new NotImplementedException();
        }

        public override void Update(Entity oEntity)
        {
            throw new NotImplementedException();
        }

        public resumen getResumen(int idEmpleado)
        {
            CommandName = "GetResumen";
            AddParameter("@idEmpleado", idEmpleado);

            return (resumen)GetOne();
        }
    }
}
