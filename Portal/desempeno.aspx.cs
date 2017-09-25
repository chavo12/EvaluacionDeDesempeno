using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EvaluacionBL;
using EvaluacionEntidades;

public partial class desempeno : System.Web.UI.Page
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
                        if (emp.TipoEmpleado.ToUpper().Contains("ADMINISTRADOR")) lectura = true;
                        emp = EvaluacionBL.EmpleadosBL.getEmpleado(idEmpleado: (int)Session["idEmpleado"]);
                        hdSupervisa.Value = "SI";
                        pnSupervisor.Visible = true;
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
                else
                {
                    hdSupervisa.Value = "NO";
                    pnSupervisor.Visible = false;
                }
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
                List<RespuestasEvaluacion> list = RespuestasEvaluacionBL.GetRespuestasEvaluacion(eval.IdEvaluacion).Where(r => r.idTipoEvaluacion == 23).ToList();
                id.Value = list[0].idRespuesta.ToString();
                idEval.Value = list[0].IdEvaluacion.ToString();
                if (supervisor != null) litOpcionesSuper.Text = Helpers.html.getOpcionesDesempeno(lectura, list[0].ValorSupervisor);
                switch (list[0].Valor)
                {
                    case "1": lit1.Text = "checked";
                        break;
                    case "2": lit2.Text = "checked";
                        break;
                    case "3": lit3.Text = "checked";
                        break;
                    case "4": lit4.Text = "checked";
                        break;
                    case "5": lit5.Text = "checked";
                        break;
                }
                if (lectura|| supervisor != null)
                {
                    lit1.Text += " disabled";
                    lit2.Text += " disabled";
                    lit3.Text += " disabled";
                    lit4.Text += " disabled";
                    lit5.Text += " disabled";
                    litLecturaEscrito.Text += " disabled";
                    litGuardar.Text = "";
                    litFinalizar.Text = "<a href=\"finalizar.aspx\" class=\"btn btn-info pull-right\" onclick=\"javascript: if (confirm('¿Está seguro que desea finalizar la evaluación?')) return responderDesempeno('SI', 'SI'); else return false; \" role=\"button\">" + ((emp.Pais != "Brasil") ? "Finalizar" : "Finalizar") + "</a>";
                    if (lectura)
                    {
                        litFortalezaLectura.Text = " disabled";
                        litOportunidadLectura.Text = " disabled";
                        litEscritoSuperLectura.Text = " disabled";
                        litFinalizar.Text = "";
                    }
                    else
                    {
                        litGuardar.Text = "<a href=\"#\" class=\"btn btn-info pull-right\" onclick=\"javascript:var result=responderDesempeno('SI','NO'); if(result)alert('La evaluación fue guardada correctamente');\" role=\"button\">Guardar</a>";
                    }
                }
                else
                {
                    if (emp.Pais.ToUpper() != "BRASIL") litGuardar.Text = "<a href=\"#\" class=\"btn btn-info pull-right\" onclick=\"javascript:var result=responderDesempeno('NO','NO'); if(result)alert('La evaluación fue guardada correctamente');\" role=\"button\">Guardar</a>";
                    else litGuardar.Text = "<a href=\"#\" class=\"btn btn-info pull-right\" onclick=\"javascript:var result=responderDesempeno('NO','NO'); if(result)alert('La evaluación fue guardada correctamente');\" role=\"button\">Salvar</a>";
                    litFinalizar.Text = "<a href=\"finalizar.aspx\" class=\"btn btn-info pull-right\" onclick=\"javascript: if (confirm('¿Está seguro que desea finalizar la evaluación?')) return responderDesempeno('NO', 'SI'); else return false; \" role=\"button\">" + ((emp.Pais != "Brasil")?"Finalizar":"Finalizar") +"</a>";
                }
                litEscrito.Text = list[0].escrito;
                string[] escritoSuper = ((list[0].escritoSupervisor == null)?"||".Split('|'):list[0].escritoSupervisor.Split('|'));
                litEscritoSuper.Text = escritoSuper[0];
                litFortaleza.Text = escritoSuper[1];
                litOportunidad.Text = escritoSuper[2];


                if (emp.Pais.ToUpper() != "BRASIL")
                {
                    litTitulo.Text = GetGlobalResourceObject("spanish", "desempeno_Titulo").ToString();
                    litSubtitulo.Text = GetGlobalResourceObject("spanish", "desempeno_Titulo1").ToString();
                    litdesemp.Text = GetGlobalResourceObject("spanish", "desempeno_detalle").ToString();
                    Literal1.Text = "Nivel 1";
                    Literal2.Text = "Nivel 2";
                    Literal3.Text = "Nivel 3";
                    Literal4.Text = "Nivel 4";
                    Literal5.Text = "Nivel 5";
                    Literal6.Text = "Insuficiente";
                    Literal7.Text = "Logro Incompleto";
                    Literal8.Text = "Logro Alineado";
                    Literal9.Text = "Logro Alto";
                    Literal10.Text = "Logro Excepcional";
                    Literal11.Text = "El desempeño estuvo notablemente debajo de las expectativas del Grupo Celistics  en todas sus áreas de responsabilidad.";
                    Literal12.Text = "El desempeño no alcanzó las expectativas del Grupo Celistics, en una o más áreas  de responsabilidad.";
                    Literal13.Text = "El desempeño se alineó las expectativas del Grupo Celistics respecto a las áreas de responsabilidad  y calidad global del trabajo. Se cumplieron los principales responsabilidades anuales.";
                    Literal14.Text = "El desempeño superó ligeramente las expectativas del Grupo Celistics  en todas las áreas de responsabilidad y la calidad del trabajo fue excelente. Se alcanzaron los responsabilidades anuales, cumpliendo con las competencias Core de la organización.";
                    Literal15.Text = "El desempeño supera ampliamente  las expectativas del Grupo Celistics. Se observó una alta calidad de trabajo realizada en todas las áreas de responsabilidad,  resultando un trabajo global supremo. Para la organización significó una contribución excepcional, aportando propuestas de mejoras en los procesos y/o procedimientos a cargo, cumpliendo con las competencias Core de la organización.";
                    Literal16.Text = "Insuficiente";
                    Literal17.Text = "Logro Incompleto";
                    Literal18.Text = "Logro Alineado";
                    Literal19.Text = "Logro Alto";
                    Literal20.Text = "Logro Excepcional";
                    litImprimir.Text = "<a href=\"#\" onclick=\"javascript:toPdf('" + id.Value + "','" + emp.IdEmpleado.ToString() + "','" + 4 + "','" + 1 + "','" + ((supervisor != null)?"1":"0") + "');\" class=\"btn btn-info pull-right\" role=\"button\">Descargar</a>";
                }
                else
                {
                    litTitulo.Text = GetGlobalResourceObject("portugues", "desempeno_Titulo").ToString();
                    litSubtitulo.Text = GetGlobalResourceObject("portugues", "desempeno_Titulo1").ToString();
                    litdesemp.Text = GetGlobalResourceObject("portugues", "desempeno_detalle").ToString();
                    Literal1.Text = "Nível 1";
                    Literal2.Text = "Nível 2";
                    Literal3.Text = "Nível 3";
                    Literal4.Text = "Nível 4";
                    Literal5.Text = "Nível 5";
                    Literal6.Text = "Insuficiente";
                    Literal7.Text = "Realização  Incompleta";
                    Literal8.Text = "Realização Alinhada";
                    Literal9.Text = "Realização Alta";
                    Literal10.Text = "Realização  Excepcional";
                    Literal11.Text = "O desempenho  esteve notavelmente abaixo das expectativas do Grupo Celistics  em todas as suas áreas de responsabilidade.";
                    Literal12.Text = "O desempenho  não atingiu as expectativas do Grupo Celistics, em uma ou mais áreas  de responsabilidade.";
                    Literal13.Text = "O desempenho  foi alinhado às expectativas do Grupo Celistics a respeito das  áreas de responsabilidade  e qualidade global do trabalho. Foram atingidas as principais responsabilidades anuais.";
                    Literal14.Text = "O desempenho superou ligeiramente as expectativas do Grupo Celistics  em todas as áreas de responsabilidade e a qualidade do trabalho foi excelente. Foram alcançadas as responsabilidades anuais, cumprindo as competências Core da organização.";
                    Literal15.Text = "O desempenho supera amplamente  as expectativas do Grupo Celistics. Foi observada uma alta qualidade de trabalho realizada em todas as áreas de responsabilidade,  resultando em um trabalho global supremo. Para a organização significou uma contribuição excepcional, com propostas de melhorias nos processos e/ou procedimentos a seu cargo.";
                    litSupervisorTexto.Text = "Realize a avaliação de acordo com a seguinte escala de seu desempenho global durante 2016. Respalde o mesmo com um breve comentário.";
                    Literal16.Text = "Insuficiente";
                    Literal17.Text = "Realização  Incompleta";
                    Literal18.Text = "Realização Alinhada";
                    Literal19.Text = "Realização Alta";
                    Literal20.Text = "Realização  Excepcional";
                    litComentarioSupervisor.Text = "Comentário";
                    Literal21.Text = "Fortalezas:";
                    Literal22.Text = "Oportunidades de melhorias:";
                    litImprimir.Text = "<a href=\"#\" onclick=\"javascript:toPdf('" + id.Value + "','" + emp.IdEmpleado.ToString() + "','" + 4 + "','" + 1 + "','" + ((supervisor != null) ? "1" : "0") + "');\" class=\"btn btn-info pull-right\" role=\"button\">Descargar</a>";
                    if (!lectura && supervisor != null) litGuardar.Text = "<a href=\"#\" class=\"btn btn-info pull-right\" onclick=\"javascript:var result=responderDesempeno('SI','NO'); if(result)alert('La evaluación fue guardada correctamente');\" role=\"button\">Salvar</a>";


                }
            }
        }
        catch (Exception ex)
        {
            if (emp == null)
            {
                Session["mensaje"] = "No se requiere que realice la Evaluación de Desempeño para el Ejercicio 2016";
                EvaluacionBL.LogsBL.SetLog(0, Request.Url.ToString(),Context.User.Identity.Name);
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