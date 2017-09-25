using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net.Mime;

namespace Helpers
{
   public class html
    {
        public static string armarResponsabilidades(string descripcion,string detalle,string titulo2,int indice,int idRespuesta,int total,string escrito,bool lectura,int idEmpleado,string idioma,string name)
        {
            string result = "    <div id=\"item" + indice.ToString() + "\" class=\"itemDiv\"> " +
     "   <div class=\"row\"> " +
      "      <div class=\"col-md-10\">" +
      "  <div class=\"panel panel-info\">" +
       "            <div class=\"panel-heading\">" +
        "                <label class=\"text-info\">" +
                            descripcion +
          "              </label>" +
           "         </div>" +
            "        <div class=\"panel-body\">" +
             "           <p class=\"text-info\">" +
              detalle +
               "         </p>" +
                "    </div>" +
                "</div>" +
            "</div>" +
        "</div>" +
        "<div class=\"row\">" +
        "    <div class=\"col-md-10\">" +
        "        <div class=\"panel panel-info\">" +
       "             <div class=\"panel-body\">" +
        "                <p class=\"text-info\">" +
         titulo2 +
          "              </p>" +
           "             <textarea " + ((lectura)? "disabled":"") + " placeholder=\"Escriba su respuesta aquí\" rows =\"3\" class=\"textarea form-control\" id=\"escrito" + indice.ToString() +"\" id=\"respuesta\">" + escrito + "</textarea>" +
            "            <input type=\"hidden\" id=\"id" + indice.ToString() + "\" value=\""+ idRespuesta.ToString() + "\" />" +
            "        </div>" +
            "    </div>" +
            "</div>" +
        "</div>" +
        "<div class=\"row\">" +
        "    <div class=\"col-md-10\">" +
        ((lectura)?"": "        <a href=\"#\" class=\"btn btn-info pull-right\" onclick=\"javascript:var result = responderResponsabilidad('" + indice + "'); if(result)alert('La evaluación fue guardada correctamente');\" role=\"button\">" + ((idioma == "portugues")? "salvar" : "Guardar") + "</a>") +
        "        <a href=\"" + ((indice == total)? "/competencia.aspx":"#") + "\" class=\"btn btn-info pull-right\" " + ((!lectura || indice < total)? " onclick=\"javascript:" + ((lectura) ? "" : "responderResponsabilidad('" + indice + "'); ") + ((indice == total) ? "" : "siguiente('" + (indice + 1).ToString() + "');") + "\" " : "") + " role=\"button\">" + ((idioma == "portugues") ? "Seguinte" : "Siguiente") + "</a>" +
        "        <a href=\"" + ((indice == 1)?"/home.aspx":"#") + "\" class=\"btn btn-info pull-right\" " + ((!lectura || indice > 1)?" onclick=\"javascript:" + ((lectura)?"":"responderResponsabilidad('" + indice + "'); ") + ((indice == 1) ? "" : "siguiente('" + (indice - 1).ToString() + "');") + "\" ":"") + " role=\"button\">Anterior</a>" +
       "        <a href=\"#\" onclick=\"javascript:toPdf('" + idRespuesta + "','" + idEmpleado + "','" + 1 + "','" + indice + "','" + 0 + "');\" class=\"btn btn-info pull-right\" role=\"button\">Descargar</a>" +
        "    </div>" +
        "</div>" +
    "</div>";

            return result;
        }

        public static string armarResponsabilidadesPDF(string descripcion, string detalle, string titulo2, int indice, int idRespuesta, int total, string escrito, bool lectura, int idEmpleado, string idioma, string name)
        {
            string result = "    <div id=\"item" + indice.ToString() + "\" class=\"itemDiv\"> " +
     "   <div class=\"row\"> " +
      "      <div class=\"col-md-10\">" +
      "  <div class=\"panel panel-info\">" +
       "            <div class=\"panel-heading\">" +
        "                <label class=\"text-info\">" +
                            descripcion +
          "              </label>" +
           "         </div>" +
            "        <div class=\"panel-body\">" +
             "           <p class=\"text-info\">" +
              detalle +
               "         </p>" +
                "    </div>" +
                "</div>" +
            "</div>" +
        "</div>" +
        "<div class=\"row\">" +
        "    <div class=\"col-md-10\">" +
        "        <div class=\"panel panel-info\">" +
       "             <div class=\"panel-body\">" +
        "                <p class=\"text-info\">" +
         titulo2 +
          "              </p>" +
           "             <p class=\"navbar-text\">" + escrito + "</p>" +
            "            <input type=\"hidden\" id=\"id" + indice.ToString() + "\" value=\"" + idRespuesta.ToString() + "\" />" +
            "        </div>" +
            "    </div>" +
            "</div>" +
        "</div>" +
    "</div>";

            return result;
        }

