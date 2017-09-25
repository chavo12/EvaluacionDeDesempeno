using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EvaluacionEntidades;
using EvaluacionBL;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf.draw;
using iTextSharp.text.pdf;
using System.IO;

public partial class pdfResumen : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int idEmpleado = int.Parse(Request.QueryString["idEmpleado"]);
            Empleados emp = EmpleadosBL.getEmpleado(idEmpleado);
            resumen resu = resumenBL.getResumen(idEmpleado);
            GenerarPDF(generarResumen(emp, resu),emp);

        }
        catch (Exception ex)
        {

            EvaluacionBL.LogsBL.SetLog(0, Request.Url.ToString(),Context.User.Identity.Name);
        }
    }

    private string generarResumen(Empleados emp, resumen res)
    {
        string result = "";
        decimal divisor = 0;
        if (res.comunicacionSuper.HasValue) divisor += 1;
        if (res.desarrolloSuper.HasValue) divisor += 1;
        if (res.gestionSuper.HasValue) divisor += 1;
        if (res.integridadSuper.HasValue) divisor += 1;
        if (res.liderazgoSuper.HasValue) divisor += 1;
        if (res.orientacionSuper.HasValue) divisor += 1;
        if (res.satifaccionSuper.HasValue) divisor += 1;
        if (res.trabajoSuper.HasValue) divisor += 1;
        if (res.visionSuper.HasValue) divisor += 1;
        string promedio = "";
        if (res.desempenoSuper.HasValue)
        {
            promedio = (Decimal.Round(((((res.visionSuper.HasValue) ? res.visionSuper.Value : 0) +
                                    ((res.trabajoSuper.HasValue) ? res.trabajoSuper.Value : 0) +
                                    ((res.satifaccionSuper.HasValue) ? res.satifaccionSuper.Value : 0) +
                                    ((res.orientacionSuper.HasValue) ? res.orientacionSuper.Value : 0) +
                ((res.liderazgoSuper.HasValue) ? res.liderazgoSuper.Value : 0) +
                ((res.integridadSuper.HasValue) ? res.integridadSuper.Value : 0) +
                ((res.gestionSuper.HasValue) ? res.gestionSuper.Value : 0) +
                ((res.desarrolloSuper.HasValue) ? res.desarrolloSuper.Value : 0) +
                ((res.comunicacionSuper.HasValue) ? res.comunicacionSuper.Value : 0)) / divisor),0).ToString() + res.desempenoSuper.Value.ToString());
        }

                             result = "<div>" +
             "<div class=\"row\">" +
        "<div class=\"col-md-12\">" +
            "<div class=\"panel panel-info\">" +
                 "<div class=\"panel-body\">" +
                      "<label class=\"text-info text-center\">" +
            "<i>Evaluación del empleado <code>" + emp.nombreCompleto + "</code></i></label>" +
                             "</div>" +
                    "</div>" +
                "</div>" +
            "</div>" +
             "<div class=\"row\"> " +
"      <div class=\"col-md-12\"> " +
"         <div class=\"panel panel-info\"> " +
"            <div class=\"panel-body\"> " +
"               <div class=\"row\"> " +
"                   <div class=\"col-md-4\"> " +
"<p class=\"text-info\"><strong>Nombre:&nbsp;</strong>" + emp.nombreCompleto + "</p>" +
"                   </div> " +
"                   <div class=\"col-md-1\"></div> " +
"                   <div class=\"col-md-7\"> " +
"<p class=\"text-info\"><strong>Cargo:&nbsp;</strong>" + emp.Cargo + "</p>" +
"                   </div> " +
"               </div> " +
"              <div class=\"row\"> " +
"                   <div class=\"col-md-4\"> " +
"<p class=\"text-info\"><strong>Departamento:&nbsp;</strong>" + emp.Departamento + "</p>" +
"                   </div> " +
"                   <div class=\"col-md-1\"></div> " +
"                  <div class=\"col-md-7\"> " +
"<p class=\"text-info\"><strong>Fecha de Ingreso:&nbsp;</strong>" + emp.Ingreso + "</p>" +
"                  </div> " +
"               </div> " +
"               <div class=\"row\"> " +
"                  <div class=\"col-md-4\"> " +
"<p class=\"text-info\"><strong>Negocio:&nbsp;</strong>" + emp.Negocio + "</p>" +
"                   </div> " +
"                   <div class=\"col-md-1\"></div> " +
"                   <div class=\"col-md-7\"> " +
"<p class=\"text-info\"><strong>Nivel:&nbsp;</strong>" + emp.Nivel + "</p>" +
"                   </div> " +
"               </div> " +
"               <div class=\"row\"> " +
"                   <div class=\"col-md-4\"> " +
"<p class=\"text-info\"><strong>País:&nbsp;</strong>" + emp.Pais + "</p>" +
"                   </div> " +
"                   <div class=\"col-md-1\"></div> " +
"                  <div class=\"col-md-7\"> " +
"<p class=\"text-info\"><strong>Id-Pia:&nbsp;</strong>" + emp.NumPia + "</p>" +
"                   </div> " +
"               </div> " +
"              <div class=\"row\"> " +
"                   <div class=\"col-md-4\"> " +
"<p class=\"text-info\"><strong>Supervisor:&nbsp;</strong>" + emp.supervisor + "</p>" +
"                   </div> " +
"                   <div class=\"col-md-1\"></div> " +
"                   <div class=\"col-md-7\"> " +
"<p class=\"text-info\"><strong>Estado Evaluación:&nbsp;</strong>" + emp.estadoEvaluacion + "</p>" +
"                   </div> " +
"              </div> " +
"           </div> " +
"       </div> " +
"   </div> " +
"</div> <br/>" + 
        "<div class=\"row\">" +
         "<div class=\"col-md-12\">" +
             "<h4><p class=\"text-info\">" + "Resumen de la Evaluación de las Competencias Autoevaluado / Supervisor" + "</p></h4>" +
         "</div>" +
         //"<div class=\"col-md-3\"></div>" +
         "<br/><br/>" +
     "</div>" +
       "<div class=\"row\">" +
       "<div class=\"col-md-1\"></div>" +
       "<div class=\"col-md-11\">" +
      "   <div class=\"panel panel-info\" style=\"border: 1px solid;\">" +
     "               <div class=\"panel-heading\">" +
      "                <p class=\"text-info letrachica\">" +
              "Resumen de Competencias:</p>" +
     "</div></div>" +
       "</div>"+
     
       "</div><br/>" +
        "<div class=\"row\">" +
        "<div class=\"col-md-1\"></div>" +
            "<div class=\"col-md-10\">" +
                "<table>" +
                "<tr><td class=\"encabezado\"><p class=\"text-info letramuychica\">Competencia</p></td><td style=\"width:5px;\" class=\"encabezado\"><p class=\"text-info letramuychica\">1</p></td><td style=\"width:5px;\" class=\"encabezado\"><p class=\"text-info letramuychica\">2</p></td>" +
                "<td style=\"width:5px;\" class=\"encabezado\"><p class=\"text-info letramuychica\">3</p></td><td style=\"width:5px;\" class=\"encabezado\"><p class=\"text-info letramuychica\">4</p></td><td style=\"width:5px;\" class=\"encabezado\"><p class=\"text-info letramuychica\">5</p></td></tr>" +
                ((res.comunicacion.HasValue) ? "<tr><td rowspan=\"2\"><p class=\"text-info letramuychica\">Comunicación</p></td><td width=\"20px\" colspan=\"5\">" +
                 "<div class=\"progress\" style=\"height:7px;\">" +
            "<div class=\"progress-bar progress-bar-success\" role=\"progressbar\"  aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width:" + porcentaje(res.comunicacion.Value) + "; height:7px;\">" +
            "</div></div></td></tr>" +
            "<tr><td colspan=\"5\">" +
             "<div class=\"progress\" style=\"height:7px;\">" +
            "<div class=\"progress-bar progress-bar-info\" role=\"progressbar\"  aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width:" + porcentaje(res.comunicacionSuper.Value) + "; height:7px;\">" +
             "</div></div>" +
                "</td></tr>" : "") +
                   ((res.gestion.HasValue) ? "<tr><td rowspan=\"2\"><p class=\"text-info letramuychica\">Gestión del Cambio</p></td><td width=\"20px\" colspan=\"5\">" +
                 "<div class=\"progress\" style=\"height:7px;\">" +
            "<div class=\"progress-bar progress-bar-success\" role=\"progressbar\"  aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width:" + porcentaje(res.gestion.Value) + "; height:7px;\">" +
            "</div></div></td></tr>" +
            "<tr><td width=\"20\" colspan=\"5\">" +
             "<div class=\"progress\" style=\"height:7px;\">" +
            "<div class=\"progress-bar progress-bar-info\" role=\"progressbar\"  aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width:" + porcentaje(res.gestionSuper.Value) + "; height:7px;\">" +
             "</div></div>" +
                "</td></tr>" : "") +
                   ((res.orientacion.HasValue) ? "<tr><td rowspan=\"2\"><p class=\"text-info letramuychica\">Orientación a los Resultados</p></td><td width=\"20px\" colspan=\"5\">" +
                 "<div class=\"progress\" style=\"height:7px;\">" +
            "<div class=\"progress-bar progress-bar-success\" role=\"progressbar\"  aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width:" + porcentaje(res.orientacion.Value) + "; height:7px;\">" +
            "</div></div></td></tr>" +
            "<tr><td width=\"20\" colspan=\"5\">" +
             "<div class=\"progress\" style=\"height:7px;\">" +
            "<div class=\"progress-bar progress-bar-info\" role=\"progressbar\"  aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width:" + porcentaje(res.orientacionSuper.Value) + "; height:7px;\">" +
             "</div></div>" +
                "</td></tr>" : "") +
                   ((res.satifaccion.HasValue) ? "<tr><td rowspan=\"2\"><p class=\"text-info letramuychica\">Satisfacción al Cliente Interno / Externo</p></td><td width=\"20px\" colspan=\"5\">" +
                 "<div class=\"progress\" style=\"height:7px;\">" +
            "<div class=\"progress-bar progress-bar-success\" role=\"progressbar\"  aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width:" + porcentaje(res.satifaccion.Value) + "; height:7px;\">" +
            "</div></div></td></tr>" +
            "<tr><td width=\"20\" colspan=\"5\">" +
             "<div class=\"progress\" style=\"height:7px;\">" +
            "<div class=\"progress-bar progress-bar-info\" role=\"progressbar\"  aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width:" + porcentaje(res.satifaccionSuper.Value) + "; height:7px;\">" +
             "</div></div>" +
                "</td></tr>" : "") +
                   ((res.trabajo.HasValue) ? "<tr><td rowspan=\"2\"><p class=\"text-info letramuychica\">Trabajo en Equipo</p></td><td width=\"20px\" colspan=\"5\">" +
                 "<div class=\"progress\" style=\"height:7px;\">" +
            "<div class=\"progress-bar progress-bar-success\" role=\"progressbar\"  aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width:" + porcentaje(res.trabajo.Value) + "; height:7px;\">" +
            "</div></div></td></tr>" +
            "<tr><td width=\"20\" colspan=\"5\">" +
             "<div class=\"progress\" style=\"height:7px;\">" +
            "<div class=\"progress-bar progress-bar-info\" role=\"progressbar\"  aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width:" + porcentaje(res.trabajoSuper.Value) + "; height:7px;\">" +
             "</div></div>" +
                "</td></tr>" : "") +
                   ((res.integridad.HasValue) ? "<tr><td rowspan=\"2\"><p class=\"text-info letramuychica\">Integridad</p></td><td width=\"20px\" colspan=\"5\">" +
                 "<div class=\"progress\" style=\"height:7px;\">" +
            "<div class=\"progress-bar progress-bar-success\" role=\"progressbar\"  aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width:" + porcentaje(res.integridad.Value) + "; height:7px;\">" +
            "</div></div></td></tr>" +
            "<tr><td width=\"20\" colspan=\"5\">" +
             "<div class=\"progress\" style=\"height:7px;\">" +
            "<div class=\"progress-bar progress-bar-info\" role=\"progressbar\"  aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width:" + porcentaje(res.integridadSuper.Value) + "; height:7px;\">" +
             "</div></div>" +
                "</td></tr>" : "") +
                   ((res.desarrollo.HasValue) ? "<tr><td rowspan=\"2\"><p class=\"text-info letramuychica\">Desarrollo de Personas</p></td><td width=\"20px\" colspan=\"5\">" +
                 "<div class=\"progress\" style=\"height:7px;\">" +
            "<div class=\"progress-bar progress-bar-success\" role=\"progressbar\"  aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width:" + porcentaje(res.desarrollo.Value) + "; height:7px;\">" +
            "</div></div></td></tr>" +
            "<tr><td width=\"20\" colspan=\"5\">" +
             "<div class=\"progress\" style=\"height:7px;\">" +
            "<div class=\"progress-bar progress-bar-info\" role=\"progressbar\"  aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width:" + porcentaje(res.desarrolloSuper.Value) + "; height:7px;\">" +
             "</div></div>" +
                "</td></tr>" : "") +
                   ((res.liderazgo.HasValue) ? "<tr><td rowspan=\"2\"><p class=\"text-info letramuychica\">Liderazgo</p></td><td width=\"20px\" colspan=\"5\">" +
                 "<div class=\"progress\" style=\"height:7px;\">" +
            "<div class=\"progress-bar progress-bar-success\" role=\"progressbar\"  aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width:" + porcentaje(res.liderazgo.Value) + "; height:7px;\">" +
            "</div></div></td></tr>" +
            "<tr><td width=\"20\" colspan=\"5\">" +
             "<div class=\"progress\" style=\"height:7px;\">" +
            "<div class=\"progress-bar progress-bar-info\" role=\"progressbar\"  aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width:" + porcentaje(res.liderazgoSuper.Value) + "; height:7px;\">" +
             "</div></div>" +
                "</td></tr>" : "") +
                   ((res.vision.HasValue) ? "<tr><td rowspan=\"2\"><p class=\"text-info letramuychica\">Visión estratégica del Negocio</p></td><td width=\"20px\" colspan=\"5\">" +
                 "<div class=\"progress\" style=\"height:7px;\">" +
            "<div class=\"progress-bar progress-bar-success\" role=\"progressbar\"  aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width:" + porcentaje(res.vision.Value) + "; height:7px;\">" +
            "</div></div></td></tr>" +
            "<tr><td width=\"20\" colspan=\"5\">" +
             "<div class=\"progress\" style=\"height:7px;\">" +
            "<div class=\"progress-bar progress-bar-info\" role=\"progressbar\"  aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width:" + porcentaje(res.visionSuper.Value) + "; height:7px;\">" +
             "</div></div>" +
                "</td></tr>" : "") +

        "</table>" +
              "</div>" +
                 
                 
              "</div>" +
              "<br/>" +
          "<div class=\"row\">" +
            "<div class=\"col-md-1\"></div>" +
              "<div class=\"col-md-2\">" +
               "<p class=\"text-info letramuychica\">Autoevaluado</p>" +
               "</div>" +
                "<div class=\"col-md-1\">" +
                 "<div class=\"progress\" style=\"height:7px;\">" +
                 "<div class=\"progress-bar progress-bar-success\" role=\"progressbar\"  aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width:100%;\">" +
                 "</div></div>" +
                 "</div></div>" +
                  "<div class=\"row\">" +
                    "<div class=\"col-md-1\"></div>" +
              "<div class=\"col-md-2\">" +
              
               "<p class=\"text-info letramuychica\">Supervisor</p>" +
               "</div>" +
                "<div class=\"col-md-1\">" +
                 "<div class=\"progress\" style=\"height:7px;\">" +
                 "<div class=\"progress-bar progress-bar-info\" role=\"progressbar\"  aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width:100%;\">" +
                 "</div></div>" +
                 "</div>" + 

                 "</div>" +
                 "<br/>" +
                 "<div class=\"row\">" +
                 "<div class=\"col-md-1\"></div>" +
                    "<div class=\"col-md-11\">" +
      "   <div class=\"panel panel-info\" style=\"border: 1px solid;\">" +
     "               <div class=\"panel-heading\">" +
      "                <p class=\"text-info letrachica\">" +
              "Promedio Calificación Competencias y Calificación General.</p>" +
     "</div></div>" +
       "</div>" +
                 "</div><br/>" +
                    "<div class=\"row\">" +
                 "<div class=\"col-md-3\"></div>" +
                   "<div class=\"col-md-9\">" +
                    " <p class=\"text-info vertical text-center\">" +
              "DESEMPEÑO GLOBAL</p>" +
              "</div></div>" +
                 "<div class=\"row\">" +
                 "<div class=\"col-md-1\"></div><br/>" +
                    "<div class=\"col-md-10\">" +
                    "<table>" +
                    "<tr><td class=\"fondoh\"></td><td class=\"fondoh\"></td><td class=\"fondoh\" width=\"55\"><p class=\"text-info letramuychica\">Insuficiente</p></td><td class=\"fondoh\" width=\"55\"><p class=\"text-info letramuychica\">Incompleto</p></td><td  class=\"fondoh\" width=\"55\"><p class=\"text-info letramuychica\">Alineado</p></td>" +
                    "<td width=\"55\" class=\"fondoh\"><p class=\"text-info letramuychica\">Alto</p></td><td class=\"fondoh\" width=\"55\"><p class=\"text-info letramuychica\">Excepcional</p></td></tr>" +
                    "<tr><td width=\"2\" rowspan=\"5\" height=\"25\" class=\"fondoh\"><p class=\"text-warning vertical\">COMPETENCIAS</p></td><td height=\"60\" class=\"fondoh\"><p class=\"text-warning letramuychica\">Excelente</p></td>" +
                    "<td  " + ((promedio == "51") ? "class=\"marca\"" : "class=\"fondo\"") + "></td>" +
                    "<td  " + ((promedio == "52") ? "class=\"marca\"" : "class=\"fondo\"") + "></td>" +
                    "<td  " + ((promedio == "53") ? "class=\"marca\"" : "class=\"fondo\"") + "></td>" +
                    "<td  " + ((promedio == "54") ? "class=\"marca\"" : "class=\"fondo\"") + "></td>" +
                    "<td  " + ((promedio == "55") ? "class=\"marca\"" : "class=\"fondo\"") + "></td></tr>" +
                    "<tr><td width=\"60\" height=\"60\" class=\"fondoh\"><p class=\"text-warning letramuychica\">Avanzada</p></td>" +
                    "<td  " + ((promedio == "41") ? "class=\"marca\"" : "class=\"fondo\"") + "></td>" +
                    "<td  " + ((promedio == "42") ? "class=\"marca\"" : "class=\"fondo\"") + "></td>" +
                    "<td  " + ((promedio == "43") ? "class=\"marca\"" : "class=\"fondo\"") + "></td>" +
                    "<td  " + ((promedio == "44") ? "class=\"marca\"" : "class=\"fondo\"") + "></td>" +
                    "<td  " + ((promedio == "45") ? "class=\"marca\"" : "class=\"fondo\"") + "></td></tr>" +
                    "<tr><td width=\"60\" height=\"60\" class=\"fondoh\"><p class=\"text-warning letramuychica\">En Desarrollo</p></td>" +
                    "<td  " + ((promedio == "31") ? "class=\"marca\"" : "class=\"fondo\"") + "></td>" +
                    "<td  " + ((promedio == "32") ? "class=\"marca\"" : "class=\"fondo\"") + "></td>" +
                    "<td  " + ((promedio == "33") ? "class=\"marca\"" : "class=\"fondo\"") + "></td>" +
                    "<td  " + ((promedio == "34") ? "class=\"marca\"" : "class=\"fondo\"") + "></td>" +
                    "<td  " + ((promedio == "35") ? "class=\"marca\"" : "class=\"fondo\"") + "></td></tr>" +
                    "<tr><td width=\"60\" height=\"60\" class=\"fondoh\"><p class=\"text-warning letramuychica\">Area de Mejora</p></td>" +
                    "<td  " + ((promedio == "21") ? "class=\"marca\"" : "class=\"fondo\"") + "></td>" +
                    "<td  " + ((promedio == "22") ? "class=\"marca\"" : "class=\"fondo\"") + "></td>" +
                    "<td  " + ((promedio == "23") ? "class=\"marca\"" : "class=\"fondo\"") + "></td>" +
                    "<td  " + ((promedio == "24") ? "class=\"marca\"" : "class=\"fondo\"") + "></td>" +
                    "<td  " + ((promedio == "25") ? "class=\"marca\"" : "class=\"fondo\"") + "></td></tr>" +
                    "<tr><td width=\"60\" height=\"60\" class=\"fondoh\"><p class=\"text-warning letramuychica\">Ausencia de Competencia</p></td>" +
                    "<td  " + ((promedio == "11") ? "class=\"marca\"" : "class=\"fondo\"") + "></td>" +
                    "<td  " + ((promedio == "12") ? "class=\"marca\"" : "class=\"fondo\"") + "></td>" +
                    "<td  " + ((promedio == "13") ? "class=\"marca\"" : "class=\"fondo\"") + "></td>" +
                    "<td  " + ((promedio == "14") ? "class=\"marca\"" : "class=\"fondo\"") + "></td>" +
                    "<td  " + ((promedio == "15") ? "class=\"marca\"" : "class=\"fondo\"") + "></td></tr>" +
                    
                    "</table>" +
                    "</div>" +
                 "</div>" +
          "</div>";


        return result;
    }

    private void GenerarPDF(string html,Empleados emp)
    {
        Byte[] bytes;

        //Boilerplate iTextSharp setup here
        //Create a stream that we can write to, in this case a MemoryStream
        using (var ms = new MemoryStream())
        {

            //Create an iTextSharp Document which is an abstraction of a PDF but **NOT** a PDF
            using (var doc = new Document(PageSize.A4,60f,0f,0f,0f))
            {

                //Create a writer that's bound to our PDF abstraction and our stream
                using (var writer = PdfWriter.GetInstance(doc, ms))
                {

                    //Open the document for writing
                    doc.Open();

                    //Our sample HTML and CSS
                    var example_html = html;
                    var example_css = "html { font-family: sans-serif;  -webkit-text-size-adjust: 100%;      -ms-text-size-adjust: 100%;} body {  margin: 0;} code {  font-family: monospace, monospace;  font-size: 1em;}  " +
                                        ".label { border: 1px solid #000;  } label {  display: inline-block;  max-width: 100%;  margin-bottom: 5px;  font-weight: bold;} .text-info {  color: #31708f;} " +
                                        ".panel { margin-bottom: 20px; background-color: #fff;  border: 1px solid;}" +
                                        ".panel-body { padding: 15px;} " +
                                        ".panel-heading { padding: 10px 15px; border-bottom: 1px solid transparent; border-top-left-radius: 3px; border-top-right-radius: 3px;}" +
                                        ".panel-info { border-color: #bce8f1;}" +
                                        ".panel-info > .panel-heading {  color: #31708f;  background-color: #d9edf7;  border-color: #bce8f1;}" +
                                        "thead {color:blue;} " +
                                        ".text-warning {color: #8a6d3b;}" +
                                        "tbody { color: blue; }" +
                                        "tfoot { color: blue; } " +
                                        ".vertical {font-size:13ppt; }" +
                    "th, td {background-color:azure; border: 1px solid white;} " +
                                        ".marca {background-color:#5bc0de; border: 1px solid white;} " +
                                          ".marcaSuper {background-color:#5cb85c; border: 1px solid white;} " +
                                          ".fondo {background-color:#D1D7D3; border: 1px solid white;} " +
                                           ".fondoh {background-color:#FEFFFF; border: 1px solid white; font-size:9ppt;} " +
                                        "th{text-align:center;}" +
                                        "td{padding-left:0px;}" +
                                        " .letramuychica{font-size:7ppt;}" +
                                        " .letrachica{font-size:9ppt;}" +
                                        "code {padding: 2px 4px; font-size: 10ppt; color: #c7254e; background-color: #f9f2f4; border-radius: 4px;}" +
                                        "i {font-style: italic;}" +
                                        ".h1 {font-size: 36px;}"  +
                                        "h2,.h2 {font-size: 30px;} h3,.h3 {font-size: 24px;}h4,.h4 {font-size: 18px;}h5,.h5 {font-size: 14px;}h6,.h6 {font-size: 12px;}" +
                                        "progress { display: inline-block; vertical-align: baseline;}" +
                                        ".progress {  height: 20px; margin-bottom: 20px; overflow: hidden; background-color: #777; border-radius: 4px; -webkit-box-shadow: inset 0 1px 2px rgba(0, 0, 0, .1); box-shadow: inset 0 1px 2px rgba(0, 0, 0, .1); }" +
                                        ".progress-bar {float: left; width: 0; height: 100%; font-size: 12px; line-height: 20px; color: #fff; text-align: center; background-color: #337ab7;   }" +
                                        ".progress-striped.progress-bar, .progress-bar-striped { background-image: -webkit-linear-gradient(45deg, rgba(255, 255, 255, .15) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .15) 50%, rgba(255, 255, 255, .15) 75%, transparent 75%, transparent); background-image:      -o-linear-gradient(45deg, rgba(255, 255, 255, .15) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .15) 50%, rgba(255, 255, 255, .15) 75%, transparent 75%, transparent); background-image: linear-gradient(45deg, rgba(255, 255, 255, .15) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .15) 50%, rgba(255, 255, 255, .15) 75%, transparent 75%, transparent); -webkit-background-size: 40px 40px; background-size: 40px 40px;}" +
                                        ".progress-bar-success { background-color: #5cb85c;}" +
                                        ".progress-striped.progress-bar-success { background-image: -webkit-linear-gradient(45deg, rgba(255, 255, 255, .15) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .15) 50%, rgba(255, 255, 255, .15) 75%, transparent 75%, transparent); background-image:      -o-linear-gradient(45deg, rgba(255, 255, 255, .15) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .15) 50%, rgba(255, 255, 255, .15) 75%, transparent 75%, transparent); background-image: linear-gradient(45deg, rgba(255, 255, 255, .15) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .15) 50%, rgba(255, 255, 255, .15) 75%, transparent 75%, transparent);}" +
                                        ".progress-bar-info { background-color: #5bc0de;}" +
                                        ".progress-striped.progress-bar-info { background-image: -webkit-linear-gradient(45deg, rgba(255, 255, 255, .15) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .15) 50%, rgba(255, 255, 255, .15) 75%, transparent 75%, transparent); background-image: -o-linear-gradient(45deg, rgba(255, 255, 255, .15) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .15) 50%, rgba(255, 255, 255, .15) 75%, transparent 75%, transparent); background-image: linear-gradient(45deg, rgba(255, 255, 255, .15) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .15) 50%, rgba(255, 255, 255, .15) 75%, transparent 75%, transparent);}" +
                                        ".progress-bar-warning {background-color: #f0ad4e;}" +
                                        ".progress-striped.progress-bar-warning { background-image: -webkit-linear-gradient(45deg, rgba(255, 255, 255, .15) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .15) 50%, rgba(255, 255, 255, .15) 75%, transparent 75%, transparent); background-image: -o-linear-gradient(45deg, rgba(255, 255, 255, .15) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .15) 50%, rgba(255, 255, 255, .15) 75%, transparent 75%, transparent); background-image:linear-gradient(45deg, rgba(255, 255, 255, .15) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .15) 50%, rgba(255, 255, 255, .15) 75%, transparent 75%, transparent);}" +
                                        ".progress-bar-danger {background-color: #d9534f;}" +
                                        ".progress-striped.progress-bar-danger {background-image: -webkit-linear-gradient(45deg, rgba(255, 255, 255, .15) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .15) 50%, rgba(255, 255, 255, .15) 75%, transparent 75%, transparent); background-image: -o-linear-gradient(45deg, rgba(255, 255, 255, .15) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .15) 50%, rgba(255, 255, 255, .15) 75%, transparent 75%, transparent); background-image: linear-gradient(45deg, rgba(255, 255, 255, .15) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .15) 50%, rgba(255, 255, 255, .15) 75%, transparent 75%, transparent);}" +
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
                                        ".text-center {text-align: center;}" +
                                        ".encabezado{background-color:#6AEC74; border: 0px solid #6AEC74; }" +
                                        " .col-md-1 {width: 8.33333333%;}";
                

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
            Response.AddHeader("Content-Disposition", "attachment;filename=Resumen-"+ emp.nombreCompleto  + ".pdf");
            Response.BinaryWrite(bytes);
        }
    }

    private string porcentaje(int nivel)
    {
       return ((nivel * 20)-5).ToString() + "%";
       
    }

    private string getNivelCompetencias(int niv,bool supervisor)
    {
        string result = "";
        switch (niv)
        {
            case 1:
                result = "Ausencia de competencia";
                break;
            case 2:
                result = "Área de mejora";
                break;
            case 3:
                result = "En desarrollo";
                break;
            case 4:
                result = "Avanzada";
                break;
            case 5:
                result = "Excelente";
                break;
        }

        return result + ((supervisor)?"- Supervisor":" - Autoevaluado");
    }

    private string getNivelDesempeno(int niv, bool supervisor)
    {
        string result = "";
        switch (niv)
        {
            case 1:
                result = "Insuficiente";
                break;
            case 2:
                result = "Logro Incompleto";
                break;
            case 3:
                result = "Logro Alineado";
                break;
            case 4:
                result = "Logro Alto";
                break;
            case 5:
                result = "Logro Excepcional";
                break;
        }

        return result + ((supervisor) ? "- Supervisor" : " - Autoevaluado");
    }
}