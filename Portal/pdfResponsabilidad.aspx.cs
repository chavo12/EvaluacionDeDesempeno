using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EvaluacionBL;
using EvaluacionEntidades;
using Helpers;
using SelectPdf;

public partial class pdfResponsabilidad : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        EvaluacionEntidades.Empleados emp = null;
        try
        {
            if (!string.IsNullOrEmpty(Request.QueryString["pdfauth"]) && Request.QueryString["pdfauth"] == "AAFD214E-80E3-440C-853A-8FF1308DDC4E")
            {
                if (!IsPostBack)
                {
                    int i = 1;
                    emp = EvaluacionBL.EmpleadosBL.getEmpleado(correoElectronico: Request.QueryString["userName"]);
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
                    if (Session["idEmpleado"] != null && !string.IsNullOrEmpty(Session["idEmpleado"].ToString()))
                    {
                        if (emp.TipoEmpleado.ToUpper().Contains("SUPERVISOR") || emp.TipoEmpleado.ToUpper().Contains("ADMINISTRADOR"))
                        {
                            supervisor = emp;
                            emp = EvaluacionBL.EmpleadosBL.getEmpleado(idEmpleado: (int)Session["idEmpleado"]);
                            pnEvaluar.Visible = true;
                            litevaluar.Text = "Evaluando al empleado <code>" + emp.nombreCompleto + "</code>";
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
                    EvaluacionEntidades.Evaluacion eval = EvaluacionBL.EvaluacionBL.GetEvaluacion(emp.IdEmpleado, desde, hasta, desdeSuper, hastaSuper);
                    if (supervisor == null)
                    {
                        pnEvaluar.Visible = false;
                        if (eval.Estado != "Autoevaluación" || eval.Inicio > DateTime.Now || eval.Fin < DateTime.Now) supervisor = emp;
                    }
                    List<RespuestasEvaluacion> list = RespuestasEvaluacionBL.GetRespuestasEvaluacion(eval.IdEvaluacion);

                    if (emp.Pais.ToUpper() != "BRASIL")
                    {
                        litTitulo.Text = "<h3><p class=\"text-info\">" + GetGlobalResourceObject("spanish", "responsabilidades_Titulo").ToString() + "</p></h3>";
                        litSubtitulo.Text = "<p class=\"text-info\">" + GetGlobalResourceObject("spanish", "responsabilidades_detalle").ToString() + "</p>";
                        var listAux = list.Where(t => t.idTipoEvaluacion == 1);
                        foreach (RespuestasEvaluacion r in listAux)
                        {
                            litResponsabilidades.Text += html.armarResponsabilidades("Descripción", i.ToString() + "- " + r.item, GetGlobalResourceObject("spanish", "responsabilidades_Titulo2").ToString(), i, r.idRespuesta, listAux.Count(), r.escrito, ((supervisor != null) ? true : false), emp.IdEmpleado, "spanish", Request.QueryString["userName"]);
                            i += 1;
                        }
                    }
                    else
                    {
                        litTitulo.Text = "<h3><p class=\"text-info\">" + GetGlobalResourceObject("portugues", "responsabilidades_Titulo").ToString() + "</p></h3>";
                        litSubtitulo.Text = "<p class=\"text-info\">" + GetGlobalResourceObject("portugues", "responsabilidades_detalle").ToString() + "</p>";
                        var listAux = list.Where(t => t.idTipoEvaluacion == 1);
                        foreach (RespuestasEvaluacion r in listAux)
                        {
                            litResponsabilidades.Text += html.armarResponsabilidades("Descrição", i.ToString() + "- " + r.item, GetGlobalResourceObject("portugues", "responsabilidades_Titulo2").ToString(), i, r.idRespuesta, listAux.Count(), r.escrito, ((supervisor != null) ? true : false), emp.IdEmpleado, "portugues", Request.QueryString["userName"]);
                            i += 1;
                        }
                    }
                }
            }
            else
            {
                Session["mensaje"] = "Ingreso no autorizado";
                Response.Redirect("/finalizar.aspx", false);
            }
        }
        catch (Exception ex)
        {
            if (emp == null)
            {
                Session["mensaje"] = "No se requiere que realice la Evaluación de Desempeño para el Ejercicio 2016";
                EvaluacionBL.LogsBL.SetLog(0, Request.Url.ToString(), ex.Message);
            }
            else
            {
                Session["mensaje"] = "Ocurrió un error, vuelva a intentarlo";
                EvaluacionBL.LogsBL.SetLog(emp.IdEmpleado, Request.Url.ToString(),Context.User.Identity.Name);
            }

            Response.Redirect("/finalizar.aspx", false);
        }
    }
    }