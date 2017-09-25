using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LBSFramework.Entitys;

namespace EvaluacionMapper
{
    public class fechasMapper : LBSFramework.ConexionBD.AbstractMapper
    {
        public override IList GetList()
        {
            return new List<EvaluacionEntidades.Fechas>();
        }

        public override Entity InicializarEntidad()
        {
            return new EvaluacionEntidades.Fechas();
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

        public EvaluacionEntidades.Fechas getFechas()
        {
            CommandName = "getFechas";
            return (EvaluacionEntidades.Fechas)GetOne();
        }

        public void guardarfechas(DateTime inicio, DateTime fin, DateTime inicioSuper, DateTime finSuper)
        {
            CommandName = "Guardarfechas";
            AddParameter("@inicio", inicio);
            AddParameter("@fin", fin);
            AddParameter("@inicioSuper", inicioSuper);
            AddParameter("@finSuper", finSuper);
            ExecuteNonQuery();
        }
    }
}