        public static string armarCompetencia(int indice,string subtitulo,string concepto,string detalle,string nivel,int total,string nivelDetalle, string opciones,int idRespuesta, string supervisor,string valor,bool lectura,bool supervisa,string valorSuper,string name,int idEmpleado)
        {
            string niveldet = "";
            foreach (string det in nivelDetalle.Split('*'))
            {
                if(!string.IsNullOrEmpty(det))
                    niveldet += "<p class=\"text-info\">*" + det + "</p>";

            }
            string result = " <div id=\"item" + indice.ToString() + "\" class=\"itemDiv\"> " +
     "   <div class=\"row\">" +
      "      <div class=\"col-md-11\">" +
       "         <h5>" +
        "            <label class=\"text-info\">" +
         subtitulo +
         "</label></h5>" +
         "   </div>" +
 "       </div>" +
  "      <div class=\"row\">" +
   "         <div class=\"col-md-11\">" +
  "            <div class=\"panel panel-info\">" +
     "               <div class=\"panel-heading\">" +
      "                  <label class=\"text-info\">" +
       concepto +
       "</label>" +
        "            </div>" +
         "           <div class=\"panel-body\">" +
          "              <p class=\"text-info\">" +
                       indice.ToString() + "- " + detalle +
            "            </p>" +
             "       </div>" +
              "  </div>" +
            "</div>" +
        "</div>" +
       " <div class=\"row\">" +
        "    <div class=\"col-md-4\">" +
         "       <div class=\"panel panel-info\" style=\"min-height: 360px;\">" +
          "          <div class=\"panel-heading\">" +
           "             <label class=\"text-info\">" +
            nivel +
            "</label>" +
             "       </div>" +
              "      <div class=\"panel-body\">" +
             niveldet +
                  "  </div>" +
               " </div>" +
            "</div>" +
           " <div class=\"col-md-7\">" +
             ((supervisa) ? "<div class=\"panel panel-danger\">" : "            <div class=\"panel panel-info\">") +
             "       <div class=\"panel-heading\">" +
              "          <label class=\"text-info\">" +
               opciones +
               "</label>" +
                "    </div>" +
                 "   <div class=\"panel-body\">" +
                  "      <table>" +
                   "         <thead>" +
                    "            <tr>" +
                    ((supervisa)? "<th style=\"width: 4%;\" class=\"fondoSupervisor\"><p class=\"text-info letrachica text-center\">A evaluar</p></th>" +
                                        "<th style =\"width: 4%;\"><p class=\"text-info letrachica text-center\">Autoevaluación</p></th>" +
                        "              <th style=\"width: 8%;\">" +
                       "                 <p class=\"text-info letrachica text-center\">Nivel</p>" +
                        "            </th>" +
                         "           <th style=\"width: 20%;\">" +
                          "              <p class=\"text-info letrachica text-center\">Nivel Desarrollo</p>" +
                           "         </th>" +
                            "        <th style=\"width: 65%;\">" +
                             "           <p class=\"text-info letrachica text-center\">Frecuencia</p>" +
                              "      </th>"
                    :               "<th style=\"width: 5%;\"></th>" +
                      "              <th style=\"width: 10%;\">" +
                       "                 <p class=\"text-info letrachica text-center\">Nivel</p>" +
                        "            </th>" +
                         "           <th style=\"width: 20%;\">" +
                          "              <p class=\"text-info letrachica text-center\">Nivel Desarrollo</p>" +
                           "         </th>" +
                            "        <th>" +
                             "           <p class=\"text-info letrachica text-center\">Frecuencia</p>" +
                              "      </th>") +
                     
                               " </tr>" +
                          "  </thead>" +
                           " <tr>" +
                           ((supervisa)? "<td class=\"fondoSupervisor\"> " +
                             "       <input type=\"radio\" " + ((valorSuper == "1") ? "checked" : "") + ((lectura)?" disabled":"") + " class=\"radioActivo\"  onchange=\"javascript:marcarValor('1','" + indice.ToString() + "');\" name=\"pruebaSuper" + idRespuesta.ToString() + "\" />" +
                              "  </td>":"")+
                            "    <td>" +
                             "       <input type=\"radio\" " + ((valor == "1")?"checked":"") + ((lectura || supervisa) ? " disabled" : "") + " onchange=\"javascript:marcarValor('1','" + indice.ToString() + "');\" name=\"prueba" + idRespuesta.ToString() +"\" />" +
                              "  </td>" +
                               " <td>" +
                                "    <p class=\"text-info  letrachica\">Nivel 1</p>" +
                           "     </td>" +
                            "    <td>" +
                             "       <p class=\"text-info letrachica\">Ausencia de competencia</p>" +
                              "  </td>" +
                               " <td>" +
                                "    <p class=\"text-info letrachica\">Ausencia de la competencias o presencia muy básica- se muestra de manera excepcional en algunos momentos críticos del desempeño.Necesita un alta grado de desarrollo.</p>" +
                            "    </td>" +
                            "</tr>" +
                            "<tr>" +
                             ((supervisa) ? "<td class=\"fondoSupervisor\"> " +
                             "       <input type=\"radio\" " + ((valorSuper == "2") ? "checked" : "") + ((lectura) ? " disabled" : "") + " class=\"radioActivo\" onchange=\"javascript:marcarValor('2','" + indice.ToString() + "');\" name=\"pruebaSuper" + idRespuesta.ToString() + "\" />" +
                              "  </td>" : "") +
                            "    <td>" +
                            "        <input type=\"radio\" " + ((valor == "2") ? "checked" : "") + ((lectura || supervisa) ? " disabled" : "") + " onchange=\"javascript:marcarValor('2','" + indice.ToString() + "');\" name=\"prueba" + idRespuesta.ToString() + "\" />" +
                            "    </td>" +
                            "    <td>" +
                            "        <p class=\"text-info letrachica\">Nivel 2</p>" +
                            "    </td>" +
                            "    <td>" +
                            "        <p class=\"text-info letrachica\">Área de mejora</p>" +
                            "    </td>" +
                            "    <td>" +
                            "        <p class=\"text-info letrachica\">La competencia esta presente de manera básica, y se muestra en algunos momentos durante el desempeño de puesto.Necesita desarrollo.</p>" +
                            "    </td>" +
                            "</tr>" +
                            "<tr>" +
                              ((supervisa) ? "<td class=\"fondoSupervisor\"> " +
                             "       <input type=\"radio\" " + ((valorSuper == "3") ? "checked" : "") + ((lectura) ? " disabled" : "") + " class=\"radioActivo\" onchange=\"javascript:marcarValor('3','" + indice.ToString() + "');\" name=\"pruebaSuper" + idRespuesta.ToString() + "\" />" +
                              "  </td>" : "") +
                            "    <td>" +
                            "        <input type=\"radio\" " + ((valor == "3") ? "checked" : "") + ((lectura || supervisa) ? " disabled" : "") + " onchange=\"javascript:marcarValor('3','" + indice.ToString() + "');\" name=\"prueba" + idRespuesta.ToString() + "\" />" +
                            "    </td>" +
                            "    <td>" +
                            "        <p class=\"text-info letrachica\">Nivel 3</p>" +
                            "    </td>" +
                            "    <td>" +
                             "       <p class=\"text-info letrachica\">En desarrollo</p>" +
                             "   </td>" +
                             "   <td>" +
                             "       <p class=\"text-info letrachica\">La competencia está presente y en fase de desarrollo: se muestra  de manera habitual, pero existen situaciones en las que aún no se pone en marcha. Necesita mejorar.</p>" +
                             "   </td>" +
                            "</tr>" +
                            "<tr>" +
                              ((supervisa) ? "<td class=\"fondoSupervisor\"> " +
                             "       <input type=\"radio\" " + ((valorSuper == "4") ? "checked" : "") + ((lectura) ? " disabled" : "") + " class=\"radioActivo\" onchange=\"javascript:marcarValor('4','" + indice.ToString() + "');\" name=\"pruebaSuper" + idRespuesta.ToString() + "\" />" +
                              "  </td>" : "") +
                            "    <td>" +
                            "        <input type=\"radio\" " + ((valor == "4") ? "checked" : "") + ((lectura || supervisa) ? " disabled" : "") + " onchange=\"javascript:marcarValor('4','" + indice.ToString() + "');\" name=\"prueba" + idRespuesta.ToString() + "\" />" +
                            "    </td>" +
                             "   <td>" +
                             "       <p class=\"text-info letrachica\">Nivel 4</p>" +
                             "   </td>" +
                             "   <td>" +
                             "       <p class=\"text-info letrachica\">Avanzada</p>" +
                             "   </td>" +
                             "   <td>" +
                             "       <p class=\"text-info letrachica\">La competencia se muestra casi siempre. Está es un nivel avanzada de desarrollo.</p>" +
                             "   </td>" +
                            "</tr>" +
                            "<tr>" +
                              ((supervisa) ? "<td class=\"fondoSupervisor\"> " +
                             "       <input type=\"radio\" " + ((valorSuper == "5") ? "checked" : "") + ((lectura) ? " disabled" : "") + " class=\"radioActivo\" onchange=\"javascript:marcarValor('5','" + indice.ToString() + "');\" name=\"pruebaSuper" + idRespuesta.ToString() + "\" />" +
                              "  </td>" : "") +
                            "    <td>" +
                            "        <input type=\"radio\" " + ((valor == "5") ? "checked" : "") + ((lectura || supervisa) ? " disabled" : "") + " onchange=\"javascript:marcarValor('5','" + indice.ToString() + "');\" name=\"prueba" + idRespuesta.ToString() + "\" />" +
                            "    </td>" +
                            "    <td>" +
                            "        <p class=\"text-info letrachica\">Nivel 5</p>" +
                            "    </td>" +
                            "    <td>" +
                            "        <p class=\"text-info letrachica\">Excelente</p>" +
                            "    </td>" +
                            "    <td>" +
                            "        <p class=\"text-info letrachica\">La competencia se muestra siempre en el nivel de excelencia requerido para el puesto.</p>" +
                            "    </td>" +
                            "</tr>" +
                        "</table>" +
                   " </div>" +
                "</div>" +
            "</div>" +
        "</div>" +
        "<div class=\"row\">" +
        "    <div class=\"col-md-11\">" +
         "            <input type=\"hidden\" id=\"id" + indice.ToString() + "\" value=\"" + idRespuesta.ToString() + "\" />" +
          "            <input type=\"hidden\" id=\"valor" + indice.ToString() + "\"/>" +
        ((lectura)?"": "        <a href=\"#\" class=\"btn btn-info pull-right\" onclick=\"javascript:var result=responderCompetenecia('" + indice + "','"+ supervisor + "','NO'); if(result)alert('La evaluación fue guardada correctamente');\" role=\"button\">Guardar</a>") +
        "        <a href=\"" + ((indice == total)? "/oportunidades.aspx":"#") + "\" class=\"btn btn-info pull-right\" onclick=\"javascript:responderCompetenecia('" + indice + "','" + supervisor + "','NO'); " + ((indice == total)?"":"siguiente('" + (indice + 1).ToString() + "');") + "\" role=\"button\">Siguiente</a>" +
        "        <a href=\"" + ((indice == 1) ? "/responsabilidades.aspx" : "#") + "\" class=\"btn btn-info pull-right\" onclick=\"javascript:responderCompetenecia('" + indice + "','" + supervisor + "','NO');" + ((indice == 1) ? "" : "siguiente('" + (indice - 1).ToString() + "');") + "\" role=\"button\">Anterior</a>" +
       "        <a href=\"#\" onclick=\"javascript:toPdf('" + idRespuesta + "','" + idEmpleado + "','" + 2 + "','" + indice + "','" + ((supervisa)?"1":"0") + "');\" class=\"btn btn-info pull-right\" role=\"button\">Descargar</a>" +
        "    </div>" +
        "</div>" +
    "</div>";
            return result;
        }

