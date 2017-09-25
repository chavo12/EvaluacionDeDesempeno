using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SelectPdf;

public partial class pdfoutresp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlToPdf converter = new HtmlToPdf();

        // create a new pdf document converting an url
        PdfDocument doc = converter.ConvertUrl("/pdfresponsabilidad?pdfauth=" + Request.QueryString["pdfauth"].ToString() + "&userName=" + Request.QueryString["userName"].ToString());

        // save pdf document
        doc.Save(Response, false, "responsabilidad.pdf");

        // close pdf document
        doc.Close();
    }
}