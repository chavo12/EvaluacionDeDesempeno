using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class excelempleados : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            List<EvaluacionEntidades.Empleados> list;
            if (!string.IsNullOrEmpty(Request.QueryString["idadmin"]))
            {
                DateTime? inicio = null;
                if (!string.IsNullOrEmpty(Request.QueryString["inicio"])) inicio = DateTime.Parse(Request.QueryString["inicio"]);
                DateTime? fin = null;
                if (!string.IsNullOrEmpty(Request.QueryString["fin"])) fin = DateTime.Parse(Request.QueryString["fin"]);
                int? idSuper = null;
                if (!string.IsNullOrEmpty(Request.QueryString["supervisorid"])) idSuper = int.Parse(Request.QueryString["supervisorid"]);
                list = EvaluacionBL.EmpleadosBL.GetEmpleadoAdmin(int.Parse(Request.QueryString["idadmin"]), Request.QueryString["pais"], inicio, fin, Request.QueryString["departamento"], Request.QueryString["estado"], idSuper);
            }
            else
            {
                list = EvaluacionBL.EmpleadosBL.GetEmpleadosSupervisados(int.Parse(Request.QueryString["idSupervisor"]));
            }
            if (list != null && list.Count > 0)
            {
                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();

                //Tipo de contenido para forzar la descarga
                response.ContentType = "application/octet-stream";
                Response.HeaderEncoding = System.Text.Encoding.Default;
                response.AddHeader("Content-Disposition", "attachment; filename=" + "EmpleadosDesempeño.xlsx");
                System.IO.MemoryStream grilla = Helpers.funcionesGenerales.ConvierteCSVEmpleados(list);
               // System.Text.Encoding encoding = new System.Text.UTF8Encoding();
               // byte[] bufferAux = grilla.ToArray();
               // byte[] buffer;
               //System.Text.Encoding utf8 = new System.Text.UTF8Encoding();
               // System.Text.Encoding win1252 = System.Text.Encoding.GetEncoding(1252);
               // buffer = System.Text.Encoding.Convert(utf8,win1252,bufferAux);
                //while (mContador < grilla.Length)
                //{
                //    buffer[mContador] = Chr grilla.Substring(mContador,1) Strings.Asc(Strings.Mid(pCSV, mContador + 1, 1));
                //    mContador = mContador + 1;
                //}

                //Envia los bytes
                response.BinaryWrite(grilla.ToArray());
                response.End();
            }
        }
        catch (Exception ex)
        { }
    }
}