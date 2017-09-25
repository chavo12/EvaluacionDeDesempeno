using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf.draw;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.xml;
using EvaluacionEntidades;
using EvaluacionBL;
using iTextSharp.tool.xml;
public partial class pdfDesempeno : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            
            int idEmpleado = int.Parse(Request.QueryString["idEmpleado"]);
            int tipo = int.Parse(Request.QueryString["tipo"]);
            int idRespueta = 0;
            int pos = 0;
            bool supervisor = Request.QueryString["supervisor"].ToString() != "0";
            if (tipo != 5)
            {
                idRespueta = int.Parse(Request.QueryString["idRespuesta"]);
                pos = int.Parse(Request.QueryString["pos"]);
            }
            EvaluacionEntidades.Empleados emp = EvaluacionBL.EmpleadosBL.getEmpleado(idEmpleado);
           // if (!string.IsNullOrEmpty(Request.QueryString["pais"])) emp.Pais = Request.QueryString["pais"];
            List<RespuestasEvaluacion> list = RespuestasEvaluacionBL.GetRespuestasEvaluacion(emp.IdEvaluacion);
            if (idRespueta != 0) list = list.Where(r => r.idRespuesta == idRespueta).ToList();
            GenerarPDF(generarRespuesta(list,supervisor,idEmpleado,emp.EmpleadoId,tipo,emp.Pais,pos,emp),emp);
        }
        catch (Exception ex)
        {

            EvaluacionBL.LogsBL.SetLog(0, Request.Url.ToString(),Context.User.Identity.Name + " --- " + ex.Message);
        }
    }

    private string titulo(RespuestasEvaluacion resp)
    {
        string result = "";
        switch (resp.idTipoEvaluacion)
        {
            case 1:
                result = GetGlobalResourceObject("spanish", "responsabilidades_Titulo").ToString();
                break;
            case 22:
                result = GetGlobalResourceObject("spanish", "oportunidad_Titulo").ToString();
                break;
            case 23: 
                result = GetGlobalResourceObject("spanish", "desempeno_Titulo").ToString();
                break;
            default:
                result = GetGlobalResourceObject("spanish", "competencias_Titulo").ToString();
                break;
        }
        return result;
    }

    private string tituloPortugues(RespuestasEvaluacion resp)
    {
        string result = "";
        switch (resp.idTipoEvaluacion)
        {
            case 1:
                result = GetGlobalResourceObject("portugues", "responsabilidades_Titulo").ToString();
                break;
            case 22:
                result = GetGlobalResourceObject("portugues", "oportunidad_Titulo").ToString();
                break;
            case 23:
                result = GetGlobalResourceObject("portugues", "desempeno_Titulo").ToString();
                break;
            default:
                result = GetGlobalResourceObject("portugues", "competencias_Titulo").ToString();
                break;
        }
        return result;
    }

    private string subtitulo(RespuestasEvaluacion resp)
    {
        string result = "";
        switch (resp.idTipoEvaluacion)
        {
            case 1:
                result = "<p>" + GetGlobalResourceObject("spanish", "responsabilidades_detalle").ToString();
                break;
            case 22:
                result = GetGlobalResourceObject("spanish", "oportunidad_detalle").ToString();
                break;
            case 23:
                result = GetGlobalResourceObject("spanish", "desempeno_Titulo1").ToString();
                break;
            default:
                result = GetGlobalResourceObject("spanish", "competencias_Titulo").ToString();
                break;
        }
        return result;
    }

    private string subtituloPortugues(RespuestasEvaluacion resp)
    {
        string result = "";
        switch (resp.idTipoEvaluacion)
        {
            case 1:
                result = "<p>" + GetGlobalResourceObject("portugues", "responsabilidades_detalle").ToString();
                break;
            case 22:
                result = GetGlobalResourceObject("portugues", "oportunidad_detalle").ToString();
                break;
            case 23:
                result = GetGlobalResourceObject("portugues", "desempeno_Titulo1").ToString();
                break;
            default:
                result = GetGlobalResourceObject("portugues", "competencias_Titulo").ToString();
                break;
        }
        return result;
    }

    private string detalle(RespuestasEvaluacion resp)
    {
        
        string result = "";
        switch (resp.idTipoEvaluacion)
        {
            case 1:
                result = GetGlobalResourceObject("spanish", "responsabilidades_Titulo2").ToString();
                break;
            case 22:
                result = GetGlobalResourceObject("spanish", "oportunidad_Titulo").ToString();
                break;
            case 23:
                result = GetGlobalResourceObject("spanish", "desempeno_detalle").ToString();
                break;
            default:
                result = GetGlobalResourceObject("spanish", "competencias_Titulo").ToString();
                break;
        }
        return result;
    }

    private string detallePortugues(RespuestasEvaluacion resp)
    {

        string result = "";
        switch (resp.idTipoEvaluacion)
        {
            case 1:
                result = GetGlobalResourceObject("portugues", "responsabilidades_Titulo2").ToString();
                break;
            case 22:
                result = GetGlobalResourceObject("portugues", "oportunidad_Titulo").ToString();
                break;
            case 23:
                result = GetGlobalResourceObject("portugues", "desempeno_detalle").ToString();
                break;
            default:
                result = GetGlobalResourceObject("portugues", "competencias_Titulo").ToString();
                break;
        }
        return result;
    }

    private string generarRespuesta(List<RespuestasEvaluacion> list, bool supervisor,int idEmpleado, string empleadoID,int tipo,string pais,int pos,Empleados emp)
    {
        string result = "";
        if (tipo == 1) result = generarResponsabilidad(list.Where(r => r.idTipoEvaluacion == 1).ToList(), supervisor, idEmpleado, empleadoID, tipo, pais, pos, emp);
        else if (tipo == 2) result = generarCompetencias(list.Where(r => r.idTipoEvaluacion != 1 && r.idTipoEvaluacion != 22 && r.idTipoEvaluacion != 23).ToList(), supervisor, idEmpleado, empleadoID, tipo, pais, pos, emp);
        else if (tipo == 3) result = generarOportunidades(list.Where(r => r.idTipoEvaluacion == 22).ToList()[0], supervisor, emp.IdEmpleado, emp.EmpleadoId, tipo, emp.Pais, pos, emp);
        else if (tipo == 4) result = generarDesempeno(list.Where(r => r.idTipoEvaluacion == 23).ToList()[0], supervisor, emp.IdEmpleado, emp.EmpleadoId, tipo, emp.Pais, pos, emp);
        else if (tipo == 5) result = generarEvaluacion(list, true, emp.IdEmpleado, emp.EmpleadoId, tipo, emp.Pais, pos, emp);

        return result;
         
    }

    public string generarEvaluacion(List<RespuestasEvaluacion> list, bool supervisor, int idEmpleado, string empleadoID, int tipo, string pais, int pos, Empleados emp)
    {
        string result = "<div>";
        result += "<div class=\"row\">" +
        "<div class=\"col-md-10\">" +
            "<div class=\"panel panel-info\" style=\"border-left-style: solid; border-left-width: 3px; \">" +
                  "          <div class=\"panel-heading\">" +
                      "<label class=\"text-info text-center\">" +
            "<i>Evaluación del empleado <code>" + emp.nombreCompleto + "</code></i></label>" +
                     "</div>" +
            "</div>" +
        "</div>" +
    "</div>";
        result += "<div class=\"row\"> " +
"      <div class=\"col-md-12\"> " +
"         <div class=\"panel panel-info\"> " +
"            <div class=\"panel-body\"> " +
"               <div class=\"row\"> " +
"                   <div class=\"col-md-4\"> " +
"<p class=\"text-info\"><strong>Nombre:&nbsp;</strong>" + emp.nombreCompleto + "</p>" +
"                   </div> " +
"                   <div class=\"col-md-1\"></div> " +
"                   <div class=\"col-md-6\"> " +
"<p class=\"text-info\"><strong>Cargo:&nbsp;</strong>" + emp.Cargo + "</p>" +
"                   </div> " +
"               </div> " +
"              <div class=\"row\"> " +
"                   <div class=\"col-md-4\"> " +
"<p class=\"text-info\"><strong>Departamento:&nbsp;</strong>" + emp.Departamento + "</p>" +
"                   </div> " +
"                   <div class=\"col-md-1\"></div> " +
"                  <div class=\"col-md-6\"> " +
"<p class=\"text-info\"><strong>Fecha de Ingreso:&nbsp;</strong>" + emp.Ingreso + "</p>" +
"                  </div> " +
"               </div> " +
"               <div class=\"row\"> " +
"                  <div class=\"col-md-4\"> " +
"<p class=\"text-info\"><strong>Negocio:&nbsp;</strong>" + emp.Negocio + "</p>" +
"                   </div> " +
"                   <div class=\"col-md-1\"></div> " +
"                   <div class=\"col-md-6\"> " +
"<p class=\"text-info\"><strong>Nivel:&nbsp;</strong>" + emp.Nivel + "</p>" +
"                   </div> " +
"               </div> " +
"               <div class=\"row\"> " +
"                   <div class=\"col-md-4\"> " +
"<p class=\"text-info\"><strong>País:&nbsp;</strong>" + emp.Pais + "</p>" +
"                   </div> " +
"                   <div class=\"col-md-1\"></div> " +
"                  <div class=\"col-md-6\"> " +
"<p class=\"text-info\"><strong>Id-Pia:&nbsp;</strong>" + emp.NumPia + "</p>" +
"                   </div> " +
"               </div> " +
"              <div class=\"row\"> " +
"                   <div class=\"col-md-4\"> " +
"<p class=\"text-info\"><strong>Supervisor:&nbsp;</strong>" + emp.supervisor + "</p>" +
"                   </div> " +
"                   <div class=\"col-md-1\"></div> " +
"                   <div class=\"col-md-6\"> " +
"<p class=\"text-info\"><strong>Estado Evaluación:&nbsp;</strong>" + emp.estadoEvaluacion + "</p>" +
"                   </div> " +
"              </div> " +
"           </div> " +
"       </div> " +
"   </div> " +
"</div> <br/>";
        result += generarResponsabilidad(list.Where(r => r.idTipoEvaluacion == 1).ToList(), supervisor, idEmpleado, empleadoID, tipo, pais, pos, emp);
        result += generarCompetencias(list.Where(r => r.idTipoEvaluacion != 1 && r.idTipoEvaluacion != 22 && r.idTipoEvaluacion != 23).ToList(), supervisor, idEmpleado, empleadoID, tipo, pais, pos, emp);
        result += generarOportunidades(list.Where(r => r.idTipoEvaluacion == 22).ToList()[0], supervisor, emp.IdEmpleado, emp.EmpleadoId, tipo, emp.Pais, pos, emp);
        result += generarDesempeno(list.Where(r => r.idTipoEvaluacion == 23).ToList()[0], supervisor, emp.IdEmpleado, emp.EmpleadoId, tipo, emp.Pais, pos, emp);
        return result + "</div>";
    }

    public string generarResponsabilidad(List<RespuestasEvaluacion> list,bool supervisor, int idEmpleado, string empleadoID, int tipo, string pais,int pos,Empleados emp)
    {
        string result = "<div>" +
            ((tipo != 5)?
        "<div class=\"row\">" +
        "<div class=\"col-md-10\">" +
            "<div class=\"panel panel-info\" style=\"border-left-style: solid; border-left-width: 3px; \">" +
                 "<div class=\"panel-body\">" +
                      "<label class=\"text-info text-center\">" +
            "<i>Evaluación del empleado <code>" + emp.nombreCompleto + "</code></i></label>" +
                     "</div>" +
            "</div>" +
        "</div>" +
    "</div>":"") +
     "<div class=\"row\">" +
         "<div class=\"col-md-10\">" +
             "<h3><p class=\"text-center\">" + titulo(list[0]) + "</p></h3>" +
         "</div>" +
     "</div><br/>" +

     "<div class=\"row\">" +
         "<div class=\"col-md-10\">" +
             "<p class=\"text-info\">" + subtitulo(list[0]) + "</p>" +
         "</div>" +
     "</div>";
        int i = 1;
        foreach (RespuestasEvaluacion resp in list)
        {
            if (pais != "Brasil")
            {
                result += Helpers.html.armarResponsabilidadesPDF("Descripción", ((pos == 0) ? i.ToString() : pos.ToString()) + "- " + resp.item, GetGlobalResourceObject("spanish", "responsabilidades_Titulo2").ToString(), ((pos == 0) ? i : pos), resp.idRespuesta, list.Count, resp.escrito, supervisor, idEmpleado, "spanish", empleadoID);
            }
            else
            {
                result += Helpers.html.armarResponsabilidadesPDF("Descripción", ((pos == 0) ? i.ToString() : pos.ToString()) + "- " + resp.item, GetGlobalResourceObject("portugues", "responsabilidades_Titulo2").ToString(), ((pos == 0) ? i : pos), resp.idRespuesta, list.Count, resp.escrito, supervisor, idEmpleado, "portugues", empleadoID);
            }
            i += 1;
        }
        return result + "</div>";
    }

    public string generarCompetencias(List<RespuestasEvaluacion> list, bool supervisor, int idEmpleado, string empleadoID, int tipo, string pais, int pos, Empleados emp)
    {
        string result = "<div style=\"page-break-before:avoid\">" +
        ((tipo != 5)?"<div class=\"row\">" +
        "<div class=\"col-md-10\">" +
            "<div class=\"panel panel-info\">" +
                 "<div class=\"panel-body\">" +
                      "<label class=\"text-info text-center\">" +
            "<i>Evaluación del empleado <code>" + emp.nombreCompleto + "</code></i></label>" +
                     "</div>" +
            "</div>" +
        "</div>" +
    "</div>" : "<br/>") +
     "<div class=\"row\">" +
         "<div class=\"col-md-11\">" +
             "<h3><p class=\"text-center\">" + titulo(list[0]) + "</p></h3>" +
         "</div>" +
     "</div>" +
     "<br/>";
        int i = 1;
        foreach (RespuestasEvaluacion resp in list)
        {
            if (pais != "Brasil")
            {
                result += Helpers.html.armarCompetenciaPDF(i, resp.TipoEvaluacion, GetGlobalResourceObject("spanish", "competencias_Titulo1").ToString(), resp.TipoEvaluacionDescrip, GetGlobalResourceObject("spanish", "competencias_Titulo2").ToString(), list.Count(), resp.item, ((supervisor) ? "Supervisor: marque la opción correspondiente" : "AutoEvaluación: marque la opción correcta"), resp.idRespuesta, "NO", resp.Valor, true, supervisor, resp.ValorSupervisor, emp.EmpleadoId);
            }
            else
            {
                result += Helpers.html.armarCompetenciaPortuguesPDF(i, resp.TipoEvaluacion, GetGlobalResourceObject("portugues", "competencias_Titulo1").ToString(), resp.TipoEvaluacionDescrip, GetGlobalResourceObject("portugues", "competencias_Titulo2").ToString(), list.Count(), resp.item, ((supervisor) ? "Supervisor: assinale a opção correspondente." : "Autoavaliação: assinale a opção correspondente."), resp.idRespuesta, "NO", resp.Valor, true, supervisor, resp.ValorSupervisor);
            }
            i += 1;
        }
        return result + "</div>";
    }

    public string generarOportunidades(RespuestasEvaluacion resp, bool supervisor, int idEmpleado, string empleadoID, int tipo, string pais, int pos, Empleados emp)
    {
        string result = "<div style=\"page-break-before:avoid\">" +
              ((tipo != 5) ? "<div class=\"row\">" +
        "<div class=\"col-md-10\">" +
            "<div class=\"panel panel-info\">" +
                 "<div class=\"panel-body\">" +
                      "<label class=\"text-info text-center\">" +
            "<i>Evaluación del empleado <code>" + emp.nombreCompleto + "</code></i></label>" +
                     "</div>" +
            "</div>" +
        "</div>" +
    "</div>" : "<br/>") +
     "<div class=\"row\">" +
         "<div class=\"col-md-11\">" +
             "<h3><p class=\"text-center\">" + ((emp.Pais != "Brasil")?titulo(resp):tituloPortugues(resp)) + "</p></h3>" +
         "</div>" +
     "</div>" +
     "<br/>" +
     " <div class=\"row\">" +
        "<div class=\"col-md-10\">" +
            "<h5>" +
                "<label class=\"text-info\">" +
                   ((emp.Pais != "Brasil")?subtitulo(resp):subtituloPortugues(resp)) + "</label></h5>" +
        "</div>" +
    "</div>" +
    "<div class=\"row\">" +
        "<div class=\"col-md-10\">" +
        "<p class=\"navbar-text\">" + resp.escrito + "</p>" +
        "</div>" +
    "</div>";
        
        return result + "</div>";
    }

    public string generarDesempeno(RespuestasEvaluacion resp, bool supervisor, int idEmpleado, string empleadoID, int tipo, string pais, int pos, Empleados emp)
    {
        string result = "<div style=\"page-break-before:avoid\">" +
              ((tipo != 5) ? "<div class=\"row\">" +
        "<div class=\"col-md-10\">" +
            "<div class=\"panel panel-info\">" +
                 "<div class=\"panel-body\">" +
                      "<label class=\"text-info text-center\">" +
            "<i>Evaluación del empleado <code>" + emp.nombreCompleto + "</code></i></label>" +
                     "</div>" +
            "</div>" +
        "</div>" +
    "</div>" : "<br/>") +
     "<div class=\"row\">" +
         "<div class=\"col-md-11\">" +
             "<h3><p class=\"text-center\">" + ((emp.Pais != "Brasil") ? titulo(resp) : tituloPortugues(resp)) + "</p></h3>" +
         "</div>" +
     "</div>" +
     "<br/>" +
     " <div class=\"row\">" +
        "<div class=\"col-md-10\">" +
            "<h5>" +
                "<label class=\"text-info\">" +
                   ((emp.Pais != "Brasil") ? detalle(resp) : detallePortugues(resp)) + "</label></h5>" +
        "</div>" +
    "</div>" +
    "  <div class=\"row\"> " +
"            <div class=\"col-md-10\"> " +
 "               <div class=\"panel panel-info\"> " +
  "                  <div class=\"panel-heading\"> " +
"                        <label class=\"text-info\"> " +
"                            Opciones</label> " +
"                    </div> " +
"                    <div class=\"panel-body\"> " +
"                        <table> " +
"                            <thead> " +
"                                <tr> " +
"                                    <th style=\"width: 15%; text-align:center\">" +
                                        ((resp.Valor == "1") ? "<p class=\"text-info text-center\">SI</p>" : "") +
                                        "</th>" +
"                                    <th style=\"width: 15%; text-align:center\"> " +
((resp.Valor == "2") ? "<p class=\"text-info  text-center\">SI</p>" : "") +
"                                    </th> " +
"                                    <th style=\"width: 15%; text-align:center\"> " +
((resp.Valor == "3") ? "<p class=\"text-info  text-center\">SI</p>" : "") +
"                                    </th> " +
"                                    <th style=\"width: 15%; text-align:center\"> " +
((resp.Valor == "4") ? "<p class=\"text-info  text-center\">SI</p>" : "") +
"                                    </th> " +
"                                    <th style=\"width: 15%; text-align:center\"> " +
((resp.Valor == "5") ? "<p class=\"text-info  text-center\">SI</p>" : "") +
"                                    </th> " +
"                                </tr> " +
"                            </thead> " +
"                            <tr> " +
"                                <td> " +
"                                   <p class=\"text-info  letramuychica  text-center\">" + ((emp.Pais.ToUpper() != "BRASIL") ? "Nivel 1" : "Nível 1") + "</p> " +
"                                </td> " +
"                                <td> " +
"                                   <p class=\"text-info  letramuychica  text-center\">" + ((emp.Pais.ToUpper() != "BRASIL") ? "Nivel 2" : "Nível 2") + "</p> " +
"                                </td> " +
"                                <td> " +
"                                   <p class=\"text-info  letramuychica  text-center\">" + ((emp.Pais.ToUpper() != "BRASIL") ? "Nivel 3" : "Nível 3") + "</p> " +
"                                </td> " +
"                                <td> " +
"                                   <p class=\"text-info  letramuychica  text-center\">" + ((emp.Pais.ToUpper() != "BRASIL") ? "Nivel 4" : "Nível 4") + "</p> " +
"                                </td> " +
"                                <td> " +
"                                   <p class=\"text-info  letramuychica  text-center\">" + ((emp.Pais.ToUpper() != "BRASIL") ? "Nivel 5" : "Nível 5") + "</p> " +
"                                </td> " +
"                            </tr> " +
"                            <tr> " +
"                                <td> " +
"                                   <p class=\"text-info  letramuychica  text-center\">" + ((emp.Pais.ToUpper() != "BRASIL") ? "Insuficiente" : "Insuficiente") + "</p> " +
"                                </td> " +
"                                <td> " +
"                                   <p class=\"text-info  letramuychica  text-center\">" + ((emp.Pais.ToUpper() != "BRASIL") ? "Logro Incompleto" : "Realização  Incompleta") + "</p> " +
"                                </td> " +
"                                <td> " +
"                                   <p class=\"text-info  letramuychica  text-center\">" + ((emp.Pais.ToUpper() != "BRASIL") ? "Logro Alineado" : "Realização Alinhada") + "</p> " +
"                                </td> " +
"                                <td> " +
"                                   <p class=\"text-info  letramuychica  text-center\">" + ((emp.Pais.ToUpper() != "BRASIL") ? "Logro Alto" : "Realização Alta") + "</p> " +
"                                </td> " +
"                                <td> " +
"                                   <p class=\"text-info  letramuychica  text-center\">" + ((emp.Pais.ToUpper() != "BRASIL") ? "Logro Excepcional" : "Realização  Excepcional") + "</p> " +
"                                </td> " +
"                            </tr> " +
"                            <tr> " +
"                                <td> " +
"                                   <p class=\"text-info  letramuychica  text-center\">" + ((emp.Pais.ToUpper() != "BRASIL") ? "El desempeño estuvo notablemente debajo de las expectativas del Grupo Celistics  en todas sus áreas de responsabilidad." : "O desempenho  esteve notavelmente abaixo das expectativas do Grupo Celistics  em todas as suas áreas de responsabilidade.") + "</p> " +
"                                </td> " +
"                                <td> " +
"                                   <p class=\"text-info  letramuychica  text-center\">" + ((emp.Pais.ToUpper() != "BRASIL") ? "El desempeño no alcanzó las expectativas del Grupo Celistics, en una o más áreas  de responsabilidad." : "O desempenho  não atingiu as expectativas do Grupo Celistics, em uma ou mais áreas  de responsabilidade.") + "</p> " +
"                                </td> " +
"                                <td> " +
"                                   <p class=\"text-info  letramuychica  text-center\">" + ((emp.Pais.ToUpper() != "BRASIL") ? "El desempeño se alineó las expectativas del Grupo Celistics respecto a las áreas de responsabilidad  y calidad global del trabajo. Se cumplieron los principales responsabilidades anuales." : "O desempenho  foi alinhado às expectativas do Grupo Celistics a respeito das  áreas de responsabilidade  e qualidade global do trabalho. Foram atingidas as principais responsabilidades anuais.") + "</p> " +
"                                </td> " +
"                                <td> " +
"                                   <p class=\"text-info  letramuychica  text-center\">" + ((emp.Pais.ToUpper() != "BRASIL") ? "El desempeño superó ligeramente las expectativas del Grupo Celistics  en todas las áreas de responsabilidad y la calidad del trabajo fue excelente. Se alcanzaron los responsabilidades anuales, cumpliendo con las competencias Core de la organización." : "O desempenho superou ligeiramente as expectativas do Grupo Celistics  em todas as áreas de responsabilidade e a qualidade do trabalho foi excelente. Foram alcançadas as responsabilidades anuais, cumprindo as competências Core da organização.") + "</p> " +
 "                               </td> " +
 "                               <td> " +
 "                                   <p class=\"text-info  letramuychica  text-center\">" + ((emp.Pais.ToUpper() != "BRASIL") ? "El desempeño supera ampliamente  las expectativas del Grupo Celistics. Se observó una alta calidad de trabajo realizada en todas las áreas de responsabilidad,  resultando un trabajo global supremo. Para la organización significó una contribución excepcional, aportando propuestas de mejoras en los procesos y/o procedimientos a cargo, cumpliendo con las competencias Core de la organización." : "O desempenho supera amplamente  as expectativas do Grupo Celistics. Foi observada uma alta qualidade de trabalho realizada em todas as áreas de responsabilidade,  resultando em um trabalho global supremo. Para a organização significou uma contribuição excepcional, com propostas de melhorias nos processos e/ou procedimentos a seu cargo.") + "</p> " +
 "                               </td> " +
 "                           </tr> " +
 "                       </table> " +
 "                   </div> " +
 "               </div> " +
 "           </div> " +
"        </div> " +
     "<br/>" +
     " <div class=\"row\">" +
        "<div class=\"col-md-10\">" +
            "<h5>" +
                "<label class=\"text-info\">" +
                   ((emp.Pais != "Brasil") ? subtitulo(resp) : subtituloPortugues(resp)) + "</label></h5>" +
        "</div></div>" +
    "<div class=\"row\">" +
        "<div class=\"col-md-10\">" +
        "<p class=\"navbar-text\">" + resp.escrito + "</p>" +
        "</div></div><br/><br/>";
        if (supervisor)
        {
            result += "<div class=\"row\">" +
"            <div class=\"col-md-11\">" +
"                <div class=\"panel panel-danger\">" +
"                    <div class=\"panel-heading\">" +
"                           <label class=\"text-info\">" +
                        ((emp.Pais.ToUpper() != "BRASIL") ? "Evalúe al Empleado de acuerdo a la siguiente escala el desempeño global durante el año 2016. Respalde el mismo con un breve comentario." : "Realize a avaliação de acordo com a seguinte escala de seu desempenho global durante 2016. Respalde o mesmo com um breve comentário.") + "</label>" +
"                    </div>" +
"                    <div class=\"panel-body\">" +
"                        <table style=\"width:100%;\">" +
"                            <thead>" +
"  <tr> " +
 "                                   <th style=\"width: 20%; text-align:center\" class=\"fondoSupervisor\">" +
  ((resp.ValorSupervisor == "1") ? "<p class=\"text-info text-center\">SI</p>" : "") +
  "</th>" +
 "                                   <th style=\"width: 20%; text-align:center\" class=\"fondoSupervisor\">" +
 ((resp.ValorSupervisor == "2") ? "<p class=\"text-info text-center\">SI</p>" : "") +
 "                                   </th>" +
 "                                   <th style=\"width: 20%; text-align:center\" class=\"fondoSupervisor\">" +
 ((resp.ValorSupervisor == "3") ? "<p class=\"text-info text-center\">SI</p>" : "") +
 "                                   </th>" +
 "                                   <th style=\"width: 20%; text-align:center\" class=\"fondoSupervisor\">" +
 ((resp.ValorSupervisor == "4") ? "<p class=\"text-info text-center\">SI</p>" : "") +
 "                                   </th>" +
 "                                   <th style=\"width: 20%; text-align:center\" class=\"fondoSupervisor\">" +
 ((resp.ValorSupervisor == "5") ? "<p class=\"text-info text-center\">SI</p>" : "") +
 "                                   </th>" +
 "                               </tr>" +
            "                            </thead>" +
"                            <tr> " +
"                                <td> " +
"                                   <p class=\"text-info  letramuychica  text-center\">" + ((emp.Pais.ToUpper() != "BRASIL") ? "Insuficiente" : "Insuficiente") + "</p> " +
"                                </td> " +
"                                <td> " +
"                                   <p class=\"text-info  letramuychica  text-center\">" + ((emp.Pais.ToUpper() != "BRASIL") ? "Logro Incompleto" : "Realização  Incompleta") + "</p> " +
"                                </td> " +
"                                <td> " +
"                                   <p class=\"text-info  letramuychica  text-center\">" + ((emp.Pais.ToUpper() != "BRASIL") ? "Logro Alineado" : "Realização Alinhada") + "</p> " +
"                                </td> " +
"                                <td> " +
"                                   <p class=\"text-info  letramuychica  text-center\">" + ((emp.Pais.ToUpper() != "BRASIL") ? "Logro Alto" : "Realização Alta") + "</p> " +
"                                </td> " +
"                                <td> " +
"                                   <p class=\"text-info  letramuychica  text-center\">" + ((emp.Pais.ToUpper() != "BRASIL") ? "Logro Excepcional" : "Realização  Excepcional") + "</p> " +
"                                </td> " +
"                            </tr> " +
"                        </table>" +
"                    </div>" +
"                </div>" +
"            </div>" +
"        </div><br/>" +
            "        <div class=\"row\">" +
            "            <div class=\"col-md-11\">" +
            "                           <label class=\"text-info\"><h5>" +
             ((emp.Pais.ToUpper() != "BRASIL") ? "Comentario del Supervisor" : "Comentário") + "</h5></label>" +
            "                    </div></div>" +
            "                        <div class=\"row\"><div class=\"col-md-10\">" +
                    "<p class=\"navbar-text\">" + ((!string.IsNullOrEmpty(resp.escritoSupervisor))?resp.escritoSupervisor.Split('|')[0]:"") + "</p>" +
            "                            </div></div><br/>" +
            "                        <div class=\"row\">" +
            "                            <div class=\"col-md-10\">" +
            "                                         <label class=\"text-info\"><h5>Foralezas</h5></label>" +
                    "<p class=\"navbar-text\">" + ((!string.IsNullOrEmpty(resp.escritoSupervisor)) ? resp.escritoSupervisor.Split('|')[1] : "") + "</p>" +
            "                                    </div>" +
            "                                </div><br/>" +

            "                              <div class=\"row\"><div class=\"col-md-10\">" +
            "                                         <label class=\"text-info\"><h5>Oportunidades de mejora</h5></label>" +
                    "<p class=\"navbar-text\">" + ((!string.IsNullOrEmpty(resp.escritoSupervisor)) ? resp.escritoSupervisor.Split('|')[2] : "") + "</p>" +
            "                                    </div>" +
            "                                </div>";

        }

        return result + "</div>";
    }



    private void GenerarPDF(string html,Empleados emp)
    {
        Byte[] bytes;

        //Boilerplate iTextSharp setup here
        //Create a stream that we can write to, in this case a MemoryStream
        using (var ms = new MemoryStream())
        {

            //Create an iTextSharp Document which is an abstraction of a PDF but **NOT** a PDF
            using (var doc = new Document(PageSize.A4,25f,10f,10f,10f))
            {

                //Create a writer that's bound to our PDF abstraction and our stream
                using (var writer = PdfWriter.GetInstance(doc, ms))
                {

                    //Open the document for writing
                    doc.Open();

                    //Our sample HTML and CSS
                    var example_html = html;
                    var example_css = "html { font-family: sans-serif;  -webkit-text-size-adjust: 100%;      -ms-text-size-adjust: 100%;} body {  margin: 0;} code {  font-family: monospace, monospace;  font-size: 1em;}  " +
                                        ".label { border: 1px solid #000;  } label {  display: inline-block;  max-width: 100%;  margin-bottom: 1px;  font-weight: bold;} .text-info {  color: #31708f; font-size:10ppt;} " +
                                        ".panel { margin-bottom: 20px; background-color: #fff;  border-style:solid; border-width:4px;}" +
                                        ".panel-body { padding: 3px;} " +
                                        ".panel-heading { padding: 5px 10px; border-bottom: 1px solid transparent; border-top-left-radius: 3px; border-top-right-radius: 3px;}" +
                                        ".panel-info { border-color: #bce8f1;}" +
                                        ".panel-info > .panel-heading {  color: #31708f;  background-color: #d9edf7;  border-color: #bce8f1;}" +
                                        "thead {color:blue;} " +
                                        "tbody { color: blue; }" +
                                        "tfoot { color: blue; } " +
                                        "th, td {background-color:azure; border: 1px solid white;} " +
                                        "th{text-align:center;}" +
                                        "td{padding-left:1 %;}" +
                                        " .letramuychica{font-size:7ppt;}" +
                                        "code {padding: 2px 4px; font-size: 10ppt; color: #c7254e; background-color: #f9f2f4; border-radius: 4px;}" +
                                        "i {font-style: italic;}" +
                                         ".col-xs-1, .col-sm-1, .col-md-1, .col-lg-1, .col-xs-2, .col-sm-2, .col-md-2, .col-lg-2, .col-xs-3, .col-sm-3, .col-md-3, .col-lg-3, .col-xs-4, .col-sm-4, .col-md-4, .col-lg-4, .col-xs-5, .col-sm-5, .col-md-5, .col-lg-5, .col-xs-6, .col-sm-6, .col-md-6, .col-lg-6, .col-xs-7, .col-sm-7, .col-md-7, .col-lg-7, .col-xs-8, .col-sm-8, .col-md-8, .col-lg-8, .col-xs-9, .col-sm-9, .col-md-9, .col-lg-9, .col-xs-10, .col-sm-10, .col-md-10, .col-lg-10, .col-xs-11, .col-sm-11, .col-md-11, .col-lg-11, .col-xs-12, .col-sm-12, .col-md-12, .col-lg-12 {  position: relative;min - height: 1px;padding - right: 15px;padding - left: 15px;}" +
                                        ".col-md-1, .col-md-2, .col-md-3, .col-md-4, .col-md-5, .col-md-6, .col-md-7, .col-md-8, .col-md-9, .col-md-10, .col-md-11, .col-md-12 {float: left;}" +
                                        " .col-md-12 {width: 100%;}" +
                                        ".col-md-11 { width: 91.66666667%;}" +
                                        "  .col-md-10 {width: 83.33333333%;}" +
                                        " .col-md-9 {width: 75%;}" +
                                        " .col-md-8 {width: 66.66666667%;}" +
                                        " .col-md-7 {width: 58.33333333%;}" +
                                        " .col-md-6 {width: 50%;}" +
                                        " .col-md-5 {width: 41.66666667%;}" +
                                        " .col-md-4 {width: 33.33333333%;}" +
                                        " .col-md-3 {width: 25%;}" +
                                        " .col-md-2 {width: 16.66666667%;}" +
                                        ".row {margin-right: -15px; margin-left: -15px;}" +
                                        " .col-md-1 {width: 8.33333333%;}" +
                                        ".h1 {font-size: 36px;}" +
                                        ".fondoSupervisor {background-color:#f2dede; text-align:center;} " +
                                        ".text-center {text-align: center;}" +
                                        "h2,.h2 {font-size: 30px;} h3,.h3 {font-size: 24px;}h4,.h4 {font-size: 18px;}h5,.h5 {font-size: 14px;}h6,.h6 {font-size: 12px;}" ;

                    using (var msCss = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(example_css)))
                    {
                        using (var msHtml = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(example_html)))
                        {
                            if (!doc.IsOpen()) doc.Open();

                            //Parse the HTML
                            writer.Open();
                            writer.CloseStream = false;
                            iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msHtml, msCss);
                            
                        }
                    }


                    doc.Close();
                }
            }


            //After all of the PDF "stuff" above is done and closed but **before** we
            //close the MemoryStream, grab all of the active bytes from the stream
            bytes = ms.ToArray();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment;filename=evaluacion" + emp.nombreCompleto + ".pdf");
            Response.BinaryWrite(bytes);
        }
    }
    

}