        public static string armarCompetenciaPDF(int indice, string subtitulo, string concepto, string detalle, string nivel, int total, string nivelDetalle, string opciones, int idRespuesta, string supervisor, string valor, bool lectura, bool supervisa, string valorSuper, string name)
        {
            string result = " <div id=\"item" + indice.ToString() + "\" class=\"itemDiv\" style=\"page-break-before:avoid\"> " +
     "   <div class=\"row\">" +
      "      <div class=\"col-md-11\">" +
       "            <div class=\"panel panel-info\" style=\"border: 1px solid;\">" +
     "               <div class=\"panel-heading\">" +
       "         <h5>" +
        "            <label class=\"text-info\">" +
         subtitulo +
         "</label></h5>" +
           "</div></div>" +
         "   </div>" +
 "       </div><br/>" +
  "      <div class=\"row\">" +
   "         <div class=\"col-md-11\">" +
 
      "                  <label class=\"text-info\">" +
       concepto +
       "</label>" +
         "            <div class=\"panel panel-info\" style=\"border: 1px solid;\">" +
         "           <div class=\"panel-body\">" +
          "              <p class=\"text-info\">" +
                       indice.ToString() + "- " + detalle +
            "            </p>" +
             "       </div>" +
              "  </div>" +
            "</div>" +
        "</div>" +
       " <div class=\"row\">" +
        "    <div class=\"col-md-4\">" +
         "       <div class=\"panel panel-info\" style=\"min-height: 360px;\">" +
          "          <div class=\"panel-heading\">" +
           "             <label class=\"text-info\">" +
            nivel +
            "</label>" +
             "       </div>" +
              "      <div class=\"panel-body\">" +
               "         <p class=\"text-info\">" +
                    nivelDetalle +
                 "       </p>" +
                  "  </div>" +
               " </div>" +
            "</div>" +
           " <div class=\"col-md-7\">" +
             ((supervisa) ? "<div class=\"panel panel-danger\">" : "<div class=\"panel panel-info\">") +
             "       <div class=\"panel-heading\">" +
              "          <label class=\"text-info\">" +
               opciones +
               "</label>" +
                "    </div>" +
                 "   <div class=\"panel-body\">" +
                  "      <table style=\"width:300px;\">" +
                   "         <thead>" +
                    "            <tr>" +
                    ((supervisa) ? "<th style=\"width: 2%;\" class=\"fondoSupervisor\"><p class=\"text-info letramuychica text-center\">Evaluado</p></th>" +
                                        "<th style =\"width: 2%;\"><p class=\"text-info letramuychica text-center\">Autoevaluación</p></th>" +
                        "              <th style=\"width: 8%;\">" +
                       "                 <p class=\"text-info letramuychica text-center\">Nivel</p>" +
                        "            </th>" +
                         "           <th style=\"width: 20%;\">" +
                          "              <p class=\"text-info letramuychica text-center\">Nivel Desarrollo</p>" +
                           "         </th>" +
                            "        <th style=\"width: 50%;\">" +
                             "           <p class=\"text-info letramuychica text-center\">Frecuencia</p>" +
                              "      </th>"
                    : "<th style=\"width: 5%;\"></th>" +
                      "              <th style=\"width: 10%;\">" +
                       "                 <p class=\"text-info letramuychica text-center\">Nivel</p>" +
                        "            </th>" +
                         "           <th style=\"width: 20%;\">" +
                          "              <p class=\"text-info letramuychica text-center\">Nivel Desarrollo</p>" +
                           "         </th>" +
                            "        <th style=\"width: 50%;\">" +
                             "           <p class=\"text-info letramuychica text-center\">Frecuencia</p>" +
                              "      </th>") +

                               " </tr>" +
                          "  </thead>" +
                           " <tr>" +
                           ((supervisa) ? "<td class=\"fondoSupervisor\"> " +
                             ((valorSuper == "1") ? "<p class=\"text-info letramuychica text-center\">SI</p>" : "")  +
                              "  </td>" : "") +
                            "    <td>" +
                             ((valor == "1") ? "<p class=\"text-info letramuychica text-center\">SI</p>" : "") +
                              "  </td>" +
                               " <td>" +
                                "    <p class=\"text-info  letramuychica\">Nivel 1</p>" +
                           "     </td>" +
                            "    <td>" +
                             "       <p class=\"text-info letramuychica\">Ausencia de competencia</p>" +
                              "  </td>" +
                               " <td>" +
                                "    <p class=\"text-info letramuychica\">Ausencia de la competencias o presencia muy básica- se muestra de manera excepcional en algunas momentos críticos del desempeño.Necesita un alta grado de desarrollo.</p>" +
                            "    </td>" +
                            "</tr>" +
                            "<tr>" +
                             ((supervisa) ? "<td class=\"fondoSupervisor\"> " +
                             ((valorSuper == "2") ? "<p class=\"text-info letramuychica text-center\">SI</p>" : "") +
                              "  </td>" : "") +
                            "    <td>" +
                            ((valor == "2") ? "<p class=\"text-info letramuychica text-center\">SI</p>" : "") +
                            "    </td>" +
                            "    <td>" +
                            "        <p class=\"text-info letramuychica\">Nivel 2</p>" +
                            "    </td>" +
                            "    <td>" +
                            "        <p class=\"text-info letramuychica\">Área de mejora</p>" +
                            "    </td>" +
                            "    <td>" +
                            "        <p class=\"text-info letramuychica\">La competencia esta presente de manera básica, y se muestra en algunos momentos durante el desempeño de puesto.Necesito desarrollo.</p>" +
                            "    </td>" +
                            "</tr>" +
                            "<tr>" +
                              ((supervisa) ? "<td class=\"fondoSupervisor\"> " +
                             ((valorSuper == "3") ? "<p class=\"text-info letramuychica text-center\">SI</p>" : "") +
                              "  </td>" : "") +
                            "    <td>" +
                            ((valor == "3") ? "<p class=\"text-info letramuychica text-center\">SI</p>" : "") +
                            "    </td>" +
                            "    <td>" +
                            "        <p class=\"text-info letramuychica\">Nivel 3</p>" +
                            "    </td>" +
                            "    <td>" +
                             "       <p class=\"text-info letramuychica\">En desarrollo</p>" +
                             "   </td>" +
                             "   <td>" +
                             "       <p class=\"text-info letramuychica\">La competencia está presente y en fase de desarrollo: se muestra  de manera habitual, pero existen situaciones en las que aún no se pone en marcha. Necesita mejora.</p>" +
                             "   </td>" +
                            "</tr>" +
                            "<tr>" +
                              ((supervisa) ? "<td class=\"fondoSupervisor\"> " +
                            ((valorSuper == "4") ? "<p class=\"text-info letramuychica text-center\">SI</p>" : "") +
                              "  </td>" : "") +
                            "    <td>" +
                            ((valor == "4") ? "<p class=\"text-info letramuychica text-center\">SI</p>" : "") +
                            "    </td>" +
                             "   <td>" +
                             "       <p class=\"text-info letramuychica\">Nivel 4</p>" +
                             "   </td>" +
                             "   <td>" +
                             "       <p class=\"text-info letramuychica\">Avanzada</p>" +
                             "   </td>" +
                             "   <td>" +
                             "       <p class=\"text-info letramuychica\">La competencias se muestra casi siempre. Está es un nivel avanzada de desarrollo.</p>" +
                             "   </td>" +
                            "</tr>" +
                            "<tr>" +
                              ((supervisa) ? "<td class=\"fondoSupervisor\"> " +
                            ((valorSuper == "5") ? "<p class=\"text-info letramuychica text-center\">SI</p>" : "") +
                              "  </td>" : "") +
                            "    <td>" +
                            ((valor == "5") ? "<p class=\"text-info letramuychica text-center\">SI</p>" : "") +
                            "    </td>" +
                            "    <td>" +
                            "        <p class=\"text-info letramuychica\">Nivel 5</p>" +
                            "    </td>" +
                            "    <td>" +
                            "        <p class=\"text-info letramuychica\">Excelente</p>" +
                            "    </td>" +
                            "    <td>" +
                            "        <p class=\"text-info letramuychica\">La competencias no sólo se muestra siempre sino que habitualmente a las exigencias del puesto, está en niveles de excelencia.</p>" +
                            "    </td>" +
                            "</tr>" +
                        "</table>" +
                   " </div>" +
                "</div>" +
            "</div>" +
        "</div>" +
    "</div>";
            return result;
        }

