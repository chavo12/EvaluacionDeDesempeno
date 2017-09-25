using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvaluacionEntidades;

namespace EvaluacionMapper
{

	public class EmpleadosMapper : LBSFramework.ConexionBD.AbstractMapper
    {		

		public override System.Collections.IList GetList()
        {
            return new List<Empleados>();
        }

        public override LBSFramework.Entitys.Entity InicializarEntidad()
        {
            return new Empleados();
        }

        public override LBSFramework.Entitys.PK Insert(LBSFramework.Entitys.Entity oEntity)
        {
            Empleados oEmpleados = (Empleados)oEntity;

            CommandName = "EmpleadosABM";
			AddParameter("@Accion", "A");
            CargarParametros(oEmpleados);
			if (oEmpleados.Identificador.ExistePK())
            { 
   			AddParameter("@IdEmpleado", oEmpleados.Identificador.valorGetPorNombre("IdEmpleado" ));
         
            }
            
            return (ExecutePK());
        }

        public override void Update(LBSFramework.Entitys.Entity oEntity)
        {
            Empleados oEmpleados = (Empleados)oEntity;

            CommandName = "EmpleadosABM";
			AddParameter("@Accion", "M");
            CargarParametros(oEmpleados);
						AddParameter("@IdEmpleado", oEmpleados.Identificador.valorGetPorNombre("IdEmpleado" ));

            ExecuteNonQuery();
        }

        public void Delete(LBSFramework.Entitys.Entity oEntity)
        {
            Empleados oEmpleados = (Empleados)oEntity;

            CommandName = "EmpleadosABM";
			AddParameter("@Accion", "B");
        
			//Agregar parametro para PK
			AddParameter("@IdEmpleado", oEmpleados.Identificador.valorGetPorNombre("IdEmpleado" ));


			ExecuteNonQuery();
        }

        public override Object retornaEntidad(LBSFramework.Entitys.PK oPK)
        {
            CommandName = "EmpleadosById";
            //Agregar parametro para PK
			AddParameter("@IdEmpleado", oPK.valorGetPorNombre("IdEmpleado" ));


            return (Empleados)GetOne();
        }

		private void CargarParametros(Empleados oEmpleados)
		{
            DateTime ingreso;
            
            
			AddParameter("@EmpleadoId", oEmpleados.EmpleadoId );
			AddParameter("@Nombre", oEmpleados.Nombre );
			AddParameter("@PApellido", oEmpleados.PApellido );
			AddParameter("@SApellido", oEmpleados.SApellido );
			AddParameter("@Cargo", oEmpleados.Cargo );
			AddParameter("@Departamento", oEmpleados.Departamento );
			AddParameter("@Negocio", oEmpleados.Negocio );
			AddParameter("@TipoEmpleado", oEmpleados.TipoEmpleado );
			if (DateTime.TryParse(oEmpleados.Ingreso, out ingreso)) AddParameter("@Ingreso", ingreso );
			AddParameter("@Pais", oEmpleados.Pais );
			AddParameter("@CorreoElectronico", oEmpleados.CorreoElectronico );
			AddParameter("@SupervisorID", oEmpleados.SupervisorID );
			AddParameter("@Nivel", oEmpleados.Nivel );
            AddParameter("@rol", oEmpleados.Rol);
            AddParameter("@NumPia", oEmpleados.NumPia);
            AddParameter("@inicio", oEmpleados.inicio);
            AddParameter("@inicioSuper", oEmpleados.inicioSuper);
            AddParameter("@fin", oEmpleados.fin);
            AddParameter("@finSuper", oEmpleados.finSuper);
        }

        public void GuardarEmpleados(Empleados emp)
        {
            CommandName = "GuardarEmpleado";
            if (emp.IdEmpleado != 0) AddParameter("@idEmpleado", emp.IdEmpleado);
            CargarParametros(emp);
            ExecuteNonQuery();
        }

        public Empleados getEmpleado(int? idEmpleado = null, string correoElectronico = "")
        {
            CommandName = "GetEmpleado";
            AddParameter("@IdEmpleado", idEmpleado);
           if(!string.IsNullOrEmpty(correoElectronico)) AddParameter("@correoElectronico", correoElectronico);
            return (Empleados)GetOne();
        }

        public List<Empleados> GetEmpleadoAdmin(int idAdmin, string pais = "",DateTime? inicio = null,DateTime? fin = null, string departamento = "", string estado = "", int? supervisorId = null)
        {
            CommandName = "GetEmpleadoAdmin";
            AddParameter("@IdAdmin", idAdmin);
            if (!string.IsNullOrEmpty(pais)) AddParameter("@Pais", pais);
            if (!string.IsNullOrEmpty(departamento)) AddParameter("@departamento", departamento);
            if (!string.IsNullOrEmpty(estado)) AddParameter("@estado", estado);
            AddParameter("@inicio", inicio);
            AddParameter("@fin", fin);
            AddParameter("@SupervisorId", supervisorId);
            return (List<Empleados>)GetEntityList();
        }

        public List<Empleados> GetEmpleadosSupervisados(int idSUpervisor)
        {
            CommandName = "GetEmpleadosSupervisados";
            AddParameter("@idSupervisor", idSUpervisor);
            return (List<Empleados>)GetEntityList();
        }

        public int GetEmpleadosSupervisadosEstado(int idSUpervisor)
        {
            CommandName = "GetEmpleadosSupervisadosEstado";
            AddParameter("@idSupervisor", idSUpervisor);
            return ExecuteScalar();
        }

        public void borrarEmpleado(int idEmpleado)
        {

            CommandName = "EliminarEmpleado";
            AddParameter("@idEmpleado",idEmpleado);
            ExecuteNonQuery();
        }


    }
}
