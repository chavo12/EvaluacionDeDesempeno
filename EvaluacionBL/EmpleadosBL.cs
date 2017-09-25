using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvaluacionEntidades;
using EvaluacionMapper;


namespace EvaluacionBL
{
	public class EmpleadosBL
	{
	  
		public static LBSFramework.Entitys.PK Save(Empleados oEmpleados)
		{
			EmpleadosMapper oEmpleadosMapper = new EmpleadosMapper();

			if (!oEmpleados.Identificador.ExistePK())
				return oEmpleadosMapper.Save(oEmpleados,"A");
			else
				return oEmpleadosMapper.Save(oEmpleados);
		}

		public static void Delete(Empleados oEmpleados)
		{
			EmpleadosMapper oEmpleadosMapper = new EmpleadosMapper();
			oEmpleadosMapper.Delete(oEmpleados);
		}
	
		public static Empleados retornarEntidad(LBSFramework.Entitys.PK oPK)
		{
			EmpleadosMapper oEmpleadosMapper = new EmpleadosMapper();
			return (Empleados)oEmpleadosMapper.retornaEntidad(oPK);            
		}

        public static Empleados getEmpleado(int? idEmpleado = null, string correoElectronico = "")
        {
            var oMapper = new EmpleadosMapper();
            if (correoElectronico.IndexOf(@"\") > -1)
            {
                int indice = correoElectronico.IndexOf(@"\") + 1;
                correoElectronico = correoElectronico.Substring(indice, correoElectronico.Length - indice);
            }
            return oMapper.getEmpleado(idEmpleado, correoElectronico);

        }

        public static void GuardarEmpleados(Empleados emp)
        {
            var oMapper = new EmpleadosMapper();
            oMapper.GuardarEmpleados(emp);
        }



        public static List<Empleados> GetEmpleadosSupervisados(int idSUpervisor)
        {
            var oMapper = new EmpleadosMapper();
            return oMapper.GetEmpleadosSupervisados(idSUpervisor);
        }

        public static int GetEmpleadosSupervisadosEstado(int idSUpervisor)
        {
            var oMapper = new EmpleadosMapper();
            return oMapper.GetEmpleadosSupervisadosEstado(idSUpervisor);
        }

        public static List<Empleados> GetEmpleadoAdmin(int idAdmin, string pais = "", DateTime? inicio = null, DateTime? fin = null, string departamento = "", string estado = "", int? supervisorId = null)
        {
            var oMapper = new EmpleadosMapper();
            return oMapper.GetEmpleadoAdmin(idAdmin,pais,inicio,fin,departamento,estado,supervisorId);
        }

        public static void borrarEmpleado(int idEmpleado)
        {
            var oMapper = new EmpleadosMapper();
            oMapper.borrarEmpleado(idEmpleado);
        }

    }
}