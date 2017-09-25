using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EvaluacionBL;

public partial class AjaxResponder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["idRespuesta"] != null)
        {
            try
            {

                int id;
                string result = "";
                if (int.TryParse(Request.Params["idRespuesta"], out id))
                {
                    if (Request.Params["supervisor"].ToUpper() == "SI")
                    {
                        RespuestasEvaluacionBL.ModificarValorRespuesta(id, valorSupervisor: Request.Params["valor"], escritoSupervisor: Request.Params["escrito"]);
                    }
                    else
                    {
                        RespuestasEvaluacionBL.ModificarValorRespuesta(id, valor: Request.Params["valor"], escrito: Request.Params["escrito"]);
                    }
                    if (Request.Params["finalizar"].ToUpper() == "SI")
                    {
                        if (int.TryParse(Request.Params["idEvaluacion"], out id))
                        {
                            if (Request.Params["supervisor"].ToUpper() == "SI")
                            {
                                EvaluacionBL.EvaluacionBL.FinalizarEvaluacionSupervisor(id);
                                try
                                {
                                    Helpers.funcionesGenerales.enviarMail(id, Helpers.html.mailSupervisor(id), "Evaluación finalizada", Server.MapPath("/Content"), true);
                                    Helpers.funcionesGenerales.enviarMail(id, Helpers.html.mailSupervisor2(id), "Evaluación finalizada", Server.MapPath("/Content"), false);
                                }
                                catch (Exception ex)
                                {
                                    EvaluacionBL.LogsBL.SetLog(0, Request.Url.ToString(), ex.Message);
                                }

                            }
                            else
                            {
                                EvaluacionBL.EvaluacionBL.FinalizarEvaluacion(id);
                                try
                                {
                                    Helpers.funcionesGenerales.enviarMail(id, Helpers.html.mailAutoevaluado(id), "Evaluación lista para evaluar", Server.MapPath("/Content"), false);
                                }
                                catch (Exception ex)
                                {
                                    EvaluacionBL.LogsBL.SetLog(0, Request.Url.ToString(), ex.Message);
                                }
                                
                            }
                        }
                    }
                    result = "ok";

                }
                else result = "Error al enviar la respuesta";

                Response.Write(result);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Debe completar toda la evalucación antes de finalizarla")
                    Response.Write(ex.Message);
                else
                    if (ex.Message.IndexOf("complet") > 0) Response.Write(ex.Message);
                    else Response.Write("no ok");
            }
            Response.End();
        }
    }
}