        public static string armarCompetenciaPortugues(int indice, string subtitulo, string concepto, string detalle, string nivel, int total, string nivelDetalle, string opciones, int idRespuesta, string supervisor, string valor, bool lectura, bool supervisa, string valorSuper,int idEmpleado)
        {
            string niveldet = "";
            foreach (string det in nivelDetalle.Split('*'))
            {
                if (!string.IsNullOrEmpty(det))
                    niveldet += "<p class=\"text-info\">*" + det + "</p>";

            }
            string result = " <div id=\"item" + indice.ToString() + "\" class=\"itemDiv\"> " +
     "   <div class=\"row\">" +
      "      <div class=\"col-md-11\">" +
       "         <h5>" +
        "            <label class=\"text-info\">" +
         subtitulo +
         "</label></h5>" +
         "   </div>" +
 "       </div>" +
  "      <div class=\"row\">" +
   "         <div class=\"col-md-11\">" +
    "            <div class=\"panel panel-info\">" +
     "               <div class=\"panel-heading\">" +
      "                  <label class=\"text-info\">" +
       concepto +
       "</label>" +
        "            </div>" +
         "           <div class=\"panel-body\">" +
          "              <p class=\"text-info\">" +
                       indice.ToString() + "- " + detalle +
            "            </p>" +
             "       </div>" +
              "  </div>" +
            "</div>" +
        "</div>" +
       " <div class=\"row\">" +
        "    <div class=\"col-md-4\">" +
         "       <div class=\"panel panel-info\" style=\"min-height: 360px;\">" +
          "          <div class=\"panel-heading\">" +
           "             <label class=\"text-info\">" +
            nivel +
            "</label>" +
             "       </div>" +
              "      <div class=\"panel-body\">" +
             niveldet +
                  "  </div>" +
               " </div>" +
            "</div>" +
           " <div class=\"col-md-7\">" +
            "    <div class=\"panel panel-info\">" +
             "       <div class=\"panel-heading\">" +
              "          <label class=\"text-info\">" +
               opciones +
               "</label>" +
                "    </div>" +
                 "   <div class=\"panel-body\">" +
                  "      <table>" +
                   "         <thead>" +
                    "            <tr>" +
                    ((supervisa) ? "<th style=\"width: 4%;\" class=\"fondoSupervisor\"><p class=\"text-info letrachica text-center\">A evaluar</p></th>" +
                                        "<th style =\"width: 4%;\"><p class=\"text-info letrachica text-center\">Autoevaluación</p></th>" +
                        "              <th style=\"width: 8%;\">" +
                       "                 <p class=\"text-info letrachica text-center\">Nível</p>" +
                        "            </th>" +
                         "           <th style=\"width: 20%;\">" +
                          "              <p class=\"text-info letrachica text-center\">Nível de Desenvolvimento</p>" +
                           "         </th>" +
                            "        <th style=\"width: 65%;\">" +
                             "           <p class=\"text-info letrachica text-center\">Frequência</p>" +
                              "      </th>"
                    : "<th style=\"width: 5%;\"></th>" +
                      "              <th style=\"width: 10%;\">" +
                       "                 <p class=\"text-info letrachica text-center\">Nível</p>" +
                        "            </th>" +
                         "           <th style=\"width: 20%;\">" +
                          "              <p class=\"text-info letrachica text-center\">Nível de Desenvolvimento</p>" +
                           "         </th>" +
                            "        <th>" +
                             "           <p class=\"text-info letrachica text-center\">Frequência</p>" +
                              "      </th>") +

                               " </tr>" +
                          "  </thead>" +
                           " <tr>" +
                           ((supervisa) ? "<td class=\"fondoSupervisor\"> " +
                             "       <input type=\"radio\" " + ((valorSuper == "1") ? "checked" : "") + ((lectura) ? " disabled" : "") + " class=\"radioActivo\"  onchange=\"javascript:marcarValor('1','" + indice.ToString() + "');\" name=\"pruebaSuper" + idRespuesta.ToString() + "\" />" +
                              "  </td>" : "") +
                            "    <td>" +
                             "       <input type=\"radio\" " + ((valor == "1") ? "checked" : "") + ((lectura || supervisa) ? " disabled" : "") + " onchange=\"javascript:marcarValor('1','" + indice.ToString() + "');\" name=\"prueba" + idRespuesta.ToString() + "\" />" +
                              "  </td>" +
                               " <td>" +
                                "    <p class=\"text-info  letrachica\">Nível 1</p>" +
                           "     </td>" +
                            "    <td>" +
                             "       <p class=\"text-info letrachica\">Ausência de competência</p>" +
                              "  </td>" +
                               " <td>" +
                                "    <p class=\"text-info letrachica\">Ausência das competências ou presença muito básica- mostra-se de forma excepcional em alguns momentos críticos do desempenho.Precisa de um alto grau de desenvolvimento.</p>" +
                            "    </td>" +
                            "</tr>" +
                            "<tr>" +
                             ((supervisa) ? "<td class=\"fondoSupervisor\"> " +
                             "       <input type=\"radio\" " + ((valorSuper == "2") ? "checked" : "") + ((lectura) ? " disabled" : "") + " class=\"radioActivo\" onchange=\"javascript:marcarValor('2','" + indice.ToString() + "');\" name=\"pruebaSuper" + idRespuesta.ToString() + "\" />" +
                              "  </td>" : "") +
                            "    <td>" +
                            "        <input type=\"radio\" " + ((valor == "2") ? "checked" : "") + ((lectura || supervisa) ? " disabled" : "") + " onchange=\"javascript:marcarValor('2','" + indice.ToString() + "');\" name=\"prueba" + idRespuesta.ToString() + "\" />" +
                            "    </td>" +
                            "    <td>" +
                            "        <p class=\"text-info letrachica\">Nível 2</p>" +
                            "    </td>" +
                            "    <td>" +
                            "        <p class=\"text-info letrachica\">Área de melhoria</p>" +
                            "    </td>" +
                            "    <td>" +
                            "        <p class=\"text-info letrachica\">A competência está presente de forma básica, e mostra-se em alguns momentos durante o desempenho de posto.Precisa de desenvolvimento.</p>" +
                            "    </td>" +
                            "</tr>" +
                            "<tr>" +
                              ((supervisa) ? "<td class=\"fondoSupervisor\"> " +
                             "       <input type=\"radio\" " + ((valorSuper == "3") ? "checked" : "") + ((lectura) ? " disabled" : "") + " class=\"radioActivo\" onchange=\"javascript:marcarValor('3','" + indice.ToString() + "');\" name=\"pruebaSuper" + idRespuesta.ToString() + "\" />" +
                              "  </td>" : "") +
                            "    <td>" +
                            "        <input type=\"radio\" " + ((valor == "3") ? "checked" : "") + ((lectura || supervisa) ? " disabled" : "") + " onchange=\"javascript:marcarValor('3','" + indice.ToString() + "');\" name=\"prueba" + idRespuesta.ToString() + "\" />" +
                            "    </td>" +
                            "    <td>" +
                            "        <p class=\"text-info letrachica\">Nível 3</p>" +
                            "    </td>" +
                            "    <td>" +
                             "       <p class=\"text-info letrachica\">Em desenvolvimento</p>" +
                             "   </td>" +
                             "   <td>" +
                             "       <p class=\"text-info letrachica\">A competência está presente e na fase de desenvolvimento: mostra-se  de forma habitual, mas existem situações em que ainda não se implementa. Precisa de uma melhoria.</p>" +
                             "   </td>" +
                            "</tr>" +
                            "<tr>" +
                              ((supervisa) ? "<td class=\"fondoSupervisor\"> " +
                             "       <input type=\"radio\" " + ((valorSuper == "4") ? "checked" : "") + ((lectura) ? " disabled" : "") + " class=\"radioActivo\" onchange=\"javascript:marcarValor('4','" + indice.ToString() + "');\" name=\"pruebaSuper" + idRespuesta.ToString() + "\" />" +
                              "  </td>" : "") +
                            "    <td>" +
                            "        <input type=\"radio\" " + ((valor == "4") ? "checked" : "") + ((lectura || supervisa) ? " disabled" : "") + " onchange=\"javascript:marcarValor('4','" + indice.ToString() + "');\" name=\"prueba" + idRespuesta.ToString() + "\" />" +
                            "    </td>" +
                             "   <td>" +
                             "       <p class=\"text-info letrachica\">Nível 4</p>" +
                             "   </td>" +
                             "   <td>" +
                             "       <p class=\"text-info letrachica\">Avançada</p>" +
                             "   </td>" +
                             "   <td>" +
                             "       <p class=\"text-info letrachica\">A competência  mostra –se quase sempre. Está em um nível avançado de desenvolvimento.</p>" +
                             "   </td>" +
                            "</tr>" +
                            "<tr>" +
                              ((supervisa) ? "<td class=\"fondoSupervisor\"> " +
                             "       <input type=\"radio\" " + ((valorSuper == "5") ? "checked" : "") + ((lectura) ? " disabled" : "") + " class=\"radioActivo\" onchange=\"javascript:marcarValor('5','" + indice.ToString() + "');\" name=\"pruebaSuper" + idRespuesta.ToString() + "\" />" +
                              "  </td>" : "") +
                            "    <td>" +
                            "        <input type=\"radio\" " + ((valor == "5") ? "checked" : "") + ((lectura || supervisa) ? " disabled" : "") + " onchange=\"javascript:marcarValor('5','" + indice.ToString() + "');\" name=\"prueba" + idRespuesta.ToString() + "\" />" +
                            "    </td>" +
                            "    <td>" +
                            "        <p class=\"text-info letrachica\">Nível 5</p>" +
                            "    </td>" +
                            "    <td>" +
                            "        <p class=\"text-info letrachica\">Excelente</p>" +
                            "    </td>" +
                            "    <td>" +
                            "        <p class=\"text-info letrachica\">A competência não só se mostra sempre ,mas também habitualmente ,para as exigências  do posto, está em níveis de excelência.</p>" +
                            "    </td>" +
                            "</tr>" +
                        "</table>" +
                   " </div>" +
                "</div>" +
            "</div>" +
        "</div>" +
        "<div class=\"row\">" +
        "    <div class=\"col-md-11\">" +
         "            <input type=\"hidden\" id=\"id" + indice.ToString() + "\" value=\"" + idRespuesta.ToString() + "\" />" +
          "            <input type=\"hidden\" id=\"valor" + indice.ToString() + "\"/>" +
        ((lectura)?"":"        <a href=\"#\" class=\"btn btn-info pull-right\" onclick=\"javascript:var result=responderCompetenecia('" + indice + "','" + supervisor + "','NO'); if(result)alert('La evaluación fue guardada correctamente');\" role=\"button\">Salvar</a>") +
        "        <a href=\"" + ((indice == total) ? " /oportunidades.aspx" : "#") + "\" class=\"btn btn-info pull-right\" onclick=\"javascript:responderCompetenecia('" + indice + "','" + supervisor + "','NO'); " + ((indice == total) ? "" : "siguiente('" + (indice + 1).ToString() + "');") + "\" role=\"button\">Seguinte</a>" +
        "        <a href=\"" + ((indice == 1) ? " /responsabilidades.aspx" : "#") + "\" class=\"btn btn-info pull-right\" onclick=\"javascript:responderCompetenecia('" + indice + "','" + supervisor + "','NO');" + ((indice == 1) ? "" : "siguiente('" + (indice - 1).ToString() + "');") + "\" role=\"button\">Anterior</a>" +
      "        <a href=\"#\" onclick=\"javascript:toPdf('" + idRespuesta + "','" + idEmpleado + "','" + 2 + "','" + indice + "','" + ((supervisa) ? "1" : "0") + "');\" class=\"btn btn-info pull-right\" role=\"button\">Descargar</a>" +
        "    </div>" +
        "</div>" +
    "</div>";
            return result;
        }

