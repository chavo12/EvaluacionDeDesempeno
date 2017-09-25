using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ajaxOportunidades : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string result = "error";
            if (!string.IsNullOrEmpty(Request.Params["descrip"]))
            {
                int idResp = 0;
                if (!string.IsNullOrEmpty(Request.Params["idOp"])) idResp = int.Parse(Request.Params["idOp"]);
                if (Session["listOp"] == null) Session["listOp"] = new List<EvaluacionEntidades.ItemsEvaluacion>();
                List<EvaluacionEntidades.ItemsEvaluacion> list = (List<EvaluacionEntidades.ItemsEvaluacion>)Session["listOp"];
                if (idResp == 0)
                {
                    EvaluacionEntidades.ItemsEvaluacion resp = new EvaluacionEntidades.ItemsEvaluacion
                    {
                        Descripcion = Request.Params["descrip"],
                        idTipoEvalucion = int.Parse(Request.Params["idTipo"]),
                        IdItem = list.Count + 1,
                        tipoEvaluacion = Helpers.funcionesGenerales.oportunidadesDescrip(int.Parse(Request.Params["idTipo"]))
                        
                    };
                    list.Add(resp);
                }
                else
                {
                    list.Where(r => r.IdItem == idResp).ToList()[0].Descripcion = Request.Params["descrip"];
                }

                result = Helpers.html.listOportunidades(list);
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