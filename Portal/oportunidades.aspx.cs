using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EvaluacionBL;
using EvaluacionEntidades;
public partial class oportunidades : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        EvaluacionEntidades.Empleados emp = null;
        try
        {
            if (!IsPostBack)
            {
                emp = EvaluacionBL.EmpleadosBL.getEmpleado(correoElectronico:Context.User.Identity.Name);
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
                bool lectura = false;
                if (Session["idEmpleado"] != null && !string.IsNullOrEmpty(Session["idEmpleado"].ToString()))
                {
                    if (emp.TipoEmpleado.ToUpper().Contains("SUPERVISOR") || emp.TipoEmpleado.ToUpper().Contains("ADMINISTRADOR"))
                    {
                        supervisor = emp;
                        lectura = true;
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
                    if (eval.Estado != "Autoevaluación" || eval.Inicio > DateTime.Now || eval.Fin < DateTime.Now) lectura = true;
                }
                
                List<RespuestasEvaluacion> list = RespuestasEvaluacionBL.GetRespuestasEvaluacion(eval.IdEvaluacion).Where(r => r.idTipoEvaluacion == 22).ToList();
                id.Value = list[0].idRespuesta.ToString();
                litEscrito.Text = list[0].escrito;
                if (lectura) litLectura.Text = "disabled";
                else litLectura.Text = "";
                if (emp.Pais.ToUpper() != "BRASIL")
                {
                    litTitulo.Text = GetGlobalResourceObject("spanish", "oportunidad_Titulo").ToString();
                    litSubtitulo.Text = GetGlobalResourceObject("spanish", "oportunidad_detalle").ToString();
                   litImprimir.Text = "<a href=\"#\" onclick=\"javascript:toPdf('" + id.Value + "','" + emp.IdEmpleado.ToString() + "','" + 3 + "','" + 1 + "');\" class=\"btn btn-info pull-right\" role=\"button\">Descargar</a>";
                    if (lectura) litGuardar.Text = "";
                    else litGuardar.Text = "<a href=\"#\" class=\"btn btn-info pull-right\" onclick=\"javascript:var result=responderOportunidad(); if(result)alert('La evaluación fue guardada correctamente');\" role=\"button\">Guardar</a>";
                }
                else
                {
                    litImprimir.Text = "<a href=\"#\" onclick=\"javascript:toPdf('" + id.Value + "','" + emp.IdEmpleado.ToString() + "','" + 3 + "','" + 1 + "');\" class=\"btn btn-info pull-right\" role=\"button\">Imprimir</a>";
                    litTitulo.Text = GetGlobalResourceObject("portugues", "oportunidad_Titulo").ToString();
                    litSubtitulo.Text = GetGlobalResourceObject("portugues", "oportunidad_detalle").ToString();
                    if (lectura) litGuardar.Text = "";
                    else litGuardar.Text = "<a href=\"#\" class=\"btn btn-info pull-right\" onclick=\"javascript:var result=responderOportunidad(); if(result)alert('La evaluación fue guardada correctamente');\" role=\"button\">Salvar</a>";
                    litSiguiente.Text = "Siguente";
                }
               
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