﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ajaxBorrarOportunidad : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string result = "error";
            if (!string.IsNullOrEmpty(Request.Params["idItem"]))
            {
                int id = int.Parse(Request.Params["idItem"]);
                if (Session["listOp"] == null) Session["listOp"] = new List<EvaluacionEntidades.ItemsEvaluacion>();
                List<EvaluacionEntidades.ItemsEvaluacion> list = (List<EvaluacionEntidades.ItemsEvaluacion>)Session["listOp"];
                var resp = list.Where(r => r.IdItem == id);
                if (resp != null) list.Remove(resp.ToList()[0]);
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