using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EvaluacionBL;
using EvaluacionEntidades;
public partial class competencia : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        EvaluacionEntidades.Empleados emp = null;
        try
        {
            if (!IsPostBack)
            {
                int i = 1;
                emp = EvaluacionBL.EmpleadosBL.getEmpleado(correoElectronico: Context.User.Identity.Name);
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
                        if (emp.TipoEmpleado.ToUpper().Contains("ADMINISTRADOR")) lectura = true;
                        emp = EvaluacionBL.EmpleadosBL.getEmpleado(idEmpleado: (int)Session["idEmpleado"]);
                        hdSupervisa.Value = "SI";
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
                else hdSupervisa.Value = "NO";
                EvaluacionEntidades.Evaluacion eval = EvaluacionBL.EvaluacionBL.GetEvaluacion(emp.IdEmpleado, desde, hasta, desdeSuper, hastaSuper);
                if (supervisor == null)
                {
                    pnEvaluar.Visible = false;
                    if (eval.Estado != "Autoevaluación" || eval.Inicio > DateTime.Now || eval.Fin < DateTime.Now) lectura = true;
                }
                else
                {
                    if (eval.Estado != "Enviado al Supervisor" || eval.InicioSupervisor > DateTime.Now || eval.FinSupervisor < DateTime.Now) lectura = true;
                }
                List<RespuestasEvaluacion> list = RespuestasEvaluacionBL.GetRespuestasEvaluacion(eval.IdEvaluacion);
                if (emp.Pais.ToUpper() != "BRASIL")
                {
                    litTitulo.Text = GetGlobalResourceObject("spanish", "competencias_Titulo").ToString();
                    var listAux = list.Where(t => t.idTipoEvaluacion != 1 && t.idTipoEvaluacion != 22 && t.idTipoEvaluacion != 23).ToList();
                    foreach (RespuestasEvaluacion r in listAux)
                    {
                        litCompetencia.Text += Helpers.html.armarCompetencia(i, r.TipoEvaluacion, GetGlobalResourceObject("spanish", "competencias_Titulo1").ToString(), r.TipoEvaluacionDescrip, GetGlobalResourceObject("spanish", "competencias_Titulo2").ToString(), listAux.Count(), r.item,((supervisor != null)? "Supervisor: marque la opción correspondiente" : "AutoEvaluación: marque la opción correcta"), r.idRespuesta, "NO", r.Valor, lectura, (supervisor != null), r.ValorSupervisor,emp.EmpleadoId,emp.IdEmpleado);
                        i += 1;
                    }
                }
                else
                {
                    litTitulo.Text = GetGlobalResourceObject("portugues", "competencias_Titulo").ToString();
                    var listAux = list.Where(t => t.idTipoEvaluacion != 1 && t.idTipoEvaluacion != 22 && t.idTipoEvaluacion != 23).ToList();
                    foreach (RespuestasEvaluacion r in listAux)
                    {
                        litCompetencia.Text += Helpers.html.armarCompetenciaPortugues(i, r.TipoEvaluacion, GetGlobalResourceObject("portugues", "competencias_Titulo1").ToString(), r.TipoEvaluacionDescrip, GetGlobalResourceObject("portugues", "competencias_Titulo2").ToString(), listAux.Count(), r.item,((supervisor != null)? "Supervisor: assinale a opção correspondente." : "Autoavaliação: assinale a opção correspondente."), r.idRespuesta, "NO", r.Valor, lectura, (supervisor != null), r.ValorSupervisor,emp.IdEmpleado);
                        i += 1;
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