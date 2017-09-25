using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class finalizar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["mensaje"] != null && !string.IsNullOrEmpty(Session["mensaje"].ToString()))
                {
                    litDetalle.Text = Session["mensaje"].ToString();
                    HttpContext.Current.Session.Clear();
                    HttpContext.Current.Session.Abandon();
                  
                }
                else
                {
                    EvaluacionEntidades.Empleados emp = EvaluacionBL.EmpleadosBL.getEmpleado(correoElectronico:Context.User.Identity.Name);
                    Session["idioma"] = ((emp.Pais.ToUpper() == "BRASIL") ? "portugues" : "spanish");
                    litDetalle.Text = GetGlobalResourceObject(Session["idioma"].ToString(), "finalizar_detalle").ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
}