        public static string armarCompetenciaPortuguesPDF(int indice, string subtitulo, string concepto, string detalle, string nivel, int total, string nivelDetalle, string opciones, int idRespuesta, string supervisor, string valor, bool lectura, bool supervisa, string valorSuper)
        {
            string result = " <div id=\"item" + indice.ToString() + "\" class=\"itemDiv\"> " +
     "   <div class=\"row\">" +
      "      <div class=\"col-md-11\">" +
       "            <div class=\"panel panel-info\" style=\"border: 1px solid;\">" +
     "               <div class=\"panel-heading\">" +
       "         <h5>" +
        "            <label class=\"text-info\">" +
         subtitulo +
         "</label></h5>" +
           "</div></div>" +
         "   </div>" +
 "       </div><br/>" +
  "      <div class=\"row\">" +
   "         <div class=\"col-md-11\">" +

      "                  <label class=\"text-info\"><h5>" +
       concepto +
       "</h5></label>" +
         "            <div class=\"panel panel-info\" style=\"border: 1px solid;\">" +
         "           <div class=\"panel-body\">" +
          "              <p class=\"text-info\">" +
                       indice.ToString() + "- " + detalle +
            "            </p>" +
             "       </div>" +
              "  </div>" +
            "</div>" +
        "</div>" +
       " <div class=\"row\">" +
        "    <div class=\"col-md-4\">" +
         "       <div class=\"panel panel-info\" style=\"min-height: 360px;\">" +
          "          <div class=\"panel-heading\">" +
           "             <label class=\"text-info\">" +
            nivel +
            "</label>" +
             "       </div>" +
              "      <div class=\"panel-body\">" +
               "         <p class=\"text-info\">" +
                    nivelDetalle +
                 "       </p>" +
                  "  </div>" +
               " </div>" +
            "</div>" +
           " <div class=\"col-md-7\">" +
            "    <div class=\"panel panel-info\">" +
             "       <div class=\"panel-heading\">" +
              "          <label class=\"text-info\">" +
               opciones +
               "</label>" +
                "    </div>" +
                 "   <div class=\"panel-body\">" +
                  "      <table style=\"width:300px;\">" +
                   "         <thead>" +
                    "            <tr>" +
                    ((supervisa) ? "<th style=\"width: 2%;\" class=\"fondoSupervisor\"><p class=\"text-info letrachica text-center\">A evaluar</p></th>" +
                                        "<th style =\"width: 2%;\"><p class=\"text-info letramuychica text-center\">Autoevaluación</p></th>" +
                        "              <th style=\"width: 8%;\">" +
                       "                 <p class=\"text-info letramuychica text-center\">Nível</p>" +
                        "            </th>" +
                         "           <th style=\"width: 20%;\">" +
                          "              <p class=\"text-info letramuychica text-center\">Nível de Desenvolvimento</p>" +
                           "         </th>" +
                            "        <th style=\"width: 50%;\">" +
                             "           <p class=\"text-info letramuychica text-center\">Frequência</p>" +
                              "      </th>"
                    : "<th style=\"width: 5%;\"></th>" +
                      "              <th style=\"width: 10%;\">" +
                       "                 <p class=\"text-info letramuychica text-center\">Nível</p>" +
                        "            </th>" +
                         "           <th style=\"width: 20%;\">" +
                          "              <p class=\"text-info letramuychica text-center\">Nível de Desenvolvimento</p>" +
                           "         </th>" +
                            "        <th>" +
                             "           <p class=\"text-info letramuychica text-center\">Frequência</p>" +
                              "      </th>") +

                               " </tr>" +
                          "  </thead>" +
                           " <tr>" +
                           ((supervisa) ? "<td class=\"fondoSupervisor\"> " +
                            ((valorSuper == "1") ? "<p class=\"text-info letramuychica text-center\">SI</p>" : "") +
                              "  </td>" : "") +
                            "    <td>" +
                             ((valor == "1") ? "<p class=\"text-info letramuychica text-center\">SI</p>" : "") +
                              "  </td>" +
                               " <td>" +
                                "    <p class=\"text-info  letramuychica\">Nível 1</p>" +
                           "     </td>" +
                            "    <td>" +
                             "       <p class=\"text-info letramuychica\">Ausência de competência</p>" +
                              "  </td>" +
                               " <td>" +
                                "    <p class=\"text-info letramuychica\">Ausência das competências ou presença muito básica- mostra-se de forma excepcional em alguns momentos críticos do desempenho.Precisa de um alto grau de desenvolvimento.</p>" +
                            "    </td>" +
                            "</tr>" +
                            "<tr>" +
                             ((supervisa) ? "<td class=\"fondoSupervisor\"> " +
                           ((valorSuper == "2") ? "<p class=\"text-info letramuychica text-center\">SI</p>" : "") +
                              "  </td>" : "") +
                            "    <td>" +
                            ((valor == "2") ? "<p class=\"text-info letramuychica text-center\">SI</p>" : "") +
                            "    </td>" +
                            "    <td>" +
                            "        <p class=\"text-info letramuychica\">Nível 2</p>" +
                            "    </td>" +
                            "    <td>" +
                            "        <p class=\"text-info letramuychica\">Área de melhoria</p>" +
                            "    </td>" +
                            "    <td>" +
                            "        <p class=\"text-info letramuychica\">A competência está presente de forma básica, e mostra-se em alguns momentos durante o desempenho de posto.Precisa de desenvolvimento.</p>" +
                            "    </td>" +
                            "</tr>" +
                            "<tr>" +
                              ((supervisa) ? "<td class=\"fondoSupervisor\"> " +
                            ((valorSuper == "3") ? "<p class=\"text-info letramuychica text-center\">SI</p>" : "") +
                              "  </td>" : "") +
                            "    <td>" +
                            ((valor == "3") ? "<p class=\"text-info letramuychica text-center\">SI</p>" : "") +
                            "    </td>" +
                            "    <td>" +
                            "        <p class=\"text-info letramuychica\">Nível 3</p>" +
                            "    </td>" +
                            "    <td>" +
                             "       <p class=\"text-info letramuychica\">Em desenvolvimento</p>" +
                             "   </td>" +
                             "   <td>" +
                             "       <p class=\"text-info letramuychica\">A competência está presente e na fase de desenvolvimento: mostra-se  de forma habitual, mas existem situações em que ainda não se implementa. Precisa de uma melhoria.</p>" +
                             "   </td>" +
                            "</tr>" +
                            "<tr>" +
                              ((supervisa) ? "<td class=\"fondoSupervisor\"> " +
                             ((valorSuper == "4") ? "<p class=\"text-info letramuychica text-center\">SI</p>" : "") +
                              "  </td>" : "") +
                            "    <td>" +
                            ((valor == "4") ? "<p class=\"text-info letramuychica text-center\">SI</p>" : "") +
                            "    </td>" +
                             "   <td>" +
                             "       <p class=\"text-info letramuychica\">Nível 4</p>" +
                             "   </td>" +
                             "   <td>" +
                             "       <p class=\"text-info letramuychica\">Avançada</p>" +
                             "   </td>" +
                             "   <td>" +
                             "       <p class=\"text-info letramuychica\">A competência  mostra –se quase sempre. Está em um nível avançado de desenvolvimento.</p>" +
                             "   </td>" +
                            "</tr>" +
                            "<tr>" +
                              ((supervisa) ? "<td class=\"fondoSupervisor\"> " +
                           ((valorSuper == "5") ? "<p class=\"text-info letramuychica text-center\">SI</p>" : "") +
                              "  </td>" : "") +
                            "    <td>" +
                            ((valor == "5") ? "<p class=\"text-info letramuychica text-center\">SI</p>" : "") +
                            "    </td>" +
                            "    <td>" +
                            "        <p class=\"text-info letramuychica\">Nível 5</p>" +
                            "    </td>" +
                            "    <td>" +
                            "        <p class=\"text-info letramuychica\">Excelente</p>" +
                            "    </td>" +
                            "    <td>" +
                            "        <p class=\"text-info letramuychica\">A competência não só se mostra sempre ,mas também habitualmente ,para as exigências  do posto, está em níveis de excelência.</p>" +
                            "    </td>" +
                            "</tr>" +
                        "</table>" +
                   " </div>" +
                "</div>" +
            "</div>" +
        "</div>" +
    "</div>";
            return result;
        }

