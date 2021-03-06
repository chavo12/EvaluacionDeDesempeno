﻿<%@ Application Language="C#" %>


<script runat="server">


    void Application_Start(object sender, EventArgs e)
    {
    }

    public void Session_OnStart()
    {
        Session["idioma"] = System.Configuration.ConfigurationManager.AppSettings["idioma"].ToString().ToUpper();
    }

    protected void Application_AuthenticateRequest(object sender, EventArgs e)
    {
        // look if any security information exists for this request
        if (HttpContext.Current.User != null)
        {
            // see if this user is authenticated, any authenticated cookie (ticket) exists for this user
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                // see if the authentication is done using FormsAuthentication
                if (HttpContext.Current.User.Identity is FormsIdentity)
                {
                    // Get the roles stored for this request from the ticket
                    // get the identity of the user
                    FormsIdentity identity = (FormsIdentity)HttpContext.Current.User.Identity;
                    //Get the form authentication ticket of the user
                    FormsAuthenticationTicket ticket = identity.Ticket;
                    //Get the roles stored as UserData into ticket
                    string[] roles = { };
                    //Create general prrincipal and assign it to current request

                    HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(identity, roles);
                }
            }
        }
    }

    //public void Application_AuthenticateRequest(object sender, EventArgs e)
    //{
    //    if (!Request.Url.ToString().Contains("AAFD214E-80E3-440C-853A-8FF1308DDC4E"))
    //    {

    //        if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["LDAP"]))
    //        {
    //            string cookieName = FormsAuthentication.FormsCookieName;
    //            HttpCookie authCookie = Context.Request.Cookies[cookieName];

    //            if (null == authCookie)
    //            //if (null == authCookie)
    //            {
    //                //There is no authentication cookie.
    //                return;
    //            }
    //            FormsAuthenticationTicket authTicket;

    //            if (authCookie == null)
    //            {

    //                authTicket = new FormsAuthenticationTicket(1,
    //                         System.Security.Principal.WindowsIdentity.GetCurrent().Name, DateTime.Now, DateTime.Now.AddMinutes(60), true, System.Security.Principal.WindowsIdentity.GetCurrent().Name);

    //                //Encrypt the ticket.
    //                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

    //                //Create a cookie, and then add the encrypted ticket to the cookie as data.
    //                authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
    //            }
    //            try
    //            {
    //                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
    //            }
    //            catch (Exception ex)
    //            {
    //                //Write the exception to the Event Log.
    //                return;
    //            }
    //            if (null == authTicket)
    //            {
    //                //Cookie failed to decrypt.
    //                return;
    //            }
    //            //When the ticket was created, the UserData property was assigned a
    //            //pipe-delimited string of group names.
    //            string[] groups = authTicket.UserData.Split(new char[] { '|' });
    //            //Create an Identity.
    //            System.Security.Principal.GenericIdentity id = new System.Security.Principal.GenericIdentity(authTicket.Name, "LdapAuthentication");
    //            //This principal flows throughout the request.
    //            System.Security.Principal.GenericPrincipal principal = new System.Security.Principal.GenericPrincipal(id, groups);
    //            Context.User = principal;
    //            EvaluacionEntidades.Empleados emp = EvaluacionBL.EmpleadosBL.getEmpleado(correoElectronico: Context.User.Identity.Name);
    //            if (emp == null)
    //            {
    //                HttpContext.Current.Session.Clear();
    //                HttpContext.Current.Session.Abandon();
    //                FormsAuthentication.SignOut();
    //                return;
    //            }
    //        }
    //        else
    //        {
    //            if (!string.IsNullOrEmpty(Request.QueryString["M"]))
    //            {

    //                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
    //                                               Request.QueryString["M"],
    //                                                DateTime.Now,
    //                                                DateTime.Now.AddMinutes(30),
    //                                                true,
    //                                                Request.QueryString["M"],
    //                                                FormsAuthentication.FormsCookiePath);
    //                string[] groups = ticket.UserData.Split(new char[] { '|' });
    //                //Create an Identity.
    //                System.Security.Principal.GenericIdentity id = new System.Security.Principal.GenericIdentity(ticket.Name);
    //                //This principal flows throughout the request.
    //                System.Security.Principal.GenericPrincipal principal = new System.Security.Principal.GenericPrincipal(id, groups);
    //                Context.User = principal;
    //            }
    //            if (Context.User != null)
    //            {
    //                EvaluacionEntidades.Empleados emp = EvaluacionBL.EmpleadosBL.getEmpleado(correoElectronico: Context.User.Identity.Name);
    //                  EvaluacionBL.LogsBL.SetLog(emp.IdEmpleado, Request.Url.ToString(),Context.User.Identity.Name);
    //                if (emp == null)
    //                {
    //                    Session["mensaje"] = "No se requiere que realice la Evaluación de Desempeño para el Ejercicio 2016";
    //                    EvaluacionBL.LogsBL.SetLog(0, Request.Url.ToString(), Context.User.Identity.Name);
    //                }
    //            }
    //            else
    //            {
    //                string cookieName = FormsAuthentication.FormsCookieName;
    //                HttpCookie authCookie = Context.Request.Cookies[cookieName];

    //                if (null == authCookie)
    //                //if (null == authCookie)
    //                {
    //                    //There is no authentication cookie.
    //                    return;
    //                }
    //                FormsAuthenticationTicket authTicket;

    //                if (authCookie == null)
    //                {

    //                    authTicket = new FormsAuthenticationTicket(1,
    //                             System.Security.Principal.WindowsIdentity.GetCurrent().Name, DateTime.Now, DateTime.Now.AddMinutes(60), true, System.Security.Principal.WindowsIdentity.GetCurrent().Name);

    //                    //Encrypt the ticket.
    //                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

    //                    //Create a cookie, and then add the encrypted ticket to the cookie as data.
    //                    authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
    //                }
    //                try
    //                {
    //                    authTicket = FormsAuthentication.Decrypt(authCookie.Value);
    //                }
    //                catch (Exception ex)
    //                {
    //                    //Write the exception to the Event Log.
    //                    return;
    //                }
    //                if (null == authTicket)
    //                {
    //                    //Cookie failed to decrypt.
    //                    return;
    //                }
    //                //When the ticket was created, the UserData property was assigned a
    //                //pipe-delimited string of group names.
    //                string[] groups = authTicket.UserData.Split(new char[] { '|' });
    //                //Create an Identity.
    //                System.Security.Principal.GenericIdentity id = new System.Security.Principal.GenericIdentity(authTicket.Name, "LdapAuthentication");
    //                //This principal flows throughout the request.
    //                System.Security.Principal.GenericPrincipal principal = new System.Security.Principal.GenericPrincipal(id, groups);
    //                Context.User = principal;
    //                EvaluacionEntidades.Empleados emp = EvaluacionBL.EmpleadosBL.getEmpleado(correoElectronico: Context.User.Identity.Name);
    //                if (emp == null)
    //                {
    //                    HttpContext.Current.Session.Clear();
    //                    HttpContext.Current.Session.Abandon();
    //                    FormsAuthentication.SignOut();
    //                    return;
    //                }


    //            }
    //            //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
    //            //                                      "amontoya",
    //            //                                      DateTime.Now,
    //            //                                      DateTime.Now.AddMinutes(30),
    //            //                                      true,
    //            //                                      "amontoya",
    //            //                                      FormsAuthentication.FormsCookiePath);
    //            //string[] groups = ticket.UserData.Split(new char[] { '|' });
    //            ////Create an Identity.
    //            //System.Security.Principal.GenericIdentity id = new System.Security.Principal.GenericIdentity(ticket.Name, "LdapAuthentication");
    //            ////This principal flows throughout the request.
    //            //System.Security.Principal.GenericPrincipal principal = new System.Security.Principal.GenericPrincipal(id, groups);
    //            //Context.User = principal;
    //        }
    //    }
    //}
</script>
