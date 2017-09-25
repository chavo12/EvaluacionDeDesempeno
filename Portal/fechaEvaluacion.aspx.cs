using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fechaEvaluacion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                int idAdmin = 0;
                if (Session["Admin"] != null && !string.IsNullOrEmpty(Session["Admin"].ToString()))
                {
                    idAdmin = int.Parse(Session["Admin"].ToString());
                }
                EvaluacionEntidades.Empleados admin = EvaluacionBL.EmpleadosBL.getEmpleado(idAdmin);
                if (admin.TipoEmpleado.ToUpper().Contains("ADMIN"))
                {
                    Session["Admin"] = admin.IdEmpleado;
                    EvaluacionEntidades.Fechas fechas = EvaluacionBL.FechasBL.getFechas();
                    Literal1.Text = " value=\"" + fechas.Inicio.ToShortDateString() + "\"";
                        Literal2.Text = " value=\"" + fechas.Fin.ToShortDateString() + "\"";
                        Literal4.Text = " value=\"" + fechas.InicioSuper.ToShortDateString() + "\"";
                        Literal5.Text = " value=\"" + fechas.FinSuper.ToShortDateString() + "\"";
                }
                else
                {
                    HttpContext.Current.Session.Clear();
                    HttpContext.Current.Session.Abandon();
                    ViewState.Clear();
                    System.Web.Security.FormsAuthentication.SignOut();
                    Response.Redirect("/logon.aspx");
                }
            }

        }
        catch (Exception ex)
        {
            Literal1.Text = " value=\"" + DateTime.Today + "\"";
            Literal2.Text = " value=\"" + DateTime.Today + "\"";
            Literal4.Text = " value=\"" + DateTime.Today + "\"";
            Literal5.Text = " value=\"" + DateTime.Today + "\"";
           
        }
    }
}