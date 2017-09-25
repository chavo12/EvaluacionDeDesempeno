using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AjaxExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DateTime? inicio = null;
            if (!string.IsNullOrEmpty(Request.Params["inicio"])) inicio = DateTime.Parse(Request.Params["inicio"]);
            DateTime? fin = null;
            if (!string.IsNullOrEmpty(Request.Params["fin"])) fin = DateTime.Parse(Request.Params["fin"]);
            int? idSuper = null;
            if (!string.IsNullOrEmpty(Request.Params["supervisorid"])) idSuper = int.Parse(Request.Params["supervisorid"]);
            var list = EvaluacionBL.EmpleadosBL.GetEmpleadoAdmin(int.Parse(Request.Params["idadmin"]), Request.Params["pais"], inicio, fin, Request.Params["departamento"], Request.Params["estado"], idSuper);
            if (list != null && list.Count > 0)
            {
                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();

                //Tipo de contenido para forzar la descarga
                response.ContentType = "application/octet-stream";
                response.AddHeader("Content-Disposition", "attachment; filename=" + "EmpleadosDesempeño.xlsx");
                System.IO.MemoryStream grilla = Helpers.funcionesGenerales.ConvierteCSVEmpleados(list);
                //byte[] buffer = new byte[grilla.Length + 1];
                //buffer = System.Text.Encoding.ASCII.GetBytes(grilla);
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