        public static string listEmpleadosSupervisor(List<EvaluacionEntidades.Empleados> list, int idSupervisor)
        {
            string result = "";
            if (list != null && list.Count > 0)
            {
                result = " <table id=\"grid-data\" class=\"table table-condensed table-hover table-striped table-bordered\">" +
                          "<thead><tr><th data-column-id=\"Nombre\" data-order=\"asc\">Nombre y Apellido</th>" +
                          "<th data-column-id=\"estado\">Estado Evaluación</th>" +
                          "<th data-column-id=\"editar\" data-width=\"20%\" data-formatter=\"editar\" data-sortable=\"false\">Evaluar</th>" +
                          "<th data-column-id=\"IdEvaluacion\" data-identifier=\"true\" data-visible=\"false\" data-visible-in-selection=\"false\">Hidden</th>" +
                          "<th data-column-id =\"IdEmpleado\" data-visible=\"false\" data-visible-in-selection=\"false\">Hidden</th>" +
                          "<th data-column-id =\"estado\" data-visible=\"false\" data-visible-in-selection=\"false\">Hidden</th>" +
                            "</tr></thead><tbody>";
                foreach (EvaluacionEntidades.Empleados u in list)
                {
                    result += "<tr><td>" + u.nombreCompleto + "</td><td>" + u.estadoEvaluacion + "</td>" +
                              "<td>evaluar</td><td>" + u.IdEvaluacion.ToString() + "</td><td>" + u.IdEmpleado.ToString() + "</td><td>" + u.estadoEvaluacion.Trim() + "</td></tr>";
                }
                result += "</tbody></table>";
            }
            else result = "<p class=\"text-info\">No tiene empleados para supervisar</p>";
            return result;
        }

        public static string listResponsabilidades(List<EvaluacionEntidades.ItemsEvaluacion> list)
        {
            string result = "";
            if (list != null && list.Count > 0)
            {
                result = " <table id=\"grid-resp\" class=\"table table-condensed table-hover table-striped table-bordered\">" +
                          "<thead><tr><th data-column-id=\"NombreResp\" data-order=\"asc\" data-width=\"90%\">Descripción</th>" +
                          "<th data-column-id=\"editar\" data-formatter=\"editar\" data-sortable=\"false\">Editar</th>" +
                          "<th data-column-id =\"id\" data-visible=\"false\" data-visible-in-selection=\"false\">Hidden</th>" +
                            "</tr></thead><tbody>";
                foreach (EvaluacionEntidades.ItemsEvaluacion u in list)
                {
                    result += "<tr><td>" + u.Descripcion + "</td><td>Editar</td>" +
                              "<td>" + u.IdItem + "</td></tr>";
                }
                result += "</tbody></table>";
            }
            else result = "<p class=\"text-info pull-center\">El empleado no tiene responsabilidades</p>";
            return result;
        }

