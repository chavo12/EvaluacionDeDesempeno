using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;
using System.IO;
using System.Net.Mime;
using System.Net.Mail;
using OfficeOpenXml;
using OfficeOpenXml.Style;




namespace Helpers
{
    public class funcionesGenerales
    {

        public static List<Item> listarReguiones()
        {
            List<Item> list = new List<Item>();
            list.Add(new Item("usa", "EE. UU."));
            list.Add(new Item("arg", "Argentina"));
            list.Add(new Item("br", "Brasil"));
            list.Add(new Item("ch", "Chile"));
            list.Add(new Item("co", "Colombia"));
            list.Add(new Item("ec", "Ecuador"));
            list.Add(new Item("sv", "El Salvador"));
            list.Add(new Item("gu", "Guatemala"));
            list.Add(new Item("mx", "Mexico"));
            list.Add(new Item("pa", "Panamá"));
            list.Add(new Item("pe", "Perú"));
            list.Add(new Item("ur", "Uruguay"));
            return list;
        }

        public static List<Item> listarReguiones2()
        {
            List<Item> list = new List<Item>();
            list.Add(new Item("usa", "USA"));
            list.Add(new Item("arg", "Argentina"));
            list.Add(new Item("br", "Brasil"));
            list.Add(new Item("ch", "Chile"));
            list.Add(new Item("co", "Colombia"));
            list.Add(new Item("cr", "Costa Rica"));
            list.Add(new Item("ec", "Ecuador"));
            list.Add(new Item("sv", "El Salvador"));
            list.Add(new Item("es", "España"));
            list.Add(new Item("gu", "Guatemala"));
            list.Add(new Item("mx", "México"));
            list.Add(new Item("pa", "Panamá"));
            list.Add(new Item("pe", "Perú"));
            list.Add(new Item("ur", "Uruguay"));
            list.Add(new Item("ni", "Nicaragua"));
            list.Add(new Item("vz", "Venezuela"));
            list.Add(new Item("ni", "República Dominicana"));
            return list;
        }

        public static List<Item> listarDepartamentos()
        {
            List<Item> list = new List<Item>();
            list.Add(new Item("Alta Dirección", "Alta Dirección"));
            list.Add(new Item("Comercial", "Comercial"));
            list.Add(new Item("Compras", "Compras"));
            list.Add(new Item("Country Manager", "Country Manager"));
            list.Add(new Item("Dirección de Negocios", "Dirección de Negocios"));
            list.Add(new Item("Finanzas", "Finanzas"));
            list.Add(new Item("Gerencia General", "Gerencia General"));
            list.Add(new Item("Legal", "Legal"));
            list.Add(new Item("Office mgt", "Office mgt"));
            list.Add(new Item("Operaciones", "Operaciones"));
            list.Add(new Item("Recursos Corporativos", "Recursos Corporativos"));
            list.Add(new Item("Seguridad", "Seguridad"));
            list.Add(new Item("Seguros", "Seguros"));
            list.Add(new Item("Sistemas", "Sistemas"));
            return list;
        }

