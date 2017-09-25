using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AjaxGuardarFechaEvaluacion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string result = "";
            DateTime? inicio = null;
            DateTime? fin = null;
            DateTime? inicioSuper = null;
            DateTime? finsSuper = null;
            if (!string.IsNullOrEmpty(Request.Params["inicio"])) inicio = DateTime.Parse(Request.Params["inicio"]);
            if (!string.IsNullOrEmpty(Request.Params["fin"])) fin = DateTime.Parse(Request.Params["fin"]);
            if (!string.IsNullOrEmpty(Request.Params["inicioSuper"])) inicioSuper = DateTime.Parse(Request.Params["inicioSuper"]);
            if (!string.IsNullOrEmpty(Request.Params["finSuper"])) finsSuper = DateTime.Parse(Request.Params["finSuper"]);
            EvaluacionBL.FechasBL.guardarfechas(inicio.Value, fin.Value, inicioSuper.Value, finsSuper.Value);
            EvaluacionBL.EvaluacionBL.ModificarFechaEvaluacion(inicio.Value, fin.Value, inicioSuper.Value, finsSuper.Value);

            Response.Write("ok");
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            EvaluacionBL.LogsBL.SetLog(0, Request.Url.ToString(), ex.Message);
        }
    }
}