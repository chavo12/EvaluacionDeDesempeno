using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AjaxResponsabilidad : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string result = "error";
            if (!string.IsNullOrEmpty(Request.Params["descrip"]))
            {
                int idResp = 0;
                if (!string.IsNullOrEmpty(Request.Params["idResp"])) idResp = int.Parse(Request.Params["idResp"]);
                if (Session["listResp"] == null) Session["listResp"] = new List<EvaluacionEntidades.ItemsEvaluacion>();
                List<EvaluacionEntidades.ItemsEvaluacion> list = (List<EvaluacionEntidades.ItemsEvaluacion>)Session["listResp"];
                if (idResp == 0)
                {
                    EvaluacionEntidades.ItemsEvaluacion resp = new EvaluacionEntidades.ItemsEvaluacion
                    {
                        Descripcion = Request.Params["descrip"],
                        idTipoEvalucion = 1,
                        IdItem = list.Count + 1
                    };
                    list.Add(resp);
                }
                else
                {
                    list.Where(r => r.IdItem == idResp).ToList()[0].Descripcion = Request.Params["descrip"];
                }
                
                result = Helpers.html.listResponsabilidades(list);
            }

            Response.Write(result);
        }
        catch (Exception ex)
        {
            Response.Write("error");
        }
        Response.End();
    }
}
