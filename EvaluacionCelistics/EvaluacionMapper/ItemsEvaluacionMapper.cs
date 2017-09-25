using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvaluacionEntidades;

namespace EvaluacionMapper
{

	public class ItemsEvaluacionMapper : LBSFramework.ConexionBD.AbstractMapper
    {		

		public override System.Collections.IList GetList()
        {
            return new List<ItemsEvaluacion>();
        }

        public override LBSFramework.Entitys.Entity InicializarEntidad()
        {
            return new ItemsEvaluacion();
        }

        public override LBSFramework.Entitys.PK Insert(LBSFramework.Entitys.Entity oEntity)
        {
            ItemsEvaluacion oItemsEvaluacion = (ItemsEvaluacion)oEntity;

            CommandName = "ItemsEvaluacionABM";
			AddParameter("@Accion", "A");
            CargarParametros(oItemsEvaluacion);
			if (oItemsEvaluacion.Identificador.ExistePK())
            { 
   			AddParameter("@IdItem", oItemsEvaluacion.Identificador.valorGetPorNombre("IdItem" ));
         
            }
            
            return (ExecutePK());
        }

        public override void Update(LBSFramework.Entitys.Entity oEntity)
        {
            ItemsEvaluacion oItemsEvaluacion = (ItemsEvaluacion)oEntity;

            CommandName = "ItemsEvaluacionABM";
			AddParameter("@Accion", "M");
            CargarParametros(oItemsEvaluacion);
						AddParameter("@IdItem", oItemsEvaluacion.Identificador.valorGetPorNombre("IdItem" ));

            ExecuteNonQuery();
        }

        public void Delete(LBSFramework.Entitys.Entity oEntity)
        {
            ItemsEvaluacion oItemsEvaluacion = (ItemsEvaluacion)oEntity;

            CommandName = "ItemsEvaluacionABM";
			AddParameter("@Accion", "B");
        
			//Agregar parametro para PK
			AddParameter("@IdItem", oItemsEvaluacion.Identificador.valorGetPorNombre("IdItem" ));


			ExecuteNonQuery();
        }

        public override Object retornaEntidad(LBSFramework.Entitys.PK oPK)
        {
            CommandName = "ItemsEvaluacionById";
            //Agregar parametro para PK
			AddParameter("@IdItem", oPK.valorGetPorNombre("IdItem" ));


            return (ItemsEvaluacion)GetOne();
        }

        public void GuardarItems(ItemsEvaluacion oItemsEvaluacion)
        {
            CommandName = "GuardarItems";
            CargarParametros(oItemsEvaluacion);
            ExecuteNonQuery();

        }

		private void CargarParametros(ItemsEvaluacion oItemsEvaluacion)
		{
			AddParameter("@Descripcion", oItemsEvaluacion.Descripcion );
			AddParameter("@idTipoEvaluacion", oItemsEvaluacion.idTipoEvalucion );
			AddParameter("@Nivel", oItemsEvaluacion.Nivel );
			AddParameter("@Escrito", oItemsEvaluacion.Escrito );
			AddParameter("@Idioma", oItemsEvaluacion.Idioma );
		
		}

	}
}
