using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["ADMIN"] != null && !string.IsNullOrEmpty(Session["ADMIN"].ToString()))
                {
                    Response.Redirect("/homeadmin.aspx", false);

                }
                else
                {
                    EvaluacionEntidades.Empleados emp = EvaluacionBL.EmpleadosBL.getEmpleado(correoElectronico: Context.User.Identity.Name);
                    if (emp != null && emp.TipoEmpleado.ToUpper() != "ADMINISTRADOR")
                    {
                        Session["idioma"] = ((emp.Pais.ToUpper() == "BRASIL") ? "portugues" : "spanish");
                        litTitulo.Text = "<h3> <p class=\"text-info\">" + GetGlobalResourceObject(Session["idioma"].ToString(), "default_Titulo").ToString() + "</p></h3>";
                        litDetalle.Text = "<p class=\"text-info\">" + GetGlobalResourceObject(Session["idioma"].ToString(), "default_detalle").ToString() + "</p>";
                        if (Session["idioma"].ToString() == "portugues") litIniciar.Text = "começo";
                    }
                    else
                    {
                        Session["mensaje"] = "El usuario ingresado no está habilitado para realizar la evaluación";
                        Response.Redirect("/finalizar.aspx", false);
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
}