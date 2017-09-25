using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EvaluacionBL;
using EvaluacionEntidades;

public partial class AjaxEditar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["idEvaluacion"] != null)
        {
            try
            {
                int id;
                int idEmpleado = 0;
                int.TryParse(Request.Params["idEmpleado"], out idEmpleado);
                string result = "";
                if (int.TryParse(Request.Params["idEvaluacion"], out id) && idEmpleado != 0)
                {
                    EvaluacionBL.EvaluacionBL.EditarEvaluacion(id);
                    var list = EmpleadosBL.GetEmpleadoAdmin(int.Parse(Request.Params["idAdmin"]), Request.Params["pais"]);
                  
                  //  EnviarMensaje(idEmpleado);
                    result = Helpers.html.listEmpleadosAdmin(list);
                }


                Response.Write(result);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Debe completar toda la evalucación antes de finalizarla")
                    Response.Write(ex.Message);
                else Response.Write("no ok");
                EvaluacionBL.LogsBL.SetLog(0, Request.Url.ToString(),ex.Message);
            }
            Response.End();
        }
    }

    private void EnviarMensaje(int id)
    {
       
            Empleados emp = EmpleadosBL.getEmpleado(id);
        Helpers.funcionesGenerales.enviarMail(emp.IdEvaluacion, Helpers.html.mailAdministrador(emp.IdEvaluacion), "Habilitación de Evaluación", Server.MapPath("/Content"), true);
         
    }
}