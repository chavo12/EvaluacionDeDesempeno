using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Helpers;
using EvaluacionEntidades;
using EvaluacionBL;
public partial class logon : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["Dominio"]) ||  string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["LDAP"]))
            {
                txtDomain.Visible = false;
            }
        }
        catch (Exception)
        {

            txtDomain.Visible = true;
        }
    }

   protected void Login_Click(object sender, EventArgs e)
    {
        bool ingresoCorrecto = false;
        Empleados emp = EvaluacionBL.EmpleadosBL.getEmpleado(correoElectronico: txtUsername.Text);
        if (emp != null)
        {
            if (emp.TipoEmpleado.Contains("ADMINISTRADOR"))
            {
                if (string.IsNullOrEmpty(emp.Ingreso) || emp.Ingreso == txtPassword.Text)
                {
                    ingresoCorrecto = true;
                    Session["Admin"] = emp.IdEmpleado;
                }
                else ingresoCorrecto = false;
            }


            if (ingresoCorrecto)
            {
                Response.Redirect("/homeadmin.aspx", false);
            }
            else
            {
                errorLabel.Text = "Authentication did not succeed. Check user name and password.";
            }

        }
        else
        {
            errorLabel.Text = "Authentication did not succeed. Check user name and password.";
        }
        //if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["LDAP"]))
        //{
        //    string adPath = System.Configuration.ConfigurationManager.AppSettings["LDAP"]; //Path to your LDAP directory server
        //                                                                                   //string adPath = string.Format("WinNT://{0}", Environment.MachineName); 
        //    LdapAuthentication adAuth = new LdapAuthentication(adPath);
        //    try
        //    {
        //        string domain = ((!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["Dominio"])) ? System.Configuration.ConfigurationManager.AppSettings["Dominio"] : txtDomain.Text);
        //        if (true == adAuth.IsAuthenticated(domain, txtUsername.Text, txtPassword.Text))
        //        {
        //            Empleados emp = EvaluacionBL.EmpleadosBL.getEmpleado(correoElectronico: txtUsername.Text);
        //            if (emp != null)
        //            {
        //                if (emp.TipoEmpleado.Contains("ADMINISTRADOR")) Session["Admin"] = emp.IdEmpleado;
        //                string groups = adAuth.GetGroups();

        //                //Create the ticket, and add the groups.
        //                bool isCookiePersistent = true;
        //                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1,
        //                          txtUsername.Text, DateTime.Now, DateTime.Now.AddMinutes(60), isCookiePersistent, groups);

        //                //Encrypt the ticket.
        //                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

        //                //Create a cookie, and then add the encrypted ticket to the cookie as data.
        //                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

        //                if (true == isCookiePersistent)
        //                    authCookie.Expires = authTicket.Expiration;

        //                //Add the cookie to the outgoing cookies collection.
        //                Response.Cookies.Add(authCookie);

        //                //You can redirect now.
        //                Response.Redirect(FormsAuthentication.GetRedirectUrl(txtUsername.Text, false));
        //            }
        //            else errorLabel.Text = "No se requiere que realice la Evaluación de Desempeño para el Ejercicio 2016";
        //        }
        //        else
        //        {
        //            Empleados emp = EvaluacionBL.EmpleadosBL.getEmpleado(correoElectronico: txtUsername.Text);
        //            if (emp.TipoEmpleado.Contains("ADMINISTRADOR") && txtPassword.Text == "admin") Session["Admin"] = emp.IdEmpleado;
        //            else errorLabel.Text = "Authentication did not succeed. Check user name and password.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        try
        //        {
        //            Empleados emp = EvaluacionBL.EmpleadosBL.getEmpleado(correoElectronico: txtUsername.Text);
        //            if (emp.TipoEmpleado.Contains("ADMINISTRADOR") && txtPassword.Text == "admin")
        //            {
        //                Session["Admin"] = emp.IdEmpleado;
        //                bool isCookiePersistent = true;
        //                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1,
        //                          txtUsername.Text, DateTime.Now, DateTime.Now.AddMinutes(60), isCookiePersistent, "Demo");

        //                //Encrypt the ticket.
        //                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

        //                //Create a cookie, and then add the encrypted ticket to the cookie as data.
        //                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

        //                if (true == isCookiePersistent)
        //                    authCookie.Expires = authTicket.Expiration;

        //                //Add the cookie to the outgoing cookies collection.
        //                Response.Cookies.Add(authCookie);
        //                Response.Redirect("/homeadmin.aspx", false);
        //            }
        //            else errorLabel.Text = "Error authenticating. " + ex.Message;
        //        }
        //        catch (Exception)
        //        {

        //            errorLabel.Text = "Error authenticating. " + ex.Message;
        //        }

        //    }
        //}
        //else
        //{
        //    Empleados emp = EvaluacionBL.EmpleadosBL.getEmpleado(correoElectronico: txtUsername.Text);
        //    if (emp != null)
        //    {
        //        if (emp.TipoEmpleado.Contains("ADMINISTRADOR")) Session["Admin"] = emp.IdEmpleado;

        //        //Create the ticket, and add the groups.
        //        bool isCookiePersistent = true;
        //        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1,
        //                  txtUsername.Text, DateTime.Now, DateTime.Now.AddMinutes(60), isCookiePersistent, "Demo");

        //        //Encrypt the ticket.
        //        string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

        //        //Create a cookie, and then add the encrypted ticket to the cookie as data.
        //        HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

        //        if (true == isCookiePersistent)
        //            authCookie.Expires = authTicket.Expiration;

        //        //Add the cookie to the outgoing cookies collection.
        //        Response.Cookies.Add(authCookie);

        //        //You can redirect now.
        //        Response.Redirect(FormsAuthentication.GetRedirectUrl(txtUsername.Text, false));

        //    }
        //    else
        //    {
        //        errorLabel.Text = "Authentication did not succeed. Check user name and password.";
        //    }
        //}
    }
}