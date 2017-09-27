using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AjaxResetClave : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            EvaluacionEntidades.Empleados emp = EvaluacionBL.EmpleadosBL.getEmpleado(idEmpleado: int.Parse(Request.Params["empleadoid"]));
            emp.clave = null;
                emp.resetClave = Guid.NewGuid();
                emp.fechaResetClave = DateTime.Now;
                EvaluacionBL.EmpleadosBL.GuardarEmpleados(emp);
        }
        catch (Exception ex)
        {

            EvaluacionBL.LogsBL.SetLog(int.Parse(Request.Params["empleadoid"]), Request.Url.ToString(), ex.Message);
        }

    }
}