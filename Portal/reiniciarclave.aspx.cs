using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class reiniciarclave : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["e"]) && !string.IsNullOrEmpty(Request.QueryString["r"]))
        {
            hdReset.Value = Request.QueryString["r"];
            hdEmp.Value = Request.QueryString["e"];
        }
        else Response.Redirect("/logon.aspx");
    }
}