        public static string enviarMail(int idEvaluacion,string mensaje,string asunto,string path, bool autoevaluado)
        {
            try
            {
                if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["EnviarMensajes"]) && System.Configuration.ConfigurationManager.AppSettings["EnviarMensajes"] == "SI")
                {
                    string mail;
                    EvaluacionEntidades.Evaluacion eval = EvaluacionBL.EvaluacionBL.GetEvaluacion(idEvaluacion: idEvaluacion);
                    EvaluacionEntidades.Empleados emp;
                    if (!autoevaluado) emp = EvaluacionBL.EmpleadosBL.getEmpleado(EvaluacionBL.EmpleadosBL.getEmpleado(eval.IdEmpleado).SupervisorID);
                    else emp = EvaluacionBL.EmpleadosBL.getEmpleado(eval.IdEmpleado);
                    mail = emp.CorreoElectronico;
                    System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                    System.Net.Mail.SmtpClient Servidor = new System.Net.Mail.SmtpClient();
                    try
                    {
                        string SMTP = System.Configuration.ConfigurationManager.AppSettings["SMTP"];
                        string Remitente = System.Configuration.ConfigurationManager.AppSettings["Remitente"];
                        string Contraseña = System.Configuration.ConfigurationManager.AppSettings["Contrasena"];
                        string Puerto = System.Configuration.ConfigurationManager.AppSettings["Puerto"];
                        string sender = System.Configuration.ConfigurationManager.AppSettings["emailRemitente"];
                        string EnableSsl = System.Configuration.ConfigurationManager.AppSettings["EnableSsl"];
                        var mailAdress = new System.Net.Mail.MailAddress(sender, Remitente);
                        string A = mail;
                        string Contenido = mensaje;
                        AlternateView html = AlternateView.CreateAlternateViewFromString(Contenido, Encoding.UTF8, MediaTypeNames.Text.Html);
                        LinkedResource img;
                        if (autoevaluado)
                        {
                            img = new LinkedResource(path + @"\header_autoevaluado.jpg", MediaTypeNames.Image.Jpeg);
                        }
                        else
                        {
                            img = new LinkedResource(path + @"\header_supervisores.jpg", MediaTypeNames.Image.Jpeg);
                        }
                        LinkedResource imgfooter = new LinkedResource(path + @"\base.jpg", MediaTypeNames.Image.Jpeg);
                        img.ContentId = "imagen";
                        imgfooter.ContentId = "imagenfooter";
                        html.LinkedResources.Add(img);
                        html.LinkedResources.Add(imgfooter);
                        string Asunto = asunto;
                        correo.IsBodyHtml = true;
                        correo.Subject = Asunto;
                        correo.To.Clear();
                        correo.To.Add(A);
                        correo.AlternateViews.Add(html);
                        correo.From = mailAdress;
                        correo.Sender = mailAdress;
                        Servidor.Host = SMTP;
                        Servidor.Port = ((!string.IsNullOrEmpty(Puerto)) ? int.Parse(Puerto) : 25);
                        if (!string.IsNullOrEmpty(EnableSsl) && EnableSsl == "SI") Servidor.EnableSsl = true;
                        if (!string.IsNullOrEmpty(Contraseña)) Servidor.Credentials = new System.Net.NetworkCredential(sender, Contraseña);
                        Servidor.Send(correo);

                        return "OK";
                    }

                    catch (Exception ex)
                    {
                        EvaluacionBL.LogsBL.SetLog(emp.IdEmpleado, "Envío de mail", ex.Message);
                        return ex.Message;
                    }
                }
                else return "OK";
            }
            catch (Exception ex)
            {
                EvaluacionBL.LogsBL.SetLog(0, "Envío de mail", ex.Message);
                return "OK";
            }
        }

        public static MemoryStream ConvierteCSVEmpleados(List<EvaluacionEntidades.Empleados> list)
        {
            MemoryStream result = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet ws = package.Workbook.Worksheets.Add("Empleados");

                ws.Cells["A1"].Value = "Nombre";
                ws.Cells["A1"].Style.Font.Bold = true;
                ws.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Column(1).AutoFit();
                ws.Cells["B1"].Value = "Id-Pia";
                ws.Cells["B1"].Style.Font.Bold = true;
                ws.Cells["B1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Column(2).AutoFit();
                ws.Cells["C1"].Value = "Mail";
                ws.Cells["C1"].Style.Font.Bold = true;
                ws.Cells["C1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Column(3).AutoFit();
                ws.Cells["D1"].Value = "Cargo";
                ws.Cells["D1"].Style.Font.Bold = true;
                ws.Cells["D1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Column(4).AutoFit();
                ws.Cells["E1"].Value = "Fec. Ingreso";
                ws.Cells["E1"].Style.Font.Bold = true;
                ws.Cells["E1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Column(5).AutoFit();
                ws.Cells["F1"].Value = "Departamento";
                ws.Cells["F1"].Style.Font.Bold = true;
                ws.Cells["F1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Column(6).AutoFit();
                ws.Cells["G1"].Value = "Negocio";
                ws.Cells["G1"].Style.Font.Bold = true;
                ws.Cells["G1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Column(7).AutoFit();
                ws.Cells["H1"].Value = "País";
                ws.Cells["H1"].Style.Font.Bold = true;
                ws.Cells["H1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Column(8).AutoFit();
                ws.Cells["I1"].Value = "Estado Evaluación";
                ws.Cells["I1"].Style.Font.Bold = true;
                ws.Cells["I1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Column(9).AutoFit();
                ws.Cells["J1"].Value = "Supervisor";
                ws.Cells["J1"].Style.Font.Bold = true;
                ws.Cells["J1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Column(10).AutoFit();
                ws.Cells["K1"].Value = "Mail Supervisor";
                ws.Cells["K1"].Style.Font.Bold = true;
                ws.Cells["K1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Column(11).AutoFit();
                ws.Cells["L1"].Value = "Desempeño Global Autoevaluado";
                ws.Cells["L1"].Style.Font.Bold = true;
                ws.Cells["L1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Column(12).AutoFit();
                ws.Column(12).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells["M1"].Value = "Desempeño Global Supervisor";
                ws.Cells["M1"].Style.Font.Bold = true;
                ws.Cells["M1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Column(13).AutoFit();
                ws.Column(13).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                int i = 2;
                list.ForEach(u => {
                    ws.Cells["A" + i.ToString()].Value = u.nombreCompleto; ws.Cells["B" + i.ToString()].Value = u.NumPia;
                    ws.Cells["C" + i.ToString()].Value = u.CorreoElectronico; ws.Cells["D" + i.ToString()].Value = u.Cargo; ws.Cells["E" + i.ToString()].Value = u.Ingreso;
                    ws.Cells["F" + i.ToString()].Value = u.Departamento; ws.Cells["G" + i.ToString()].Value = u.Negocio; ws.Cells["H" + i.ToString()].Value = u.Pais;
                    ws.Cells["I" + i.ToString()].Value = u.estadoEvaluacion; ws.Cells["J" + i.ToString()].Value = u.supervisor; ws.Cells["K" + i.ToString()].Value = u.mailSupervisor;
                    ws.Cells["L" + i.ToString()].Value = u.desempenoGlobal; ws.Cells["M" + i.ToString()].Value = u.desempenoGlobalSuper; i += 1;

                });

                ws.Column(1).AutoFit();
                ws.Column(2).AutoFit();
                ws.Column(3).AutoFit();
                ws.Column(4).AutoFit();
                ws.Column(5).AutoFit();
                ws.Column(6).AutoFit();
                ws.Column(7).AutoFit();
                ws.Column(8).AutoFit();
                ws.Column(9).AutoFit();
                ws.Column(10).AutoFit();
                ws.Column(11).AutoFit();
                ws.Column(12).AutoFit();
                ws.Column(13).AutoFit();

                package.SaveAs(result);


            }
            return result;
            //string mSalida = "";

            //mSalida = "\"Nombre y Apellido\";\"Estado Evaluación\";\"Pais\";\"Departamento\";\"Mail\";\"Nivel\"\n\r";
            //list.ForEach(e => mSalida += "\"" + e.nombreCompleto + "\";\"" + e.estadoEvaluacion + "\";\"" + e.Pais + "\";\"" + e.Departamento + "\";\"" + e.CorreoElectronico + "\";\"" + e.Nivel + "\"\n\r");
            //return mSalida;
        }

        //public static string ConvierteCSVEmpleados(List<EvaluacionEntidades.Empleados> list)
        //{

        //    string mSalida = "";

        //    mSalida = "\"Nombre y Apellido\";\"Estado Evaluación\";\"Pais\";\"Departamento\";\"Mail\";\"Nivel\";\"Id-Pia\";\"Cargo\";\"Fec. Ingreso\";\"Negocio\";" +
        //                "\"Estado Evaluación\";\"Supervisor\";\"Mail Supervisor\";\"Desempeño Global Autoevaluado\";\"Desempeño Globla Supervisor\"\n\r";
        //    list.ForEach(e => mSalida += "\"" + e.nombreCompleto + "\";\"" + e.estadoEvaluacion + "\";\"" + e.Pais + "\";\"" + e.Departamento + "\";\"" + e.CorreoElectronico + "\";\"" + e.Nivel + "\";" +
        //                    "\"" + e.NumPia +"\";\"" + e.Cargo +"\";\"" + e.Ingreso +"\";\"" + e.Negocio +"\";\"" + e.estadoEvaluacion +"\";\"" + e.supervisor +"\";\"" + e.mailSupervisor +"\";\"" + e.desempenoGlobal + "\";\"" + e.desempenoGlobalSuper +"\";\n\r");
        //    return mSalida;
        //}

        public static int oportunidadesPortugues(int id)
        {
            int result = 0;
            switch (id)
            {
                case 2:
                    result = 24;
                    break;
                case 5:
                    result = 25;
                    break;
                case 8:
                    result = 26;
                    break;
                case 11:
                    result = 27;
                    break;
                case 14:
                    result = 28;
                    break;
                case 17:
                    result = 29;
                    break;
                case 20:
                    result = 30;
                    break;
                case 21:
                    result = 31;
                    break;
                case 32:
                    result = 33;
                    break;
            }
            return result;
          
        }

        public static string oportunidadesDescrip(int id)
        {
            string result = "";
            switch (id)
            {
                case 2:
                    result = "Comunicación";
                    break;
                case 5:
                    result = "Gestión del Cambio";
                    break;
                case 8:
                    result = "Orientación a los Resultados";
                    break;
                case 11:
                    result = "Satisfacción al Cliente Interno / Externo";
                    break;
                case 14:
                    result = "Trabajo en Equipo";
                    break;
                case 17:
                    result = "Integridad";
                    break;
                case 20:
                    result = "Desarrollo de Personas";
                    break;
                case 21:
                    result = "Liderazgo";
                    break;
                case 32:
                    result = "Visión estratégica del Negocio";
                    break;
            }
            return result;

        }



    }

    public class Item
    {
        public string value { get; set; }
        public string descripcion { get; set; }

        public Item(string val, string descrip)
        {
            value = val;
            descripcion = descrip;
        }
    }

   

   

}
