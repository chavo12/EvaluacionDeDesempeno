using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AjaxGuardarEmpleado : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string result;
            DateTime? inicio = null;
            DateTime? fin = null;
            DateTime? inicioSuper = null;
            DateTime? finsSuper = null;
            if (!string.IsNullOrEmpty(Request.Params["inicio"])) inicio = DateTime.Parse(Request.Params["inicio"]);
            if (!string.IsNullOrEmpty(Request.Params["fin"])) fin = DateTime.Parse(Request.Params["fin"]);
            if (!string.IsNullOrEmpty(Request.Params["inicioSuper"])) inicioSuper = DateTime.Parse(Request.Params["inicioSuper"]);
            if (!string.IsNullOrEmpty(Request.Params["finSuper"])) finsSuper = DateTime.Parse(Request.Params["finSuper"]);

            DateTime ingreso;
            string fechaIngreso;
            if (DateTime.TryParse(Request.Params["ingreso"], out ingreso))
            {
                fechaIngreso = ingreso.ToShortDateString();
            }
            else fechaIngreso = "";


            EvaluacionEntidades.Empleados emp = new EvaluacionEntidades.Empleados
            {
                IdEmpleado = int.Parse(Request.Params["idEmpleado"]),
                EmpleadoId = Request.Params["empleadoid"],
                Cargo = Request.Params["cargo"],
                CorreoElectronico = Request.Params["correo"],
                Departamento = Request.Params["departamento"],
                Ingreso = fechaIngreso,
                Negocio = Request.Params["negocio"],
                Nombre = Request.Params["nombre"],
                NumPia = Request.Params["numpia"],
                Pais = Request.Params["pais"],
                PApellido = Request.Params["papellido"],
                SApellido = Request.Params["sapellido"],
                SupervisorID = int.Parse(Request.Params["supervisor"]),
                TipoEmpleado = Request.Params["tipoempleado"],
                Rol = Request.Params["rol"],
                inicio = inicio,
                Nivel = Request.Params["nivel"],
                fin  = fin,
                inicioSuper= inicioSuper,
                finSuper = finsSuper
            };
            if (emp.IdEmpleado == 0)
            {
                if (Session["listResp"] != null)
                {
                    List<EvaluacionEntidades.ItemsEvaluacion> listResp = (List<EvaluacionEntidades.ItemsEvaluacion>)Session["listResp"];
                    if (listResp.Count > 0)
                    {
                        if (Session["listOp"] != null)
                        {
                            List<EvaluacionEntidades.ItemsEvaluacion> listOp = (List<EvaluacionEntidades.ItemsEvaluacion>)Session["listOp"];
                            if (listOp.Count > 0)
                            {
                                listResp.ForEach(r => { r.Nivel = emp.NumPia; r.Idioma = ((emp.Pais == "Brasil") ? "por" : "es"); r.IdItem = 0; EvaluacionBL.ItemsEvaluacionBL.GuardarResponsabilidad(r); });
                                listOp.ForEach(r => { r.Nivel = emp.NumPia; r.Idioma = ((emp.Pais == "Brasil") ? "por" : "es"); r.idTipoEvalucion = ((emp.Pais == "Brasil") ? Helpers.funcionesGenerales.oportunidadesPortugues(r.idTipoEvalucion) : r.idTipoEvalucion); r.IdItem = 0; EvaluacionBL.ItemsEvaluacionBL.GuardarCompetencias(r); });
                                EvaluacionBL.EmpleadosBL.GuardarEmpleados(emp);
                                var list = EvaluacionBL.EmpleadosBL.GetEmpleadoAdmin(int.Parse(Request.Params["idAdmin"]));
                                result = Helpers.html.listEmpleadosAdmin(list);
                            }
                            else result = "oprtunidad";
                        }
                        else result = "oportunidad";
                    }
                    else result = "responsabilidad";
                }
                else result = "responsabilidad";
            }
            else
            {
                EvaluacionBL.EmpleadosBL.GuardarEmpleados(emp);
                var list = EvaluacionBL.EmpleadosBL.GetEmpleadoAdmin(int.Parse(Request.Params["idAdmin"]));
                result = Helpers.html.listEmpleadosAdmin(list);
            }
            Response.Write(result);
            
        }
        catch (Exception ex)
        {
            EvaluacionBL.LogsBL.SetLog(0, Request.Url.ToString(),Context.User.Identity.Name);
            Response.Write(ex.Message);
        }
        Response.End();
    }
}