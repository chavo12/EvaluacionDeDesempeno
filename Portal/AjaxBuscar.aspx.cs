using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AjaxBuscar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
            try
            {
                string result = "";
                DateTime? inicio = null;
                if (!string.IsNullOrEmpty(Request.Params["inicio"])) inicio = DateTime.Parse(Request.Params["inicio"]);
                DateTime? fin = null;
                if (!string.IsNullOrEmpty(Request.Params["fin"])) fin= DateTime.Parse(Request.Params["fin"]);
                int? idSuper = null;
                if (!string.IsNullOrEmpty(Request.Params["supervisorid"])) idSuper = int.Parse(Request.Params["supervisorid"]);
                var list = EvaluacionBL.EmpleadosBL.GetEmpleadoAdmin(int.Parse(Request.Params["idadmin"]), Request.Params["pais"], inicio, fin, Request.Params["departamento"], Request.Params["estado"], idSuper);
                if (list != null && list.Count > 0)
                    result = ((Request.Params["barra"] == "0")?Helpers.html.listEmpleadosAdmin(list):Helpers.html.estadosEvaluacionesBarras(list));
                else result = ((Request.Params["barra"] == "0") ? "<label class=\"text-info\">No se encontraron empleados en la búsqueda realizada</label>":"");
                Response.Write(result);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Debe completar toda la evalucación antes de finalizarla")
                    Response.Write(ex.Message);
                else Response.Write("Ocurrio un error al realizar la búsqueda");
            }
            Response.End();
    }
}