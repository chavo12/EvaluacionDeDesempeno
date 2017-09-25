using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        EvaluacionEntidades.Empleados emp = null;
        try
        {
            if (!IsPostBack)
            {
                if (Session["ADMIN"] != null && !string.IsNullOrEmpty(Session["ADMIN"].ToString()))
                {
                    EvaluacionBL.LogsBL.SetLog(0, Request.Url.ToString(), Context.User.Identity.Name);
                    Response.Redirect("/homeadmin.aspx?e=" + Session["ADMIN"].ToString(), false);
                }
                else
                {
                    emp = EvaluacionBL.EmpleadosBL.getEmpleado(correoElectronico: Context.User.Identity.Name);
                    if (emp.TipoEmpleado.ToUpper() != "ADMINISTRADOR")
                    {
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
                        EvaluacionEntidades.Empleados supervisor = null;
                        if (!string.IsNullOrEmpty(Request.QueryString["idEmpleado"]))
                        {
                            if (emp.TipoEmpleado.ToUpper().Contains("SUPERVISOR"))
                            {
                                supervisor = emp;

                                emp = EvaluacionBL.EmpleadosBL.getEmpleado(idEmpleado: int.Parse(Request.QueryString["idEmpleado"]));
                                Session["idEmpleado"] = emp.IdEmpleado;

                            }
                            else
                            {
                                HttpContext.Current.Session.Clear();
                                HttpContext.Current.Session.Abandon();
                                ViewState.Clear();
                                System.Web.Security.FormsAuthentication.SignOut();
                                Response.Redirect("/default.aspx");
                            }
                        }
                        else Session["idEmpleado"] = null;

                        if (string.IsNullOrEmpty(Request.QueryString["idEmpleado"]) && emp.TipoEmpleado.ToUpper().Contains("SUPERVISOR"))
                        {
                            pnEmpleados.Visible = true;
                            idsuper.Value = emp.IdEmpleado.ToString();
                            litGrilla.Text = Helpers.html.listEmpleadosSupervisor(EvaluacionBL.EmpleadosBL.GetEmpleadosSupervisados(emp.IdEmpleado), emp.IdEmpleado);
                            int progreso = EvaluacionBL.EmpleadosBL.GetEmpleadosSupervisadosEstado(emp.IdEmpleado);
                            litProgresoSuper.Text = progreso.ToString() + "%";
                            litProgresoSuperwidth.Text = "style=\"width:" + progreso.ToString() + "%;\"";
                        }
                        else
                        {
                            pnEmpleados.Visible = false;
                            if (emp.Pais.ToUpper() != "BRASIL") litdescargar.Text = "<a href=\"#\" onclick=\"javascript:toPdf('" + 0 + "','" + emp.IdEmpleado + "','" + 5 + "','" + 0 + "','" + 1 + "');\" class=\"btn btn-info pull-right\" role=\"button\">Descargar Evaluación</a>";
                            else litdescargar.Text = "<a href=\"#\" onclick=\"javascript:toPdf('" + 0 + "','" + emp.IdEmpleado + "','" + 5 + "','" + 0 + "','" + 1 + "');\" class=\"btn btn-info pull-right\" role=\"button\">Descargar Evaluación</a>";
                        }

                        EvaluacionEntidades.Evaluacion eval = EvaluacionBL.EvaluacionBL.GetEvaluacion(emp.IdEmpleado, desde, hasta, desdeSuper, hastaSuper);
                        if (supervisor == null)
                        {
                            if (eval.Estado.ToUpper() == "AUTOEVALUACIÓN")
                            {
                                pnComenzar.Visible = true;
                                pnFinalizada.Visible = false;
                                pnenviado.Visible = false;
                            }
                            else if (eval.Estado.ToUpper() == "FINALIZADA")
                            {
                                pnComenzar.Visible = false;
                                pnFinalizada.Visible = true;
                                pnenviado.Visible = false;
                                if (emp.Pais.ToUpper() != "BRASIL") litResumen.Text = "<a href=\"#\" onclick=\"javascript:toResumen(" + emp.IdEmpleado + ");\" class=\"btn btn-info pull-right\" role=\"button\">Descargar Resumen</a><a href=\"#\" onclick=\"javascript:toPdf('" + 0 + "','" + emp.IdEmpleado + "','" + 5 + "','" + 0 + "','" + 1 + "');\" class=\"btn btn-info pull-right\" role=\"button\">Descargar Evaluación</a>";
                                else litResumen.Text = "<a href=\"#\" onclick=\"javascript:toResumen(" + emp.IdEmpleado + ");\" class=\"btn btn-info pull-right\" role=\"button\">Descargar Resumen</a><a href=\"#\" onclick=\"javascript:toPdf('" + 0 + "','" + emp.IdEmpleado + "','" + 5 + "','" + 0 + "','" + 1 + "');\" class=\"btn btn-info pull-right\" role=\"button\">Descargar Evaluación</a>";


                            }
                            else if (eval.Estado.ToUpper() == "ENVIADO AL SUPERVISOR")
                            {
                                pnComenzar.Visible = false;
                                litComenzar.Text = "";
                                if (emp.Pais.ToUpper() != "BRASIL") litenviado.Text = "<a href=\"#\" onclick=\"javascript:toPdf('" + 0 + "','" + emp.IdEmpleado + "','" + 5 + "','" + 0 + "','" + 1 + "');\" class=\"btn btn-info pull-right\" role=\"button\">Descargar Evaluación</a>";
                                else litenviado.Text = "<a href=\"#\" onclick=\"javascript:toPdf('" + 0 + "','" + emp.IdEmpleado + "','" + 5 + "','" + 0 + "','" + 1 + "');\" class=\"btn btn-info pull-right\" role=\"button\">Descargar Evaluación</a>";
                            }


                        }
                        else pnFinalizada.Visible = false;
                        var listCompletado = EvaluacionBL.TipoEvaluacionBL.GetEstadoTipoEvaluacion(eval.IdEvaluacion);
                        litResponsabilidadNum.Text = listCompletado[3].completado.ToString() + "%";
                        litResponsabilidadWidth.Text = "style=\"width:" + listCompletado[3].completado.ToString() + "%;\"";
                        litCompetenciaNUm.Text = listCompletado[0].completado.ToString() + "%";
                        litCompetenciaWidth.Text = "style=\"width:" + listCompletado[0].completado.ToString() + "%;\"";
                        litOportunidadNum.Text = listCompletado[2].completado.ToString() + "%";
                        litOportunidadWidth.Text = "style=\"width:" + listCompletado[2].completado.ToString() + "%;\"";
                        litDesempenonum.Text = listCompletado[1].completado.ToString() + "%";
                        litDesempenowidth.Text = "style=\"width:" + listCompletado[1].completado.ToString() + "%;\"";
                        if (emp.Pais.ToUpper() != "BRASIL")
                        {
                            litTitulo.Text = "<h3><p class=\"text-info\">" + GetGlobalResourceObject("spanish", "home_Titulo").ToString() + "</p></h3>";
                            litSubtitulo.Text = "<p class=\"text-info\">Autoevaluación Empleado:<code>" + eval.Inicio.ToShortDateString() + "</code> al <code>" + eval.Fin.Value.ToShortDateString() + "</code> - Evaluacion Supervisor:<code>" + eval.InicioSupervisor.ToShortDateString() + "</code> al <code>" + eval.FinSupervisor.Value.ToShortDateString() + "</code></p>";
                            litNombre.Text = "<p class=\"text-info\"><strong>Nombre:&nbsp;</strong>" + emp.nombreCompleto + "</p>";
                            // litAlcance.Text = "<p class=\"text-info\"><strong>Alcance:&nbsp;</strong>" + emp.Rol + "</p>";
                            litCargo.Text = "<p class=\"text-info\"><strong>Cargo:&nbsp;</strong>" + emp.Cargo + "</p>";
                            litDepartamento.Text = "<p class=\"text-info\"><strong>Departamento:&nbsp;</strong>" + emp.Departamento + "</p>";
                            litIngreso.Text = "<p class=\"text-info\"><strong>Fecha de Ingreso:&nbsp;</strong>" + emp.Ingreso + "</p>";
                            litNegocio.Text = "<p class=\"text-info\"><strong>Negocio:&nbsp;</strong>" + emp.Negocio + "</p>";
                            litNivel.Text = "<p class=\"text-info\"><strong>Nivel:&nbsp;</strong>" + emp.Nivel + "</p>";
                            litPais.Text = "<p class=\"text-info\"><strong>País:&nbsp;</strong>" + emp.Pais + "</p>";
                            litPia.Text = "<p class=\"text-info\"><strong>Id-Pia:&nbsp;</strong>" + emp.NumPia + "</p>";
                            litSupervisor.Text = "<p class=\"text-info\"><strong>Supervisor:&nbsp;</strong>" + emp.supervisor + "</p>";
                            litEstado.Text = "<p class=\"text-info\"><strong>Estado Evaluación:&nbsp;</strong>" + emp.estadoEvaluacion + "</p>";
                            if (supervisor != null) litComenzar.Text = "Evaluar";
                            else litComenzar.Text = "Comenzar evaluación";
                        }
                        else
                        {
                            litResponsabilidades.Text = "Responsabilidades";
                            litCompetencias.Text = "Competencias Core";
                            litOportunidades.Text = "Oportunidades de Melhoria";
                            litDesempeno.Text = "Desempenho global";
                            litTitulo.Text = "<h3><p class=\"text-info\">" + GetGlobalResourceObject("portugues", "home_Titulo").ToString() + "</p></h3>";
                            litSubtitulo.Text = "<p class=\"text-info\">Autoavaliação Funcionário:<code>" + eval.Inicio.ToShortDateString() + "</code> a <code>" + eval.Fin.Value.ToShortDateString() + "</code> - Avaliação supervisor:<code>" + eval.InicioSupervisor.ToShortDateString() + "</code> a <code>" + eval.FinSupervisor.Value.ToShortDateString() + "</code></p>";
                            litNombre.Text = "<p class=\"text-info\"><strong>Nome:&nbsp;</strong>" + emp.nombreCompleto + "</p>";
                            // litAlcance.Text = "<p class=\"text-info\"><strong>Alcance:&nbsp;</strong>" + emp.Rol + "</p>";
                            litCargo.Text = "<p class=\"text-info\"><strong>Cargo:&nbsp;</strong>" + emp.Cargo + "</p>";
                            litDepartamento.Text = "<p class=\"text-info\"><strong>Departamento:&nbsp;</strong>" + emp.Departamento + "</p>";
                            litIngreso.Text = "<p class=\"text-info\"><strong>Data Ingresso:&nbsp;</strong>" + emp.Ingreso + "</p>";
                            litNegocio.Text = "<p class=\"text-info\"><strong>Negócio:&nbsp;</strong>" + emp.Negocio + "</p>";
                            litNivel.Text = "<p class=\"text-info\"><strong>Nível:&nbsp;</strong>" + emp.Nivel + "</p>";
                            litPais.Text = "<p class=\"text-info\"><strong>País:&nbsp;</strong>" + emp.Pais + "</p>";
                            litPia.Text = "<p class=\"text-info\"><strong>Id-Pia:&nbsp;</strong>" + emp.NumPia + "</p>";
                            litSupervisor.Text = "<p class=\"text-info\"><strong>Supervisor:&nbsp;</strong>" + emp.supervisor + "</p>";
                            litEstado.Text = "<p class=\"text-info\"><strong>Avaliação Do Estado:&nbsp;</strong>" + emp.estadoEvaluacion + "</p>";
                            if (supervisor != null) litComenzar.Text = "Avaliar";
                            else litComenzar.Text = "Iniciar a avaliação";

                        }


                    }
                    else
                    {
                        EvaluacionBL.LogsBL.SetLog(2222, Request.Url.ToString(),Context.User.Identity.Name);
                        Response.Redirect("/homeadmin.aspx?er=" + Context.User.Identity.Name, false);
                    }
                }
                    
                }
            }
        catch (Exception ex)
        {
            if (emp == null)
            {
                Session["mensaje"] = "No se requiere que realice la Evaluación de Desempeño para el Ejercicio 2016";
                EvaluacionBL.LogsBL.SetLog(0, Request.Url.ToString(), Context.User.Identity.Name);
            }
            else
            {
                Session["mensaje"] = "Ocurrió un error, vuelva a intentarlo";
                EvaluacionBL.LogsBL.SetLog(emp.IdEmpleado, Request.Url.ToString(), ex.Message);
            }
            
            Response.Redirect("/finalizar.aspx", false);
        }
    
    }
}