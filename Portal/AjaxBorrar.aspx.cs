using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EvaluacionBL;
using EvaluacionEntidades;

public partial class AjaxBorrar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["idEvaluacion"] != null)
        {
            try
            {
                int id;
                string result = "";
                if (int.TryParse(Request.Params["idEvaluacion"], out id))
                {
                    EvaluacionBL.EvaluacionBL.BorrarEvaluacion(id);
                    var list = EmpleadosBL.GetEmpleadoAdmin(int.Parse(Request.Params["idAdmin"]));
                    result = Helpers.html.listEmpleadosAdmin(list);
                }


                Response.Write(result);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Debe completar toda la evalucación antes de finalizarla")
                    Response.Write(ex.Message);
                else Response.Write("no ok");
            }
            Response.End();
        }
    }
}