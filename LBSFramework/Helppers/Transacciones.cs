using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LBSFramework.Helppers
{
    class TransaccionesMapper : LBSFramework.ConexionBD.AbstractMapper
    {

       public override System.Collections.IList GetList()
       {
           throw new NotImplementedException();
       }

       public override Entitys.Entity InicializarEntidad()
       {
           throw new NotImplementedException();
       }

       public override object retornaEntidad(Entitys.PK oPK)
       {
           throw new NotImplementedException();
       }

       public override Entitys.PK Insert(Entitys.Entity oEntity)
       {
           throw new NotImplementedException();
       }

       public override void Update(Entitys.Entity oEntity)
       {
           throw new NotImplementedException();
       }
    }

    public class Transacciones
    {
        public static void Begin()
        {
            TransaccionesMapper oMapper = new TransaccionesMapper();
            oMapper.BeginTransaction();
        }

        public static void Commit()
        {
            TransaccionesMapper oMapper = new TransaccionesMapper();
            oMapper.CommitTransaction();
        }

        public static void Rollback()
        {
            TransaccionesMapper oMapper = new TransaccionesMapper();
            oMapper.RollbackTransaction();
        }
    }
}