        public static string listOportunidades(List<EvaluacionEntidades.ItemsEvaluacion> list)
        {
            string result = "";
            if (list != null && list.Count > 0)
            {
                result = " <table id=\"grid-op\" class=\"table table-condensed table-hover table-striped table-bordered\">" +
                    "<thead><tr><th data-column-id=\"TipoEval\" data-order=\"asc\" data-width=\"25%\">Oportunidad</th>" +
                          "<th data-column-id=\"Nombre\" data-order=\"asc\" data-width=\"65%\">Descripción</th>" +
                          "<th data-column-id=\"editar\" data-formatter=\"editar\" data-sortable=\"false\">Editar</th>" +
                          "<th data-column-id =\"id\" data-visible=\"false\" data-visible-in-selection=\"false\">Hidden</th>" +
                            "</tr></thead><tbody>";
                foreach (EvaluacionEntidades.ItemsEvaluacion u in list)
                {
                    result += "<tr><td>" + u.tipoEvaluacion + "</td><td>" + u.Descripcion + "</td><td>Editar</td>" +
                              "<td>" + u.IdItem + "</td></tr>";
                }
                result += "</tbody></table>";
            }
            else result = "<p class=\"text-info pull-center\">El empleado no tiene Competencias</p>";
            return result;
        }

        public static string listEmpleadosAdmin(List<EvaluacionEntidades.Empleados> list)
        {
            string result = "";
            EvaluacionEntidades.Evaluacion eval;
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
            if (list != null && list.Count > 0)
            {
                result = " <table id=\"grid-data\" class=\"table table-condensed table-hover table-striped table-bordered\">" +
                          "<thead><tr><th data-column-id=\"Nombre\" data-order=\"asc\">Nombre y Apellido</th>" +
                          "<th data-column-id=\"estado\">Estado Evaluación</th>" +
                          "<th data-column-id=\"pais\">País</th>" +
                            "<th data-column-id=\"departamento\">Departamento</th>" +
                              "<th data-column-id=\"mail\">Mail</th>" +
                              "<th data-column-id=\"nivel\">Nivel</th>" +
                          "<th data-column-id=\"borrar\" data-formatter=\"borrar\" data-sortable=\"false\">Acciones</th>" +
                          "<th data-column-id=\"IdEvaluacion\" data-identifier=\"true\" data-visible=\"false\" data-visible-in-selection=\"false\">Hidden</th>" +
                          "<th data-column-id=\"Id\" data-identifier=\"true\" data-visible=\"false\" data-visible-in-selection=\"false\">Hidden</th>" +

                            "</tr></thead><tbody>";
                foreach (EvaluacionEntidades.Empleados u in list)
                {
                    eval = EvaluacionBL.EvaluacionBL.GetEvaluacion(u.IdEmpleado, desde, hasta, desdeSuper, hastaSuper);
                    result += "<tr><td><p class=\"text-info letrachica\">" + u.nombreCompleto + "</p></td><td>" + eval.Estado + "</td>" +
                              "<td><p class=\"text-info letrachica\">" + u.Pais + "</p></td>" +
                              "<td><p class=\"text-info letrachica\">" + u.Departamento + "</p></td>" +
                              "<td><p class=\"text-info letrachica\">" + u.CorreoElectronico + "</p></td>" +
                              "<td><p class=\"text-info letrachica\">" + u.Nivel + "</p></td>" +
                              "<td>Acciones</td><td>" + eval.IdEvaluacion.ToString() + "</td><td>" + u.IdEmpleado.ToString() + "</td></tr>";
                }
                result += "</tbody></table>";
            }
            else result = "<p class=\"text-info\">No tiene empleados para supervisar</p>";
            return result;
        }

        public static string getOpcionesDesempeno(bool lectura, string valor)
        {
            string lect = ((lectura) ? "disabled" : "");
            string result = "  <tr> " +
 "                                   <th style=\"width: 20%; text-align:center\" class=\"fondoSupervisor\"> <input type=\"radio\" " + lect + " " + ((valor == "1")?"checked":"") + " class=\"radioActivo\" onchange=\"javascript:marcarValor('1','1');\" name=\"pruebaSuper\" /></th>" +
 "                                   <th style=\"width: 20%; text-align:center\" class=\"fondoSupervisor\">" +
 "                                       <input type=\"radio\" " + lect + " onchange=\"javascript:marcarValor('2','1');\" " + ((valor == "2") ? "checked" : "") + " class=\"radioActivo\" name=\"pruebaSuper\" />" +
 "                                   </th>" +
 "                                   <th style=\"width: 20%; text-align:center\" class=\"fondoSupervisor\">" +
 "                                       <input type=\"radio\" " + lect + " onchange=\"javascript:marcarValor('3','1');\" " + ((valor == "3") ? "checked" : "") + " class=\"radioActivo\" name=\"pruebaSuper\" />" +
 "                                   </th>" +
 "                                   <th style=\"width: 20%; text-align:center\" class=\"fondoSupervisor\">" +
 "                                       <input type=\"radio\" " + lect + " onchange=\"javascript:marcarValor('4','1');\" " + ((valor == "4") ? "checked" : "") + " class=\"radioActivo\" name=\"pruebaSuper\" />" +
 "                                   </th>" +
 "                                   <th style=\"width: 20%; text-align:center\" class=\"fondoSupervisor\">" +
 "                                       <input type=\"radio\" " + lect + " onchange=\"javascript:marcarValor('5','1');\" " + ((valor == "5") ? "checked" : "") + " class=\"radioActivo\" name=\"pruebaSuper\" />" +
 "                                   </th>" +
 "                               </tr>";
            return result;
        }

        public static string mailAutoevaluado(int idEvaluacion)
        {

            string result = "";

            EvaluacionEntidades.Evaluacion eval = EvaluacionBL.EvaluacionBL.GetEvaluacion(idEvaluacion:idEvaluacion);
            EvaluacionEntidades.Empleados emp = EvaluacionBL.EmpleadosBL.getEmpleado(eval.IdEmpleado);
            if (emp.Pais != "Brasil")
                result = "<html><body><table height=\"100%\" style=\"width:80%;\"><tr><td><img style=\"width:100%;\" src='cid:imagen'/></td></tr><tr><td><p>Estimado/a Colaborador,</p></br><p>Te informo que " + emp.nombreCompleto + " ha finalizado la etapa de Autoevaluación de su Evaluación de Desempeño, te invito a ingresar a la herramienta para evaluarlo/la y completar el proceso." +
                        "</p></br></br><p>Atentamente</p></br><p>Dirección de Recursos Humanos</p></td></tr><tr><td><img style=\"width:100%;\" src='cid:imagenfooter'/></td></tr></table></body></html>";
            else
                result = "<html><body><table height=\"100%\" style=\"width:80%;\"><tr><td><img style=\"width:100%;\" src='cid:imagen'/></td></tr><tr><td><p>Prezado/a Colaborador,</p></br><p>Informamos-lhe que " + emp.nombreCompleto + " finalizou  sua etapa de   Auto-avaliação da sua Avaliação de Desempenho. Convidamos você a acessar à ferramenta para avaliá-lo/la e completar o processo" +
                        "</p></br></br><p>Atenciosamente,</p></br><p>Direção de Recursos Humanos</p></td></tr><tr><td><img style=\"width:100%;\" src='cid:imagenfooter'/></td></tr></table></body></html>";
            return result;
        }

        public static string mailSupervisor(int idEvaluacion)
        {

            string result = "";

            EvaluacionEntidades.Evaluacion eval = EvaluacionBL.EvaluacionBL.GetEvaluacion(idEvaluacion: idEvaluacion);
            EvaluacionEntidades.Empleados emp = EvaluacionBL.EmpleadosBL.getEmpleado(eval.IdEmpleado);
            emp = EvaluacionBL.EmpleadosBL.getEmpleado(emp.SupervisorID);
            if (emp.Pais != "Brasil")
                result = "<html><body><table height=\"100%\" style=\"width:80%;\"><tr><td><img style=\"width:100%;\" src='cid:imagen'/></td></tr><tr><td><p>Estimado/a Colaborador,</p></br><p>Te informo que " + emp.nombreCompleto + " ha finalizado la calificación de tu Evaluación de Desempeño, te invito a tener una reunión con él/ella con el objetivo de conversar sobre la misma." +
                        "</p></br></br><p>Atentamente</p></br><p>Dirección de Recursos Humanos</p></td></tr><tr><td><img style=\"width:100%;\" src='cid:imagenfooter'/></td></tr></table></body></html>";
            else
                result = "<html><body><table height=\"100%\" style=\"width:80%;\"><tr><td><img style=\"width:100%;\" src='cid:imagen'/></td></tr><tr><td><p>Prezado/a Colaborador,</p></br><p>Informamos-lhe que " + emp.nombreCompleto + " finalizou a qualificação da sua Avaliação de Desempenho. Convidamos você para ter uma reunião com ele/ela, com o intuito de conversar a esse respeito." +
                        "</p></br></br><p>Atenciosamente,</p></br><p>Direção de Recursos Humanos</p></td></tr><tr><td><img style=\"width:100%;\" src='cid:imagenfooter'/></td></tr></table></body></html>";
            return result;
        }

