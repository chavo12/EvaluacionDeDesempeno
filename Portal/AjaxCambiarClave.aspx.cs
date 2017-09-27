using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AjaxCambiarClave : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(Request.Params["empleadoid"]))
            {
                EvaluacionEntidades.Empleados emp = EvaluacionBL.EmpleadosBL.getEmpleado(correoElectronico: Request.Params["empleadoid"]);
                if (string.IsNullOrEmpty(emp.clave) && emp.resetClave.HasValue && emp.resetClave.Value.ToString() == Request.Params["reset"])
                {
                    emp.clave = Request.Params["clave"];
                    emp.resetClave = null;
                    emp.fechaResetClave = null;
                    EvaluacionBL.EmpleadosBL.GuardarEmpleados(emp);
                }
            }
        }
        catch (Exception ex)
        {
            EvaluacionBL.LogsBL.SetLog(0, Request.Url.ToString(), Request.Params["idEmpleado"].ToString() + " - " +  ex.Message);
        }
        
    }
}