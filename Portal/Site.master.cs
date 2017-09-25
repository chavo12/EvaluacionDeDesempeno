using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Site : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!IsPostBack)
            {
                EvaluacionEntidades.Empleados emp = EvaluacionBL.EmpleadosBL.getEmpleado(correoElectronico: Context.User.Identity.Name);
                litSesion.Text = "<span class=\"pull-right\">Bienvenido, " + emp.nombreCompleto + " - </span>";
                if (emp.TipoEmpleado.Contains("ADMINISTRADOR"))
                {
                    litHome.Text = "Home";
                }
                else
                {
                    if (emp.Pais.ToUpper() != "BRASIL")
                    {
                        litHome.Text = "Home";
                        litResponsabilidades.Text = "Responsabilidades";
                        litOportunidades.Text = "Oportunidades";
                        litDesempeño.Text = "Desempeño Global";
                        litCompetencias.Text = "Competencias";
                    }
                    else
                    {
                        litSesion.Text = "<span class=\"pull-right\">bem - vindo, " + emp.nombreCompleto + " - </span>";
                        litHome.Text = "Home";
                        litResponsabilidades.Text = "Responsabilidades";
                        litOportunidades.Text = "Oportunidades";
                        litDesempeño.Text = "Desempenho global";
                        litCompetencias.Text = "Competencias Core";
                    }
                }

            }
           
        }
        catch (Exception ex)
        {
        }
    }



    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
        Session["ADMIN"] = null;
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
            ViewState.Clear();
            Response.Redirect("/logon.aspx");
    }
}