        public static string mailSupervisor2(int idEvaluacion)
        {

            string result = "";

            EvaluacionEntidades.Evaluacion eval = EvaluacionBL.EvaluacionBL.GetEvaluacion(idEvaluacion: idEvaluacion);
            EvaluacionEntidades.Empleados emp = EvaluacionBL.EmpleadosBL.getEmpleado(eval.IdEmpleado);
            EvaluacionEntidades.Empleados sup = EvaluacionBL.EmpleadosBL.getEmpleado(emp.SupervisorID);
            if (emp.Pais != "Brasil")
                result = "<html><body><table height=\"100%\" style=\"width:80%;\"><tr><td><img style=\"width:100%;\" src='cid:imagen'/></td></tr><tr><td><p>Estimado/a " + sup.nombreCompleto + ",</p></br><p>Has concluido la etapa de evaluación de tu colaborador/a " + emp.nombreCompleto + ", te invitamos a que tengas una reunión de feedback para cerrar el proceso y conversar sobre su calificación y oportunidades de mejora. Esta instancia es clave en el proceso de evaluación de desempeño para conocer de forma directa las expectativas de él/ella sobre su desempeño y la posición." +
                        "</p></br></br><p>Atentamente</p></br><p>Dirección de Recursos Humanos</p></td></tr><tr><td><img style=\"width:100%;\" src='cid:imagenfooter'/></td></tr></table></body></html>";
            else
                result = "<html><body><table height=\"100%\" style=\"width:80%;\"><tr><td><img style=\"width:100%;\" src='cid:imagen'/></td></tr><tr><td><p>Prezado/a " + sup.nombreCompleto + ",</p></br><p>Tendo concluído a etapa de avaliação de seu colaborador/sua " + emp.nombreCompleto + ", convidamos você a ter uma reunião de feedback para encerrar o processo e conversar sobre a qualificação dada e oportunidades de melhoria. Esta instância é chave no processo de avaliação de desempenho para conhecer de forma direta as expectativas dele/dela a respeito de seu desempenho e posição." +
                    "</p></br></br><p>Atenciosamente</p></br><p>Direção de Recursos Humanos</p></td></tr><tr><td><img style=\"width:100%;\" src='cid:imagenfooter'/></td></tr></table></body></html>";
            return result;
        }

        public static string mailAdministrador(int idEvaluacion)
        {

            string result = "";

            EvaluacionEntidades.Evaluacion eval = EvaluacionBL.EvaluacionBL.GetEvaluacion(idEvaluacion: idEvaluacion);
            EvaluacionEntidades.Empleados emp = EvaluacionBL.EmpleadosBL.getEmpleado(eval.IdEmpleado);
            result = "<html><body><table height=\"100%\" style=\"width:80%;\"><tr><td><img style=\"width:100%;\" src='cid:imagen'/></td></tr><tr><td><p>Su evaluación ha sido habilitada para poder modificarla el " + eval.FechaEstado + ".</p></td></tr><tr><td><img style=\"width:100%;\" src='cid:imagenfooter'/></td></tr></table></body></html>";
            return result;
        }

        public static string estadosEvaluacionesBarras(List<EvaluacionEntidades.Empleados> list)
        {
            string result = "";
            if (list != null && list.Count > 0)
            {
                int auto = 0;
                int enviada = 0;
                int final = 0;

                var aux = list.Where(e => e.estadoEvaluacion.ToUpper() == "AUTOEVALUACIÓN");
                if (aux != null) auto = aux.Count();
                aux = list.Where(e => e.estadoEvaluacion.ToUpper() == "ENVIADO AL SUPERVISOR");
                if (aux != null) enviada = aux.Count();
                aux = list.Where(e => e.estadoEvaluacion.ToUpper() == "FINALIZADA");
                string finales = "";
                if (aux != null)
                {
                    final = aux.Count();
                    aux.ToList().ForEach(u => finales += u.IdEmpleado + "|");
                }
                result = "<div class=\"panel panel-info\"> " +
     "               <div class=\"panel-heading\">" +
     "                   <label class=\"text-info\">" +
     "                       Estados Evaluaciones (" + list.Count + " evaluaciones)</label>" +
     "               </div>" +
     "               <div class=\"panel-body\">" +
     "                   <div class=\"row\">" +
     "                       <div class=\"col-md-8\">" +
     "                           <label class=\"text-info\">" +
     "                               Autoevaluación</label>" +
     "                       </div>" +
     "                       </div>" +
     "                     <div class=\"row\">" +
     "                       <div class=\"col-md-8\">" +
     "<h6><label class=\"text-info\">" + auto.ToString() + " Evaluaciones</label></h6>" +
     "                           <div class=\"progress\" style=\"height:60px;\">" +
     "                               <div class=\"progress-bar progress-bar-danger\" role=\"progressbar\" aria-valuenow=\"60\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width:" + (auto * 100 / list.Count).ToString() + "%; height:60px; font-size:36px;\">" +
     "<p style=\"margin-top:20px;\">" + (auto * 100 / list.Count).ToString() + "%</p>" +
     "                               </div>" +
     "                           </div>" +
     "                       </div>" +
     "                         </div>" +
     "                     <div class=\"row\">" +
     "                       <div class=\"col-md-8\">" +
     "                           <label class=\"text-info\">" +
     "                               Enviadas al Supervisor</label>" +
     "                       </div>" +
     "                         </div>" +
     "                     <div class=\"row\">" +
     "                       <div class=\"col-md-8\">" +
     "<h6><label class=\"text-info\">" + enviada.ToString() + " Evaluaciones</label></h6>" +
     "                           <div class=\"progress\" style=\"height:60px;\">" +
     "                               <div class=\"progress-bar progress-bar-info\" role=\"progressbar\" aria-valuenow=\"60\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width:" + (enviada * 100 / list.Count).ToString() + "%; height:60px; font-size:36px;\" >" +
     "<p style=\"margin-top:20px;\">" + (enviada * 100 / list.Count).ToString() + "%</p>" +
     "                               </div>" +
     "                           </div>" +
     "                       </div>" +
     "                   </div>" +
     "                   <div class=\"row\">" +
     "                       <div class=\"col-md-8\">" +
     "                           <label class=\"text-info\">" +
     "                               Finalizadas</label>" +
     "<input type=\"hidden\" value=\"" + finales + "\" id=\"hdfinalizados\"/>" +
     "                       </div>" +
     "                       </div>" +
     "                   <div class=\"row\">" +
     "                       <div class=\"col-md-8\">" +
     "<h6><label class=\"text-info\">" + final.ToString() + " Evaluaciones</label></h6>" +
     "                          <div class=\"progress\" style=\"height:60px;\">" +
      "                              <div class=\"progress-bar progress-bar-success\" role=\"progressbar\" aria-valuenow=\"60\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width:" + (final * 100 / list.Count).ToString() + "%; height:60px; font-size:36px;\" >" +
      "<p style=\"margin-top:20px;\">" + (final * 100 / list.Count).ToString() + "%</p>" +
      "                              </div>" +
      "                          </div>" +
      "                      </div>" +
      "                  </div>" +
      "          </div>" +
      "              </div>";
            }
        return result;
        }
    }




    
}
