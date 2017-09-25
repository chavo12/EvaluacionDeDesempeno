using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ajaxBorrarEmpleado : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string result;
            int id;
            if (!string.IsNullOrEmpty(Request.Params["idEmpleado"]))
            {
                id = int.Parse(Request.Params["idEmpleado"]);
                EvaluacionBL.EmpleadosBL.borrarEmpleado(id);
                result = "ok";
            }
            else result = "no ok";

            Response.Write(result);

        }
        catch (Exception ex)
        {
            EvaluacionBL.LogsBL.SetLog(0, Request.Url.ToString(), Context.User.Identity.Name);
            Response.Write(ex.Message);
        }
        Response.End();
    }
}