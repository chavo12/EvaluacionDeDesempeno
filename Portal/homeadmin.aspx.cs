using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class homeadmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                int id = 0;
                if (Session["Admin"] != null && !string.IsNullOrEmpty(Session["Admin"].ToString()))
                {
                    id = int.Parse(Session["Admin"].ToString());
                    EvaluacionEntidades.Empleados emp = EvaluacionBL.EmpleadosBL.getEmpleado(id);
                    if (emp.TipoEmpleado.ToUpper().Contains("ADMIN"))
                    {
                        Session["Admin"] = emp.IdEmpleado;
                        idadmin.Value = emp.IdEmpleado.ToString();
                        var list = EvaluacionBL.EmpleadosBL.GetEmpleadoAdmin(emp.IdEmpleado, "");
                        litEstadosBarra.Text = Helpers.html.estadosEvaluacionesBarras(list);
                        litGrilla.Text = Helpers.html.listEmpleadosAdmin(list);
                        var paises = Helpers.funcionesGenerales.listarReguiones2();
                        litPaisoption.Text = "<option value=\"\">Todos</option>";
                        paises.ForEach(i => litPaisoption.Text += "<option value=\"" + i.descripcion + "\">" + i.descripcion + "</option>");
                        var departamentos = Helpers.funcionesGenerales.listarDepartamentos();
                        litDepartamentos.Text = "<option value=\"\">Todos</option>";
                        departamentos.ForEach(d => litDepartamentos.Text += "<option value=\"" + d.descripcion + "\">" + d.descripcion + "</option>");
                    }
                    else
                    {
                        HttpContext.Current.Session.Clear();
                        HttpContext.Current.Session.Abandon();
                        ViewState.Clear();
                        Response.Redirect("/logon.aspx");
                    }
                }
                else
                {
                    HttpContext.Current.Session.Clear();
                    HttpContext.Current.Session.Abandon();
                    ViewState.Clear();
                    Response.Redirect("/logon.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            EvaluacionBL.LogsBL.SetLog(0, Request.Url.ToString(), ex.Message);
        }
    }
}