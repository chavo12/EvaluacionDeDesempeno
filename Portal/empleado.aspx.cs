using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class empleado : System.Web.UI.Page
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
                    int id = 0;
                    EvaluacionEntidades.Empleados emp = null;
                    if (Request.QueryString["idEmpleado"] != null && int.TryParse(Request.QueryString["idEmpleado"], out id))
                    {
                       
                        emp = EvaluacionBL.EmpleadosBL.getEmpleado(idEmpleado: id);
                        DateTime desde = DateTime.Today;
                        DateTime desdeSuper = DateTime.Today;
                        DateTime hasta = DateTime.Today.AddDays(15);
                        DateTime hastaSuper = DateTime.Today.AddDays(30);
                        try
                        {
                            DateTime.TryParse(System.Configuration.ConfigurationManager.AppSettings["Desde"], out desde);
                            DateTime.TryParse(System.Configuration.ConfigurationManager.AppSettings["DesdeSuper"], out desdeSuper);
                            DateTime.TryParse(System.Configuration.ConfigurationManager.AppSettings["Hasta"], out hasta);
                            DateTime.TryParse(System.Configuration.ConfigurationManager.AppSettings["HastaSuper"], out hastaSuper);
                            if (desde == DateTime.MinValue) desde = DateTime.Today;
                            if (desdeSuper == DateTime.MinValue) desdeSuper = DateTime.Today.AddDays(16);
                            if (hasta == DateTime.MinValue) hasta = DateTime.Today.AddDays(15);
                            if (hastaSuper == DateTime.MinValue) hastaSuper = DateTime.Today.AddDays(46);
                        }
                        catch (Exception ex)
                        { }
                        var eval = EvaluacionBL.EvaluacionBL.GetEvaluacion(IdEmpleado: id, fin: hasta, inicio: desde, inicioSupervisor: desdeSuper, finSupervisor: hastaSuper);
                        pnEvaluacion.Visible = true;
                        litCargo.Text = " value=\"" + emp.Cargo +"\"";
                        litCorreo.Text = " value=\"" + emp.CorreoElectronico + "\"";
                        litDepartamento.Text = " value=\"" + emp.Departamento + "\"";
                        litEmpleadoId.Text = " value=\"" + emp.EmpleadoId + "\"";
                        litIngreso.Text = " value=\"" + emp.Ingreso + "\"";
                        litNegocio.Text = " value=\"" + emp.Negocio + "\"";
                        litNombre.Text = " value=\"" + emp.Nombre + "\"";
                        litPapellido.Text = " value=\"" + emp.PApellido + "\"";
                        litSapellido.Text = " value=\"" + emp.SApellido + "\"";
                        litPia.Text = " value=\"" + emp.NumPia + "\"";
                        Literal1.Text = " value=\"" + eval.Inicio.ToShortDateString() + "\"";
                        Literal2.Text = " value=\"" + eval.Fin.Value.ToShortDateString() + "\"";
                        Literal4.Text = " value=\"" + eval.InicioSupervisor.ToShortDateString() + "\"";
                        Literal5.Text = " value=\"" + eval.FinSupervisor.Value.ToShortDateString() + "\"";
                        hdid.Value = emp.IdEmpleado.ToString();
                        pnResponsabilidades.Visible = false;
                        pnOportunidades.Visible = false;
                    }
                    else
                    {
                        hdid.Value = "0";
                        pnEvaluacion.Visible = false;
                        pnResponsabilidades.Visible = true;
                        pnOportunidades.Visible = true;
                        litGrillaResponsabilidad.Text = "<p class=\"text-info  pull-center\">El empleado no tiene responsabilidades</p>";
                        litGrillaOp.Text = "<p class=\"text-info  pull-center\">El empleado no tiene Competencias</p>";
                    }
                    hdidAdmin.Value = admin.IdEmpleado.ToString();
                    var listEmpleado = EvaluacionBL.EmpleadosBL.GetEmpleadoAdmin(admin.IdEmpleado).ToList().Where(em => em.TipoEmpleado.ToUpper().Contains("SUPERVISOR"));
                    if (listEmpleado != null) listEmpleado.ToList().ForEach(em => litSupervisorOPtion.Text += "<option value=\"" + em.IdEmpleado + "\" " + ((emp != null && em.IdEmpleado == emp.SupervisorID)?" selected=\"selected\"":"") + " >" + em.nombreCompleto + "</option>");
                    string[] niveles = { "Soporte", "Jefes - Gerentes", "Especialistas" };
                    niveles.ToList().ForEach(n => litniveloption.Text += "<option value=\"" + n + "\" " + ((emp != null && n == emp.Nivel) ? " selected =\"selected\"" : "") + " >" + n + "</option>");
                    var paises = Helpers.funcionesGenerales.listarReguiones2();
                    paises.ForEach(i => litPaisoption.Text += "<option value=\"" + i.descripcion + "\" " + ((emp != null && i.descripcion == emp.Pais) ? " selected =\"selected\"" : "") + " >" + i.descripcion + "</option>");
                    littipooption.Text = "<option value=\"AUTOEVALUADO\" " + ((emp != null && emp.TipoEmpleado.ToUpper() == "AUTOEVALUADO") ? " selected =\"selected\"" : "") + " >AUTOEVALUADO</option><option value=\"AUTOEVALUADO-SUPERVISOR\" " + ((emp != null && emp.TipoEmpleado.ToUpper() == "AUTOEVALUADO-SUPERVISOR") ? " selected =\"selected\"" : "") + " >AUTOEVALUADO - SUPERVISOR</option>";



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
        catch (Exception)
        {

            throw;
        }
    }
}